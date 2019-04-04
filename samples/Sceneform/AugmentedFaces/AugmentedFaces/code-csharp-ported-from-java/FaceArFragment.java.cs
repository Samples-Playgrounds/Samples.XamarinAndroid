// mc++ /*
// mc++  * Copyright 2019 Google LLC. All Rights Reserved.
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
// mc++ package com.google.ar.sceneform.samples.augmentedfaces;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.annotation.Nullable;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ import android.widget.FrameLayout;
// mc++ import com.google.ar.core.Config;
// mc++ import com.google.ar.core.Config.AugmentedFaceMode;
// mc++ import com.google.ar.core.Session;
// mc++ import com.google.ar.sceneform.ux.ArFragment;
// mc++ import java.util.EnumSet;
// mc++ import java.util.Set;
// mc++ 
// mc++ /** Implements ArFragment and configures the session for using the augmented faces feature. */
// mc++ public class FaceArFragment extends ArFragment {
// mc++ 
// mc++   @Override
// mc++   protected Config getSessionConfiguration(Session session) {
// mc++     Config config = new Config(session);
// mc++     config.setAugmentedFaceMode(AugmentedFaceMode.MESH3D);
// mc++     return config;
// mc++   }
// mc++ 
// mc++   @Override
// mc++   protected Set<Session.Feature> getSessionFeatures() {
// mc++     return EnumSet.of(Session.Feature.FRONT_CAMERA);
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Override to turn off planeDiscoveryController. Plane trackables are not supported with the
// mc++    * front camera.
// mc++    */
// mc++   @Override
// mc++   public View onCreateView(
// mc++       LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
// mc++     FrameLayout frameLayout =
// mc++         (FrameLayout) super.onCreateView(inflater, container, savedInstanceState);
// mc++ 
// mc++     getPlaneDiscoveryController().hide();
// mc++     getPlaneDiscoveryController().setInstructionView(null);
// mc++ 
// mc++     return frameLayout;
// mc++   }
// mc++ }
