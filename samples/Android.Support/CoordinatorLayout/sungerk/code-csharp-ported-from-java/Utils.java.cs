// mc++ package sunger.net.org.coordinatorlayoutdemos.utils;
// mc++ 
// mc++ /**
// mc++  * Created by sunger on 2015/12/15.
// mc++  */
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.content.res.TypedArray;
// mc++ 
// mc++ import sunger.net.org.coordinatorlayoutdemos.R;
// mc++ 
// mc++ public class Utils {
// mc++ 
// mc++     public static int getToolbarHeight(Context context) {
// mc++         final TypedArray styledAttributes = context.getTheme().obtainStyledAttributes(
// mc++                 new int[]{R.attr.actionBarSize});
// mc++         int toolbarHeight = (int) styledAttributes.getDimension(0, 0);
// mc++         styledAttributes.recycle();
// mc++ 
// mc++         return toolbarHeight;
// mc++     }
// mc++ }
