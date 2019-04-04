// mc++ package com.xujun.contralayout.UI.viewPager;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.TabLayout;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v4.view.ViewPager;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.UI.ListFragment;
// mc++ import com.xujun.contralayout.base.BaseFragmentAdapter;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ import java.util.List;
// mc++ 
// mc++ public class ViewPagerParallaxSnap extends AppCompatActivity {
// mc++     ViewPager mViewPager;
// mc++     List<Fragment> mFragments;
// mc++     Toolbar mToolbar;
// mc++ 
// mc++     String[]  mTitles=new String[]{
// mc++             "主页","微博","相册"
// mc++     };
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_view_pager_parallax_snap);
// mc++         mViewPager=(ViewPager)findViewById(R.id.viewpager);
// mc++         mToolbar= (Toolbar) findViewById(R.id.toolbar);
// mc++ 
// mc++         mToolbar.setTitle("唐嫣");
// mc++         setSupportActionBar(mToolbar);
// mc++         getSupportActionBar().setDisplayHomeAsUpEnabled(true);
// mc++ 
// mc++ 
// mc++         setupViewPager();
// mc++     }
// mc++ 
// mc++     private void setupViewPager() {
// mc++         final ViewPager viewPager = (ViewPager) findViewById(R.id.viewpager);
// mc++         setupViewPager(viewPager);
// mc++ 
// mc++         TabLayout tabLayout = (TabLayout) findViewById(R.id.tabs);
// mc++         tabLayout.setupWithViewPager(viewPager);
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
