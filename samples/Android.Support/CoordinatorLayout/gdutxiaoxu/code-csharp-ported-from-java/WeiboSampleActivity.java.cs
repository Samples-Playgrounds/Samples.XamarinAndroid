// mc++ package com.xujun.contralayout.UI.weibo;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.design.widget.TabLayout;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v4.view.ViewPager;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.View;
// mc++ import android.widget.Toast;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.UI.ListFragment;
// mc++ import com.xujun.contralayout.UI.weibo.behavior.WeiboHeaderPagerBehavior;
// mc++ import com.xujun.contralayout.base.BaseFragmentAdapter;
// mc++ import com.xujun.contralayout.base.BaseMVPActivity;
// mc++ import com.xujun.contralayout.base.mvp.IBasePresenter;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ import java.util.List;
// mc++ 
// mc++ public class WeiboSampleActivity extends BaseMVPActivity implements WeiboHeaderPagerBehavior.OnPagerStateListener {
// mc++ 
// mc++     ViewPager mViewPager;
// mc++     List<Fragment> mFragments;
// mc++     Toolbar mToolbar;
// mc++ 
// mc++     String[] mTitles = new String[]{
// mc++             "主页", "微博", "相册"
// mc++     };
// mc++     private View mHeaderView;
// mc++     private WeiboHeaderPagerBehavior mHeaderPagerBehavior;
// mc++     private View mIvBack;
// mc++ 
// mc++     @Override
// mc++     protected IBasePresenter setPresenter() {
// mc++         return null;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected int getContentViewLayoutID() {
// mc++         return R.layout.activity_weibo_sample;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void initView() {
// mc++         mViewPager = (ViewPager) findViewById(R.id.viewpager);
// mc++         mHeaderView = findViewById(R.id.id_weibo_header);
// mc++         mIvBack = findViewById(R.id.iv_back);
// mc++         mIvBack.setOnClickListener(new View.OnClickListener() {
// mc++             @Override
// mc++             public void onClick(View v) {
// mc++                 handleBack();
// mc++             }
// mc++         });
// mc++         mIvBack.setVisibility(View.INVISIBLE);
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void initData(Bundle savedInstanceState) {
// mc++         super.initData(savedInstanceState);
// mc++         setupViewPager();
// mc++         CoordinatorLayout.LayoutParams layoutParams = (CoordinatorLayout.LayoutParams)
// mc++                 mHeaderView.getLayoutParams();
// mc++         mHeaderPagerBehavior = (WeiboHeaderPagerBehavior) layoutParams.getBehavior();
// mc++         mHeaderPagerBehavior.setPagerStateListener(this);
// mc++     }
// mc++ 
// mc++     private void setupViewPager() {
// mc++         final ViewPager viewPager = (ViewPager) findViewById(R.id.viewpager);
// mc++         setupViewPager(viewPager);
// mc++         TabLayout tabLayout = (TabLayout) findViewById(R.id.tabs);
// mc++         tabLayout.setupWithViewPager(viewPager);
// mc++     }
// mc++ 
// mc++     private void setupViewPager(ViewPager viewPager) {
// mc++         mFragments = new ArrayList<>();
// mc++         for (int i = 0; i < mTitles.length; i++) {
// mc++             ListFragment listFragment = ListFragment.newInstance(mTitles[i]);
// mc++             mFragments.add(listFragment);
// mc++         }
// mc++         BaseFragmentAdapter adapter =
// mc++                 new BaseFragmentAdapter(getSupportFragmentManager(), mFragments, mTitles);
// mc++ 
// mc++ 
// mc++         viewPager.setAdapter(adapter);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onPagerClosed() {
// mc++         Toast.makeText(this, "关闭了", Toast.LENGTH_SHORT).show();
// mc++         mIvBack.setVisibility(View.VISIBLE);
// mc++         for(Fragment fragment:mFragments){
// mc++             ListFragment listFragment= (ListFragment) fragment;
// mc++             listFragment.tooglePager(false);
// mc++         }
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onPagerOpened() {
// mc++         Toast.makeText(this, "打开了", Toast.LENGTH_SHORT).show();
// mc++         mIvBack.setVisibility(View.INVISIBLE);
// mc++         for(Fragment fragment:mFragments){
// mc++             ListFragment listFragment= (ListFragment) fragment;
// mc++             listFragment.tooglePager(true);
// mc++         }
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onBackPressed() {
// mc++         handleBack();
// mc++     }
// mc++ 
// mc++     private void handleBack() {
// mc++         if(mHeaderPagerBehavior.isClosed()){
// mc++             mHeaderPagerBehavior.openPager();
// mc++             return;
// mc++         }
// mc++         finish();
// mc++     }
// mc++ }
