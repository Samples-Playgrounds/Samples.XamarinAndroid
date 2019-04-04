// mc++ package com.xujun.contralayout.UI.weibo.weight;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.annotation.Nullable;
// mc++ import android.support.v4.view.NestedScrollingChild;
// mc++ import android.support.v4.view.NestedScrollingChildHelper;
// mc++ import android.support.v4.view.ViewCompat;
// mc++ import android.util.AttributeSet;
// mc++ import android.util.Log;
// mc++ import android.view.MotionEvent;
// mc++ import android.widget.LinearLayout;
// mc++ 
// mc++ /**
// mc++  * @author meitu.xujun  on 2017/5/2 14:43
// mc++  * @version 0.1
// mc++  */
// mc++ 
// mc++ public class NestedLinearLayout extends LinearLayout implements NestedScrollingChild {
// mc++ 
// mc++     private static final String TAG = "NestedLinearLayout";
// mc++ 
// mc++     private final int[] offset = new int[2];
// mc++     private final int[] consumed = new int[2];
// mc++ 
// mc++     private NestedScrollingChildHelper mScrollingChildHelper;
// mc++     private int lastY;
// mc++ 
// mc++     public NestedLinearLayout(Context context) {
// mc++         this(context, null);
// mc++     }
// mc++ 
// mc++     public NestedLinearLayout(Context context, @Nullable AttributeSet attrs) {
// mc++         this(context, attrs, 0);
// mc++     }
// mc++ 
// mc++     public NestedLinearLayout(Context context, @Nullable AttributeSet attrs, int defStyleAttr) {
// mc++         super(context, attrs, defStyleAttr);
// mc++         initData();
// mc++     }
// mc++ 
// mc++     private void initData() {
// mc++         if (mScrollingChildHelper == null) {
// mc++             mScrollingChildHelper = new NestedScrollingChildHelper(this);
// mc++             mScrollingChildHelper.setNestedScrollingEnabled(true);
// mc++         }
// mc++     }
// mc++ 
// mc++   @Override
// mc++     public boolean onInterceptTouchEvent(MotionEvent event) {
// mc++         switch (event.getAction()){
// mc++             case MotionEvent.ACTION_DOWN:
// mc++                 lastY = (int) event.getRawY();
// mc++                 // 当开始滑动的时候，告诉父view
// mc++                 startNestedScroll(ViewCompat.SCROLL_AXIS_HORIZONTAL
// mc++                         | ViewCompat.SCROLL_AXIS_VERTICAL);
// mc++                 break;
// mc++             case MotionEvent.ACTION_MOVE:
// mc++ 
// mc++                 return true;
// mc++         }
// mc++         return super.onInterceptTouchEvent(event);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onTouchEvent(MotionEvent event) {
// mc++         switch (event.getAction()){
// mc++             case MotionEvent.ACTION_MOVE:
// mc++                 Log.i(TAG, "onTouchEvent: ACTION_MOVE=");
// mc++                 int y = (int) (event.getRawY());
// mc++                 int dy =lastY- y;
// mc++                 lastY = y;
// mc++                 Log.i(TAG, "onTouchEvent: lastY=" + lastY);
// mc++                 Log.i(TAG, "onTouchEvent: dy=" + dy);
// mc++                 //  dy < 0 下拉， dy>0 赏花
// mc++                 if (dy >0) { // 上滑的时候才交给父类去处理
// mc++                     if (startNestedScroll(ViewCompat.SCROLL_AXIS_VERTICAL) // 如果找到了支持嵌套滚动的父类
// mc++                             && dispatchNestedPreScroll(0, dy, consumed, offset)) {//
// mc++                         // 父类进行了一部分滚动
// mc++                     }
// mc++                 }else{
// mc++                     if (startNestedScroll(ViewCompat.SCROLL_AXIS_VERTICAL) // 如果找到了支持嵌套滚动的父类
// mc++                             && dispatchNestedScroll(0, 0, 0,dy, offset)) {//
// mc++                         // 父类进行了一部分滚动
// mc++                     }
// mc++                 }
// mc++                 break;
// mc++         }
// mc++         return true;
// mc++     }
// mc++ 
// mc++ 
// mc++ 
// mc++     private NestedScrollingChildHelper getScrollingChildHelper() {
// mc++         return mScrollingChildHelper;
// mc++     }
// mc++ 
// mc++     // 接口实现--------------------------------------------------
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
// mc++     public boolean dispatchNestedScroll(int dxConsumed, int dyConsumed,
// mc++                                         int dxUnconsumed, int dyUnconsumed, int[] offsetInWindow) {
// mc++         return getScrollingChildHelper().dispatchNestedScroll(dxConsumed,
// mc++                 dyConsumed, dxUnconsumed, dyUnconsumed, offsetInWindow);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean dispatchNestedPreScroll(int dx, int dy, int[] consumed,
// mc++                                            int[] offsetInWindow) {
// mc++         return getScrollingChildHelper().dispatchNestedPreScroll(dx, dy,
// mc++                 consumed, offsetInWindow);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean dispatchNestedFling(float velocityX, float velocityY,
// mc++                                        boolean consumed) {
// mc++         return getScrollingChildHelper().dispatchNestedFling(velocityX,
// mc++                 velocityY, consumed);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean dispatchNestedPreFling(float velocityX, float velocityY) {
// mc++         return getScrollingChildHelper().dispatchNestedPreFling(velocityX,
// mc++                 velocityY);
// mc++     }
// mc++ }
