// mc++ /*
// mc++  * Copyright 2018 Google LLC
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
// mc++ package com.google.ar.sceneform.samples.augmentedimage;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.view.View;
// mc++ import android.widget.ImageView;
// mc++ import com.google.ar.core.AugmentedImage;
// mc++ import com.google.ar.core.Frame;
// mc++ import com.google.ar.core.TrackingState;
// mc++ import com.google.ar.sceneform.FrameTime;
// mc++ import com.google.ar.sceneform.samples.common.helpers.SnackbarHelper;
// mc++ import com.google.ar.sceneform.ux.ArFragment;
// mc++ import java.util.Collection;
// mc++ import java.util.HashMap;
// mc++ import java.util.Map;
// mc++ 
// mc++ /**
// mc++  * This application demonstrates using augmented images to place anchor nodes. app to include image
// mc++  * tracking functionality.
// mc++  */
// mc++ public class AugmentedImageActivity extends AppCompatActivity {
// mc++ 
// mc++   private ArFragment arFragment;
// mc++   private ImageView fitToScanView;
// mc++ 
// mc++   // Augmented image and its associated center pose anchor, keyed by the augmented image in
// mc++   // the database.
// mc++   private final Map<AugmentedImage, AugmentedImageNode> augmentedImageMap = new HashMap<>();
// mc++ 
// mc++   @Override
// mc++   protected void onCreate(Bundle savedInstanceState) {
// mc++     super.onCreate(savedInstanceState);
// mc++     setContentView(R.layout.activity_main);
// mc++ 
// mc++     arFragment = (ArFragment) getSupportFragmentManager().findFragmentById(R.id.ux_fragment);
// mc++     fitToScanView = findViewById(R.id.image_view_fit_to_scan);
// mc++ 
// mc++     arFragment.getArSceneView().getScene().addOnUpdateListener(this::onUpdateFrame);
// mc++   }
// mc++ 
// mc++   @Override
// mc++   protected void onResume() {
// mc++     super.onResume();
// mc++     if (augmentedImageMap.isEmpty()) {
// mc++       fitToScanView.setVisibility(View.VISIBLE);
// mc++     }
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Registered with the Sceneform Scene object, this method is called at the start of each frame.
// mc++    *
// mc++    * @param frameTime - time since last frame.
// mc++    */
// mc++   private void onUpdateFrame(FrameTime frameTime) {
// mc++     Frame frame = arFragment.getArSceneView().getArFrame();
// mc++ 
// mc++     // If there is no frame or ARCore is not tracking yet, just return.
// mc++     if (frame == null || frame.getCamera().getTrackingState() != TrackingState.TRACKING) {
// mc++       return;
// mc++     }
// mc++ 
// mc++     Collection<AugmentedImage> updatedAugmentedImages =
// mc++         frame.getUpdatedTrackables(AugmentedImage.class);
// mc++     for (AugmentedImage augmentedImage : updatedAugmentedImages) {
// mc++       switch (augmentedImage.getTrackingState()) {
// mc++         case PAUSED:
// mc++           // When an image is in PAUSED state, but the camera is not PAUSED, it has been detected,
// mc++           // but not yet tracked.
// mc++           String text = "Detected Image " + augmentedImage.getIndex();
// mc++           SnackbarHelper.getInstance().showMessage(this, text);
// mc++           break;
// mc++ 
// mc++         case TRACKING:
// mc++           // Have to switch to UI Thread to update View.
// mc++           fitToScanView.setVisibility(View.GONE);
// mc++ 
// mc++           // Create a new anchor for newly found images.
// mc++           if (!augmentedImageMap.containsKey(augmentedImage)) {
// mc++             AugmentedImageNode node = new AugmentedImageNode(this);
// mc++             node.setImage(augmentedImage);
// mc++             augmentedImageMap.put(augmentedImage, node);
// mc++             arFragment.getArSceneView().getScene().addChild(node);
// mc++           }
// mc++           break;
// mc++ 
// mc++         case STOPPED:
// mc++           augmentedImageMap.remove(augmentedImage);
// mc++           break;
// mc++       }
// mc++     }
// mc++   }
// mc++ }
