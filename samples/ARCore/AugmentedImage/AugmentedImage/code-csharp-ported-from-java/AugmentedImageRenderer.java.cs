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
// mc++ package com.google.ar.core.examples.java.augmentedimage.rendering;
// mc++ 
// mc++ import android.content.Context;
// mc++ import com.google.ar.core.Anchor;
// mc++ import com.google.ar.core.AugmentedImage;
// mc++ import com.google.ar.core.Pose;
// mc++ import com.google.ar.core.examples.java.augmentedimage.rendering.ObjectRenderer.BlendMode;
// mc++ import java.io.IOException;
// mc++ 
// mc++ /** Renders an augmented image. */
// mc++ public class AugmentedImageRenderer {
// mc++   private static final String TAG = "AugmentedImageRenderer";
// mc++ 
// mc++   private static final float TINT_INTENSITY = 0.1f;
// mc++   private static final float TINT_ALPHA = 1.0f;
// mc++   private static final int[] TINT_COLORS_HEX = {
// mc++     0x000000, 0xF44336, 0xE91E63, 0x9C27B0, 0x673AB7, 0x3F51B5, 0x2196F3, 0x03A9F4, 0x00BCD4,
// mc++     0x009688, 0x4CAF50, 0x8BC34A, 0xCDDC39, 0xFFEB3B, 0xFFC107, 0xFF9800,
// mc++   };
// mc++ 
// mc++   private final ObjectRenderer imageFrameUpperLeft = new ObjectRenderer();
// mc++   private final ObjectRenderer imageFrameUpperRight = new ObjectRenderer();
// mc++   private final ObjectRenderer imageFrameLowerLeft = new ObjectRenderer();
// mc++   private final ObjectRenderer imageFrameLowerRight = new ObjectRenderer();
// mc++ 
// mc++   public AugmentedImageRenderer() {}
// mc++ 
// mc++   public void createOnGlThread(Context context) throws IOException {
// mc++ 
// mc++     imageFrameUpperLeft.createOnGlThread(
// mc++         context, "models/frame_upper_left.obj", "models/frame_base.png");
// mc++     imageFrameUpperLeft.setMaterialProperties(0.0f, 3.5f, 1.0f, 6.0f);
// mc++     imageFrameUpperLeft.setBlendMode(BlendMode.SourceAlpha);
// mc++ 
// mc++     imageFrameUpperRight.createOnGlThread(
// mc++         context, "models/frame_upper_right.obj", "models/frame_base.png");
// mc++     imageFrameUpperRight.setMaterialProperties(0.0f, 3.5f, 1.0f, 6.0f);
// mc++     imageFrameUpperRight.setBlendMode(BlendMode.SourceAlpha);
// mc++ 
// mc++     imageFrameLowerLeft.createOnGlThread(
// mc++         context, "models/frame_lower_left.obj", "models/frame_base.png");
// mc++     imageFrameLowerLeft.setMaterialProperties(0.0f, 3.5f, 1.0f, 6.0f);
// mc++     imageFrameLowerLeft.setBlendMode(BlendMode.SourceAlpha);
// mc++ 
// mc++     imageFrameLowerRight.createOnGlThread(
// mc++         context, "models/frame_lower_right.obj", "models/frame_base.png");
// mc++     imageFrameLowerRight.setMaterialProperties(0.0f, 3.5f, 1.0f, 6.0f);
// mc++     imageFrameLowerRight.setBlendMode(BlendMode.SourceAlpha);
// mc++   }
// mc++ 
// mc++   public void draw(
// mc++       float[] viewMatrix,
// mc++       float[] projectionMatrix,
// mc++       AugmentedImage augmentedImage,
// mc++       Anchor centerAnchor,
// mc++       float[] colorCorrectionRgba) {
// mc++     float[] tintColor =
// mc++         convertHexToColor(TINT_COLORS_HEX[augmentedImage.getIndex() % TINT_COLORS_HEX.length]);
// mc++ 
// mc++     Pose[] localBoundaryPoses = {
// mc++       Pose.makeTranslation(
// mc++           -0.5f * augmentedImage.getExtentX(),
// mc++           0.0f,
// mc++           -0.5f * augmentedImage.getExtentZ()), // upper left
// mc++       Pose.makeTranslation(
// mc++           0.5f * augmentedImage.getExtentX(),
// mc++           0.0f,
// mc++           -0.5f * augmentedImage.getExtentZ()), // upper right
// mc++       Pose.makeTranslation(
// mc++           0.5f * augmentedImage.getExtentX(),
// mc++           0.0f,
// mc++           0.5f * augmentedImage.getExtentZ()), // lower right
// mc++       Pose.makeTranslation(
// mc++           -0.5f * augmentedImage.getExtentX(),
// mc++           0.0f,
// mc++           0.5f * augmentedImage.getExtentZ()) // lower left
// mc++     };
// mc++ 
// mc++     Pose anchorPose = centerAnchor.getPose();
// mc++     Pose[] worldBoundaryPoses = new Pose[4];
// mc++     for (int i = 0; i < 4; ++i) {
// mc++       worldBoundaryPoses[i] = anchorPose.compose(localBoundaryPoses[i]);
// mc++     }
// mc++ 
// mc++     float scaleFactor = 1.0f;
// mc++     float[] modelMatrix = new float[16];
// mc++ 
// mc++     worldBoundaryPoses[0].toMatrix(modelMatrix, 0);
// mc++     imageFrameUpperLeft.updateModelMatrix(modelMatrix, scaleFactor);
// mc++     imageFrameUpperLeft.draw(viewMatrix, projectionMatrix, colorCorrectionRgba, tintColor);
// mc++ 
// mc++     worldBoundaryPoses[1].toMatrix(modelMatrix, 0);
// mc++     imageFrameUpperRight.updateModelMatrix(modelMatrix, scaleFactor);
// mc++     imageFrameUpperRight.draw(viewMatrix, projectionMatrix, colorCorrectionRgba, tintColor);
// mc++ 
// mc++     worldBoundaryPoses[2].toMatrix(modelMatrix, 0);
// mc++     imageFrameLowerRight.updateModelMatrix(modelMatrix, scaleFactor);
// mc++     imageFrameLowerRight.draw(viewMatrix, projectionMatrix, colorCorrectionRgba, tintColor);
// mc++ 
// mc++     worldBoundaryPoses[3].toMatrix(modelMatrix, 0);
// mc++     imageFrameLowerLeft.updateModelMatrix(modelMatrix, scaleFactor);
// mc++     imageFrameLowerLeft.draw(viewMatrix, projectionMatrix, colorCorrectionRgba, tintColor);
// mc++   }
// mc++ 
// mc++   private static float[] convertHexToColor(int colorHex) {
// mc++     // colorHex is in 0xRRGGBB format
// mc++     float red = ((colorHex & 0xFF0000) >> 16) / 255.0f * TINT_INTENSITY;
// mc++     float green = ((colorHex & 0x00FF00) >> 8) / 255.0f * TINT_INTENSITY;
// mc++     float blue = (colorHex & 0x0000FF) / 255.0f * TINT_INTENSITY;
// mc++     return new float[] {red, green, blue, TINT_ALPHA};
// mc++   }
// mc++ }
