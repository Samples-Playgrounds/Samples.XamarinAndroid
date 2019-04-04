// mc++ package sunger.net.org.coordinatorlayoutdemos.activity;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.AppBarLayout;
// mc++ import android.support.design.widget.CollapsingToolbarLayout;
// mc++ import android.support.design.widget.TabLayout;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v4.view.ViewPager;
// mc++ import android.support.v4.widget.SwipeRefreshLayout;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ import java.util.List;
// mc++ 
// mc++ import sunger.net.org.coordinatorlayoutdemos.R;
// mc++ import sunger.net.org.coordinatorlayoutdemos.adapter.MainTabAdapter;
// mc++ import sunger.net.org.coordinatorlayoutdemos.fragment.NestedscrollFragment;
// mc++ import sunger.net.org.coordinatorlayoutdemos.fragment.RecyclerFragment;
// mc++ import sunger.net.org.coordinatorlayoutdemos.utils.Utils;
// mc++ 
// mc++ /**
// mc++  * Created by sunger on 15/12/15.
// mc++  */
// mc++ public class HeaderViewPagerActivty extends BaseCompatActivity implements SwipeRefreshLayout.OnRefreshListener, AppBarLayout.OnOffsetChangedListener {
// mc++     private ViewPager mViewPager;
// mc++     private TabLayout mTabLayout;
// mc++     private MainTabAdapter mAdapter;
// mc++     private SwipeRefreshLayout mSwipeRefreshLayout;
// mc++     private AppBarLayout appBarLayout;
// mc++     private RecyclerFragment fragment;
// mc++     private Toolbar toolbar;
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_header_viewpager);
// mc++ 
// mc++         CollapsingToolbarLayout collapsingToolbar = (CollapsingToolbarLayout) findViewById(
// mc++                 R.id.collapse_toolbar);
// mc++         collapsingToolbar.setTitleEnabled(false);
// mc++         appBarLayout = (AppBarLayout) findViewById(R.id.appBarLayout);
// mc++         appBarLayout.addOnOffsetChangedListener(this);
// mc++ 
// mc++         mSwipeRefreshLayout = (SwipeRefreshLayout) findViewById(R.id.swipe_container);
// mc++         mSwipeRefreshLayout.setProgressViewOffset(false, 0, 100);
// mc++         mSwipeRefreshLayout.setColorSchemeResources(R.color.colorPrimary);
// mc++         mSwipeRefreshLayout.setOnRefreshListener(this);
// mc++ 
// mc++         toolbar = (Toolbar) findViewById(R.id.toolbar);
// mc++ 
// mc++         //set the toolbar
// mc++         int toolbar_hight = Utils.getToolbarHeight(this) * 2;
// mc++         CollapsingToolbarLayout.LayoutParams params = (CollapsingToolbarLayout.LayoutParams) toolbar.getLayoutParams();
// mc++         params.height = toolbar_hight;
// mc++         toolbar.setLayoutParams(params);
// mc++ 
// mc++         setUpCommonBackTooblBar(R.id.toolbar, "HeaderViewPager");
// mc++         mViewPager = (ViewPager) findViewById(R.id.view_pager);
// mc++         mTabLayout = (TabLayout) findViewById(R.id.tab_layout);
// mc++ 
// mc++         List<Fragment> fragments = new ArrayList<>();
// mc++         fragment = new RecyclerFragment();
// mc++         fragments.add(fragment);
// mc++         fragments.add(new NestedscrollFragment());
// mc++         fragments.add(new NestedscrollFragment());
// mc++ 
// mc++         List<String> titles = new ArrayList<>();
// mc++         titles.add("Item1");
// mc++         titles.add("Item2");
// mc++         titles.add("Item3");
// mc++ 
// mc++         mTabLayout.addTab(mTabLayout.newTab().setText(titles.get(0)));
// mc++         mTabLayout.addTab(mTabLayout.newTab().setText(titles.get(1)));
// mc++         mTabLayout.addTab(mTabLayout.newTab().setText(titles.get(2)));
// mc++ 
// mc++ 
// mc++         mAdapter = new MainTabAdapter(getSupportFragmentManager(), fragments, titles);
// mc++         mViewPager.setAdapter(mAdapter);
// mc++         mTabLayout.setupWithViewPager(mViewPager);
// mc++         mTabLayout.setTabsFromPagerAdapter(mAdapter);
// mc++     }
// mc++ 
// mc++ 
// mc++     @Override
// mc++     public void onOffsetChanged(AppBarLayout appBarLayout, int i) {
// mc++         mSwipeRefreshLayout.setEnabled(i == 0);
// mc++         float alpha = (float) Math.abs(i) / (float) appBarLayout.getTotalScrollRange() * 1.0f;
// mc++         toolbar.setAlpha(alpha);
// mc++     }
// mc++ 
// mc++ 
// mc++     @Override
// mc++     public void onRefresh() {
// mc++        if(mViewPager.getCurrentItem()==0) {
// mc++            fragment.refresh();
// mc++        }
// mc++         mSwipeRefreshLayout.postDelayed(new Runnable() {
// mc++             @Override
// mc++             public void run() {
// mc++                 mSwipeRefreshLayout.setRefreshing(false);
// mc++             }
// mc++         }, 2000);
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onResume() {
// mc++         super.onResume();
// mc++         appBarLayout.addOnOffsetChangedListener(this);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onPause() {
// mc++         super.onPause();
// mc++         appBarLayout.removeOnOffsetChangedListener(this);
// mc++     }
// mc++ }
