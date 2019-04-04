// mc++ package com.zhy.stickynavlayout.view;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.v4.view.NestedScrollingParent;
// mc++ import android.support.v4.view.NestedScrollingParentHelper;
// mc++ import android.support.v4.view.ViewCompat;
// mc++ import android.support.v4.view.ViewPager;
// mc++ import android.util.AttributeSet;
// mc++ import android.util.Log;
// mc++ import android.view.VelocityTracker;
// mc++ import android.view.View;
// mc++ import android.view.ViewConfiguration;
// mc++ import android.view.ViewGroup;
// mc++ import android.widget.LinearLayout;
// mc++ import android.widget.OverScroller;
// mc++ 
// mc++ import com.zhy.stickynavlayout.R;
// mc++ 
// mc++ 
// mc++ public class StickyNavLayout extends LinearLayout implements NestedScrollingParent {
// mc++ 
// mc++     private NestedScrollingParentHelper parentHelper = new NestedScrollingParentHelper(this);
// mc++     private View mTop;
// mc++     private View mNav;
// mc++     private ViewPager mViewPager;
// mc++     private int mTopViewHeight;
// mc++     private OverScroller mScroller;
// mc++     private VelocityTracker mVelocityTracker;
// mc++     private int mTouchSlop;
// mc++     private int mMaximumVelocity, mMinimumVelocity;
// mc++     private float mLastY;
// mc++     private boolean mDragging;
// mc++ 
// mc++     public StickyNavLayout(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++         setOrientation(LinearLayout.VERTICAL);
// mc++ 
// mc++         mScroller = new OverScroller(context);
// mc++         mTouchSlop = ViewConfiguration.get(context).getScaledTouchSlop();
// mc++         mMaximumVelocity = ViewConfiguration.get(context).getScaledMaximumFlingVelocity();
// mc++         mMinimumVelocity = ViewConfiguration.get(context).getScaledMinimumFlingVelocity();
// mc++     }
// mc++ 
// mc++ 
// mc++     @Override
// mc++     protected void onFinishInflate() {
// mc++         super.onFinishInflate();
// mc++         mTop = findViewById(R.id.id_stickynavlayout_topview);
// mc++         mNav = findViewById(R.id.id_stickynavlayout_indicator);
// mc++         mViewPager = (ViewPager) findViewById(R.id.id_stickynavlayout_viewpager);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec) {
// mc++         super.onMeasure(widthMeasureSpec, heightMeasureSpec);
// mc++ 
// mc++         mTopViewHeight = mTop.getMeasuredHeight();
// mc++ 
// mc++         //上面测量的结果是viewPager的高度只能占满父控件的剩余空间
// mc++         //重新设置viewPager的高度
// mc++         ViewGroup.LayoutParams layoutParams = mViewPager.getLayoutParams();
// mc++         layoutParams.height = getMeasuredHeight() - mNav.getMeasuredHeight();
// mc++         mViewPager.setLayoutParams(layoutParams);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void scrollTo(int x, int y) {
// mc++         //限制滚动范围
// mc++         if (y < 0) {
// mc++             y = 0;
// mc++         }
// mc++         if (y > mTopViewHeight) {
// mc++             y = mTopViewHeight;
// mc++         }
// mc++ 
// mc++         super.scrollTo(x, y);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void computeScroll() {
// mc++         if (mScroller.computeScrollOffset()) {
// mc++             scrollTo(0, mScroller.getCurrY());
// mc++             invalidate();
// mc++         }
// mc++     }
// mc++ 
// mc++     public void fling(int velocityY) {
// mc++         mScroller.fling(0, getScrollY(), 0, velocityY, 0, 0, 0, mTopViewHeight);
// mc++         invalidate();
// mc++     }
// mc++ 
// mc++ //实现NestedScrollParent接口-------------------------------------------------------------------------
// mc++ 
// mc++     @Override
// mc++     public boolean onStartNestedScroll(View child, View target, int nestedScrollAxes) {
// mc++         return true;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onNestedScrollAccepted(View child, View target, int nestedScrollAxes) {
// mc++         parentHelper.onNestedScrollAccepted(child, target, nestedScrollAxes);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onStopNestedScroll(View target) {
// mc++         parentHelper.onStopNestedScroll(target);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onNestedScroll(View target, int dxConsumed, int dyConsumed, int dxUnconsumed, int dyUnconsumed) {
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onNestedPreScroll(View target, int dx, int dy, int[] consumed) {
// mc++         boolean hiddenTop = dy > 0 && getScrollY() < mTopViewHeight;
// mc++         boolean showTop = dy < 0 && getScrollY() >= 0 && !ViewCompat.canScrollVertically(target, -1);
// mc++ 
// mc++         if (hiddenTop || showTop) {
// mc++             scrollBy(0, dy);
// mc++             consumed[1] = dy;
// mc++         }
// mc++     }
// mc++ 
// mc++     //boolean consumed:子view是否消耗了fling
// mc++     //返回值：自己是否消耗了fling。可见，要消耗只能全部消耗
// mc++     @Override
// mc++     public boolean onNestedFling(View target, float velocityX, float velocityY, boolean consumed) {
// mc++         Log.e("onNestedFling", "called");
// mc++         return false;
// mc++     }
// mc++ 
// mc++     //返回值：自己是否消耗了fling。可见，要消耗只能全部消耗
// mc++     @Override
// mc++     public boolean onNestedPreFling(View target, float velocityX, float velocityY) {
// mc++         Log.e("onNestedPreFling", "called");
// mc++         if (getScrollY() < mTopViewHeight) {
// mc++             fling((int) velocityY);
// mc++             return true;
// mc++         } else {
// mc++             return false;
// mc++         }
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getNestedScrollAxes() {
// mc++         return parentHelper.getNestedScrollAxes();
// mc++     }
// mc++ }
