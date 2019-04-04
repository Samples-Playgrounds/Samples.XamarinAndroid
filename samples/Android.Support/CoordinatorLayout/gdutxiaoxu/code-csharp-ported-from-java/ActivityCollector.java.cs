// mc++ package com.xujun.contralayout.base;
// mc++ 
// mc++ import android.app.Activity;
// mc++ import android.util.Log;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ import java.util.List;
// mc++ 
// mc++ /**
// mc++  * @author xujun  on 2016/12/27.
// mc++  *  gdutxiaoxu@163.com
// mc++  */
// mc++ 
// mc++ public class ActivityCollector {
// mc++ 
// mc++     public static final String TAG = "ActivityCollector";
// mc++ 
// mc++     private static List<Activity> mActivitys = new ArrayList<>();
// mc++ 
// mc++     public static void add(Activity activity) {
// mc++         mActivitys.add(activity);
// mc++     }
// mc++ 
// mc++     public static void remove(Activity activity) {
// mc++         mActivitys.remove(activity);
// mc++     }
// mc++ 
// mc++     public static Activity getTop() {
// mc++         if (mActivitys.isEmpty()) {
// mc++             return null;
// mc++         }
// mc++         return mActivitys.get(mActivitys.size() - 1);
// mc++     }
// mc++ 
// mc++     public static void quit() {
// mc++         try {
// mc++ 
// mc++             for (Activity a : mActivitys) {
// mc++                 if (!a.isFinishing()) {
// mc++                     a.finish();
// mc++                 }
// mc++ 
// mc++             }
// mc++             mActivitys.clear();
// mc++         } catch (Exception e) {
// mc++             System.exit(0);
// mc++             Log.e(TAG, "quit: e=" + e.getMessage());
// mc++         }
// mc++ 
// mc++     }
// mc++ }
