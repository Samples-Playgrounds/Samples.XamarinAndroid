// mc++ package com.xujun.contralayout.UI.viewPager;
// mc++ 
// mc++ import android.support.design.widget.AppBarLayout;
// mc++ 
// mc++ /**
// mc++  * @author meitu.xujun  on 2017/5/1 11:27
// mc++  * @version 0.1
// mc++  */
// mc++ 
// mc++ public  class AppBarStateChangeListener implements AppBarLayout.OnOffsetChangedListener {
// mc++ 
// mc++     public enum State {
// mc++         EXPANDED,
// mc++         COLLAPSED,
// mc++         INTERNEDIATE
// mc++     }
// mc++ 
// mc++     State mLastState;
// mc++ 
// mc++     public State getCurrentState() {
// mc++         return mCurrentState;
// mc++     }
// mc++ 
// mc++     private State mCurrentState = State.INTERNEDIATE;
// mc++ 
// mc++     OnStateChangedListener mOnStateChangedListener;
// mc++ 
// mc++     public void setCurrentState(State currentState) {
// mc++         mCurrentState = currentState;
// mc++     }
// mc++ 
// mc++     public State getLastState() {
// mc++         return mLastState;
// mc++     }
// mc++ 
// mc++     public void setOnStateChangedListener(OnStateChangedListener onStateChangedListener) {
// mc++         mOnStateChangedListener = onStateChangedListener;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public final void onOffsetChanged(AppBarLayout appBarLayout, int i) {
// mc++         if (i == 0) {
// mc++             if (mCurrentState != State.EXPANDED) {
// mc++                 mOnStateChangedListener.onExpanded();
// mc++             }
// mc++             mLastState=mCurrentState;
// mc++             mCurrentState = State.EXPANDED;
// mc++         } else if (Math.abs(i) >= appBarLayout.getTotalScrollRange()) {
// mc++             if (mCurrentState != State.COLLAPSED) {
// mc++                 mOnStateChangedListener.onCollapsed();
// mc++             }
// mc++             mLastState=mCurrentState;
// mc++             mCurrentState = State.COLLAPSED;
// mc++         } else {
// mc++             if (mCurrentState != State.INTERNEDIATE) {
// mc++                 if (mCurrentState == State.COLLAPSED) {
// mc++                     //由折叠变为中间状态时
// mc++                     mOnStateChangedListener.onInternediateFromCollapsed();
// mc++                 } else if (mCurrentState == State.EXPANDED) {
// mc++                     mOnStateChangedListener.onInternediateFromExpand();
// mc++                 }
// mc++                 mLastState=mCurrentState;
// mc++                 mCurrentState = State.INTERNEDIATE;
// mc++             }
// mc++             mOnStateChangedListener.onInternediate();
// mc++         }
// mc++     }
// mc++ 
// mc++ 
// mc++ 
// mc++     public interface OnStateChangedListener{
// mc++         //展开
// mc++         void onExpanded();
// mc++ 
// mc++         //折叠
// mc++         void onCollapsed();
// mc++ 
// mc++         //展开向折叠时的中间状态
// mc++         void onInternediateFromExpand();
// mc++ 
// mc++         //折叠向展开时的中间状态
// mc++         void onInternediateFromCollapsed();
// mc++ 
// mc++         //中间状态
// mc++         void onInternediate();
// mc++     }
// mc++ }
