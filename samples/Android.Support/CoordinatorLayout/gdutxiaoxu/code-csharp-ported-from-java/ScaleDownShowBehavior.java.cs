// mc++ /*
// mc++  * Copyright 2016 Yan Zhenjie
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
// mc++ package com.xujun.contralayout.behavior;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.design.widget.FloatingActionButton;
// mc++ import android.support.v4.view.ViewCompat;
// mc++ import android.support.v4.view.ViewPropertyAnimatorListener;
// mc++ import android.util.AttributeSet;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ import com.xujun.contralayout.utils.AnimatorUtil;
// mc++ 
// mc++ /**
// mc++  * <p>下拉时显示FAB，上拉隐藏，留出更多位置给用户。</p>
// mc++  * Created on 2016/12/1.
// mc++  *
// mc++  * @author xujun
// mc++  */
// mc++ public class ScaleDownShowBehavior extends FloatingActionButton.Behavior {
// mc++     /**
// mc++      * 退出动画是否正在执行。
// mc++      */
// mc++     private boolean isAnimatingOut = false;
// mc++ 
// mc++     private OnStateChangedListener mOnStateChangedListener;
// mc++ 
// mc++     public ScaleDownShowBehavior(Context context, AttributeSet attrs) {
// mc++         super();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onStartNestedScroll(CoordinatorLayout coordinatorLayout, FloatingActionButton child, View directTargetChild, View target, int nestedScrollAxes) {
// mc++         return nestedScrollAxes == ViewCompat.SCROLL_AXIS_VERTICAL;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onNestedScroll(CoordinatorLayout coordinatorLayout, FloatingActionButton child, View target, int dxConsumed, int dyConsumed, int dxUnconsumed, int dyUnconsumed) {
// mc++         if ((dyConsumed > 0 || dyUnconsumed > 0) && !isAnimatingOut && child.getVisibility() == View.VISIBLE) {//往下滑
// mc++             child.setVisibility(View.INVISIBLE);
// mc++             AnimatorUtil.scaleHide(child, viewPropertyAnimatorListener);
// mc++             if (mOnStateChangedListener != null) {
// mc++                 mOnStateChangedListener.onChanged(false);
// mc++             }
// mc++         } else if ((dyConsumed < 0 || dyUnconsumed < 0) && child.getVisibility() != View.VISIBLE) {
// mc++             AnimatorUtil.scaleShow(child, null);
// mc++             if (mOnStateChangedListener != null) {
// mc++                 mOnStateChangedListener.onChanged(true);
// mc++             }
// mc++         }
// mc++     }
// mc++ 
// mc++     public void setOnStateChangedListener(OnStateChangedListener mOnStateChangedListener) {
// mc++         this.mOnStateChangedListener = mOnStateChangedListener;
// mc++     }
// mc++ 
// mc++     // 外部监听显示和隐藏。
// mc++     public interface OnStateChangedListener {
// mc++         void onChanged(boolean isShow);
// mc++     }
// mc++ 
// mc++     public static <V extends View> ScaleDownShowBehavior from(V view) {
// mc++         ViewGroup.LayoutParams params = view.getLayoutParams();
// mc++         if (!(params instanceof CoordinatorLayout.LayoutParams)) {
// mc++             throw new IllegalArgumentException("The view is not a child of CoordinatorLayout");
// mc++         }
// mc++         CoordinatorLayout.Behavior behavior = ((CoordinatorLayout.LayoutParams) params).getBehavior();
// mc++         if (!(behavior instanceof ScaleDownShowBehavior)) {
// mc++             throw new IllegalArgumentException("The view is not associated with ScaleDownShowBehavior");
// mc++         }
// mc++         return (ScaleDownShowBehavior) behavior;
// mc++     }
// mc++ 
// mc++     private ViewPropertyAnimatorListener viewPropertyAnimatorListener = new ViewPropertyAnimatorListener() {
// mc++ 
// mc++         @Override
// mc++         public void onAnimationStart(View view) {
// mc++             isAnimatingOut = true;
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public void onAnimationEnd(View view) {
// mc++             isAnimatingOut = false;
// mc++             // 注意不要设置为 Gone，这样在高版本的会导致 viewBehavior.onNestedScrol 没机会调用
// mc++             view.setVisibility(View.INVISIBLE);
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public void onAnimationCancel(View arg0) {
// mc++             isAnimatingOut = false;
// mc++         }
// mc++     };
// mc++ }
