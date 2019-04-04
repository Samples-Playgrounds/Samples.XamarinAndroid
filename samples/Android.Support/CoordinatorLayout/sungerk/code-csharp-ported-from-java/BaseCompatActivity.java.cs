// mc++ package sunger.net.org.coordinatorlayoutdemos.activity;
// mc++ 
// mc++ import android.os.Build;
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.util.TypedValue;
// mc++ import android.view.View;
// mc++ import android.view.Window;
// mc++ import android.view.WindowManager;
// mc++ 
// mc++ import sunger.net.org.coordinatorlayoutdemos.R;
// mc++ import sunger.net.org.coordinatorlayoutdemos.utils.SystemBarTintManager;
// mc++ 
// mc++ 
// mc++ /**
// mc++  * Created by sunger on 2015/10/27.
// mc++  */
// mc++ public class BaseCompatActivity extends AppCompatActivity {
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setStateBarColor(R.color.colorPrimary);
// mc++     }
// mc++ 
// mc++     protected void setStateBarColor(int resId) {
// mc++         if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT) {
// mc++             Window win = getWindow();
// mc++             WindowManager.LayoutParams winParams = win.getAttributes();
// mc++             final int bits = WindowManager.LayoutParams.FLAG_TRANSLUCENT_STATUS;
// mc++             winParams.flags |= bits;
// mc++             win.setAttributes(winParams);
// mc++             SystemBarTintManager tintManager = new SystemBarTintManager(this);
// mc++             tintManager.setStatusBarTintEnabled(true);
// mc++             tintManager.setStatusBarTintResource(resId);
// mc++             tintManager.setStatusBarDarkMode(true, this);
// mc++         }
// mc++     }
// mc++ 
// mc++ 
// mc++     protected void setUpCommonBackTooblBar(int toolbarId, String title) {
// mc++         Toolbar mToolbar = (Toolbar) findViewById(toolbarId);
// mc++         mToolbar.setTitle(title);
// mc++         setSupportActionBar(mToolbar);
// mc++         toobarAsBackButton(mToolbar);
// mc++     }
// mc++ 
// mc++     protected void setUpCommonBackTooblBar(int toolbarId, int titleId) {
// mc++         setUpCommonBackTooblBar(toolbarId, getString(titleId));
// mc++     }
// mc++ 
// mc++     public int getActionBarSize() {
// mc++         TypedValue tv = new TypedValue();
// mc++         if (getTheme().resolveAttribute(android.R.attr.actionBarSize, tv, true)) {
// mc++             return TypedValue.complexToDimensionPixelSize(tv.data, getResources().getDisplayMetrics());
// mc++         }
// mc++         return 0;
// mc++     }
// mc++ 
// mc++ 
// mc++     public View getRootView() {
// mc++         return findViewById(android.R.id.content);
// mc++     }
// mc++ 
// mc++ 
// mc++     /**
// mc++      * toolbar点击返回，模拟系统返回
// mc++      * 设置toolbar 为箭头按钮
// mc++      * app:navigationIcon="?attr/homeAsUpIndicator"
// mc++      *
// mc++      * @param toolbar
// mc++      */
// mc++     public void toobarAsBackButton(Toolbar toolbar) {
// mc++         getSupportActionBar().setDisplayHomeAsUpEnabled(true);
// mc++         getSupportActionBar().setDisplayShowHomeEnabled(true);
// mc++         toolbar.setNavigationOnClickListener(new View.OnClickListener() {
// mc++             @Override
// mc++             public void onClick(View v) {
// mc++                 onBackPressed();
// mc++             }
// mc++         });
// mc++     }
// mc++ 
// mc++ 
// mc++ }
