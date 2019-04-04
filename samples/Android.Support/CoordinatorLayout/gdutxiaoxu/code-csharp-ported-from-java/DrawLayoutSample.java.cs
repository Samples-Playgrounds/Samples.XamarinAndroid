// mc++ package com.xujun.contralayout.UI.drawlayout;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.annotation.NonNull;
// mc++ import android.support.design.widget.NavigationView;
// mc++ import android.support.v4.view.GravityCompat;
// mc++ import android.support.v4.widget.DrawerLayout;
// mc++ import android.support.v7.app.ActionBar;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.MenuItem;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.UI.ItemFragement;
// mc++ 
// mc++ public class DrawLayoutSample extends AppCompatActivity {
// mc++ 
// mc++     private Toolbar mToolbar;
// mc++     private NavigationView mNavigationView;
// mc++     private DrawerLayout mDrawerLayout;
// mc++ 
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_navigation_view);
// mc++ 
// mc++         mToolbar = (Toolbar) findViewById(R.id.id_toolbar);
// mc++         mDrawerLayout = (DrawerLayout) findViewById(R.id.activity_navigation);
// mc++         mNavigationView = (NavigationView) findViewById(R.id.nav_view);
// mc++         //<activity android:name=".NavigationActivity" android:theme="@style/AppThemeNoActionBar"></activity>  
// mc++         //<style name="AppThemeNoActionBar" parent="Theme.AppCompat.Light.NoActionBar">  
// mc++         //初始化toolbar，这里得使用NoActionBar的主题，使用ToolBar替换系统自带的ActionBar达到自己的需求  
// mc++         setSupportActionBar(mToolbar);
// mc++         ActionBar actionBar = getSupportActionBar();
// mc++         //关联图标和侧滑栏  
// mc++         actionBar.setHomeAsUpIndicator(R.drawable.menu);
// mc++         //设置actionBar和侧滑栏关联  
// mc++         actionBar.setDisplayHomeAsUpEnabled(true);
// mc++         //初始化drawerlayout和navigationview  
// mc++         if (mNavigationView != null) {
// mc++             //设置监听回调  
// mc++             mNavigationView.setNavigationItemSelectedListener(new NavigationView
// mc++                     .OnNavigationItemSelectedListener() {
// mc++                 @Override
// mc++                 public boolean onNavigationItemSelected(@NonNull MenuItem item) {
// mc++                     //根据选中不同的选项来进行不同的操作  
// mc++                     switch (item.getItemId()) {
// mc++                         case R.id.nav_home:
// mc++                             getSupportFragmentManager().beginTransaction().replace(R.id.content,
// mc++                                     ItemFragement.newInstance("主页")).commit();
// mc++                             mToolbar.setTitle("主页");
// mc++                             break;
// mc++                         case R.id.nav_friends:
// mc++                             getSupportFragmentManager().beginTransaction().replace(R.id.content,
// mc++                                     ItemFragement.newInstance("我的好友")).commit();
// mc++                             mToolbar.setTitle("我的好友");
// mc++                             break;
// mc++                         case R.id.nav_discussion:
// mc++                             getSupportFragmentManager().beginTransaction().replace(R.id.content, 
// mc++                                     ItemFragement.newInstance("热文论坛")).commit();
// mc++                             mToolbar.setTitle("热文论坛");
// mc++                             break;
// mc++                         case R.id.nav_messages:
// mc++                             getSupportFragmentManager().beginTransaction().replace(R.id.content,
// mc++                                     ItemFragement.newInstance("我的消息")).commit();
// mc++                             mToolbar.setTitle("我的消息");
// mc++                             break;
// mc++                         case R.id.sub1:
// mc++                             getSupportFragmentManager().beginTransaction().replace(R.id.content,
// mc++                                     ItemFragement.newInstance("子项1")).commit();
// mc++                             mToolbar.setTitle("子项1");
// mc++                             break;
// mc++                         case R.id.sub2:
// mc++                             getSupportFragmentManager().beginTransaction().replace(R.id.content,
// mc++                                     ItemFragement.newInstance("子项2")).commit();
// mc++                             mToolbar.setTitle("子项2");
// mc++                             break;
// mc++                     }
// mc++                     //设置选项选中效果  
// mc++                     item.setChecked(true);
// mc++                     //选了侧边栏选项之后，关闭侧边栏  
// mc++                     mDrawerLayout.closeDrawers();
// mc++                     //这里返回true有选中的效果，源码中有解释  
// mc++                     return true;
// mc++                 }
// mc++             });
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onOptionsItemSelected(MenuItem item) {
// mc++         switch (item.getItemId()) {
// mc++             //点击左上角的菜单选项，这是在actionBar.setHomeAsUpIndicator(R.drawable.center_image_collection);
// mc++             // 这儿设置的。  
// mc++             case android.R.id.home:
// mc++                 //点击之后打开侧滑栏  
// mc++                 mDrawerLayout.openDrawer(GravityCompat.START);
// mc++                 return true;
// mc++         }
// mc++         return super.onOptionsItemSelected(item);
// mc++     }
// mc++ 
// mc++ }
