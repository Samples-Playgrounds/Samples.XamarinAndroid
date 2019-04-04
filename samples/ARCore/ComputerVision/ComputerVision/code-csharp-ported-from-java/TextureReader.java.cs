// mc++ /*
// mc++  * Copyright 2017 Google Inc. All Rights Reserved.
// mc++  *
// mc++  * Licensed under the Apache License, Version 2.0 (the "License");
// mc++  * you may not use this file except in compliance with the License.
// mc++  * You may obtain a copy of the License at
// mc++  *
// mc++  *      http://www.apache.org/licenses/LICENSE-2.0
// mc++  *
// mc++  * Unless required by applicable law or agreed to in writing, software
// mc++  * distributed under the License is distributed on an "AS IS" BASIS,
// mc++  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// mc++  * See the License for the specific language governing permissions and
// mc++  * limitations under the License.
// mc++  */
// mc++ 
// mc++ package com.google.ar.core.examples.java.computervision;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.opengl.GLES11Ext;
// mc++ import android.opengl.GLES20;
// mc++ import android.opengl.GLES30;
// mc++ import com.google.ar.core.examples.java.common.rendering.ShaderUtil;
// mc++ import java.io.IOException;
// mc++ import java.nio.ByteBuffer;
// mc++ import java.nio.ByteOrder;
// mc++ import java.nio.FloatBuffer;
// mc++ import java.nio.IntBuffer;
// mc++ 
// mc++ /**
// mc++  * Helper class for ARCore apps to read camera image from an OpenGL OES texture.
// mc++  *
// mc++  * <p>This class provides two methods for reading pixels from a texture:
// mc++  *
// mc++  * <p>(A) All-in-one method: this method utilizes two frame buffers. It does not block the caller
// mc++  * thread. Instead it submits a reading request to read pixel to back buffer from the current
// mc++  * texture, and returns pixels from the front buffer bund to texture supplied to the previous call
// mc++  * to this function. This can be done by calling submitAndAcquire() function.
// mc++  *
// mc++  * <p>(B) Asychronous method: this method utilizes multiple frame buffers and it does not block the
// mc++  * caller thread. This method allows you to read a texture in a lower frequency than rendering
// mc++  * frequency(Calling submitAndAcquire() in a lower frequency will result in an "old" image buffer
// mc++  * that was submitted a few frames ago). This method contains three routines: submitFrame(),
// mc++  * acquireFrame() and releaseFrame().
// mc++  *
// mc++  * <p>First, you call submitFrame() to submit a frame reading request. GPU will start the reading
// mc++  * process in background:
// mc++  *
// mc++  * <p>bufferIndex = submitFrame(textureId, textureWidth, textureHeight);
// mc++  *
// mc++  * <p>Second, you call acquireFrame() to get the actual image frame:
// mc++  *
// mc++  * <p>imageBuffer = acquireFrame(bufferIndex);
// mc++  *
// mc++  * <p>Last, when you finish using of the imageBuffer retured from acquireFrame(), you need to
// mc++  * release the associated frame buffer so that you can reuse it in later frame:
// mc++  *
// mc++  * <p>releaseFrame(bufferIndex);
// mc++  *
// mc++  * <p>Note: To use any of the above two methods, you need to call create() routine to initialize the
// mc++  * reader before calling any of the reading routine. You will also need to call destroy() method to
// mc++  * release the internal resource when you are done with the reader.
// mc++  */
// mc++ public class TextureReader {
// mc++   private static final String TAG = TextureReader.class.getSimpleName();
// mc++ 
// mc++   // By default, we create only two internal buffers. So you can only hold more than one buffer
// mc++   // index in your app without releasing it. If you need to hold more than one buffers, you can
// mc++   // increase the bufferCount member.
// mc++   private final int bufferCount = 2;
// mc++   private int[] frameBuffer;
// mc++   private int[] texture;
// mc++   private int[] pbo;
// mc++   private Boolean[] bufferUsed;
// mc++   private int frontIndex = -1;
// mc++   private int backIndex = -1;
// mc++ 
// mc++   // By default, the output image format is set to RGBA. You can also set it to IMAGE_FORMAT_I8.
// mc++   private int imageFormat = TextureReaderImage.IMAGE_FORMAT_RGBA;
// mc++   private int imageWidth = 0;
// mc++   private int imageHeight = 0;
// mc++   private int pixelBufferSize = 0;
// mc++   private Boolean keepAspectRatio = false;
// mc++ 
// mc++   private FloatBuffer quadVertices;
// mc++   private FloatBuffer quadTexCoord;
// mc++   private int quadProgram;
// mc++   private int quadPositionAttrib;
// mc++   private int quadTexCoordAttrib;
// mc++   private static final int COORDS_PER_VERTEX = 3;
// mc++   private static final int TEXCOORDS_PER_VERTEX = 2;
// mc++   private static final int FLOAT_SIZE = 4;
// mc++   private static final float[] QUAD_COORDS =
// mc++       new float[] {
// mc++         -1.0f, -1.0f,
// mc++          0.0f, -1.0f,
// mc++         +1.0f,  0.0f,
// mc++         +1.0f, -1.0f,
// mc++          0.0f, +1.0f,
// mc++         +1.0f,  0.0f,
// mc++       };
// mc++ 
// mc++   private static final float[] QUAD_TEXCOORDS =
// mc++       new float[] {
// mc++         0.0f, 0.0f,
// mc++         0.0f, 1.0f,
// mc++         1.0f, 0.0f,
// mc++         1.0f, 1.0f,
// mc++       };
// mc++ 
// mc++   /**
// mc++    * Creates the texture reader. This function needs to be called from the OpenGL rendering thread.
// mc++    *
// mc++    * @param format the format of the output pixel buffer. It can be one of the two values:
// mc++    *     TextureReaderImage.IMAGE_FORMAT_RGBA or TextureReaderImage.IMAGE_FORMAT_I8.
// mc++    * @param width the width of the output image.
// mc++    * @param height the height of the output image.
// mc++    * @param keepAspectRatio whether or not to keep aspect ratio. If true, the output image may be
// mc++    *     cropped if the image aspect ratio is different from the texture aspect ratio. If false, the
// mc++    *     output image covers the entire texture scope and no cropping is applied.
// mc++    */
// mc++   public void create(Context context, int format, int width, int height, Boolean keepAspectRatio)
// mc++       throws IOException {
// mc++     if (format != TextureReaderImage.IMAGE_FORMAT_RGBA
// mc++         && format != TextureReaderImage.IMAGE_FORMAT_I8) {
// mc++       throw new RuntimeException("Image format not supported.");
// mc++     }
// mc++ 
// mc++     this.keepAspectRatio = keepAspectRatio;
// mc++     imageFormat = format;
// mc++     imageWidth = width;
// mc++     imageHeight = height;
// mc++     frontIndex = -1;
// mc++     backIndex = -1;
// mc++ 
// mc++     if (imageFormat == TextureReaderImage.IMAGE_FORMAT_RGBA) {
// mc++       pixelBufferSize = imageWidth * imageHeight * 4;
// mc++     } else if (imageFormat == TextureReaderImage.IMAGE_FORMAT_I8) {
// mc++       pixelBufferSize = imageWidth * imageHeight;
// mc++     }
// mc++ 
// mc++     // Create framebuffers and PBOs.
// mc++     pbo = new int[bufferCount];
// mc++     frameBuffer = new int[bufferCount];
// mc++     texture = new int[bufferCount];
// mc++     bufferUsed = new Boolean[bufferCount];
// mc++     GLES30.glGenBuffers(bufferCount, pbo, 0);
// mc++     GLES20.glGenFramebuffers(bufferCount, frameBuffer, 0);
// mc++     GLES20.glGenTextures(bufferCount, texture, 0);
// mc++ 
// mc++     for (int i = 0; i < bufferCount; i++) {
// mc++       bufferUsed[i] = false;
// mc++       GLES20.glBindFramebuffer(GLES20.GL_FRAMEBUFFER, frameBuffer[i]);
// mc++ 
// mc++       GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, texture[i]);
// mc++       GLES30.glTexImage2D(
// mc++           GLES30.GL_TEXTURE_2D,
// mc++           0,
// mc++           imageFormat == TextureReaderImage.IMAGE_FORMAT_I8 ? GLES30.GL_R8 : GLES30.GL_RGBA,
// mc++           imageWidth,
// mc++           imageHeight,
// mc++           0,
// mc++           imageFormat == TextureReaderImage.IMAGE_FORMAT_I8 ? GLES30.GL_RED : GLES30.GL_RGBA,
// mc++           GLES30.GL_UNSIGNED_BYTE,
// mc++           null);
// mc++       GLES20.glTexParameteri(
// mc++           GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_WRAP_S, GLES20.GL_CLAMP_TO_EDGE);
// mc++       GLES20.glTexParameteri(
// mc++           GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_WRAP_T, GLES20.GL_CLAMP_TO_EDGE);
// mc++       GLES20.glTexParameteri(GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_MAG_FILTER, GLES20.GL_LINEAR);
// mc++       GLES20.glTexParameteri(GLES20.GL_TEXTURE_2D, GLES20.GL_TEXTURE_MIN_FILTER, GLES20.GL_LINEAR);
// mc++       GLES20.glFramebufferTexture2D(
// mc++           GLES20.GL_FRAMEBUFFER, GLES20.GL_COLOR_ATTACHMENT0, GLES20.GL_TEXTURE_2D, texture[i], 0);
// mc++ 
// mc++       int status = GLES20.glCheckFramebufferStatus(GLES20.GL_FRAMEBUFFER);
// mc++       if (status != GLES20.GL_FRAMEBUFFER_COMPLETE) {
// mc++         throw new RuntimeException(
// mc++             this
// mc++                 + ": Failed to set up render buffer with status "
// mc++                 + status
// mc++                 + " and error "
// mc++                 + GLES20.glGetError());
// mc++       }
// mc++ 
// mc++       // Setup PBOs
// mc++       GLES30.glBindBuffer(GLES30.GL_PIXEL_PACK_BUFFER, pbo[i]);
// mc++       GLES30.glBufferData(
// mc++           GLES30.GL_PIXEL_PACK_BUFFER, pixelBufferSize, null, GLES30.GL_DYNAMIC_READ);
// mc++       GLES30.glBindBuffer(GLES30.GL_PIXEL_PACK_BUFFER, 0);
// mc++     }
// mc++ 
// mc++     GLES20.glBindFramebuffer(GLES20.GL_FRAMEBUFFER, 0);
// mc++ 
// mc++     // Load shader program.
// mc++     int numVertices = 4;
// mc++     if (numVertices != QUAD_COORDS.length / COORDS_PER_VERTEX) {
// mc++       throw new RuntimeException("Unexpected number of vertices in BackgroundRenderer.");
// mc++     }
// mc++ 
// mc++     ByteBuffer bbVertices = ByteBuffer.allocateDirect(QUAD_COORDS.length * FLOAT_SIZE);
// mc++     bbVertices.order(ByteOrder.nativeOrder());
// mc++     quadVertices = bbVertices.asFloatBuffer();
// mc++     quadVertices.put(QUAD_COORDS);
// mc++     quadVertices.position(0);
// mc++ 
// mc++     ByteBuffer bbTexCoords =
// mc++         ByteBuffer.allocateDirect(numVertices * TEXCOORDS_PER_VERTEX * FLOAT_SIZE);
// mc++     bbTexCoords.order(ByteOrder.nativeOrder());
// mc++     quadTexCoord = bbTexCoords.asFloatBuffer();
// mc++     quadTexCoord.put(QUAD_TEXCOORDS);
// mc++     quadTexCoord.position(0);
// mc++ 
// mc++     int vertexShader =
// mc++         ShaderUtil.loadGLShader(TAG, context, GLES20.GL_VERTEX_SHADER, "shaders/gpu_download.vert");
// mc++     int fragmentShader =
// mc++         ShaderUtil.loadGLShader(
// mc++             TAG,
// mc++             context,
// mc++             GLES20.GL_FRAGMENT_SHADER,
// mc++             imageFormat == TextureReaderImage.IMAGE_FORMAT_I8
// mc++                 ? "shaders/gpu_download_i8.frag"
// mc++                 : "shaders/gpu_download_rgba.frag");
// mc++ 
// mc++     quadProgram = GLES20.glCreateProgram();
// mc++     GLES20.glAttachShader(quadProgram, vertexShader);
// mc++     GLES20.glAttachShader(quadProgram, fragmentShader);
// mc++     GLES20.glLinkProgram(quadProgram);
// mc++     GLES20.glUseProgram(quadProgram);
// mc++ 
// mc++     quadPositionAttrib = GLES20.glGetAttribLocation(quadProgram, "a_Position");
// mc++     quadTexCoordAttrib = GLES20.glGetAttribLocation(quadProgram, "a_TexCoord");
// mc++     int texLoc = GLES20.glGetUniformLocation(quadProgram, "sTexture");
// mc++     GLES20.glUniform1i(texLoc, 0);
// mc++   }
// mc++ 
// mc++   /** Destroy the texture reader. */
// mc++   public void destroy() {
// mc++     if (frameBuffer != null) {
// mc++       GLES20.glDeleteFramebuffers(bufferCount, frameBuffer, 0);
// mc++       frameBuffer = null;
// mc++     }
// mc++     if (texture != null) {
// mc++       GLES20.glDeleteTextures(bufferCount, texture, 0);
// mc++       texture = null;
// mc++     }
// mc++     if (pbo != null) {
// mc++       GLES30.glDeleteBuffers(bufferCount, pbo, 0);
// mc++       pbo = null;
// mc++     }
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Submits a frame reading request. This routine does not return the result frame buffer
// mc++    * immediately. Instead, it returns a frame buffer index, which can be used to acquire the frame
// mc++    * buffer later through acquireFrame().
// mc++    *
// mc++    * <p>If there is no enough frame buffer available, an exception will be thrown.
// mc++    *
// mc++    * @param textureId the id of the input OpenGL texture.
// mc++    * @param textureWidth width of the texture in pixels.
// mc++    * @param textureHeight height of the texture in pixels.
// mc++    * @return the index to the frame buffer this request is associated to. You should use this index
// mc++    *     to acquire the frame using acquireFrame(); and you should release the frame buffer using
// mc++    *     releaseBuffer() routine after using of the frame.
// mc++    */
// mc++   public int submitFrame(int textureId, int textureWidth, int textureHeight) {
// mc++     // Find next buffer.
// mc++     int bufferIndex = -1;
// mc++     for (int i = 0; i < bufferCount; i++) {
// mc++       if (!bufferUsed[i]) {
// mc++         bufferIndex = i;
// mc++         break;
// mc++       }
// mc++     }
// mc++     if (bufferIndex == -1) {
// mc++       throw new RuntimeException("No buffer available.");
// mc++     }
// mc++ 
// mc++     // Bind both read and write to framebuffer.
// mc++     GLES20.glBindFramebuffer(GLES20.GL_FRAMEBUFFER, frameBuffer[bufferIndex]);
// mc++ 
// mc++     // Save and setup viewport
// mc++     IntBuffer viewport = IntBuffer.allocate(4);
// mc++     GLES20.glGetIntegerv(GLES20.GL_VIEWPORT, viewport);
// mc++     GLES20.glViewport(0, 0, imageWidth, imageHeight);
// mc++ 
// mc++     // Draw texture to framebuffer.
// mc++     drawTexture(textureId, textureWidth, textureHeight);
// mc++ 
// mc++     // Start reading into PBO
// mc++     GLES30.glBindBuffer(GLES30.GL_PIXEL_PACK_BUFFER, pbo[bufferIndex]);
// mc++     GLES30.glReadBuffer(GLES30.GL_COLOR_ATTACHMENT0);
// mc++ 
// mc++     GLES30.glReadPixels(
// mc++         0,
// mc++         0,
// mc++         imageWidth,
// mc++         imageHeight,
// mc++         imageFormat == TextureReaderImage.IMAGE_FORMAT_I8 ? GLES30.GL_RED : GLES20.GL_RGBA,
// mc++         GLES20.GL_UNSIGNED_BYTE,
// mc++         0);
// mc++ 
// mc++     // Restore viewport.
// mc++     GLES20.glViewport(viewport.get(0), viewport.get(1), viewport.get(2), viewport.get(3));
// mc++ 
// mc++     GLES20.glBindFramebuffer(GLES20.GL_FRAMEBUFFER, 0);
// mc++     GLES30.glBindBuffer(GLES30.GL_PIXEL_PACK_BUFFER, 0);
// mc++ 
// mc++     bufferUsed[bufferIndex] = true;
// mc++     return bufferIndex;
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Acquires the frame requested earlier. This routine returns a TextureReaderImage object that
// mc++    * contains the pixels mapped to the frame buffer requested previously through submitFrame().
// mc++    *
// mc++    * <p>If input buffer index is invalid, an exception will be thrown.
// mc++    *
// mc++    * @param bufferIndex the index to the frame buffer to be acquired. It has to be a frame index
// mc++    *     returned from submitFrame().
// mc++    * @return a TextureReaderImage object if succeed. Null otherwise.
// mc++    */
// mc++   public TextureReaderImage acquireFrame(int bufferIndex) {
// mc++     if (bufferIndex < 0 || bufferIndex >= bufferCount || !bufferUsed[bufferIndex]) {
// mc++       throw new RuntimeException("Invalid buffer index.");
// mc++     }
// mc++ 
// mc++     // Bind the current PB and acquire the pixel buffer.
// mc++     GLES30.glBindBuffer(GLES30.GL_PIXEL_PACK_BUFFER, pbo[bufferIndex]);
// mc++     ByteBuffer mapped =
// mc++         (ByteBuffer)
// mc++             GLES30.glMapBufferRange(
// mc++                 GLES30.GL_PIXEL_PACK_BUFFER, 0, pixelBufferSize, GLES30.GL_MAP_READ_BIT);
// mc++ 
// mc++     // Wrap the mapped buffer into TextureReaderImage object.
// mc++     TextureReaderImage buffer =
// mc++         new TextureReaderImage(imageWidth, imageHeight, imageFormat, mapped);
// mc++ 
// mc++     return buffer;
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Releases a previously requested frame buffer. If input buffer index is invalid, an exception
// mc++    * will be thrown.
// mc++    *
// mc++    * @param bufferIndex the index to the frame buffer to be acquired. It has to be a frame index
// mc++    *     returned from submitFrame().
// mc++    */
// mc++   public void releaseFrame(int bufferIndex) {
// mc++     if (bufferIndex < 0 || bufferIndex >= bufferCount || !bufferUsed[bufferIndex]) {
// mc++       throw new RuntimeException("Invalid buffer index.");
// mc++     }
// mc++     GLES30.glBindBuffer(GLES30.GL_PIXEL_PACK_BUFFER, pbo[bufferIndex]);
// mc++     GLES30.glUnmapBuffer(GLES30.GL_PIXEL_PACK_BUFFER);
// mc++     GLES30.glBindBuffer(GLES30.GL_PIXEL_PACK_BUFFER, 0);
// mc++     bufferUsed[bufferIndex] = false;
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Reads pixels using dual buffers. This function sends the reading request to GPU and returns the
// mc++    * result from the previous call. Thus, the first call always returns null. The pixelBuffer member
// mc++    * in the returned object maps to the internal buffer. This buffer cannot be overrode, and it
// mc++    * becomes invalid after next call to submitAndAcquire().
// mc++    *
// mc++    * @param textureId the OpenGL texture Id.
// mc++    * @param textureWidth width of the texture in pixels.
// mc++    * @param textureHeight height of the texture in pixels.
// mc++    * @return a TextureReaderImage object that contains the pixels read from the texture.
// mc++    */
// mc++   public TextureReaderImage submitAndAcquire(int textureId, int textureWidth, int textureHeight) {
// mc++     // Release previously used front buffer.
// mc++     if (frontIndex != -1) {
// mc++       releaseFrame(frontIndex);
// mc++     }
// mc++ 
// mc++     // Move previous back buffer to front buffer.
// mc++     frontIndex = backIndex;
// mc++ 
// mc++     // Submit new request on back buffer.
// mc++     backIndex = submitFrame(textureId, textureWidth, textureHeight);
// mc++ 
// mc++     // Acquire frame from the new front buffer.
// mc++     if (frontIndex != -1) {
// mc++       return acquireFrame(frontIndex);
// mc++     }
// mc++ 
// mc++     return null;
// mc++   }
// mc++ 
// mc++   /** Draws texture to full screen. */
// mc++   private void drawTexture(int textureId, int textureWidth, int textureHeight) {
// mc++     // Disable features that we don't use.
// mc++     GLES20.glDisable(GLES20.GL_DEPTH_TEST);
// mc++     GLES20.glDisable(GLES20.GL_CULL_FACE);
// mc++     GLES20.glDisable(GLES20.GL_SCISSOR_TEST);
// mc++     GLES20.glDisable(GLES20.GL_STENCIL_TEST);
// mc++     GLES20.glDisable(GLES20.GL_BLEND);
// mc++     GLES20.glDepthMask(false);
// mc++     GLES20.glBindBuffer(GLES20.GL_ARRAY_BUFFER, 0);
// mc++     GLES20.glBindBuffer(GLES20.GL_ELEMENT_ARRAY_BUFFER, 0);
// mc++     GLES30.glBindVertexArray(0);
// mc++ 
// mc++     // Clear buffers.
// mc++     GLES20.glClearColor(0, 0, 0, 0);
// mc++     GLES20.glClear(GLES20.GL_COLOR_BUFFER_BIT | GLES20.GL_DEPTH_BUFFER_BIT);
// mc++ 
// mc++     // Set the vertex positions.
// mc++     GLES20.glVertexAttribPointer(
// mc++         quadPositionAttrib, COORDS_PER_VERTEX, GLES20.GL_FLOAT, false, 0, quadVertices);
// mc++ 
// mc++     // Calculate the texture coordinates.
// mc++     if (keepAspectRatio) {
// mc++       int renderWidth = 0;
// mc++       int renderHeight = 0;
// mc++       float textureAspectRatio = (float) textureWidth / textureHeight;
// mc++       float imageAspectRatio = (float) imageWidth / imageHeight;
// mc++       if (textureAspectRatio < imageAspectRatio) {
// mc++         renderWidth = imageWidth;
// mc++         renderHeight = textureHeight * imageWidth / textureWidth;
// mc++       } else {
// mc++         renderWidth = textureWidth * imageHeight / textureHeight;
// mc++         renderHeight = imageHeight;
// mc++       }
// mc++       float offsetU = (float) (renderWidth - imageWidth) / renderWidth / 2;
// mc++       float offsetV = (float) (renderHeight - imageHeight) / renderHeight / 2;
// mc++ 
// mc++       float[] texCoords =
// mc++           new float[] {
// mc++             offsetU, offsetV, offsetU, 1 - offsetV, 1 - offsetU, offsetV, 1 - offsetU, 1 - offsetV
// mc++           };
// mc++ 
// mc++       quadTexCoord.put(texCoords);
// mc++       quadTexCoord.position(0);
// mc++     } else {
// mc++       quadTexCoord.put(QUAD_TEXCOORDS);
// mc++       quadTexCoord.position(0);
// mc++     }
// mc++ 
// mc++     // Set the texture coordinates.
// mc++     GLES20.glVertexAttribPointer(
// mc++         quadTexCoordAttrib, TEXCOORDS_PER_VERTEX, GLES20.GL_FLOAT, false, 0, quadTexCoord);
// mc++ 
// mc++     // Enable vertex arrays
// mc++     GLES20.glEnableVertexAttribArray(quadPositionAttrib);
// mc++     GLES20.glEnableVertexAttribArray(quadTexCoordAttrib);
// mc++ 
// mc++     GLES20.glUseProgram(quadProgram);
// mc++ 
// mc++     // Select input texture.
// mc++     GLES20.glActiveTexture(GLES20.GL_TEXTURE0);
// mc++     GLES20.glBindTexture(GLES11Ext.GL_TEXTURE_EXTERNAL_OES, textureId);
// mc++ 
// mc++     // Draw a quad with texture.
// mc++     GLES20.glDrawArrays(GLES20.GL_TRIANGLE_STRIP, 0, 4);
// mc++ 
// mc++     // Disable vertex arrays
// mc++     GLES20.glDisableVertexAttribArray(quadPositionAttrib);
// mc++     GLES20.glDisableVertexAttribArray(quadTexCoordAttrib);
// mc++ 
// mc++     // Reset texture binding.
// mc++     GLES20.glBindTexture(GLES11Ext.GL_TEXTURE_EXTERNAL_OES, 0);
// mc++   }
// mc++ }
