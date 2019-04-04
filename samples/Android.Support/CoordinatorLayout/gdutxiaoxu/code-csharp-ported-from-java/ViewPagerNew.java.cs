// mc++ package com.xujun.contralayout.UI.viewPager;
// mc++ 
// mc++ import android.os.Build;
// mc++ import android.os.Bundle;
// mc++ import android.support.annotation.RequiresApi;
// mc++ import android.support.design.widget.AppBarLayout;
// mc++ import android.support.design.widget.TabLayout;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v4.view.ViewPager;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ import android.view.ViewTreeObserver;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.UI.ListFragment;
// mc++ import com.xujun.contralayout.base.BaseFragmentAdapter;
// mc++ import com.xujun.contralayout.base.WriteLogUtil;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ import java.util.List;
// mc++ 
// mc++ public class ViewPagerNew extends AppCompatActivity {
// mc++     ViewPager mViewPager;
// mc++     List<Fragment> mFragments;
// mc++ 
// mc++     AppBarLayout mAppBarLayout;
// mc++ 
// mc++     View mView;
// mc++ 
// mc++     private static final String TAG = "ViewPagerNew";
// mc++ 
// mc++ 
// mc++     String[]  mTitles=new String[]{
// mc++             "主页","微博","相册"
// mc++     };
// mc++ 
// mc++ 
// mc++     private int mHeight;
// mc++     private View mSearchTitle;
// mc++ 
// mc++     View ll_contain;
// mc++     View  ll_content;
// mc++     private int mHeightContent;
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_test);
// mc++         mView=findViewById(R.id.view);
// mc++         ll_content=findViewById(R.id.ll_content);
// mc++         ll_contain=findViewById(R.id.ll_contain);
// mc++         mViewPager=(ViewPager)findViewById(R.id.viewpager);
// mc++         mAppBarLayout=(AppBarLayout) findViewById(R.id.appBar);
// mc++         mSearchTitle = findViewById(R.id.search_title);
// mc++         mView.getViewTreeObserver().addOnGlobalLayoutListener(new ViewTreeObserver.OnGlobalLayoutListener() {
// mc++ 
// mc++ 
// mc++             @RequiresApi(api = Build.VERSION_CODES.JELLY_BEAN)
// mc++             @Override
// mc++             public void onGlobalLayout() {
// mc++                 mView.getViewTreeObserver().removeOnGlobalLayoutListener(this);
// mc++                 mHeight=findViewById(R.id.headview).getHeight();
// mc++                 mHeightContent = ll_content.getHeight()-findViewById(R.id.tabs).getHeight();
// mc++ 
// mc++                 WriteLogUtil.i(" mHeight="+mHeight);
// mc++                 WriteLogUtil.i(" mHeightContent="+mHeightContent);
// mc++             }
// mc++         });
// mc++ 
// mc++         mAppBarLayout.addOnOffsetChangedListener(new AppBarLayout.OnOffsetChangedListener() {
// mc++             @Override
// mc++             public void onOffsetChanged(AppBarLayout appBarLayout, int verticalOffset) {
// mc++                 int abs = Math.abs(verticalOffset);
// mc++                 WriteLogUtil.i(TAG," abs="+abs);
// mc++                 if(abs<mHeight){
// mc++                     ll_contain.setVisibility(View.GONE);
// mc++                     ViewGroup.LayoutParams layoutParams = mSearchTitle.getLayoutParams();
// mc++                     layoutParams.height=0;
// mc++                     mSearchTitle.setLayoutParams(layoutParams);
// mc++                     mSearchTitle.setVisibility(View.GONE);
// mc++                 }else{
// mc++                     mSearchTitle.setVisibility(View.VISIBLE);
// mc++                     ll_contain.setVisibility(View.VISIBLE);
// mc++                     ViewGroup.LayoutParams layoutParams = mSearchTitle.getLayoutParams();
// mc++                     layoutParams.height=100;
// mc++                     mSearchTitle.setLayoutParams(layoutParams);
// mc++ 
// mc++                 }
// mc++             /*    if(mHeightContent <=0){
// mc++                     return;
// mc++                 }
// mc++                 if(  abs< mHeightContent){
// mc++ 
// mc++                     ll_content.setPadding(0,0,0,0);
// mc++                 }else{
// mc++ 
// mc++                     ll_content.setPadding(0,100,0,0);
// mc++                 }*/
// mc++             }
// mc++         });
// mc++ 
// mc++ 
// mc++ 
// mc++ 
// mc++ 
// mc++         setupViewPager();
// mc++     }
// mc++ 
// mc++ 
// mc++ 
// mc++     private void setupViewPager() {
// mc++         final ViewPager viewPager = (ViewPager) findViewById(R.id.viewpager);
// mc++         setupViewPager(viewPager);
// mc++ 
// mc++         TabLayout tabLayout = (TabLayout) findViewById(R.id.tabs);
// mc++         tabLayout.setupWithViewPager(viewPager);
// mc++ 
// mc++        /* TabLayout tabLayout2 = (TabLayout) findViewById(R.id.tab2);
// mc++         tabLayout2.setupWithViewPager(viewPager);*/
// mc++     }
// mc++ 
// mc++     private void setupViewPager(ViewPager viewPager) {
// mc++         mFragments=new ArrayList<>();
// mc++         for(int i=0;i<mTitles.length;i++){
// mc++             ListFragment listFragment = ListFragment.newInstance(mTitles[i]);
// mc++             mFragments.add(listFragment);
// mc++         }
// mc++         BaseFragmentAdapter adapter =
// mc++                 new BaseFragmentAdapter(getSupportFragmentManager(),mFragments,mTitles);
// mc++ 
// mc++ 
// mc++ 
// mc++         viewPager.setAdapter(adapter);
// mc++     }
// mc++ }
