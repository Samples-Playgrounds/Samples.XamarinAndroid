// mc++ package com.xujun.contralayout.UI.weibo.behavior;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.design.BuildConfig;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.v4.view.ViewCompat;
// mc++ import android.util.AttributeSet;
// mc++ import android.util.Log;
// mc++ import android.view.MotionEvent;
// mc++ import android.view.View;
// mc++ import android.widget.OverScroller;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.base.BaseAPP;
// mc++ 
// mc++ import java.lang.ref.WeakReference;
// mc++ 
// mc++ 
// mc++ public class WeiboHeaderPagerBehavior extends ViewOffsetBehavior {
// mc++     private static final String TAG = "UcNewsHeaderPager";
// mc++     public static final int STATE_OPENED = 0;
// mc++     public static final int STATE_CLOSED = 1;
// mc++     public static final int DURATION_SHORT = 300;
// mc++     public static final int DURATION_LONG = 600;
// mc++ 
// mc++     private int mCurState = STATE_OPENED;
// mc++     private OnPagerStateListener mPagerStateListener;
// mc++ 
// mc++     private OverScroller mOverScroller;
// mc++ 
// mc++     private WeakReference<CoordinatorLayout> mParent;
// mc++     private WeakReference<View> mChild;
// mc++ 
// mc++     public void setPagerStateListener(OnPagerStateListener pagerStateListener) {
// mc++         mPagerStateListener = pagerStateListener;
// mc++     }
// mc++ 
// mc++     public WeiboHeaderPagerBehavior() {
// mc++         init();
// mc++     }
// mc++ 
// mc++     public WeiboHeaderPagerBehavior(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++         init();
// mc++     }
// mc++ 
// mc++     private void init() {
// mc++         mOverScroller = new OverScroller(BaseAPP.getAppContext());
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void layoutChild(CoordinatorLayout parent, View child, int layoutDirection) {
// mc++         super.layoutChild(parent, child, layoutDirection);
// mc++         mParent = new WeakReference<CoordinatorLayout>(parent);
// mc++         mChild = new WeakReference<View>(child);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onStartNestedScroll(CoordinatorLayout coordinatorLayout, View child, View
// mc++             directTargetChild, View target, int nestedScrollAxes) {
// mc++         if (BuildConfig.DEBUG) {
// mc++             Log.d(TAG, "onStartNestedScroll: nestedScrollAxes=" + nestedScrollAxes);
// mc++         }
// mc++ 
// mc++         boolean canScroll = canScroll(child, 0);
// mc++         //拦截垂直方向上的滚动事件且当前状态是打开的并且还可以继续向上收缩
// mc++         return (nestedScrollAxes & ViewCompat.SCROLL_AXIS_VERTICAL) != 0 && canScroll &&
// mc++                 !isClosed(child);
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onNestedPreFling(CoordinatorLayout coordinatorLayout, View child, View target,
// mc++                                     float velocityX, float velocityY) {
// mc++         // consumed the flinging behavior until Closed
// mc++ 
// mc++         boolean coumsed = !isClosed(child);
// mc++         Log.i(TAG, "onNestedPreFling: coumsed=" +coumsed);
// mc++         return coumsed;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onNestedFling(CoordinatorLayout coordinatorLayout, View child, View target,
// mc++                                  float velocityX, float velocityY, boolean consumed) {
// mc++         Log.i(TAG, "onNestedFling: velocityY=" +velocityY);
// mc++         return super.onNestedFling(coordinatorLayout, child, target, velocityX, velocityY,
// mc++                 consumed);
// mc++ 
// mc++     }
// mc++ 
// mc++     private boolean isClosed(View child) {
// mc++         boolean isClosed = child.getTranslationY() == getHeaderOffsetRange();
// mc++         return isClosed;
// mc++     }
// mc++ 
// mc++     public boolean isClosed() {
// mc++         return mCurState == STATE_CLOSED;
// mc++     }
// mc++ 
// mc++     private void changeState(int newState) {
// mc++         if (mCurState != newState) {
// mc++             mCurState = newState;
// mc++             if (mCurState == STATE_OPENED) {
// mc++                 if (mPagerStateListener != null) {
// mc++                     mPagerStateListener.onPagerOpened();
// mc++                 }
// mc++ 
// mc++             } else {
// mc++                 if (mPagerStateListener != null) {
// mc++                     mPagerStateListener.onPagerClosed();
// mc++                 }
// mc++ 
// mc++             }
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     // 表示 Header TransLationY 的值是否达到我们指定的阀值， headerOffsetRange，到达了，返回 false，
// mc++     // 否则，返回 true。注意 TransLationY 是负数。
// mc++     private boolean canScroll(View child, float pendingDy) {
// mc++         int pendingTranslationY = (int) (child.getTranslationY() - pendingDy);
// mc++         int headerOffsetRange = getHeaderOffsetRange();
// mc++         if (pendingTranslationY >= headerOffsetRange && pendingTranslationY <= 0) {
// mc++             return true;
// mc++         }
// mc++         return false;
// mc++     }
// mc++ 
// mc++ 
// mc++ 
// mc++     @Override
// mc++     public boolean onInterceptTouchEvent(CoordinatorLayout parent, final View child, MotionEvent
// mc++             ev) {
// mc++ 
// mc++         boolean closed = isClosed();
// mc++         Log.i(TAG, "onInterceptTouchEvent: closed=" + closed);
// mc++         if (ev.getAction() == MotionEvent.ACTION_UP && !closed) {
// mc++             handleActionUp(parent,child);
// mc++         }
// mc++ 
// mc++         return super.onInterceptTouchEvent(parent, child, ev);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onNestedPreScroll(CoordinatorLayout coordinatorLayout, View child, View target,
// mc++                                   int dx, int dy, int[] consumed) {
// mc++         super.onNestedPreScroll(coordinatorLayout, child, target, dx, dy, consumed);
// mc++         //dy>0 scroll up;dy<0,scroll down
// mc++         Log.i(TAG, "onNestedPreScroll: dy=" + dy);
// mc++         float halfOfDis = dy;
// mc++         //    不能滑动了，直接给 Header 设置 终值，防止出错
// mc++         if (!canScroll(child, halfOfDis)) {
// mc++             child.setTranslationY(halfOfDis > 0 ? getHeaderOffsetRange() : 0);
// mc++         } else {
// mc++             child.setTranslationY(child.getTranslationY() - halfOfDis);
// mc++         }
// mc++         //consumed all scroll behavior after we started Nested Scrolling
// mc++         consumed[1] = dy;
// mc++     }
// mc++ 
// mc++     //    需要注意的是  Header 我们是通过 setTranslationY 来移出屏幕的，所以这个值是负数
// mc++     private int getHeaderOffsetRange() {
// mc++         return BaseAPP.getInstance().getResources().getDimensionPixelOffset(R.dimen
// mc++                 .weibo_header_offset);
// mc++     }
// mc++ 
// mc++     private void handleActionUp(CoordinatorLayout parent, final View child) {
// mc++         if (BuildConfig.DEBUG) {
// mc++             Log.d(TAG, "handleActionUp: ");
// mc++         }
// mc++         if (mFlingRunnable != null) {
// mc++             child.removeCallbacks(mFlingRunnable);
// mc++             mFlingRunnable = null;
// mc++         }
// mc++         mFlingRunnable = new FlingRunnable(parent, child);
// mc++         if (child.getTranslationY() < getHeaderOffsetRange() / 6.0f) {
// mc++             mFlingRunnable.scrollToClosed(DURATION_SHORT);
// mc++         } else {
// mc++             mFlingRunnable.scrollToOpen(DURATION_SHORT);
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     private void onFlingFinished(CoordinatorLayout coordinatorLayout, View layout) {
// mc++         changeState(isClosed(layout) ? STATE_CLOSED : STATE_OPENED);
// mc++     }
// mc++ 
// mc++     public void openPager() {
// mc++         openPager(DURATION_LONG);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * @param duration open animation duration
// mc++      */
// mc++     public void openPager(int duration) {
// mc++         View child = mChild.get();
// mc++         CoordinatorLayout parent = mParent.get();
// mc++         if (isClosed() && child != null) {
// mc++             if (mFlingRunnable != null) {
// mc++                 child.removeCallbacks(mFlingRunnable);
// mc++                 mFlingRunnable = null;
// mc++             }
// mc++             mFlingRunnable = new FlingRunnable(parent, child);
// mc++             mFlingRunnable.scrollToOpen(duration);
// mc++         }
// mc++     }
// mc++ 
// mc++     public void closePager() {
// mc++         closePager(DURATION_LONG);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * @param duration close animation duration
// mc++      */
// mc++     public void closePager(int duration) {
// mc++         View child = mChild.get();
// mc++         CoordinatorLayout parent = mParent.get();
// mc++         if (!isClosed()) {
// mc++             if (mFlingRunnable != null) {
// mc++                 child.removeCallbacks(mFlingRunnable);
// mc++                 mFlingRunnable = null;
// mc++             }
// mc++             mFlingRunnable = new FlingRunnable(parent, child);
// mc++             mFlingRunnable.scrollToClosed(duration);
// mc++         }
// mc++     }
// mc++ 
// mc++     private FlingRunnable mFlingRunnable;
// mc++ 
// mc++     /**
// mc++      * For animation , Why not use {@link android.view.ViewPropertyAnimator } to play animation
// mc++      * is of the
// mc++      * other {@link CoordinatorLayout.Behavior} that depend on this could not receiving the
// mc++      * correct result of
// mc++      * {@link View#getTranslationY()} after animation finished for whatever reason that i don't know
// mc++      */
// mc++     private class FlingRunnable implements Runnable {
// mc++         private final CoordinatorLayout mParent;
// mc++         private final View mLayout;
// mc++ 
// mc++         FlingRunnable(CoordinatorLayout parent, View layout) {
// mc++             mParent = parent;
// mc++             mLayout = layout;
// mc++         }
// mc++ 
// mc++         public void scrollToClosed(int duration) {
// mc++             float curTranslationY = ViewCompat.getTranslationY(mLayout);
// mc++             float dy = getHeaderOffsetRange() - curTranslationY;
// mc++             if (BuildConfig.DEBUG) {
// mc++                 Log.d(TAG, "scrollToClosed:offest:" + getHeaderOffsetRange());
// mc++                 Log.d(TAG, "scrollToClosed: cur0:" + curTranslationY + ",end0:" + dy);
// mc++                 Log.d(TAG, "scrollToClosed: cur:" + Math.round(curTranslationY) + ",end:" + Math
// mc++                         .round(dy));
// mc++                 Log.d(TAG, "scrollToClosed: cur1:" + (int) (curTranslationY) + ",end:" + (int) dy);
// mc++             }
// mc++             mOverScroller.startScroll(0, Math.round(curTranslationY - 0.1f), 0, Math.round(dy +
// mc++                     0.1f), duration);
// mc++             start();
// mc++         }
// mc++ 
// mc++         public void scrollToOpen(int duration) {
// mc++             float curTranslationY = ViewCompat.getTranslationY(mLayout);
// mc++             mOverScroller.startScroll(0, (int) curTranslationY, 0, (int) -curTranslationY,
// mc++                     duration);
// mc++             start();
// mc++         }
// mc++ 
// mc++         private void start() {
// mc++             if (mOverScroller.computeScrollOffset()) {
// mc++                 mFlingRunnable = new FlingRunnable(mParent, mLayout);
// mc++                 ViewCompat.postOnAnimation(mLayout, mFlingRunnable);
// mc++             } else {
// mc++                 onFlingFinished(mParent, mLayout);
// mc++             }
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public void run() {
// mc++             if (mLayout != null && mOverScroller != null) {
// mc++                 if (mOverScroller.computeScrollOffset()) {
// mc++                     if (BuildConfig.DEBUG) {
// mc++                         Log.d(TAG, "run: " + mOverScroller.getCurrY());
// mc++                     }
// mc++                     ViewCompat.setTranslationY(mLayout, mOverScroller.getCurrY());
// mc++                     ViewCompat.postOnAnimation(mLayout, this);
// mc++                 } else {
// mc++                     onFlingFinished(mParent, mLayout);
// mc++                 }
// mc++             }
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * callback for HeaderPager 's state
// mc++      */
// mc++     public interface OnPagerStateListener {
// mc++         /**
// mc++          * do callback when pager closed
// mc++          */
// mc++         void onPagerClosed();
// mc++ 
// mc++         /**
// mc++          * do callback when pager opened
// mc++          */
// mc++         void onPagerOpened();
// mc++     }
// mc++ 
// mc++ }
