// mc++ package com.loonggg.coordinatorlayoutdemo;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.graphics.drawable.Drawable;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v4.app.FragmentManager;
// mc++ import android.support.v4.app.FragmentPagerAdapter;
// mc++ import android.support.v4.view.PagerAdapter;
// mc++ import android.text.Spannable;
// mc++ import android.text.SpannableString;
// mc++ import android.text.style.ImageSpan;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ public class ViewPagerAdapter extends FragmentPagerAdapter {
// mc++ 
// mc++     final int PAGE_COUNT = 5;
// mc++     private String tabTitles[] = new String[]{"", "分享", "收藏", "关注", "关注者"};
// mc++     private Context context;
// mc++ 
// mc++     public ViewPagerAdapter(FragmentManager fm, Context context) {
// mc++         super(fm);
// mc++         this.context = context;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public Fragment getItem(int position) {
// mc++         return PageFragment.newInstance(position + 1);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getCount() {
// mc++         return PAGE_COUNT;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public CharSequence getPageTitle(int position) {
// mc++         return tabTitles[position];
// mc++     }
// mc++ }
