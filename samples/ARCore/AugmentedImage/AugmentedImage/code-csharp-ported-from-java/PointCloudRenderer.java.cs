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
// mc++ import android.opengl.GLES20;
// mc++ import android.opengl.GLSurfaceView;
// mc++ import android.opengl.Matrix;
// mc++ import com.google.ar.core.PointCloud;
// mc++ import java.io.IOException;
// mc++ 
// mc++ /** Renders a point cloud. */
// mc++ public class PointCloudRenderer {
// mc++   private static final String TAG = PointCloud.class.getSimpleName();
// mc++ 
// mc++   // Shader names.
// mc++   private static final String VERTEX_SHADER_NAME = "shaders/point_cloud.vert";
// mc++   private static final String FRAGMENT_SHADER_NAME = "shaders/point_cloud.frag";
// mc++ 
// mc++   private static final int BYTES_PER_FLOAT = Float.SIZE / 8;
// mc++   private static final int FLOATS_PER_POINT = 4; // X,Y,Z,confidence.
// mc++   private static final int BYTES_PER_POINT = BYTES_PER_FLOAT * FLOATS_PER_POINT;
// mc++   private static final int INITIAL_BUFFER_POINTS = 1000;
// mc++ 
// mc++   private int vbo;
// mc++   private int vboSize;
// mc++ 
// mc++   private int programName;
// mc++   private int positionAttribute;
// mc++   private int modelViewProjectionUniform;
// mc++   private int colorUniform;
// mc++   private int pointSizeUniform;
// mc++ 
// mc++   private int numPoints = 0;
// mc++ 
// mc++   // Keep track of the last point cloud rendered to avoid updating the VBO if point cloud
// mc++   // was not changed.  Do this using the timestamp since we can't compare PointCloud objects.
// mc++   private long lastTimestamp = 0;
// mc++ 
// mc++   public PointCloudRenderer() {}
// mc++ 
// mc++   /**
// mc++    * Allocates and initializes OpenGL resources needed by the plane renderer. Must be called on the
// mc++    * OpenGL thread, typically in {@link GLSurfaceView.Renderer#onSurfaceCreated(GL10, EGLConfig)}.
// mc++    *
// mc++    * @param context Needed to access shader source.
// mc++    */
// mc++   public void createOnGlThread(Context context) throws IOException {
// mc++     ShaderUtil.checkGLError(TAG, "before create");
// mc++ 
// mc++     int[] buffers = new int[1];
// mc++     GLES20.glGenBuffers(1, buffers, 0);
// mc++     vbo = buffers[0];
// mc++     GLES20.glBindBuffer(GLES20.GL_ARRAY_BUFFER, vbo);
// mc++ 
// mc++     vboSize = INITIAL_BUFFER_POINTS * BYTES_PER_POINT;
// mc++     GLES20.glBufferData(GLES20.GL_ARRAY_BUFFER, vboSize, null, GLES20.GL_DYNAMIC_DRAW);
// mc++     GLES20.glBindBuffer(GLES20.GL_ARRAY_BUFFER, 0);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "buffer alloc");
// mc++ 
// mc++     int vertexShader =
// mc++         ShaderUtil.loadGLShader(TAG, context, GLES20.GL_VERTEX_SHADER, VERTEX_SHADER_NAME);
// mc++     int passthroughShader =
// mc++         ShaderUtil.loadGLShader(TAG, context, GLES20.GL_FRAGMENT_SHADER, FRAGMENT_SHADER_NAME);
// mc++ 
// mc++     programName = GLES20.glCreateProgram();
// mc++     GLES20.glAttachShader(programName, vertexShader);
// mc++     GLES20.glAttachShader(programName, passthroughShader);
// mc++     GLES20.glLinkProgram(programName);
// mc++     GLES20.glUseProgram(programName);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "program");
// mc++ 
// mc++     positionAttribute = GLES20.glGetAttribLocation(programName, "a_Position");
// mc++     colorUniform = GLES20.glGetUniformLocation(programName, "u_Color");
// mc++     modelViewProjectionUniform = GLES20.glGetUniformLocation(programName, "u_ModelViewProjection");
// mc++     pointSizeUniform = GLES20.glGetUniformLocation(programName, "u_PointSize");
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "program  params");
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Updates the OpenGL buffer contents to the provided point. Repeated calls with the same point
// mc++    * cloud will be ignored.
// mc++    */
// mc++   public void update(PointCloud cloud) {
// mc++     if (cloud.getTimestamp() == lastTimestamp) {
// mc++       // Redundant call.
// mc++       return;
// mc++     }
// mc++     ShaderUtil.checkGLError(TAG, "before update");
// mc++ 
// mc++     GLES20.glBindBuffer(GLES20.GL_ARRAY_BUFFER, vbo);
// mc++     lastTimestamp = cloud.getTimestamp();
// mc++ 
// mc++     // If the VBO is not large enough to fit the new point cloud, resize it.
// mc++     numPoints = cloud.getPoints().remaining() / FLOATS_PER_POINT;
// mc++     if (numPoints * BYTES_PER_POINT > vboSize) {
// mc++       while (numPoints * BYTES_PER_POINT > vboSize) {
// mc++         vboSize *= 2;
// mc++       }
// mc++       GLES20.glBufferData(GLES20.GL_ARRAY_BUFFER, vboSize, null, GLES20.GL_DYNAMIC_DRAW);
// mc++     }
// mc++     GLES20.glBufferSubData(
// mc++         GLES20.GL_ARRAY_BUFFER, 0, numPoints * BYTES_PER_POINT, cloud.getPoints());
// mc++     GLES20.glBindBuffer(GLES20.GL_ARRAY_BUFFER, 0);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "after update");
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Renders the point cloud. ARCore point cloud is given in world space.
// mc++    *
// mc++    * @param cameraView the camera view matrix for this frame, typically from {@link
// mc++    *     com.google.ar.core.Camera#getViewMatrix(float[], int)}.
// mc++    * @param cameraPerspective the camera projection matrix for this frame, typically from {@link
// mc++    *     com.google.ar.core.Camera#getProjectionMatrix(float[], int, float, float)}.
// mc++    */
// mc++   public void draw(float[] cameraView, float[] cameraPerspective) {
// mc++     float[] modelViewProjection = new float[16];
// mc++     Matrix.multiplyMM(modelViewProjection, 0, cameraPerspective, 0, cameraView, 0);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "Before draw");
// mc++ 
// mc++     GLES20.glUseProgram(programName);
// mc++     GLES20.glEnableVertexAttribArray(positionAttribute);
// mc++     GLES20.glBindBuffer(GLES20.GL_ARRAY_BUFFER, vbo);
// mc++     GLES20.glVertexAttribPointer(positionAttribute, 4, GLES20.GL_FLOAT, false, BYTES_PER_POINT, 0);
// mc++     GLES20.glUniform4f(colorUniform, 31.0f / 255.0f, 188.0f / 255.0f, 210.0f / 255.0f, 1.0f);
// mc++     GLES20.glUniformMatrix4fv(modelViewProjectionUniform, 1, false, modelViewProjection, 0);
// mc++     GLES20.glUniform1f(pointSizeUniform, 5.0f);
// mc++ 
// mc++     GLES20.glDrawArrays(GLES20.GL_POINTS, 0, numPoints);
// mc++     GLES20.glDisableVertexAttribArray(positionAttribute);
// mc++     GLES20.glBindBuffer(GLES20.GL_ARRAY_BUFFER, 0);
// mc++ 
// mc++     ShaderUtil.checkGLError(TAG, "Draw");
// mc++   }
// mc++ }
