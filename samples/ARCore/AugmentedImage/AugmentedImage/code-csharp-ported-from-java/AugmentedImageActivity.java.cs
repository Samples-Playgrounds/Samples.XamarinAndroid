// mc++ /*
// mc++  * Copyright 2018 Google Inc. All Rights Reserved.
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
// mc++ package com.google.ar.core.examples.java.augmentedimage;
// mc++ 
// mc++ import android.graphics.Bitmap;
// mc++ import android.graphics.BitmapFactory;
// mc++ import android.net.Uri;
// mc++ import android.opengl.GLES20;
// mc++ import android.opengl.GLSurfaceView;
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.util.Log;
// mc++ import android.util.Pair;
// mc++ import android.view.View;
// mc++ import android.widget.ImageView;
// mc++ import android.widget.Toast;
// mc++ import com.bumptech.glide.Glide;
// mc++ import com.bumptech.glide.RequestManager;
// mc++ import com.google.ar.core.Anchor;
// mc++ import com.google.ar.core.ArCoreApk;
// mc++ import com.google.ar.core.AugmentedImage;
// mc++ import com.google.ar.core.AugmentedImageDatabase;
// mc++ import com.google.ar.core.Camera;
// mc++ import com.google.ar.core.Config;
// mc++ import com.google.ar.core.Frame;
// mc++ import com.google.ar.core.Session;
// mc++ import com.google.ar.core.TrackingState;
// mc++ import com.google.ar.core.examples.java.augmentedimage.rendering.AugmentedImageRenderer;
// mc++ import com.google.ar.core.examples.java.common.helpers.CameraPermissionHelper;
// mc++ import com.google.ar.core.examples.java.common.helpers.DisplayRotationHelper;
// mc++ import com.google.ar.core.examples.java.common.helpers.FullScreenHelper;
// mc++ import com.google.ar.core.examples.java.common.helpers.SnackbarHelper;
// mc++ import com.google.ar.core.examples.java.common.rendering.BackgroundRenderer;
// mc++ import com.google.ar.core.exceptions.CameraNotAvailableException;
// mc++ import com.google.ar.core.exceptions.UnavailableApkTooOldException;
// mc++ import com.google.ar.core.exceptions.UnavailableArcoreNotInstalledException;
// mc++ import com.google.ar.core.exceptions.UnavailableSdkTooOldException;
// mc++ import com.google.ar.core.exceptions.UnavailableUserDeclinedInstallationException;
// mc++ import java.io.IOException;
// mc++ import java.io.InputStream;
// mc++ import java.util.Collection;
// mc++ import java.util.HashMap;
// mc++ import java.util.Map;
// mc++ import javax.microedition.khronos.egl.EGLConfig;
// mc++ import javax.microedition.khronos.opengles.GL10;
// mc++ 
// mc++ /** This app extends the HelloAR Java app to include image tracking functionality. */
// mc++ public class AugmentedImageActivity extends AppCompatActivity implements GLSurfaceView.Renderer {
// mc++   private static final String TAG = AugmentedImageActivity.class.getSimpleName();
// mc++ 
// mc++   // Rendering. The Renderers are created here, and initialized when the GL surface is created.
// mc++   private GLSurfaceView surfaceView;
// mc++   private ImageView fitToScanView;
// mc++   private RequestManager glideRequestManager;
// mc++ 
// mc++   private boolean installRequested;
// mc++ 
// mc++   private Session session;
// mc++   private final SnackbarHelper messageSnackbarHelper = new SnackbarHelper();
// mc++   private DisplayRotationHelper displayRotationHelper;
// mc++ 
// mc++   private final BackgroundRenderer backgroundRenderer = new BackgroundRenderer();
// mc++   private final AugmentedImageRenderer augmentedImageRenderer = new AugmentedImageRenderer();
// mc++ 
// mc++   private boolean shouldConfigureSession = false;
// mc++ 
// mc++   // Augmented image configuration and rendering.
// mc++   // Load a single image (true) or a pre-generated image database (false).
// mc++   private final boolean useSingleImage = false;
// mc++   // Augmented image and its associated center pose anchor, keyed by index of the augmented image in
// mc++   // the
// mc++   // database.
// mc++   private final Map<Integer, Pair<AugmentedImage, Anchor>> augmentedImageMap = new HashMap<>();
// mc++ 
// mc++   @Override
// mc++   protected void onCreate(Bundle savedInstanceState) {
// mc++     super.onCreate(savedInstanceState);
// mc++     setContentView(R.layout.activity_main);
// mc++     surfaceView = findViewById(R.id.surfaceview);
// mc++     displayRotationHelper = new DisplayRotationHelper(/*context=*/ this);
// mc++ 
// mc++     // Set up renderer.
// mc++     surfaceView.setPreserveEGLContextOnPause(true);
// mc++     surfaceView.setEGLContextClientVersion(2);
// mc++     surfaceView.setEGLConfigChooser(8, 8, 8, 8, 16, 0); // Alpha used for plane blending.
// mc++     surfaceView.setRenderer(this);
// mc++     surfaceView.setRenderMode(GLSurfaceView.RENDERMODE_CONTINUOUSLY);
// mc++     surfaceView.setWillNotDraw(false);
// mc++ 
// mc++     fitToScanView = findViewById(R.id.image_view_fit_to_scan);
// mc++     glideRequestManager = Glide.with(this);
// mc++     glideRequestManager
// mc++         .load(Uri.parse("file:///android_asset/fit_to_scan.png"))
// mc++         .into(fitToScanView);
// mc++ 
// mc++     installRequested = false;
// mc++   }
// mc++ 
// mc++   @Override
// mc++   protected void onResume() {
// mc++     super.onResume();
// mc++ 
// mc++     if (session == null) {
// mc++       Exception exception = null;
// mc++       String message = null;
// mc++       try {
// mc++         switch (ArCoreApk.getInstance().requestInstall(this, !installRequested)) {
// mc++           case INSTALL_REQUESTED:
// mc++             installRequested = true;
// mc++             return;
// mc++           case INSTALLED:
// mc++             break;
// mc++         }
// mc++ 
// mc++         // ARCore requires camera permissions to operate. If we did not yet obtain runtime
// mc++         // permission on Android M and above, now is a good time to ask the user for it.
// mc++         if (!CameraPermissionHelper.hasCameraPermission(this)) {
// mc++           CameraPermissionHelper.requestCameraPermission(this);
// mc++           return;
// mc++         }
// mc++ 
// mc++         session = new Session(/* context = */ this);
// mc++       } catch (UnavailableArcoreNotInstalledException
// mc++           | UnavailableUserDeclinedInstallationException e) {
// mc++         message = "Please install ARCore";
// mc++         exception = e;
// mc++       } catch (UnavailableApkTooOldException e) {
// mc++         message = "Please update ARCore";
// mc++         exception = e;
// mc++       } catch (UnavailableSdkTooOldException e) {
// mc++         message = "Please update this app";
// mc++         exception = e;
// mc++       } catch (Exception e) {
// mc++         message = "This device does not support AR";
// mc++         exception = e;
// mc++       }
// mc++ 
// mc++       if (message != null) {
// mc++         messageSnackbarHelper.showError(this, message);
// mc++         Log.e(TAG, "Exception creating session", exception);
// mc++         return;
// mc++       }
// mc++ 
// mc++       shouldConfigureSession = true;
// mc++     }
// mc++ 
// mc++     if (shouldConfigureSession) {
// mc++       configureSession();
// mc++       shouldConfigureSession = false;
// mc++     }
// mc++ 
// mc++     // Note that order matters - see the note in onPause(), the reverse applies here.
// mc++     try {
// mc++       session.resume();
// mc++     } catch (CameraNotAvailableException e) {
// mc++       // In some cases (such as another camera app launching) the camera may be given to
// mc++       // a different app instead. Handle this properly by showing a message and recreate the
// mc++       // session at the next iteration.
// mc++       messageSnackbarHelper.showError(this, "Camera not available. Please restart the app.");
// mc++       session = null;
// mc++       return;
// mc++     }
// mc++     surfaceView.onResume();
// mc++     displayRotationHelper.onResume();
// mc++ 
// mc++     fitToScanView.setVisibility(View.VISIBLE);
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onPause() {
// mc++     super.onPause();
// mc++     if (session != null) {
// mc++       // Note that the order matters - GLSurfaceView is paused first so that it does not try
// mc++       // to query the session. If Session is paused before GLSurfaceView, GLSurfaceView may
// mc++       // still call session.update() and get a SessionPausedException.
// mc++       displayRotationHelper.onPause();
// mc++       surfaceView.onPause();
// mc++       session.pause();
// mc++     }
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onRequestPermissionsResult(int requestCode, String[] permissions, int[] results) {
// mc++     if (!CameraPermissionHelper.hasCameraPermission(this)) {
// mc++       Toast.makeText(
// mc++               this, "Camera permissions are needed to run this application", Toast.LENGTH_LONG)
// mc++           .show();
// mc++       if (!CameraPermissionHelper.shouldShowRequestPermissionRationale(this)) {
// mc++         // Permission denied with checking "Do not ask again".
// mc++         CameraPermissionHelper.launchPermissionSettings(this);
// mc++       }
// mc++       finish();
// mc++     }
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onWindowFocusChanged(boolean hasFocus) {
// mc++     super.onWindowFocusChanged(hasFocus);
// mc++     FullScreenHelper.setFullScreenOnWindowFocusChanged(this, hasFocus);
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onSurfaceCreated(GL10 gl, EGLConfig config) {
// mc++     GLES20.glClearColor(0.1f, 0.1f, 0.1f, 1.0f);
// mc++ 
// mc++     // Prepare the rendering objects. This involves reading shaders, so may throw an IOException.
// mc++     try {
// mc++       // Create the texture and pass it to ARCore session to be filled during update().
// mc++       backgroundRenderer.createOnGlThread(/*context=*/ this);
// mc++       augmentedImageRenderer.createOnGlThread(/*context=*/ this);
// mc++     } catch (IOException e) {
// mc++       Log.e(TAG, "Failed to read an asset file", e);
// mc++     }
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onSurfaceChanged(GL10 gl, int width, int height) {
// mc++     displayRotationHelper.onSurfaceChanged(width, height);
// mc++     GLES20.glViewport(0, 0, width, height);
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onDrawFrame(GL10 gl) {
// mc++     // Clear screen to notify driver it should not load any pixels from previous frame.
// mc++     GLES20.glClear(GLES20.GL_COLOR_BUFFER_BIT | GLES20.GL_DEPTH_BUFFER_BIT);
// mc++ 
// mc++     if (session == null) {
// mc++       return;
// mc++     }
// mc++     // Notify ARCore session that the view size changed so that the perspective matrix and
// mc++     // the video background can be properly adjusted.
// mc++     displayRotationHelper.updateSessionIfNeeded(session);
// mc++ 
// mc++     try {
// mc++       session.setCameraTextureName(backgroundRenderer.getTextureId());
// mc++ 
// mc++       // Obtain the current frame from ARSession. When the configuration is set to
// mc++       // UpdateMode.BLOCKING (it is by default), this will throttle the rendering to the
// mc++       // camera framerate.
// mc++       Frame frame = session.update();
// mc++       Camera camera = frame.getCamera();
// mc++ 
// mc++       // If frame is ready, render camera preview image to the GL surface.
// mc++       backgroundRenderer.draw(frame);
// mc++ 
// mc++       // If not tracking, don't draw 3d objects.
// mc++       if (camera.getTrackingState() == TrackingState.PAUSED) {
// mc++         return;
// mc++       }
// mc++ 
// mc++       // Get projection matrix.
// mc++       float[] projmtx = new float[16];
// mc++       camera.getProjectionMatrix(projmtx, 0, 0.1f, 100.0f);
// mc++ 
// mc++       // Get camera matrix and draw.
// mc++       float[] viewmtx = new float[16];
// mc++       camera.getViewMatrix(viewmtx, 0);
// mc++ 
// mc++       // Compute lighting from average intensity of the image.
// mc++       final float[] colorCorrectionRgba = new float[4];
// mc++       frame.getLightEstimate().getColorCorrection(colorCorrectionRgba, 0);
// mc++ 
// mc++       // Visualize augmented images.
// mc++       drawAugmentedImages(frame, projmtx, viewmtx, colorCorrectionRgba);
// mc++     } catch (Throwable t) {
// mc++       // Avoid crashing the application due to unhandled exceptions.
// mc++       Log.e(TAG, "Exception on the OpenGL thread", t);
// mc++     }
// mc++   }
// mc++ 
// mc++   private void configureSession() {
// mc++     Config config = new Config(session);
// mc++     config.setFocusMode(Config.FocusMode.AUTO);
// mc++     if (!setupAugmentedImageDatabase(config)) {
// mc++       messageSnackbarHelper.showError(this, "Could not setup augmented image database");
// mc++     }
// mc++     session.configure(config);
// mc++   }
// mc++ 
// mc++   private void drawAugmentedImages(
// mc++       Frame frame, float[] projmtx, float[] viewmtx, float[] colorCorrectionRgba) {
// mc++     Collection<AugmentedImage> updatedAugmentedImages =
// mc++         frame.getUpdatedTrackables(AugmentedImage.class);
// mc++ 
// mc++     // Iterate to update augmentedImageMap, remove elements we cannot draw.
// mc++     for (AugmentedImage augmentedImage : updatedAugmentedImages) {
// mc++       switch (augmentedImage.getTrackingState()) {
// mc++         case PAUSED:
// mc++           // When an image is in PAUSED state, but the camera is not PAUSED, it has been detected,
// mc++           // but not yet tracked.
// mc++           String text = String.format("Detected Image %d", augmentedImage.getIndex());
// mc++           messageSnackbarHelper.showMessage(this, text);
// mc++           break;
// mc++ 
// mc++         case TRACKING:
// mc++           // Have to switch to UI Thread to update View.
// mc++           this.runOnUiThread(
// mc++               new Runnable() {
// mc++                 @Override
// mc++                 public void run() {
// mc++                   fitToScanView.setVisibility(View.GONE);
// mc++                 }
// mc++               });
// mc++ 
// mc++           // Create a new anchor for newly found images.
// mc++           if (!augmentedImageMap.containsKey(augmentedImage.getIndex())) {
// mc++             Anchor centerPoseAnchor = augmentedImage.createAnchor(augmentedImage.getCenterPose());
// mc++             augmentedImageMap.put(
// mc++                 augmentedImage.getIndex(), Pair.create(augmentedImage, centerPoseAnchor));
// mc++           }
// mc++           break;
// mc++ 
// mc++         case STOPPED:
// mc++           augmentedImageMap.remove(augmentedImage.getIndex());
// mc++           break;
// mc++ 
// mc++         default:
// mc++           break;
// mc++       }
// mc++     }
// mc++ 
// mc++     // Draw all images in augmentedImageMap
// mc++     for (Pair<AugmentedImage, Anchor> pair : augmentedImageMap.values()) {
// mc++       AugmentedImage augmentedImage = pair.first;
// mc++       Anchor centerAnchor = augmentedImageMap.get(augmentedImage.getIndex()).second;
// mc++       switch (augmentedImage.getTrackingState()) {
// mc++         case TRACKING:
// mc++           augmentedImageRenderer.draw(
// mc++               viewmtx, projmtx, augmentedImage, centerAnchor, colorCorrectionRgba);
// mc++           break;
// mc++         default:
// mc++           break;
// mc++       }
// mc++     }
// mc++   }
// mc++ 
// mc++   private boolean setupAugmentedImageDatabase(Config config) {
// mc++     AugmentedImageDatabase augmentedImageDatabase;
// mc++ 
// mc++     // There are two ways to configure an AugmentedImageDatabase:
// mc++     // 1. Add Bitmap to DB directly
// mc++     // 2. Load a pre-built AugmentedImageDatabase
// mc++     // Option 2) has
// mc++     // * shorter setup time
// mc++     // * doesn't require images to be packaged in apk.
// mc++     if (useSingleImage) {
// mc++       Bitmap augmentedImageBitmap = loadAugmentedImageBitmap();
// mc++       if (augmentedImageBitmap == null) {
// mc++         return false;
// mc++       }
// mc++ 
// mc++       augmentedImageDatabase = new AugmentedImageDatabase(session);
// mc++       augmentedImageDatabase.addImage("image_name", augmentedImageBitmap);
// mc++       // If the physical size of the image is known, you can instead use:
// mc++       //     augmentedImageDatabase.addImage("image_name", augmentedImageBitmap, widthInMeters);
// mc++       // This will improve the initial detection speed. ARCore will still actively estimate the
// mc++       // physical size of the image as it is viewed from multiple viewpoints.
// mc++     } else {
// mc++       // This is an alternative way to initialize an AugmentedImageDatabase instance,
// mc++       // load a pre-existing augmented image database.
// mc++       try (InputStream is = getAssets().open("sample_database.imgdb")) {
// mc++         augmentedImageDatabase = AugmentedImageDatabase.deserialize(session, is);
// mc++       } catch (IOException e) {
// mc++         Log.e(TAG, "IO exception loading augmented image database.", e);
// mc++         return false;
// mc++       }
// mc++     }
// mc++ 
// mc++     config.setAugmentedImageDatabase(augmentedImageDatabase);
// mc++     return true;
// mc++   }
// mc++ 
// mc++   private Bitmap loadAugmentedImageBitmap() {
// mc++     try (InputStream is = getAssets().open("default.jpg")) {
// mc++       return BitmapFactory.decodeStream(is);
// mc++     } catch (IOException e) {
// mc++       Log.e(TAG, "IO exception loading augmented image bitmap.", e);
// mc++     }
// mc++     return null;
// mc++   }
// mc++ }
