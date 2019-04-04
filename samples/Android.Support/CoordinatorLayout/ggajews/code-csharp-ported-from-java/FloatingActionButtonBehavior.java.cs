// mc++ package com.getbase.coordinatorlayoutdemo;
// mc++ 
// mc++ import com.getbase.floatingactionbutton.FloatingActionButton;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.design.widget.Snackbar.SnackbarLayout;
// mc++ import android.util.AttributeSet;
// mc++ import android.view.View;
// mc++ 
// mc++ public class FloatingActionButtonBehavior extends CoordinatorLayout.Behavior<FloatingActionButton> {
// mc++ 
// mc++   public FloatingActionButtonBehavior(Context context, AttributeSet attrs) {
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public boolean layoutDependsOn(CoordinatorLayout parent, FloatingActionButton child, View dependency) {
// mc++     return dependency instanceof SnackbarLayout;
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public boolean onDependentViewChanged(CoordinatorLayout parent, FloatingActionButton child, View dependency) {
// mc++     float translationY = Math.min(0, dependency.getTranslationY() - dependency.getHeight());
// mc++     child.setTranslationY(translationY);
// mc++     return true;
// mc++   }
// mc++ }
