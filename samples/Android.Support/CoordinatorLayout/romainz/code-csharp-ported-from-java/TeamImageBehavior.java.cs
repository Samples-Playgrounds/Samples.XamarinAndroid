// mc++ package com.zanon.sample.coordinatorlayout.behaviors;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.content.res.TypedArray;
// mc++ import android.support.design.widget.AppBarLayout;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.util.AttributeSet;
// mc++ import android.view.View;
// mc++ import android.widget.ImageView;
// mc++ 
// mc++ import com.zanon.coordinatorlayout.R;
// mc++ 
// mc++ @SuppressWarnings("unused")
// mc++ public class TeamImageBehavior extends CoordinatorLayout.Behavior<ImageView> {
// mc++ 
// mc++     private final Context mContext;
// mc++     private int mToolbarHeight;
// mc++     private int mMaxScrollAppBar;
// mc++     private float mImageSizeToolbar;
// mc++     private float mImageSizeMax;
// mc++ 
// mc++     /**
// mc++      * Constructor
// mc++      *
// mc++      * @param context Context
// mc++      * @param attrs   AttributeSet
// mc++      */
// mc++     public TeamImageBehavior(Context context, AttributeSet attrs) {
// mc++         mContext = context;
// mc++         init();
// mc++     }
// mc++ 
// mc++     private void init() {
// mc++         mImageSizeToolbar = mContext.getResources().getDimensionPixelSize(R.dimen.custom_behavior_image_size_toolbar);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean layoutDependsOn(CoordinatorLayout parent, ImageView child, View dependency) {
// mc++         return dependency instanceof AppBarLayout;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onDependentViewChanged(CoordinatorLayout parent, ImageView child, View dependency) {
// mc++         AppBarLayout appBarLayout = (AppBarLayout) dependency;
// mc++         shouldInitProperties(child, appBarLayout);
// mc++ 
// mc++         int currentScroll = dependency.getBottom();
// mc++         if (currentScroll < mToolbarHeight) {
// mc++             currentScroll = mToolbarHeight;
// mc++         }
// mc++         float percentage = currentScroll * 100 / mMaxScrollAppBar;
// mc++         float currentImageDelta = percentage * (mImageSizeMax - mImageSizeToolbar) / 100;
// mc++         CoordinatorLayout.LayoutParams params = (CoordinatorLayout.LayoutParams) child.getLayoutParams();
// mc++         params.width = (int) (mImageSizeToolbar + currentImageDelta);
// mc++         params.height = (int) (mImageSizeToolbar + currentImageDelta);
// mc++         child.setLayoutParams(params);
// mc++         return true;
// mc++     }
// mc++ 
// mc++     private void shouldInitProperties(ImageView child, AppBarLayout appBarLayout) {
// mc++         if (mImageSizeMax == 0) {
// mc++             mImageSizeMax = child.getHeight();
// mc++         }
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
// mc++ 
// mc++ }
