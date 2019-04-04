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
// mc++ package com.google.ar.sceneform.samples.chromakeyvideo;
// mc++ 
// mc++ import android.app.Activity;
// mc++ import android.app.ActivityManager;
// mc++ import android.content.Context;
// mc++ import android.graphics.SurfaceTexture;
// mc++ import android.media.MediaPlayer;
// mc++ import android.os.Build;
// mc++ import android.os.Build.VERSION_CODES;
// mc++ import android.os.Bundle;
// mc++ import android.support.annotation.Nullable;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.util.Log;
// mc++ import android.view.Gravity;
// mc++ import android.view.MotionEvent;
// mc++ import android.widget.Toast;
// mc++ import com.google.ar.core.Anchor;
// mc++ import com.google.ar.core.HitResult;
// mc++ import com.google.ar.core.Plane;
// mc++ import com.google.ar.sceneform.AnchorNode;
// mc++ import com.google.ar.sceneform.Node;
// mc++ import com.google.ar.sceneform.math.Vector3;
// mc++ import com.google.ar.sceneform.rendering.Color;
// mc++ import com.google.ar.sceneform.rendering.ExternalTexture;
// mc++ import com.google.ar.sceneform.rendering.ModelRenderable;
// mc++ import com.google.ar.sceneform.ux.ArFragment;
// mc++ 
// mc++ /**
// mc++  * This is an example activity that shows how to display a video with chroma key filtering in
// mc++  * Sceneform.
// mc++  */
// mc++ public class ChromaKeyVideoActivity extends AppCompatActivity {
// mc++   private static final String TAG = ChromaKeyVideoActivity.class.getSimpleName();
// mc++   private static final double MIN_OPENGL_VERSION = 3.0;
// mc++ 
// mc++   private ArFragment arFragment;
// mc++ 
// mc++   @Nullable private ModelRenderable videoRenderable;
// mc++   private MediaPlayer mediaPlayer;
// mc++ 
// mc++   // The color to filter out of the video.
// mc++   private static final Color CHROMA_KEY_COLOR = new Color(0.1843f, 1.0f, 0.098f);
// mc++ 
// mc++   // Controls the height of the video in world space.
// mc++   private static final float VIDEO_HEIGHT_METERS = 0.85f;
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
// mc++     setContentView(R.layout.activity_video);
// mc++     arFragment = (ArFragment) getSupportFragmentManager().findFragmentById(R.id.ux_fragment);
// mc++ 
// mc++     // Create an ExternalTexture for displaying the contents of the video.
// mc++     ExternalTexture texture = new ExternalTexture();
// mc++ 
// mc++     // Create an Android MediaPlayer to capture the video on the external texture's surface.
// mc++     mediaPlayer = MediaPlayer.create(this, R.raw.lion_chroma);
// mc++     mediaPlayer.setSurface(texture.getSurface());
// mc++     mediaPlayer.setLooping(true);
// mc++ 
// mc++     // Create a renderable with a material that has a parameter of type 'samplerExternal' so that
// mc++     // it can display an ExternalTexture. The material also has an implementation of a chroma key
// mc++     // filter.
// mc++     ModelRenderable.builder()
// mc++         .setSource(this, R.raw.chroma_key_video)
// mc++         .build()
// mc++         .thenAccept(
// mc++             renderable -> {
// mc++               videoRenderable = renderable;
// mc++               renderable.getMaterial().setExternalTexture("videoTexture", texture);
// mc++               renderable.getMaterial().setFloat4("keyColor", CHROMA_KEY_COLOR);
// mc++             })
// mc++         .exceptionally(
// mc++             throwable -> {
// mc++               Toast toast =
// mc++                   Toast.makeText(this, "Unable to load video renderable", Toast.LENGTH_LONG);
// mc++               toast.setGravity(Gravity.CENTER, 0, 0);
// mc++               toast.show();
// mc++               return null;
// mc++             });
// mc++ 
// mc++     arFragment.setOnTapArPlaneListener(
// mc++         (HitResult hitResult, Plane plane, MotionEvent motionEvent) -> {
// mc++           if (videoRenderable == null) {
// mc++             return;
// mc++           }
// mc++ 
// mc++           // Create the Anchor.
// mc++           Anchor anchor = hitResult.createAnchor();
// mc++           AnchorNode anchorNode = new AnchorNode(anchor);
// mc++           anchorNode.setParent(arFragment.getArSceneView().getScene());
// mc++ 
// mc++           // Create a node to render the video and add it to the anchor.
// mc++           Node videoNode = new Node();
// mc++           videoNode.setParent(anchorNode);
// mc++ 
// mc++           // Set the scale of the node so that the aspect ratio of the video is correct.
// mc++           float videoWidth = mediaPlayer.getVideoWidth();
// mc++           float videoHeight = mediaPlayer.getVideoHeight();
// mc++           videoNode.setLocalScale(
// mc++               new Vector3(
// mc++                   VIDEO_HEIGHT_METERS * (videoWidth / videoHeight), VIDEO_HEIGHT_METERS, 1.0f));
// mc++ 
// mc++           // Start playing the video when the first node is placed.
// mc++           if (!mediaPlayer.isPlaying()) {
// mc++             mediaPlayer.start();
// mc++ 
// mc++             // Wait to set the renderable until the first frame of the  video becomes available.
// mc++             // This prevents the renderable from briefly appearing as a black quad before the video
// mc++             // plays.
// mc++             texture
// mc++                 .getSurfaceTexture()
// mc++                 .setOnFrameAvailableListener(
// mc++                     (SurfaceTexture surfaceTexture) -> {
// mc++                       videoNode.setRenderable(videoRenderable);
// mc++                       texture.getSurfaceTexture().setOnFrameAvailableListener(null);
// mc++                     });
// mc++           } else {
// mc++             videoNode.setRenderable(videoRenderable);
// mc++           }
// mc++         });
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onDestroy() {
// mc++     super.onDestroy();
// mc++ 
// mc++     if (mediaPlayer != null) {
// mc++       mediaPlayer.release();
// mc++       mediaPlayer = null;
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
// mc++ }
