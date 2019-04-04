// mc++ /*
// mc++  * Copyright 2018 Google Inc. All Rights Reserved.
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
// mc++ package com.google.ar.core.examples.java.computervision;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.opengl.GLES11Ext;
// mc++ import android.opengl.GLES20;
// mc++ import android.opengl.GLSurfaceView;
// mc++ import com.google.ar.core.Coordinates2d;
// mc++ import com.google.ar.core.Frame;
// mc++ import com.google.ar.core.Session;
// mc++ import com.google.ar.core.examples.java.common.rendering.ShaderUtil;
// mc++ import java.io.IOException;
// mc++ import java.nio.ByteBuffer;
// mc++ import java.nio.ByteOrder;
// mc++ import java.nio.FloatBuffer;
// mc++ import javax.microedition.khronos.egl.EGLConfig;
// mc++ import javax.microedition.khronos.opengles.GL10;
// mc++ 
// mc++ /**
// mc++  * This class renders the screen with images from both GPU and CPU. The top half of the screen shows
// mc++  * the GPU image, while the bottom half of the screen shows the CPU image.
// mc++  */
// mc++ public class CpuImageRenderer {
// mc++   private static final String TAG = CpuImageRenderer.class.getSimpleName();
// mc++ 
// mc++   private static final int COORDS_PER_VERTEX = 2;
// mc++   private static final int TEXCOORDS_PER_VERTEX = 2;
// mc++   private static final int FLOAT_SIZE = 4;
// mc++ 
// mc++   private FloatBuffer quadCoords;
// mc++   private FloatBuffer quadTexCoords;
// mc++   private FloatBuffer quadImgCoords;
// mc++ 
// mc++   private int quadProgram;
// mc++ 
// mc++   private int quadPositionAttrib;
// mc++   private int quadTexCoordAttrib;
// mc++   private int quadImgCoordAttrib;
// mc++   private int quadSplitterUniform;
// mc++   private int backgroundTextureId = -1;
// mc++   private int overlayTextureId = -1;
// mc++   private float splitterPosition = 0.0f;
// mc++ 
// mc++   public int getTextureId() {
// mc++     return backgroundTextureId;
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
// mc++     int[] textures = new int[2];
// mc++     GLES20.glGenTextures(2, textures, 0);
// mc++ 
// mc++     // Generate the background texture.
// mc++     backgroundTextureId = textures[0];
// mc++     GLES20.glBindTexture(GLES11Ext.GL_TEXTURE_EXTERNAL_OES, backgroundTextureId);
// mc++     GLES20.glTexParameteri(
// mc++         GLES11Ext.GL_TEXTURE_EXTERNAL_OES, GLES20.GL_TEXTURE_WRAP_S, GLES20.GL_CLAMP_TO_EDGE);
// mc++     GLES20.glTexParameteri(
// mc++         GLES11Ext.GL_TEXTURE_EXTERNAL_OES, GLES20.GL_TEXTURE_WRAP_T, GLES20.GL_CLAMP_TO_EDGE);
// mc++     GLES20.glTexParameteri(
// mc++         GLES11Ext.GL_TEXTURE_EXTERNAL_OES, GLES20.GL_TEXTURE_MIN_FILTER, GLES20.GL_NEAREST);
// mc++     GLES20.glTexParameteri(
// mc++         GLES11Ext.GL_TEXTURE_EXTERNAL_OES, GLES20.GL_TEXTURE_MAG_FILTER, GLES20.GL_NEAREST);
// mc++ 
// mc++     // Generate the CPU Image overlay texture.
// mc++     overlayTextureId = textures[1];
// mc++     GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, overlayTextureId);
// mc++     GLES20.glTexParameteri(GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_WRAP_S, GLES20.GL_CLAMP_TO_EDGE);
// mc++     GLES20.glTexParameteri(GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_WRAP_T, GLES20.GL_CLAMP_TO_EDGE);
// mc++     GLES20.glTexParameteri(GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_MIN_FILTER, GLES20.GL_NEAREST);
// mc++ 
// mc++     int numVertices = QUAD_COORDS.length / COORDS_PER_VERTEX;
// mc++     ByteBuffer bbCoords = ByteBuffer.allocateDirect(QUAD_COORDS.length * FLOAT_SIZE);
// mc++     bbCoords.order(ByteOrder.nativeOrder());
// mc++     quadCoords = bbCoords.asFloatBuffer();
// mc++     quadCoords.put(QUAD_COORDS);
// mc++     quadCoords.position(0);
// mc++ 
// mc++     ByteBuffer bbTexCoords =
// mc++         ByteBuffer.allocateDirect(numVertices * TEXCOORDS_PER_VERTEX * FLOAT_SIZE);
// mc++     bbTexCoords.order(ByteOrder.nativeOrder());
// mc++     quadTexCoords = bbTexCoords.asFloatBuffer();
// mc++ 
// mc++     ByteBuffer bbImgCoords =
// mc++         ByteBuffer.allocateDirect(numVertices * TEXCOORDS_PER_VERTEX * FLOAT_SIZE);
// mc++     bbImgCoords.order(ByteOrder.nativeOrder());
// mc++     quadImgCoords = bbImgCoords.asFloatBuffer();
// mc++ 
// mc++     int vertexShader =
// mc++         ShaderUtil.loadGLShader(
// mc++             TAG, context, GLES20.GL_VERTEX_SHADER, "shaders/cpu_screenquad.vert");
// mc++     int fragmentShader =
// mc++         ShaderUtil.loadGLShader(
// mc++             TAG, context, GLES20.GL_FRAGMENT_SHADER, "shaders/cpu_screenquad.frag");
// mc++ 
// mc++     quadProgram = GLES20.glCreateProgram();
// mc++     GLES20.glAttachShader(quadProgram, vertexShader);
// mc++     GLES20.glAttachShader(quadProgram, fragmentShader);
// mc++     GLES20.glLinkProgram(quadProgram);
// mc++     GLES20.glUseProgram(quadProgram);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "Program creation");
// mc++ 
// mc++     quadPositionAttrib = GLES20.glGetAttribLocation(quadProgram, "a_Position");
// mc++     quadTexCoordAttrib = GLES20.glGetAttribLocation(quadProgram, "a_TexCoord");
// mc++     quadImgCoordAttrib = GLES20.glGetAttribLocation(quadProgram, "a_ImgCoord");
// mc++     quadSplitterUniform = GLES20.glGetUniformLocation(quadProgram, "s_SplitterPosition");
// mc++ 
// mc++     int texLoc = GLES20.glGetUniformLocation(quadProgram, "TexVideo");
// mc++     GLES20.glUniform1i(texLoc, 0);
// mc++ 
// mc++     texLoc = GLES20.glGetUniformLocation(quadProgram, "TexCpuImageGrayscale");
// mc++     GLES20.glUniform1i(texLoc, 1);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "Program parameters");
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Gets the texture splitter position.
// mc++    *
// mc++    * @return the splitter position.
// mc++    */
// mc++   public float getSplitterPosition() {
// mc++     return splitterPosition;
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Sets the splitter position. This position defines the splitting position between the background
// mc++    * video and the image.
// mc++    *
// mc++    * @param position the new splitter position.
// mc++    */
// mc++   public void setSplitterPosition(float position) {
// mc++     splitterPosition = position;
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Draws the AR background image. The image will be drawn such that virtual content rendered with
// mc++    * the matrices provided by {@link Frame#getViewMatrix(float[], int)} and {@link
// mc++    * Session#getProjectionMatrix(float[], int, float, float)} will accurately follow static physical
// mc++    * objects. This must be called <b>before</b> drawing virtual content.
// mc++    *
// mc++    * @param frame The last {@code Frame} returned by {@link Session#update()}.
// mc++    * @param imageWidth The processed image width.
// mc++    * @param imageHeight The processed image height.
// mc++    * @param processedImageBytesGrayscale the processed bytes of the image, grayscale par only. Can
// mc++    *     be null.
// mc++    * @param screenAspectRatio The aspect ratio of the screen.
// mc++    * @param cameraToDisplayRotation The rotation of camera with respect to the display. The value is
// mc++    *     one of android.view.Surface.ROTATION_#(0, 90, 180, 270).
// mc++    */
// mc++   public void drawWithCpuImage(
// mc++       Frame frame,
// mc++       int imageWidth,
// mc++       int imageHeight,
// mc++       ByteBuffer processedImageBytesGrayscale,
// mc++       float screenAspectRatio,
// mc++       int cameraToDisplayRotation) {
// mc++ 
// mc++     // Apply overlay image buffer
// mc++     if (processedImageBytesGrayscale != null) {
// mc++       GLES20.glActiveTexture(GLES20.GL_TEXTURE1);
// mc++       GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, overlayTextureId);
// mc++       GLES20.glTexImage2D(
// mc++           GLES20.GL_TEXTURE_2D,
// mc++           0,
// mc++           GLES20.GL_LUMINANCE,
// mc++           imageWidth,
// mc++           imageHeight,
// mc++           0,
// mc++           GLES20.GL_LUMINANCE,
// mc++           GLES20.GL_UNSIGNED_BYTE,
// mc++           processedImageBytesGrayscale);
// mc++     }
// mc++ 
// mc++     updateTextureCoordinates(frame);
// mc++ 
// mc++     // Rest of the draw code is shared between the two functions.
// mc++     drawWithoutCpuImage();
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Same as above, but will not update the CPU image drawn. Should be used when a CPU image is
// mc++    * unavailable for any reason, and only background should be drawn.
// mc++    */
// mc++   public void drawWithoutCpuImage() {
// mc++     // No need to test or write depth, the screen quad has arbitrary depth, and is expected
// mc++     // to be drawn first.
// mc++     GLES20.glDisable(GLES20.GL_DEPTH_TEST);
// mc++     GLES20.glDepthMask(false);
// mc++ 
// mc++     GLES20.glActiveTexture(GLES20.GL_TEXTURE0);
// mc++     GLES20.glBindTexture(GLES11Ext.GL_TEXTURE_EXTERNAL_OES, backgroundTextureId);
// mc++ 
// mc++     GLES20.glUseProgram(quadProgram);
// mc++ 
// mc++     // Set the vertex positions.
// mc++     GLES20.glVertexAttribPointer(
// mc++         quadPositionAttrib, COORDS_PER_VERTEX, GLES20.GL_FLOAT, false, 0, quadCoords);
// mc++ 
// mc++     // Set splitter position.
// mc++     GLES20.glUniform1f(quadSplitterUniform, splitterPosition);
// mc++ 
// mc++     // Set the GPU image texture coordinates.
// mc++     GLES20.glVertexAttribPointer(
// mc++         quadTexCoordAttrib, TEXCOORDS_PER_VERTEX, GLES20.GL_FLOAT, false, 0, quadTexCoords);
// mc++ 
// mc++     // Set the CPU image texture coordinates.
// mc++     GLES20.glVertexAttribPointer(
// mc++         quadImgCoordAttrib, TEXCOORDS_PER_VERTEX, GLES20.GL_FLOAT, false, 0, quadImgCoords);
// mc++ 
// mc++     // Enable vertex arrays
// mc++     GLES20.glEnableVertexAttribArray(quadPositionAttrib);
// mc++     GLES20.glEnableVertexAttribArray(quadTexCoordAttrib);
// mc++     GLES20.glEnableVertexAttribArray(quadImgCoordAttrib);
// mc++ 
// mc++     GLES20.glDrawArrays(GLES20.GL_TRIANGLE_STRIP, 0, 4);
// mc++ 
// mc++     // Disable vertex arrays
// mc++     GLES20.glDisableVertexAttribArray(quadPositionAttrib);
// mc++     GLES20.glDisableVertexAttribArray(quadTexCoordAttrib);
// mc++     GLES20.glDisableVertexAttribArray(quadImgCoordAttrib);
// mc++ 
// mc++     // Restore the depth state for further drawing.
// mc++     GLES20.glDepthMask(true);
// mc++     GLES20.glEnable(GLES20.GL_DEPTH_TEST);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "Draw");
// mc++   }
// mc++ 
// mc++   private void updateTextureCoordinates(Frame frame) {
// mc++     if (frame == null) {
// mc++       return;
// mc++     }
// mc++ 
// mc++     // Update GPU image texture coordinates.
// mc++     frame.transformCoordinates2d(
// mc++         Coordinates2d.OPENGL_NORMALIZED_DEVICE_COORDINATES,
// mc++         quadCoords,
// mc++         Coordinates2d.IMAGE_NORMALIZED,
// mc++         quadImgCoords);
// mc++ 
// mc++     // Update GPU image texture coordinates.
// mc++     frame.transformCoordinates2d(
// mc++         Coordinates2d.OPENGL_NORMALIZED_DEVICE_COORDINATES,
// mc++         quadCoords,
// mc++         Coordinates2d.TEXTURE_NORMALIZED,
// mc++         quadTexCoords);
// mc++   }
// mc++ 
// mc++   private static final float[] QUAD_COORDS =
// mc++       new float[] {
// mc++         -1.0f, -1.0f, -1.0f, +1.0f, +1.0f, -1.0f, +1.0f, +1.0f,
// mc++       };
// mc++ }
