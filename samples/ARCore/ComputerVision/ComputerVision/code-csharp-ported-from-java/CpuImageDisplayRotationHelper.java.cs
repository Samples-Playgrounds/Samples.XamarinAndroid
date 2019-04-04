// mc++ /*
// mc++  * Copyright 2018 Google Inc. All Rights Reserved.
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
// mc++ package com.google.ar.core.examples.java.computervision;
// mc++ 
// mc++ import android.app.Activity;
// mc++ import android.content.Context;
// mc++ import android.hardware.Camera;
// mc++ import android.hardware.Camera.CameraInfo;
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
// mc++ public class CpuImageDisplayRotationHelper implements DisplayListener {
// mc++   private boolean viewportChanged;
// mc++   private int viewportWidth;
// mc++   private int viewportHeight;
// mc++   private final Context context;
// mc++   private final Display display;
// mc++ 
// mc++   /**
// mc++    * Constructs the CpuImageDisplayRotationHelper but does not register the listener yet.
// mc++    *
// mc++    * @param context the Android {@link Context}.
// mc++    */
// mc++   public CpuImageDisplayRotationHelper(Context context) {
// mc++     this.context = context;
// mc++     display = context.getSystemService(WindowManager.class).getDefaultDisplay();
// mc++   }
// mc++ 
// mc++   /** Registers the display listener. Should be called from {@link Activity#onResume()}. */
// mc++   public void onResume() {
// mc++     context.getSystemService(DisplayManager.class).registerDisplayListener(this, null);
// mc++   }
// mc++ 
// mc++   /** Unregisters the display listener. Should be called from {@link Activity#onPause()}. */
// mc++   public void onPause() {
// mc++     context.getSystemService(DisplayManager.class).unregisterDisplayListener(this);
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
// mc++    * Returns the current rotation state of android display. Same as {@link Display#getRotation()}.
// mc++    */
// mc++   public int getRotation() {
// mc++     return display.getRotation();
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Returns the aspect ratio of viewport.
// mc++    */
// mc++   public float getViewportAspectRatio() {
// mc++     float aspectRatio;
// mc++     switch (getCameraToDisplayRotation()) {
// mc++       case Surface.ROTATION_90:
// mc++       case Surface.ROTATION_270:
// mc++         aspectRatio = (float) viewportHeight / (float) viewportWidth;
// mc++         break;
// mc++       case Surface.ROTATION_0:
// mc++       case Surface.ROTATION_180:
// mc++       default:
// mc++         aspectRatio = (float) viewportWidth / (float) viewportHeight;
// mc++         break;
// mc++     }
// mc++     return aspectRatio;
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Returns the rotation of the back-facing camera with respect to the display. The value is one of
// mc++    * android.view.Surface.ROTATION_#(0, 90, 180, 270).
// mc++    */
// mc++   public int getCameraToDisplayRotation() {
// mc++     // Get screen to device rotation in degress.
// mc++     int screenDegrees = 0;
// mc++     switch (getRotation()) {
// mc++       case Surface.ROTATION_0:
// mc++         screenDegrees = 0;
// mc++         break;
// mc++       case Surface.ROTATION_90:
// mc++         screenDegrees = 90;
// mc++         break;
// mc++       case Surface.ROTATION_180:
// mc++         screenDegrees = 180;
// mc++         break;
// mc++       case Surface.ROTATION_270:
// mc++         screenDegrees = 270;
// mc++         break;
// mc++       default:
// mc++         break;
// mc++     }
// mc++ 
// mc++     CameraInfo cameraInfo = new CameraInfo();
// mc++     Camera.getCameraInfo(CameraInfo.CAMERA_FACING_BACK, cameraInfo);
// mc++ 
// mc++     int cameraToScreenDegrees = (cameraInfo.orientation - screenDegrees + 360) % 360;
// mc++ 
// mc++     // Convert degrees to rotation ids.
// mc++     int cameraToScreenRotation = Surface.ROTATION_0;
// mc++     switch (cameraToScreenDegrees) {
// mc++       case 0:
// mc++         cameraToScreenRotation = Surface.ROTATION_0;
// mc++         break;
// mc++       case 90:
// mc++         cameraToScreenRotation = Surface.ROTATION_90;
// mc++         break;
// mc++       case 180:
// mc++         cameraToScreenRotation = Surface.ROTATION_180;
// mc++         break;
// mc++       case 270:
// mc++         cameraToScreenRotation = Surface.ROTATION_270;
// mc++         break;
// mc++       default:
// mc++         break;
// mc++     }
// mc++ 
// mc++     return cameraToScreenRotation;
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
