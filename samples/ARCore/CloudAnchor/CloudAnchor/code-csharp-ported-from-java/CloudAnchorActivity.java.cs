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
// mc++ package com.google.ar.core.examples.java.cloudanchor;
// mc++ 
// mc++ import android.opengl.GLES20;
// mc++ import android.opengl.GLSurfaceView;
// mc++ import android.os.Bundle;
// mc++ import android.support.annotation.GuardedBy;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.util.Log;
// mc++ import android.view.GestureDetector;
// mc++ import android.view.MotionEvent;
// mc++ import android.widget.Button;
// mc++ import android.widget.TextView;
// mc++ import android.widget.Toast;
// mc++ import com.google.ar.core.Anchor;
// mc++ import com.google.ar.core.Anchor.CloudAnchorState;
// mc++ import com.google.ar.core.ArCoreApk;
// mc++ import com.google.ar.core.Camera;
// mc++ import com.google.ar.core.Config;
// mc++ import com.google.ar.core.Config.CloudAnchorMode;
// mc++ import com.google.ar.core.Frame;
// mc++ import com.google.ar.core.HitResult;
// mc++ import com.google.ar.core.Plane;
// mc++ import com.google.ar.core.Point;
// mc++ import com.google.ar.core.Point.OrientationMode;
// mc++ import com.google.ar.core.PointCloud;
// mc++ import com.google.ar.core.Session;
// mc++ import com.google.ar.core.Trackable;
// mc++ import com.google.ar.core.TrackingState;
// mc++ import com.google.ar.core.examples.java.common.helpers.CameraPermissionHelper;
// mc++ import com.google.ar.core.examples.java.common.helpers.DisplayRotationHelper;
// mc++ import com.google.ar.core.examples.java.common.helpers.FullScreenHelper;
// mc++ import com.google.ar.core.examples.java.common.helpers.SnackbarHelper;
// mc++ import com.google.ar.core.examples.java.common.rendering.BackgroundRenderer;
// mc++ import com.google.ar.core.examples.java.common.rendering.ObjectRenderer;
// mc++ import com.google.ar.core.examples.java.common.rendering.ObjectRenderer.BlendMode;
// mc++ import com.google.ar.core.examples.java.common.rendering.PlaneRenderer;
// mc++ import com.google.ar.core.examples.java.common.rendering.PointCloudRenderer;
// mc++ import com.google.ar.core.exceptions.CameraNotAvailableException;
// mc++ import com.google.ar.core.exceptions.UnavailableApkTooOldException;
// mc++ import com.google.ar.core.exceptions.UnavailableArcoreNotInstalledException;
// mc++ import com.google.ar.core.exceptions.UnavailableSdkTooOldException;
// mc++ import com.google.common.base.Preconditions;
// mc++ import com.google.firebase.database.DatabaseError;
// mc++ import java.io.IOException;
// mc++ import javax.microedition.khronos.egl.EGLConfig;
// mc++ import javax.microedition.khronos.opengles.GL10;
// mc++ 
// mc++ /**
// mc++  * Main Activity for the Cloud Anchor Example
// mc++  *
// mc++  * <p>This is a simple example that shows how to host and resolve anchors using ARCore Cloud Anchors
// mc++  * API calls. This app only has at most one anchor at a time, to focus more on the cloud aspect of
// mc++  * anchors.
// mc++  */
// mc++ public class CloudAnchorActivity extends AppCompatActivity implements GLSurfaceView.Renderer {
// mc++   private static final String TAG = CloudAnchorActivity.class.getSimpleName();
// mc++   private static final float[] OBJECT_COLOR = new float[] {139.0f, 195.0f, 74.0f, 255.0f};
// mc++ 
// mc++   private enum HostResolveMode {
// mc++     NONE,
// mc++     HOSTING,
// mc++     RESOLVING,
// mc++   }
// mc++ 
// mc++   // Rendering. The Renderers are created here, and initialized when the GL surface is created.
// mc++   private GLSurfaceView surfaceView;
// mc++   private final BackgroundRenderer backgroundRenderer = new BackgroundRenderer();
// mc++   private final ObjectRenderer virtualObject = new ObjectRenderer();
// mc++   private final ObjectRenderer virtualObjectShadow = new ObjectRenderer();
// mc++   private final PlaneRenderer planeRenderer = new PlaneRenderer();
// mc++   private final PointCloudRenderer pointCloudRenderer = new PointCloudRenderer();
// mc++ 
// mc++   private boolean installRequested;
// mc++ 
// mc++   // Temporary matrices allocated here to reduce number of allocations for each frame.
// mc++   private final float[] anchorMatrix = new float[16];
// mc++   private final float[] viewMatrix = new float[16];
// mc++   private final float[] projectionMatrix = new float[16];
// mc++ 
// mc++   // Locks needed for synchronization
// mc++   private final Object singleTapLock = new Object();
// mc++   private final Object anchorLock = new Object();
// mc++ 
// mc++   // Tap handling and UI.
// mc++   private GestureDetector gestureDetector;
// mc++   private final SnackbarHelper snackbarHelper = new SnackbarHelper();
// mc++   private DisplayRotationHelper displayRotationHelper;
// mc++   private Button hostButton;
// mc++   private Button resolveButton;
// mc++   private TextView roomCodeText;
// mc++ 
// mc++   @GuardedBy("singleTapLock")
// mc++   private MotionEvent queuedSingleTap;
// mc++ 
// mc++   private Session session;
// mc++ 
// mc++   @GuardedBy("anchorLock")
// mc++   private Anchor anchor;
// mc++ 
// mc++   // Cloud Anchor Components.
// mc++   private FirebaseManager firebaseManager;
// mc++   private final CloudAnchorManager cloudManager = new CloudAnchorManager();
// mc++   private HostResolveMode currentMode;
// mc++   private RoomCodeAndCloudAnchorIdListener hostListener;
// mc++ 
// mc++   @Override
// mc++   protected void onCreate(Bundle savedInstanceState) {
// mc++     super.onCreate(savedInstanceState);
// mc++     setContentView(R.layout.activity_main);
// mc++     surfaceView = findViewById(R.id.surfaceview);
// mc++     displayRotationHelper = new DisplayRotationHelper(this);
// mc++ 
// mc++     // Set up tap listener.
// mc++     gestureDetector =
// mc++         new GestureDetector(
// mc++             this,
// mc++             new GestureDetector.SimpleOnGestureListener() {
// mc++               @Override
// mc++               public boolean onSingleTapUp(MotionEvent e) {
// mc++                 synchronized (singleTapLock) {
// mc++                   if (currentMode == HostResolveMode.HOSTING) {
// mc++                     queuedSingleTap = e;
// mc++                   }
// mc++                 }
// mc++                 return true;
// mc++               }
// mc++ 
// mc++               @Override
// mc++               public boolean onDown(MotionEvent e) {
// mc++                 return true;
// mc++               }
// mc++             });
// mc++     surfaceView.setOnTouchListener((v, event) -> gestureDetector.onTouchEvent(event));
// mc++ 
// mc++     // Set up renderer.
// mc++     surfaceView.setPreserveEGLContextOnPause(true);
// mc++     surfaceView.setEGLContextClientVersion(2);
// mc++     surfaceView.setEGLConfigChooser(8, 8, 8, 8, 16, 0); // Alpha used for plane blending.
// mc++     surfaceView.setRenderer(this);
// mc++     surfaceView.setRenderMode(GLSurfaceView.RENDERMODE_CONTINUOUSLY);
// mc++     surfaceView.setWillNotDraw(false);
// mc++     installRequested = false;
// mc++ 
// mc++     // Initialize UI components.
// mc++     hostButton = findViewById(R.id.host_button);
// mc++     hostButton.setOnClickListener((view) -> onHostButtonPress());
// mc++     resolveButton = findViewById(R.id.resolve_button);
// mc++     resolveButton.setOnClickListener((view) -> onResolveButtonPress());
// mc++     roomCodeText = findViewById(R.id.room_code_text);
// mc++ 
// mc++     // Initialize Cloud Anchor variables.
// mc++     firebaseManager = new FirebaseManager(this);
// mc++     currentMode = HostResolveMode.NONE;
// mc++   }
// mc++ 
// mc++   @Override
// mc++   protected void onResume() {
// mc++     super.onResume();
// mc++ 
// mc++     if (session == null) {
// mc++       Exception exception = null;
// mc++       int messageId = -1;
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
// mc++         session = new Session(this);
// mc++       } catch (UnavailableArcoreNotInstalledException e) {
// mc++         messageId = R.string.snackbar_arcore_unavailable;
// mc++         exception = e;
// mc++       } catch (UnavailableApkTooOldException e) {
// mc++         messageId = R.string.snackbar_arcore_too_old;
// mc++         exception = e;
// mc++       } catch (UnavailableSdkTooOldException e) {
// mc++         messageId = R.string.snackbar_arcore_sdk_too_old;
// mc++         exception = e;
// mc++       } catch (Exception e) {
// mc++         messageId = R.string.snackbar_arcore_exception;
// mc++         exception = e;
// mc++       }
// mc++ 
// mc++       if (exception != null) {
// mc++         snackbarHelper.showError(this, getString(messageId));
// mc++         Log.e(TAG, "Exception creating session", exception);
// mc++         return;
// mc++       }
// mc++ 
// mc++       // Create default config and check if supported.
// mc++       Config config = new Config(session);
// mc++       config.setCloudAnchorMode(CloudAnchorMode.ENABLED);
// mc++       session.configure(config);
// mc++ 
// mc++       // Setting the session in the HostManager.
// mc++       cloudManager.setSession(session);
// mc++       // Show the inital message only in the first resume.
// mc++       snackbarHelper.showMessage(this, getString(R.string.snackbar_initial_message));
// mc++     }
// mc++ 
// mc++     // Note that order matters - see the note in onPause(), the reverse applies here.
// mc++     try {
// mc++       session.resume();
// mc++     } catch (CameraNotAvailableException e) {
// mc++       // In some cases (such as another camera app launching) the camera may be given to
// mc++       // a different app instead. Handle this properly by showing a message and recreate the
// mc++       // session at the next iteration.
// mc++       snackbarHelper.showError(this, getString(R.string.snackbar_camera_unavailable));
// mc++       session = null;
// mc++       return;
// mc++     }
// mc++     surfaceView.onResume();
// mc++     displayRotationHelper.onResume();
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
// mc++       Toast.makeText(this, "Camera permission is needed to run this application", Toast.LENGTH_LONG)
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
// mc++   /**
// mc++    * Handles the most recent user tap.
// mc++    *
// mc++    * <p>We only ever handle one tap at a time, since this app only allows for a single anchor.
// mc++    *
// mc++    * @param frame the current AR frame
// mc++    * @param cameraTrackingState the current camera tracking state
// mc++    */
// mc++   private void handleTap(Frame frame, TrackingState cameraTrackingState) {
// mc++     // Handle taps. Handling only one tap per frame, as taps are usually low frequency
// mc++     // compared to frame rate.
// mc++     synchronized (singleTapLock) {
// mc++       synchronized (anchorLock) {
// mc++         // Only handle a tap if the anchor is currently null, the queued tap is non-null and the
// mc++         // camera is currently tracking.
// mc++         if (anchor == null
// mc++             && queuedSingleTap != null
// mc++             && cameraTrackingState == TrackingState.TRACKING) {
// mc++           Preconditions.checkState(
// mc++               currentMode == HostResolveMode.HOSTING,
// mc++               "We should only be creating an anchor in hosting mode.");
// mc++           for (HitResult hit : frame.hitTest(queuedSingleTap)) {
// mc++             if (shouldCreateAnchorWithHit(hit)) {
// mc++               Anchor newAnchor = hit.createAnchor();
// mc++               Preconditions.checkNotNull(hostListener, "The host listener cannot be null.");
// mc++               cloudManager.hostCloudAnchor(newAnchor, hostListener);
// mc++               setNewAnchor(newAnchor);
// mc++               snackbarHelper.showMessage(this, getString(R.string.snackbar_anchor_placed));
// mc++               break; // Only handle the first valid hit.
// mc++             }
// mc++           }
// mc++         }
// mc++       }
// mc++       queuedSingleTap = null;
// mc++     }
// mc++   }
// mc++ 
// mc++   /** Returns {@code true} if and only if the hit can be used to create an Anchor reliably. */
// mc++   private static boolean shouldCreateAnchorWithHit(HitResult hit) {
// mc++     Trackable trackable = hit.getTrackable();
// mc++     if (trackable instanceof Plane) {
// mc++       // Check if the hit was within the plane's polygon.
// mc++       return ((Plane) trackable).isPoseInPolygon(hit.getHitPose());
// mc++     } else if (trackable instanceof Point) {
// mc++       // Check if the hit was against an oriented point.
// mc++       return ((Point) trackable).getOrientationMode() == OrientationMode.ESTIMATED_SURFACE_NORMAL;
// mc++     }
// mc++     return false;
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onSurfaceCreated(GL10 gl, EGLConfig config) {
// mc++     GLES20.glClearColor(0.1f, 0.1f, 0.1f, 1.0f);
// mc++ 
// mc++     // Prepare the rendering objects. This involves reading shaders, so may throw an IOException.
// mc++     try {
// mc++       // Create the texture and pass it to ARCore session to be filled during update().
// mc++       backgroundRenderer.createOnGlThread(this);
// mc++       planeRenderer.createOnGlThread(this, "models/trigrid.png");
// mc++       pointCloudRenderer.createOnGlThread(this);
// mc++ 
// mc++       virtualObject.createOnGlThread(this, "models/andy.obj", "models/andy.png");
// mc++       virtualObject.setMaterialProperties(0.0f, 2.0f, 0.5f, 6.0f);
// mc++ 
// mc++       virtualObjectShadow.createOnGlThread(
// mc++           this, "models/andy_shadow.obj", "models/andy_shadow.png");
// mc++       virtualObjectShadow.setBlendMode(BlendMode.Shadow);
// mc++       virtualObjectShadow.setMaterialProperties(1.0f, 0.0f, 0.0f, 1.0f);
// mc++     } catch (IOException ex) {
// mc++       Log.e(TAG, "Failed to read an asset file", ex);
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
// mc++       TrackingState cameraTrackingState = camera.getTrackingState();
// mc++ 
// mc++       // Notify the cloudManager of all the updates.
// mc++       cloudManager.onUpdate();
// mc++ 
// mc++       // Handle user input.
// mc++       handleTap(frame, cameraTrackingState);
// mc++ 
// mc++       // If frame is ready, render camera preview image to the GL surface.
// mc++       backgroundRenderer.draw(frame);
// mc++ 
// mc++       // If not tracking, don't draw 3d objects.
// mc++       if (cameraTrackingState == TrackingState.PAUSED) {
// mc++         return;
// mc++       }
// mc++ 
// mc++       // Get camera and projection matrices.
// mc++       camera.getViewMatrix(viewMatrix, 0);
// mc++       camera.getProjectionMatrix(projectionMatrix, 0, 0.1f, 100.0f);
// mc++ 
// mc++       // Visualize tracked points.
// mc++       // Use try-with-resources to automatically release the point cloud.
// mc++       try (PointCloud pointCloud = frame.acquirePointCloud()) {
// mc++         pointCloudRenderer.update(pointCloud);
// mc++         pointCloudRenderer.draw(viewMatrix, projectionMatrix);
// mc++       }
// mc++ 
// mc++       // Visualize planes.
// mc++       planeRenderer.drawPlanes(
// mc++           session.getAllTrackables(Plane.class), camera.getDisplayOrientedPose(), projectionMatrix);
// mc++ 
// mc++       // Check if the anchor can be visualized or not, and get its pose if it can be.
// mc++       boolean shouldDrawAnchor = false;
// mc++       synchronized (anchorLock) {
// mc++         if (anchor != null && anchor.getTrackingState() == TrackingState.TRACKING) {
// mc++           // Get the current pose of an Anchor in world space. The Anchor pose is updated
// mc++           // during calls to session.update() as ARCore refines its estimate of the world.
// mc++           anchor.getPose().toMatrix(anchorMatrix, 0);
// mc++           shouldDrawAnchor = true;
// mc++         }
// mc++       }
// mc++ 
// mc++       // Visualize anchor.
// mc++       if (shouldDrawAnchor) {
// mc++         float[] colorCorrectionRgba = new float[4];
// mc++         frame.getLightEstimate().getColorCorrection(colorCorrectionRgba, 0);
// mc++ 
// mc++         // Update and draw the model and its shadow.
// mc++         float scaleFactor = 1.0f;
// mc++         virtualObject.updateModelMatrix(anchorMatrix, scaleFactor);
// mc++         virtualObjectShadow.updateModelMatrix(anchorMatrix, scaleFactor);
// mc++         virtualObject.draw(viewMatrix, projectionMatrix, colorCorrectionRgba, OBJECT_COLOR);
// mc++         virtualObjectShadow.draw(viewMatrix, projectionMatrix, colorCorrectionRgba, OBJECT_COLOR);
// mc++       }
// mc++     } catch (Throwable t) {
// mc++       // Avoid crashing the application due to unhandled exceptions.
// mc++       Log.e(TAG, "Exception on the OpenGL thread", t);
// mc++     }
// mc++   }
// mc++ 
// mc++   /** Sets the new value of the current anchor. Detaches the old anchor, if it was non-null. */
// mc++   private void setNewAnchor(Anchor newAnchor) {
// mc++     synchronized (anchorLock) {
// mc++       if (anchor != null) {
// mc++         anchor.detach();
// mc++       }
// mc++       anchor = newAnchor;
// mc++     }
// mc++   }
// mc++ 
// mc++   /** Callback function invoked when the Host Button is pressed. */
// mc++   private void onHostButtonPress() {
// mc++     if (currentMode == HostResolveMode.HOSTING) {
// mc++       resetMode();
// mc++       return;
// mc++     }
// mc++ 
// mc++     if (hostListener != null) {
// mc++       return;
// mc++     }
// mc++     resolveButton.setEnabled(false);
// mc++     hostButton.setText(R.string.cancel);
// mc++     snackbarHelper.showMessageWithDismiss(this, getString(R.string.snackbar_on_host));
// mc++ 
// mc++     hostListener = new RoomCodeAndCloudAnchorIdListener();
// mc++     firebaseManager.getNewRoomCode(hostListener);
// mc++   }
// mc++ 
// mc++   /** Callback function invoked when the Resolve Button is pressed. */
// mc++   private void onResolveButtonPress() {
// mc++     if (currentMode == HostResolveMode.RESOLVING) {
// mc++       resetMode();
// mc++       return;
// mc++     }
// mc++     ResolveDialogFragment dialogFragment = new ResolveDialogFragment();
// mc++     dialogFragment.setOkListener(this::onRoomCodeEntered);
// mc++     dialogFragment.show(getSupportFragmentManager(), "ResolveDialog");
// mc++   }
// mc++ 
// mc++   /** Resets the mode of the app to its initial state and removes the anchors. */
// mc++   private void resetMode() {
// mc++     hostButton.setText(R.string.host_button_text);
// mc++     hostButton.setEnabled(true);
// mc++     resolveButton.setText(R.string.resolve_button_text);
// mc++     resolveButton.setEnabled(true);
// mc++     roomCodeText.setText(R.string.initial_room_code);
// mc++     currentMode = HostResolveMode.NONE;
// mc++     firebaseManager.clearRoomListener();
// mc++     hostListener = null;
// mc++     setNewAnchor(null);
// mc++     snackbarHelper.hide(this);
// mc++     cloudManager.clearListeners();
// mc++   }
// mc++ 
// mc++   /** Callback function invoked when the user presses the OK button in the Resolve Dialog. */
// mc++   private void onRoomCodeEntered(Long roomCode) {
// mc++     currentMode = HostResolveMode.RESOLVING;
// mc++     hostButton.setEnabled(false);
// mc++     resolveButton.setText(R.string.cancel);
// mc++     roomCodeText.setText(String.valueOf(roomCode));
// mc++     snackbarHelper.showMessageWithDismiss(this, getString(R.string.snackbar_on_resolve));
// mc++ 
// mc++     // Register a new listener for the given room.
// mc++     firebaseManager.registerNewListenerForRoom(
// mc++         roomCode,
// mc++         (cloudAnchorId) -> {
// mc++           // When the cloud anchor ID is available from Firebase.
// mc++           cloudManager.resolveCloudAnchor(
// mc++               cloudAnchorId,
// mc++               (anchor) -> {
// mc++                 // When the anchor has been resolved, or had a final error state.
// mc++                 CloudAnchorState cloudState = anchor.getCloudAnchorState();
// mc++                 if (cloudState.isError()) {
// mc++                   Log.w(
// mc++                       TAG,
// mc++                       "The anchor in room "
// mc++                           + roomCode
// mc++                           + " could not be resolved. The error state was "
// mc++                           + cloudState);
// mc++                   snackbarHelper.showMessageWithDismiss(
// mc++                       CloudAnchorActivity.this,
// mc++                       getString(R.string.snackbar_resolve_error, cloudState));
// mc++                   return;
// mc++                 }
// mc++                 snackbarHelper.showMessageWithDismiss(
// mc++                     CloudAnchorActivity.this, getString(R.string.snackbar_resolve_success));
// mc++                 setNewAnchor(anchor);
// mc++               });
// mc++         });
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Listens for both a new room code and an anchor ID, and shares the anchor ID in Firebase with
// mc++    * the room code when both are available.
// mc++    */
// mc++   private final class RoomCodeAndCloudAnchorIdListener
// mc++       implements CloudAnchorManager.CloudAnchorListener, FirebaseManager.RoomCodeListener {
// mc++ 
// mc++     private Long roomCode;
// mc++     private String cloudAnchorId;
// mc++ 
// mc++     @Override
// mc++     public void onNewRoomCode(Long newRoomCode) {
// mc++       Preconditions.checkState(roomCode == null, "The room code cannot have been set before.");
// mc++       roomCode = newRoomCode;
// mc++       roomCodeText.setText(String.valueOf(roomCode));
// mc++       snackbarHelper.showMessageWithDismiss(
// mc++           CloudAnchorActivity.this, getString(R.string.snackbar_room_code_available));
// mc++       checkAndMaybeShare();
// mc++       synchronized (singleTapLock) {
// mc++         // Change currentMode to HOSTING after receiving the room code (not when the 'Host' button
// mc++         // is tapped), to prevent an anchor being placed before we know the room code and able to
// mc++         // share the anchor ID.
// mc++         currentMode = HostResolveMode.HOSTING;
// mc++       }
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onError(DatabaseError error) {
// mc++       Log.w(TAG, "A Firebase database error happened.", error.toException());
// mc++       snackbarHelper.showError(
// mc++           CloudAnchorActivity.this, getString(R.string.snackbar_firebase_error));
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onCloudTaskComplete(Anchor anchor) {
// mc++       CloudAnchorState cloudState = anchor.getCloudAnchorState();
// mc++       if (cloudState.isError()) {
// mc++         Log.e(TAG, "Error hosting a cloud anchor, state " + cloudState);
// mc++         snackbarHelper.showMessageWithDismiss(
// mc++             CloudAnchorActivity.this, getString(R.string.snackbar_host_error, cloudState));
// mc++         return;
// mc++       }
// mc++       Preconditions.checkState(
// mc++           cloudAnchorId == null, "The cloud anchor ID cannot have been set before.");
// mc++       cloudAnchorId = anchor.getCloudAnchorId();
// mc++       setNewAnchor(anchor);
// mc++       checkAndMaybeShare();
// mc++     }
// mc++ 
// mc++     private void checkAndMaybeShare() {
// mc++       if (roomCode == null || cloudAnchorId == null) {
// mc++         return;
// mc++       }
// mc++       firebaseManager.storeAnchorIdInRoom(roomCode, cloudAnchorId);
// mc++       snackbarHelper.showMessageWithDismiss(
// mc++           CloudAnchorActivity.this, getString(R.string.snackbar_cloud_id_shared));
// mc++     }
// mc++   }
// mc++ }
