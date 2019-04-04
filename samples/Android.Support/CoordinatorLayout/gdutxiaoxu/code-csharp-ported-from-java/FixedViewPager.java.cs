// mc++ package github.hellocsl.ucmainpager;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.v4.view.ViewPager;
// mc++ import android.util.AttributeSet;
// mc++ import android.view.MotionEvent;
// mc++ 
// mc++ /**
// mc++  * @author meitu.xujun  on 2017//2 15:33
// mc++  * @version 0.1
// mc++  */
// mc++ 
// mc++ public class FixedViewPager extends ViewPager {
// mc++     private boolean mIsScrollable=true;
// mc++ 
// mc++     public FixedViewPager(Context context) {
// mc++         super(context);
// mc++     }
// mc++ 
// mc++     public FixedViewPager(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++     }
// mc++ 
// mc++     public void setScrollable(boolean isScrollable) {
// mc++         this.mIsScrollable = isScrollable;
// mc++     }
// mc++ 
// mc++     @Override public boolean onInterceptTouchEvent(MotionEvent arg0) { if (mIsScrollable) return super.onInterceptTouchEvent(arg0); else return false; }
// mc++ 
// mc++     @Override
// mc++     public boolean onTouchEvent(MotionEvent ev) {
// mc++         if (mIsScrollable) {
// mc++             return super.onTouchEvent(ev);
// mc++         } else {
// mc++             return false;
// mc++         }
// mc++ 
// mc++     }
// mc++ }
