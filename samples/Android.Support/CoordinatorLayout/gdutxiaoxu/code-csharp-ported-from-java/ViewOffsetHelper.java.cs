// mc++ package com.xujun.contralayout.UI.weibo.behavior;
// mc++ 
// mc++ 
// mc++ import android.os.Build;
// mc++ import android.support.v4.view.ViewCompat;
// mc++ import android.view.View;
// mc++ import android.view.ViewParent;
// mc++ 
// mc++ /**
// mc++  * Copy from Android design library
// mc++  * <p>
// mc++  * Utility helper for moving a {@link View} around using
// mc++  * {@link View#offsetLeftAndRight(int)} and
// mc++  * {@link View#offsetTopAndBottom(int)}.
// mc++  * <p>
// mc++  * Also the setting of absolute offsets (similar to translationX/Y), rather than additive
// mc++  * offsets.
// mc++  */
// mc++ public class ViewOffsetHelper {
// mc++ 
// mc++     private final View mView;
// mc++ 
// mc++     private int mLayoutTop;
// mc++     private int mLayoutLeft;
// mc++     private int mOffsetTop;
// mc++     private int mOffsetLeft;
// mc++ 
// mc++     public ViewOffsetHelper(View view) {
// mc++         mView = view;
// mc++     }
// mc++ 
// mc++     public void onViewLayout() {
// mc++         // Now grab the intended top
// mc++         mLayoutTop = mView.getTop();
// mc++         mLayoutLeft = mView.getLeft();
// mc++ 
// mc++         // And offset it as needed
// mc++         updateOffsets();
// mc++     }
// mc++ 
// mc++     private void updateOffsets() {
// mc++         ViewCompat.offsetTopAndBottom(mView, mOffsetTop - (mView.getTop() - mLayoutTop));
// mc++         ViewCompat.offsetLeftAndRight(mView, mOffsetLeft - (mView.getLeft() - mLayoutLeft));
// mc++ 
// mc++         // Manually invalidate the view and parent to make sure we get drawn pre-M
// mc++         if (Build.VERSION.SDK_INT < 23) {
// mc++             tickleInvalidationFlag(mView);
// mc++             final ViewParent vp = mView.getParent();
// mc++             if (vp instanceof View) {
// mc++                 tickleInvalidationFlag((View) vp);
// mc++             }
// mc++         }
// mc++     }
// mc++ 
// mc++     private static void tickleInvalidationFlag(View view) {
// mc++         final float y = ViewCompat.getTranslationY(view);
// mc++         ViewCompat.setTranslationY(view, y + 1);
// mc++         ViewCompat.setTranslationY(view, y);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Set the top and bottom offset for this {@link ViewOffsetHelper}'s view.
// mc++      *
// mc++      * @param offset the offset in px.
// mc++      * @return true if the offset has changed
// mc++      */
// mc++     public boolean setTopAndBottomOffset(int offset) {
// mc++         if (mOffsetTop != offset) {
// mc++             mOffsetTop = offset;
// mc++             updateOffsets();
// mc++             return true;
// mc++         }
// mc++         return false;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Set the left and right offset for this {@link ViewOffsetHelper}'s view.
// mc++      *
// mc++      * @param offset the offset in px.
// mc++      * @return true if the offset has changed
// mc++      */
// mc++     public boolean setLeftAndRightOffset(int offset) {
// mc++         if (mOffsetLeft != offset) {
// mc++             mOffsetLeft = offset;
// mc++             updateOffsets();
// mc++             return true;
// mc++         }
// mc++         return false;
// mc++     }
// mc++ 
// mc++     public int getTopAndBottomOffset() {
// mc++         return mOffsetTop;
// mc++     }
// mc++ 
// mc++     public int getLeftAndRightOffset() {
// mc++         return mOffsetLeft;
// mc++     }
// mc++ }
