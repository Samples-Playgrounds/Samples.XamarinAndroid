// mc++ package com.xujun.contralayout.base;
// mc++ 
// mc++ import android.support.design.widget.TabLayout;
// mc++ import android.support.v4.view.ViewPager;
// mc++ import android.view.View;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ 
// mc++ /**
// mc++  * @author xujun  on 2016/12/2.
// mc++  * @email gdutxiaoxu@163.com
// mc++  */
// mc++ 
// mc++ public abstract class BaseViewPagerFragment extends BaseFragment {
// mc++ 
// mc++     private TabLayout tabLayout;
// mc++     private ViewPager viewPager;
// mc++ 
// mc++     protected BaseFragmentAdapter mAdapter;
// mc++ 
// mc++ 
// mc++     @Override
// mc++     protected void initView(View view) {
// mc++         tabLayout = (TabLayout) view.findViewById(R.id.tabLayout);
// mc++         viewPager = (ViewPager) view.findViewById(R.id.view_pager);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected int getLayoutId() {
// mc++         return R.layout.fragment_view_pager;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void initData() {
// mc++         super.initData();
// mc++         mAdapter= getViewPagerAdapter();
// mc++         viewPager.setAdapter(mAdapter);
// mc++         tabLayout.setupWithViewPager(viewPager);
// mc++     }
// mc++ 
// mc++     protected abstract BaseFragmentAdapter getViewPagerAdapter() ;
// mc++ 
// mc++     @Override
// mc++     public void fetchData() {
// mc++ 
// mc++     }
// mc++ }
