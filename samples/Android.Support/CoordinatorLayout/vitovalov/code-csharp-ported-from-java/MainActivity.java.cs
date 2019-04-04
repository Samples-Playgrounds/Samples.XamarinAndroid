// mc++ package io.github.vitovalov.tabbedcoordinator;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.CollapsingToolbarLayout;
// mc++ import android.support.design.widget.TabLayout;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v4.app.FragmentManager;
// mc++ import android.support.v4.app.FragmentPagerAdapter;
// mc++ import android.support.v4.view.ViewPager;
// mc++ import android.support.v4.widget.DrawerLayout;
// mc++ import android.support.v7.app.ActionBarDrawerToggle;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ 
// mc++ import java.util.*;
// mc++ 
// mc++ public class MainActivity extends AppCompatActivity {
// mc++ 
// mc++     Toolbar toolbar;
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_main);
// mc++ 
// mc++         setupToolbar();
// mc++ 
// mc++         setupViewPager();
// mc++ 
// mc++         setupCollapsingToolbar();
// mc++ 
// mc++         setupDrawer();
// mc++ 
// mc++     }
// mc++ 
// mc++     private void setupDrawer() {
// mc++         DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
// mc++         ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
// mc++                 this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
// mc++         drawer.setDrawerListener(toggle);
// mc++         toggle.syncState();
// mc++ 
// mc++     }
// mc++ 
// mc++     private void setupCollapsingToolbar() {
// mc++         final CollapsingToolbarLayout collapsingToolbar = (CollapsingToolbarLayout) findViewById(
// mc++                 R.id.collapse_toolbar);
// mc++ 
// mc++         collapsingToolbar.setTitleEnabled(false);
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
// mc++     private void setupToolbar() {
// mc++         toolbar = (Toolbar) findViewById(R.id.toolbar);
// mc++         setSupportActionBar(toolbar);
// mc++         getSupportActionBar().setTitle("TabbedCoordinatorLayout");
// mc++         getSupportActionBar().setDisplayHomeAsUpEnabled(true);
// mc++     }
// mc++ 
// mc++     private void setupViewPager(ViewPager viewPager) {
// mc++         ViewPagerAdapter adapter = new ViewPagerAdapter(getSupportFragmentManager());
// mc++         adapter.addFrag(new TabFragment(), "Tab 1");
// mc++         adapter.addFrag(new TabFragment(), "Tab 2");
// mc++         adapter.addFrag(new TabFragment(), "Tab 3");
// mc++ 
// mc++         viewPager.setAdapter(adapter);
// mc++     }
// mc++ 
// mc++     static class ViewPagerAdapter extends FragmentPagerAdapter {
// mc++ 
// mc++         private final List<Fragment> mFragmentList = new ArrayList<>();
// mc++         private final List<String> mFragmentTitleList = new ArrayList<>();
// mc++ 
// mc++         public ViewPagerAdapter(FragmentManager manager) {
// mc++             super(manager);
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public Fragment getItem(int position) {
// mc++             return mFragmentList.get(position);
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public int getCount() {
// mc++             return mFragmentList.size();
// mc++         }
// mc++ 
// mc++         public void addFrag(Fragment fragment, String title) {
// mc++             mFragmentList.add(fragment);
// mc++             mFragmentTitleList.add(title);
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public CharSequence getPageTitle(int position) {
// mc++             return mFragmentTitleList.get(position);
// mc++         }
// mc++     }
// mc++ }
