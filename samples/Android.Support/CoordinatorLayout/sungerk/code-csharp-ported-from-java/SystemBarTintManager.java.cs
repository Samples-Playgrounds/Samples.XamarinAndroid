// mc++ package sunger.net.org.coordinatorlayoutdemos.utils;
// mc++ 
// mc++ import android.annotation.SuppressLint;
// mc++ import android.annotation.TargetApi;
// mc++ import android.app.Activity;
// mc++ import android.content.Context;
// mc++ import android.content.res.Configuration;
// mc++ import android.content.res.Resources;
// mc++ import android.content.res.TypedArray;
// mc++ import android.graphics.drawable.Drawable;
// mc++ import android.os.Build;
// mc++ import android.util.DisplayMetrics;
// mc++ import android.util.TypedValue;
// mc++ import android.view.Gravity;
// mc++ import android.view.View;
// mc++ import android.view.ViewConfiguration;
// mc++ import android.view.ViewGroup;
// mc++ import android.view.Window;
// mc++ import android.view.WindowManager;
// mc++ import android.widget.FrameLayout.LayoutParams;
// mc++ 
// mc++ import java.lang.reflect.Field;
// mc++ import java.lang.reflect.Method;
// mc++ 
// mc++ /**
// mc++  * Created by Administrator on 2015/11/2.
// mc++  */
// mc++ public class SystemBarTintManager {
// mc++     /**
// mc++      * The default system bar tint color value.
// mc++      */
// mc++     public static final int DEFAULT_TINT_COLOR = 0x99000000;
// mc++ 
// mc++     private final SystemBarConfig mConfig;
// mc++     private boolean mStatusBarAvailable;
// mc++     private boolean mNavBarAvailable;
// mc++     private boolean mStatusBarTintEnabled;
// mc++     private boolean mNavBarTintEnabled;
// mc++     private View mStatusBarTintView;
// mc++     private View mNavBarTintView;
// mc++     private static boolean sIsMiuiV6;
// mc++     private static String sNavBarOverride = null;
// mc++ 
// mc++     static {
// mc++         Method methodGetter = null;
// mc++         try {
// mc++             Class<?> sysClass = Class.forName("android.os.SystemProperties");
// mc++             methodGetter = sysClass.getDeclaredMethod("get", String.class);
// mc++             sIsMiuiV6 = "V6".equals((String) methodGetter.invoke(sysClass, "ro.miui.ui.version.name"));
// mc++         } catch (Exception e) {
// mc++             e.printStackTrace();
// mc++         } finally {
// mc++             if (methodGetter != null) {
// mc++                 try {
// mc++                     sNavBarOverride = (String) methodGetter.invoke(null, "qemu.hw.mainkeys");
// mc++                 } catch (Exception e) {
// mc++                     e.printStackTrace();
// mc++                     sNavBarOverride = null;
// mc++                 }
// mc++             }
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * @param activity The host activity.
// mc++      */
// mc++     @TargetApi(19)
// mc++     public SystemBarTintManager(Activity activity) {
// mc++ 
// mc++ 
// mc++         Window win = activity.getWindow();
// mc++         ViewGroup decorViewGroup = (ViewGroup) win.getDecorView();
// mc++ 
// mc++         if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT) {
// mc++             // check theme attrs
// mc++             int[] attrs = {android.R.attr.windowTranslucentStatus, android.R.attr.windowTranslucentNavigation};
// mc++             TypedArray a = activity.obtainStyledAttributes(attrs);
// mc++             try {
// mc++                 mStatusBarAvailable = a.getBoolean(0, false);
// mc++                 mNavBarAvailable = a.getBoolean(1, false);
// mc++             } finally {
// mc++                 a.recycle();
// mc++             }
// mc++ 
// mc++             // check window flags
// mc++             WindowManager.LayoutParams winParams = win.getAttributes();
// mc++             int bits = WindowManager.LayoutParams.FLAG_TRANSLUCENT_STATUS;
// mc++             if ((winParams.flags & bits) != 0) {
// mc++                 mStatusBarAvailable = true;
// mc++             }
// mc++             bits = WindowManager.LayoutParams.FLAG_TRANSLUCENT_NAVIGATION;
// mc++             if ((winParams.flags & bits) != 0) {
// mc++                 mNavBarAvailable = true;
// mc++             }
// mc++         }
// mc++ 
// mc++         mConfig = new SystemBarConfig(activity, mStatusBarAvailable, mNavBarAvailable);
// mc++         // device might not have virtual navigation keys
// mc++         if (!mConfig.hasNavigtionBar()) {
// mc++             mNavBarAvailable = false;
// mc++         }
// mc++ 
// mc++         if (mStatusBarAvailable) {
// mc++             setupStatusBarView(activity, decorViewGroup);
// mc++         }
// mc++         if (mNavBarAvailable) {
// mc++             setupNavBarView(activity, decorViewGroup);
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Enable tinting of the system status bar.
// mc++      *
// mc++      * @param enabled True to enable tinting, false to disable it (default).
// mc++      */
// mc++     public void setStatusBarTintEnabled(boolean enabled) {
// mc++         mStatusBarTintEnabled = enabled;
// mc++         if (mStatusBarAvailable) {
// mc++             mStatusBarTintView.setVisibility(enabled ? View.VISIBLE : View.GONE);
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * set status bar darkmode
// mc++      *
// mc++      * @param darkmode
// mc++      * @param activity
// mc++      */
// mc++     public void setStatusBarDarkMode(boolean darkmode, Activity activity) {
// mc++         if (sIsMiuiV6) {
// mc++             Class<? extends Window> clazz = activity.getWindow().getClass();
// mc++             try {
// mc++                 int darkModeFlag = 0;
// mc++                 Class<?> layoutParams = Class.forName("android.view.MiuiWindowManager$LayoutParams");
// mc++                 Field field = layoutParams.getField("EXTRA_FLAG_STATUS_BAR_DARK_MODE");
// mc++                 darkModeFlag = field.getInt(layoutParams);
// mc++                 Method extraFlagField = clazz.getMethod("setExtraFlags", int.class, int.class);
// mc++                 extraFlagField.invoke(activity.getWindow(), darkmode ? darkModeFlag : 0, darkModeFlag);
// mc++             } catch (Exception e) {
// mc++                 e.printStackTrace();
// mc++             }
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Enable tinting of the system navigation bar.
// mc++      *
// mc++      * @param enabled True to enable tinting, false to disable it (default).
// mc++      */
// mc++     public void setNavigationBarTintEnabled(boolean enabled) {
// mc++         mNavBarTintEnabled = enabled;
// mc++         if (mNavBarAvailable) {
// mc++             mNavBarTintView.setVisibility(enabled ? View.VISIBLE : View.GONE);
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Apply the specified color tint to all system UI bars.
// mc++      *
// mc++      * @param color The color of the background tint.
// mc++      */
// mc++     public void setTintColor(int color) {
// mc++         setStatusBarTintColor(color);
// mc++         setNavigationBarTintColor(color);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Apply the specified drawable or color resource to all system UI bars.
// mc++      *
// mc++      * @param res The identifier of the resource.
// mc++      */
// mc++     public void setTintResource(int res) {
// mc++         setStatusBarTintResource(res);
// mc++         setNavigationBarTintResource(res);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Apply the specified drawable to all system UI bars.
// mc++      *
// mc++      * @param drawable The drawable to use as the background, or null to remove it.
// mc++      */
// mc++     public void setTintDrawable(Drawable drawable) {
// mc++         setStatusBarTintDrawable(drawable);
// mc++         setNavigationBarTintDrawable(drawable);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Apply the specified alpha to all system UI bars.
// mc++      *
// mc++      * @param alpha The alpha to use
// mc++      */
// mc++     public void setTintAlpha(float alpha) {
// mc++         setStatusBarAlpha(alpha);
// mc++         setNavigationBarAlpha(alpha);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Apply the specified color tint to the system status bar.
// mc++      *
// mc++      * @param color The color of the background tint.
// mc++      */
// mc++     public void setStatusBarTintColor(int color) {
// mc++         if (mStatusBarAvailable) {
// mc++             mStatusBarTintView.setBackgroundColor(color);
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Apply the specified drawable or color resource to the system status bar.
// mc++      *
// mc++      * @param res The identifier of the resource.
// mc++      */
// mc++     public void setStatusBarTintResource(int res) {
// mc++         if (mStatusBarAvailable) {
// mc++             mStatusBarTintView.setBackgroundResource(res);
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Apply the specified drawable to the system status bar.
// mc++      *
// mc++      * @param drawable The drawable to use as the background, or null to remove it.
// mc++      */
// mc++     @SuppressWarnings("deprecation")
// mc++     public void setStatusBarTintDrawable(Drawable drawable) {
// mc++         if (mStatusBarAvailable) {
// mc++             mStatusBarTintView.setBackgroundDrawable(drawable);
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Apply the specified alpha to the system status bar.
// mc++      *
// mc++      * @param alpha The alpha to use
// mc++      */
// mc++     @TargetApi(11)
// mc++     public void setStatusBarAlpha(float alpha) {
// mc++         if (mStatusBarAvailable && Build.VERSION.SDK_INT >= Build.VERSION_CODES.HONEYCOMB) {
// mc++             mStatusBarTintView.setAlpha(alpha);
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Apply the specified color tint to the system navigation bar.
// mc++      *
// mc++      * @param color The color of the background tint.
// mc++      */
// mc++     public void setNavigationBarTintColor(int color) {
// mc++         if (mNavBarAvailable) {
// mc++             mNavBarTintView.setBackgroundColor(color);
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Apply the specified drawable or color resource to the system navigation bar.
// mc++      *
// mc++      * @param res The identifier of the resource.
// mc++      */
// mc++     public void setNavigationBarTintResource(int res) {
// mc++         if (mNavBarAvailable) {
// mc++             mNavBarTintView.setBackgroundResource(res);
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Apply the specified drawable to the system navigation bar.
// mc++      *
// mc++      * @param drawable The drawable to use as the background, or null to remove it.
// mc++      */
// mc++     @SuppressWarnings("deprecation")
// mc++     public void setNavigationBarTintDrawable(Drawable drawable) {
// mc++         if (mNavBarAvailable) {
// mc++             mNavBarTintView.setBackgroundDrawable(drawable);
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Apply the specified alpha to the system navigation bar.
// mc++      *
// mc++      * @param alpha The alpha to use
// mc++      */
// mc++     @TargetApi(11)
// mc++     public void setNavigationBarAlpha(float alpha) {
// mc++         if (mNavBarAvailable && Build.VERSION.SDK_INT >= Build.VERSION_CODES.HONEYCOMB) {
// mc++             mNavBarTintView.setAlpha(alpha);
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Get the system bar configuration.
// mc++      *
// mc++      * @return The system bar configuration for the current device configuration.
// mc++      */
// mc++     public SystemBarConfig getConfig() {
// mc++         return mConfig;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Is tinting enabled for the system status bar?
// mc++      *
// mc++      * @return True if enabled, False otherwise.
// mc++      */
// mc++     public boolean isStatusBarTintEnabled() {
// mc++         return mStatusBarTintEnabled;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Is tinting enabled for the system navigation bar?
// mc++      *
// mc++      * @return True if enabled, False otherwise.
// mc++      */
// mc++     public boolean isNavBarTintEnabled() {
// mc++         return mNavBarTintEnabled;
// mc++     }
// mc++ 
// mc++     private void setupStatusBarView(Context context, ViewGroup decorViewGroup) {
// mc++         mStatusBarTintView = new View(context);
// mc++         LayoutParams params = new LayoutParams(LayoutParams.MATCH_PARENT, mConfig.getStatusBarHeight());
// mc++         params.gravity = Gravity.TOP;
// mc++         if (mNavBarAvailable && !mConfig.isNavigationAtBottom()) {
// mc++             params.rightMargin = mConfig.getNavigationBarWidth();
// mc++         }
// mc++         mStatusBarTintView.setLayoutParams(params);
// mc++         mStatusBarTintView.setBackgroundColor(DEFAULT_TINT_COLOR);
// mc++         mStatusBarTintView.setVisibility(View.GONE);
// mc++         decorViewGroup.addView(mStatusBarTintView);
// mc++     }
// mc++ 
// mc++     private void setupNavBarView(Context context, ViewGroup decorViewGroup) {
// mc++         mNavBarTintView = new View(context);
// mc++         LayoutParams params;
// mc++         if (mConfig.isNavigationAtBottom()) {
// mc++             params = new LayoutParams(LayoutParams.MATCH_PARENT, mConfig.getNavigationBarHeight());
// mc++             params.gravity = Gravity.BOTTOM;
// mc++         } else {
// mc++             params = new LayoutParams(mConfig.getNavigationBarWidth(), LayoutParams.MATCH_PARENT);
// mc++             params.gravity = Gravity.RIGHT;
// mc++         }
// mc++         mNavBarTintView.setLayoutParams(params);
// mc++         mNavBarTintView.setBackgroundColor(DEFAULT_TINT_COLOR);
// mc++         mNavBarTintView.setVisibility(View.GONE);
// mc++         decorViewGroup.addView(mNavBarTintView);
// mc++     }
// mc++ 
// mc++     public static class SystemBarConfig {
// mc++ 
// mc++         private static final String STATUS_BAR_HEIGHT_RES_NAME = "status_bar_height";
// mc++         private static final String NAV_BAR_HEIGHT_RES_NAME = "navigation_bar_height";
// mc++         private static final String NAV_BAR_HEIGHT_LANDSCAPE_RES_NAME = "navigation_bar_height_landscape";
// mc++         private static final String NAV_BAR_WIDTH_RES_NAME = "navigation_bar_width";
// mc++         private static final String SHOW_NAV_BAR_RES_NAME = "config_showNavigationBar";
// mc++ 
// mc++         private final boolean mTranslucentStatusBar;
// mc++         private final boolean mTranslucentNavBar;
// mc++         private final int mStatusBarHeight;
// mc++         private final int mActionBarHeight;
// mc++         private final boolean mHasNavigationBar;
// mc++         private final int mNavigationBarHeight;
// mc++         private final int mNavigationBarWidth;
// mc++         private final boolean mInPortrait;
// mc++         private final float mSmallestWidthDp;
// mc++ 
// mc++         private SystemBarConfig(Activity activity, boolean translucentStatusBar, boolean traslucentNavBar) {
// mc++             Resources res = activity.getResources();
// mc++             mInPortrait = (res.getConfiguration().orientation == Configuration.ORIENTATION_PORTRAIT);
// mc++             mSmallestWidthDp = getSmallestWidthDp(activity);
// mc++             mStatusBarHeight = getInternalDimensionSize(res, STATUS_BAR_HEIGHT_RES_NAME);
// mc++             mActionBarHeight = getActionBarHeight(activity);
// mc++             mNavigationBarHeight = getNavigationBarHeight(activity);
// mc++             mNavigationBarWidth = getNavigationBarWidth(activity);
// mc++             mHasNavigationBar = (mNavigationBarHeight > 0);
// mc++             mTranslucentStatusBar = translucentStatusBar;
// mc++             mTranslucentNavBar = traslucentNavBar;
// mc++         }
// mc++ 
// mc++         @TargetApi(14)
// mc++         private int getActionBarHeight(Context context) {
// mc++             int result = 0;
// mc++             if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.ICE_CREAM_SANDWICH) {
// mc++                 TypedValue tv = new TypedValue();
// mc++                 context.getTheme().resolveAttribute(android.R.attr.actionBarSize, tv, true);
// mc++                 result = context.getResources().getDimensionPixelSize(tv.resourceId);
// mc++             }
// mc++             return result;
// mc++         }
// mc++ 
// mc++         @TargetApi(14)
// mc++         private int getNavigationBarHeight(Context context) {
// mc++             Resources res = context.getResources();
// mc++             int result = 0;
// mc++             if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.ICE_CREAM_SANDWICH) {
// mc++                 if (!hasNavBar(context)) {
// mc++                     String key;
// mc++                     if (mInPortrait) {
// mc++                         key = NAV_BAR_HEIGHT_RES_NAME;
// mc++                     } else {
// mc++                         key = NAV_BAR_HEIGHT_LANDSCAPE_RES_NAME;
// mc++                     }
// mc++                     return getInternalDimensionSize(res, key);
// mc++                 }
// mc++             }
// mc++             return result;
// mc++         }
// mc++ 
// mc++         @TargetApi(14)
// mc++         private int getNavigationBarWidth(Context context) {
// mc++             Resources res = context.getResources();
// mc++             int result = 0;
// mc++             if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.ICE_CREAM_SANDWICH) {
// mc++                 if (!hasNavBar(context)) {
// mc++                     return getInternalDimensionSize(res, NAV_BAR_WIDTH_RES_NAME);
// mc++                 }
// mc++             }
// mc++             return result;
// mc++         }
// mc++ 
// mc++         private int getInternalDimensionSize(Resources res, String key) {
// mc++             int result = 0;
// mc++             int resourceId = res.getIdentifier(key, "dimen", "android");
// mc++             if (resourceId > 0) {
// mc++                 result = res.getDimensionPixelSize(resourceId);
// mc++             }
// mc++             return result;
// mc++         }
// mc++ 
// mc++         @SuppressLint("NewApi")
// mc++         private float getSmallestWidthDp(Activity activity) {
// mc++             DisplayMetrics metrics = new DisplayMetrics();
// mc++             if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.JELLY_BEAN) {
// mc++                 activity.getWindowManager().getDefaultDisplay().getRealMetrics(metrics);
// mc++             } else {
// mc++                 // TODO this is not correct, but we don't really care pre-kitkat
// mc++                 activity.getWindowManager().getDefaultDisplay().getMetrics(metrics);
// mc++             }
// mc++             float widthDp = metrics.widthPixels / metrics.density;
// mc++             float heightDp = metrics.heightPixels / metrics.density;
// mc++             return Math.min(widthDp, heightDp);
// mc++         }
// mc++ 
// mc++         /**
// mc++          * Should a navigation bar appear at the bottom of the screen in the current
// mc++          * device configuration? A navigation bar may appear on the right side of
// mc++          * the screen in certain configurations.
// mc++          *
// mc++          * @return True if navigation should appear at the bottom of the screen, False otherwise.
// mc++          */
// mc++         public boolean isNavigationAtBottom() {
// mc++             return (mSmallestWidthDp >= 600 || mInPortrait);
// mc++         }
// mc++ 
// mc++         /**
// mc++          * Get the height of the system status bar.
// mc++          *
// mc++          * @return The height of the status bar (in pixels).
// mc++          */
// mc++         public int getStatusBarHeight() {
// mc++             return mStatusBarHeight;
// mc++         }
// mc++ 
// mc++         /**
// mc++          * Get the height of the action bar.
// mc++          *
// mc++          * @return The height of the action bar (in pixels).
// mc++          */
// mc++         public int getActionBarHeight() {
// mc++             return mActionBarHeight;
// mc++         }
// mc++ 
// mc++         /**
// mc++          * Does this device have a system navigation bar?
// mc++          *
// mc++          * @return True if this device uses soft key navigation, False otherwise.
// mc++          */
// mc++         public boolean hasNavigtionBar() {
// mc++             return mHasNavigationBar;
// mc++         }
// mc++ 
// mc++         /**
// mc++          * Get the height of the system navigation bar.
// mc++          *
// mc++          * @return The height of the navigation bar (in pixels). If the device does not have
// mc++          * soft navigation keys, this will always return 0.
// mc++          */
// mc++         public int getNavigationBarHeight() {
// mc++             return mNavigationBarHeight;
// mc++         }
// mc++ 
// mc++         /**
// mc++          * Get the width of the system navigation bar when it is placed vertically on the screen.
// mc++          *
// mc++          * @return The width of the navigation bar (in pixels). If the device does not have
// mc++          * soft navigation keys, this will always return 0.
// mc++          */
// mc++         public int getNavigationBarWidth() {
// mc++             return mNavigationBarWidth;
// mc++         }
// mc++ 
// mc++         /**
// mc++          * Get the layout inset for any system UI that appears at the top of the screen.
// mc++          *
// mc++          * @param withActionBar True to include the height of the action bar, False otherwise.
// mc++          * @return The layout inset (in pixels).
// mc++          */
// mc++         public int getPixelInsetTop(boolean withActionBar) {
// mc++             return (mTranslucentStatusBar ? mStatusBarHeight : 0) + (withActionBar ? mActionBarHeight : 0);
// mc++         }
// mc++ 
// mc++         /**
// mc++          * Get the layout inset for any system UI that appears at the bottom of the screen.
// mc++          *
// mc++          * @return The layout inset (in pixels).
// mc++          */
// mc++         public int getPixelInsetBottom() {
// mc++             if (mTranslucentNavBar && isNavigationAtBottom()) {
// mc++                 return mNavigationBarHeight;
// mc++             } else {
// mc++                 return 0;
// mc++             }
// mc++         }
// mc++ 
// mc++         /**
// mc++          * Get the layout inset for any system UI that appears at the right of the screen.
// mc++          *
// mc++          * @return The layout inset (in pixels).
// mc++          */
// mc++         public int getPixelInsetRight() {
// mc++             if (mTranslucentNavBar && !isNavigationAtBottom()) {
// mc++                 return mNavigationBarWidth;
// mc++             } else {
// mc++                 return 0;
// mc++             }
// mc++         }
// mc++ 
// mc++         private boolean hasNavBar(Context context) {
// mc++             Resources res = context.getResources();
// mc++             int resourceId = res.getIdentifier(SHOW_NAV_BAR_RES_NAME, "bool", "android");
// mc++             if (resourceId != 0) {
// mc++                 boolean hasNav = res.getBoolean(resourceId);
// mc++                 // check override flag (see static block)
// mc++                 if ("1".equals(sNavBarOverride)) {
// mc++                     hasNav = false;
// mc++                 } else if ("0".equals(sNavBarOverride)) {
// mc++                     hasNav = true;
// mc++                 }
// mc++                 return hasNav;
// mc++             } else { // fallback
// mc++                 return !hasPermanentMenuKey(context);
// mc++             }
// mc++         }
// mc++ 
// mc++         private boolean hasPermanentMenuKey(Context cxt) {
// mc++             try {
// mc++                 WindowManager wm = (WindowManager) cxt.getSystemService(Context.WINDOW_SERVICE);
// mc++ 
// mc++                 ViewConfiguration config = ViewConfiguration.get(cxt);
// mc++                 Field menuKeyField = ViewConfiguration.class
// mc++                         .getDeclaredField("sHasPermanentMenuKey");
// mc++                 if (menuKeyField != null) {
// mc++                     menuKeyField.setAccessible(true);
// mc++                     return menuKeyField.getBoolean(config);
// mc++                 }
// mc++             } catch (Exception e) {
// mc++                 e.printStackTrace();
// mc++             }
// mc++ 
// mc++             return false;
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++ }
