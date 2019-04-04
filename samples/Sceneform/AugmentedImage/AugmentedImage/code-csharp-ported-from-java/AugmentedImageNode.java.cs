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
// mc++ import android.content.Context;
// mc++ import android.net.Uri;
// mc++ import android.util.Log;
// mc++ import com.google.ar.core.AugmentedImage;
// mc++ import com.google.ar.sceneform.AnchorNode;
// mc++ import com.google.ar.sceneform.Node;
// mc++ import com.google.ar.sceneform.math.Vector3;
// mc++ import com.google.ar.sceneform.rendering.ModelRenderable;
// mc++ import java.util.concurrent.CompletableFuture;
// mc++ 
// mc++ /**
// mc++  * Node for rendering an augmented image. The image is framed by placing the virtual picture frame
// mc++  * at the corners of the augmented image trackable.
// mc++  */
// mc++ @SuppressWarnings({"AndroidApiChecker"})
// mc++ public class AugmentedImageNode extends AnchorNode {
// mc++ 
// mc++   private static final String TAG = "AugmentedImageNode";
// mc++ 
// mc++   // The augmented image represented by this node.
// mc++   private AugmentedImage image;
// mc++ 
// mc++   // Models of the 4 corners.  We use completable futures here to simplify
// mc++   // the error handling and asynchronous loading.  The loading is started with the
// mc++   // first construction of an instance, and then used when the image is set.
// mc++   private static CompletableFuture<ModelRenderable> ulCorner;
// mc++   private static CompletableFuture<ModelRenderable> urCorner;
// mc++   private static CompletableFuture<ModelRenderable> lrCorner;
// mc++   private static CompletableFuture<ModelRenderable> llCorner;
// mc++ 
// mc++   public AugmentedImageNode(Context context) {
// mc++     // Upon construction, start loading the models for the corners of the frame.
// mc++     if (ulCorner == null) {
// mc++       ulCorner =
// mc++           ModelRenderable.builder()
// mc++               .setSource(context, Uri.parse("models/frame_upper_left.sfb"))
// mc++               .build();
// mc++       urCorner =
// mc++           ModelRenderable.builder()
// mc++               .setSource(context, Uri.parse("models/frame_upper_right.sfb"))
// mc++               .build();
// mc++       llCorner =
// mc++           ModelRenderable.builder()
// mc++               .setSource(context, Uri.parse("models/frame_lower_left.sfb"))
// mc++               .build();
// mc++       lrCorner =
// mc++           ModelRenderable.builder()
// mc++               .setSource(context, Uri.parse("models/frame_lower_right.sfb"))
// mc++               .build();
// mc++     }
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Called when the AugmentedImage is detected and should be rendered. A Sceneform node tree is
// mc++    * created based on an Anchor created from the image. The corners are then positioned based on the
// mc++    * extents of the image. There is no need to worry about world coordinates since everything is
// mc++    * relative to the center of the image, which is the parent node of the corners.
// mc++    */
// mc++   @SuppressWarnings({"AndroidApiChecker", "FutureReturnValueIgnored"})
// mc++   public void setImage(AugmentedImage image) {
// mc++     this.image = image;
// mc++ 
// mc++     // If any of the models are not loaded, then recurse when all are loaded.
// mc++     if (!ulCorner.isDone() || !urCorner.isDone() || !llCorner.isDone() || !lrCorner.isDone()) {
// mc++       CompletableFuture.allOf(ulCorner, urCorner, llCorner, lrCorner)
// mc++           .thenAccept((Void aVoid) -> setImage(image))
// mc++           .exceptionally(
// mc++               throwable -> {
// mc++                 Log.e(TAG, "Exception loading", throwable);
// mc++                 return null;
// mc++               });
// mc++     }
// mc++ 
// mc++     // Set the anchor based on the center of the image.
// mc++     setAnchor(image.createAnchor(image.getCenterPose()));
// mc++ 
// mc++     // Make the 4 corner nodes.
// mc++     Vector3 localPosition = new Vector3();
// mc++     Node cornerNode;
// mc++ 
// mc++     // Upper left corner.
// mc++     localPosition.set(-0.5f * image.getExtentX(), 0.0f, -0.5f * image.getExtentZ());
// mc++     cornerNode = new Node();
// mc++     cornerNode.setParent(this);
// mc++     cornerNode.setLocalPosition(localPosition);
// mc++     cornerNode.setRenderable(ulCorner.getNow(null));
// mc++ 
// mc++     // Upper right corner.
// mc++     localPosition.set(0.5f * image.getExtentX(), 0.0f, -0.5f * image.getExtentZ());
// mc++     cornerNode = new Node();
// mc++     cornerNode.setParent(this);
// mc++     cornerNode.setLocalPosition(localPosition);
// mc++     cornerNode.setRenderable(urCorner.getNow(null));
// mc++ 
// mc++     // Lower right corner.
// mc++     localPosition.set(0.5f * image.getExtentX(), 0.0f, 0.5f * image.getExtentZ());
// mc++     cornerNode = new Node();
// mc++     cornerNode.setParent(this);
// mc++     cornerNode.setLocalPosition(localPosition);
// mc++     cornerNode.setRenderable(lrCorner.getNow(null));
// mc++ 
// mc++     // Lower left corner.
// mc++     localPosition.set(-0.5f * image.getExtentX(), 0.0f, 0.5f * image.getExtentZ());
// mc++     cornerNode = new Node();
// mc++     cornerNode.setParent(this);
// mc++     cornerNode.setLocalPosition(localPosition);
// mc++     cornerNode.setRenderable(llCorner.getNow(null));
// mc++   }
// mc++ 
// mc++   public AugmentedImage getImage() {
// mc++     return image;
// mc++   }
// mc++ }
