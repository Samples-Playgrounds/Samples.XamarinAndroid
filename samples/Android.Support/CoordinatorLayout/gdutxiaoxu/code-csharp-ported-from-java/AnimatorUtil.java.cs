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
// mc++ package com.xujun.contralayout.utils;
// mc++ 
// mc++ import android.animation.ValueAnimator;
// mc++ import android.support.v4.view.ViewCompat;
// mc++ import android.support.v4.view.ViewPropertyAnimatorListener;
// mc++ import android.support.v4.view.animation.LinearOutSlowInInterpolator;
// mc++ import android.util.Log;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ import android.view.animation.AccelerateInterpolator;
// mc++ 
// mc++ /**
// mc++  * Created on 2016/7/14.
// mc++  *
// mc++  * @author Yan Zhenjie.
// mc++  */
// mc++ public class AnimatorUtil {
// mc++ 
// mc++     public static  final String TAG="xujun";
// mc++ 
// mc++     public static final LinearOutSlowInInterpolator FAST_OUT_SLOW_IN_INTERPOLATOR = new LinearOutSlowInInterpolator();
// mc++ 
// mc++     // 显示view
// mc++     public static void scaleShow(View view, ViewPropertyAnimatorListener viewPropertyAnimatorListener) {
// mc++         view.setVisibility(View.VISIBLE);
// mc++         ViewCompat.animate(view)
// mc++                 .scaleX(1.0f)
// mc++                 .scaleY(1.0f)
// mc++                 .alpha(1.0f)
// mc++                 .setDuration(800)
// mc++                 .setListener(viewPropertyAnimatorListener)
// mc++                 .setInterpolator(FAST_OUT_SLOW_IN_INTERPOLATOR)
// mc++                 .start();
// mc++     }
// mc++ 
// mc++     // 隐藏view
// mc++     public static void scaleHide(View view, ViewPropertyAnimatorListener viewPropertyAnimatorListener) {
// mc++         ViewCompat.animate(view)
// mc++                 .scaleX(0.0f)
// mc++                 .scaleY(0.0f)
// mc++                 .alpha(0.0f)
// mc++                 .setDuration(800)
// mc++                 .setInterpolator(FAST_OUT_SLOW_IN_INTERPOLATOR)
// mc++                 .setListener(viewPropertyAnimatorListener)
// mc++                 .start();
// mc++     }
// mc++ 
// mc++     public static void tanslation(final View view,float  start,float end){
// mc++         final ValueAnimator animator=ValueAnimator.ofFloat(start,end);
// mc++         view.setVisibility(View.VISIBLE);
// mc++         animator.addUpdateListener(new ValueAnimator.AnimatorUpdateListener() {
// mc++             @Override
// mc++             public void onAnimationUpdate(ValueAnimator valueAnimator) {
// mc++                 Float value = (Float) animator.getAnimatedValue();
// mc++                 Log.i(TAG, "tanslation: value="+value);
// mc++                 view.setTranslationY(value);
// mc++             }
// mc++         });
// mc++         animator.setDuration(200);
// mc++         animator.setInterpolator(FAST_OUT_SLOW_IN_INTERPOLATOR);
// mc++         animator.start();
// mc++     }
// mc++ 
// mc++     public static void showHeight(final View view,float  start,float end){
// mc++         final ValueAnimator animator=ValueAnimator.ofFloat(start,end);
// mc++         final ViewGroup.LayoutParams layoutParams = view.getLayoutParams();
// mc++         animator.addUpdateListener(new ValueAnimator.AnimatorUpdateListener() {
// mc++             @Override
// mc++             public void onAnimationUpdate(ValueAnimator valueAnimator) {
// mc++                 float value = (Float) animator.getAnimatedValue();
// mc++                 layoutParams.height=(int) value;
// mc++                 view.setLayoutParams(layoutParams);
// mc++                 Log.i(TAG, "onAnimationUpdate: value="+value);
// mc++ 
// mc++             }
// mc++         });
// mc++         animator.setDuration(500);
// mc++         animator.setInterpolator(new AccelerateInterpolator());
// mc++         animator.start();
// mc++     }
// mc++ 
// mc++     public static void show(final View view,int  start,int end){
// mc++        final int height = view.getHeight();
// mc++         final ValueAnimator animator=ValueAnimator.ofInt(start,end);
// mc++         animator.addUpdateListener(new ValueAnimator.AnimatorUpdateListener() {
// mc++             @Override
// mc++             public void onAnimationUpdate(ValueAnimator valueAnimator) {
// mc++                 int value = (Integer) animator.getAnimatedValue();
// mc++                 Log.i(TAG, "onAnimationUpdate: value="+value);
// mc++                 view.setTop(value);
// mc++                 view.setBottom(value+height);
// mc++             }
// mc++         });
// mc++         view.setVisibility(View.VISIBLE);
// mc++         animator.setDuration(200);
// mc++         animator.setInterpolator(FAST_OUT_SLOW_IN_INTERPOLATOR);
// mc++         animator.start();
// mc++     }
// mc++ 
// mc++ }
