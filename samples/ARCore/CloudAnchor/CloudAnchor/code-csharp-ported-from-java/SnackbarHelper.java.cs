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
// mc++ import android.support.design.widget.BaseTransientBottomBar;
// mc++ import android.support.design.widget.Snackbar;
// mc++ import android.view.View;
// mc++ import android.widget.TextView;
// mc++ 
// mc++ /**
// mc++  * Helper to manage the sample snackbar. Hides the Android boilerplate code, and exposes simpler
// mc++  * methods.
// mc++  */
// mc++ public final class SnackbarHelper {
// mc++   private static final int BACKGROUND_COLOR = 0xbf323232;
// mc++   private Snackbar messageSnackbar;
// mc++   private enum DismissBehavior { HIDE, SHOW, FINISH };
// mc++   private int maxLines = 2;
// mc++   private String lastMessage = "";
// mc++ 
// mc++   public boolean isShowing() {
// mc++     return messageSnackbar != null;
// mc++   }
// mc++ 
// mc++   /** Shows a snackbar with a given message. */
// mc++   public void showMessage(Activity activity, String message) {
// mc++     if (!message.isEmpty() && (!isShowing() || !lastMessage.equals(message))) {
// mc++       lastMessage = message;
// mc++       show(activity, message, DismissBehavior.HIDE);
// mc++     }
// mc++   }
// mc++ 
// mc++   /** Shows a snackbar with a given message, and a dismiss button. */
// mc++   public void showMessageWithDismiss(Activity activity, String message) {
// mc++     show(activity, message, DismissBehavior.SHOW);
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Shows a snackbar with a given error message. When dismissed, will finish the activity. Useful
// mc++    * for notifying errors, where no further interaction with the activity is possible.
// mc++    */
// mc++   public void showError(Activity activity, String errorMessage) {
// mc++     show(activity, errorMessage, DismissBehavior.FINISH);
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Hides the currently showing snackbar, if there is one. Safe to call from any thread. Safe to
// mc++    * call even if snackbar is not shown.
// mc++    */
// mc++   public void hide(Activity activity) {
// mc++     if (!isShowing()) {
// mc++       return;
// mc++     }
// mc++     lastMessage = "";
// mc++     Snackbar messageSnackbarToHide = messageSnackbar;
// mc++     messageSnackbar = null;
// mc++     activity.runOnUiThread(
// mc++         new Runnable() {
// mc++           @Override
// mc++           public void run() {
// mc++             messageSnackbarToHide.dismiss();
// mc++           }
// mc++         });
// mc++   }
// mc++ 
// mc++   public void setMaxLines(int lines) {
// mc++     maxLines = lines;
// mc++   }
// mc++ 
// mc++   private void show(
// mc++       final Activity activity, final String message, final DismissBehavior dismissBehavior) {
// mc++     activity.runOnUiThread(
// mc++         new Runnable() {
// mc++           @Override
// mc++           public void run() {
// mc++             messageSnackbar =
// mc++                 Snackbar.make(
// mc++                     activity.findViewById(android.R.id.content),
// mc++                     message,
// mc++                     Snackbar.LENGTH_INDEFINITE);
// mc++             messageSnackbar.getView().setBackgroundColor(BACKGROUND_COLOR);
// mc++             if (dismissBehavior != DismissBehavior.HIDE) {
// mc++               messageSnackbar.setAction(
// mc++                   "Dismiss",
// mc++                   new View.OnClickListener() {
// mc++                     @Override
// mc++                     public void onClick(View v) {
// mc++                       messageSnackbar.dismiss();
// mc++                     }
// mc++                   });
// mc++               if (dismissBehavior == DismissBehavior.FINISH) {
// mc++                 messageSnackbar.addCallback(
// mc++                     new BaseTransientBottomBar.BaseCallback<Snackbar>() {
// mc++                       @Override
// mc++                       public void onDismissed(Snackbar transientBottomBar, int event) {
// mc++                         super.onDismissed(transientBottomBar, event);
// mc++                         activity.finish();
// mc++                       }
// mc++                     });
// mc++               }
// mc++             }
// mc++             ((TextView)
// mc++                     messageSnackbar
// mc++                         .getView()
// mc++                         .findViewById(android.support.design.R.id.snackbar_text))
// mc++                 .setMaxLines(maxLines);
// mc++             messageSnackbar.show();
// mc++           }
// mc++         });
// mc++   }
// mc++ }
