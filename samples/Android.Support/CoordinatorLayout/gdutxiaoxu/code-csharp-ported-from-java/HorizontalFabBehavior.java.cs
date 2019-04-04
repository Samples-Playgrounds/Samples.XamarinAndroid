// mc++ package com.xujun.contralayout.behavior;
// mc++ 
// mc++ import android.animation.Animator;
// mc++ import android.content.Context;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.v4.view.ViewCompat;
// mc++ import android.support.v4.view.animation.FastOutSlowInInterpolator;
// mc++ import android.util.AttributeSet;
// mc++ import android.util.Log;
// mc++ import android.view.View;
// mc++ import android.view.ViewPropertyAnimator;
// mc++ import android.view.animation.Interpolator;
// mc++ 
// mc++ /**
// mc++  *  FloatingActionButton behavior 向上向下隐藏的
// mc++  * @author xujun  on 2016/12/1.
// mc++  * @email gdutxiaoxu@163.com
// mc++  */
// mc++ 
// mc++ public class HorizontalFabBehavior extends CoordinatorLayout.Behavior<View> {
// mc++ 
// mc++     private static final Interpolator INTERPOLATOR = new FastOutSlowInInterpolator();
// mc++ 
// mc++     private float viewX;//控件距离coordinatorLayout底部距离
// mc++     private boolean isAnimate;//动画是否在进行
// mc++ 
// mc++     public HorizontalFabBehavior(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++     }
// mc++ 
// mc++     public static  final String TAG="xujun";
// mc++ 
// mc++     boolean isRight = true;
// mc++ 
// mc++     //在嵌套滑动开始前回调
// mc++     @Override
// mc++     public boolean onStartNestedScroll(CoordinatorLayout coordinatorLayout, View child, View directTargetChild, View target, int nestedScrollAxes) {
// mc++ 
// mc++         if(child.getVisibility() == View.VISIBLE&&viewX==0){
// mc++             //获取控件距离父布局（coordinatorLayout）底部距离
// mc++ 
// mc++             if(isRight){
// mc++                 viewX=coordinatorLayout.getWidth()-child.getLeft();
// mc++                 Log.i(TAG, "onStartNestedScroll: viewX=" +viewX);
// mc++             }else{
// mc++                 viewX=-child.getRight();
// mc++             }
// mc++ 
// mc++         }
// mc++ 
// mc++         return (nestedScrollAxes & ViewCompat.SCROLL_AXIS_VERTICAL) != 0;//判断是否竖直滚动
// mc++     }
// mc++ 
// mc++     //在嵌套滑动进行时，对象消费滚动距离前回调
// mc++     @Override
// mc++     public void onNestedPreScroll(CoordinatorLayout coordinatorLayout, View child, View target, int dx, int dy, int[] consumed) {
// mc++         //dy大于0是向上滚动 小于0是向下滚动
// mc++ 
// mc++         if (dy >=0&&!isAnimate&&child.getVisibility()==View.VISIBLE) {
// mc++             hide(child);
// mc++         } else if (dy <0&&!isAnimate&&child.getVisibility()==View.GONE) {
// mc++             show(child);
// mc++         }
// mc++     }
// mc++ 
// mc++     //隐藏时的动画
// mc++     private void hide(final View view) {
// mc++         ViewPropertyAnimator animator = view.animate().translationX(viewX).
// mc++                 setInterpolator(INTERPOLATOR).setDuration(200);
// mc++ 
// mc++         animator.setListener(new Animator.AnimatorListener() {
// mc++             @Override
// mc++             public void onAnimationStart(Animator animator) {
// mc++                 isAnimate=true;
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onAnimationEnd(Animator animator) {
// mc++                 view.setVisibility(View.GONE);
// mc++                 isAnimate=false;
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onAnimationCancel(Animator animator) {
// mc++                 show(view);
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onAnimationRepeat(Animator animator) {
// mc++             }
// mc++         });
// mc++         animator.start();
// mc++     }
// mc++ 
// mc++     //显示时的动画
// mc++     private void show(final View view) {
// mc++         ViewPropertyAnimator animator = view.animate().translationX(0).setInterpolator(INTERPOLATOR).
// mc++                 setDuration(200);
// mc++         animator.setListener(new Animator.AnimatorListener() {
// mc++             @Override
// mc++             public void onAnimationStart(Animator animator) {
// mc++                 view.setVisibility(View.VISIBLE);
// mc++                 isAnimate=true;
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onAnimationEnd(Animator animator) {
// mc++                 isAnimate=false;
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onAnimationCancel(Animator animator) {
// mc++                 hide(view);
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onAnimationRepeat(Animator animator) {
// mc++             }
// mc++         });
// mc++         animator.start();
// mc++     }
// mc++ }
