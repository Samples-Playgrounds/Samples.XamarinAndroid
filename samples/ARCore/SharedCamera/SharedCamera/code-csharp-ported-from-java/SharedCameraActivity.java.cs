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
// mc++ package com.google.ar.core.examples.java.sharedcamera;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.graphics.ImageFormat;
// mc++ import android.graphics.SurfaceTexture;
// mc++ import android.hardware.camera2.CameraAccessException;
// mc++ import android.hardware.camera2.CameraCaptureSession;
// mc++ import android.hardware.camera2.CameraCharacteristics;
// mc++ import android.hardware.camera2.CameraDevice;
// mc++ import android.hardware.camera2.CameraManager;
// mc++ import android.hardware.camera2.CaptureFailure;
// mc++ import android.hardware.camera2.CaptureRequest;
// mc++ import android.hardware.camera2.TotalCaptureResult;
// mc++ import android.media.Image;
// mc++ import android.media.ImageReader;
// mc++ import android.opengl.GLES20;
// mc++ import android.opengl.GLSurfaceView;
// mc++ import android.os.Build;
// mc++ import android.os.Bundle;
// mc++ import android.os.ConditionVariable;
// mc++ import android.os.Handler;
// mc++ import android.os.HandlerThread;
// mc++ import android.support.annotation.NonNull;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.util.Log;
// mc++ import android.util.Size;
// mc++ import android.view.MotionEvent;
// mc++ import android.view.Surface;
// mc++ import android.widget.LinearLayout;
// mc++ import android.widget.Switch;
// mc++ import android.widget.TextView;
// mc++ import android.widget.Toast;
// mc++ import com.google.ar.core.Anchor;
// mc++ import com.google.ar.core.ArCoreApk;
// mc++ import com.google.ar.core.Camera;
// mc++ import com.google.ar.core.Config;
// mc++ import com.google.ar.core.Frame;
// mc++ import com.google.ar.core.HitResult;
// mc++ import com.google.ar.core.Plane;
// mc++ import com.google.ar.core.Point;
// mc++ import com.google.ar.core.Point.OrientationMode;
// mc++ import com.google.ar.core.PointCloud;
// mc++ import com.google.ar.core.Session;
// mc++ import com.google.ar.core.SharedCamera;
// mc++ import com.google.ar.core.Trackable;
// mc++ import com.google.ar.core.TrackingState;
// mc++ import com.google.ar.core.examples.java.common.helpers.CameraPermissionHelper;
// mc++ import com.google.ar.core.examples.java.common.helpers.DisplayRotationHelper;
// mc++ import com.google.ar.core.examples.java.common.helpers.FullScreenHelper;
// mc++ import com.google.ar.core.examples.java.common.helpers.SnackbarHelper;
// mc++ import com.google.ar.core.examples.java.common.helpers.TapHelper;
// mc++ import com.google.ar.core.examples.java.common.rendering.BackgroundRenderer;
// mc++ import com.google.ar.core.examples.java.common.rendering.ObjectRenderer;
// mc++ import com.google.ar.core.examples.java.common.rendering.ObjectRenderer.BlendMode;
// mc++ import com.google.ar.core.examples.java.common.rendering.PlaneRenderer;
// mc++ import com.google.ar.core.examples.java.common.rendering.PointCloudRenderer;
// mc++ import com.google.ar.core.exceptions.CameraNotAvailableException;
// mc++ import com.google.ar.core.exceptions.UnavailableException;
// mc++ import java.io.IOException;
// mc++ import java.util.ArrayList;
// mc++ import java.util.Arrays;
// mc++ import java.util.EnumSet;
// mc++ import java.util.List;
// mc++ import java.util.concurrent.atomic.AtomicBoolean;
// mc++ import javax.microedition.khronos.egl.EGLConfig;
// mc++ import javax.microedition.khronos.opengles.GL10;
// mc++ 
// mc++ /**
// mc++  * This is a simple example that demonstrates how to use the Camera2 API while sharing camera access
// mc++  * with ARCore. An on-screen switch can be used to pause and resume ARCore. The app utilizes a
// mc++  * trivial sepia camera effect while ARCore is paused, and seamlessly hands camera capture request
// mc++  * control over to ARCore when it is running.
// mc++  *
// mc++  * <p>This app demonstrates:
// mc++  *
// mc++  * <ul>
// mc++  *   <li>Starting in AR or non-AR mode by setting the initial value of `arMode`
// mc++  *   <li>Toggling between non-AR and AR mode using an on screen switch
// mc++  *   <li>Pausing and resuming the app while in AR or non-AR mode
// mc++  *   <li>Requesting CAMERA_PERMISSION when app starts, and each time the app is resumed
// mc++  * </ul>
// mc++  */
// mc++ public class SharedCameraActivity extends AppCompatActivity
// mc++     implements GLSurfaceView.Renderer,
// mc++         ImageReader.OnImageAvailableListener,
// mc++         SurfaceTexture.OnFrameAvailableListener {
// mc++   private static final String TAG = SharedCameraActivity.class.getSimpleName();
// mc++ 
// mc++   // Whether the app is currently in AR mode. Initial value determines initial state.
// mc++   private boolean arMode = false;
// mc++ 
// mc++   // Whether the surface texture has been attached to the GL context.
// mc++   boolean isGlAttached;
// mc++ 
// mc++   // GL Surface used to draw camera preview image.
// mc++   private GLSurfaceView surfaceView;
// mc++ 
// mc++   // Text view for displaying on screen status message.
// mc++   private TextView statusTextView;
// mc++ 
// mc++   // Linear layout that contains preview image and status text.
// mc++   private LinearLayout imageTextLinearLayout;
// mc++ 
// mc++   // Switch to allow pausing and resuming of ARCore.
// mc++   private Switch arcoreSwitch;
// mc++ 
// mc++   // ARCore session that supports camera sharing.
// mc++   private Session sharedSession;
// mc++ 
// mc++   // Camera capture session. Used by both non-AR and AR modes.
// mc++   private CameraCaptureSession captureSession;
// mc++ 
// mc++   // Reference to the camera system service.
// mc++   private CameraManager cameraManager;
// mc++ 
// mc++   // A list of CaptureRequest keys that can cause delays when switching between AR and non-AR modes.
// mc++   private List<CaptureRequest.Key<?>> keysThatCanCauseCaptureDelaysWhenModified;
// mc++ 
// mc++   // Camera device. Used by both non-AR and AR modes.
// mc++   private CameraDevice cameraDevice;
// mc++ 
// mc++   // Looper handler thread.
// mc++   private HandlerThread backgroundThread;
// mc++ 
// mc++   // Looper handler.
// mc++   private Handler backgroundHandler;
// mc++ 
// mc++   // ARCore shared camera instance, obtained from ARCore session that supports sharing.
// mc++   private SharedCamera sharedCamera;
// mc++ 
// mc++   // Camera ID for the camera used by ARCore.
// mc++   private String cameraId;
// mc++ 
// mc++   // Ensure GL surface draws only occur when new frames are available.
// mc++   private final AtomicBoolean shouldUpdateSurfaceTexture = new AtomicBoolean(false);
// mc++ 
// mc++   // Whether ARCore is currently active.
// mc++   private boolean arcoreActive;
// mc++ 
// mc++   // Whether the GL surface has been created.
// mc++   private boolean surfaceCreated;
// mc++ 
// mc++   // Camera preview capture request builder
// mc++   private CaptureRequest.Builder previewCaptureRequestBuilder;
// mc++ 
// mc++   // Image reader that continuously processes CPU images.
// mc++   private ImageReader cpuImageReader;
// mc++ 
// mc++   // Total number of CPU images processed.
// mc++   private int cpuImagesProcessed;
// mc++ 
// mc++   // Various helper classes, see hello_ar_java sample to learn more.
// mc++   private final SnackbarHelper messageSnackbarHelper = new SnackbarHelper();
// mc++   private DisplayRotationHelper displayRotationHelper;
// mc++   private TapHelper tapHelper;
// mc++ 
// mc++   // Renderers, see hello_ar_java sample to learn more.
// mc++   private final BackgroundRenderer backgroundRenderer = new BackgroundRenderer();
// mc++   private final ObjectRenderer virtualObject = new ObjectRenderer();
// mc++   private final ObjectRenderer virtualObjectShadow = new ObjectRenderer();
// mc++   private final PlaneRenderer planeRenderer = new PlaneRenderer();
// mc++   private final PointCloudRenderer pointCloudRenderer = new PointCloudRenderer();
// mc++ 
// mc++   // Temporary matrix allocated here to reduce number of allocations for each frame.
// mc++   private final float[] anchorMatrix = new float[16];
// mc++   private static final float[] DEFAULT_COLOR = new float[] {0f, 0f, 0f, 0f};
// mc++ 
// mc++   // Anchors created from taps, see hello_ar_java sample to learn more.
// mc++   private final ArrayList<ColoredAnchor> anchors = new ArrayList<>();
// mc++ 
// mc++   // Required for test run.
// mc++   private static final Short AUTOMATOR_DEFAULT = 0;
// mc++   private static final String AUTOMATOR_KEY = "automator";
// mc++   private final AtomicBoolean automatorRun = new AtomicBoolean(false);
// mc++ 
// mc++   // Prevent any changes to camera capture session after CameraManager.openCamera() is called, but
// mc++   // before camera device becomes active.
// mc++   private boolean captureSessionChangesPossible = true;
// mc++ 
// mc++   // A check mechanism to ensure that the camera closed properly so that the app can safely exit.
// mc++   private final ConditionVariable safeToExitApp = new ConditionVariable();
// mc++ 
// mc++   private static class ColoredAnchor {
// mc++     public final Anchor anchor;
// mc++     public final float[] color;
// mc++ 
// mc++     public ColoredAnchor(Anchor a, float[] color4f) {
// mc++       this.anchor = a;
// mc++       this.color = color4f;
// mc++     }
// mc++   }
// mc++ 
// mc++   // Camera device state callback.
// mc++   private final CameraDevice.StateCallback cameraDeviceCallback =
// mc++       new CameraDevice.StateCallback() {
// mc++         @Override
// mc++         public void onOpened(@NonNull CameraDevice cameraDevice) {
// mc++           Log.d(TAG, "Camera device ID " + cameraDevice.getId() + " opened.");
// mc++           SharedCameraActivity.this.cameraDevice = cameraDevice;
// mc++           createCameraPreviewSession();
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public void onClosed(@NonNull CameraDevice cameraDevice) {
// mc++           Log.d(TAG, "Camera device ID " + cameraDevice.getId() + " closed.");
// mc++           SharedCameraActivity.this.cameraDevice = null;
// mc++           safeToExitApp.open();
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public void onDisconnected(@NonNull CameraDevice cameraDevice) {
// mc++           Log.w(TAG, "Camera device ID " + cameraDevice.getId() + " disconnected.");
// mc++           cameraDevice.close();
// mc++           SharedCameraActivity.this.cameraDevice = null;
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public void onError(@NonNull CameraDevice cameraDevice, int error) {
// mc++           Log.e(TAG, "Camera device ID " + cameraDevice.getId() + " error " + error);
// mc++           cameraDevice.close();
// mc++           SharedCameraActivity.this.cameraDevice = null;
// mc++           // Fatal error. Quit application.
// mc++           finish();
// mc++         }
// mc++       };
// mc++ 
// mc++   // Repeating camera capture session state callback.
// mc++   CameraCaptureSession.StateCallback cameraCaptureCallback =
// mc++       new CameraCaptureSession.StateCallback() {
// mc++ 
// mc++         // Called when the camera capture session is first configured after the app
// mc++         // is initialized, and again each time the activity is resumed.
// mc++         @Override
// mc++         public void onConfigured(@NonNull CameraCaptureSession session) {
// mc++           Log.d(TAG, "Camera capture session configured.");
// mc++           captureSession = session;
// mc++           if (arMode) {
// mc++             setRepeatingCaptureRequest();
// mc++             // Note, resumeARCore() must be called in onActive(), not here.
// mc++           } else {
// mc++             // Calls `setRepeatingCaptureRequest()`.
// mc++             resumeCamera2();
// mc++           }
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public void onSurfacePrepared(
// mc++             @NonNull CameraCaptureSession session, @NonNull Surface surface) {
// mc++           Log.d(TAG, "Camera capture surface prepared.");
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public void onReady(@NonNull CameraCaptureSession session) {
// mc++           Log.d(TAG, "Camera capture session ready.");
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public void onActive(@NonNull CameraCaptureSession session) {
// mc++           Log.d(TAG, "Camera capture session active.");
// mc++           if (arMode && !arcoreActive) {
// mc++             resumeARCore();
// mc++           }
// mc++           synchronized (SharedCameraActivity.this) {
// mc++             captureSessionChangesPossible = true;
// mc++             SharedCameraActivity.this.notify();
// mc++           }
// mc++           updateSnackbarMessage();
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public void onCaptureQueueEmpty(@NonNull CameraCaptureSession session) {
// mc++           Log.w(TAG, "Camera capture queue empty.");
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public void onClosed(@NonNull CameraCaptureSession session) {
// mc++           Log.d(TAG, "Camera capture session closed.");
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public void onConfigureFailed(@NonNull CameraCaptureSession session) {
// mc++           Log.e(TAG, "Failed to configure camera capture session.");
// mc++         }
// mc++       };
// mc++ 
// mc++   // Repeating camera capture session capture callback.
// mc++   private final CameraCaptureSession.CaptureCallback captureSessionCallback =
// mc++       new CameraCaptureSession.CaptureCallback() {
// mc++ 
// mc++         @Override
// mc++         public void onCaptureCompleted(
// mc++             @NonNull CameraCaptureSession session,
// mc++             @NonNull CaptureRequest request,
// mc++             @NonNull TotalCaptureResult result) {
// mc++           shouldUpdateSurfaceTexture.set(true);
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public void onCaptureBufferLost(
// mc++             @NonNull CameraCaptureSession session,
// mc++             @NonNull CaptureRequest request,
// mc++             @NonNull Surface target,
// mc++             long frameNumber) {
// mc++           Log.e(TAG, "onCaptureBufferLost: " + frameNumber);
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public void onCaptureFailed(
// mc++             @NonNull CameraCaptureSession session,
// mc++             @NonNull CaptureRequest request,
// mc++             @NonNull CaptureFailure failure) {
// mc++           Log.e(TAG, "onCaptureFailed: " + failure.getFrameNumber() + " " + failure.getReason());
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public void onCaptureSequenceAborted(
// mc++             @NonNull CameraCaptureSession session, int sequenceId) {
// mc++           Log.e(TAG, "onCaptureSequenceAborted: " + sequenceId + " " + session);
// mc++         }
// mc++       };
// mc++ 
// mc++   @Override
// mc++   protected void onCreate(Bundle savedInstanceState) {
// mc++     super.onCreate(savedInstanceState);
// mc++     setContentView(R.layout.activity_main);
// mc++ 
// mc++     Bundle extraBundle = getIntent().getExtras();
// mc++     if (extraBundle != null && 1 == extraBundle.getShort(AUTOMATOR_KEY, AUTOMATOR_DEFAULT)) {
// mc++       automatorRun.set(true);
// mc++     }
// mc++ 
// mc++     // GL surface view that renders camera preview image.
// mc++     surfaceView = findViewById(R.id.glsurfaceview);
// mc++     surfaceView.setPreserveEGLContextOnPause(true);
// mc++     surfaceView.setEGLContextClientVersion(2);
// mc++     surfaceView.setEGLConfigChooser(8, 8, 8, 8, 16, 0);
// mc++     surfaceView.setRenderer(this);
// mc++     surfaceView.setRenderMode(GLSurfaceView.RENDERMODE_CONTINUOUSLY);
// mc++ 
// mc++     // Helpers, see hello_ar_java sample to learn more.
// mc++     displayRotationHelper = new DisplayRotationHelper(this);
// mc++     tapHelper = new TapHelper(this);
// mc++     surfaceView.setOnTouchListener(tapHelper);
// mc++ 
// mc++     imageTextLinearLayout = findViewById(R.id.image_text_layout);
// mc++     statusTextView = findViewById(R.id.text_view);
// mc++     arcoreSwitch = findViewById(R.id.arcore_switch);
// mc++ 
// mc++     // Ensure initial switch position is set based on initial value of `arMode` variable.
// mc++     arcoreSwitch.setChecked(arMode);
// mc++ 
// mc++     arcoreSwitch.setOnCheckedChangeListener(
// mc++         (view, checked) -> {
// mc++           Log.i(TAG, "Switching to " + (checked ? "AR" : "non-AR") + " mode.");
// mc++           if (checked) {
// mc++             arMode = true;
// mc++             resumeARCore();
// mc++           } else {
// mc++             arMode = false;
// mc++             pauseARCore();
// mc++             resumeCamera2();
// mc++           }
// mc++           updateSnackbarMessage();
// mc++         });
// mc++ 
// mc++     messageSnackbarHelper.setMaxLines(4);
// mc++     updateSnackbarMessage();
// mc++   }
// mc++ 
// mc++   private synchronized void waitUntilCameraCaptureSesssionIsActive() {
// mc++     while (!captureSessionChangesPossible) {
// mc++       try {
// mc++         this.wait();
// mc++       } catch (InterruptedException e) {
// mc++         Log.e(TAG, "Unable to wait for a safe time to make changes to the capture session", e);
// mc++       }
// mc++     }
// mc++   }
// mc++ 
// mc++   @Override
// mc++   protected void onResume() {
// mc++     super.onResume();
// mc++     waitUntilCameraCaptureSesssionIsActive();
// mc++     startBackgroundThread();
// mc++     surfaceView.onResume();
// mc++ 
// mc++     // When the activity starts and resumes for the first time, openCamera() will be called
// mc++     // from onSurfaceCreated(). In subsequent resumes we call openCamera() here.
// mc++     if (surfaceCreated) {
// mc++       openCamera();
// mc++     }
// mc++ 
// mc++     displayRotationHelper.onResume();
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onPause() {
// mc++     surfaceView.onPause();
// mc++     waitUntilCameraCaptureSesssionIsActive();
// mc++     displayRotationHelper.onPause();
// mc++     if (arMode) {
// mc++       pauseARCore();
// mc++     }
// mc++     closeCamera();
// mc++     stopBackgroundThread();
// mc++     super.onPause();
// mc++   }
// mc++ 
// mc++   private void resumeCamera2() {
// mc++     setRepeatingCaptureRequest();
// mc++     sharedCamera.getSurfaceTexture().setOnFrameAvailableListener(this);
// mc++   }
// mc++ 
// mc++   private void resumeARCore() {
// mc++     // Ensure that session is valid before triggering ARCore resume. Handles the case where the user
// mc++     // manually uninstalls ARCore while the app is paused and then resumes.
// mc++     if (sharedSession == null) {
// mc++       return;
// mc++     }
// mc++ 
// mc++     if (!arcoreActive) {
// mc++       try {
// mc++         // Resume ARCore.
// mc++         sharedSession.resume();
// mc++         arcoreActive = true;
// mc++         updateSnackbarMessage();
// mc++ 
// mc++         // Set capture session callback while in AR mode.
// mc++         sharedCamera.setCaptureCallback(captureSessionCallback, backgroundHandler);
// mc++       } catch (CameraNotAvailableException e) {
// mc++         Log.e(TAG, "Failed to resume ARCore session", e);
// mc++         return;
// mc++       }
// mc++     }
// mc++   }
// mc++ 
// mc++   private void pauseARCore() {
// mc++     shouldUpdateSurfaceTexture.set(false);
// mc++     if (arcoreActive) {
// mc++       // Pause ARCore.
// mc++       sharedSession.pause();
// mc++       arcoreActive = false;
// mc++       updateSnackbarMessage();
// mc++     }
// mc++   }
// mc++ 
// mc++   private void updateSnackbarMessage() {
// mc++     messageSnackbarHelper.showMessage(
// mc++         this,
// mc++         arcoreActive
// mc++             ? "ARCore is active.\nSearch for plane, then tap to place a 3D model."
// mc++             : "ARCore is paused.\nCamera effects enabled.");
// mc++   }
// mc++ 
// mc++   // Called when starting non-AR mode or switching to non-AR mode.
// mc++   // Also called when app starts in AR mode, or resumes in AR mode.
// mc++   private void setRepeatingCaptureRequest() {
// mc++     try {
// mc++       setCameraEffects(previewCaptureRequestBuilder);
// mc++ 
// mc++       captureSession.setRepeatingRequest(
// mc++           previewCaptureRequestBuilder.build(), captureSessionCallback, backgroundHandler);
// mc++     } catch (CameraAccessException e) {
// mc++       Log.e(TAG, "Failed to set repeating request", e);
// mc++     }
// mc++   }
// mc++ 
// mc++   private void createCameraPreviewSession() {
// mc++     try {
// mc++       // Note that isGlAttached will be set to true in AR mode in onDrawFrame().
// mc++       sharedSession.setCameraTextureName(backgroundRenderer.getTextureId());
// mc++       sharedCamera.getSurfaceTexture().setOnFrameAvailableListener(this);
// mc++ 
// mc++       // Create an ARCore compatible capture request using `TEMPLATE_RECORD`.
// mc++       previewCaptureRequestBuilder =
// mc++           cameraDevice.createCaptureRequest(CameraDevice.TEMPLATE_RECORD);
// mc++ 
// mc++       // Build surfaces list, starting with ARCore provided surfaces.
// mc++       List<Surface> surfaceList = sharedCamera.getArCoreSurfaces();
// mc++ 
// mc++       // Add a CPU image reader surface. On devices that don't support CPU image access, the image
// mc++       // may arrive significantly later, or not arrive at all.
// mc++       surfaceList.add(cpuImageReader.getSurface());
// mc++ 
// mc++       // Surface list should now contain three surfaces:
// mc++       // 0. sharedCamera.getSurfaceTexture()
// mc++       // 1. …
// mc++       // 2. cpuImageReader.getSurface()
// mc++ 
// mc++       // Add ARCore surfaces and CPU image surface targets.
// mc++       for (Surface surface : surfaceList) {
// mc++         previewCaptureRequestBuilder.addTarget(surface);
// mc++       }
// mc++ 
// mc++       // Wrap our callback in a shared camera callback.
// mc++       CameraCaptureSession.StateCallback wrappedCallback =
// mc++           sharedCamera.createARSessionStateCallback(cameraCaptureCallback, backgroundHandler);
// mc++ 
// mc++       // Create camera capture session for camera preview using ARCore wrapped callback.
// mc++       cameraDevice.createCaptureSession(surfaceList, wrappedCallback, backgroundHandler);
// mc++     } catch (CameraAccessException e) {
// mc++       Log.e(TAG, "CameraAccessException", e);
// mc++     }
// mc++   }
// mc++ 
// mc++   // Start background handler thread, used to run callbacks without blocking UI thread.
// mc++   private void startBackgroundThread() {
// mc++     backgroundThread = new HandlerThread("sharedCameraBackground");
// mc++     backgroundThread.start();
// mc++     backgroundHandler = new Handler(backgroundThread.getLooper());
// mc++   }
// mc++ 
// mc++   // Stop background handler thread.
// mc++   private void stopBackgroundThread() {
// mc++     if (backgroundThread != null) {
// mc++       backgroundThread.quitSafely();
// mc++       try {
// mc++         backgroundThread.join();
// mc++         backgroundThread = null;
// mc++         backgroundHandler = null;
// mc++       } catch (InterruptedException e) {
// mc++         Log.e(TAG, "Interrupted while trying to join background handler thread", e);
// mc++       }
// mc++     }
// mc++   }
// mc++ 
// mc++   // Perform various checks, then open camera device and create CPU image reader.
// mc++   private void openCamera() {
// mc++     // Don't open camera if already opened.
// mc++     if (cameraDevice != null) {
// mc++       return;
// mc++     }
// mc++ 
// mc++     // Verify CAMERA_PERMISSION has been granted.
// mc++     if (!CameraPermissionHelper.hasCameraPermission(this)) {
// mc++       CameraPermissionHelper.requestCameraPermission(this);
// mc++       return;
// mc++     }
// mc++ 
// mc++     // Make sure that ARCore is installed, up to date, and supported on this device.
// mc++     if (!isARCoreSupportedAndUpToDate()) {
// mc++       return;
// mc++     }
// mc++ 
// mc++     if (sharedSession == null) {
// mc++       try {
// mc++         // Create ARCore session that supports camera sharing.
// mc++         sharedSession = new Session(this, EnumSet.of(Session.Feature.SHARED_CAMERA));
// mc++       } catch (UnavailableException e) {
// mc++         Log.e(TAG, "Failed to create ARCore session that supports camera sharing", e);
// mc++         return;
// mc++       }
// mc++ 
// mc++       // Enable auto focus mode while ARCore is running.
// mc++       Config config = sharedSession.getConfig();
// mc++       config.setFocusMode(Config.FocusMode.AUTO);
// mc++       sharedSession.configure(config);
// mc++     }
// mc++ 
// mc++     // Store the ARCore shared camera reference.
// mc++     sharedCamera = sharedSession.getSharedCamera();
// mc++ 
// mc++     // Store the ID of the camera used by ARCore.
// mc++     cameraId = sharedSession.getCameraConfig().getCameraId();
// mc++ 
// mc++     // Use the currently configured CPU image size.
// mc++     Size desiredCpuImageSize = sharedSession.getCameraConfig().getImageSize();
// mc++     cpuImageReader =
// mc++         ImageReader.newInstance(
// mc++             desiredCpuImageSize.getWidth(),
// mc++             desiredCpuImageSize.getHeight(),
// mc++             ImageFormat.YUV_420_888,
// mc++             2);
// mc++     cpuImageReader.setOnImageAvailableListener(this, backgroundHandler);
// mc++ 
// mc++     // When ARCore is running, make sure it also updates our CPU image surface.
// mc++     sharedCamera.setAppSurfaces(this.cameraId, Arrays.asList(cpuImageReader.getSurface()));
// mc++ 
// mc++     try {
// mc++ 
// mc++       // Wrap our callback in a shared camera callback.
// mc++       CameraDevice.StateCallback wrappedCallback =
// mc++           sharedCamera.createARDeviceStateCallback(cameraDeviceCallback, backgroundHandler);
// mc++ 
// mc++       // Store a reference to the camera system service.
// mc++       cameraManager = (CameraManager) this.getSystemService(Context.CAMERA_SERVICE);
// mc++ 
// mc++       // Get the characteristics for the ARCore camera.
// mc++       CameraCharacteristics characteristics = cameraManager.getCameraCharacteristics(this.cameraId);
// mc++ 
// mc++       // On Android P and later, get list of keys that are difficult to apply per-frame and can
// mc++       // result in unexpected delays when modified during the capture session lifetime.
// mc++       if (Build.VERSION.SDK_INT >= 28) {
// mc++         keysThatCanCauseCaptureDelaysWhenModified = characteristics.getAvailableSessionKeys();
// mc++         if (keysThatCanCauseCaptureDelaysWhenModified == null) {
// mc++           // Initialize the list to an empty list if getAvailableSessionKeys() returns null.
// mc++           keysThatCanCauseCaptureDelaysWhenModified = new ArrayList<>();
// mc++         }
// mc++       }
// mc++ 
// mc++       // Prevent app crashes due to quick operations on camera open / close by waiting for the
// mc++       // capture session's onActive() callback to be triggered.
// mc++       captureSessionChangesPossible = false;
// mc++ 
// mc++       // Open the camera device using the ARCore wrapped callback.
// mc++       cameraManager.openCamera(cameraId, wrappedCallback, backgroundHandler);
// mc++     } catch (CameraAccessException | IllegalArgumentException | SecurityException e) {
// mc++       Log.e(TAG, "Failed to open camera", e);
// mc++     }
// mc++   }
// mc++ 
// mc++   private <T> boolean checkIfKeyCanCauseDelay(CaptureRequest.Key<T> key) {
// mc++     if (Build.VERSION.SDK_INT >= 28) {
// mc++       // On Android P and later, return true if key is difficult to apply per-frame.
// mc++       return keysThatCanCauseCaptureDelaysWhenModified.contains(key);
// mc++     } else {
// mc++       // On earlier Android versions, log a warning since there is no API to determine whether
// mc++       // the key is difficult to apply per-frame. Certain keys such as CONTROL_AE_TARGET_FPS_RANGE
// mc++       // are known to cause a noticeable delay on certain devices.
// mc++       // If avoiding unexpected capture delays when switching between non-AR and AR modes is
// mc++       // important, verify the runtime behavior on each pre-Android P device on which the app will
// mc++       // be distributed. Note that this device-specific runtime behavior may change when the
// mc++       // device's operating system is updated.
// mc++       Log.w(
// mc++           TAG,
// mc++           "Changing "
// mc++               + key
// mc++               + " may cause a noticeable capture delay. Please verify actual runtime behavior on"
// mc++               + " specific pre-Android P devices that this app will be distributed on.");
// mc++       // Allow the change since we're unable to determine whether it can cause unexpected delays.
// mc++       return false;
// mc++     }
// mc++   }
// mc++ 
// mc++   // If possible, apply effect in non-AR mode, to help visually distinguish between from AR mode.
// mc++   private void setCameraEffects(CaptureRequest.Builder captureBuilder) {
// mc++     if (checkIfKeyCanCauseDelay(CaptureRequest.CONTROL_EFFECT_MODE)) {
// mc++       Log.w(TAG, "Not setting CONTROL_EFFECT_MODE since it can cause delays between transitions.");
// mc++     } else {
// mc++       Log.d(TAG, "Setting CONTROL_EFFECT_MODE to SEPIA in non-AR mode.");
// mc++       captureBuilder.set(
// mc++           CaptureRequest.CONTROL_EFFECT_MODE, CaptureRequest.CONTROL_EFFECT_MODE_SEPIA);
// mc++     }
// mc++   }
// mc++ 
// mc++   // Close the camera device.
// mc++   private void closeCamera() {
// mc++     if (captureSession != null) {
// mc++       captureSession.close();
// mc++       captureSession = null;
// mc++     }
// mc++     if (cameraDevice != null) {
// mc++       waitUntilCameraCaptureSesssionIsActive();
// mc++       safeToExitApp.close();
// mc++       cameraDevice.close();
// mc++       safeToExitApp.block();
// mc++     }
// mc++     if (cpuImageReader != null) {
// mc++       cpuImageReader.close();
// mc++       cpuImageReader = null;
// mc++     }
// mc++   }
// mc++ 
// mc++   // Surface texture on frame available callback, used only in non-AR mode.
// mc++   @Override
// mc++   public void onFrameAvailable(SurfaceTexture surfaceTexture) {
// mc++     // Log.d(TAG, "onFrameAvailable()");
// mc++   }
// mc++ 
// mc++   // CPU image reader callback.
// mc++   @Override
// mc++   public void onImageAvailable(ImageReader imageReader) {
// mc++     Image image = imageReader.acquireLatestImage();
// mc++     if (image == null) {
// mc++       Log.w(TAG, "onImageAvailable: Skipping null image.");
// mc++       return;
// mc++     }
// mc++ 
// mc++     image.close();
// mc++     cpuImagesProcessed++;
// mc++ 
// mc++     // Reduce the screen update to once every two seconds with 30fps if running as automated test.
// mc++     if (!automatorRun.get() || (automatorRun.get() && cpuImagesProcessed % 60 == 0)) {
// mc++       runOnUiThread(
// mc++           () ->
// mc++               statusTextView.setText(
// mc++                   "CPU images processed: "
// mc++                       + cpuImagesProcessed
// mc++                       + "\n\nMode: "
// mc++                       + (arMode ? "AR" : "non-AR")
// mc++                       + " \nARCore active: "
// mc++                       + arcoreActive
// mc++                       + " \nShould update surface texture: "
// mc++                       + shouldUpdateSurfaceTexture.get()));
// mc++     }
// mc++   }
// mc++ 
// mc++   // Android permission request callback.
// mc++   @Override
// mc++   public void onRequestPermissionsResult(int requestCode, String[] permissions, int[] results) {
// mc++     if (!CameraPermissionHelper.hasCameraPermission(this)) {
// mc++       Toast.makeText(
// mc++               getApplicationContext(),
// mc++               "Camera permission is needed to run this application",
// mc++               Toast.LENGTH_LONG)
// mc++           .show();
// mc++       if (!CameraPermissionHelper.shouldShowRequestPermissionRationale(this)) {
// mc++         // Permission denied with checking "Do not ask again".
// mc++         CameraPermissionHelper.launchPermissionSettings(this);
// mc++       }
// mc++       finish();
// mc++     }
// mc++   }
// mc++ 
// mc++   // Android focus change callback.
// mc++   @Override
// mc++   public void onWindowFocusChanged(boolean hasFocus) {
// mc++     super.onWindowFocusChanged(hasFocus);
// mc++     FullScreenHelper.setFullScreenOnWindowFocusChanged(this, hasFocus);
// mc++   }
// mc++ 
// mc++   // GL surface created callback. Will be called on the GL thread.
// mc++   @Override
// mc++   public void onSurfaceCreated(GL10 gl, EGLConfig config) {
// mc++     surfaceCreated = true;
// mc++ 
// mc++     // Set GL clear color to black.
// mc++     GLES20.glClearColor(0f, 0f, 0f, 1.0f);
// mc++ 
// mc++     // Prepare the rendering objects. This involves reading shaders, so may throw an IOException.
// mc++     try {
// mc++       // Create the camera preview image texture. Used in non-AR and AR mode.
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
// mc++ 
// mc++       openCamera();
// mc++     } catch (IOException e) {
// mc++       Log.e(TAG, "Failed to read an asset file", e);
// mc++     }
// mc++   }
// mc++ 
// mc++   // GL surface changed callback. Will be called on the GL thread.
// mc++   @Override
// mc++   public void onSurfaceChanged(GL10 gl, int width, int height) {
// mc++     GLES20.glViewport(0, 0, width, height);
// mc++     displayRotationHelper.onSurfaceChanged(width, height);
// mc++ 
// mc++     runOnUiThread(
// mc++         () -> {
// mc++           // Adjust layout based on display orientation.
// mc++           imageTextLinearLayout.setOrientation(
// mc++               width > height ? LinearLayout.HORIZONTAL : LinearLayout.VERTICAL);
// mc++         });
// mc++   }
// mc++ 
// mc++   // GL draw callback. Will be called each frame on the GL thread.
// mc++   @Override
// mc++   public void onDrawFrame(GL10 gl) {
// mc++     // Use the cGL clear color specified in onSurfaceCreated() to erase the GL surface.
// mc++     GLES20.glClear(GLES20.GL_COLOR_BUFFER_BIT | GLES20.GL_DEPTH_BUFFER_BIT);
// mc++ 
// mc++     if (!shouldUpdateSurfaceTexture.get()) {
// mc++       // Not ready to draw.
// mc++       return;
// mc++     }
// mc++ 
// mc++     // Handle display rotations.
// mc++     displayRotationHelper.updateSessionIfNeeded(sharedSession);
// mc++ 
// mc++     try {
// mc++       if (arMode) {
// mc++         onDrawFrameARCore();
// mc++       } else {
// mc++         onDrawFrameCamera2();
// mc++       }
// mc++     } catch (Throwable t) {
// mc++       // Avoid crashing the application due to unhandled exceptions.
// mc++       Log.e(TAG, "Exception on the OpenGL thread", t);
// mc++     }
// mc++   }
// mc++ 
// mc++   // Draw frame when in non-AR mode. Called on the GL thread.
// mc++   public void onDrawFrameCamera2() {
// mc++     SurfaceTexture texture = sharedCamera.getSurfaceTexture();
// mc++ 
// mc++     // Ensure the surface is attached to the GL context.
// mc++     if (!isGlAttached) {
// mc++       texture.attachToGLContext(backgroundRenderer.getTextureId());
// mc++       isGlAttached = true;
// mc++     }
// mc++ 
// mc++     // Update the surface.
// mc++     texture.updateTexImage();
// mc++ 
// mc++     // Account for any difference between camera sensor orientation and display orientation.
// mc++     int rotationDegrees = displayRotationHelper.getCameraSensorToDisplayRotation(cameraId);
// mc++ 
// mc++     // Determine size of the camera preview image.
// mc++     Size size = sharedSession.getCameraConfig().getTextureSize();
// mc++ 
// mc++     // Determine aspect ratio of the output GL surface, accounting for the current display rotation
// mc++     // relative to the camera sensor orientation of the device.
// mc++     float displayAspectRatio =
// mc++         displayRotationHelper.getCameraSensorRelativeViewportAspectRatio(cameraId);
// mc++ 
// mc++     // Render camera preview image to the GL surface.
// mc++     backgroundRenderer.draw(size.getWidth(), size.getHeight(), displayAspectRatio, rotationDegrees);
// mc++   }
// mc++ 
// mc++   // Draw frame when in AR mode. Called on the GL thread.
// mc++   public void onDrawFrameARCore() throws CameraNotAvailableException {
// mc++     if (!arcoreActive) {
// mc++       // ARCore not yet active, so nothing to draw yet.
// mc++       return;
// mc++     }
// mc++ 
// mc++     // Perform ARCore per-frame update.
// mc++     Frame frame = sharedSession.update();
// mc++     Camera camera = frame.getCamera();
// mc++ 
// mc++     // ARCore attached the surface to GL context using the texture ID we provided
// mc++     // in createCameraPreviewSession() via sharedSession.setCameraTextureName(…).
// mc++     isGlAttached = true;
// mc++ 
// mc++     // Handle screen tap.
// mc++     handleTap(frame, camera);
// mc++ 
// mc++     // If frame is ready, render camera preview image to the GL surface.
// mc++     backgroundRenderer.draw(frame);
// mc++ 
// mc++     // If not tracking, don't draw 3D objects.
// mc++     if (camera.getTrackingState() == TrackingState.PAUSED) {
// mc++       return;
// mc++     }
// mc++ 
// mc++     // Get projection matrix.
// mc++     float[] projmtx = new float[16];
// mc++     camera.getProjectionMatrix(projmtx, 0, 0.1f, 100.0f);
// mc++ 
// mc++     // Get camera matrix and draw.
// mc++     float[] viewmtx = new float[16];
// mc++     camera.getViewMatrix(viewmtx, 0);
// mc++ 
// mc++     // Compute lighting from average intensity of the image.
// mc++     // The first three components are color scaling factors.
// mc++     // The last one is the average pixel intensity in gamma space.
// mc++     final float[] colorCorrectionRgba = new float[4];
// mc++     frame.getLightEstimate().getColorCorrection(colorCorrectionRgba, 0);
// mc++ 
// mc++     // Visualize tracked points.
// mc++     // Use try-with-resources to automatically release the point cloud.
// mc++     try (PointCloud pointCloud = frame.acquirePointCloud()) {
// mc++       pointCloudRenderer.update(pointCloud);
// mc++       pointCloudRenderer.draw(viewmtx, projmtx);
// mc++     }
// mc++ 
// mc++     // If we detected any plane and snackbar is visible, then hide the snackbar.
// mc++     if (messageSnackbarHelper.isShowing()) {
// mc++       for (Plane plane : sharedSession.getAllTrackables(Plane.class)) {
// mc++         if (plane.getTrackingState() == TrackingState.TRACKING) {
// mc++           messageSnackbarHelper.hide(this);
// mc++           break;
// mc++         }
// mc++       }
// mc++     }
// mc++ 
// mc++     // Visualize planes.
// mc++     planeRenderer.drawPlanes(
// mc++         sharedSession.getAllTrackables(Plane.class), camera.getDisplayOrientedPose(), projmtx);
// mc++ 
// mc++     // Visualize anchors created by touch.
// mc++     float scaleFactor = 1.0f;
// mc++     for (ColoredAnchor coloredAnchor : anchors) {
// mc++       if (coloredAnchor.anchor.getTrackingState() != TrackingState.TRACKING) {
// mc++         continue;
// mc++       }
// mc++       // Get the current pose of an Anchor in world space. The Anchor pose is updated
// mc++       // during calls to sharedSession.update() as ARCore refines its estimate of the world.
// mc++       coloredAnchor.anchor.getPose().toMatrix(anchorMatrix, 0);
// mc++ 
// mc++       // Update and draw the model and its shadow.
// mc++       virtualObject.updateModelMatrix(anchorMatrix, scaleFactor);
// mc++       virtualObjectShadow.updateModelMatrix(anchorMatrix, scaleFactor);
// mc++       virtualObject.draw(viewmtx, projmtx, colorCorrectionRgba, coloredAnchor.color);
// mc++       virtualObjectShadow.draw(viewmtx, projmtx, colorCorrectionRgba, coloredAnchor.color);
// mc++     }
// mc++   }
// mc++ 
// mc++   // Handle only one tap per frame, as taps are usually low frequency compared to frame rate.
// mc++   private void handleTap(Frame frame, Camera camera) {
// mc++     MotionEvent tap = tapHelper.poll();
// mc++     if (tap != null && camera.getTrackingState() == TrackingState.TRACKING) {
// mc++       for (HitResult hit : frame.hitTest(tap)) {
// mc++         // Check if any plane was hit, and if it was hit inside the plane polygon
// mc++         Trackable trackable = hit.getTrackable();
// mc++         // Creates an anchor if a plane or an oriented point was hit.
// mc++         if ((trackable instanceof Plane
// mc++                 && ((Plane) trackable).isPoseInPolygon(hit.getHitPose())
// mc++                 && (PlaneRenderer.calculateDistanceToPlane(hit.getHitPose(), camera.getPose()) > 0))
// mc++             || (trackable instanceof Point
// mc++                 && ((Point) trackable).getOrientationMode()
// mc++                     == OrientationMode.ESTIMATED_SURFACE_NORMAL)) {
// mc++           // Hits are sorted by depth. Consider only closest hit on a plane or oriented point.
// mc++           // Cap the number of objects created. This avoids overloading both the
// mc++           // rendering system and ARCore.
// mc++           if (anchors.size() >= 20) {
// mc++             anchors.get(0).anchor.detach();
// mc++             anchors.remove(0);
// mc++           }
// mc++ 
// mc++           // Assign a color to the object for rendering based on the trackable type
// mc++           // this anchor attached to. For AR_TRACKABLE_POINT, it's blue color, and
// mc++           // for AR_TRACKABLE_PLANE, it's green color.
// mc++           float[] objColor;
// mc++           if (trackable instanceof Point) {
// mc++             objColor = new float[] {66.0f, 133.0f, 244.0f, 255.0f};
// mc++           } else if (trackable instanceof Plane) {
// mc++             objColor = new float[] {139.0f, 195.0f, 74.0f, 255.0f};
// mc++           } else {
// mc++             objColor = DEFAULT_COLOR;
// mc++           }
// mc++ 
// mc++           // Adding an Anchor tells ARCore that it should track this position in
// mc++           // space. This anchor is created on the Plane to place the 3D model
// mc++           // in the correct position relative both to the world and to the plane.
// mc++           anchors.add(new ColoredAnchor(hit.createAnchor(), objColor));
// mc++           break;
// mc++         }
// mc++       }
// mc++     }
// mc++   }
// mc++ 
// mc++   private boolean isARCoreSupportedAndUpToDate() {
// mc++     // Make sure ARCore is installed and supported on this device.
// mc++     ArCoreApk.Availability availability = ArCoreApk.getInstance().checkAvailability(this);
// mc++     switch (availability) {
// mc++       case SUPPORTED_INSTALLED:
// mc++         break;
// mc++       case SUPPORTED_APK_TOO_OLD:
// mc++       case SUPPORTED_NOT_INSTALLED:
// mc++         try {
// mc++           // Request ARCore installation or update if needed.
// mc++           ArCoreApk.InstallStatus installStatus =
// mc++               ArCoreApk.getInstance().requestInstall(this, /*userRequestedInstall=*/ true);
// mc++           switch (installStatus) {
// mc++             case INSTALL_REQUESTED:
// mc++               Log.e(TAG, "ARCore installation requested.");
// mc++               return false;
// mc++             case INSTALLED:
// mc++               break;
// mc++           }
// mc++         } catch (UnavailableException e) {
// mc++           Log.e(TAG, "ARCore not installed", e);
// mc++           runOnUiThread(
// mc++               () ->
// mc++                   Toast.makeText(
// mc++                           getApplicationContext(), "ARCore not installed\n" + e, Toast.LENGTH_LONG)
// mc++                       .show());
// mc++           finish();
// mc++           return false;
// mc++         }
// mc++         break;
// mc++       case UNKNOWN_ERROR:
// mc++       case UNKNOWN_CHECKING:
// mc++       case UNKNOWN_TIMED_OUT:
// mc++       case UNSUPPORTED_DEVICE_NOT_CAPABLE:
// mc++         Log.e(
// mc++             TAG,
// mc++             "ARCore is not supported on this device, ArCoreApk.checkAvailability() returned "
// mc++                 + availability);
// mc++         runOnUiThread(
// mc++             () ->
// mc++                 Toast.makeText(
// mc++                         getApplicationContext(),
// mc++                         "ARCore is not supported on this device, "
// mc++                             + "ArCoreApk.checkAvailability() returned "
// mc++                             + availability,
// mc++                         Toast.LENGTH_LONG)
// mc++                     .show());
// mc++         return false;
// mc++     }
// mc++     return true;
// mc++   }
// mc++ }
