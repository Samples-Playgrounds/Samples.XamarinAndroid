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
// mc++ package com.google.ar.core.examples.java.computervision;
// mc++ 
// mc++ import android.graphics.ImageFormat;
// mc++ import android.media.Image;
// mc++ import android.opengl.GLES20;
// mc++ import android.opengl.GLSurfaceView;
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.util.Log;
// mc++ import android.util.Size;
// mc++ import android.view.GestureDetector;
// mc++ import android.view.MotionEvent;
// mc++ import android.view.View;
// mc++ import android.widget.CompoundButton;
// mc++ import android.widget.RadioButton;
// mc++ import android.widget.RadioGroup;
// mc++ import android.widget.Switch;
// mc++ import android.widget.TextView;
// mc++ import android.widget.Toast;
// mc++ import com.google.ar.core.ArCoreApk;
// mc++ import com.google.ar.core.Camera;
// mc++ import com.google.ar.core.CameraConfig;
// mc++ import com.google.ar.core.CameraIntrinsics;
// mc++ import com.google.ar.core.Config;
// mc++ import com.google.ar.core.Frame;
// mc++ import com.google.ar.core.Session;
// mc++ import com.google.ar.core.examples.java.common.helpers.CameraPermissionHelper;
// mc++ import com.google.ar.core.examples.java.common.helpers.FullScreenHelper;
// mc++ import com.google.ar.core.examples.java.common.helpers.SnackbarHelper;
// mc++ import com.google.ar.core.exceptions.CameraNotAvailableException;
// mc++ import com.google.ar.core.exceptions.NotYetAvailableException;
// mc++ import com.google.ar.core.exceptions.UnavailableApkTooOldException;
// mc++ import com.google.ar.core.exceptions.UnavailableArcoreNotInstalledException;
// mc++ import com.google.ar.core.exceptions.UnavailableSdkTooOldException;
// mc++ import com.google.ar.core.exceptions.UnavailableUserDeclinedInstallationException;
// mc++ import java.io.IOException;
// mc++ import java.nio.ByteBuffer;
// mc++ import java.util.List;
// mc++ import javax.microedition.khronos.egl.EGLConfig;
// mc++ import javax.microedition.khronos.opengles.GL10;
// mc++ 
// mc++ /** This is a simple example that demonstrates CPU image access with ARCore. */
// mc++ public class ComputerVisionActivity extends AppCompatActivity implements GLSurfaceView.Renderer {
// mc++   private static final String TAG = ComputerVisionActivity.class.getSimpleName();
// mc++   private static final String CAMERA_INTRINSICS_TEXT_FORMAT =
// mc++       "Unrotated Camera %s %s Intrinsics:\n\tFocal Length: (%.2f, %.2f)"
// mc++           + "\n\tPrincipal Point: (%.2f, %.2f)"
// mc++           + "\n\t%s Image Dimensions: (%d, %d)"
// mc++           + "\n\tUnrotated Field of View: (%.2f˚, %.2f˚)"
// mc++           + "\n\tRender frame time: %.1f ms (%.0ffps)"
// mc++           + "\n\tCPU image frame time: %.1f ms (%.0ffps)";
// mc++   private static final float RADIANS_TO_DEGREES = (float) (180 / Math.PI);
// mc++ 
// mc++   // This app demonstrates two approaches to obtaining image data accessible on CPU:
// mc++   // 1. Access the CPU image directly from ARCore. This approach delivers a frame without latency
// mc++   //    (if available), but currently is lower resolution than the GPU image.
// mc++   // 2. Download the texture from GPU. This approach incurs a 1-frame latency, but allows a high
// mc++   //    resolution image.
// mc++   private enum ImageAcquisitionPath {
// mc++     CPU_DIRECT_ACCESS,
// mc++     GPU_DOWNLOAD
// mc++   }
// mc++ 
// mc++   // Select the image acquisition path here.
// mc++   private final ImageAcquisitionPath imageAcquisitionPath = ImageAcquisitionPath.CPU_DIRECT_ACCESS;
// mc++ 
// mc++   // Session management and rendering.
// mc++   private GLSurfaceView surfaceView;
// mc++   private Session session;
// mc++   private Config config;
// mc++   private boolean installRequested;
// mc++   private final SnackbarHelper messageSnackbarHelper = new SnackbarHelper();
// mc++   private CpuImageDisplayRotationHelper cpuImageDisplayRotationHelper;
// mc++   private final CpuImageRenderer cpuImageRenderer = new CpuImageRenderer();
// mc++   private final EdgeDetector edgeDetector = new EdgeDetector();
// mc++   private GestureDetector gestureDetector;
// mc++ 
// mc++   // This lock prevents changing resolution as the frame is being rendered. ARCore requires all
// mc++   // CPU images to be released before changing resolution.
// mc++   private final Object frameImageInUseLock = new Object();
// mc++ 
// mc++   // Camera intrinsics text view.
// mc++   private TextView cameraIntrinsicsTextView;
// mc++ 
// mc++   // The fields below are used for the GPU_DOWNLOAD image acquisition path.
// mc++   private final TextureReader textureReader = new TextureReader();
// mc++   private int gpuDownloadFrameBufferIndex = -1;
// mc++ 
// mc++   // ARCore full resolution GL texture typically has a size of 1920 x 1080.
// mc++   private static final int TEXTURE_WIDTH = 1920;
// mc++   private static final int TEXTURE_HEIGHT = 1080;
// mc++ 
// mc++   // We choose a lower sampling resolution.
// mc++   private static final int IMAGE_WIDTH = 1280;
// mc++   private static final int IMAGE_HEIGHT = 720;
// mc++ 
// mc++   // For Camera Configuration APIs usage.
// mc++   private boolean isLowResolutionSelected;
// mc++   private CameraConfig cpuLowResolutionCameraConfig;
// mc++   private CameraConfig cpuHighResolutionCameraConfig;
// mc++ 
// mc++   private Switch focusModeSwitch;
// mc++ 
// mc++   private final FrameTimeHelper renderFrameTimeHelper = new FrameTimeHelper();
// mc++   private final FrameTimeHelper cpuImageFrameTimeHelper = new FrameTimeHelper();
// mc++ 
// mc++   @Override
// mc++   protected void onCreate(Bundle savedInstanceState) {
// mc++     super.onCreate(savedInstanceState);
// mc++     setContentView(R.layout.activity_main);
// mc++     surfaceView = findViewById(R.id.surfaceview);
// mc++     cameraIntrinsicsTextView = findViewById(R.id.camera_intrinsics_view);
// mc++     surfaceView = findViewById(R.id.surfaceview);
// mc++     focusModeSwitch = (Switch) findViewById(R.id.switch_focus_mode);
// mc++     focusModeSwitch.setOnCheckedChangeListener(this::onFocusModeChanged);
// mc++ 
// mc++     cpuImageDisplayRotationHelper = new CpuImageDisplayRotationHelper(/*context=*/ this);
// mc++ 
// mc++     gestureDetector =
// mc++         new GestureDetector(
// mc++             this,
// mc++             new GestureDetector.SimpleOnGestureListener() {
// mc++               @Override
// mc++               public boolean onSingleTapUp(MotionEvent e) {
// mc++                 float newPosition = (cpuImageRenderer.getSplitterPosition() < 0.5f) ? 1.0f : 0.0f;
// mc++                 cpuImageRenderer.setSplitterPosition(newPosition);
// mc++ 
// mc++                 // Display the CPU resolution related UI only when CPU image is being displayed.
// mc++                 boolean show = (newPosition < 0.5f);
// mc++                 RadioGroup radioGroup = (RadioGroup) findViewById(R.id.radio_camera_configs);
// mc++                 radioGroup.setVisibility(show ? View.VISIBLE : View.INVISIBLE);
// mc++                 return true;
// mc++               }
// mc++ 
// mc++               @Override
// mc++               public boolean onDown(MotionEvent e) {
// mc++                 return true;
// mc++               }
// mc++             });
// mc++ 
// mc++     // Setup a touch listener to control the texture splitter position.
// mc++     surfaceView.setOnTouchListener((unusedView, event) -> gestureDetector.onTouchEvent(event));
// mc++ 
// mc++     // Set up renderer.
// mc++     surfaceView.setPreserveEGLContextOnPause(true);
// mc++     surfaceView.setEGLContextClientVersion(2);
// mc++     surfaceView.setEGLConfigChooser(8, 8, 8, 8, 16, 0); // Alpha used for plane blending.
// mc++     surfaceView.setRenderer(this);
// mc++     surfaceView.setRenderMode(GLSurfaceView.RENDERMODE_CONTINUOUSLY);
// mc++     surfaceView.setWillNotDraw(false);
// mc++ 
// mc++     getLifecycle().addObserver(renderFrameTimeHelper);
// mc++     getLifecycle().addObserver(cpuImageFrameTimeHelper);
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
// mc++         session = new Session(/* context= */ this);
// mc++         config = new Config(session);
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
// mc++     }
// mc++ 
// mc++     obtainCameraConfigs();
// mc++ 
// mc++     focusModeSwitch.setChecked(config.getFocusMode() != Config.FocusMode.FIXED);
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
// mc++     cpuImageDisplayRotationHelper.onResume();
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onPause() {
// mc++     super.onPause();
// mc++     if (session != null) {
// mc++       // Note that the order matters - GLSurfaceView is paused first so that it does not try
// mc++       // to query the session. If Session is paused before GLSurfaceView, GLSurfaceView may
// mc++       // still call session.update() and get a SessionPausedException.
// mc++       cpuImageDisplayRotationHelper.onPause();
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
// mc++   @Override
// mc++   public void onSurfaceCreated(GL10 gl, EGLConfig config) {
// mc++     GLES20.glClearColor(0.1f, 0.1f, 0.1f, 1.0f);
// mc++ 
// mc++     // Create the texture and pass it to ARCore session to be filled during update().
// mc++     try {
// mc++       cpuImageRenderer.createOnGlThread(/* context= */ this);
// mc++ 
// mc++       // The image format can be either IMAGE_FORMAT_RGBA or IMAGE_FORMAT_I8.
// mc++       // Set keepAspectRatio to false so that the output image covers the whole viewport.
// mc++       textureReader.create(
// mc++           /* context= */ this,
// mc++           TextureReaderImage.IMAGE_FORMAT_I8,
// mc++           IMAGE_WIDTH,
// mc++           IMAGE_HEIGHT,
// mc++           false);
// mc++ 
// mc++     } catch (IOException e) {
// mc++       Log.e(TAG, "Failed to read an asset file", e);
// mc++     }
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onSurfaceChanged(GL10 gl, int width, int height) {
// mc++     cpuImageDisplayRotationHelper.onSurfaceChanged(width, height);
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
// mc++     cpuImageDisplayRotationHelper.updateSessionIfNeeded(session);
// mc++ 
// mc++     try {
// mc++       session.setCameraTextureName(cpuImageRenderer.getTextureId());
// mc++       final Frame frame = session.update();
// mc++ 
// mc++       renderFrameTimeHelper.nextFrame();
// mc++ 
// mc++       switch (imageAcquisitionPath) {
// mc++         case CPU_DIRECT_ACCESS:
// mc++           renderProcessedImageCpuDirectAccess(frame);
// mc++           break;
// mc++         case GPU_DOWNLOAD:
// mc++           renderProcessedImageGpuDownload(frame);
// mc++           break;
// mc++       }
// mc++ 
// mc++       // Update the camera intrinsics' text.
// mc++       runOnUiThread(() -> cameraIntrinsicsTextView.setText(getCameraIntrinsicsText(frame)));
// mc++     } catch (Exception t) {
// mc++       // Avoid crashing the application due to unhandled exceptions.
// mc++       Log.e(TAG, "Exception on the OpenGL thread", t);
// mc++     }
// mc++   }
// mc++ 
// mc++   /* Demonstrates how to access a CPU image directly from ARCore. */
// mc++   private void renderProcessedImageCpuDirectAccess(Frame frame) {
// mc++     // Lock the image use to avoid pausing & resuming session when the image is in use. This is
// mc++     // because switching resolutions requires all images to be released before session.resume() is
// mc++     // called.
// mc++     synchronized (frameImageInUseLock) {
// mc++       try (Image image = frame.acquireCameraImage()) {
// mc++         if (image.getFormat() != ImageFormat.YUV_420_888) {
// mc++           throw new IllegalArgumentException(
// mc++               "Expected image in YUV_420_888 format, got format " + image.getFormat());
// mc++         }
// mc++ 
// mc++         ByteBuffer processedImageBytesGrayscale =
// mc++             edgeDetector.detect(
// mc++                 image.getWidth(),
// mc++                 image.getHeight(),
// mc++                 image.getPlanes()[0].getRowStride(),
// mc++                 image.getPlanes()[0].getBuffer());
// mc++ 
// mc++         cpuImageRenderer.drawWithCpuImage(
// mc++             frame,
// mc++             image.getWidth(),
// mc++             image.getHeight(),
// mc++             processedImageBytesGrayscale,
// mc++             cpuImageDisplayRotationHelper.getViewportAspectRatio(),
// mc++             cpuImageDisplayRotationHelper.getCameraToDisplayRotation());
// mc++ 
// mc++         // Measure frame time since last successful execution of drawWithCpuImage().
// mc++         cpuImageFrameTimeHelper.nextFrame();
// mc++       } catch (NotYetAvailableException e) {
// mc++         // This exception will routinely happen during startup, and is expected. cpuImageRenderer
// mc++         // will handle null image properly, and will just render the background.
// mc++         cpuImageRenderer.drawWithoutCpuImage();
// mc++       }
// mc++     }
// mc++   }
// mc++ 
// mc++   /* Demonstrates how to access a CPU image using a download from GPU. */
// mc++   private void renderProcessedImageGpuDownload(Frame frame) {
// mc++     // If there is a frame being requested previously, acquire the pixels and process it.
// mc++     if (gpuDownloadFrameBufferIndex >= 0) {
// mc++       TextureReaderImage image = textureReader.acquireFrame(gpuDownloadFrameBufferIndex);
// mc++ 
// mc++       if (image.format != TextureReaderImage.IMAGE_FORMAT_I8) {
// mc++         throw new IllegalArgumentException(
// mc++             "Expected image in I8 format, got format " + image.format);
// mc++       }
// mc++ 
// mc++       ByteBuffer processedImageBytesGrayscale =
// mc++           edgeDetector.detect(image.width, image.height, /* stride= */ image.width, image.buffer);
// mc++ 
// mc++       // You should always release frame buffer after using. Otherwise the next call to
// mc++       // submitFrame() may fail.
// mc++       textureReader.releaseFrame(gpuDownloadFrameBufferIndex);
// mc++ 
// mc++       cpuImageRenderer.drawWithCpuImage(
// mc++           frame,
// mc++           IMAGE_WIDTH,
// mc++           IMAGE_HEIGHT,
// mc++           processedImageBytesGrayscale,
// mc++           cpuImageDisplayRotationHelper.getViewportAspectRatio(),
// mc++           cpuImageDisplayRotationHelper.getCameraToDisplayRotation());
// mc++ 
// mc++       // Measure frame time since last successful execution of drawWithCpuImage().
// mc++       cpuImageFrameTimeHelper.nextFrame();
// mc++     } else {
// mc++       cpuImageRenderer.drawWithoutCpuImage();
// mc++     }
// mc++ 
// mc++     // Submit request for the texture from the current frame.
// mc++     gpuDownloadFrameBufferIndex =
// mc++         textureReader.submitFrame(cpuImageRenderer.getTextureId(), TEXTURE_WIDTH, TEXTURE_HEIGHT);
// mc++   }
// mc++ 
// mc++   public void onLowResolutionRadioButtonClicked(View view) {
// mc++     boolean checked = ((RadioButton) view).isChecked();
// mc++     if (checked && !isLowResolutionSelected) {
// mc++       // Display low resolution.
// mc++       onCameraConfigChanged(cpuLowResolutionCameraConfig);
// mc++       isLowResolutionSelected = true;
// mc++     }
// mc++   }
// mc++ 
// mc++   public void onHighResolutionRadioButtonClicked(View view) {
// mc++     boolean checked = ((RadioButton) view).isChecked();
// mc++     if (checked && isLowResolutionSelected) {
// mc++       // Display high resolution.
// mc++       onCameraConfigChanged(cpuHighResolutionCameraConfig);
// mc++       isLowResolutionSelected = false;
// mc++     }
// mc++   }
// mc++ 
// mc++   private void onFocusModeChanged(CompoundButton unusedButton, boolean isChecked) {
// mc++     config.setFocusMode(isChecked ? Config.FocusMode.AUTO : Config.FocusMode.FIXED);
// mc++     session.configure(config);
// mc++   }
// mc++ 
// mc++   private void onCameraConfigChanged(CameraConfig cameraConfig) {
// mc++     // To change the AR camera config - first we pause the AR session, set the desired camera
// mc++     // config and then resume the AR session.
// mc++     if (session != null) {
// mc++       // Block here if the image is still being used.
// mc++       synchronized (frameImageInUseLock) {
// mc++         session.pause();
// mc++         session.setCameraConfig(cameraConfig);
// mc++         try {
// mc++           session.resume();
// mc++         } catch (CameraNotAvailableException ex) {
// mc++           // In a rare case (such as another camera app launching) the camera may be given to a
// mc++           // different app and so may not be available to this app. Handle this properly by showing
// mc++           // a message and recreate the session at the next iteration.
// mc++           messageSnackbarHelper.showError(this, "Camera not available. Please restart the app.");
// mc++           session = null;
// mc++           return;
// mc++         }
// mc++       }
// mc++ 
// mc++       // Let the user know that the camera config is set.
// mc++       String toastMessage =
// mc++           "Set the camera config with CPU image resolution of "
// mc++               + cameraConfig.getImageSize().getWidth()
// mc++               + "x"
// mc++               + cameraConfig.getImageSize().getHeight()
// mc++               + ".";
// mc++       Toast.makeText(this, toastMessage, Toast.LENGTH_LONG).show();
// mc++     }
// mc++   }
// mc++ 
// mc++   // Obtains the supported camera configs and build the list of radio button one for each camera
// mc++   // config.
// mc++   private void obtainCameraConfigs() {
// mc++     // First obtain the session handle before getting the list of various camera configs.
// mc++     if (session != null) {
// mc++       List<CameraConfig> cameraConfigs = session.getSupportedCameraConfigs();
// mc++ 
// mc++       // Determine the highest and lowest CPU resolutions.
// mc++       cpuLowResolutionCameraConfig =
// mc++           getCameraConfigWithLowestOrHighestResolution(cameraConfigs, true);
// mc++       cpuHighResolutionCameraConfig =
// mc++           getCameraConfigWithLowestOrHighestResolution(cameraConfigs, false);
// mc++ 
// mc++       // Update the radio buttons with the resolution info.
// mc++       updateRadioButtonText(
// mc++           R.id.radio_low_res, cpuLowResolutionCameraConfig, getString(R.string.label_low_res));
// mc++       updateRadioButtonText(
// mc++           R.id.radio_high_res, cpuHighResolutionCameraConfig, getString(R.string.label_high_res));
// mc++       isLowResolutionSelected = true;
// mc++     }
// mc++   }
// mc++ 
// mc++   private void updateRadioButtonText(int id, CameraConfig cameraConfig, String prefix) {
// mc++     RadioButton radioButton = (RadioButton) findViewById(id);
// mc++     Size resolution = cameraConfig.getImageSize();
// mc++     radioButton.setText(prefix + " (" + resolution.getWidth() + "x" + resolution.getHeight() + ")");
// mc++   }
// mc++ 
// mc++   private CameraConfig getCameraConfigWithLowestOrHighestResolution(
// mc++       List<CameraConfig> cameraConfigs, boolean lowest) {
// mc++     CameraConfig cameraConfig = cameraConfigs.get(0);
// mc++     for (int index = 1; index < cameraConfigs.size(); index++) {
// mc++       if (lowest) {
// mc++         if (cameraConfigs.get(index).getImageSize().getHeight()
// mc++             < cameraConfig.getImageSize().getHeight()) {
// mc++           cameraConfig = cameraConfigs.get(index);
// mc++         }
// mc++       } else {
// mc++         if (cameraConfigs.get(index).getImageSize().getHeight()
// mc++             > cameraConfig.getImageSize().getHeight()) {
// mc++           cameraConfig = cameraConfigs.get(index);
// mc++         }
// mc++       }
// mc++     }
// mc++     return cameraConfig;
// mc++   }
// mc++ 
// mc++   private String getCameraIntrinsicsText(Frame frame) {
// mc++     Camera camera = frame.getCamera();
// mc++ 
// mc++     boolean forGpuTexture = (cpuImageRenderer.getSplitterPosition() > 0.5f);
// mc++     CameraIntrinsics intrinsics =
// mc++         forGpuTexture ? camera.getTextureIntrinsics() : camera.getImageIntrinsics();
// mc++     String intrinsicsLabel = forGpuTexture ? "Texture" : "Image";
// mc++     String imageType = forGpuTexture ? "GPU" : "CPU";
// mc++ 
// mc++     float[] focalLength = intrinsics.getFocalLength();
// mc++     float[] principalPoint = intrinsics.getPrincipalPoint();
// mc++     int[] imageSize = intrinsics.getImageDimensions();
// mc++ 
// mc++     float fovX = (float) (2 * Math.atan2((double) imageSize[0], (double) (2 * focalLength[0])));
// mc++     float fovY = (float) (2 * Math.atan2((double) imageSize[1], (double) (2 * focalLength[1])));
// mc++     fovX *= RADIANS_TO_DEGREES;
// mc++     fovY *= RADIANS_TO_DEGREES;
// mc++ 
// mc++     return String.format(
// mc++         CAMERA_INTRINSICS_TEXT_FORMAT,
// mc++         imageType,
// mc++         intrinsicsLabel,
// mc++         focalLength[0],
// mc++         focalLength[1],
// mc++         principalPoint[0],
// mc++         principalPoint[1],
// mc++         imageType,
// mc++         imageSize[0],
// mc++         imageSize[1],
// mc++         fovX,
// mc++         fovY,
// mc++         renderFrameTimeHelper.getSmoothedFrameTime(),
// mc++         renderFrameTimeHelper.getSmoothedFrameRate(),
// mc++         cpuImageFrameTimeHelper.getSmoothedFrameTime(),
// mc++         cpuImageFrameTimeHelper.getSmoothedFrameRate());
// mc++   }
// mc++ }
