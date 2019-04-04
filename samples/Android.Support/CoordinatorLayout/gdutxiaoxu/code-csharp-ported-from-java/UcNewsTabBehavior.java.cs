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
// mc++  * 新闻tab behavior
// mc++  * <p/>
// mc++  * Created by chensuilun on 16/7/25.
// mc++  */
// mc++ public class UcNewsTabBehavior extends HeaderScrollingViewBehavior {
// mc++     private static final String TAG = "UcNewsTabBehavior";
// mc++ 
// mc++     public UcNewsTabBehavior() {
// mc++     }
// mc++ 
// mc++     public UcNewsTabBehavior(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++     }
// mc++ 
// mc++ 
// mc++     @Override
// mc++     protected void layoutChild(CoordinatorLayout parent, View child, int layoutDirection) {
// mc++         super.layoutChild(parent, child, layoutDirection);
// mc++         if (BuildConfig.DEBUG) {
// mc++             Log.d(TAG, "layoutChild:top" + child.getTop() + ",height" + child.getHeight());
// mc++         }
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean layoutDependsOn(CoordinatorLayout parent, View child, View dependency) {
// mc++         return isDependOn(dependency);
// mc++     }
// mc++ 
// mc++ 
// mc++     @Override
// mc++     public boolean onDependentViewChanged(CoordinatorLayout parent, View child, View dependency) {
// mc++         if (BuildConfig.DEBUG) {
// mc++             Log.d(TAG, "onDependentViewChanged: dependency.getTranslationY():"+dependency.getTranslationY());
// mc++         }
// mc++         offsetChildAsNeeded(parent, child, dependency);
// mc++         return false;
// mc++     }
// mc++ 
// mc++     private void offsetChildAsNeeded(CoordinatorLayout parent, View child, View dependency) {
// mc++         float offsetRange = dependency.getTop() + getFinalHeight() - child.getTop();
// mc++         int headerOffsetRange = getHeaderOffsetRange();
// mc++         if (dependency.getTranslationY() == headerOffsetRange) {
// mc++             child.setTranslationY(offsetRange);
// mc++         } else if (dependency.getTranslationY() == 0) {
// mc++             child.setTranslationY(0);
// mc++         } else {
// mc++             child.setTranslationY((int) (dependency.getTranslationY() / (getHeaderOffsetRange() * 1.0f) * offsetRange));
// mc++         }
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
// mc++     private int getHeaderOffsetRange() {
// mc++         return DemoApplication.getAppContext().getResources().getDimensionPixelOffset(R.dimen.uc_news_header_pager_offset);
// mc++     }
// mc++ 
// mc++     private int getFinalHeight() {
// mc++         return DemoApplication.getAppContext().getResources().getDimensionPixelOffset(R.dimen.uc_news_header_title_height);
// mc++     }
// mc++ 
// mc++ 
// mc++     private boolean isDependOn(View dependency) {
// mc++         return dependency != null && dependency.getId() == R.id.id_uc_news_header_pager;
// mc++     }
// mc++ }
