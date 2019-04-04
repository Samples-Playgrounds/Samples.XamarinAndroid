// mc++ /*
// mc++  * Copyright 2017 Google Inc. All Rights Reserved.
// mc++  * Licensed under the Apache License, Version 2.0 (the "License");
// mc++  * you may not use this file except in compliance with the License.
// mc++  * You may obtain a copy of the License at
// mc++  *
// mc++  *   http://www.apache.org/licenses/LICENSE-2.0
// mc++  *
// mc++  * Unless required by applicable law or agreed to in writing, software
// mc++  * distributed under the License is distributed on an "AS IS" BASIS,
// mc++  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// mc++  * See the License for the specific language governing permissions and
// mc++  * limitations under the License.
// mc++  */
// mc++ package com.google.ar.core.examples.java.common.helpers;
// mc++ 
// mc++ import android.app.Activity;
// mc++ import android.content.Context;
// mc++ import android.hardware.camera2.CameraAccessException;
// mc++ import android.hardware.camera2.CameraCharacteristics;
// mc++ import android.hardware.camera2.CameraManager;
// mc++ import android.hardware.display.DisplayManager;
// mc++ import android.hardware.display.DisplayManager.DisplayListener;
// mc++ import android.view.Display;
// mc++ import android.view.Surface;
// mc++ import android.view.WindowManager;
// mc++ import com.google.ar.core.Session;
// mc++ 
// mc++ /**
// mc++  * Helper to track the display rotations. In particular, the 180 degree rotations are not notified
// mc++  * by the onSurfaceChanged() callback, and thus they require listening to the android display
// mc++  * events.
// mc++  */
// mc++ public final class DisplayRotationHelper implements DisplayListener {
// mc++   private boolean viewportChanged;
// mc++   private int viewportWidth;
// mc++   private int viewportHeight;
// mc++   private final Display display;
// mc++   private final DisplayManager displayManager;
// mc++   private final CameraManager cameraManager;
// mc++ 
// mc++   /**
// mc++    * Constructs the DisplayRotationHelper but does not register the listener yet.
// mc++    *
// mc++    * @param context the Android {@link Context}.
// mc++    */
// mc++   public DisplayRotationHelper(Context context) {
// mc++     displayManager = (DisplayManager) context.getSystemService(Context.DISPLAY_SERVICE);
// mc++     cameraManager = (CameraManager) context.getSystemService(Context.CAMERA_SERVICE);
// mc++     WindowManager windowManager = (WindowManager) context.getSystemService(Context.WINDOW_SERVICE);
// mc++     display = windowManager.getDefaultDisplay();
// mc++   }
// mc++ 
// mc++   /** Registers the display listener. Should be called from {@link Activity#onResume()}. */
// mc++   public void onResume() {
// mc++     displayManager.registerDisplayListener(this, null);
// mc++   }
// mc++ 
// mc++   /** Unregisters the display listener. Should be called from {@link Activity#onPause()}. */
// mc++   public void onPause() {
// mc++     displayManager.unregisterDisplayListener(this);
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Records a change in surface dimensions. This will be later used by {@link
// mc++    * #updateSessionIfNeeded(Session)}. Should be called from {@link
// mc++    * android.opengl.GLSurfaceView.Renderer
// mc++    * #onSurfaceChanged(javax.microedition.khronos.opengles.GL10, int, int)}.
// mc++    *
// mc++    * @param width the updated width of the surface.
// mc++    * @param height the updated height of the surface.
// mc++    */
// mc++   public void onSurfaceChanged(int width, int height) {
// mc++     viewportWidth = width;
// mc++     viewportHeight = height;
// mc++     viewportChanged = true;
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Updates the session display geometry if a change was posted either by {@link
// mc++    * #onSurfaceChanged(int, int)} call or by {@link #onDisplayChanged(int)} system callback. This
// mc++    * function should be called explicitly before each call to {@link Session#update()}. This
// mc++    * function will also clear the 'pending update' (viewportChanged) flag.
// mc++    *
// mc++    * @param session the {@link Session} object to update if display geometry changed.
// mc++    */
// mc++   public void updateSessionIfNeeded(Session session) {
// mc++     if (viewportChanged) {
// mc++       int displayRotation = display.getRotation();
// mc++       session.setDisplayGeometry(displayRotation, viewportWidth, viewportHeight);
// mc++       viewportChanged = false;
// mc++     }
// mc++   }
// mc++ 
// mc++   /**
// mc++    *  Returns the aspect ratio of the GL surface viewport while accounting for the display rotation
// mc++    *  relative to the device camera sensor orientation.
// mc++    */
// mc++   public float getCameraSensorRelativeViewportAspectRatio(String cameraId) {
// mc++     float aspectRatio;
// mc++     int cameraSensorToDisplayRotation = getCameraSensorToDisplayRotation(cameraId);
// mc++     switch (cameraSensorToDisplayRotation) {
// mc++       case 90:
// mc++       case 270:
// mc++         aspectRatio = (float) viewportHeight / (float) viewportWidth;
// mc++         break;
// mc++       case 0:
// mc++       case 180:
// mc++         aspectRatio = (float) viewportWidth / (float) viewportHeight;
// mc++         break;
// mc++       default:
// mc++         throw new RuntimeException("Unhandled rotation: " + cameraSensorToDisplayRotation);
// mc++     }
// mc++     return aspectRatio;
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Returns the rotation of the back-facing camera with respect to the display. The value is one of
// mc++    * 0, 90, 180, 270.
// mc++    */
// mc++   public int getCameraSensorToDisplayRotation(String cameraId) {
// mc++     CameraCharacteristics characteristics;
// mc++     try {
// mc++       characteristics = cameraManager.getCameraCharacteristics(cameraId);
// mc++     } catch (CameraAccessException e) {
// mc++       throw new RuntimeException("Unable to determine display orientation", e);
// mc++     }
// mc++ 
// mc++     // Camera sensor orientation.
// mc++     int sensorOrientation = characteristics.get(CameraCharacteristics.SENSOR_ORIENTATION);
// mc++ 
// mc++     // Current display orientation.
// mc++     int displayOrientation = toDegrees(display.getRotation());
// mc++ 
// mc++     // Make sure we return 0, 90, 180, or 270 degrees.
// mc++     return (sensorOrientation - displayOrientation + 360) % 360;
// mc++   }
// mc++ 
// mc++   private int toDegrees(int rotation) {
// mc++     switch (rotation) {
// mc++       case Surface.ROTATION_0:
// mc++         return 0;
// mc++       case Surface.ROTATION_90:
// mc++         return 90;
// mc++       case Surface.ROTATION_180:
// mc++         return 180;
// mc++       case Surface.ROTATION_270:
// mc++         return 270;
// mc++       default:
// mc++         throw new RuntimeException("Unknown rotation " + rotation);
// mc++     }
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onDisplayAdded(int displayId) {}
// mc++ 
// mc++   @Override
// mc++   public void onDisplayRemoved(int displayId) {}
// mc++ 
// mc++   @Override
// mc++   public void onDisplayChanged(int displayId) {
// mc++     viewportChanged = true;
// mc++   }
// mc++ }
