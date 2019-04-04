// mc++ /*
// mc++  * Copyright 2018 Google Inc. All Rights Reserved.
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
// mc++ package com.google.ar.core.examples.java.cloudanchor;
// mc++ 
// mc++ import android.app.AlertDialog;
// mc++ import android.app.Dialog;
// mc++ import android.os.Bundle;
// mc++ import android.support.v4.app.DialogFragment;
// mc++ import android.support.v4.app.FragmentActivity;
// mc++ import android.text.Editable;
// mc++ import android.view.View;
// mc++ import android.widget.EditText;
// mc++ import com.google.common.base.Preconditions;
// mc++ 
// mc++ /** A DialogFragment for the Resolve Dialog Box. */
// mc++ public class ResolveDialogFragment extends DialogFragment {
// mc++ 
// mc++   interface OkListener {
// mc++     /**
// mc++      * This method is called by the dialog box when its OK button is pressed.
// mc++      *
// mc++      * @param dialogValue the long value from the dialog box
// mc++      */
// mc++     void onOkPressed(Long dialogValue);
// mc++   }
// mc++ 
// mc++   private EditText roomCodeField;
// mc++   private OkListener okListener;
// mc++ 
// mc++   public void setOkListener(OkListener okListener) {
// mc++     this.okListener = okListener;
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public Dialog onCreateDialog(Bundle savedInstanceState) {
// mc++     FragmentActivity activity =
// mc++         Preconditions.checkNotNull(getActivity(), "The activity cannot be null.");
// mc++     AlertDialog.Builder builder = new AlertDialog.Builder(activity);
// mc++ 
// mc++     // Passing null as the root is fine, because the view is for a dialog.
// mc++     View dialogView = activity.getLayoutInflater().inflate(R.layout.resolve_dialog, null);
// mc++     roomCodeField = dialogView.findViewById(R.id.room_code_input);
// mc++     builder
// mc++         .setView(dialogView)
// mc++         .setTitle(R.string.resolve_dialog_title)
// mc++         .setPositiveButton(
// mc++             R.string.resolve_dialog_ok,
// mc++             (dialog, which) -> {
// mc++               Editable roomCodeText = roomCodeField.getText();
// mc++               if (okListener != null && roomCodeText != null && roomCodeText.length() > 0) {
// mc++                 Long longVal = Long.valueOf(roomCodeText.toString());
// mc++                 okListener.onOkPressed(longVal);
// mc++               }
// mc++             })
// mc++         .setNegativeButton(R.string.cancel, (dialog, which) -> {});
// mc++     return builder.create();
// mc++   }
// mc++ }
