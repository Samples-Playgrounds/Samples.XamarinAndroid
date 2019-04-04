// mc++ /*
// mc++  * Copyright 2017 Google Inc. All Rights Reserved.
// mc++  * Licensed under the Apache License, Version 2.0 (the "License");
// mc++  * you may not use this file except in compliance with the License.
// mc++  * You may obtain a copy of the License at
// mc++  *
// mc++  *   http://www.apache.org/licenses/LICENSE-2.0
// mc++  *
// mc++  * Unless required by applicable law or agreed to in writing, software
// mc++  * distributed under the License is distributed on an "AS IS" BASIS,
// mc++  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// mc++  * See the License for the specific language governing permissions and
// mc++  * limitations under the License.
// mc++  */
// mc++ package com.google.ar.core.examples.java.common.rendering;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.graphics.Bitmap;
// mc++ import android.graphics.BitmapFactory;
// mc++ import android.opengl.GLES20;
// mc++ import android.opengl.GLSurfaceView;
// mc++ import android.opengl.GLUtils;
// mc++ import android.opengl.Matrix;
// mc++ import com.google.ar.core.Camera;
// mc++ import com.google.ar.core.Plane;
// mc++ import com.google.ar.core.Pose;
// mc++ import com.google.ar.core.TrackingState;
// mc++ import java.io.IOException;
// mc++ import java.nio.ByteBuffer;
// mc++ import java.nio.ByteOrder;
// mc++ import java.nio.FloatBuffer;
// mc++ import java.nio.ShortBuffer;
// mc++ import java.util.ArrayList;
// mc++ import java.util.Collection;
// mc++ import java.util.Collections;
// mc++ import java.util.Comparator;
// mc++ import java.util.HashMap;
// mc++ import java.util.List;
// mc++ import java.util.Map;
// mc++ 
// mc++ /** Renders the detected AR planes. */
// mc++ public class PlaneRenderer {
// mc++   private static final String TAG = PlaneRenderer.class.getSimpleName();
// mc++ 
// mc++   // Shader names.
// mc++   private static final String VERTEX_SHADER_NAME = "shaders/plane.vert";
// mc++   private static final String FRAGMENT_SHADER_NAME = "shaders/plane.frag";
// mc++ 
// mc++   private static final int BYTES_PER_FLOAT = Float.SIZE / 8;
// mc++   private static final int BYTES_PER_SHORT = Short.SIZE / 8;
// mc++   private static final int COORDS_PER_VERTEX = 3; // x, z, alpha
// mc++ 
// mc++   private static final int VERTS_PER_BOUNDARY_VERT = 2;
// mc++   private static final int INDICES_PER_BOUNDARY_VERT = 3;
// mc++   private static final int INITIAL_BUFFER_BOUNDARY_VERTS = 64;
// mc++ 
// mc++   private static final int INITIAL_VERTEX_BUFFER_SIZE_BYTES =
// mc++       BYTES_PER_FLOAT * COORDS_PER_VERTEX * VERTS_PER_BOUNDARY_VERT * INITIAL_BUFFER_BOUNDARY_VERTS;
// mc++ 
// mc++   private static final int INITIAL_INDEX_BUFFER_SIZE_BYTES =
// mc++       BYTES_PER_SHORT
// mc++           * INDICES_PER_BOUNDARY_VERT
// mc++           * INDICES_PER_BOUNDARY_VERT
// mc++           * INITIAL_BUFFER_BOUNDARY_VERTS;
// mc++ 
// mc++   private static final float FADE_RADIUS_M = 0.25f;
// mc++   private static final float DOTS_PER_METER = 10.0f;
// mc++   private static final float EQUILATERAL_TRIANGLE_SCALE = (float) (1 / Math.sqrt(3));
// mc++ 
// mc++   // Using the "signed distance field" approach to render sharp lines and circles.
// mc++   // {dotThreshold, lineThreshold, lineFadeSpeed, occlusionScale}
// mc++   // dotThreshold/lineThreshold: red/green intensity above which dots/lines are present
// mc++   // lineFadeShrink:  lines will fade in between alpha = 1-(1/lineFadeShrink) and 1.0
// mc++   // occlusionShrink: occluded planes will fade out between alpha = 0 and 1/occlusionShrink
// mc++   private static final float[] GRID_CONTROL = {0.2f, 0.4f, 2.0f, 1.5f};
// mc++ 
// mc++   private int planeProgram;
// mc++   private final int[] textures = new int[1];
// mc++ 
// mc++   private int planeXZPositionAlphaAttribute;
// mc++ 
// mc++   private int planeModelUniform;
// mc++   private int planeNormalUniform;
// mc++   private int planeModelViewProjectionUniform;
// mc++   private int textureUniform;
// mc++   private int lineColorUniform;
// mc++   private int dotColorUniform;
// mc++   private int gridControlUniform;
// mc++   private int planeUvMatrixUniform;
// mc++ 
// mc++   private FloatBuffer vertexBuffer =
// mc++       ByteBuffer.allocateDirect(INITIAL_VERTEX_BUFFER_SIZE_BYTES)
// mc++           .order(ByteOrder.nativeOrder())
// mc++           .asFloatBuffer();
// mc++   private ShortBuffer indexBuffer =
// mc++       ByteBuffer.allocateDirect(INITIAL_INDEX_BUFFER_SIZE_BYTES)
// mc++           .order(ByteOrder.nativeOrder())
// mc++           .asShortBuffer();
// mc++ 
// mc++   // Temporary lists/matrices allocated here to reduce number of allocations for each frame.
// mc++   private final float[] modelMatrix = new float[16];
// mc++   private final float[] modelViewMatrix = new float[16];
// mc++   private final float[] modelViewProjectionMatrix = new float[16];
// mc++   private final float[] planeColor = new float[4];
// mc++   private final float[] planeAngleUvMatrix =
// mc++       new float[4]; // 2x2 rotation matrix applied to uv coords.
// mc++ 
// mc++   private final Map<Plane, Integer> planeIndexMap = new HashMap<>();
// mc++ 
// mc++   public PlaneRenderer() {}
// mc++ 
// mc++   /**
// mc++    * Allocates and initializes OpenGL resources needed by the plane renderer. Must be called on the
// mc++    * OpenGL thread, typically in {@link GLSurfaceView.Renderer#onSurfaceCreated(GL10, EGLConfig)}.
// mc++    *
// mc++    * @param context Needed to access shader source and texture PNG.
// mc++    * @param gridDistanceTextureName Name of the PNG file containing the grid texture.
// mc++    */
// mc++   public void createOnGlThread(Context context, String gridDistanceTextureName) throws IOException {
// mc++     int vertexShader =
// mc++         ShaderUtil.loadGLShader(TAG, context, GLES20.GL_VERTEX_SHADER, VERTEX_SHADER_NAME);
// mc++     int passthroughShader =
// mc++         ShaderUtil.loadGLShader(TAG, context, GLES20.GL_FRAGMENT_SHADER, FRAGMENT_SHADER_NAME);
// mc++ 
// mc++     planeProgram = GLES20.glCreateProgram();
// mc++     GLES20.glAttachShader(planeProgram, vertexShader);
// mc++     GLES20.glAttachShader(planeProgram, passthroughShader);
// mc++     GLES20.glLinkProgram(planeProgram);
// mc++     GLES20.glUseProgram(planeProgram);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "Program creation");
// mc++ 
// mc++     // Read the texture.
// mc++     Bitmap textureBitmap =
// mc++         BitmapFactory.decodeStream(context.getAssets().open(gridDistanceTextureName));
// mc++ 
// mc++     GLES20.glActiveTexture(GLES20.GL_TEXTURE0);
// mc++     GLES20.glGenTextures(textures.length, textures, 0);
// mc++     GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, textures[0]);
// mc++ 
// mc++     GLES20.glTexParameteri(
// mc++         GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_MIN_FILTER, GLES20.GL_LINEAR_MIPMAP_LINEAR);
// mc++     GLES20.glTexParameteri(GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_MAG_FILTER, GLES20.GL_LINEAR);
// mc++     GLUtils.texImage2D(GLES20.GL_TEXTURE_2D, 0, textureBitmap, 0);
// mc++     GLES20.glGenerateMipmap(GLES20.GL_TEXTURE_2D);
// mc++     GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, 0);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "Texture loading");
// mc++ 
// mc++     planeXZPositionAlphaAttribute = GLES20.glGetAttribLocation(planeProgram, "a_XZPositionAlpha");
// mc++ 
// mc++     planeModelUniform = GLES20.glGetUniformLocation(planeProgram, "u_Model");
// mc++     planeNormalUniform = GLES20.glGetUniformLocation(planeProgram, "u_Normal");
// mc++     planeModelViewProjectionUniform =
// mc++         GLES20.glGetUniformLocation(planeProgram, "u_ModelViewProjection");
// mc++     textureUniform = GLES20.glGetUniformLocation(planeProgram, "u_Texture");
// mc++     lineColorUniform = GLES20.glGetUniformLocation(planeProgram, "u_lineColor");
// mc++     dotColorUniform = GLES20.glGetUniformLocation(planeProgram, "u_dotColor");
// mc++     gridControlUniform = GLES20.glGetUniformLocation(planeProgram, "u_gridControl");
// mc++     planeUvMatrixUniform = GLES20.glGetUniformLocation(planeProgram, "u_PlaneUvMatrix");
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "Program parameters");
// mc++   }
// mc++ 
// mc++   /** Updates the plane model transform matrix and extents. */
// mc++   private void updatePlaneParameters(
// mc++       float[] planeMatrix, float extentX, float extentZ, FloatBuffer boundary) {
// mc++     System.arraycopy(planeMatrix, 0, modelMatrix, 0, 16);
// mc++     if (boundary == null) {
// mc++       vertexBuffer.limit(0);
// mc++       indexBuffer.limit(0);
// mc++       return;
// mc++     }
// mc++ 
// mc++     // Generate a new set of vertices and a corresponding triangle strip index set so that
// mc++     // the plane boundary polygon has a fading edge. This is done by making a copy of the
// mc++     // boundary polygon vertices and scaling it down around center to push it inwards. Then
// mc++     // the index buffer is setup accordingly.
// mc++     boundary.rewind();
// mc++     int boundaryVertices = boundary.limit() / 2;
// mc++     int numVertices;
// mc++     int numIndices;
// mc++ 
// mc++     numVertices = boundaryVertices * VERTS_PER_BOUNDARY_VERT;
// mc++     // drawn as GL_TRIANGLE_STRIP with 3n-2 triangles (n-2 for fill, 2n for perimeter).
// mc++     numIndices = boundaryVertices * INDICES_PER_BOUNDARY_VERT;
// mc++ 
// mc++     if (vertexBuffer.capacity() < numVertices * COORDS_PER_VERTEX) {
// mc++       int size = vertexBuffer.capacity();
// mc++       while (size < numVertices * COORDS_PER_VERTEX) {
// mc++         size *= 2;
// mc++       }
// mc++       vertexBuffer =
// mc++           ByteBuffer.allocateDirect(BYTES_PER_FLOAT * size)
// mc++               .order(ByteOrder.nativeOrder())
// mc++               .asFloatBuffer();
// mc++     }
// mc++     vertexBuffer.rewind();
// mc++     vertexBuffer.limit(numVertices * COORDS_PER_VERTEX);
// mc++ 
// mc++     if (indexBuffer.capacity() < numIndices) {
// mc++       int size = indexBuffer.capacity();
// mc++       while (size < numIndices) {
// mc++         size *= 2;
// mc++       }
// mc++       indexBuffer =
// mc++           ByteBuffer.allocateDirect(BYTES_PER_SHORT * size)
// mc++               .order(ByteOrder.nativeOrder())
// mc++               .asShortBuffer();
// mc++     }
// mc++     indexBuffer.rewind();
// mc++     indexBuffer.limit(numIndices);
// mc++ 
// mc++     // Note: when either dimension of the bounding box is smaller than 2*FADE_RADIUS_M we
// mc++     // generate a bunch of 0-area triangles.  These don't get rendered though so it works
// mc++     // out ok.
// mc++     float xScale = Math.max((extentX - 2 * FADE_RADIUS_M) / extentX, 0.0f);
// mc++     float zScale = Math.max((extentZ - 2 * FADE_RADIUS_M) / extentZ, 0.0f);
// mc++ 
// mc++     while (boundary.hasRemaining()) {
// mc++       float x = boundary.get();
// mc++       float z = boundary.get();
// mc++       vertexBuffer.put(x);
// mc++       vertexBuffer.put(z);
// mc++       vertexBuffer.put(0.0f);
// mc++       vertexBuffer.put(x * xScale);
// mc++       vertexBuffer.put(z * zScale);
// mc++       vertexBuffer.put(1.0f);
// mc++     }
// mc++ 
// mc++     // step 1, perimeter
// mc++     indexBuffer.put((short) ((boundaryVertices - 1) * 2));
// mc++     for (int i = 0; i < boundaryVertices; ++i) {
// mc++       indexBuffer.put((short) (i * 2));
// mc++       indexBuffer.put((short) (i * 2 + 1));
// mc++     }
// mc++     indexBuffer.put((short) 1);
// mc++     // This leaves us on the interior edge of the perimeter between the inset vertices
// mc++     // for boundary verts n-1 and 0.
// mc++ 
// mc++     // step 2, interior:
// mc++     for (int i = 1; i < boundaryVertices / 2; ++i) {
// mc++       indexBuffer.put((short) ((boundaryVertices - 1 - i) * 2 + 1));
// mc++       indexBuffer.put((short) (i * 2 + 1));
// mc++     }
// mc++     if (boundaryVertices % 2 != 0) {
// mc++       indexBuffer.put((short) ((boundaryVertices / 2) * 2 + 1));
// mc++     }
// mc++   }
// mc++ 
// mc++   private void draw(float[] cameraView, float[] cameraPerspective, float[] planeNormal) {
// mc++     // Build the ModelView and ModelViewProjection matrices
// mc++     // for calculating cube position and light.
// mc++     Matrix.multiplyMM(modelViewMatrix, 0, cameraView, 0, modelMatrix, 0);
// mc++     Matrix.multiplyMM(modelViewProjectionMatrix, 0, cameraPerspective, 0, modelViewMatrix, 0);
// mc++ 
// mc++     // Set the position of the plane
// mc++     vertexBuffer.rewind();
// mc++     GLES20.glVertexAttribPointer(
// mc++         planeXZPositionAlphaAttribute,
// mc++         COORDS_PER_VERTEX,
// mc++         GLES20.GL_FLOAT,
// mc++         false,
// mc++         BYTES_PER_FLOAT * COORDS_PER_VERTEX,
// mc++         vertexBuffer);
// mc++ 
// mc++     // Set the Model and ModelViewProjection matrices in the shader.
// mc++     GLES20.glUniformMatrix4fv(planeModelUniform, 1, false, modelMatrix, 0);
// mc++     GLES20.glUniform3f(planeNormalUniform, planeNormal[0], planeNormal[1], planeNormal[2]);
// mc++     GLES20.glUniformMatrix4fv(
// mc++         planeModelViewProjectionUniform, 1, false, modelViewProjectionMatrix, 0);
// mc++ 
// mc++     indexBuffer.rewind();
// mc++     GLES20.glDrawElements(
// mc++         GLES20.GL_TRIANGLE_STRIP, indexBuffer.limit(), GLES20.GL_UNSIGNED_SHORT, indexBuffer);
// mc++     ShaderUtil.checkGLError(TAG, "Drawing plane");
// mc++   }
// mc++ 
// mc++   static class SortablePlane {
// mc++     final float distance;
// mc++     final Plane plane;
// mc++ 
// mc++     SortablePlane(float distance, Plane plane) {
// mc++       this.distance = distance;
// mc++       this.plane = plane;
// mc++     }
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Draws the collection of tracked planes, with closer planes hiding more distant ones.
// mc++    *
// mc++    * @param allPlanes The collection of planes to draw.
// mc++    * @param cameraPose The pose of the camera, as returned by {@link Camera#getPose()}
// mc++    * @param cameraPerspective The projection matrix, as returned by {@link
// mc++    *     Camera#getProjectionMatrix(float[], int, float, float)}
// mc++    */
// mc++   public void drawPlanes(Collection<Plane> allPlanes, Pose cameraPose, float[] cameraPerspective) {
// mc++     // Planes must be sorted by distance from camera so that we draw closer planes first, and
// mc++     // they occlude the farther planes.
// mc++     List<SortablePlane> sortedPlanes = new ArrayList<>();
// mc++ 
// mc++     for (Plane plane : allPlanes) {
// mc++       if (plane.getTrackingState() != TrackingState.TRACKING || plane.getSubsumedBy() != null) {
// mc++         continue;
// mc++       }
// mc++ 
// mc++       float distance = calculateDistanceToPlane(plane.getCenterPose(), cameraPose);
// mc++       if (distance < 0) { // Plane is back-facing.
// mc++         continue;
// mc++       }
// mc++       sortedPlanes.add(new SortablePlane(distance, plane));
// mc++     }
// mc++     Collections.sort(
// mc++         sortedPlanes,
// mc++         new Comparator<SortablePlane>() {
// mc++           @Override
// mc++           public int compare(SortablePlane a, SortablePlane b) {
// mc++             return Float.compare(a.distance, b.distance);
// mc++           }
// mc++         });
// mc++ 
// mc++     float[] cameraView = new float[16];
// mc++     cameraPose.inverse().toMatrix(cameraView, 0);
// mc++ 
// mc++     // Planes are drawn with additive blending, masked by the alpha channel for occlusion.
// mc++ 
// mc++     // Start by clearing the alpha channel of the color buffer to 1.0.
// mc++     GLES20.glClearColor(1, 1, 1, 1);
// mc++     GLES20.glColorMask(false, false, false, true);
// mc++     GLES20.glClear(GLES20.GL_COLOR_BUFFER_BIT);
// mc++     GLES20.glColorMask(true, true, true, true);
// mc++ 
// mc++     // Disable depth write.
// mc++     GLES20.glDepthMask(false);
// mc++ 
// mc++     // Additive blending, masked by alpha channel, clearing alpha channel.
// mc++     GLES20.glEnable(GLES20.GL_BLEND);
// mc++     GLES20.glBlendFuncSeparate(
// mc++         GLES20.GL_DST_ALPHA, GLES20.GL_ONE, // RGB (src, dest)
// mc++         GLES20.GL_ZERO, GLES20.GL_ONE_MINUS_SRC_ALPHA); // ALPHA (src, dest)
// mc++ 
// mc++     // Set up the shader.
// mc++     GLES20.glUseProgram(planeProgram);
// mc++ 
// mc++     // Attach the texture.
// mc++     GLES20.glActiveTexture(GLES20.GL_TEXTURE0);
// mc++     GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, textures[0]);
// mc++     GLES20.glUniform1i(textureUniform, 0);
// mc++ 
// mc++     // Shared fragment uniforms.
// mc++     GLES20.glUniform4fv(gridControlUniform, 1, GRID_CONTROL, 0);
// mc++ 
// mc++     // Enable vertex arrays
// mc++     GLES20.glEnableVertexAttribArray(planeXZPositionAlphaAttribute);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "Setting up to draw planes");
// mc++ 
// mc++     for (SortablePlane sortedPlane : sortedPlanes) {
// mc++       Plane plane = sortedPlane.plane;
// mc++       float[] planeMatrix = new float[16];
// mc++       plane.getCenterPose().toMatrix(planeMatrix, 0);
// mc++ 
// mc++       float[] normal = new float[3];
// mc++       // Get transformed Y axis of plane's coordinate system.
// mc++       plane.getCenterPose().getTransformedAxis(1, 1.0f, normal, 0);
// mc++ 
// mc++       updatePlaneParameters(
// mc++           planeMatrix, plane.getExtentX(), plane.getExtentZ(), plane.getPolygon());
// mc++ 
// mc++       // Get plane index. Keep a map to assign same indices to same planes.
// mc++       Integer planeIndex = planeIndexMap.get(plane);
// mc++       if (planeIndex == null) {
// mc++         planeIndex = planeIndexMap.size();
// mc++         planeIndexMap.put(plane, planeIndex);
// mc++       }
// mc++ 
// mc++       // Set plane color. Computed deterministically from the Plane index.
// mc++       int colorIndex = planeIndex % PLANE_COLORS_RGBA.length;
// mc++       colorRgbaToFloat(planeColor, PLANE_COLORS_RGBA[colorIndex]);
// mc++       GLES20.glUniform4fv(lineColorUniform, 1, planeColor, 0);
// mc++       GLES20.glUniform4fv(dotColorUniform, 1, planeColor, 0);
// mc++ 
// mc++       // Each plane will have its own angle offset from others, to make them easier to
// mc++       // distinguish. Compute a 2x2 rotation matrix from the angle.
// mc++       float angleRadians = planeIndex * 0.144f;
// mc++       float uScale = DOTS_PER_METER;
// mc++       float vScale = DOTS_PER_METER * EQUILATERAL_TRIANGLE_SCALE;
// mc++       planeAngleUvMatrix[0] = +(float) Math.cos(angleRadians) * uScale;
// mc++       planeAngleUvMatrix[1] = -(float) Math.sin(angleRadians) * vScale;
// mc++       planeAngleUvMatrix[2] = +(float) Math.sin(angleRadians) * uScale;
// mc++       planeAngleUvMatrix[3] = +(float) Math.cos(angleRadians) * vScale;
// mc++       GLES20.glUniformMatrix2fv(planeUvMatrixUniform, 1, false, planeAngleUvMatrix, 0);
// mc++ 
// mc++       draw(cameraView, cameraPerspective, normal);
// mc++     }
// mc++ 
// mc++     // Clean up the state we set
// mc++     GLES20.glDisableVertexAttribArray(planeXZPositionAlphaAttribute);
// mc++     GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, 0);
// mc++     GLES20.glDisable(GLES20.GL_BLEND);
// mc++     GLES20.glDepthMask(true);
// mc++     GLES20.glClearColor(0.1f, 0.1f, 0.1f, 1.0f);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "Cleaning up after drawing planes");
// mc++   }
// mc++ 
// mc++   // Calculate the normal distance to plane from cameraPose, the given planePose should have y axis
// mc++   // parallel to plane's normal, for example plane's center pose or hit test pose.
// mc++   public static float calculateDistanceToPlane(Pose planePose, Pose cameraPose) {
// mc++     float[] normal = new float[3];
// mc++     float cameraX = cameraPose.tx();
// mc++     float cameraY = cameraPose.ty();
// mc++     float cameraZ = cameraPose.tz();
// mc++     // Get transformed Y axis of plane's coordinate system.
// mc++     planePose.getTransformedAxis(1, 1.0f, normal, 0);
// mc++     // Compute dot product of plane's normal with vector from camera to plane center.
// mc++     return (cameraX - planePose.tx()) * normal[0]
// mc++         + (cameraY - planePose.ty()) * normal[1]
// mc++         + (cameraZ - planePose.tz()) * normal[2];
// mc++   }
// mc++ 
// mc++   private static void colorRgbaToFloat(float[] planeColor, int colorRgba) {
// mc++     planeColor[0] = ((float) ((colorRgba >> 24) & 0xff)) / 255.0f;
// mc++     planeColor[1] = ((float) ((colorRgba >> 16) & 0xff)) / 255.0f;
// mc++     planeColor[2] = ((float) ((colorRgba >> 8) & 0xff)) / 255.0f;
// mc++     planeColor[3] = ((float) ((colorRgba >> 0) & 0xff)) / 255.0f;
// mc++   }
// mc++ 
// mc++   private static final int[] PLANE_COLORS_RGBA = {
// mc++     0xFFFFFFFF,
// mc++     0xF44336FF,
// mc++     0xE91E63FF,
// mc++     0x9C27B0FF,
// mc++     0x673AB7FF,
// mc++     0x3F51B5FF,
// mc++     0x2196F3FF,
// mc++     0x03A9F4FF,
// mc++     0x00BCD4FF,
// mc++     0x009688FF,
// mc++     0x4CAF50FF,
// mc++     0x8BC34AFF,
// mc++     0xCDDC39FF,
// mc++     0xFFEB3BFF,
// mc++     0xFFC107FF,
// mc++     0xFF9800FF,
// mc++   };
// mc++ }
