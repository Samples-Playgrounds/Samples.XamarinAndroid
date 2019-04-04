// mc++ package github.hellocsl.ucmainpager.behavior.uc;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.v4.view.ViewCompat;
// mc++ import android.util.AttributeSet;
// mc++ import android.util.Log;
// mc++ import android.view.MotionEvent;
// mc++ import android.view.View;
// mc++ import android.widget.OverScroller;
// mc++ 
// mc++ import java.lang.ref.WeakReference;
// mc++ 
// mc++ import github.hellocsl.ucmainpager.BuildConfig;
// mc++ import github.hellocsl.ucmainpager.DemoApplication;
// mc++ import github.hellocsl.ucmainpager.R;
// mc++ import github.hellocsl.ucmainpager.behavior.helper.ViewOffsetBehavior;
// mc++ 
// mc++ 
// mc++ /**
// mc++  * 可滚动的新闻列表的头部
// mc++  * <p/>
// mc++  * Created by chensuilun on 16/7/24.
// mc++  */
// mc++ public class UcNewsHeaderPagerBehavior extends ViewOffsetBehavior {
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
// mc++ 
// mc++     public void setPagerStateListener(OnPagerStateListener pagerStateListener) {
// mc++         mPagerStateListener = pagerStateListener;
// mc++     }
// mc++ 
// mc++     public UcNewsHeaderPagerBehavior() {
// mc++         init();
// mc++     }
// mc++ 
// mc++ 
// mc++     public UcNewsHeaderPagerBehavior(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++         init();
// mc++     }
// mc++ 
// mc++     private void init() {
// mc++         mOverScroller = new OverScroller(DemoApplication.getAppContext());
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
// mc++     public boolean onStartNestedScroll(CoordinatorLayout coordinatorLayout, View child, View directTargetChild, View target, int nestedScrollAxes) {
// mc++         if (BuildConfig.DEBUG) {
// mc++             Log.d(TAG, "onStartNestedScroll: nestedScrollAxes="+nestedScrollAxes);
// mc++         }
// mc++         boolean canScroll = canScroll(child, 0);
// mc++         return (nestedScrollAxes & ViewCompat.SCROLL_AXIS_VERTICAL) != 0 && canScroll && !isClosed(child);
// mc++ //        return (nestedScrollAxes & ViewCompat.SCROLL_AXIS_VERTICAL) != 0 && canScroll(child, 0) ;
// mc++     }
// mc++ 
// mc++ 
// mc++     @Override
// mc++     public boolean onNestedPreFling(CoordinatorLayout coordinatorLayout, View child, View target, float velocityX, float velocityY) {
// mc++         // consumed the flinging behavior until Closed
// mc++         return !isClosed(child);
// mc++     }
// mc++ 
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
// mc++ 
// mc++     private void changeState(int newState) {
// mc++         if (mCurState != newState) {
// mc++             mCurState = newState;
// mc++             if (mCurState == STATE_OPENED) {
// mc++                 mPagerStateListener.onPagerOpened();
// mc++             } else {
// mc++                 mPagerStateListener.onPagerClosed();
// mc++             }
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     private boolean canScroll(View child, float pendingDy) {
// mc++         int pendingTranslationY = (int) (child.getTranslationY() - pendingDy);
// mc++         int headerOffsetRange = getHeaderOffsetRange();
// mc++         if (pendingTranslationY >= headerOffsetRange && pendingTranslationY <= 0) {
// mc++             return true;
// mc++         }
// mc++         return false;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onInterceptTouchEvent(CoordinatorLayout parent, final View child, MotionEvent ev) {
// mc++ 
// mc++         boolean closed = isClosed();
// mc++         Log.i(TAG, "onInterceptTouchEvent: closed=" +closed);
// mc++         if (ev.getAction() == MotionEvent.ACTION_UP && !closed) {
// mc++             handleActionUp(parent, child);
// mc++         }
// mc++      /* if (ev.getAction() == MotionEvent.ACTION_UP ) {
// mc++             handleActionUp(parent, child);
// mc++         }*/
// mc++         return super.onInterceptTouchEvent(parent, child, ev);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onNestedPreScroll(CoordinatorLayout coordinatorLayout, View child, View target, int dx, int dy, int[] consumed) {
// mc++         super.onNestedPreScroll(coordinatorLayout, child, target, dx, dy, consumed);
// mc++         //dy>0 scroll up;dy<0,scroll down
// mc++         Log.i(TAG, "onNestedPreScroll: dy=" +dy);
// mc++         float halfOfDis = dy / 4.0f;
// mc++         if (!canScroll(child, halfOfDis)) {
// mc++             child.setTranslationY(halfOfDis > 0 ? getHeaderOffsetRange() : 0);
// mc++         } else {
// mc++             child.setTranslationY(child.getTranslationY() - halfOfDis);
// mc++         }
// mc++         //consumed all scroll behavior after we started Nested Scrolling
// mc++         consumed[1] = dy;
// mc++     }
// mc++ 
// mc++ 
// mc++     private int getHeaderOffsetRange() {
// mc++         return DemoApplication.getAppContext().getResources().getDimensionPixelOffset(R.dimen.uc_news_header_pager_offset);
// mc++     }
// mc++ 
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
// mc++         if (child.getTranslationY() < getHeaderOffsetRange() / 3.0f) {
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
// mc++ 
// mc++     private FlingRunnable mFlingRunnable;
// mc++ 
// mc++     /**
// mc++      * For animation , Why not use {@link android.view.ViewPropertyAnimator } to play animation is of the
// mc++      * other {@link android.support.design.widget.CoordinatorLayout.Behavior} that depend on this could not receiving the correct result of
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
// mc++                 Log.d(TAG, "scrollToClosed: cur:" + Math.round(curTranslationY) + ",end:" + Math.round(dy));
// mc++                 Log.d(TAG, "scrollToClosed: cur1:" + (int) (curTranslationY) + ",end:" + (int) dy);
// mc++             }
// mc++             mOverScroller.startScroll(0, Math.round(curTranslationY - 0.1f), 0, Math.round(dy + 0.1f), duration);
// mc++             start();
// mc++         }
// mc++ 
// mc++         public void scrollToOpen(int duration) {
// mc++             float curTranslationY = ViewCompat.getTranslationY(mLayout);
// mc++             mOverScroller.startScroll(0, (int) curTranslationY, 0, (int) -curTranslationY, duration);
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
