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
// mc++ import android.content.Context;
// mc++ import android.view.GestureDetector;
// mc++ import android.view.MotionEvent;
// mc++ import android.view.View;
// mc++ import android.view.View.OnTouchListener;
// mc++ import java.util.concurrent.ArrayBlockingQueue;
// mc++ import java.util.concurrent.BlockingQueue;
// mc++ 
// mc++ /**
// mc++  * Helper to detect taps using Android GestureDetector, and pass the taps between UI thread and
// mc++  * render thread.
// mc++  */
// mc++ public final class TapHelper implements OnTouchListener {
// mc++   private final GestureDetector gestureDetector;
// mc++   private final BlockingQueue<MotionEvent> queuedSingleTaps = new ArrayBlockingQueue<>(16);
// mc++ 
// mc++   /**
// mc++    * Creates the tap helper.
// mc++    *
// mc++    * @param context the application's context.
// mc++    */
// mc++   public TapHelper(Context context) {
// mc++     gestureDetector =
// mc++         new GestureDetector(
// mc++             context,
// mc++             new GestureDetector.SimpleOnGestureListener() {
// mc++               @Override
// mc++               public boolean onSingleTapUp(MotionEvent e) {
// mc++                 // Queue tap if there is space. Tap is lost if queue is full.
// mc++                 queuedSingleTaps.offer(e);
// mc++                 return true;
// mc++               }
// mc++ 
// mc++               @Override
// mc++               public boolean onDown(MotionEvent e) {
// mc++                 return true;
// mc++               }
// mc++             });
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Polls for a tap.
// mc++    *
// mc++    * @return if a tap was queued, a MotionEvent for the tap. Otherwise null if no taps are queued.
// mc++    */
// mc++   public MotionEvent poll() {
// mc++     return queuedSingleTaps.poll();
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public boolean onTouch(View view, MotionEvent motionEvent) {
// mc++     return gestureDetector.onTouchEvent(motionEvent);
// mc++   }
// mc++ }
