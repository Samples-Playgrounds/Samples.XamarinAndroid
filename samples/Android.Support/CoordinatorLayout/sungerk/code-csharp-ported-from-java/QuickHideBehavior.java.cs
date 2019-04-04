// mc++ package sunger.net.org.coordinatorlayoutdemos.behavior;
// mc++ 
// mc++ import android.animation.ObjectAnimator;
// mc++ import android.content.Context;
// mc++ import android.content.res.TypedArray;
// mc++ import android.support.design.widget.AppBarLayout;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.design.widget.FloatingActionButton;
// mc++ import android.support.v4.view.ViewCompat;
// mc++ import android.util.AttributeSet;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ import sunger.net.org.coordinatorlayoutdemos.R;
// mc++ 
// mc++ 
// mc++ /**
// mc++  * Simple scrolling behavior that monitors nested events in the scrolling
// mc++  * container to implement a quick hide/show for the attached view.
// mc++  */
// mc++ public class QuickHideBehavior extends CoordinatorLayout.Behavior<View> {
// mc++ 
// mc++     private static final int DIRECTION_UP = 1;
// mc++     private static final int DIRECTION_DOWN = -1;
// mc++ 
// mc++     /* Tracking direction of user motion */
// mc++     private int mScrollingDirection;
// mc++     /* Tracking last threshold crossed */
// mc++     private int mScrollTrigger;
// mc++ 
// mc++     /* Accumulated scroll distance */
// mc++     private int mScrollDistance;
// mc++     /* Distance threshold to trigger animation */
// mc++     private int mScrollThreshold;
// mc++ 
// mc++ 
// mc++     private ObjectAnimator mAnimator;
// mc++ 
// mc++     //Required to instantiate as a default behavior
// mc++     public QuickHideBehavior() {
// mc++     }
// mc++ 
// mc++     //Required to attach behavior via XML
// mc++     public QuickHideBehavior(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++ 
// mc++         TypedArray a = context.getTheme()
// mc++                 .obtainStyledAttributes(new int[] {R.attr.actionBarSize});
// mc++         //Use half the standard action bar height
// mc++         mScrollThreshold = a.getDimensionPixelSize(0, 0) / 2;
// mc++         a.recycle();
// mc++     }
// mc++ 
// mc++     //Called before a nested scroll event. Return true to declare interest
// mc++     @Override
// mc++     public boolean onStartNestedScroll(CoordinatorLayout coordinatorLayout,
// mc++                                        View child, View directTargetChild, View target,
// mc++                                        int nestedScrollAxes) {
// mc++         //We have to declare interest in the scroll to receive further events
// mc++         return (nestedScrollAxes & ViewCompat.SCROLL_AXIS_VERTICAL) != 0;
// mc++     }
// mc++ 
// mc++     //Called before the scrolling child consumes the event
// mc++     // We can steal all/part of the event by filling in the consumed array
// mc++     @Override
// mc++     public void onNestedPreScroll(CoordinatorLayout coordinatorLayout,
// mc++                                   View child, View target,
// mc++                                   int dx, int dy,
// mc++                                   int[] consumed) {
// mc++         //Determine direction changes here
// mc++         if (dy > 0 && mScrollingDirection != DIRECTION_UP) {
// mc++             mScrollingDirection = DIRECTION_UP;
// mc++             mScrollDistance = 0;
// mc++         } else if (dy < 0 && mScrollingDirection != DIRECTION_DOWN) {
// mc++             mScrollingDirection = DIRECTION_DOWN;
// mc++             mScrollDistance = 0;
// mc++         }
// mc++     }
// mc++ 
// mc++     //Called after the scrolling child consumes the event, with amount consumed
// mc++     @Override
// mc++     public void onNestedScroll(CoordinatorLayout coordinatorLayout,
// mc++                                View child, View target,
// mc++                                int dxConsumed, int dyConsumed,
// mc++                                int dxUnconsumed, int dyUnconsumed) {
// mc++         //Consumed distance is the actual distance traveled by the scrolling view
// mc++         mScrollDistance += dyConsumed;
// mc++         if (mScrollDistance > mScrollThreshold
// mc++                 && mScrollTrigger != DIRECTION_UP) {
// mc++             //Hide the target view
// mc++             mScrollTrigger = DIRECTION_UP;
// mc++             restartAnimator(child, getTargetHideValue(coordinatorLayout, child));
// mc++         } else if (mScrollDistance < -mScrollThreshold
// mc++                 && mScrollTrigger != DIRECTION_DOWN) {
// mc++             //Return the target view
// mc++             mScrollTrigger = DIRECTION_DOWN;
// mc++             restartAnimator(child, 0f);
// mc++         }
// mc++     }
// mc++ 
// mc++     //Called after the scrolling child handles the fling
// mc++     @Override
// mc++     public boolean onNestedFling(CoordinatorLayout coordinatorLayout,
// mc++                                  View child, View target,
// mc++                                  float velocityX, float velocityY,
// mc++                                  boolean consumed) {
// mc++         //We only care when the target view is already handling the fling
// mc++         if (consumed) {
// mc++             if (velocityY > 0 && mScrollTrigger != DIRECTION_UP) {
// mc++                 mScrollTrigger = DIRECTION_UP;
// mc++                 restartAnimator(child, getTargetHideValue(coordinatorLayout, child));
// mc++             } else if (velocityY < 0 && mScrollTrigger != DIRECTION_DOWN) {
// mc++                 mScrollTrigger = DIRECTION_DOWN;
// mc++                 restartAnimator(child, 0f);
// mc++             }
// mc++         }
// mc++ 
// mc++         return false;
// mc++     }
// mc++ 
// mc++     /* Helper Methods */
// mc++ 
// mc++     //Helper to trigger hide/show animation
// mc++     private void restartAnimator(View target, float value) {
// mc++         if (mAnimator != null) {
// mc++             mAnimator.cancel();
// mc++             mAnimator = null;
// mc++         }
// mc++ 
// mc++         mAnimator = ObjectAnimator
// mc++                 .ofFloat(target, View.TRANSLATION_Y, value)
// mc++                 .setDuration(250);
// mc++         mAnimator.start();
// mc++     }
// mc++ 
// mc++     private float getTargetHideValue(ViewGroup parent, View target) {
// mc++         if (target instanceof AppBarLayout) {
// mc++             return -target.getHeight();
// mc++         } else if (target instanceof FloatingActionButton) {
// mc++             return parent.getHeight() - target.getTop();
// mc++         }else {
// mc++             return target.getHeight();
// mc++         }
// mc++     }
// mc++ }
