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
// mc++ package com.google.ar.sceneform.samples.videorecording;
// mc++ 
// mc++ import android.app.Activity;
// mc++ import android.app.ActivityManager;
// mc++ import android.content.ContentValues;
// mc++ import android.content.Context;
// mc++ import android.media.CamcorderProfile;
// mc++ import android.os.Build;
// mc++ import android.os.Build.VERSION_CODES;
// mc++ import android.os.Bundle;
// mc++ import android.provider.MediaStore;
// mc++ import android.support.design.widget.FloatingActionButton;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.util.Log;
// mc++ import android.view.Gravity;
// mc++ import android.view.MotionEvent;
// mc++ import android.view.View;
// mc++ import android.widget.Toast;
// mc++ import com.google.ar.core.Anchor;
// mc++ import com.google.ar.core.HitResult;
// mc++ import com.google.ar.core.Plane;
// mc++ import com.google.ar.sceneform.AnchorNode;
// mc++ import com.google.ar.sceneform.rendering.ModelRenderable;
// mc++ import com.google.ar.sceneform.ux.TransformableNode;
// mc++ 
// mc++ /**
// mc++  * This is an example activity that uses the Sceneform UX package to make common AR tasks easier.
// mc++  */
// mc++ public class VideoRecordingActivity extends AppCompatActivity
// mc++     implements ModelLoader.ModelLoaderCallbacks {
// mc++   private static final String TAG = VideoRecordingActivity.class.getSimpleName();
// mc++   private static final double MIN_OPENGL_VERSION = 3.0;
// mc++ 
// mc++   private WritingArFragment arFragment;
// mc++   private ModelRenderable andyRenderable;
// mc++   // Model loader class to avoid leaking the activity context.
// mc++   private ModelLoader modelLoader;
// mc++ 
// mc++   // VideoRecorder encapsulates all the video recording functionality.
// mc++   private VideoRecorder videoRecorder;
// mc++ 
// mc++   // The UI to record.
// mc++   private FloatingActionButton recordButton;
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
// mc++     arFragment = (WritingArFragment) getSupportFragmentManager().findFragmentById(R.id.ux_fragment);
// mc++ 
// mc++     modelLoader = new ModelLoader(this);
// mc++     modelLoader.loadModel(this, R.raw.andy);
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
// mc++ 
// mc++     // Initialize the VideoRecorder.
// mc++     videoRecorder = new VideoRecorder();
// mc++     int orientation = getResources().getConfiguration().orientation;
// mc++     videoRecorder.setVideoQuality(CamcorderProfile.QUALITY_2160P, orientation);
// mc++     videoRecorder.setSceneView(arFragment.getArSceneView());
// mc++ 
// mc++     recordButton = findViewById(R.id.record);
// mc++     recordButton.setOnClickListener(this::toggleRecording);
// mc++     recordButton.setEnabled(true);
// mc++     recordButton.setImageResource(R.drawable.round_videocam);
// mc++   }
// mc++ 
// mc++   @Override
// mc++   protected void onPause() {
// mc++     if (videoRecorder.isRecording()) {
// mc++       toggleRecording(null);
// mc++     }
// mc++     super.onPause();
// mc++   }
// mc++ 
// mc++   /*
// mc++    * Used as a handler for onClick, so the signature must match onClickListener.
// mc++    */
// mc++   private void toggleRecording(View unusedView) {
// mc++     if (!arFragment.hasWritePermission()) {
// mc++       Log.e(TAG, "Video recording requires the WRITE_EXTERNAL_STORAGE permission");
// mc++       Toast.makeText(
// mc++               this,
// mc++               "Video recording requires the WRITE_EXTERNAL_STORAGE permission",
// mc++               Toast.LENGTH_LONG)
// mc++           .show();
// mc++       arFragment.launchPermissionSettings();
// mc++       return;
// mc++     }
// mc++     boolean recording = videoRecorder.onToggleRecord();
// mc++     if (recording) {
// mc++       recordButton.setImageResource(R.drawable.round_stop);
// mc++     } else {
// mc++       recordButton.setImageResource(R.drawable.round_videocam);
// mc++       String videoPath = videoRecorder.getVideoPath().getAbsolutePath();
// mc++       Toast.makeText(this, "Video saved: " + videoPath, Toast.LENGTH_SHORT).show();
// mc++       Log.d(TAG, "Video saved: " + videoPath);
// mc++ 
// mc++       // Send  notification of updated content.
// mc++       ContentValues values = new ContentValues();
// mc++       values.put(MediaStore.Video.Media.TITLE, "Sceneform Video");
// mc++       values.put(MediaStore.Video.Media.MIME_TYPE, "video/mp4");
// mc++       values.put(MediaStore.Video.Media.DATA, videoPath);
// mc++       getContentResolver().insert(MediaStore.Video.Media.EXTERNAL_CONTENT_URI, values);
// mc++     }
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
// mc++ 
// mc++   @Override
// mc++   public void setRenderable(ModelRenderable modelRenderable) {
// mc++     andyRenderable = modelRenderable;
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onLoadException(Throwable throwable) {
// mc++     Toast toast = Toast.makeText(this, "Unable to load andy renderable", Toast.LENGTH_LONG);
// mc++     toast.setGravity(Gravity.CENTER, 0, 0);
// mc++     toast.show();
// mc++     Log.e(TAG, "Unable to load andy renderable", throwable);
// mc++   }
// mc++ }
