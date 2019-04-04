// mc++ /*
// mc++  * Copyright 2017 Google Inc. All Rights Reserved.
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
// mc++ package com.google.ar.core.examples.java.helloar;
// mc++ 
// mc++ import android.opengl.GLES20;
// mc++ import android.opengl.GLSurfaceView;
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.util.Log;
// mc++ import android.view.MotionEvent;
// mc++ import android.widget.Toast;
// mc++ import com.google.ar.core.Anchor;
// mc++ import com.google.ar.core.ArCoreApk;
// mc++ import com.google.ar.core.Camera;
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
// mc++ import com.google.ar.core.examples.java.common.helpers.TapHelper;
// mc++ import com.google.ar.core.examples.java.common.rendering.BackgroundRenderer;
// mc++ import com.google.ar.core.examples.java.common.rendering.ObjectRenderer;
// mc++ import com.google.ar.core.examples.java.common.rendering.ObjectRenderer.BlendMode;
// mc++ import com.google.ar.core.examples.java.common.rendering.PlaneRenderer;
// mc++ import com.google.ar.core.examples.java.common.rendering.PointCloudRenderer;
// mc++ import com.google.ar.core.exceptions.CameraNotAvailableException;
// mc++ import com.google.ar.core.exceptions.UnavailableApkTooOldException;
// mc++ import com.google.ar.core.exceptions.UnavailableArcoreNotInstalledException;
// mc++ import com.google.ar.core.exceptions.UnavailableDeviceNotCompatibleException;
// mc++ import com.google.ar.core.exceptions.UnavailableSdkTooOldException;
// mc++ import com.google.ar.core.exceptions.UnavailableUserDeclinedInstallationException;
// mc++ import java.io.IOException;
// mc++ import java.util.ArrayList;
// mc++ import javax.microedition.khronos.egl.EGLConfig;
// mc++ import javax.microedition.khronos.opengles.GL10;
// mc++ 
// mc++ /**
// mc++  * This is a simple example that shows how to create an augmented reality (AR) application using the
// mc++  * ARCore API. The application will display any detected planes and will allow the user to tap on a
// mc++  * plane to place a 3d model of the Android robot.
// mc++  */
// mc++ public class HelloArActivity extends AppCompatActivity implements GLSurfaceView.Renderer {
// mc++   private static final String TAG = HelloArActivity.class.getSimpleName();
// mc++ 
// mc++   // Rendering. The Renderers are created here, and initialized when the GL surface is created.
// mc++   private GLSurfaceView surfaceView;
// mc++ 
// mc++   private boolean installRequested;
// mc++ 
// mc++   private Session session;
// mc++   private final SnackbarHelper messageSnackbarHelper = new SnackbarHelper();
// mc++   private DisplayRotationHelper displayRotationHelper;
// mc++   private TapHelper tapHelper;
// mc++ 
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
// mc++   private static final String SEARCHING_PLANE_MESSAGE = "Searching for surfaces...";
// mc++ 
// mc++   // Anchors created from taps used for object placing with a given color.
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
// mc++   private final ArrayList<ColoredAnchor> anchors = new ArrayList<>();
// mc++ 
// mc++   @Override
// mc++   protected void onCreate(Bundle savedInstanceState) {
// mc++     super.onCreate(savedInstanceState);
// mc++     setContentView(R.layout.activity_main);
// mc++     surfaceView = findViewById(R.id.surfaceview);
// mc++     displayRotationHelper = new DisplayRotationHelper(/*context=*/ this);
// mc++ 
// mc++     // Set up tap listener.
// mc++     tapHelper = new TapHelper(/*context=*/ this);
// mc++     surfaceView.setOnTouchListener(tapHelper);
// mc++ 
// mc++     // Set up renderer.
// mc++     surfaceView.setPreserveEGLContextOnPause(true);
// mc++     surfaceView.setEGLContextClientVersion(2);
// mc++     surfaceView.setEGLConfigChooser(8, 8, 8, 8, 16, 0); // Alpha used for plane blending.
// mc++     surfaceView.setRenderer(this);
// mc++     surfaceView.setRenderMode(GLSurfaceView.RENDERMODE_CONTINUOUSLY);
// mc++     surfaceView.setWillNotDraw(false);
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
// mc++         // Create the session.
// mc++         session = new Session(/* context= */ this);
// mc++ 
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
// mc++       } catch (UnavailableDeviceNotCompatibleException e) {
// mc++         message = "This device does not support AR";
// mc++         exception = e;
// mc++       } catch (Exception e) {
// mc++         message = "Failed to create AR session";
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
// mc++ 
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
// mc++   @Override
// mc++   public void onSurfaceCreated(GL10 gl, EGLConfig config) {
// mc++     GLES20.glClearColor(0.1f, 0.1f, 0.1f, 1.0f);
// mc++ 
// mc++     // Prepare the rendering objects. This involves reading shaders, so may throw an IOException.
// mc++     try {
// mc++       // Create the texture and pass it to ARCore session to be filled during update().
// mc++       backgroundRenderer.createOnGlThread(/*context=*/ this);
// mc++       planeRenderer.createOnGlThread(/*context=*/ this, "models/trigrid.png");
// mc++       pointCloudRenderer.createOnGlThread(/*context=*/ this);
// mc++ 
// mc++       virtualObject.createOnGlThread(/*context=*/ this, "models/andy.obj", "models/andy.png");
// mc++       virtualObject.setMaterialProperties(0.0f, 2.0f, 0.5f, 6.0f);
// mc++ 
// mc++       virtualObjectShadow.createOnGlThread(
// mc++           /*context=*/ this, "models/andy_shadow.obj", "models/andy_shadow.png");
// mc++       virtualObjectShadow.setBlendMode(BlendMode.Shadow);
// mc++       virtualObjectShadow.setMaterialProperties(1.0f, 0.0f, 0.0f, 1.0f);
// mc++ 
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
// mc++       // Handle one tap per frame.
// mc++       handleTap(frame, camera);
// mc++ 
// mc++       // If frame is ready, render camera preview image to the GL surface.
// mc++       backgroundRenderer.draw(frame);
// mc++ 
// mc++       // If not tracking, don't draw 3D objects, show tracking failure reason instead.
// mc++       if (camera.getTrackingState() == TrackingState.PAUSED) {
// mc++         messageSnackbarHelper.showMessage(
// mc++             this, TrackingStateHelper.getTrackingFailureReasonString(camera));
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
// mc++       // The first three components are color scaling factors.
// mc++       // The last one is the average pixel intensity in gamma space.
// mc++       final float[] colorCorrectionRgba = new float[4];
// mc++       frame.getLightEstimate().getColorCorrection(colorCorrectionRgba, 0);
// mc++ 
// mc++       // Visualize tracked points.
// mc++       // Use try-with-resources to automatically release the point cloud.
// mc++       try (PointCloud pointCloud = frame.acquirePointCloud()) {
// mc++         pointCloudRenderer.update(pointCloud);
// mc++         pointCloudRenderer.draw(viewmtx, projmtx);
// mc++       }
// mc++ 
// mc++       // No tracking error at this point. If we detected any plane, then hide the
// mc++       // message UI, otherwise show searchingPlane message.
// mc++       if (hasTrackingPlane()) {
// mc++         messageSnackbarHelper.hide(this);
// mc++       } else {
// mc++         messageSnackbarHelper.showMessage(this, SEARCHING_PLANE_MESSAGE);
// mc++       }
// mc++ 
// mc++       // Visualize planes.
// mc++       planeRenderer.drawPlanes(
// mc++           session.getAllTrackables(Plane.class), camera.getDisplayOrientedPose(), projmtx);
// mc++ 
// mc++       // Visualize anchors created by touch.
// mc++       float scaleFactor = 1.0f;
// mc++       for (ColoredAnchor coloredAnchor : anchors) {
// mc++         if (coloredAnchor.anchor.getTrackingState() != TrackingState.TRACKING) {
// mc++           continue;
// mc++         }
// mc++         // Get the current pose of an Anchor in world space. The Anchor pose is updated
// mc++         // during calls to session.update() as ARCore refines its estimate of the world.
// mc++         coloredAnchor.anchor.getPose().toMatrix(anchorMatrix, 0);
// mc++ 
// mc++         // Update and draw the model and its shadow.
// mc++         virtualObject.updateModelMatrix(anchorMatrix, scaleFactor);
// mc++         virtualObjectShadow.updateModelMatrix(anchorMatrix, scaleFactor);
// mc++         virtualObject.draw(viewmtx, projmtx, colorCorrectionRgba, coloredAnchor.color);
// mc++         virtualObjectShadow.draw(viewmtx, projmtx, colorCorrectionRgba, coloredAnchor.color);
// mc++       }
// mc++ 
// mc++     } catch (Throwable t) {
// mc++       // Avoid crashing the application due to unhandled exceptions.
// mc++       Log.e(TAG, "Exception on the OpenGL thread", t);
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
// mc++   /** Checks if we detected at least one plane. */
// mc++   private boolean hasTrackingPlane() {
// mc++     for (Plane plane : session.getAllTrackables(Plane.class)) {
// mc++       if (plane.getTrackingState() == TrackingState.TRACKING) {
// mc++         return true;
// mc++       }
// mc++     }
// mc++     return false;
// mc++   }
// mc++ }
