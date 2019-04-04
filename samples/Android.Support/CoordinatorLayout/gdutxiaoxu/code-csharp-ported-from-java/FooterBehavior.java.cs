// mc++ package com.xujun.contralayout.behavior;
// mc++ 
// mc++ import android.animation.Animator;
// mc++ import android.content.Context;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.v4.view.ViewCompat;
// mc++ import android.support.v4.view.animation.FastOutSlowInInterpolator;
// mc++ import android.util.AttributeSet;
// mc++ import android.view.View;
// mc++ import android.view.ViewPropertyAnimator;
// mc++ import android.view.animation.Interpolator;
// mc++ 
// mc++ /**
// mc++  * 知乎效果底部 behavior
// mc++  *
// mc++  * @author xujun  on 2016/11/30.
// mc++  * @email gdutxiaoxu@163.com
// mc++  */
// mc++ 
// mc++ public class FooterBehavior extends CoordinatorLayout.Behavior<View> {
// mc++ 
// mc++     private static final Interpolator INTERPOLATOR = new FastOutSlowInInterpolator();
// mc++ 
// mc++     private int sinceDirectionChange;
// mc++ 
// mc++     public FooterBehavior(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++     }
// mc++ 
// mc++     //1.判断滑动的方向 我们需要垂直滑动
// mc++     @Override
// mc++     public boolean onStartNestedScroll(CoordinatorLayout coordinatorLayout, View child,
// mc++                                        View directTargetChild, View target, int nestedScrollAxes) {
// mc++         return (nestedScrollAxes & ViewCompat.SCROLL_AXIS_VERTICAL) != 0;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onNestedScroll(CoordinatorLayout coordinatorLayout, View child, View target, int
// mc++             dxConsumed, int dyConsumed, int dxUnconsumed, int dyUnconsumed) {
// mc++         super.onNestedScroll(coordinatorLayout, child, target, dxConsumed, dyConsumed,
// mc++                 dxUnconsumed, dyUnconsumed);
// mc++     }
// mc++ 
// mc++     //2.根据滑动的距离显示和隐藏footer view
// mc++     @Override
// mc++     public void onNestedPreScroll(CoordinatorLayout coordinatorLayout, View child,
// mc++                                   View target, int dx, int dy, int[] consumed) {
// mc++         if (dy > 0 && sinceDirectionChange < 0 || dy < 0 && sinceDirectionChange > 0) {
// mc++             child.animate().cancel();
// mc++             sinceDirectionChange = 0;
// mc++         }
// mc++         sinceDirectionChange += dy;
// mc++         int visibility = child.getVisibility();
// mc++         if (sinceDirectionChange > child.getHeight() && visibility == View.VISIBLE) {
// mc++             hide(child);
// mc++         } else {
// mc++             if (sinceDirectionChange < 0 ) {
// mc++                 show(child);
// mc++             }
// mc++         }
// mc++     }
// mc++ 
// mc++     private void hide(final View view) {
// mc++         ViewPropertyAnimator animator = view.animate().translationY(view.getHeight()).
// mc++                 setInterpolator(INTERPOLATOR).setDuration(200);
// mc++         animator.setListener(new Animator.AnimatorListener() {
// mc++             @Override
// mc++             public void onAnimationStart(Animator animator) {
// mc++ 
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onAnimationEnd(Animator animator) {
// mc++ //                view.setVisibility(View.GONE);
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onAnimationCancel(Animator animator) {
// mc++                 show(view);
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onAnimationRepeat(Animator animator) {
// mc++ 
// mc++             }
// mc++         });
// mc++         animator.start();
// mc++     }
// mc++ 
// mc++     private void show(final View view) {
// mc++         ViewPropertyAnimator animator = view.animate().translationY(0).
// mc++                 setInterpolator(INTERPOLATOR).
// mc++                 setDuration(200);
// mc++         animator.setListener(new Animator.AnimatorListener() {
// mc++             @Override
// mc++             public void onAnimationStart(Animator animator) {
// mc++ 
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onAnimationEnd(Animator animator) {
// mc++                 view.setVisibility(View.VISIBLE);
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onAnimationCancel(Animator animator) {
// mc++                 hide(view);
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onAnimationRepeat(Animator animator) {
// mc++ 
// mc++             }
// mc++         });
// mc++         animator.start();
// mc++ 
// mc++     }
// mc++ }
