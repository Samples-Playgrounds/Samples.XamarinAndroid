// mc++ package com.xujun.contralayout.UI.weibo.behavior;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.graphics.Rect;
// mc++ import android.support.design.widget.AppBarLayout;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.v4.view.GravityCompat;
// mc++ import android.support.v4.view.ViewCompat;
// mc++ import android.util.AttributeSet;
// mc++ import android.view.Gravity;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ /**
// mc++  * Copy from Android design library
// mc++  * <p/>
// mc++  * Created by xujun
// mc++  */
// mc++ public abstract class HeaderScrollingViewBehavior extends ViewOffsetBehavior<View> {
// mc++     private final Rect mTempRect1 = new Rect();
// mc++     private final Rect mTempRect2 = new Rect();
// mc++ 
// mc++     private int mVerticalLayoutGap = 0;
// mc++     private int mOverlayTop;
// mc++ 
// mc++     public HeaderScrollingViewBehavior() {
// mc++     }
// mc++ 
// mc++     public HeaderScrollingViewBehavior(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onMeasureChild(CoordinatorLayout parent, View child, int parentWidthMeasureSpec, int widthUsed, int parentHeightMeasureSpec, int heightUsed) {
// mc++         final int childLpHeight = child.getLayoutParams().height;
// mc++         if (childLpHeight == ViewGroup.LayoutParams.MATCH_PARENT || childLpHeight == ViewGroup.LayoutParams.WRAP_CONTENT) {
// mc++             // If the menu's height is set to match_parent/wrap_content then measure it
// mc++             // with the maximum visible height
// mc++ 
// mc++             final List<View> dependencies = parent.getDependencies(child);
// mc++             final View header = findFirstDependency(dependencies);
// mc++             if (header != null) {
// mc++                 if (ViewCompat.getFitsSystemWindows(header) && !ViewCompat.getFitsSystemWindows(child)) {
// mc++                     // If the header is fitting system windows then we need to also,
// mc++                     // otherwise we'll get CoL's compatible measuring
// mc++                     ViewCompat.setFitsSystemWindows(child, true);
// mc++ 
// mc++                     if (ViewCompat.getFitsSystemWindows(child)) {
// mc++                         // If the set succeeded, trigger a new layout and return true
// mc++                         child.requestLayout();
// mc++                         return true;
// mc++                     }
// mc++                 }
// mc++ 
// mc++                 if (ViewCompat.isLaidOut(header)) {
// mc++                     int availableHeight = View.MeasureSpec.getSize(parentHeightMeasureSpec);
// mc++                     if (availableHeight == 0) {
// mc++                         // If the measure spec doesn't specify a size, use the current height
// mc++                         availableHeight = parent.getHeight();
// mc++                     }
// mc++ 
// mc++                     final int height = availableHeight - header.getMeasuredHeight() + getScrollRange(header);
// mc++                     final int heightMeasureSpec = View.MeasureSpec.makeMeasureSpec(height,
// mc++                             childLpHeight == ViewGroup.LayoutParams.MATCH_PARENT ? View.MeasureSpec.EXACTLY : View.MeasureSpec.AT_MOST);
// mc++ 
// mc++                     // Now measure the scrolling view with the correct height
// mc++                     parent.onMeasureChild(child, parentWidthMeasureSpec, widthUsed, heightMeasureSpec, heightUsed);
// mc++ 
// mc++                     return true;
// mc++                 }
// mc++             }
// mc++         }
// mc++         return false;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void layoutChild(final CoordinatorLayout parent, final View child, final int layoutDirection) {
// mc++         final List<View> dependencies = parent.getDependencies(child);
// mc++         final View header = findFirstDependency(dependencies);
// mc++ 
// mc++         if (header != null) {
// mc++             final CoordinatorLayout.LayoutParams lp = (CoordinatorLayout.LayoutParams) child.getLayoutParams();
// mc++             final Rect available = mTempRect1;
// mc++             available.set(parent.getPaddingLeft() + lp.leftMargin, header.getBottom() + lp.topMargin,
// mc++                     parent.getWidth() - parent.getPaddingRight() - lp.rightMargin,
// mc++                     parent.getHeight() + header.getBottom() - parent.getPaddingBottom() - lp.bottomMargin);
// mc++ 
// mc++             final Rect out = mTempRect2;
// mc++             GravityCompat.apply(resolveGravity(lp.gravity), child.getMeasuredWidth(), child.getMeasuredHeight(), available, out, layoutDirection);
// mc++ 
// mc++             final int overlap = getOverlapPixelsForOffset(header);
// mc++ 
// mc++             child.layout(out.left, out.top - overlap, out.right, out.bottom - overlap);
// mc++             mVerticalLayoutGap = out.top - header.getBottom();
// mc++         } else {
// mc++             // If we don't have a dependency, let super handle it
// mc++             super.layoutChild(parent, child, layoutDirection);
// mc++             mVerticalLayoutGap = 0;
// mc++         }
// mc++     }
// mc++ 
// mc++     float getOverlapRatioForOffset(final View header) {
// mc++         return 1f;
// mc++     }
// mc++ 
// mc++     final int getOverlapPixelsForOffset(final View header) {
// mc++         return mOverlayTop == 0
// mc++                 ? 0
// mc++                 : MathUtils.constrain(Math.round(getOverlapRatioForOffset(header) * mOverlayTop),
// mc++                 0, mOverlayTop);
// mc++ 
// mc++     }
// mc++ 
// mc++     private static int resolveGravity(int gravity) {
// mc++         return gravity == Gravity.NO_GRAVITY ? GravityCompat.START | Gravity.TOP : gravity;
// mc++     }
// mc++ 
// mc++     protected abstract View findFirstDependency(List<View> views);
// mc++ 
// mc++     protected int getScrollRange(View v) {
// mc++         return v.getMeasuredHeight();
// mc++     }
// mc++ 
// mc++     /**
// mc++      * The gap between the top of the scrolling view and the bottom of the header layout in pixels.
// mc++      */
// mc++     final int getVerticalLayoutGap() {
// mc++         return mVerticalLayoutGap;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Set the distance that this view should overlap any {@link AppBarLayout}.
// mc++      *
// mc++      * @param overlayTop the distance in px
// mc++      */
// mc++     public final void setOverlayTop(int overlayTop) {
// mc++         mOverlayTop = overlayTop;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Returns the distance that this view should overlap any {@link AppBarLayout}.
// mc++      */
// mc++     public final int getOverlayTop() {
// mc++         return mOverlayTop;
// mc++     }
// mc++ 
// mc++ }
