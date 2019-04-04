// mc++ package sunger.net.org.coordinatorlayoutdemos.activity;
// mc++ 
// mc++ import android.content.Intent;
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.NavigationView;
// mc++ import android.support.design.widget.TabLayout;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v4.view.GravityCompat;
// mc++ import android.support.v4.view.ViewPager;
// mc++ import android.support.v4.widget.DrawerLayout;
// mc++ import android.support.v7.app.ActionBarDrawerToggle;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.Menu;
// mc++ import android.view.MenuItem;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ import java.util.List;
// mc++ 
// mc++ import sunger.net.org.coordinatorlayoutdemos.R;
// mc++ import sunger.net.org.coordinatorlayoutdemos.adapter.MainTabAdapter;
// mc++ import sunger.net.org.coordinatorlayoutdemos.fragment.NestedscrollFragment;
// mc++ import sunger.net.org.coordinatorlayoutdemos.fragment.RecyclerGridFragment;
// mc++ import sunger.net.org.coordinatorlayoutdemos.fragment.RecyclerLinearFragment;
// mc++ import sunger.net.org.coordinatorlayoutdemos.fragment.RecyclerStaggeredFragment;
// mc++ 
// mc++ public class MainActivity extends BaseCompatActivity
// mc++         implements NavigationView.OnNavigationItemSelectedListener {
// mc++     private ViewPager mViewPager;
// mc++     private TabLayout mTabLayout;
// mc++     private MainTabAdapter mAdapter;
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_main);
// mc++         Toolbar toolbar = (Toolbar) findViewById(R.id.tool_bar);
// mc++         setSupportActionBar(toolbar);
// mc++         DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
// mc++         ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
// mc++                 this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
// mc++         drawer.setDrawerListener(toggle);
// mc++         toggle.syncState();
// mc++         NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
// mc++         navigationView.setNavigationItemSelectedListener(this);
// mc++         mViewPager = (ViewPager) findViewById(R.id.view_pager);
// mc++         mTabLayout = (TabLayout) findViewById(R.id.tab_layout);
// mc++ 
// mc++         List<Fragment> fragments = new ArrayList<>();
// mc++         fragments.add(new RecyclerLinearFragment());
// mc++         fragments.add(new RecyclerGridFragment());
// mc++         fragments.add(new RecyclerStaggeredFragment());
// mc++         fragments.add(new NestedscrollFragment());
// mc++ 
// mc++         List<String> titles = new ArrayList<>();
// mc++         titles.add("RecyclerLinear");
// mc++         titles.add("RecyclerGrid");
// mc++         titles.add("RecyclerStaggered");
// mc++         titles.add("Nestedscroll");
// mc++ 
// mc++         mTabLayout.addTab(mTabLayout.newTab().setText(titles.get(0)));
// mc++         mTabLayout.addTab(mTabLayout.newTab().setText(titles.get(1)));
// mc++         mTabLayout.addTab(mTabLayout.newTab().setText(titles.get(2)));
// mc++         mTabLayout.addTab(mTabLayout.newTab().setText(titles.get(3)));
// mc++ 
// mc++ 
// mc++         mAdapter = new MainTabAdapter(getSupportFragmentManager(), fragments, titles);
// mc++         mViewPager.setAdapter(mAdapter);
// mc++         mTabLayout.setupWithViewPager(mViewPager);
// mc++         mTabLayout.setTabsFromPagerAdapter(mAdapter);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onBackPressed() {
// mc++         DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
// mc++         if (drawer.isDrawerOpen(GravityCompat.START)) {
// mc++             drawer.closeDrawer(GravityCompat.START);
// mc++         } else {
// mc++             super.onBackPressed();
// mc++         }
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onCreateOptionsMenu(Menu menu) {
// mc++         // Inflate the menu; this adds items to the action bar if it is present.
// mc++         getMenuInflater().inflate(R.menu.main, menu);
// mc++         return true;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onOptionsItemSelected(MenuItem item) {
// mc++         // Handle action bar item clicks here. The action bar will
// mc++         // automatically handle clicks on the Home/Up button, so long
// mc++         // as you specify a parent activity in AndroidManifest.xml.
// mc++         int id = item.getItemId();
// mc++ 
// mc++         //noinspection SimplifiableIfStatement
// mc++         if (id == R.id.action_settings) {
// mc++             return true;
// mc++         }
// mc++ 
// mc++         return super.onOptionsItemSelected(item);
// mc++     }
// mc++ 
// mc++     @SuppressWarnings("StatementWithEmptyBody")
// mc++     @Override
// mc++     public boolean onNavigationItemSelected(MenuItem item) {
// mc++         // Handle navigation view item clicks here.
// mc++         int id = item.getItemId();
// mc++ 
// mc++         if (id == R.id.nav_camera) {
// mc++             // Handle the camera action
// mc++         } else if (id == R.id.nav_scroller_hidden_toolbar) {
// mc++             startActivity(new Intent(this, JianShuActivity.class));
// mc++ 
// mc++ 
// mc++         } else if (id == R.id.nav_recycler_hidden_toolbar) {
// mc++             startActivity(new Intent(this, HideToolBarActivity.class));
// mc++         } else if (id == R.id.nav_viewpager) {
// mc++             startActivity(new Intent(this, HeaderViewPagerActivty.class));
// mc++         } else if (id == R.id.nav_share) {
// mc++ 
// mc++         } else if (id == R.id.nav_send) {
// mc++ 
// mc++         }
// mc++ 
// mc++         DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
// mc++         drawer.closeDrawer(GravityCompat.START);
// mc++         return true;
// mc++     }
// mc++ }
