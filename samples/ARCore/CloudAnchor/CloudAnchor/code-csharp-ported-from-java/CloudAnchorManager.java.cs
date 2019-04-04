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
// mc++ import android.support.annotation.Nullable;
// mc++ import com.google.ar.core.Anchor;
// mc++ import com.google.ar.core.Anchor.CloudAnchorState;
// mc++ import com.google.ar.core.Session;
// mc++ import com.google.common.base.Preconditions;
// mc++ import java.util.HashMap;
// mc++ import java.util.Iterator;
// mc++ import java.util.Map;
// mc++ 
// mc++ /**
// mc++  * A helper class to handle all the Cloud Anchors logic, and add a callback-like mechanism on top of
// mc++  * the existing ARCore API.
// mc++  */
// mc++ class CloudAnchorManager {
// mc++   private static final String TAG =
// mc++       CloudAnchorActivity.class.getSimpleName() + "." + CloudAnchorManager.class.getSimpleName();
// mc++ 
// mc++   /** Listener for the results of a host or resolve operation. */
// mc++   interface CloudAnchorListener {
// mc++ 
// mc++     /** This method is invoked when the results of a Cloud Anchor operation are available. */
// mc++     void onCloudTaskComplete(Anchor anchor);
// mc++   }
// mc++ 
// mc++   @Nullable private Session session = null;
// mc++   private final HashMap<Anchor, CloudAnchorListener> pendingAnchors = new HashMap<>();
// mc++ 
// mc++   /**
// mc++    * This method is used to set the session, since it might not be available when this object is
// mc++    * created.
// mc++    */
// mc++   synchronized void setSession(Session session) {
// mc++     this.session = session;
// mc++   }
// mc++ 
// mc++   /**
// mc++    * This method hosts an anchor. The {@code listener} will be invoked when the results are
// mc++    * available.
// mc++    */
// mc++   synchronized void hostCloudAnchor(Anchor anchor, CloudAnchorListener listener) {
// mc++     Preconditions.checkNotNull(session, "The session cannot be null.");
// mc++     Anchor newAnchor = session.hostCloudAnchor(anchor);
// mc++     pendingAnchors.put(newAnchor, listener);
// mc++   }
// mc++ 
// mc++   /**
// mc++    * This method resolves an anchor. The {@code listener} will be invoked when the results are
// mc++    * available.
// mc++    */
// mc++   synchronized void resolveCloudAnchor(String anchorId, CloudAnchorListener listener) {
// mc++     Preconditions.checkNotNull(session, "The session cannot be null.");
// mc++     Anchor newAnchor = session.resolveCloudAnchor(anchorId);
// mc++     pendingAnchors.put(newAnchor, listener);
// mc++   }
// mc++ 
// mc++   /** Should be called after a {@link Session#update()} call. */
// mc++   synchronized void onUpdate() {
// mc++     Preconditions.checkNotNull(session, "The session cannot be null.");
// mc++     Iterator<Map.Entry<Anchor, CloudAnchorListener>> iter = pendingAnchors.entrySet().iterator();
// mc++     while (iter.hasNext()) {
// mc++       Map.Entry<Anchor, CloudAnchorListener> entry = iter.next();
// mc++       Anchor anchor = entry.getKey();
// mc++       if (isReturnableState(anchor.getCloudAnchorState())) {
// mc++         CloudAnchorListener listener = entry.getValue();
// mc++         listener.onCloudTaskComplete(anchor);
// mc++         iter.remove();
// mc++       }
// mc++     }
// mc++   }
// mc++ 
// mc++   /** Used to clear any currently registered listeners, so they wont be called again. */
// mc++   synchronized void clearListeners() {
// mc++     pendingAnchors.clear();
// mc++   }
// mc++ 
// mc++   private static boolean isReturnableState(CloudAnchorState cloudState) {
// mc++     switch (cloudState) {
// mc++       case NONE:
// mc++       case TASK_IN_PROGRESS:
// mc++         return false;
// mc++       default:
// mc++         return true;
// mc++     }
// mc++   }
// mc++ }
