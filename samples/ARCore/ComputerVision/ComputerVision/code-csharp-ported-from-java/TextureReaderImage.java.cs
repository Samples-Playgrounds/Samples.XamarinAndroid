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
// mc++ package com.google.ar.core.examples.java.computervision;
// mc++ 
// mc++ import java.nio.ByteBuffer;
// mc++ 
// mc++ /** Image Buffer Class. */
// mc++ public class TextureReaderImage {
// mc++   /** The id corresponding to RGBA8888. */
// mc++   public static final int IMAGE_FORMAT_RGBA = 0;
// mc++ 
// mc++   /** The id corresponding to grayscale. */
// mc++   public static final int IMAGE_FORMAT_I8 = 1;
// mc++ 
// mc++   /** The width of the image, in pixels. */
// mc++   public int width;
// mc++ 
// mc++   /** The height of the image, in pixels. */
// mc++   public int height;
// mc++ 
// mc++   /** The image buffer. */
// mc++   public ByteBuffer buffer;
// mc++ 
// mc++   /** Pixel format. Can be either IMAGE_FORMAT_RGBA or IMAGE_FORMAT_I8. */
// mc++   public int format;
// mc++ 
// mc++   /** Default constructor. */
// mc++   public TextureReaderImage() {
// mc++     width = 1;
// mc++     height = 1;
// mc++     format = IMAGE_FORMAT_RGBA;
// mc++     buffer = ByteBuffer.allocateDirect(4);
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Constructor.
// mc++    *
// mc++    * @param imgWidth the width of the image, in pixels.
// mc++    * @param imgHeight the height of the image, in pixels.
// mc++    * @param imgFormat the format of the image.
// mc++    * @param imgBuffer the buffer of the image pixels.
// mc++    */
// mc++   public TextureReaderImage(int imgWidth, int imgHeight, int imgFormat, ByteBuffer imgBuffer) {
// mc++     if (imgWidth == 0 || imgHeight == 0) {
// mc++       throw new RuntimeException("Invalid image size.");
// mc++     }
// mc++ 
// mc++     if (imgFormat != IMAGE_FORMAT_RGBA && imgFormat != IMAGE_FORMAT_I8) {
// mc++       throw new RuntimeException("Invalid image format.");
// mc++     }
// mc++ 
// mc++     if (imgBuffer == null) {
// mc++       throw new RuntimeException("Pixel buffer cannot be null.");
// mc++     }
// mc++ 
// mc++     width = imgWidth;
// mc++     height = imgHeight;
// mc++     format = imgFormat;
// mc++     buffer = imgBuffer;
// mc++   }
// mc++ }
