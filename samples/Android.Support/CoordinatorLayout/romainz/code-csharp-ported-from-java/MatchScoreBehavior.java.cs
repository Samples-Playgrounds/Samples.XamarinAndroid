// mc++ package com.zanon.sample.coordinatorlayout.behaviors;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.content.res.Resources;
// mc++ import android.content.res.TypedArray;
// mc++ import android.support.design.widget.AppBarLayout;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.util.AttributeSet;
// mc++ import android.view.View;
// mc++ import android.widget.TextView;
// mc++ 
// mc++ import com.zanon.coordinatorlayout.R;
// mc++ 
// mc++ /**
// mc++  * Created by romain on 26/10/15.
// mc++  */
// mc++ public class MatchScoreBehavior extends CoordinatorLayout.Behavior<TextView> {
// mc++ 
// mc++     private static final float TOOLBAR_SCALE = 0.7f;
// mc++ 
// mc++     private final Context mContext;
// mc++     private int mToolbarHeight;
// mc++     private int mMaxScrollAppBar;
// mc++     private float mTextSizeMax;
// mc++ 
// mc++     /**
// mc++      * Constructor
// mc++      *
// mc++      * @param context Context
// mc++      * @param attrs   AttributeSet
// mc++      */
// mc++     public MatchScoreBehavior(Context context, AttributeSet attrs) {
// mc++         mContext = context;
// mc++         init(context);
// mc++     }
// mc++ 
// mc++     private void init(Context context) {
// mc++         Resources resources = context.getResources();
// mc++         mTextSizeMax = resources.getDimension(R.dimen.custom_behavior_text_size);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean layoutDependsOn(CoordinatorLayout parent, TextView child, View dependency) {
// mc++         return dependency instanceof AppBarLayout;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onDependentViewChanged(CoordinatorLayout parent, TextView child, View dependency) {
// mc++         AppBarLayout appBarLayout = (AppBarLayout) dependency;
// mc++         shouldInitProperties(child, appBarLayout);
// mc++ 
// mc++         int currentScroll = dependency.getBottom();
// mc++         float percentage = (float) currentScroll / (float) mMaxScrollAppBar;
// mc++         if (percentage < TOOLBAR_SCALE) {
// mc++             percentage = TOOLBAR_SCALE;
// mc++         }
// mc++         child.setScaleY(percentage);
// mc++         child.setScaleX(percentage);
// mc++         return true;
// mc++     }
// mc++ 
// mc++     private void shouldInitProperties(TextView child, AppBarLayout appBarLayout) {
// mc++         if (mMaxScrollAppBar == 0) {
// mc++             mMaxScrollAppBar = appBarLayout.getTotalScrollRange();
// mc++         }
// mc++         if (mToolbarHeight == 0) {
// mc++             TypedArray styledAttributes = mContext.getTheme().obtainStyledAttributes(
// mc++                     new int[]{android.R.attr.actionBarSize});
// mc++             mToolbarHeight = (int) styledAttributes.getDimension(0, 0);
// mc++             styledAttributes.recycle();
// mc++         }
// mc++     }
// mc++ }
