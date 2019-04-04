// mc++ package com.xujun.contralayout.UI.weibo.behavior;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ 
// mc++ import android.util.AttributeSet;
// mc++ import android.view.View;
// mc++ 
// mc++ /**
// mc++  * Copy from Android design library
// mc++  * <p>
// mc++  * Behavior will automatically sets up a {@link ViewOffsetHelper} on a {@link View}.
// mc++  */
// mc++ public class ViewOffsetBehavior<V extends View> extends CoordinatorLayout.Behavior<V> {
// mc++ 
// mc++     private ViewOffsetHelper mViewOffsetHelper;
// mc++ 
// mc++     private int mTempTopBottomOffset = 0;
// mc++     private int mTempLeftRightOffset = 0;
// mc++ 
// mc++     public ViewOffsetBehavior() {
// mc++     }
// mc++ 
// mc++     public ViewOffsetBehavior(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onLayoutChild(CoordinatorLayout parent, V child, int layoutDirection) {
// mc++         // First let lay the child out
// mc++         layoutChild(parent, child, layoutDirection);
// mc++ 
// mc++         if (mViewOffsetHelper == null) {
// mc++             mViewOffsetHelper = new ViewOffsetHelper(child);
// mc++         }
// mc++         mViewOffsetHelper.onViewLayout();
// mc++ 
// mc++         if (mTempTopBottomOffset != 0) {
// mc++             mViewOffsetHelper.setTopAndBottomOffset(mTempTopBottomOffset);
// mc++             mTempTopBottomOffset = 0;
// mc++         }
// mc++         if (mTempLeftRightOffset != 0) {
// mc++             mViewOffsetHelper.setLeftAndRightOffset(mTempLeftRightOffset);
// mc++             mTempLeftRightOffset = 0;
// mc++         }
// mc++ 
// mc++         return true;
// mc++     }
// mc++ 
// mc++     protected void layoutChild(CoordinatorLayout parent, V child, int layoutDirection) {
// mc++         // Let the parent lay it out by default
// mc++         parent.onLayoutChild(child, layoutDirection);
// mc++     }
// mc++ 
// mc++     public boolean setTopAndBottomOffset(int offset) {
// mc++         if (mViewOffsetHelper != null) {
// mc++             return mViewOffsetHelper.setTopAndBottomOffset(offset);
// mc++         } else {
// mc++             mTempTopBottomOffset = offset;
// mc++         }
// mc++         return false;
// mc++     }
// mc++ 
// mc++     public boolean setLeftAndRightOffset(int offset) {
// mc++         if (mViewOffsetHelper != null) {
// mc++             return mViewOffsetHelper.setLeftAndRightOffset(offset);
// mc++         } else {
// mc++             mTempLeftRightOffset = offset;
// mc++         }
// mc++         return false;
// mc++     }
// mc++ 
// mc++     public int getTopAndBottomOffset() {
// mc++         return mViewOffsetHelper != null ? mViewOffsetHelper.getTopAndBottomOffset() : 0;
// mc++     }
// mc++ 
// mc++     public int getLeftAndRightOffset() {
// mc++         return mViewOffsetHelper != null ? mViewOffsetHelper.getLeftAndRightOffset() : 0;
// mc++     }
// mc++ }
