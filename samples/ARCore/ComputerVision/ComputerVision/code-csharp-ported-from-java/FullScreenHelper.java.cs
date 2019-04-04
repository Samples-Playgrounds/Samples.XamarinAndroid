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
// mc++ import android.view.View;
// mc++ import android.view.WindowManager;
// mc++ 
// mc++ /** Helper to set up the Android full screen mode. */
// mc++ public final class FullScreenHelper {
// mc++   /**
// mc++    * Sets the Android fullscreen flags. Expected to be called from {@link
// mc++    * Activity#onWindowFocusChanged(boolean hasFocus)}.
// mc++    *
// mc++    * @param activity the Activity on which the full screen mode will be set.
// mc++    * @param hasFocus the hasFocus flag passed from the {@link Activity#onWindowFocusChanged(boolean
// mc++    *     hasFocus)} callback.
// mc++    */
// mc++   public static void setFullScreenOnWindowFocusChanged(Activity activity, boolean hasFocus) {
// mc++     if (hasFocus) {
// mc++       // https://developer.android.com/training/system-ui/immersive.html#sticky
// mc++       activity
// mc++           .getWindow()
// mc++           .getDecorView()
// mc++           .setSystemUiVisibility(
// mc++               View.SYSTEM_UI_FLAG_LAYOUT_STABLE
// mc++                   | View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION
// mc++                   | View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN
// mc++                   | View.SYSTEM_UI_FLAG_HIDE_NAVIGATION
// mc++                   | View.SYSTEM_UI_FLAG_FULLSCREEN
// mc++                   | View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY);
// mc++       activity.getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
// mc++     }
// mc++   }
// mc++ }
