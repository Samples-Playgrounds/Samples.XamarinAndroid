// mc++ /*
// mc++  * Copyright (C) 2015 takahirom
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
// mc++ package com.github.takahirom.webview_in_coodinator_layout;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.v4.view.MotionEventCompat;
// mc++ import android.support.v4.view.NestedScrollingChild;
// mc++ import android.support.v4.view.NestedScrollingChildHelper;
// mc++ import android.support.v4.view.ViewCompat;
// mc++ import android.util.AttributeSet;
// mc++ import android.view.MotionEvent;
// mc++ import android.webkit.WebView;
// mc++ 
// mc++ public class NestedWebView extends WebView implements NestedScrollingChild {
// mc++     private int mLastY;
// mc++     private final int[] mScrollOffset = new int[2];
// mc++     private final int[] mScrollConsumed = new int[2];
// mc++     private int mNestedOffsetY;
// mc++     private NestedScrollingChildHelper mChildHelper;
// mc++ 
// mc++     public NestedWebView(Context context) {
// mc++         this(context, null);
// mc++     }
// mc++ 
// mc++     public NestedWebView(Context context, AttributeSet attrs) {
// mc++         this(context, attrs, android.R.attr.webViewStyle);
// mc++     }
// mc++ 
// mc++     public NestedWebView(Context context, AttributeSet attrs, int defStyleAttr) {
// mc++         super(context, attrs, defStyleAttr);
// mc++         mChildHelper = new NestedScrollingChildHelper(this);
// mc++         setNestedScrollingEnabled(true);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onTouchEvent(MotionEvent ev) {
// mc++         boolean returnValue = false;
// mc++ 
// mc++         MotionEvent event = MotionEvent.obtain(ev);
// mc++         final int action = MotionEventCompat.getActionMasked(event);
// mc++         if (action == MotionEvent.ACTION_DOWN) {
// mc++             mNestedOffsetY = 0;
// mc++         }
// mc++         int eventY = (int) event.getY();
// mc++         event.offsetLocation(0, mNestedOffsetY);
// mc++         switch (action) {
// mc++             case MotionEvent.ACTION_MOVE:
// mc++                 int deltaY = mLastY - eventY;
// mc++                 // NestedPreScroll
// mc++                 if (dispatchNestedPreScroll(0, deltaY, mScrollConsumed, mScrollOffset)) {
// mc++                     deltaY -= mScrollConsumed[1];
// mc++                     mLastY = eventY - mScrollOffset[1];
// mc++                     event.offsetLocation(0, -mScrollOffset[1]);
// mc++                     mNestedOffsetY += mScrollOffset[1];
// mc++                 }
// mc++                 returnValue = super.onTouchEvent(event);
// mc++ 
// mc++                 // NestedScroll
// mc++                 if (dispatchNestedScroll(0, mScrollOffset[1], 0, deltaY, mScrollOffset)) {
// mc++                     event.offsetLocation(0, mScrollOffset[1]);
// mc++                     mNestedOffsetY += mScrollOffset[1];
// mc++                     mLastY -= mScrollOffset[1];
// mc++                 }
// mc++                 break;
// mc++             case MotionEvent.ACTION_DOWN:
// mc++                 returnValue = super.onTouchEvent(event);
// mc++                 mLastY = eventY;
// mc++                 // start NestedScroll
// mc++                 startNestedScroll(ViewCompat.SCROLL_AXIS_VERTICAL);
// mc++                 break;
// mc++             case MotionEvent.ACTION_UP:
// mc++             case MotionEvent.ACTION_CANCEL:
// mc++                 returnValue = super.onTouchEvent(event);
// mc++                 // end NestedScroll
// mc++                 stopNestedScroll();
// mc++                 break;
// mc++         }
// mc++         return returnValue;
// mc++     }
// mc++ 
// mc++     // Nested Scroll implements
// mc++     @Override
// mc++     public void setNestedScrollingEnabled(boolean enabled) {
// mc++         mChildHelper.setNestedScrollingEnabled(enabled);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean isNestedScrollingEnabled() {
// mc++         return mChildHelper.isNestedScrollingEnabled();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean startNestedScroll(int axes) {
// mc++         return mChildHelper.startNestedScroll(axes);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void stopNestedScroll() {
// mc++         mChildHelper.stopNestedScroll();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean hasNestedScrollingParent() {
// mc++         return mChildHelper.hasNestedScrollingParent();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean dispatchNestedScroll(int dxConsumed, int dyConsumed, int dxUnconsumed, int dyUnconsumed,
// mc++                                         int[] offsetInWindow) {
// mc++         return mChildHelper.dispatchNestedScroll(dxConsumed, dyConsumed, dxUnconsumed, dyUnconsumed, offsetInWindow);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean dispatchNestedPreScroll(int dx, int dy, int[] consumed, int[] offsetInWindow) {
// mc++         return mChildHelper.dispatchNestedPreScroll(dx, dy, consumed, offsetInWindow);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean dispatchNestedFling(float velocityX, float velocityY, boolean consumed) {
// mc++         return mChildHelper.dispatchNestedFling(velocityX, velocityY, consumed);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean dispatchNestedPreFling(float velocityX, float velocityY) {
// mc++         return mChildHelper.dispatchNestedPreFling(velocityX, velocityY);
// mc++     }
// mc++ 
// mc++ }
