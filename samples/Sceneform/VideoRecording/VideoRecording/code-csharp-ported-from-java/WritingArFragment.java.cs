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
// mc++ 
// mc++ package com.google.ar.sceneform.samples.videorecording;
// mc++ 
// mc++ import android.Manifest;
// mc++ import android.content.Intent;
// mc++ import android.content.pm.PackageManager;
// mc++ import android.net.Uri;
// mc++ import android.provider.Settings;
// mc++ import android.support.v4.app.ActivityCompat;
// mc++ import com.google.ar.sceneform.ux.ArFragment;
// mc++ 
// mc++ /**
// mc++  * Writing Ar Fragment extends the ArFragment class to include the WRITER_EXTERNAL_STORAGE
// mc++  * permission. This adds this permission to the list of permissions presented to the user for
// mc++  * granting.
// mc++  */
// mc++ public class WritingArFragment extends ArFragment {
// mc++   @Override
// mc++   public String[] getAdditionalPermissions() {
// mc++     String[] additionalPermissions = super.getAdditionalPermissions();
// mc++     int permissionLength = additionalPermissions != null ? additionalPermissions.length : 0;
// mc++     String[] permissions = new String[permissionLength + 1];
// mc++     permissions[0] = Manifest.permission.WRITE_EXTERNAL_STORAGE;
// mc++     if (permissionLength > 0) {
// mc++       System.arraycopy(additionalPermissions, 0, permissions, 1, additionalPermissions.length);
// mc++     }
// mc++     return permissions;
// mc++   }
// mc++ 
// mc++   public boolean hasWritePermission() {
// mc++     return ActivityCompat.checkSelfPermission(
// mc++             this.requireActivity(), Manifest.permission.WRITE_EXTERNAL_STORAGE)
// mc++         == PackageManager.PERMISSION_GRANTED;
// mc++   }
// mc++ 
// mc++   /** Launch Application Setting to grant permissions. */
// mc++   public void launchPermissionSettings() {
// mc++     Intent intent = new Intent();
// mc++     intent.setAction(Settings.ACTION_APPLICATION_DETAILS_SETTINGS);
// mc++     intent.setData(Uri.fromParts("package", requireActivity().getPackageName(), null));
// mc++     requireActivity().startActivity(intent);
// mc++   }
// mc++ }
