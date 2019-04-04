// mc++ package com.xujun.contralayout.base.permissions;
// mc++ 
// mc++ import android.app.Activity;
// mc++ import android.os.Build;
// mc++ import android.support.annotation.NonNull;
// mc++ import android.support.annotation.RequiresApi;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ import static android.support.v4.app.ActivityCompat.shouldShowRequestPermissionRationale;
// mc++ 
// mc++ /**
// mc++  * @author meitu.xujun  on 2017/4/7 09:53
// mc++  * @version 0.1
// mc++  */
// mc++ 
// mc++ public class PermissionHelper {
// mc++ 
// mc++     public static boolean isM() {
// mc++         return Build.VERSION.SDK_INT >= Build.VERSION_CODES.M;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 处理是否有权限被永久拒绝
// mc++      *
// mc++      * @param activity
// mc++      * @param deniedPermissions    被拒绝的权限（包括永久被拒绝的权限和只被拒绝一次的权限），处理完之后变成只被拒绝一次的权限
// mc++      * @param permanentPermissions 永久被拒绝的权限（勾选了不再提醒）
// mc++      * @return 如果有权限被永久拒绝，返回 true，否则返回 false。
// mc++      */
// mc++     @RequiresApi(api = Build.VERSION_CODES.M)
// mc++     public static boolean handlePermissionPermanentlyDenied(@NonNull Activity activity,
// mc++                                                             @NonNull List<String>
// mc++                                                                     deniedPermissions, List<String>
// mc++                                                                     permanentPermissions) {
// mc++         for (String deniedPermission : deniedPermissions) {
// mc++             if (permissionPermanentlyDenied(activity, deniedPermission)) {
// mc++                 permanentPermissions.add(deniedPermission);
// mc++                 deniedPermissions.remove(deniedPermission);
// mc++             }
// mc++ 
// mc++         }
// mc++         if (!permanentPermissions.isEmpty()) {
// mc++             return true;
// mc++         }
// mc++         return false;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 判断权限是否被永久拒绝
// mc++      *
// mc++      * @param activity
// mc++      * @param deniedPermission
// mc++      * @return
// mc++      */
// mc++     @RequiresApi(api = Build.VERSION_CODES.M)
// mc++     public static boolean permissionPermanentlyDenied(@NonNull Activity activity,
// mc++                                                       @NonNull String deniedPermission) {
// mc++         return !shouldShowRequestPermissionRationale(activity, deniedPermission);
// mc++     }
// mc++ }
