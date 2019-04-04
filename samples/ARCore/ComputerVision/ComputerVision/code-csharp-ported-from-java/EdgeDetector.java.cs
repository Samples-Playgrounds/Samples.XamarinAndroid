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
// mc++ import java.nio.ByteBuffer;
// mc++ 
// mc++ /** Detects edges from input YUV image. */
// mc++ public class EdgeDetector {
// mc++   private byte[] inputPixels = new byte[0]; // Reuse java byte array to avoid multiple allocations.
// mc++ 
// mc++   private static final int SOBEL_EDGE_THRESHOLD = 128 * 128;
// mc++ 
// mc++   /**
// mc++    * Process a grayscale image using the Sobel edge detector.
// mc++    *
// mc++    * @param width image width.
// mc++    * @param height image height.
// mc++    * @param stride image stride (number of bytes per row, equals to width if no row padding).
// mc++    * @param input bytes of the image, assumed single channel grayscale of size [stride * height].
// mc++    * @return bytes of the processed image, where the byte value is the strength of the edge at that
// mc++    *     pixel. Number of bytes is width * height, row padding (if any) is removed.
// mc++    */
// mc++   public synchronized ByteBuffer detect(int width, int height, int stride, ByteBuffer input) {
// mc++     // Reallocate input byte array if its size is different from the required size.
// mc++     if (stride * height > inputPixels.length) {
// mc++       inputPixels = new byte[stride * height];
// mc++     }
// mc++ 
// mc++     // Allocate a new output byte array.
// mc++     byte[] outputPixels = new byte[width * height];
// mc++ 
// mc++     // Copy input buffer into a java array for ease of access. This is not the most optimal
// mc++     // way to process an image, but used here for simplicity.
// mc++     input.position(0);
// mc++ 
// mc++     // Note: On certain devices with specific resolution where the stride is not equal to the width.
// mc++     // In such situation the memory allocated for the frame may not be exact multiple of stride x
// mc++     // height hence the capacity of the ByteBuffer could be less. To handle such situations it will
// mc++     // be better to transfer the exact amount of image bytes to the destination bytes.
// mc++     input.get(inputPixels, 0, input.capacity());
// mc++ 
// mc++     // Detect edges.
// mc++     for (int j = 1; j < height - 1; j++) {
// mc++       for (int i = 1; i < width - 1; i++) {
// mc++         // Offset of the pixel at [i, j] of the input image.
// mc++         int offset = (j * stride) + i;
// mc++ 
// mc++         // Neighbour pixels around the pixel at [i, j].
// mc++         int a00 = inputPixels[offset - stride - 1];
// mc++         int a01 = inputPixels[offset - stride];
// mc++         int a02 = inputPixels[offset - stride + 1];
// mc++         int a10 = inputPixels[offset - 1];
// mc++         int a12 = inputPixels[offset + 1];
// mc++         int a20 = inputPixels[offset + stride - 1];
// mc++         int a21 = inputPixels[offset + stride];
// mc++         int a22 = inputPixels[offset + stride + 1];
// mc++ 
// mc++         // Sobel X filter:
// mc++         //   -1, 0, 1,
// mc++         //   -2, 0, 2,
// mc++         //   -1, 0, 1
// mc++         int xSum = -a00 - (2 * a10) - a20 + a02 + (2 * a12) + a22;
// mc++ 
// mc++         // Sobel Y filter:
// mc++         //    1, 2, 1,
// mc++         //    0, 0, 0,
// mc++         //   -1, -2, -1
// mc++         int ySum = a00 + (2 * a01) + a02 - a20 - (2 * a21) - a22;
// mc++ 
// mc++         if ((xSum * xSum) + (ySum * ySum) > SOBEL_EDGE_THRESHOLD) {
// mc++           outputPixels[(j * width) + i] = (byte) 0xFF;
// mc++         } else {
// mc++           outputPixels[(j * width) + i] = (byte) 0x1F;
// mc++         }
// mc++       }
// mc++     }
// mc++ 
// mc++     return ByteBuffer.wrap(outputPixels);
// mc++   }
// mc++ }
