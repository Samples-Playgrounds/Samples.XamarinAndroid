// mc++ package sunger.net.org.coordinatorlayoutdemos.adapter;
// mc++ 
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v4.app.FragmentManager;
// mc++ import android.support.v4.app.FragmentStatePagerAdapter;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ /**
// mc++  * Created by sunger on 2015/12/15.
// mc++  */
// mc++ public class MainTabAdapter extends FragmentStatePagerAdapter {
// mc++     private List<Fragment> listFragment;
// mc++     private List<String> listTitle;
// mc++ 
// mc++     public MainTabAdapter(FragmentManager fm,List<Fragment> listFragment,List<String> listTitle) {
// mc++         super(fm);
// mc++         this.listFragment=listFragment;
// mc++         this.listTitle=listTitle;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public Fragment getItem(int position) {
// mc++         return listFragment.get(position);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getCount() {
// mc++         return listFragment.size();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public CharSequence getPageTitle(int position) {
// mc++         return listTitle.get(position);
// mc++     }
// mc++ }
