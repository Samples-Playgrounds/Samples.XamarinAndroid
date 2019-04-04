// mc++ package com.zanon.sample.coordinatorlayout.behaviors;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.design.widget.FloatingActionButton;
// mc++ import android.support.design.widget.Snackbar;
// mc++ import android.util.AttributeSet;
// mc++ import android.view.View;
// mc++ 
// mc++ /**
// mc++  * Created by romain on 27/10/15.
// mc++  */
// mc++ public class SnackbarBehavior extends FloatingActionButton.Behavior {
// mc++ 
// mc++     public SnackbarBehavior(Context context, AttributeSet attrs) {
// mc++         super();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean layoutDependsOn(CoordinatorLayout parent, FloatingActionButton child, View dependency) {
// mc++         return dependency instanceof Snackbar.SnackbarLayout;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onDependentViewChanged(CoordinatorLayout parent, FloatingActionButton child, View dependency) {
// mc++         float translationY = Math.min(0, dependency.getTranslationY() - dependency.getHeight());
// mc++         child.setTranslationY(translationY);
// mc++         return true;
// mc++     }
// mc++ }
