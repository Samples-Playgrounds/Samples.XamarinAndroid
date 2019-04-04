// mc++ package com.xujun.contralayout.adapter;
// mc++ 
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v4.app.FragmentActivity;
// mc++ import android.support.v4.app.FragmentTransaction;
// mc++ import android.widget.RadioGroup;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ /**
// mc++  * @author xujun  on 2016/12/2.
// mc++  * @email gdutxiaoxu@163.com
// mc++  */
// mc++ 
// mc++ public class ZhiHuAdapter implements RadioGroup
// mc++         .OnCheckedChangeListener {
// mc++ 
// mc++     int currentTab = 0;
// mc++     FragmentActivity mFragmentActivity;
// mc++     int mContentId;
// mc++ 
// mc++     List<Fragment> mFragmentList;
// mc++ 
// mc++     protected FragmentToogleListener mFragmentToogleListener;
// mc++ 
// mc++     public ZhiHuAdapter(FragmentActivity fragmentActivity, List<Fragment> fragmentList, int
// mc++             contentId) {
// mc++         this.mFragmentList = fragmentList;
// mc++         this.mFragmentActivity = fragmentActivity;
// mc++         this.mContentId = contentId;
// mc++     }
// mc++ 
// mc++     public void setFragmentToogleListener(FragmentToogleListener fragmentToogleListener) {
// mc++         this.mFragmentToogleListener = fragmentToogleListener;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onCheckedChanged(RadioGroup radioGroup, int checkId) {
// mc++         for (int i = 0; i < radioGroup.getChildCount(); i++) {
// mc++             if (radioGroup.getChildAt(i).getId() == checkId) {
// mc++                 //  即将要展示的Fragment
// mc++                 Fragment target = mFragmentList.get(i);
// mc++                 Fragment currentFragment = getCurrentFragment();
// mc++                 currentFragment.onPause();
// mc++ 
// mc++                 FragmentTransaction fragmentTransaction = getFragmentTransaction();
// mc++                 if (target.isAdded()) {
// mc++                     target.onResume();
// mc++                     fragmentTransaction.show(target).hide(currentFragment);
// mc++ 
// mc++                 } else {
// mc++                     fragmentTransaction.add(mContentId, target).show(target).hide(currentFragment);
// mc++                 }
// mc++                 fragmentTransaction.commit();
// mc++                 currentTab = i;
// mc++ 
// mc++                 if (mFragmentToogleListener != null) {
// mc++                     mFragmentToogleListener.onToogleChange(target, currentTab);
// mc++                 }
// mc++ 
// mc++             }
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     private FragmentTransaction getFragmentTransaction() {
// mc++         return mFragmentActivity
// mc++                 .getSupportFragmentManager().beginTransaction();
// mc++     }
// mc++ 
// mc++     public Fragment getCurrentFragment() {
// mc++         return mFragmentList.get(currentTab);
// mc++     }
// mc++ 
// mc++     public interface FragmentToogleListener {
// mc++         void onToogleChange(Fragment fragment, int currentTab);
// mc++     }
// mc++ }
