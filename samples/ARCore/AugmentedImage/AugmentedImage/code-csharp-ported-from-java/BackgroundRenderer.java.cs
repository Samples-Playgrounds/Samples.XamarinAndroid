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
// mc++ import android.opengl.GLES11Ext;
// mc++ import android.opengl.GLES20;
// mc++ import android.opengl.GLSurfaceView;
// mc++ import android.support.annotation.NonNull;
// mc++ import com.google.ar.core.Coordinates2d;
// mc++ import com.google.ar.core.Frame;
// mc++ import java.io.IOException;
// mc++ import java.nio.ByteBuffer;
// mc++ import java.nio.ByteOrder;
// mc++ import java.nio.FloatBuffer;
// mc++ 
// mc++ /**
// mc++  * This class renders the AR background from camera feed. It creates and hosts the texture given to
// mc++  * ARCore to be filled with the camera image.
// mc++  */
// mc++ public class BackgroundRenderer {
// mc++   private static final String TAG = BackgroundRenderer.class.getSimpleName();
// mc++ 
// mc++   // Shader names.
// mc++   private static final String VERTEX_SHADER_NAME = "shaders/screenquad.vert";
// mc++   private static final String FRAGMENT_SHADER_NAME = "shaders/screenquad.frag";
// mc++ 
// mc++   private static final int COORDS_PER_VERTEX = 2;
// mc++   private static final int TEXCOORDS_PER_VERTEX = 2;
// mc++   private static final int FLOAT_SIZE = 4;
// mc++ 
// mc++   private FloatBuffer quadCoords;
// mc++   private FloatBuffer quadTexCoords;
// mc++ 
// mc++   private int quadProgram;
// mc++ 
// mc++   private int quadPositionParam;
// mc++   private int quadTexCoordParam;
// mc++   private int textureId = -1;
// mc++ 
// mc++   public int getTextureId() {
// mc++     return textureId;
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Allocates and initializes OpenGL resources needed by the background renderer. Must be called on
// mc++    * the OpenGL thread, typically in {@link GLSurfaceView.Renderer#onSurfaceCreated(GL10,
// mc++    * EGLConfig)}.
// mc++    *
// mc++    * @param context Needed to access shader source.
// mc++    */
// mc++   public void createOnGlThread(Context context) throws IOException {
// mc++     // Generate the background texture.
// mc++     int[] textures = new int[1];
// mc++     GLES20.glGenTextures(1, textures, 0);
// mc++     textureId = textures[0];
// mc++     int textureTarget = GLES11Ext.GL_TEXTURE_EXTERNAL_OES;
// mc++     GLES20.glBindTexture(textureTarget, textureId);
// mc++     GLES20.glTexParameteri(textureTarget, GLES20.GL_TEXTURE_WRAP_S, GLES20.GL_CLAMP_TO_EDGE);
// mc++     GLES20.glTexParameteri(textureTarget, GLES20.GL_TEXTURE_WRAP_T, GLES20.GL_CLAMP_TO_EDGE);
// mc++     GLES20.glTexParameteri(textureTarget, GLES20.GL_TEXTURE_MIN_FILTER, GLES20.GL_LINEAR);
// mc++     GLES20.glTexParameteri(textureTarget, GLES20.GL_TEXTURE_MAG_FILTER, GLES20.GL_LINEAR);
// mc++ 
// mc++     int numVertices = 4;
// mc++     if (numVertices != QUAD_COORDS.length / COORDS_PER_VERTEX) {
// mc++       throw new RuntimeException("Unexpected number of vertices in BackgroundRenderer.");
// mc++     }
// mc++ 
// mc++     ByteBuffer bbCoords = ByteBuffer.allocateDirect(QUAD_COORDS.length * FLOAT_SIZE);
// mc++     bbCoords.order(ByteOrder.nativeOrder());
// mc++     quadCoords = bbCoords.asFloatBuffer();
// mc++     quadCoords.put(QUAD_COORDS);
// mc++     quadCoords.position(0);
// mc++ 
// mc++     ByteBuffer bbTexCoordsTransformed =
// mc++         ByteBuffer.allocateDirect(numVertices * TEXCOORDS_PER_VERTEX * FLOAT_SIZE);
// mc++     bbTexCoordsTransformed.order(ByteOrder.nativeOrder());
// mc++     quadTexCoords = bbTexCoordsTransformed.asFloatBuffer();
// mc++ 
// mc++     int vertexShader =
// mc++         ShaderUtil.loadGLShader(TAG, context, GLES20.GL_VERTEX_SHADER, VERTEX_SHADER_NAME);
// mc++     int fragmentShader =
// mc++         ShaderUtil.loadGLShader(TAG, context, GLES20.GL_FRAGMENT_SHADER, FRAGMENT_SHADER_NAME);
// mc++ 
// mc++     quadProgram = GLES20.glCreateProgram();
// mc++     GLES20.glAttachShader(quadProgram, vertexShader);
// mc++     GLES20.glAttachShader(quadProgram, fragmentShader);
// mc++     GLES20.glLinkProgram(quadProgram);
// mc++     GLES20.glUseProgram(quadProgram);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "Program creation");
// mc++ 
// mc++     quadPositionParam = GLES20.glGetAttribLocation(quadProgram, "a_Position");
// mc++     quadTexCoordParam = GLES20.glGetAttribLocation(quadProgram, "a_TexCoord");
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "Program parameters");
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Draws the AR background image. The image will be drawn such that virtual content rendered with
// mc++    * the matrices provided by {@link com.google.ar.core.Camera#getViewMatrix(float[], int)} and
// mc++    * {@link com.google.ar.core.Camera#getProjectionMatrix(float[], int, float, float)} will
// mc++    * accurately follow static physical objects. This must be called <b>before</b> drawing virtual
// mc++    * content.
// mc++    *
// mc++    * @param frame The current {@code Frame} as returned by {@link Session#update()}.
// mc++    */
// mc++   public void draw(@NonNull Frame frame) {
// mc++     // If display rotation changed (also includes view size change), we need to re-query the uv
// mc++     // coordinates for the screen rect, as they may have changed as well.
// mc++     if (frame.hasDisplayGeometryChanged()) {
// mc++       frame.transformCoordinates2d(
// mc++           Coordinates2d.OPENGL_NORMALIZED_DEVICE_COORDINATES,
// mc++           quadCoords,
// mc++           Coordinates2d.TEXTURE_NORMALIZED,
// mc++           quadTexCoords);
// mc++     }
// mc++ 
// mc++     if (frame.getTimestamp() == 0) {
// mc++       // Suppress rendering if the camera did not produce the first frame yet. This is to avoid
// mc++       // drawing possible leftover data from previous sessions if the texture is reused.
// mc++       return;
// mc++     }
// mc++ 
// mc++     draw();
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Draws the camera image using the currently configured {@link BackgroundRenderer#quadTexCoords}
// mc++    * image texture coordinates.
// mc++    *
// mc++    * <p>The image will be center cropped if the camera sensor aspect ratio does not match the screen
// mc++    * aspect ratio, which matches the cropping behavior of {@link
// mc++    * Frame#transformCoordinates2d(Coordinates2d, float[], Coordinates2d, float[])}.
// mc++    */
// mc++   public void draw(
// mc++       int imageWidth, int imageHeight, float screenAspectRatio, int cameraToDisplayRotation) {
// mc++     // Crop the camera image to fit the screen aspect ratio.
// mc++     float imageAspectRatio = (float) imageWidth / imageHeight;
// mc++     float croppedWidth;
// mc++     float croppedHeight;
// mc++     if (screenAspectRatio < imageAspectRatio) {
// mc++       croppedWidth = imageHeight * screenAspectRatio;
// mc++       croppedHeight = imageHeight;
// mc++     } else {
// mc++       croppedWidth = imageWidth;
// mc++       croppedHeight = imageWidth / screenAspectRatio;
// mc++     }
// mc++ 
// mc++     float u = (imageWidth - croppedWidth) / imageWidth * 0.5f;
// mc++     float v = (imageHeight - croppedHeight) / imageHeight * 0.5f;
// mc++ 
// mc++     float[] texCoordTransformed;
// mc++     switch (cameraToDisplayRotation) {
// mc++       case 90:
// mc++         texCoordTransformed = new float[] {1 - u, 1 - v, u, 1 - v, 1 - u, v, u, v};
// mc++         break;
// mc++       case 180:
// mc++         texCoordTransformed = new float[] {1 - u, v, 1 - u, 1 - v, u, v, u, 1 - v};
// mc++         break;
// mc++       case 270:
// mc++         texCoordTransformed = new float[] {u, v, 1 - u, v, u, 1 - v, 1 - u, 1 - v};
// mc++         break;
// mc++       case 0:
// mc++         texCoordTransformed = new float[] {u, 1 - v, u, v, 1 - u, 1 - v, 1 - u, v};
// mc++         break;
// mc++       default:
// mc++         throw new IllegalArgumentException("Unhandled rotation: " + cameraToDisplayRotation);
// mc++     }
// mc++ 
// mc++     // Write image texture coordinates.
// mc++     quadTexCoords.position(0);
// mc++     quadTexCoords.put(texCoordTransformed);
// mc++ 
// mc++     draw();
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Draws the camera background image using the currently configured {@link
// mc++    * BackgroundRenderer#quadTexCoords} image texture coordinates.
// mc++    */
// mc++   private void draw() {
// mc++     // Ensure position is rewound before use.
// mc++     quadTexCoords.position(0);
// mc++ 
// mc++     // No need to test or write depth, the screen quad has arbitrary depth, and is expected
// mc++     // to be drawn first.
// mc++     GLES20.glDisable(GLES20.GL_DEPTH_TEST);
// mc++     GLES20.glDepthMask(false);
// mc++ 
// mc++     GLES20.glBindTexture(GLES11Ext.GL_TEXTURE_EXTERNAL_OES, textureId);
// mc++ 
// mc++     GLES20.glUseProgram(quadProgram);
// mc++ 
// mc++     // Set the vertex positions.
// mc++     GLES20.glVertexAttribPointer(
// mc++         quadPositionParam, COORDS_PER_VERTEX, GLES20.GL_FLOAT, false, 0, quadCoords);
// mc++ 
// mc++     // Set the texture coordinates.
// mc++     GLES20.glVertexAttribPointer(
// mc++         quadTexCoordParam, TEXCOORDS_PER_VERTEX, GLES20.GL_FLOAT, false, 0, quadTexCoords);
// mc++ 
// mc++     // Enable vertex arrays
// mc++     GLES20.glEnableVertexAttribArray(quadPositionParam);
// mc++     GLES20.glEnableVertexAttribArray(quadTexCoordParam);
// mc++ 
// mc++     GLES20.glDrawArrays(GLES20.GL_TRIANGLE_STRIP, 0, 4);
// mc++ 
// mc++     // Disable vertex arrays
// mc++     GLES20.glDisableVertexAttribArray(quadPositionParam);
// mc++     GLES20.glDisableVertexAttribArray(quadTexCoordParam);
// mc++ 
// mc++     // Restore the depth state for further drawing.
// mc++     GLES20.glDepthMask(true);
// mc++     GLES20.glEnable(GLES20.GL_DEPTH_TEST);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "BackgroundRendererDraw");
// mc++   }
// mc++ 
// mc++   private static final float[] QUAD_COORDS =
// mc++       new float[] {
// mc++         -1.0f, -1.0f, -1.0f, +1.0f, +1.0f, -1.0f, +1.0f, +1.0f,
// mc++       };
// mc++ }
