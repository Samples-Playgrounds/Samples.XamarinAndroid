// mc++ package com.xujun.contralayout.UI.weibo.behavior;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.content.res.Resources;
// mc++ import android.support.design.BuildConfig;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.util.AttributeSet;
// mc++ import android.util.Log;
// mc++ import android.view.View;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.base.BaseAPP;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ /**
// mc++  * 可滚动的 Content Behavior
// mc++  * <p/>
// mc++  * Created by xujun
// mc++  */
// mc++ public class WeiboContentBehavior extends HeaderScrollingViewBehavior {
// mc++     private static final String TAG = "WeiboContentBehavior";
// mc++ 
// mc++     public WeiboContentBehavior() {
// mc++     }
// mc++ 
// mc++     public WeiboContentBehavior(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean layoutDependsOn(CoordinatorLayout parent, View child, View dependency) {
// mc++         return isDependOn(dependency);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onDependentViewChanged(CoordinatorLayout parent, View child, View dependency) {
// mc++         if (BuildConfig.DEBUG) {
// mc++             Log.d(TAG, "onDependentViewChanged");
// mc++         }
// mc++         offsetChildAsNeeded(parent, child, dependency);
// mc++         return false;
// mc++     }
// mc++ 
// mc++     private void offsetChildAsNeeded(CoordinatorLayout parent, View child, View dependency) {
// mc++         float dependencyTranslationY = dependency.getTranslationY();
// mc++         int translationY = (int) (-dependencyTranslationY / (getHeaderOffsetRange() * 1.0f) *
// mc++                 getScrollRange(dependency));
// mc++         Log.i(TAG, "offsetChildAsNeeded: translationY=" + translationY);
// mc++         child.setTranslationY(translationY);
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected View findFirstDependency(List<View> views) {
// mc++         for (int i = 0, z = views.size(); i < z; i++) {
// mc++             View view = views.get(i);
// mc++             if (isDependOn(view)) return view;
// mc++         }
// mc++         return null;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected int getScrollRange(View v) {
// mc++         if (isDependOn(v)) {
// mc++             return Math.max(0, v.getMeasuredHeight() - getFinalHeight());
// mc++         } else {
// mc++             return super.getScrollRange(v);
// mc++         }
// mc++     }
// mc++ 
// mc++     private int getHeaderOffsetRange() {
// mc++         return BaseAPP.getInstance().getResources().getDimensionPixelOffset(R.dimen
// mc++                 .weibo_header_offset);
// mc++     }
// mc++ 
// mc++     private int getFinalHeight() {
// mc++         Resources resources = BaseAPP.getInstance().getResources();
// mc++ 
// mc++         return 0;
// mc++     }
// mc++ 
// mc++     private boolean isDependOn(View dependency) {
// mc++         return dependency != null && dependency.getId() == R.id.id_weibo_header;
// mc++     }
// mc++ }
