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
// mc++ import android.content.Context;
// mc++ import android.util.Log;
// mc++ import com.google.common.base.Preconditions;
// mc++ import com.google.firebase.FirebaseApp;
// mc++ import com.google.firebase.database.DataSnapshot;
// mc++ import com.google.firebase.database.DatabaseError;
// mc++ import com.google.firebase.database.DatabaseReference;
// mc++ import com.google.firebase.database.FirebaseDatabase;
// mc++ import com.google.firebase.database.MutableData;
// mc++ import com.google.firebase.database.Transaction;
// mc++ import com.google.firebase.database.ValueEventListener;
// mc++ 
// mc++ /** A helper class to manage all communications with Firebase. */
// mc++ class FirebaseManager {
// mc++   private static final String TAG =
// mc++       CloudAnchorActivity.class.getSimpleName() + "." + FirebaseManager.class.getSimpleName();
// mc++ 
// mc++   /** Listener for a new room code. */
// mc++   interface RoomCodeListener {
// mc++ 
// mc++     /** Invoked when a new room code is available from Firebase. */
// mc++     void onNewRoomCode(Long newRoomCode);
// mc++ 
// mc++     /** Invoked if a Firebase Database Error happened while fetching the room code. */
// mc++     void onError(DatabaseError error);
// mc++   }
// mc++ 
// mc++   /** Listener for a new cloud anchor ID. */
// mc++   interface CloudAnchorIdListener {
// mc++ 
// mc++     /** Invoked when a new cloud anchor ID is available. */
// mc++     void onNewCloudAnchorId(String cloudAnchorId);
// mc++   }
// mc++ 
// mc++   // Names of the nodes used in the Firebase Database
// mc++   private static final String ROOT_FIREBASE_HOTSPOTS = "hotspot_list";
// mc++   private static final String ROOT_LAST_ROOM_CODE = "last_room_code";
// mc++ 
// mc++   // Some common keys and values used when writing to the Firebase Database.
// mc++   private static final String KEY_DISPLAY_NAME = "display_name";
// mc++   private static final String KEY_ANCHOR_ID = "hosted_anchor_id";
// mc++   private static final String KEY_TIMESTAMP = "updated_at_timestamp";
// mc++   private static final String DISPLAY_NAME_VALUE = "Android EAP Sample";
// mc++ 
// mc++   private final FirebaseApp app;
// mc++   private final DatabaseReference hotspotListRef;
// mc++   private final DatabaseReference roomCodeRef;
// mc++   private DatabaseReference currentRoomRef = null;
// mc++   private ValueEventListener currentRoomListener = null;
// mc++ 
// mc++   /**
// mc++    * Default constructor for the FirebaseManager.
// mc++    *
// mc++    * @param context The application context.
// mc++    */
// mc++   FirebaseManager(Context context) {
// mc++     app = FirebaseApp.initializeApp(context);
// mc++     if (app != null) {
// mc++       DatabaseReference rootRef = FirebaseDatabase.getInstance(app).getReference();
// mc++       hotspotListRef = rootRef.child(ROOT_FIREBASE_HOTSPOTS);
// mc++       roomCodeRef = rootRef.child(ROOT_LAST_ROOM_CODE);
// mc++ 
// mc++       DatabaseReference.goOnline();
// mc++     } else {
// mc++       Log.d(TAG, "Could not connect to Firebase Database!");
// mc++       hotspotListRef = null;
// mc++       roomCodeRef = null;
// mc++     }
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Gets a new room code from the Firebase Database. Invokes the listener method when a new room
// mc++    * code is available.
// mc++    */
// mc++   void getNewRoomCode(RoomCodeListener listener) {
// mc++     Preconditions.checkNotNull(app, "Firebase App was null");
// mc++     roomCodeRef.runTransaction(
// mc++         new Transaction.Handler() {
// mc++           @Override
// mc++           public Transaction.Result doTransaction(MutableData currentData) {
// mc++             Long nextCode = Long.valueOf(1);
// mc++             Object currVal = currentData.getValue();
// mc++             if (currVal != null) {
// mc++               Long lastCode = Long.valueOf(currVal.toString());
// mc++               nextCode = lastCode + 1;
// mc++             }
// mc++             currentData.setValue(nextCode);
// mc++             return Transaction.success(currentData);
// mc++           }
// mc++ 
// mc++           @Override
// mc++           public void onComplete(DatabaseError error, boolean committed, DataSnapshot currentData) {
// mc++             if (!committed) {
// mc++               listener.onError(error);
// mc++               return;
// mc++             }
// mc++             Long roomCode = currentData.getValue(Long.class);
// mc++             listener.onNewRoomCode(roomCode);
// mc++           }
// mc++         });
// mc++   }
// mc++ 
// mc++   /** Stores the given anchor ID in the given room code. */
// mc++   void storeAnchorIdInRoom(Long roomCode, String cloudAnchorId) {
// mc++     Preconditions.checkNotNull(app, "Firebase App was null");
// mc++     DatabaseReference roomRef = hotspotListRef.child(String.valueOf(roomCode));
// mc++     roomRef.child(KEY_DISPLAY_NAME).setValue(DISPLAY_NAME_VALUE);
// mc++     roomRef.child(KEY_ANCHOR_ID).setValue(cloudAnchorId);
// mc++     roomRef.child(KEY_TIMESTAMP).setValue(System.currentTimeMillis());
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Registers a new listener for the given room code. The listener is invoked whenever the data for
// mc++    * the room code is changed.
// mc++    */
// mc++   void registerNewListenerForRoom(Long roomCode, CloudAnchorIdListener listener) {
// mc++     Preconditions.checkNotNull(app, "Firebase App was null");
// mc++     clearRoomListener();
// mc++     currentRoomRef = hotspotListRef.child(String.valueOf(roomCode));
// mc++     currentRoomListener =
// mc++         new ValueEventListener() {
// mc++           @Override
// mc++           public void onDataChange(DataSnapshot dataSnapshot) {
// mc++             Object valObj = dataSnapshot.child(KEY_ANCHOR_ID).getValue();
// mc++             if (valObj != null) {
// mc++               String anchorId = String.valueOf(valObj);
// mc++               if (!anchorId.isEmpty()) {
// mc++                 listener.onNewCloudAnchorId(anchorId);
// mc++               }
// mc++             }
// mc++           }
// mc++ 
// mc++           @Override
// mc++           public void onCancelled(DatabaseError databaseError) {
// mc++             Log.w(TAG, "The Firebase operation was cancelled.", databaseError.toException());
// mc++           }
// mc++         };
// mc++     currentRoomRef.addValueEventListener(currentRoomListener);
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Resets the current room listener registered using {@link #registerNewListenerForRoom(Long,
// mc++    * CloudAnchorIdListener)}.
// mc++    */
// mc++   void clearRoomListener() {
// mc++     if (currentRoomListener != null && currentRoomRef != null) {
// mc++       currentRoomRef.removeEventListener(currentRoomListener);
// mc++       currentRoomListener = null;
// mc++       currentRoomRef = null;
// mc++     }
// mc++   }
// mc++ }
