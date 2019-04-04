// mc++ package com.zanon.sample.coordinatorlayout;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.content.Intent;
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.TabLayout;
// mc++ import android.support.v4.view.ViewPager;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.MenuItem;
// mc++ import android.view.View;
// mc++ 
// mc++ import com.zanon.coordinatorlayout.R;
// mc++ import com.zanon.sample.coordinatorlayout.adapters.PagerTitleAdapter;
// mc++ 
// mc++ /**
// mc++  * Created by romain on 15/10/15.
// mc++  */
// mc++ public class PagerTabsParallaxImageActivity extends AppCompatActivity implements View.OnClickListener {
// mc++ 
// mc++     public static void start(Context context) {
// mc++         Intent intent = new Intent(context, PagerTabsParallaxImageActivity.class);
// mc++         context.startActivity(intent);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_pager_tabs_parallax_image);
// mc++ 
// mc++         Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
// mc++         setSupportActionBar(toolbar);
// mc++         getSupportActionBar().setDisplayHomeAsUpEnabled(true);
// mc++ 
// mc++         ViewPager pager = (ViewPager) findViewById(R.id.pager);
// mc++         PagerTitleAdapter pagerAdapter = new PagerTitleAdapter(this);
// mc++         pager.setAdapter(pagerAdapter);
// mc++         TabLayout pagerTabs = (TabLayout) findViewById(R.id.pager_tabs);
// mc++         pagerTabs.setupWithViewPager(pager);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onClick(View v) {
// mc++         switch (v.getId()) {
// mc++             case R.id.fab:
// mc++                 break;
// mc++             default:
// mc++                 break;
// mc++         }
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onOptionsItemSelected(MenuItem item) {
// mc++         switch (item.getItemId()) {
// mc++             case android.R.id.home:
// mc++                 finish();
// mc++                 return true;
// mc++         }
// mc++         return super.onOptionsItemSelected(item);
// mc++     }
// mc++ 
// mc++ }
