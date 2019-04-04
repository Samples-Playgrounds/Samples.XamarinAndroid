// mc++ package com.zhy.stickynavlayout.view;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.v4.view.NestedScrollingChild;
// mc++ import android.support.v4.view.NestedScrollingChildHelper;
// mc++ import android.support.v4.view.ViewCompat;
// mc++ import android.util.AttributeSet;
// mc++ import android.view.MotionEvent;
// mc++ import android.widget.LinearLayout;
// mc++ 
// mc++ public class MyNestedScrollChild extends LinearLayout implements NestedScrollingChild {
// mc++     private NestedScrollingChildHelper mScrollingChildHelper;
// mc++     private final int[] offset = new int[2];
// mc++     private final int[] consumed = new int[2];
// mc++     private int lastY;
// mc++     private int showHeight;
// mc++ 
// mc++     public MyNestedScrollChild(Context context) {
// mc++         super(context);
// mc++     }
// mc++ 
// mc++     public MyNestedScrollChild(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec) {
// mc++         //第一次测量，因为布局文件中高度是wrap_content，因此测量模式为ATMOST，即高度不能超过父控件的剩余空间
// mc++         super.onMeasure(widthMeasureSpec, heightMeasureSpec);
// mc++         showHeight = getMeasuredHeight();
// mc++ 
// mc++         //第二次测量，对高度没有任何限制，那么测量出来的就是完全展示内容所需要的高度
// mc++         heightMeasureSpec = MeasureSpec.makeMeasureSpec(0, MeasureSpec.UNSPECIFIED);
// mc++         super.onMeasure(widthMeasureSpec, heightMeasureSpec);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onTouchEvent(MotionEvent e) {
// mc++         switch (e.getAction()) {
// mc++             case MotionEvent.ACTION_DOWN:
// mc++                 lastY = (int) e.getRawY();
// mc++                 break;
// mc++             case MotionEvent.ACTION_MOVE:
// mc++                 int y = (int) (e.getRawY());
// mc++                 int dy = y - lastY;
// mc++                 lastY = y;
// mc++ 
// mc++                 if (startNestedScroll(ViewCompat.SCROLL_AXIS_HORIZONTAL) //如果找到了支持嵌套滚动的父类
// mc++                         && dispatchNestedPreScroll(0, dy, consumed, offset)) {//父类进行了一部分滚动
// mc++ 
// mc++                     int remain = dy - consumed[1];//获取滚动的剩余距离
// mc++                     if (remain != 0) {
// mc++                         scrollBy(0, -remain);
// mc++                     }
// mc++ 
// mc++                 } else {
// mc++                     scrollBy(0, -dy);
// mc++                 }
// mc++         }
// mc++ 
// mc++         return true;
// mc++     }
// mc++ 
// mc++     //scrollBy内部会调用scrollTo
// mc++     //限制滚动范围
// mc++     @Override
// mc++     public void scrollTo(int x, int y) {
// mc++         int MaxY = getMeasuredHeight() - showHeight;
// mc++         if (y > MaxY) {
// mc++             y = MaxY;
// mc++         }
// mc++         if (y < 0) {
// mc++             y = 0;
// mc++         }
// mc++         super.scrollTo(x, y);
// mc++     }
// mc++ 
// mc++     private NestedScrollingChildHelper getScrollingChildHelper() {
// mc++         if (mScrollingChildHelper == null) {
// mc++             mScrollingChildHelper = new NestedScrollingChildHelper(this);
// mc++             mScrollingChildHelper.setNestedScrollingEnabled(true);
// mc++         }
// mc++         return mScrollingChildHelper;
// mc++     }
// mc++ 
// mc++ //接口实现--------------------------------------------------
// mc++ 
// mc++     @Override
// mc++     public void setNestedScrollingEnabled(boolean enabled) {
// mc++         getScrollingChildHelper().setNestedScrollingEnabled(enabled);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean isNestedScrollingEnabled() {
// mc++         return getScrollingChildHelper().isNestedScrollingEnabled();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean startNestedScroll(int axes) {
// mc++         return getScrollingChildHelper().startNestedScroll(axes);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void stopNestedScroll() {
// mc++         getScrollingChildHelper().stopNestedScroll();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean hasNestedScrollingParent() {
// mc++         return getScrollingChildHelper().hasNestedScrollingParent();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean dispatchNestedScroll(int dxConsumed, int dyConsumed, int dxUnconsumed, int dyUnconsumed, int[] offsetInWindow) {
// mc++         return getScrollingChildHelper().dispatchNestedScroll(dxConsumed, dyConsumed, dxUnconsumed, dyUnconsumed, offsetInWindow);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean dispatchNestedPreScroll(int dx, int dy, int[] consumed, int[] offsetInWindow) {
// mc++         return getScrollingChildHelper().dispatchNestedPreScroll(dx, dy, consumed, offsetInWindow);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean dispatchNestedFling(float velocityX, float velocityY, boolean consumed) {
// mc++         return getScrollingChildHelper().dispatchNestedFling(velocityX, velocityY, consumed);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean dispatchNestedPreFling(float velocityX, float velocityY) {
// mc++         return getScrollingChildHelper().dispatchNestedPreFling(velocityX, velocityY);
// mc++     }
// mc++ }
