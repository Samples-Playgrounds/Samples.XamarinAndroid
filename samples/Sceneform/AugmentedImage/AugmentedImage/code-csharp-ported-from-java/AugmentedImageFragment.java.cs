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
// mc++ package com.google.ar.sceneform.samples.augmentedimage;
// mc++ 
// mc++ import android.app.ActivityManager;
// mc++ import android.content.Context;
// mc++ import android.content.res.AssetManager;
// mc++ import android.graphics.Bitmap;
// mc++ import android.graphics.BitmapFactory;
// mc++ import android.os.Build;
// mc++ import android.os.Bundle;
// mc++ import android.support.annotation.Nullable;
// mc++ import android.util.Log;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ import com.google.ar.core.AugmentedImageDatabase;
// mc++ import com.google.ar.core.Config;
// mc++ import com.google.ar.core.Session;
// mc++ import com.google.ar.sceneform.samples.common.helpers.SnackbarHelper;
// mc++ import com.google.ar.sceneform.ux.ArFragment;
// mc++ import java.io.IOException;
// mc++ import java.io.InputStream;
// mc++ 
// mc++ /**
// mc++  * Extend the ArFragment to customize the ARCore session configuration to include Augmented Images.
// mc++  */
// mc++ public class AugmentedImageFragment extends ArFragment {
// mc++   private static final String TAG = "AugmentedImageFragment";
// mc++ 
// mc++   // This is the name of the image in the sample database.  A copy of the image is in the assets
// mc++   // directory.  Opening this image on your computer is a good quick way to test the augmented image
// mc++   // matching.
// mc++   private static final String DEFAULT_IMAGE_NAME = "default.jpg";
// mc++ 
// mc++   // This is a pre-created database containing the sample image.
// mc++   private static final String SAMPLE_IMAGE_DATABASE = "sample_database.imgdb";
// mc++ 
// mc++   // Augmented image configuration and rendering.
// mc++   // Load a single image (true) or a pre-generated image database (false).
// mc++   private static final boolean USE_SINGLE_IMAGE = false;
// mc++ 
// mc++   // Do a runtime check for the OpenGL level available at runtime to avoid Sceneform crashing the
// mc++   // application.
// mc++   private static final double MIN_OPENGL_VERSION = 3.0;
// mc++ 
// mc++   @Override
// mc++   public void onAttach(Context context) {
// mc++     super.onAttach(context);
// mc++ 
// mc++     // Check for Sceneform being supported on this device.  This check will be integrated into
// mc++     // Sceneform eventually.
// mc++     if (Build.VERSION.SDK_INT < Build.VERSION_CODES.N) {
// mc++       Log.e(TAG, "Sceneform requires Android N or later");
// mc++       SnackbarHelper.getInstance()
// mc++           .showError(getActivity(), "Sceneform requires Android N or later");
// mc++     }
// mc++ 
// mc++     String openGlVersionString =
// mc++         ((ActivityManager) context.getSystemService(Context.ACTIVITY_SERVICE))
// mc++             .getDeviceConfigurationInfo()
// mc++             .getGlEsVersion();
// mc++     if (Double.parseDouble(openGlVersionString) < MIN_OPENGL_VERSION) {
// mc++       Log.e(TAG, "Sceneform requires OpenGL ES 3.0 or later");
// mc++       SnackbarHelper.getInstance()
// mc++           .showError(getActivity(), "Sceneform requires OpenGL ES 3.0 or later");
// mc++     }
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public View onCreateView(
// mc++       LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
// mc++     View view = super.onCreateView(inflater, container, savedInstanceState);
// mc++ 
// mc++     // Turn off the plane discovery since we're only looking for images
// mc++     getPlaneDiscoveryController().hide();
// mc++     getPlaneDiscoveryController().setInstructionView(null);
// mc++     getArSceneView().getPlaneRenderer().setEnabled(false);
// mc++     return view;
// mc++   }
// mc++ 
// mc++   @Override
// mc++   protected Config getSessionConfiguration(Session session) {
// mc++     Config config = super.getSessionConfiguration(session);
// mc++     if (!setupAugmentedImageDatabase(config, session)) {
// mc++       SnackbarHelper.getInstance()
// mc++           .showError(getActivity(), "Could not setup augmented image database");
// mc++     }
// mc++     return config;
// mc++   }
// mc++ 
// mc++   private boolean setupAugmentedImageDatabase(Config config, Session session) {
// mc++     AugmentedImageDatabase augmentedImageDatabase;
// mc++ 
// mc++     AssetManager assetManager = getContext() != null ? getContext().getAssets() : null;
// mc++     if (assetManager == null) {
// mc++       Log.e(TAG, "Context is null, cannot intitialize image database.");
// mc++       return false;
// mc++     }
// mc++ 
// mc++     // There are two ways to configure an AugmentedImageDatabase:
// mc++     // 1. Add Bitmap to DB directly
// mc++     // 2. Load a pre-built AugmentedImageDatabase
// mc++     // Option 2) has
// mc++     // * shorter setup time
// mc++     // * doesn't require images to be packaged in apk.
// mc++     if (USE_SINGLE_IMAGE) {
// mc++       Bitmap augmentedImageBitmap = loadAugmentedImageBitmap(assetManager);
// mc++       if (augmentedImageBitmap == null) {
// mc++         return false;
// mc++       }
// mc++ 
// mc++       augmentedImageDatabase = new AugmentedImageDatabase(session);
// mc++       augmentedImageDatabase.addImage(DEFAULT_IMAGE_NAME, augmentedImageBitmap);
// mc++       // If the physical size of the image is known, you can instead use:
// mc++       //     augmentedImageDatabase.addImage("image_name", augmentedImageBitmap, widthInMeters);
// mc++       // This will improve the initial detection speed. ARCore will still actively estimate the
// mc++       // physical size of the image as it is viewed from multiple viewpoints.
// mc++     } else {
// mc++       // This is an alternative way to initialize an AugmentedImageDatabase instance,
// mc++       // load a pre-existing augmented image database.
// mc++       try (InputStream is = getContext().getAssets().open(SAMPLE_IMAGE_DATABASE)) {
// mc++         augmentedImageDatabase = AugmentedImageDatabase.deserialize(session, is);
// mc++       } catch (IOException e) {
// mc++         Log.e(TAG, "IO exception loading augmented image database.", e);
// mc++         return false;
// mc++       }
// mc++     }
// mc++ 
// mc++     config.setAugmentedImageDatabase(augmentedImageDatabase);
// mc++     return true;
// mc++   }
// mc++ 
// mc++   private Bitmap loadAugmentedImageBitmap(AssetManager assetManager) {
// mc++     try (InputStream is = assetManager.open(DEFAULT_IMAGE_NAME)) {
// mc++       return BitmapFactory.decodeStream(is);
// mc++     } catch (IOException e) {
// mc++       Log.e(TAG, "IO exception loading augmented image bitmap.", e);
// mc++     }
// mc++     return null;
// mc++   }
// mc++ }
