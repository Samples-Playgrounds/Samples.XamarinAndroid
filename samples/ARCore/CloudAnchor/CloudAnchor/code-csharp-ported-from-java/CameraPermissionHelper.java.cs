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
// mc++ import android.Manifest;
// mc++ import android.app.Activity;
// mc++ import android.content.Intent;
// mc++ import android.content.pm.PackageManager;
// mc++ import android.net.Uri;
// mc++ import android.provider.Settings;
// mc++ import android.support.v4.app.ActivityCompat;
// mc++ import android.support.v4.content.ContextCompat;
// mc++ 
// mc++ /** Helper to ask camera permission. */
// mc++ public final class CameraPermissionHelper {
// mc++   private static final int CAMERA_PERMISSION_CODE = 0;
// mc++   private static final String CAMERA_PERMISSION = Manifest.permission.CAMERA;
// mc++ 
// mc++   /** Check to see we have the necessary permissions for this app. */
// mc++   public static boolean hasCameraPermission(Activity activity) {
// mc++     return ContextCompat.checkSelfPermission(activity, CAMERA_PERMISSION)
// mc++         == PackageManager.PERMISSION_GRANTED;
// mc++   }
// mc++ 
// mc++   /** Check to see we have the necessary permissions for this app, and ask for them if we don't. */
// mc++   public static void requestCameraPermission(Activity activity) {
// mc++     ActivityCompat.requestPermissions(
// mc++         activity, new String[] {CAMERA_PERMISSION}, CAMERA_PERMISSION_CODE);
// mc++   }
// mc++ 
// mc++   /** Check to see if we need to show the rationale for this permission. */
// mc++   public static boolean shouldShowRequestPermissionRationale(Activity activity) {
// mc++     return ActivityCompat.shouldShowRequestPermissionRationale(activity, CAMERA_PERMISSION);
// mc++   }
// mc++ 
// mc++   /** Launch Application Setting to grant permission. */
// mc++   public static void launchPermissionSettings(Activity activity) {
// mc++     Intent intent = new Intent();
// mc++     intent.setAction(Settings.ACTION_APPLICATION_DETAILS_SETTINGS);
// mc++     intent.setData(Uri.fromParts("package", activity.getPackageName(), null));
// mc++     activity.startActivity(intent);
// mc++   }
// mc++ }
