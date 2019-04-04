// mc++ package com.zanon.sample.coordinatorlayout.adapters;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.v4.view.PagerAdapter;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ import com.zanon.sample.coordinatorlayout.ui.ContentView;
// mc++ 
// mc++ /**
// mc++  * Created by romain on 28/10/15.
// mc++  */
// mc++ public class PagerTitleAdapter extends PagerAdapter {
// mc++ 
// mc++     private static final String PAGE = "Page ";
// mc++ 
// mc++     private Context mContext;
// mc++ 
// mc++     public PagerTitleAdapter(Context context) {
// mc++         mContext = context;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public Object instantiateItem(ViewGroup container, int position) {
// mc++         ContentView contentView = new ContentView(mContext);
// mc++         container.addView(contentView);
// mc++         return contentView;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void destroyItem(ViewGroup container, int position, Object object) {
// mc++         container.removeView((View) object);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public CharSequence getPageTitle(int position) {
// mc++         return PAGE + (position + 1);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getCount() {
// mc++         return 3;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean isViewFromObject(View view, Object object) {
// mc++         return view == object;
// mc++     }
// mc++ }
