// mc++ /*
// mc++  * Copyright 2018 Google LLC. All Rights Reserved.
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
// mc++ package com.google.ar.sceneform.samples.hellosceneform;
// mc++ 
// mc++ import android.app.Activity;
// mc++ import android.app.ActivityManager;
// mc++ import android.content.Context;
// mc++ import android.os.Build;
// mc++ import android.os.Build.VERSION_CODES;
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.util.Log;
// mc++ import android.view.Gravity;
// mc++ import android.view.MotionEvent;
// mc++ import android.widget.Toast;
// mc++ import com.google.ar.core.Anchor;
// mc++ import com.google.ar.core.HitResult;
// mc++ import com.google.ar.core.Plane;
// mc++ import com.google.ar.sceneform.AnchorNode;
// mc++ import com.google.ar.sceneform.rendering.ModelRenderable;
// mc++ import com.google.ar.sceneform.ux.ArFragment;
// mc++ import com.google.ar.sceneform.ux.TransformableNode;
// mc++ 
// mc++ /**
// mc++  * This is an example activity that uses the Sceneform UX package to make common AR tasks easier.
// mc++  */
// mc++ public class HelloSceneformActivity extends AppCompatActivity {
// mc++   private static final String TAG = HelloSceneformActivity.class.getSimpleName();
// mc++   private static final double MIN_OPENGL_VERSION = 3.0;
// mc++ 
// mc++   private ArFragment arFragment;
// mc++   private ModelRenderable andyRenderable;
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
// mc++     setContentView(R.layout.activity_ux);
// mc++     arFragment = (ArFragment) getSupportFragmentManager().findFragmentById(R.id.ux_fragment);
// mc++ 
// mc++     // When you build a Renderable, Sceneform loads its resources in the background while returning
// mc++     // a CompletableFuture. Call thenAccept(), handle(), or check isDone() before calling get().
// mc++     ModelRenderable.builder()
// mc++         .setSource(this, R.raw.andy)
// mc++         .build()
// mc++         .thenAccept(renderable -> andyRenderable = renderable)
// mc++         .exceptionally(
// mc++             throwable -> {
// mc++               Toast toast =
// mc++                   Toast.makeText(this, "Unable to load andy renderable", Toast.LENGTH_LONG);
// mc++               toast.setGravity(Gravity.CENTER, 0, 0);
// mc++               toast.show();
// mc++               return null;
// mc++             });
// mc++ 
// mc++     arFragment.setOnTapArPlaneListener(
// mc++         (HitResult hitResult, Plane plane, MotionEvent motionEvent) -> {
// mc++           if (andyRenderable == null) {
// mc++             return;
// mc++           }
// mc++ 
// mc++           // Create the Anchor.
// mc++           Anchor anchor = hitResult.createAnchor();
// mc++           AnchorNode anchorNode = new AnchorNode(anchor);
// mc++           anchorNode.setParent(arFragment.getArSceneView().getScene());
// mc++ 
// mc++           // Create the transformable andy and add it to the anchor.
// mc++           TransformableNode andy = new TransformableNode(arFragment.getTransformationSystem());
// mc++           andy.setParent(anchorNode);
// mc++           andy.setRenderable(andyRenderable);
// mc++           andy.select();
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
// mc++     if (Build.VERSION.SDK_INT < VERSION_CODES.N) {
// mc++       Log.e(TAG, "Sceneform requires Android N or later");
// mc++       Toast.makeText(activity, "Sceneform requires Android N or later", Toast.LENGTH_LONG).show();
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
