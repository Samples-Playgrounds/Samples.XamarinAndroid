// mc++ package github.hellocsl.ucmainpager.adapter;
// mc++ 
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v4.app.FragmentManager;
// mc++ import android.support.v4.app.FragmentStatePagerAdapter;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ /**
// mc++  * Created by HelloCsl(cslgogogo@gmail.com) on 2016/3/1 0001.
// mc++  */
// mc++ public class TestFragmentAdapter extends FragmentStatePagerAdapter {
// mc++     List<? extends Fragment> mFragments;
// mc++ 
// mc++ 
// mc++     public TestFragmentAdapter(List<? extends Fragment> fragments, FragmentManager fm) {
// mc++         super(fm);
// mc++         this.mFragments = fragments;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public Fragment getItem(int position) {
// mc++         return mFragments.get(position);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getCount() {
// mc++         return mFragments == null ? 0 : mFragments.size();
// mc++     }
// mc++ }
