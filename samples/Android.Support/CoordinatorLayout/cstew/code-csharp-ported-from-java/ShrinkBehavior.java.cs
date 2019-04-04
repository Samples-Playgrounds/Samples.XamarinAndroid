// mc++ package com.bignerdranch.android.custombehavior;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.design.widget.FloatingActionButton;
// mc++ import android.support.design.widget.Snackbar;
// mc++ import android.support.v4.view.ViewCompat;
// mc++ import android.util.AttributeSet;
// mc++ import android.view.View;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ public class ShrinkBehavior extends CoordinatorLayout.Behavior<FloatingActionButton> {
// mc++ 
// mc++     public ShrinkBehavior() { }
// mc++ 
// mc++     public ShrinkBehavior(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean layoutDependsOn(CoordinatorLayout parent, FloatingActionButton child, View dependency) {
// mc++         return dependency instanceof Snackbar.SnackbarLayout;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onDependentViewChanged(CoordinatorLayout parent, FloatingActionButton child, View dependency) {
// mc++         float translationY = getFabTranslationYForSnackbar(parent, child);
// mc++         float percentComplete = -translationY / dependency.getHeight();
// mc++         float scaleFactor = 1 - percentComplete;
// mc++ 
// mc++         child.setScaleX(scaleFactor);
// mc++         child.setScaleY(scaleFactor);
// mc++         return false;
// mc++     }
// mc++ 
// mc++     private float getFabTranslationYForSnackbar(CoordinatorLayout parent,
// mc++                                                 FloatingActionButton fab) {
// mc++         float minOffset = 0;
// mc++         final List<View> dependencies = parent.getDependencies(fab);
// mc++         for (int i = 0, z = dependencies.size(); i < z; i++) {
// mc++             final View view = dependencies.get(i);
// mc++             if (view instanceof Snackbar.SnackbarLayout && parent.doViewsOverlap(fab, view)) {
// mc++                 minOffset = Math.min(minOffset,
// mc++                         ViewCompat.getTranslationY(view) - view.getHeight());
// mc++             }
// mc++         }
// mc++ 
// mc++         return minOffset;
// mc++     }
// mc++ }
