// mc++ package github.hellocsl.ucmainpager.behavior.uc;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.util.AttributeSet;
// mc++ import android.util.Log;
// mc++ import android.view.View;
// mc++ 
// mc++ import github.hellocsl.ucmainpager.BuildConfig;
// mc++ import github.hellocsl.ucmainpager.DemoApplication;
// mc++ import github.hellocsl.ucmainpager.R;
// mc++ 
// mc++ /**
// mc++  * 新闻标题
// mc++  * <p/>
// mc++  * Created by chensuilun on 16/7/25.
// mc++  */
// mc++ public class UcNewsTitleBehavior extends CoordinatorLayout.Behavior<View> {
// mc++     private static final String TAG = "UcNewsTitleBehavior";
// mc++ 
// mc++     public UcNewsTitleBehavior() {
// mc++     }
// mc++ 
// mc++     public UcNewsTitleBehavior(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++     }
// mc++ 
// mc++ 
// mc++     @Override
// mc++     public boolean onLayoutChild(CoordinatorLayout parent, View child, int layoutDirection) {
// mc++         // FIXME: 16/7/27 不知道为啥在XML设置-45dip,解析出来的topMargin少了1个px,所以这里用代码设置一遍
// mc++         ((CoordinatorLayout.LayoutParams) child.getLayoutParams()).topMargin = -getTitleHeight();
// mc++         parent.onLayoutChild(child, layoutDirection);
// mc++         if (BuildConfig.DEBUG) {
// mc++             Log.d(TAG, "layoutChild:top" + child.getTop() + ",height" + child.getHeight());
// mc++         }
// mc++         return true;
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
// mc++         offsetChildAsNeeded(parent, child, dependency);
// mc++         return false;
// mc++     }
// mc++ 
// mc++     private void offsetChildAsNeeded(CoordinatorLayout parent, View child, View dependency) {
// mc++         int headerOffsetRange = getHeaderOffsetRange();
// mc++         int titleOffsetRange = getTitleHeight();
// mc++         if (BuildConfig.DEBUG) {
// mc++             Log.d(TAG, "offsetChildAsNeeded:" + dependency.getTranslationY());
// mc++         }
// mc++         if (dependency.getTranslationY() == headerOffsetRange) {
// mc++             child.setTranslationY(titleOffsetRange);
// mc++         } else if (dependency.getTranslationY() == 0) {
// mc++             child.setTranslationY(0);
// mc++         } else {
// mc++             child.setTranslationY((int) (dependency.getTranslationY() / (headerOffsetRange * 1.0f) * titleOffsetRange));
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     private int getHeaderOffsetRange() {
// mc++         return DemoApplication.getAppContext().getResources().getDimensionPixelOffset(R.dimen.uc_news_header_pager_offset);
// mc++     }
// mc++ 
// mc++     private int getTitleHeight() {
// mc++         return DemoApplication.getAppContext().getResources().getDimensionPixelOffset(R.dimen.uc_news_header_title_height);
// mc++     }
// mc++ 
// mc++ 
// mc++     private boolean isDependOn(View dependency) {
// mc++         return dependency != null && dependency.getId() == R.id.id_uc_news_header_pager;
// mc++     }
// mc++ }
