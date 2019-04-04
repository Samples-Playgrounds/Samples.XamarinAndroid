// mc++ /*
// mc++ Copyright 2018 Google LLC
// mc++ 
// mc++ Licensed under the Apache License, Version 2.0 (the "License");
// mc++ you may not use this file except in compliance with the License.
// mc++ You may obtain a copy of the License at
// mc++ 
// mc++     https://www.apache.org/licenses/LICENSE-2.0
// mc++ 
// mc++ Unless required by applicable law or agreed to in writing, software
// mc++ distributed under the License is distributed on an "AS IS" BASIS,
// mc++ WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// mc++ See the License for the specific language governing permissions and
// mc++ limitations under the License.
// mc++ */
// mc++ 
// mc++ package com.google.devrel.ar.codelab;
// mc++ 
// mc++ import android.Manifest;
// mc++ 
// mc++ import com.google.ar.sceneform.ux.ArFragment;
// mc++ 
// mc++ public class WritingArFragment extends ArFragment {
// mc++     @Override
// mc++     public String[] getAdditionalPermissions() {
// mc++         String[] additionalPermissions = super.getAdditionalPermissions();
// mc++         int permissionLength = additionalPermissions != null ? additionalPermissions.length : 0;
// mc++         String[] permissions = new String[permissionLength + 1];
// mc++         permissions[0] = Manifest.permission.WRITE_EXTERNAL_STORAGE;
// mc++         if (permissionLength > 0) {
// mc++             System.arraycopy(additionalPermissions, 0, permissions, 1, additionalPermissions.length);
// mc++         }
// mc++         return permissions;
// mc++     }
// mc++ }
