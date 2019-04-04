// mc++ package com.xujun.contralayout.UI.viewPager;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.TabLayout;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v4.view.ViewPager;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.UI.ListFragment;
// mc++ import com.xujun.contralayout.base.BaseFragmentAdapter;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ import java.util.List;
// mc++ 
// mc++ public class ViewPagerSample extends AppCompatActivity {
// mc++ 
// mc++     ViewPager mViewPager;
// mc++     List<Fragment> mFragments;
// mc++ 
// mc++     String[] mTitles = new String[]{
// mc++             "主页", "微博", "相册"
// mc++     };
// mc++     private TabLayout mTabLayout;
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_third);
// mc++         // 第一步，初始化ViewPager和TabLayout
// mc++         mViewPager = (ViewPager) findViewById(R.id.viewpager);
// mc++         mTabLayout = (TabLayout) findViewById(R.id.tabs);
// mc++         setupViewPager();
// mc++     }
// mc++ 
// mc++     private void setupViewPager() {
// mc++ 
// mc++         mFragments = new ArrayList<>();
// mc++         for (int i = 0; i < mTitles.length; i++) {
// mc++             ListFragment listFragment = ListFragment.newInstance(mTitles[i]);
// mc++             mFragments.add(listFragment);
// mc++         }
// mc++         // 第二步：为ViewPager设置适配器
// mc++         BaseFragmentAdapter adapter =
// mc++                 new BaseFragmentAdapter(getSupportFragmentManager(), mFragments, mTitles);
// mc++ 
// mc++         mViewPager.setAdapter(adapter);
// mc++         //  第三步：将ViewPager与TableLayout 绑定在一起
// mc++         mTabLayout.setupWithViewPager(mViewPager);
// mc++     }
// mc++ 
// mc++ 
// mc++ }
