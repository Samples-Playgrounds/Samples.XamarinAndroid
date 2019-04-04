// mc++ package com.xujun.contralayout.UI.zhihu;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.AppBarLayout;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.util.Log;
// mc++ import android.view.ViewGroup;
// mc++ import android.widget.FrameLayout;
// mc++ import android.widget.RadioButton;
// mc++ import android.widget.RadioGroup;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.UI.ItemFragement;
// mc++ import com.xujun.contralayout.adapter.ZhiHuAdapter;
// mc++ import com.xujun.contralayout.utils.AnimatorUtil;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ import java.util.List;
// mc++ 
// mc++ public class ZhiHuHomeActivity extends AppCompatActivity {
// mc++ 
// mc++     FrameLayout mFl;
// mc++     RadioGroup mRg;
// mc++ 
// mc++     private AppBarLayout mAppBarLayout;
// mc++ 
// mc++     public static final String TAG = "xujun";
// mc++ 
// mc++     private int mCurrentTab = 0;
// mc++ 
// mc++     public static final String[] mTiltles = new String[]{
// mc++             "home", "course", "direct", "me"
// mc++     };
// mc++     private List<Fragment> mFragments;
// mc++     private Fragment mCurFragment;
// mc++     private ZhiHuAdapter mZhiHuAdapter;
// mc++ 
// mc++     private int mAppBarHeight;
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_zhihu_home);
// mc++         initView();
// mc++         initEvent();
// mc++         initHeaderAndFooter();
// mc++     }
// mc++ 
// mc++     private void initHeaderAndFooter() {
// mc++         mAppBarLayout.post(new Runnable() {
// mc++             @Override
// mc++             public void run() {
// mc++                 mAppBarHeight = mAppBarLayout.getHeight();
// mc++             }
// mc++         });
// mc++     }
// mc++ 
// mc++     private void initEvent() {
// mc++         ((RadioButton) mRg.getChildAt(mCurrentTab)).setChecked(true);
// mc++         mFragments = new ArrayList<>();
// mc++ 
// mc++ 
// mc++         mFragments.add(new HomeFragment());
// mc++         mFragments.add(ItemFragement.newInstance(mTiltles[1]));
// mc++         mFragments.add(ItemFragement.newInstance(mTiltles[2]));
// mc++         mFragments.add(new FourFragment());
// mc++ 
// mc++         mCurFragment = mFragments.get(mCurrentTab);
// mc++ 
// mc++ 
// mc++         mZhiHuAdapter = new ZhiHuAdapter(this, mFragments, R.id.fl);
// mc++         mRg.setOnCheckedChangeListener(mZhiHuAdapter);
// mc++         mZhiHuAdapter.setFragmentToogleListener(new ZhiHuAdapter.FragmentToogleListener() {
// mc++             @Override
// mc++             public void onToogleChange(Fragment fragment, int currentTab) {
// mc++                 if (currentTab == 0) {
// mc++                     setAppLayoutHeight(mAppBarHeight);
// mc++                 } else {
// mc++                     setAppLayoutHeight(0);
// mc++                 }
// mc++                 Log.i(TAG, "onToogleChange: " + currentTab);
// mc++                 mCurrentTab = currentTab;
// mc++             }
// mc++         });
// mc++         replace(mCurFragment);
// mc++ 
// mc++ 
// mc++     }
// mc++ 
// mc++     private void setAppLayoutHeight(int height) {
// mc++         ViewGroup.LayoutParams layoutParams = mAppBarLayout.getLayoutParams();
// mc++         layoutParams.height = height;
// mc++         mAppBarLayout.setLayoutParams(layoutParams);
// mc++     }
// mc++ 
// mc++     private void initView() {
// mc++         mFl = (FrameLayout) findViewById(R.id.fl);
// mc++         mRg = (RadioGroup) findViewById(R.id.rg);
// mc++         mAppBarLayout = (AppBarLayout) findViewById(R.id.appBarLayout);
// mc++     }
// mc++ 
// mc++     public void replace(Fragment fragment) {
// mc++         getSupportFragmentManager().beginTransaction()
// mc++                 .replace(R.id.fl, fragment).commit();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onBackPressed() {
// mc++         if (isBottomHide()) {
// mc++             ((RadioButton) mRg.getChildAt(0)).setChecked(true);
// mc++             if (mCurrentTab == 0) {
// mc++                 AnimatorUtil.showHeight(mAppBarLayout, 0, mAppBarHeight);
// mc++             }
// mc++             float translationY = mRg.getTranslationY();
// mc++             Log.i(TAG, "onBackPressed: translationY=" + translationY);
// mc++             AnimatorUtil.tanslation(mRg, mRg.getTranslationY(), 0);
// mc++         } else {
// mc++             super.onBackPressed();
// mc++         }
// mc++ 
// mc++ 
// mc++     }
// mc++ 
// mc++     public boolean isBottomHide() {
// mc++         //     这里mRg的TranslationY之所以会改变，是因为我们改变了他的值
// mc++         float translationY = mRg.getTranslationY();
// mc++         return translationY > 0;
// mc++ 
// mc++     }
// mc++ }
