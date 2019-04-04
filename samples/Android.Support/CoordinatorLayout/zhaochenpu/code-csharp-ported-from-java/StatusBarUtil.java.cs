// mc++ package com.example.zcp.coordinatorlayoutdemo.ui;
// mc++ 
// mc++ import android.annotation.TargetApi;
// mc++ import android.app.Activity;
// mc++ import android.graphics.Color;
// mc++ import android.os.Build;
// mc++ import android.view.View;
// mc++ import android.view.Window;
// mc++ import android.view.WindowManager;
// mc++ 
// mc++ import java.lang.reflect.Field;
// mc++ import java.lang.reflect.Method;
// mc++ 
// mc++ /**
// mc++  * Created by 赵晨璞
// mc++  *
// mc++  * 文章地址http://www.jianshu.com/p/7f5a9969be53
// mc++  */
// mc++ public class StatusBarUtil {
// mc++ 
// mc++     /**
// mc++      * 修改状态栏为全透明
// mc++      * @param activity
// mc++      */
// mc++     @TargetApi(19)
// mc++     public static void transparencyBar(Activity activity){
// mc++         if(Build.VERSION.SDK_INT >= Build.VERSION_CODES.LOLLIPOP) {
// mc++             Window window = activity.getWindow();
// mc++             window.clearFlags(WindowManager.LayoutParams.FLAG_TRANSLUCENT_STATUS
// mc++                     | WindowManager.LayoutParams.FLAG_TRANSLUCENT_NAVIGATION);
// mc++             window.getDecorView().setSystemUiVisibility(View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN
// mc++                     | View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION
// mc++                     | View.SYSTEM_UI_FLAG_LAYOUT_STABLE);
// mc++             window.addFlags(WindowManager.LayoutParams.FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS);
// mc++             window.setStatusBarColor(Color.TRANSPARENT);
// mc++             window.setNavigationBarColor(Color.TRANSPARENT);
// mc++         } else
// mc++         if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT) {
// mc++             Window window =activity.getWindow();
// mc++             window.setFlags(WindowManager.LayoutParams.FLAG_TRANSLUCENT_STATUS,
// mc++                     WindowManager.LayoutParams.FLAG_TRANSLUCENT_STATUS);
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 修改状态栏颜色，支持4.4以上版本
// mc++      * @param activity
// mc++      * @param colorId
// mc++      */
// mc++     public static void setStatusBarColor(Activity activity,int colorId) {
// mc++ 
// mc++         if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.LOLLIPOP) {
// mc++             Window window = activity.getWindow();
// mc++ //      window.addFlags(WindowManager.LayoutParams.FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS);
// mc++             window.setStatusBarColor(activity.getResources().getColor(colorId));
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      *设置状态栏黑色字体图标，
// mc++      * 适配4.4以上版本MIUIV、Flyme和6.0以上版本其他Android
// mc++      * @param activity
// mc++      * @return 1:MIUUI 2:Flyme 3:android6.0
// mc++      */
// mc++     public static int StatusBarLightMode(Activity activity){
// mc++         int result=0;
// mc++         if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT) {
// mc++             if(MIUISetStatusBarLightMode(activity.getWindow(), true)){
// mc++                 result=1;
// mc++             }else if(FlymeSetStatusBarLightMode(activity.getWindow(), true)){
// mc++                 result=2;
// mc++             }else if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {
// mc++                 activity.getWindow().getDecorView().setSystemUiVisibility( View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN|View.SYSTEM_UI_FLAG_LIGHT_STATUS_BAR);
// mc++                 result=3;
// mc++             }
// mc++         }
// mc++         return result;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 已知系统类型时，设置状态栏黑色字体图标。
// mc++      * 适配4.4以上版本MIUIV、Flyme和6.0以上版本其他Android
// mc++      * @param activity
// mc++      * @param type 1:MIUUI 2:Flyme 3:android6.0
// mc++      */
// mc++     public static void StatusBarLightMode(Activity activity,int type){
// mc++         if(type==1){
// mc++             MIUISetStatusBarLightMode(activity.getWindow(), true);
// mc++         }else if(type==2){
// mc++             FlymeSetStatusBarLightMode(activity.getWindow(), true);
// mc++         }else if(type==3){
// mc++             activity.getWindow().getDecorView().setSystemUiVisibility( View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN|View.SYSTEM_UI_FLAG_LIGHT_STATUS_BAR);
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 清除MIUI或flyme或6.0以上版本状态栏黑色字体
// mc++      */
// mc++     public static void StatusBarDarkMode(Activity activity, int type){
// mc++         if(type==1){
// mc++             MIUISetStatusBarLightMode(activity.getWindow(), false);
// mc++         }else if(type==2){
// mc++             FlymeSetStatusBarLightMode(activity.getWindow(), false);
// mc++         }else if(type==3){
// mc++             activity.getWindow().getDecorView().setSystemUiVisibility(View.SYSTEM_UI_FLAG_VISIBLE);
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++ 
// mc++     /**
// mc++      * 设置状态栏图标为深色和魅族特定的文字风格
// mc++      * 可以用来判断是否为Flyme用户
// mc++      * @param window 需要设置的窗口
// mc++      * @param dark 是否把状态栏字体及图标颜色设置为深色
// mc++      * @return  boolean 成功执行返回true
// mc++      *
// mc++      */
// mc++     public static boolean FlymeSetStatusBarLightMode(Window window, boolean dark) {
// mc++         boolean result = false;
// mc++         if (window != null) {
// mc++             try {
// mc++                 WindowManager.LayoutParams lp = window.getAttributes();
// mc++                 Field darkFlag = WindowManager.LayoutParams.class
// mc++                         .getDeclaredField("MEIZU_FLAG_DARK_STATUS_BAR_ICON");
// mc++                 Field meizuFlags = WindowManager.LayoutParams.class
// mc++                         .getDeclaredField("meizuFlags");
// mc++                 darkFlag.setAccessible(true);
// mc++                 meizuFlags.setAccessible(true);
// mc++                 int bit = darkFlag.getInt(null);
// mc++                 int value = meizuFlags.getInt(lp);
// mc++                 if (dark) {
// mc++                     value |= bit;
// mc++                 } else {
// mc++                     value &= ~bit;
// mc++                 }
// mc++                 meizuFlags.setInt(lp, value);
// mc++                 window.setAttributes(lp);
// mc++                 result = true;
// mc++             } catch (Exception e) {
// mc++ 
// mc++             }
// mc++         }
// mc++         return result;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 设置状态栏字体图标为深色，需要MIUIV6以上
// mc++      * @param window 需要设置的窗口
// mc++      * @param dark 是否把状态栏字体及图标颜色设置为深色
// mc++      * @return  boolean 成功执行返回true
// mc++      *
// mc++      */
// mc++     public static boolean MIUISetStatusBarLightMode(Window window, boolean dark) {
// mc++         boolean result = false;
// mc++         if (window != null) {
// mc++             Class clazz = window.getClass();
// mc++             try {
// mc++                 int darkModeFlag = 0;
// mc++                 Class layoutParams = Class.forName("android.view.MiuiWindowManager$LayoutParams");
// mc++                 Field field = layoutParams.getField("EXTRA_FLAG_STATUS_BAR_DARK_MODE");
// mc++                 darkModeFlag = field.getInt(layoutParams);
// mc++                 Method extraFlagField = clazz.getMethod("setExtraFlags", int.class, int.class);
// mc++                 if(dark){
// mc++                     extraFlagField.invoke(window,darkModeFlag,darkModeFlag);//状态栏透明且黑色字体
// mc++                 }else{
// mc++                     extraFlagField.invoke(window, 0, darkModeFlag);//清除黑色字体
// mc++                 }
// mc++                 result=true;
// mc++             }catch (Exception e){
// mc++ 
// mc++             }
// mc++         }
// mc++         return result;
// mc++     }
// mc++ 
// mc++ }
