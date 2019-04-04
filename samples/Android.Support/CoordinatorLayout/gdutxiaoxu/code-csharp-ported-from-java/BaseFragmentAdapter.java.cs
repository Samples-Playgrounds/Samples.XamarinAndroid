// mc++ package com.xujun.contralayout.base;
// mc++ 
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v4.app.FragmentManager;
// mc++ import android.support.v4.app.FragmentPagerAdapter;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ import java.util.List;
// mc++ 
// mc++ /**
// mc++  * @ explain:
// mc++  * @ author：xujun on 2016/4/28 17:34
// mc++  * @ email：gdutxiaoxu@163.com
// mc++  */
// mc++ public class BaseFragmentAdapter extends FragmentPagerAdapter {
// mc++ 
// mc++     protected List<Fragment> mFragmentList;
// mc++ 
// mc++     protected String[] mTitles;
// mc++ 
// mc++     public BaseFragmentAdapter(FragmentManager fm) {
// mc++         this(fm, null, null);
// mc++     }
// mc++ 
// mc++     public BaseFragmentAdapter(FragmentManager fm, List<Fragment> fragmentList, String[] mTitles) {
// mc++         super(fm);
// mc++         if (fragmentList == null) {
// mc++             fragmentList = new ArrayList<>();
// mc++         }
// mc++         this.mFragmentList = fragmentList;
// mc++         this.mTitles = mTitles;
// mc++     }
// mc++ 
// mc++     public void add(Fragment fragment) {
// mc++         if (isEmpty()) {
// mc++             mFragmentList = new ArrayList<>();
// mc++ 
// mc++         }
// mc++         mFragmentList.add(fragment);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public Fragment getItem(int position) {
// mc++         //        Logger.i("BaseFragmentAdapter position=" +position);
// mc++         return isEmpty() ? null : mFragmentList.get(position);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getCount() {
// mc++         return isEmpty() ? 0 : mFragmentList.size();
// mc++     }
// mc++ 
// mc++     public boolean isEmpty() {
// mc++         return mFragmentList == null;
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public CharSequence getPageTitle(int position) {
// mc++         return mTitles[position];
// mc++     }
// mc++ 
// mc++     /*  @Override
// mc++     public int getItemPosition(Object object) {
// mc++         return PagerAdapter.POSITION_NONE;
// mc++     }*/
// mc++ 
// mc++ 
// mc++ }
