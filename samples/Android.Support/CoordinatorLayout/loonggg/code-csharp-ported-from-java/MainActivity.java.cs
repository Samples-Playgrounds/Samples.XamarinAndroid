// mc++ package com.loonggg.coordinatorlayoutdemo;
// mc++ 
// mc++ import android.annotation.TargetApi;
// mc++ import android.os.Build;
// mc++ import android.support.design.widget.AppBarLayout;
// mc++ import android.support.design.widget.CollapsingToolbarLayout;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.design.widget.TabLayout;
// mc++ import android.support.v4.view.ViewPager;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.Menu;
// mc++ import android.view.MenuItem;
// mc++ import android.view.View;
// mc++ import android.widget.ImageView;
// mc++ import android.widget.LinearLayout;
// mc++ import android.widget.Toast;
// mc++ 
// mc++ import com.bumptech.glide.Glide;
// mc++ import com.bumptech.glide.load.resource.drawable.GlideDrawable;
// mc++ import com.bumptech.glide.request.animation.GlideAnimation;
// mc++ import com.bumptech.glide.request.target.SimpleTarget;
// mc++ import com.jaeger.library.StatusBarUtil;
// mc++ 
// mc++ import jp.wasabeef.glide.transformations.BlurTransformation;
// mc++ import jp.wasabeef.glide.transformations.RoundedCornersTransformation;
// mc++ 
// mc++ public class MainActivity extends AppCompatActivity {
// mc++     private LinearLayout head_layout;
// mc++     private TabLayout toolbar_tab;
// mc++     private ViewPager main_vp_container;
// mc++     private CollapsingToolbarLayout mCollapsingToolbarLayout;
// mc++     private CoordinatorLayout root_layout;
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_main);
// mc++         AppBarLayout app_bar_layout = (AppBarLayout) findViewById(R.id.app_bar_layout);
// mc++         Toolbar mToolbar = (Toolbar) findViewById(R.id.toolbar);
// mc++         setSupportActionBar(mToolbar);
// mc++         getSupportActionBar().setDisplayHomeAsUpEnabled(true);
// mc++         mToolbar.setNavigationOnClickListener(new View.OnClickListener() {
// mc++             @Override
// mc++             public void onClick(View v) {
// mc++                 onBackPressed();
// mc++             }
// mc++         });
// mc++         head_layout = (LinearLayout) findViewById(R.id.head_layout);
// mc++         root_layout = (CoordinatorLayout) findViewById(R.id.root_layout);
// mc++         //使用CollapsingToolbarLayout必须把title设置到CollapsingToolbarLayout上，设置到Toolbar上则不会显示
// mc++         mCollapsingToolbarLayout = (CollapsingToolbarLayout) findViewById(R.id
// mc++                 .collapsing_toolbar_layout);
// mc++         app_bar_layout.addOnOffsetChangedListener(new AppBarLayout.OnOffsetChangedListener() {
// mc++             @Override
// mc++             public void onOffsetChanged(AppBarLayout appBarLayout, int verticalOffset) {
// mc++                 if (verticalOffset <= -head_layout.getHeight() / 2) {
// mc++                     mCollapsingToolbarLayout.setTitle("涩郎");
// mc++                 } else {
// mc++                     mCollapsingToolbarLayout.setTitle(" ");
// mc++                 }
// mc++             }
// mc++         });
// mc++         toolbar_tab = (TabLayout) findViewById(R.id.toolbar_tab);
// mc++         main_vp_container = (ViewPager) findViewById(R.id.main_vp_container);
// mc++ 
// mc++         ViewPagerAdapter vpAdapter = new ViewPagerAdapter(getSupportFragmentManager(), this);
// mc++         main_vp_container.setAdapter(vpAdapter);
// mc++         main_vp_container.addOnPageChangeListener(new TabLayout.TabLayoutOnPageChangeListener
// mc++                 (toolbar_tab));
// mc++         toolbar_tab.setOnTabSelectedListener(new TabLayout.ViewPagerOnTabSelectedListener
// mc++                 (main_vp_container));
// mc++         //tablayout和viewpager建立联系为什么不用下面这个方法呢？自己去研究一下，可能收获更多
// mc++         //toolbar_tab.setupWithViewPager(main_vp_container);
// mc++         loadBlurAndSetStatusBar();
// mc++ 
// mc++         ImageView head_iv = (ImageView) findViewById(R.id.head_iv);
// mc++         Glide.with(this).load(R.mipmap.bg).bitmapTransform(new RoundedCornersTransformation(this,
// mc++                 90, 0)).into(head_iv);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 设置毛玻璃效果和沉浸状态栏
// mc++      */
// mc++     private void loadBlurAndSetStatusBar() {
// mc++         StatusBarUtil.setTranslucent(MainActivity.this, StatusBarUtil.DEFAULT_STATUS_BAR_ALPHA);
// mc++         Glide.with(this).load(R.mipmap.bg).bitmapTransform(new BlurTransformation(this, 100))
// mc++                 .into(new SimpleTarget<GlideDrawable>() {
// mc++             @TargetApi(Build.VERSION_CODES.JELLY_BEAN)
// mc++             @Override
// mc++             public void onResourceReady(GlideDrawable resource, GlideAnimation<? super
// mc++                     GlideDrawable> glideAnimation) {
// mc++                 head_layout.setBackground(resource);
// mc++                 root_layout.setBackground(resource);
// mc++             }
// mc++         });
// mc++ 
// mc++         Glide.with(this).load(R.mipmap.bg).bitmapTransform(new BlurTransformation(this, 100))
// mc++                 .into(new SimpleTarget<GlideDrawable>() {
// mc++             @Override
// mc++             public void onResourceReady(GlideDrawable resource, GlideAnimation<? super
// mc++                     GlideDrawable> glideAnimation) {
// mc++                 mCollapsingToolbarLayout.setContentScrim(resource);
// mc++             }
// mc++         });
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onCreateOptionsMenu(Menu menu) {
// mc++         getMenuInflater().inflate(R.menu.menu_main, menu);
// mc++         return true;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onOptionsItemSelected(MenuItem item) {
// mc++         String msg = "";
// mc++         switch (item.getItemId()) {
// mc++             case R.id.webview:
// mc++                 msg += "博客跳转";
// mc++                 break;
// mc++             case R.id.weibo:
// mc++                 msg += "微博跳转";
// mc++                 break;
// mc++             case R.id.action_settings:
// mc++                 msg += "设置";
// mc++                 break;
// mc++         }
// mc++         if (!msg.equals("")) {
// mc++             Toast.makeText(MainActivity.this, msg, Toast.LENGTH_SHORT).show();
// mc++         }
// mc++         return super.onOptionsItemSelected(item);
// mc++     }
// mc++ }
