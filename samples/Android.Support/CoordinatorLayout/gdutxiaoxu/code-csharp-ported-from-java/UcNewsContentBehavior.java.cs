// mc++ package github.hellocsl.ucmainpager.behavior.uc;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.util.AttributeSet;
// mc++ import android.util.Log;
// mc++ import android.view.View;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ import github.hellocsl.ucmainpager.BuildConfig;
// mc++ import github.hellocsl.ucmainpager.DemoApplication;
// mc++ import github.hellocsl.ucmainpager.R;
// mc++ import github.hellocsl.ucmainpager.behavior.helper.HeaderScrollingViewBehavior;
// mc++ 
// mc++ 
// mc++ /**
// mc++  * 可滚动的新闻列表Behavior
// mc++  * <p/>
// mc++  * Created by chensuilun on 16/7/24.
// mc++  */
// mc++ public class UcNewsContentBehavior extends HeaderScrollingViewBehavior {
// mc++     private static final String TAG = "UcNewsContentBehavior";
// mc++ 
// mc++     public UcNewsContentBehavior() {
// mc++     }
// mc++ 
// mc++     public UcNewsContentBehavior(Context context, AttributeSet attrs) {
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
// mc++         child.setTranslationY((int) (-dependency.getTranslationY() / (getHeaderOffsetRange() * 1.0f) * getScrollRange(dependency)));
// mc++ 
// mc++     }
// mc++ 
// mc++ 
// mc++     @Override
// mc++     protected View findFirstDependency(List<View> views) {
// mc++         for (int i = 0, z = views.size(); i < z; i++) {
// mc++             View view = views.get(i);
// mc++             if (isDependOn(view))
// mc++                 return view;
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
// mc++         return DemoApplication.getAppContext().getResources().getDimensionPixelOffset(R.dimen.uc_news_header_pager_offset);
// mc++     }
// mc++ 
// mc++     private int getFinalHeight() {
// mc++         return DemoApplication.getAppContext().getResources().getDimensionPixelOffset(R.dimen.uc_news_tabs_height) + DemoApplication.getAppContext().getResources().getDimensionPixelOffset(R.dimen.uc_news_header_title_height);
// mc++     }
// mc++ 
// mc++ 
// mc++     private boolean isDependOn(View dependency) {
// mc++         return dependency != null && dependency.getId() == R.id.id_uc_news_header_pager;
// mc++     }
// mc++ }
