// mc++ /*
// mc++  * Copyright 2018 Google LLC
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
// mc++ package com.google.ar.sceneform.samples.solarsystem;
// mc++ 
// mc++ import android.Manifest;
// mc++ import android.app.Activity;
// mc++ import android.app.ActivityManager;
// mc++ import android.content.Context;
// mc++ import android.content.Intent;
// mc++ import android.content.pm.PackageManager;
// mc++ import android.net.Uri;
// mc++ import android.os.Build;
// mc++ import android.os.Build.VERSION_CODES;
// mc++ import android.os.Handler;
// mc++ import android.os.Looper;
// mc++ import android.provider.Settings;
// mc++ import android.support.annotation.Nullable;
// mc++ import android.support.v4.app.ActivityCompat;
// mc++ import android.support.v4.content.ContextCompat;
// mc++ import android.util.Log;
// mc++ import android.view.Gravity;
// mc++ import android.widget.Toast;
// mc++ import com.google.ar.core.ArCoreApk;
// mc++ import com.google.ar.core.Config;
// mc++ import com.google.ar.core.Session;
// mc++ import com.google.ar.core.exceptions.UnavailableApkTooOldException;
// mc++ import com.google.ar.core.exceptions.UnavailableArcoreNotInstalledException;
// mc++ import com.google.ar.core.exceptions.UnavailableDeviceNotCompatibleException;
// mc++ import com.google.ar.core.exceptions.UnavailableException;
// mc++ import com.google.ar.core.exceptions.UnavailableSdkTooOldException;
// mc++ 
// mc++ /** Static utility methods to simplify creating multiple demo activities. */
// mc++ public class DemoUtils {
// mc++   private static final String TAG = "SceneformDemoUtils";
// mc++   private static final double MIN_OPENGL_VERSION = 3.0;
// mc++ 
// mc++   private DemoUtils() {}
// mc++ 
// mc++   /**
// mc++    * Creates and shows a Toast containing an error message. If there was an exception passed in it
// mc++    * will be appended to the toast. The error will also be written to the Log
// mc++    */
// mc++   public static void displayError(
// mc++       final Context context, final String errorMsg, @Nullable final Throwable problem) {
// mc++     final String tag = context.getClass().getSimpleName();
// mc++     final String toastText;
// mc++     if (problem != null && problem.getMessage() != null) {
// mc++       Log.e(tag, errorMsg, problem);
// mc++       toastText = errorMsg + ": " + problem.getMessage();
// mc++     } else if (problem != null) {
// mc++       Log.e(tag, errorMsg, problem);
// mc++       toastText = errorMsg;
// mc++     } else {
// mc++       Log.e(tag, errorMsg);
// mc++       toastText = errorMsg;
// mc++     }
// mc++ 
// mc++     new Handler(Looper.getMainLooper())
// mc++         .post(
// mc++             () -> {
// mc++               Toast toast = Toast.makeText(context, toastText, Toast.LENGTH_LONG);
// mc++               toast.setGravity(Gravity.CENTER, 0, 0);
// mc++               toast.show();
// mc++             });
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Creates an ARCore session. This checks for the CAMERA permission, and if granted, checks the
// mc++    * state of the ARCore installation. If there is a problem an exception is thrown. Care must be
// mc++    * taken to update the installRequested flag as needed to avoid an infinite checking loop. It
// mc++    * should be set to true if null is returned from this method, and called again when the
// mc++    * application is resumed.
// mc++    *
// mc++    * @param activity - the activity currently active.
// mc++    * @param installRequested - the indicator for ARCore that when checking the state of ARCore, if
// mc++    *     an installation was already requested. This is true if this method previously returned
// mc++    *     null. and the camera permission has been granted.
// mc++    */
// mc++   public static Session createArSession(Activity activity, boolean installRequested)
// mc++       throws UnavailableException {
// mc++     Session session = null;
// mc++     // if we have the camera permission, create the session
// mc++     if (hasCameraPermission(activity)) {
// mc++       switch (ArCoreApk.getInstance().requestInstall(activity, !installRequested)) {
// mc++         case INSTALL_REQUESTED:
// mc++           return null;
// mc++         case INSTALLED:
// mc++           break;
// mc++       }
// mc++       session = new Session(activity);
// mc++       // IMPORTANT!!!  ArSceneView requires the `LATEST_CAMERA_IMAGE` non-blocking update mode.
// mc++       Config config = new Config(session);
// mc++       config.setUpdateMode(Config.UpdateMode.LATEST_CAMERA_IMAGE);
// mc++       session.configure(config);
// mc++     }
// mc++     return session;
// mc++   }
// mc++ 
// mc++   /** Check to see we have the necessary permissions for this app, and ask for them if we don't. */
// mc++   public static void requestCameraPermission(Activity activity, int requestCode) {
// mc++     ActivityCompat.requestPermissions(
// mc++         activity, new String[] {Manifest.permission.CAMERA}, requestCode);
// mc++   }
// mc++ 
// mc++   /** Check to see we have the necessary permissions for this app. */
// mc++   public static boolean hasCameraPermission(Activity activity) {
// mc++     return ContextCompat.checkSelfPermission(activity, Manifest.permission.CAMERA)
// mc++         == PackageManager.PERMISSION_GRANTED;
// mc++   }
// mc++   /** Check to see if we need to show the rationale for this permission. */
// mc++   public static boolean shouldShowRequestPermissionRationale(Activity activity) {
// mc++     return ActivityCompat.shouldShowRequestPermissionRationale(
// mc++         activity, Manifest.permission.CAMERA);
// mc++   }
// mc++ 
// mc++   /** Launch Application Setting to grant permission. */
// mc++   public static void launchPermissionSettings(Activity activity) {
// mc++     Intent intent = new Intent();
// mc++     intent.setAction(Settings.ACTION_APPLICATION_DETAILS_SETTINGS);
// mc++     intent.setData(Uri.fromParts("package", activity.getPackageName(), null));
// mc++     activity.startActivity(intent);
// mc++   }
// mc++ 
// mc++   public static void handleSessionException(
// mc++       Activity activity, UnavailableException sessionException) {
// mc++ 
// mc++     String message;
// mc++     if (sessionException instanceof UnavailableArcoreNotInstalledException) {
// mc++       message = "Please install ARCore";
// mc++     } else if (sessionException instanceof UnavailableApkTooOldException) {
// mc++       message = "Please update ARCore";
// mc++     } else if (sessionException instanceof UnavailableSdkTooOldException) {
// mc++       message = "Please update this app";
// mc++     } else if (sessionException instanceof UnavailableDeviceNotCompatibleException) {
// mc++       message = "This device does not support AR";
// mc++     } else {
// mc++       message = "Failed to create AR session";
// mc++       Log.e(TAG, "Exception: " + sessionException);
// mc++     }
// mc++     Toast.makeText(activity, message, Toast.LENGTH_LONG).show();
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
// mc++ 
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
