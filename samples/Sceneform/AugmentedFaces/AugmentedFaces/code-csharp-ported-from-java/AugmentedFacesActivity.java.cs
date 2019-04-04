// mc++ /*
// mc++  * Copyright 2019 Google LLC. All Rights Reserved.
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
// mc++ package com.google.ar.sceneform.samples.augmentedfaces;
// mc++ 
// mc++ import android.app.Activity;
// mc++ import android.app.ActivityManager;
// mc++ import android.content.Context;
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.util.Log;
// mc++ import android.widget.Toast;
// mc++ import com.google.ar.core.ArCoreApk;
// mc++ import com.google.ar.core.AugmentedFace;
// mc++ import com.google.ar.core.TrackingState;
// mc++ import com.google.ar.sceneform.ArSceneView;
// mc++ import com.google.ar.sceneform.FrameTime;
// mc++ import com.google.ar.sceneform.Scene;
// mc++ import com.google.ar.sceneform.rendering.ModelRenderable;
// mc++ import com.google.ar.sceneform.rendering.Renderable;
// mc++ import com.google.ar.sceneform.rendering.Texture;
// mc++ import com.google.ar.sceneform.ux.AugmentedFaceNode;
// mc++ import java.util.Collection;
// mc++ import java.util.HashMap;
// mc++ import java.util.Iterator;
// mc++ import java.util.Map;
// mc++ 
// mc++ /**
// mc++  * This is an example activity that uses the Sceneform UX package to make common Augmented Faces
// mc++  * tasks easier.
// mc++  */
// mc++ public class AugmentedFacesActivity extends AppCompatActivity {
// mc++   private static final String TAG = AugmentedFacesActivity.class.getSimpleName();
// mc++ 
// mc++   private static final double MIN_OPENGL_VERSION = 3.0;
// mc++ 
// mc++   private FaceArFragment arFragment;
// mc++ 
// mc++   private ModelRenderable faceRegionsRenderable;
// mc++   private Texture faceMeshTexture;
// mc++ 
// mc++   private final HashMap<AugmentedFace, AugmentedFaceNode> faceNodeMap = new HashMap<>();
// mc++ 
// mc++   @Override
// mc++   @SuppressWarnings({"AndroidApiChecker", "FutureReturnValueIgnored"})
// mc++   // CompletableFuture requires api level 24
// mc++   // FutureReturnValueIgnored is not valid
// mc++   protected void onCreate(Bundle savedInstanceState) {
// mc++     super.onCreate(savedInstanceState);
// mc++ 
// mc++     if (!checkIsSupportedDeviceOrFinish(this)) {
// mc++       return;
// mc++     }
// mc++ 
// mc++     setContentView(R.layout.activity_face_mesh);
// mc++     arFragment = (FaceArFragment) getSupportFragmentManager().findFragmentById(R.id.face_fragment);
// mc++ 
// mc++     // Load the face regions renderable.
// mc++     // This is a skinned model that renders 3D objects mapped to the regions of the augmented face.
// mc++     ModelRenderable.builder()
// mc++         .setSource(this, R.raw.fox_face)
// mc++         .build()
// mc++         .thenAccept(
// mc++             modelRenderable -> {
// mc++               faceRegionsRenderable = modelRenderable;
// mc++               modelRenderable.setShadowCaster(false);
// mc++               modelRenderable.setShadowReceiver(false);
// mc++             });
// mc++ 
// mc++     // Load the face mesh texture.
// mc++     Texture.builder()
// mc++         .setSource(this, R.drawable.fox_face_mesh_texture)
// mc++         .build()
// mc++         .thenAccept(texture -> faceMeshTexture = texture);
// mc++ 
// mc++     ArSceneView sceneView = arFragment.getArSceneView();
// mc++ 
// mc++     // This is important to make sure that the camera stream renders first so that
// mc++     // the face mesh occlusion works correctly.
// mc++     sceneView.setCameraStreamRenderPriority(Renderable.RENDER_PRIORITY_FIRST);
// mc++ 
// mc++     Scene scene = sceneView.getScene();
// mc++ 
// mc++     scene.addOnUpdateListener(
// mc++         (FrameTime frameTime) -> {
// mc++           if (faceRegionsRenderable == null || faceMeshTexture == null) {
// mc++             return;
// mc++           }
// mc++ 
// mc++           Collection<AugmentedFace> faceList =
// mc++               sceneView.getSession().getAllTrackables(AugmentedFace.class);
// mc++ 
// mc++           // Make new AugmentedFaceNodes for any new faces.
// mc++           for (AugmentedFace face : faceList) {
// mc++             if (!faceNodeMap.containsKey(face)) {
// mc++               AugmentedFaceNode faceNode = new AugmentedFaceNode(face);
// mc++               faceNode.setParent(scene);
// mc++               faceNode.setFaceRegionsRenderable(faceRegionsRenderable);
// mc++               faceNode.setFaceMeshTexture(faceMeshTexture);
// mc++               faceNodeMap.put(face, faceNode);
// mc++             }
// mc++           }
// mc++ 
// mc++           // Remove any AugmentedFaceNodes associated with an AugmentedFace that stopped tracking.
// mc++           Iterator<Map.Entry<AugmentedFace, AugmentedFaceNode>> iter =
// mc++               faceNodeMap.entrySet().iterator();
// mc++           while (iter.hasNext()) {
// mc++             Map.Entry<AugmentedFace, AugmentedFaceNode> entry = iter.next();
// mc++             AugmentedFace face = entry.getKey();
// mc++             if (face.getTrackingState() == TrackingState.STOPPED) {
// mc++               AugmentedFaceNode faceNode = entry.getValue();
// mc++               faceNode.setParent(null);
// mc++               iter.remove();
// mc++             }
// mc++           }
// mc++         });
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Returns false and displays an error message if Sceneform can not run, true if Sceneform can run
// mc++    * on this device.
// mc++    *
// mc++    * <p>Sceneform requires Android N on the device as well as OpenGL 3.0 capabilities.
// mc++    *
// mc++    * <p>Finishes the activity if Sceneform can not run
// mc++    */
// mc++   public static boolean checkIsSupportedDeviceOrFinish(final Activity activity) {
// mc++     if (ArCoreApk.getInstance().checkAvailability(activity)
// mc++         == ArCoreApk.Availability.UNSUPPORTED_DEVICE_NOT_CAPABLE) {
// mc++       Log.e(TAG, "Augmented Faces requires ArCore.");
// mc++       Toast.makeText(activity, "Augmented Faces requires ArCore", Toast.LENGTH_LONG).show();
// mc++       activity.finish();
// mc++       return false;
// mc++     }
// mc++     String openGlVersionString =
// mc++         ((ActivityManager) activity.getSystemService(Context.ACTIVITY_SERVICE))
// mc++             .getDeviceConfigurationInfo()
// mc++             .getGlEsVersion();
// mc++     if (Double.parseDouble(openGlVersionString) < MIN_OPENGL_VERSION) {
// mc++       Log.e(TAG, "Sceneform requires OpenGL ES 3.0 later");
// mc++       Toast.makeText(activity, "Sceneform requires OpenGL ES 3.0 or later", Toast.LENGTH_LONG)
// mc++           .show();
// mc++       activity.finish();
// mc++       return false;
// mc++     }
// mc++     return true;
// mc++   }
// mc++ }
