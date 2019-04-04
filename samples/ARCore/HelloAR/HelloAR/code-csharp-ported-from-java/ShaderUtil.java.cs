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
// mc++ import android.util.Log;
// mc++ import java.io.BufferedReader;
// mc++ import java.io.IOException;
// mc++ import java.io.InputStream;
// mc++ import java.io.InputStreamReader;
// mc++ 
// mc++ /** Shader helper functions. */
// mc++ public class ShaderUtil {
// mc++   /**
// mc++    * Converts a raw text file, saved as a resource, into an OpenGL ES shader.
// mc++    *
// mc++    * @param type The type of shader we will be creating.
// mc++    * @param filename The filename of the asset file about to be turned into a shader.
// mc++    * @return The shader object handler.
// mc++    */
// mc++   public static int loadGLShader(String tag, Context context, int type, String filename)
// mc++       throws IOException {
// mc++     String code = readRawTextFileFromAssets(context, filename);
// mc++     int shader = GLES20.glCreateShader(type);
// mc++     GLES20.glShaderSource(shader, code);
// mc++     GLES20.glCompileShader(shader);
// mc++ 
// mc++     // Get the compilation status.
// mc++     final int[] compileStatus = new int[1];
// mc++     GLES20.glGetShaderiv(shader, GLES20.GL_COMPILE_STATUS, compileStatus, 0);
// mc++ 
// mc++     // If the compilation failed, delete the shader.
// mc++     if (compileStatus[0] == 0) {
// mc++       Log.e(tag, "Error compiling shader: " + GLES20.glGetShaderInfoLog(shader));
// mc++       GLES20.glDeleteShader(shader);
// mc++       shader = 0;
// mc++     }
// mc++ 
// mc++     if (shader == 0) {
// mc++       throw new RuntimeException("Error creating shader.");
// mc++     }
// mc++ 
// mc++     return shader;
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Checks if we've had an error inside of OpenGL ES, and if so what that error is.
// mc++    *
// mc++    * @param label Label to report in case of error.
// mc++    * @throws RuntimeException If an OpenGL error is detected.
// mc++    */
// mc++   public static void checkGLError(String tag, String label) {
// mc++     int lastError = GLES20.GL_NO_ERROR;
// mc++     // Drain the queue of all errors.
// mc++     int error;
// mc++     while ((error = GLES20.glGetError()) != GLES20.GL_NO_ERROR) {
// mc++       Log.e(tag, label + ": glError " + error);
// mc++       lastError = error;
// mc++     }
// mc++     if (lastError != GLES20.GL_NO_ERROR) {
// mc++       throw new RuntimeException(label + ": glError " + lastError);
// mc++     }
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Converts a raw text file into a string.
// mc++    *
// mc++    * @param filename The filename of the asset file about to be turned into a shader.
// mc++    * @return The context of the text file, or null in case of error.
// mc++    */
// mc++   private static String readRawTextFileFromAssets(Context context, String filename)
// mc++       throws IOException {
// mc++     try (InputStream inputStream = context.getAssets().open(filename);
// mc++         BufferedReader reader = new BufferedReader(new InputStreamReader(inputStream))) {
// mc++       StringBuilder sb = new StringBuilder();
// mc++       String line;
// mc++       while ((line = reader.readLine()) != null) {
// mc++         sb.append(line).append("\n");
// mc++       }
// mc++       return sb.toString();
// mc++     }
// mc++   }
// mc++ }
