// mc++ package com.xujun.contralayout.base;
// mc++ 
// mc++ import android.app.Activity;
// mc++ import android.app.ProgressDialog;
// mc++ import android.content.Context;
// mc++ import android.content.Intent;
// mc++ import android.content.pm.PackageManager;
// mc++ import android.os.Bundle;
// mc++ import android.os.Parcelable;
// mc++ import android.support.annotation.IdRes;
// mc++ import android.support.annotation.NonNull;
// mc++ import android.support.v4.app.ActivityCompat;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v4.app.FragmentActivity;
// mc++ import android.support.v4.app.FragmentManager;
// mc++ import android.support.v4.app.FragmentTransaction;
// mc++ import android.support.v4.content.ContextCompat;
// mc++ import android.util.Log;
// mc++ import android.view.View;
// mc++ import android.widget.Toast;
// mc++ 
// mc++ import com.xujun.contralayout.base.mvp.IBasePresenter;
// mc++ import com.xujun.contralayout.base.permissions.PermissionHelper;
// mc++ import com.xujun.contralayout.base.permissions.PermissonListener;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ 
// mc++ /**
// mc++  * @author meitu.xujun on 2017/4/19 18:18
// mc++  * @version 0.1
// mc++  */
// mc++ public abstract class BaseMVPActivity<P extends IBasePresenter> extends FragmentActivity {
// mc++ 
// mc++     private static final String DEFAULT_NAME = "DEFAULT_NAME";
// mc++     public static final int REQUEST_CODE = 1;
// mc++ 
// mc++     protected Context mContext;
// mc++     protected P mPresenter;
// mc++ 
// mc++     protected static final String DEFAULT_PARCEABLE_NAME = "DEFAULT_PARCEABLE_NAME";
// mc++     protected static final String DEFAULT_PARCEABLE_LIST_NAME = "DEFAULT_PARCEABLE_LIST_NAME";
// mc++     public static final String TAG = "xujun";
// mc++ 
// mc++     protected String className = getClass().getSimpleName();
// mc++ 
// mc++     protected ProgressDialog dialog;
// mc++ 
// mc++     private static PermissonListener mPermissonListener;
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         ActivityCollector.add(this);
// mc++         initWindows();
// mc++         // base setup
// mc++         mContext = this;
// mc++ 
// mc++         if (getContentViewLayoutID() != 0) {
// mc++             setContentView(getContentViewLayoutID());
// mc++             mPresenter = setPresenter();
// mc++             if (mPresenter != null) {
// mc++                 mPresenter.onCreate();
// mc++             }
// mc++             initView();
// mc++             initListener();
// mc++             initData(savedInstanceState);
// mc++         } else {
// mc++             Log.w("BaseActivity", "onCreate: Error contentView");
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onDestroy() {
// mc++         super.onDestroy();
// mc++         ActivityCollector.remove(this);
// mc++         if (dialog != null) {
// mc++             dialog.cancel();
// mc++             dialog.dismiss();
// mc++         }
// mc++ 
// mc++         if (mPresenter != null) {
// mc++             mPresenter.onDestroy();
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     protected abstract P setPresenter();
// mc++ 
// mc++     /**
// mc++      * 在setContentView前初始化Window设置
// mc++      */
// mc++     protected void initWindows() {
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 获取Layout的id
// mc++      */
// mc++     protected abstract int getContentViewLayoutID();
// mc++ 
// mc++     protected abstract void initView();
// mc++ 
// mc++     protected void initListener() {
// mc++     }
// mc++ 
// mc++     protected void initData(Bundle savedInstanceState) {
// mc++ 
// mc++     }
// mc++ 
// mc++     public void setOnClickListener(View.OnClickListener listener, @IdRes int... ids) {
// mc++         for (int id : ids) {
// mc++             findViewById(id).setOnClickListener(listener);
// mc++         }
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onStart() {
// mc++         super.onStart();
// mc++         Log.i(TAG, "onCreate: =" + className);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onRestart() {
// mc++         super.onRestart();
// mc++         Log.i(TAG, "onRestart: =" + className);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onResume() {
// mc++         super.onResume();
// mc++         Log.i(TAG, "onResume: =" + className);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onPause() {
// mc++         super.onPause();
// mc++         Log.i(TAG, "onPause: =" + className);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onStop() {
// mc++         super.onStop();
// mc++         Log.i(TAG, "onStop: =" + className);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onSaveInstanceState(Bundle outState) {
// mc++         super.onSaveInstanceState(outState);
// mc++ 
// mc++         Log.i(TAG, "onSaveInstanceState: " + className);
// mc++     }
// mc++ 
// mc++     public static void requestPermissions(String[] permissions, PermissonListener
// mc++             perissonListener) {
// mc++         //  这个  top Activity 是为了支持静态方法
// mc++         Activity top = ActivityCollector.getTop();
// mc++         if (top == null) {
// mc++             return;
// mc++         }
// mc++         ArrayList<String> list = new ArrayList<>();
// mc++         mPermissonListener = perissonListener;
// mc++         for (int i = 0; i < permissions.length; i++) {
// mc++             String permission = permissions[i];
// mc++             if (ContextCompat.checkSelfPermission(top, permission) != PackageManager
// mc++                     .PERMISSION_GRANTED) {
// mc++                 ActivityCompat.requestPermissions(top, permissions, REQUEST_CODE);
// mc++                 list.add(permission);
// mc++             }
// mc++ 
// mc++         }
// mc++         if (list.isEmpty()) {
// mc++             mPermissonListener.onGranted();
// mc++         }
// mc++ 
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions,
// mc++                                            @NonNull int[] grantResults) {
// mc++         super.onRequestPermissionsResult(requestCode, permissions, grantResults);
// mc++         switch (requestCode) {
// mc++             case 1:
// mc++                 ArrayList<String> list = new ArrayList<>();
// mc++                 for (int i = 0; i < grantResults.length; i++) {
// mc++                     int grantResult = grantResults[i];
// mc++                     String permission = permissions[i];
// mc++                     if (grantResult != PackageManager.PERMISSION_GRANTED) {
// mc++                         list.add(permission);
// mc++                     }
// mc++                 }
// mc++                 // 权限全部被允许
// mc++                 if (list.isEmpty()) {
// mc++                     mPermissonListener.onGranted();
// mc++                 } else { // 有权限被拒绝
// mc++                     ArrayList<String> permanentDeniedPermissions = new ArrayList<>();
// mc++                     if (PermissionHelper.isM() && PermissionHelper
// mc++                             .handlePermissionPermanentlyDenied(this, list,
// mc++                                     permanentDeniedPermissions)) {
// mc++                         mPermissonListener.onParmanentDenied(permanentDeniedPermissions);
// mc++                     }
// mc++                     if (!list.isEmpty()) {
// mc++                         mPermissonListener.onDenied(list);
// mc++                     }
// mc++ 
// mc++                 }
// mc++                 break;
// mc++             default:
// mc++                 break;
// mc++         }
// mc++     }
// mc++ 
// mc++ 
// mc++ 
// mc++     protected <T> T checkNotNull(T t) {
// mc++         if (t == null) {
// mc++             throw new NullPointerException();
// mc++         }
// mc++         return t;
// mc++     }
// mc++ 
// mc++     protected void showFragment(int containerId, Fragment from, Fragment to, String tag) {
// mc++         FragmentManager supportFragmentManager = getSupportFragmentManager();
// mc++         FragmentTransaction transaction = supportFragmentManager.beginTransaction();
// mc++         if (!to.isAdded()) {    // 先判断是否被add过
// mc++             if (tag != null) {
// mc++                 transaction.hide(from).add(containerId, to, tag);
// mc++             } else {
// mc++                 transaction.hide(from).add(containerId, to);
// mc++             }
// mc++ 
// mc++             // 隐藏当前的fragment，add下一个到Activity中
// mc++         } else {
// mc++             transaction.hide(from).show(to); // 隐藏当前的fragment，显示下一个
// mc++         }
// mc++         transaction.commit();
// mc++ 
// mc++     }
// mc++ 
// mc++     protected void replaceFragment(int containerId, Fragment fragment) {
// mc++         FragmentManager fragmentManager = getSupportFragmentManager();
// mc++         FragmentTransaction transaction = fragmentManager.beginTransaction();
// mc++         transaction.replace(containerId, fragment).commit();
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 启动Activity
// mc++      */
// mc++     public void readyGo(Class<?> clazz) {
// mc++         Intent intent = new Intent(this, clazz);
// mc++         startActivity(intent);
// mc++     }
// mc++ 
// mc++ 
// mc++ 
// mc++     public void readyGo(Class<?> clazz, Parcelable parcelable) {
// mc++         this.readyGo(clazz, DEFAULT_PARCEABLE_NAME, parcelable);
// mc++     }
// mc++ 
// mc++     public void readyGo(Class<?> clazz, String name, Parcelable parcelable) {
// mc++         Intent intent = new Intent(this, clazz);
// mc++         if (null != parcelable) {
// mc++             intent = intent.putExtra(name, parcelable);
// mc++         }
// mc++         startActivity(intent);
// mc++     }
// mc++ 
// mc++     public void readyGo(Class<?> clazz, Parcelable parcelable, String action) {
// mc++         Intent intent = new Intent(this, clazz);
// mc++         intent.setAction(action);
// mc++         if (null != parcelable) {
// mc++             intent = intent.putExtra(DEFAULT_PARCEABLE_NAME, parcelable);
// mc++         }
// mc++         startActivity(intent);
// mc++     }
// mc++ 
// mc++     public void readyGo(Class<? extends Activity> clazz, ArrayList<? extends Parcelable>
// mc++             parcelableList) {
// mc++         this.readyGo(clazz, DEFAULT_PARCEABLE_LIST_NAME, parcelableList);
// mc++     }
// mc++ 
// mc++     public void readyGo(Class<? extends Activity> clazz, String name, ArrayList<? extends
// mc++             Parcelable> parcelableList) {
// mc++         Intent intent = new Intent(this, clazz);
// mc++         if (null != parcelableList) {
// mc++             intent = intent.putExtra(name, parcelableList);
// mc++         }
// mc++         startActivity(intent);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 启动Activity并传递数据
// mc++      */
// mc++     public void readyGo(Class<?> clazz, Bundle bundle) {
// mc++         Intent intent = new Intent(this, clazz);
// mc++         if (null != bundle) {
// mc++             intent.putExtras(bundle);
// mc++         }
// mc++         startActivity(intent);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 启动Activity然后结束本Activity
// mc++      */
// mc++     public void readyGoThenKill(Class<?> clazz) {
// mc++         Intent intent = new Intent(this, clazz);
// mc++         startActivity(intent);
// mc++         finish();
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 启动Activity并传递数据,然后结束本Activity
// mc++      */
// mc++     public void readyGoThenKill(Class<?> clazz, Bundle bundle) {
// mc++         Intent intent = new Intent(this, clazz);
// mc++         if (null != bundle) {
// mc++             intent.putExtras(bundle);
// mc++         }
// mc++         startActivity(intent);
// mc++         finish();
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 显示菊花
// mc++      */
// mc++     public void showProressDialog() {
// mc++         if (dialog == null) {
// mc++             dialog = new ProgressDialog(this);
// mc++         }
// mc++         dialog.show();
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 显示带信息的菊花
// mc++      */
// mc++     public void showProressDialog(String msg) {
// mc++ 
// mc++         if (dialog == null) {
// mc++             dialog = new ProgressDialog(this);
// mc++         }
// mc++         dialog.setMessage(msg);
// mc++         dialog.show();
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 隐藏菊花
// mc++      */
// mc++     public void hideProgressDialog() {
// mc++         if (dialog != null) {
// mc++             dialog.hide();
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 根据字符串弹出Toast
// mc++      */
// mc++     protected void showToast(String msg) {
// mc++         Toast.makeText(getApplicationContext(), msg, Toast.LENGTH_SHORT).show();
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 根据资源id弹出Toast
// mc++      */
// mc++     protected void showToast(int msg) {
// mc++         Toast.makeText(getApplicationContext(), msg, Toast.LENGTH_SHORT).show();
// mc++     }
// mc++ 
// mc++ 
// mc++ }
