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
// mc++ import android.opengl.GLUtils;
// mc++ import android.opengl.Matrix;
// mc++ import de.javagl.obj.Obj;
// mc++ import de.javagl.obj.ObjData;
// mc++ import de.javagl.obj.ObjReader;
// mc++ import de.javagl.obj.ObjUtils;
// mc++ import java.io.IOException;
// mc++ import java.io.InputStream;
// mc++ import java.nio.ByteBuffer;
// mc++ import java.nio.ByteOrder;
// mc++ import java.nio.FloatBuffer;
// mc++ import java.nio.IntBuffer;
// mc++ import java.nio.ShortBuffer;
// mc++ 
// mc++ /** Renders an object loaded from an OBJ file in OpenGL. */
// mc++ public class ObjectRenderer {
// mc++   private static final String TAG = ObjectRenderer.class.getSimpleName();
// mc++ 
// mc++   /**
// mc++    * Blend mode.
// mc++    *
// mc++    * @see #setBlendMode(BlendMode)
// mc++    */
// mc++   public enum BlendMode {
// mc++     /** Multiplies the destination color by the source alpha. */
// mc++     Shadow,
// mc++     /** Normal alpha blending. */
// mc++     Grid
// mc++   }
// mc++ 
// mc++   // Shader names.
// mc++   private static final String VERTEX_SHADER_NAME = "shaders/object.vert";
// mc++   private static final String FRAGMENT_SHADER_NAME = "shaders/object.frag";
// mc++ 
// mc++   private static final int COORDS_PER_VERTEX = 3;
// mc++   private static final float[] DEFAULT_COLOR = new float[] {0f, 0f, 0f, 0f};
// mc++ 
// mc++   // Note: the last component must be zero to avoid applying the translational part of the matrix.
// mc++   private static final float[] LIGHT_DIRECTION = new float[] {0.250f, 0.866f, 0.433f, 0.0f};
// mc++   private final float[] viewLightDirection = new float[4];
// mc++ 
// mc++   // Object vertex buffer variables.
// mc++   private int vertexBufferId;
// mc++   private int verticesBaseAddress;
// mc++   private int texCoordsBaseAddress;
// mc++   private int normalsBaseAddress;
// mc++   private int indexBufferId;
// mc++   private int indexCount;
// mc++ 
// mc++   private int program;
// mc++   private final int[] textures = new int[1];
// mc++ 
// mc++   // Shader location: model view projection matrix.
// mc++   private int modelViewUniform;
// mc++   private int modelViewProjectionUniform;
// mc++ 
// mc++   // Shader location: object attributes.
// mc++   private int positionAttribute;
// mc++   private int normalAttribute;
// mc++   private int texCoordAttribute;
// mc++ 
// mc++   // Shader location: texture sampler.
// mc++   private int textureUniform;
// mc++ 
// mc++   // Shader location: environment properties.
// mc++   private int lightingParametersUniform;
// mc++ 
// mc++   // Shader location: material properties.
// mc++   private int materialParametersUniform;
// mc++ 
// mc++   // Shader location: color correction property
// mc++   private int colorCorrectionParameterUniform;
// mc++ 
// mc++   // Shader location: object color property (to change the primary color of the object).
// mc++   private int colorUniform;
// mc++ 
// mc++   private BlendMode blendMode = null;
// mc++ 
// mc++   // Temporary matrices allocated here to reduce number of allocations for each frame.
// mc++   private final float[] modelMatrix = new float[16];
// mc++   private final float[] modelViewMatrix = new float[16];
// mc++   private final float[] modelViewProjectionMatrix = new float[16];
// mc++ 
// mc++   // Set some default material properties to use for lighting.
// mc++   private float ambient = 0.3f;
// mc++   private float diffuse = 1.0f;
// mc++   private float specular = 1.0f;
// mc++   private float specularPower = 6.0f;
// mc++ 
// mc++   public ObjectRenderer() {}
// mc++ 
// mc++   /**
// mc++    * Creates and initializes OpenGL resources needed for rendering the model.
// mc++    *
// mc++    * @param context Context for loading the shader and below-named model and texture assets.
// mc++    * @param objAssetName Name of the OBJ file containing the model geometry.
// mc++    * @param diffuseTextureAssetName Name of the PNG file containing the diffuse texture map.
// mc++    */
// mc++   public void createOnGlThread(Context context, String objAssetName, String diffuseTextureAssetName)
// mc++       throws IOException {
// mc++     final int vertexShader =
// mc++         ShaderUtil.loadGLShader(TAG, context, GLES20.GL_VERTEX_SHADER, VERTEX_SHADER_NAME);
// mc++     final int fragmentShader =
// mc++         ShaderUtil.loadGLShader(TAG, context, GLES20.GL_FRAGMENT_SHADER, FRAGMENT_SHADER_NAME);
// mc++ 
// mc++     program = GLES20.glCreateProgram();
// mc++     GLES20.glAttachShader(program, vertexShader);
// mc++     GLES20.glAttachShader(program, fragmentShader);
// mc++     GLES20.glLinkProgram(program);
// mc++     GLES20.glUseProgram(program);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "Program creation");
// mc++ 
// mc++     modelViewUniform = GLES20.glGetUniformLocation(program, "u_ModelView");
// mc++     modelViewProjectionUniform = GLES20.glGetUniformLocation(program, "u_ModelViewProjection");
// mc++ 
// mc++     positionAttribute = GLES20.glGetAttribLocation(program, "a_Position");
// mc++     normalAttribute = GLES20.glGetAttribLocation(program, "a_Normal");
// mc++     texCoordAttribute = GLES20.glGetAttribLocation(program, "a_TexCoord");
// mc++ 
// mc++     textureUniform = GLES20.glGetUniformLocation(program, "u_Texture");
// mc++ 
// mc++     lightingParametersUniform = GLES20.glGetUniformLocation(program, "u_LightingParameters");
// mc++     materialParametersUniform = GLES20.glGetUniformLocation(program, "u_MaterialParameters");
// mc++     colorCorrectionParameterUniform =
// mc++         GLES20.glGetUniformLocation(program, "u_ColorCorrectionParameters");
// mc++     colorUniform = GLES20.glGetUniformLocation(program, "u_ObjColor");
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "Program parameters");
// mc++ 
// mc++     // Read the texture.
// mc++     Bitmap textureBitmap =
// mc++         BitmapFactory.decodeStream(context.getAssets().open(diffuseTextureAssetName));
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
// mc++     textureBitmap.recycle();
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "Texture loading");
// mc++ 
// mc++     // Read the obj file.
// mc++     InputStream objInputStream = context.getAssets().open(objAssetName);
// mc++     Obj obj = ObjReader.read(objInputStream);
// mc++ 
// mc++     // Prepare the Obj so that its structure is suitable for
// mc++     // rendering with OpenGL:
// mc++     // 1. Triangulate it
// mc++     // 2. Make sure that texture coordinates are not ambiguous
// mc++     // 3. Make sure that normals are not ambiguous
// mc++     // 4. Convert it to single-indexed data
// mc++     obj = ObjUtils.convertToRenderable(obj);
// mc++ 
// mc++     // OpenGL does not use Java arrays. ByteBuffers are used instead to provide data in a format
// mc++     // that OpenGL understands.
// mc++ 
// mc++     // Obtain the data from the OBJ, as direct buffers:
// mc++     IntBuffer wideIndices = ObjData.getFaceVertexIndices(obj, 3);
// mc++     FloatBuffer vertices = ObjData.getVertices(obj);
// mc++     FloatBuffer texCoords = ObjData.getTexCoords(obj, 2);
// mc++     FloatBuffer normals = ObjData.getNormals(obj);
// mc++ 
// mc++     // Convert int indices to shorts for GL ES 2.0 compatibility
// mc++     ShortBuffer indices =
// mc++         ByteBuffer.allocateDirect(2 * wideIndices.limit())
// mc++             .order(ByteOrder.nativeOrder())
// mc++             .asShortBuffer();
// mc++     while (wideIndices.hasRemaining()) {
// mc++       indices.put((short) wideIndices.get());
// mc++     }
// mc++     indices.rewind();
// mc++ 
// mc++     int[] buffers = new int[2];
// mc++     GLES20.glGenBuffers(2, buffers, 0);
// mc++     vertexBufferId = buffers[0];
// mc++     indexBufferId = buffers[1];
// mc++ 
// mc++     // Load vertex buffer
// mc++     verticesBaseAddress = 0;
// mc++     texCoordsBaseAddress = verticesBaseAddress + 4 * vertices.limit();
// mc++     normalsBaseAddress = texCoordsBaseAddress + 4 * texCoords.limit();
// mc++     final int totalBytes = normalsBaseAddress + 4 * normals.limit();
// mc++ 
// mc++     GLES20.glBindBuffer(GLES20.GL_ARRAY_BUFFER, vertexBufferId);
// mc++     GLES20.glBufferData(GLES20.GL_ARRAY_BUFFER, totalBytes, null, GLES20.GL_STATIC_DRAW);
// mc++     GLES20.glBufferSubData(
// mc++         GLES20.GL_ARRAY_BUFFER, verticesBaseAddress, 4 * vertices.limit(), vertices);
// mc++     GLES20.glBufferSubData(
// mc++         GLES20.GL_ARRAY_BUFFER, texCoordsBaseAddress, 4 * texCoords.limit(), texCoords);
// mc++     GLES20.glBufferSubData(
// mc++         GLES20.GL_ARRAY_BUFFER, normalsBaseAddress, 4 * normals.limit(), normals);
// mc++     GLES20.glBindBuffer(GLES20.GL_ARRAY_BUFFER, 0);
// mc++ 
// mc++     // Load index buffer
// mc++     GLES20.glBindBuffer(GLES20.GL_ELEMENT_ARRAY_BUFFER, indexBufferId);
// mc++     indexCount = indices.limit();
// mc++     GLES20.glBufferData(
// mc++         GLES20.GL_ELEMENT_ARRAY_BUFFER, 2 * indexCount, indices, GLES20.GL_STATIC_DRAW);
// mc++     GLES20.glBindBuffer(GLES20.GL_ELEMENT_ARRAY_BUFFER, 0);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "OBJ buffer load");
// mc++ 
// mc++     Matrix.setIdentityM(modelMatrix, 0);
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Selects the blending mode for rendering.
// mc++    *
// mc++    * @param blendMode The blending mode. Null indicates no blending (opaque rendering).
// mc++    */
// mc++   public void setBlendMode(BlendMode blendMode) {
// mc++     this.blendMode = blendMode;
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Updates the object model matrix and applies scaling.
// mc++    *
// mc++    * @param modelMatrix A 4x4 model-to-world transformation matrix, stored in column-major order.
// mc++    * @param scaleFactor A separate scaling factor to apply before the {@code modelMatrix}.
// mc++    * @see android.opengl.Matrix
// mc++    */
// mc++   public void updateModelMatrix(float[] modelMatrix, float scaleFactor) {
// mc++     float[] scaleMatrix = new float[16];
// mc++     Matrix.setIdentityM(scaleMatrix, 0);
// mc++     scaleMatrix[0] = scaleFactor;
// mc++     scaleMatrix[5] = scaleFactor;
// mc++     scaleMatrix[10] = scaleFactor;
// mc++     Matrix.multiplyMM(this.modelMatrix, 0, modelMatrix, 0, scaleMatrix, 0);
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Sets the surface characteristics of the rendered model.
// mc++    *
// mc++    * @param ambient Intensity of non-directional surface illumination.
// mc++    * @param diffuse Diffuse (matte) surface reflectivity.
// mc++    * @param specular Specular (shiny) surface reflectivity.
// mc++    * @param specularPower Surface shininess. Larger values result in a smaller, sharper specular
// mc++    *     highlight.
// mc++    */
// mc++   public void setMaterialProperties(
// mc++       float ambient, float diffuse, float specular, float specularPower) {
// mc++     this.ambient = ambient;
// mc++     this.diffuse = diffuse;
// mc++     this.specular = specular;
// mc++     this.specularPower = specularPower;
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Draws the model.
// mc++    *
// mc++    * @param cameraView A 4x4 view matrix, in column-major order.
// mc++    * @param cameraPerspective A 4x4 projection matrix, in column-major order.
// mc++    * @param lightIntensity Illumination intensity. Combined with diffuse and specular material
// mc++    *     properties.
// mc++    * @see #setBlendMode(BlendMode)
// mc++    * @see #updateModelMatrix(float[], float)
// mc++    * @see #setMaterialProperties(float, float, float, float)
// mc++    * @see android.opengl.Matrix
// mc++    */
// mc++   public void draw(float[] cameraView, float[] cameraPerspective, float[] colorCorrectionRgba) {
// mc++     draw(cameraView, cameraPerspective, colorCorrectionRgba, DEFAULT_COLOR);
// mc++   }
// mc++ 
// mc++   public void draw(
// mc++       float[] cameraView,
// mc++       float[] cameraPerspective,
// mc++       float[] colorCorrectionRgba,
// mc++       float[] objColor) {
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "Before draw");
// mc++ 
// mc++     // Build the ModelView and ModelViewProjection matrices
// mc++     // for calculating object position and light.
// mc++     Matrix.multiplyMM(modelViewMatrix, 0, cameraView, 0, modelMatrix, 0);
// mc++     Matrix.multiplyMM(modelViewProjectionMatrix, 0, cameraPerspective, 0, modelViewMatrix, 0);
// mc++ 
// mc++     GLES20.glUseProgram(program);
// mc++ 
// mc++     // Set the lighting environment properties.
// mc++     Matrix.multiplyMV(viewLightDirection, 0, modelViewMatrix, 0, LIGHT_DIRECTION, 0);
// mc++     normalizeVec3(viewLightDirection);
// mc++     GLES20.glUniform4f(
// mc++         lightingParametersUniform,
// mc++         viewLightDirection[0],
// mc++         viewLightDirection[1],
// mc++         viewLightDirection[2],
// mc++         1.f);
// mc++     GLES20.glUniform4fv(colorCorrectionParameterUniform, 1, colorCorrectionRgba, 0);
// mc++ 
// mc++     // Set the object color property.
// mc++     GLES20.glUniform4fv(colorUniform, 1, objColor, 0);
// mc++ 
// mc++     // Set the object material properties.
// mc++     GLES20.glUniform4f(materialParametersUniform, ambient, diffuse, specular, specularPower);
// mc++ 
// mc++     // Attach the object texture.
// mc++     GLES20.glActiveTexture(GLES20.GL_TEXTURE0);
// mc++     GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, textures[0]);
// mc++     GLES20.glUniform1i(textureUniform, 0);
// mc++ 
// mc++     // Set the vertex attributes.
// mc++     GLES20.glBindBuffer(GLES20.GL_ARRAY_BUFFER, vertexBufferId);
// mc++ 
// mc++     GLES20.glVertexAttribPointer(
// mc++         positionAttribute, COORDS_PER_VERTEX, GLES20.GL_FLOAT, false, 0, verticesBaseAddress);
// mc++     GLES20.glVertexAttribPointer(normalAttribute, 3, GLES20.GL_FLOAT, false, 0, normalsBaseAddress);
// mc++     GLES20.glVertexAttribPointer(
// mc++         texCoordAttribute, 2, GLES20.GL_FLOAT, false, 0, texCoordsBaseAddress);
// mc++ 
// mc++     GLES20.glBindBuffer(GLES20.GL_ARRAY_BUFFER, 0);
// mc++ 
// mc++     // Set the ModelViewProjection matrix in the shader.
// mc++     GLES20.glUniformMatrix4fv(modelViewUniform, 1, false, modelViewMatrix, 0);
// mc++     GLES20.glUniformMatrix4fv(modelViewProjectionUniform, 1, false, modelViewProjectionMatrix, 0);
// mc++ 
// mc++     // Enable vertex arrays
// mc++     GLES20.glEnableVertexAttribArray(positionAttribute);
// mc++     GLES20.glEnableVertexAttribArray(normalAttribute);
// mc++     GLES20.glEnableVertexAttribArray(texCoordAttribute);
// mc++ 
// mc++     if (blendMode != null) {
// mc++       GLES20.glDepthMask(false);
// mc++       GLES20.glEnable(GLES20.GL_BLEND);
// mc++       switch (blendMode) {
// mc++         case Shadow:
// mc++           // Multiplicative blending function for Shadow.
// mc++           GLES20.glBlendFunc(GLES20.GL_ZERO, GLES20.GL_ONE_MINUS_SRC_ALPHA);
// mc++           break;
// mc++         case Grid:
// mc++           // Grid, additive blending function.
// mc++           GLES20.glBlendFunc(GLES20.GL_SRC_ALPHA, GLES20.GL_ONE_MINUS_SRC_ALPHA);
// mc++           break;
// mc++       }
// mc++     }
// mc++ 
// mc++     GLES20.glBindBuffer(GLES20.GL_ELEMENT_ARRAY_BUFFER, indexBufferId);
// mc++     GLES20.glDrawElements(GLES20.GL_TRIANGLES, indexCount, GLES20.GL_UNSIGNED_SHORT, 0);
// mc++     GLES20.glBindBuffer(GLES20.GL_ELEMENT_ARRAY_BUFFER, 0);
// mc++ 
// mc++     if (blendMode != null) {
// mc++       GLES20.glDisable(GLES20.GL_BLEND);
// mc++       GLES20.glDepthMask(true);
// mc++     }
// mc++ 
// mc++     // Disable vertex arrays
// mc++     GLES20.glDisableVertexAttribArray(positionAttribute);
// mc++     GLES20.glDisableVertexAttribArray(normalAttribute);
// mc++     GLES20.glDisableVertexAttribArray(texCoordAttribute);
// mc++ 
// mc++     GLES20.glBindTexture(GLES20.GL_TEXTURE_2D, 0);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "After draw");
// mc++   }
// mc++ 
// mc++   private static void normalizeVec3(float[] v) {
// mc++     float reciprocalLength = 1.0f / (float) Math.sqrt(v[0] * v[0] + v[1] * v[1] + v[2] * v[2]);
// mc++     v[0] *= reciprocalLength;
// mc++     v[1] *= reciprocalLength;
// mc++     v[2] *= reciprocalLength;
// mc++   }
// mc++ }
