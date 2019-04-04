// mc++ package com.xujun.contralayout.UI;
// mc++ 
// mc++ import android.Manifest;
// mc++ import android.app.Activity;
// mc++ import android.content.Intent;
// mc++ import android.graphics.Rect;
// mc++ import android.os.Bundle;
// mc++ import android.util.DisplayMetrics;
// mc++ import android.util.Log;
// mc++ import android.view.View;
// mc++ import android.view.ViewTreeObserver;
// mc++ import android.view.Window;
// mc++ import android.widget.Toast;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.UI.FloatingActiobButtton.FloatingActionButtonActivity;
// mc++ import com.xujun.contralayout.UI.FloatingActiobButtton.HorizontalSample;
// mc++ import com.xujun.contralayout.UI.bottomsheet.BottomSheetActivity;
// mc++ import com.xujun.contralayout.UI.cardView.CardViewSample;
// mc++ import com.xujun.contralayout.UI.drawlayout.DrawLayoutSample;
// mc++ import com.xujun.contralayout.UI.toolBar.ToolBarSample;
// mc++ import com.xujun.contralayout.UI.toolBar.ToolBarSampleSnar;
// mc++ import com.xujun.contralayout.UI.viewPager.ViewPagerNew;
// mc++ import com.xujun.contralayout.UI.viewPager.ViewPagerParallax;
// mc++ import com.xujun.contralayout.UI.viewPager.ViewPagerParallaxSnap;
// mc++ import com.xujun.contralayout.UI.viewPager.ViewPagerSample;
// mc++ import com.xujun.contralayout.UI.weibo.WeiboSampleActivity;
// mc++ import com.xujun.contralayout.UI.zhihu.ZhiHuActivity;
// mc++ import com.xujun.contralayout.UI.zhihu.ZhiHuHomeActivity;
// mc++ import com.xujun.contralayout.base.BaseMVPActivity;
// mc++ import com.xujun.contralayout.base.mvp.IBasePresenter;
// mc++ import com.xujun.contralayout.base.permissions.PermissonListener;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ public class MainActivity extends BaseMVPActivity {
// mc++ 
// mc++     public static final String TAG = "xujun";
// mc++ 
// mc++     String[] mPermissions = new String[]{Manifest.permission.WRITE_EXTERNAL_STORAGE};
// mc++ 
// mc++     @Override
// mc++     protected IBasePresenter setPresenter() {
// mc++         return null;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected int getContentViewLayoutID() {
// mc++         return R.layout.activity_main;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void initView() {
// mc++ 
// mc++         final View decorView = getWindow().getDecorView();
// mc++         decorView.getViewTreeObserver().addOnGlobalLayoutListener(new ViewTreeObserver
// mc++                 .OnGlobalLayoutListener() {
// mc++ 
// mc++             @Override
// mc++             public void onGlobalLayout() {
// mc++                 decorView.getViewTreeObserver().removeGlobalOnLayoutListener(this);
// mc++                 Rect rect = new Rect();
// mc++                 //getWindow().getDecorView()得到的View是Window中的最顶层View，可以从Window中获取到该View，
// mc++                 //然后该View有个getWindowVisibleDisplayFrame()方法可以获取到程序显示的区域，
// mc++                 //包括标题栏，但不包括状态栏。
// mc++                 getWindow().getDecorView().getWindowVisibleDisplayFrame(rect);
// mc++                 //1.获取状态栏高度：
// mc++                 //根据上面所述，我们可以通过rect对象得到手机状态栏的高度
// mc++                 int statusBarHeight = rect.top;
// mc++                 //2.获取标题栏高度：
// mc++                 getWindow().findViewById(Window.ID_ANDROID_CONTENT);
// mc++                 //该方法获取到的View是程序不包括标题栏的部分，这样我们就可以计算出标题栏的高度了。
// mc++                 int contentTop = getWindow().findViewById(Window.ID_ANDROID_CONTENT).getTop();
// mc++                 //statusBarHeight是上面所求的状态栏的高度
// mc++                 int titleBarHeight = contentTop - statusBarHeight;
// mc++                 Log.i(TAG, "onGlobalLayout: titleBarHeight=" + titleBarHeight);
// mc++                 Log.i(TAG, "onGlobalLayout: statusBarHeight=" + statusBarHeight);
// mc++             }
// mc++         });
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void initData(Bundle savedInstanceState) {
// mc++         super.initData(savedInstanceState);
// mc++         requestPermissions(mPermissions, new PermissonListener() {
// mc++             @Override
// mc++             public void onGranted() {
// mc++                 Toast.makeText(MainActivity.this, "授权成功", Toast.LENGTH_SHORT).show();
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onDenied(List<String> permisons) {
// mc++                 Toast.makeText(MainActivity.this, "授权失败", Toast.LENGTH_SHORT).show();
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onParmanentDenied(List<String> parmanentDeniedPer) {
// mc++                 Toast.makeText(MainActivity.this, "权限被永久拒绝", Toast.LENGTH_SHORT).show();
// mc++             }
// mc++         });
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onWindowFocusChanged(boolean hasFocus) {
// mc++         super.onWindowFocusChanged(hasFocus);
// mc++ 
// mc++         //屏幕
// mc++         DisplayMetrics dm = new DisplayMetrics();
// mc++         getWindowManager().getDefaultDisplay().getMetrics(dm);
// mc++         Log.e("WangJ", "屏幕高:" + dm.heightPixels);
// mc++ 
// mc++         //应用区域
// mc++         Rect outRect1 = new Rect();
// mc++         getWindow().getDecorView().getWindowVisibleDisplayFrame(outRect1);
// mc++         Log.e("WangJ", "应用区顶部" + outRect1.top);
// mc++         Log.e("WangJ", "应用区高" + outRect1.height());
// mc++ 
// mc++         //View绘制区域
// mc++         Rect outRect2 = new Rect();
// mc++         getWindow().findViewById(Window.ID_ANDROID_CONTENT).getDrawingRect(outRect2);
// mc++         Log.e("WangJ", "View绘制区域顶部-错误方法：" + outRect2.top);
// mc++         //不能像上边一样由outRect2.top获取，这种方式获得的top是0，可能是bug吧
// mc++         int viewTop = getWindow().findViewById(Window.ID_ANDROID_CONTENT).getTop();   //要用这种方法
// mc++         Log.e("WangJ", "View绘制区域顶部-正确方法：" + viewTop);
// mc++         Log.e("WangJ", "View绘制区域高度：" + outRect2.height());
// mc++     }
// mc++ 
// mc++     public void onButtonClick(View view) {
// mc++         switch (view.getId()) {
// mc++             case R.id.btn_recycler_snap:
// mc++                 jump(ToolBarSampleSnar.class);
// mc++                 break;
// mc++ 
// mc++             case R.id.btn_toolBar:
// mc++                 jump(ToolBarSample.class);
// mc++                 break;
// mc++ 
// mc++             case R.id.btn_viewPager:
// mc++                 jump(ViewPagerSample.class);
// mc++                 break;
// mc++ 
// mc++             case R.id.btn_parallax:
// mc++                 jump(ViewPagerParallax.class);
// mc++                 break;
// mc++ 
// mc++             case R.id.btn_parallax_snap:
// mc++                 jump(ViewPagerParallaxSnap.class);
// mc++                 break;
// mc++ 
// mc++             case R.id.btn_floatingAction:
// mc++                 jump(FloatingActionButtonActivity.class);
// mc++                 break;
// mc++ 
// mc++             case R.id.btn_floatingAction_horizontal:
// mc++                 jump(HorizontalSample.class);
// mc++                 break;
// mc++ 
// mc++             case R.id.btn_zhihu:
// mc++                 jump(ZhiHuActivity.class);
// mc++                 break;
// mc++ 
// mc++             case R.id.btn_zhihu_home:
// mc++                 jump(ZhiHuHomeActivity.class);
// mc++                 break;
// mc++ 
// mc++             case R.id.btn_jianshu:
// mc++                 jump(JianShuActivity.class);
// mc++                 break;
// mc++ 
// mc++             case R.id.btn_cardView:
// mc++                 jump(CardViewSample.class);
// mc++                 break;
// mc++ 
// mc++ 
// mc++             case R.id.btn_drawlayout:
// mc++                 jump(DrawLayoutSample.class);
// mc++                 break;
// mc++ 
// mc++             case R.id.btn_bottom_sheet:
// mc++                 jump(BottomSheetActivity.class);
// mc++                 break;
// mc++ 
// mc++             case R.id.btn_viewpager_new:
// mc++                 jump(ViewPagerNew.class);
// mc++                 break;
// mc++ 
// mc++ 
// mc++             case R.id.btn_weibo_sample:
// mc++                 jump(WeiboSampleActivity.class);
// mc++                 break;
// mc++ 
// mc++ 
// mc++             default:
// mc++                 break;
// mc++         }
// mc++     }
// mc++ 
// mc++     public void jump(Class<? extends Activity> clz) {
// mc++         Intent intent = new Intent(this, clz);
// mc++         startActivity(intent);
// mc++     }
// mc++ }
