// mc++ package com.example.zcp.coordinatorlayoutdemo.ui;
// mc++ 
// mc++ import android.graphics.Color;
// mc++ import android.support.design.widget.Snackbar;
// mc++ import android.view.Gravity;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.widget.LinearLayout;
// mc++ import android.widget.TextView;
// mc++ 
// mc++ import com.example.zcp.coordinatorlayoutdemo.R;
// mc++ 
// mc++ /**
// mc++  * Created by 赵晨璞 on 2016/6/16.
// mc++  * 彩色Snackbar工具类，请看我之前的文章《没时间解释了，快使用Snackbar!——Android Snackbar花式使用指南》
// mc++  * http://www.jianshu.com/p/cd1e80e64311
// mc++  */
// mc++ public class SnackbarUtil {
// mc++ 
// mc++     public static final   int Info = 1;
// mc++     public static final  int Confirm = 2;
// mc++     public static final  int Warning = 3;
// mc++     public static final  int Alert = 4;
// mc++ 
// mc++ 
// mc++     public static  int red = 0xfff44336;
// mc++     public static  int green = 0xff4caf50;
// mc++     public static  int blue = 0xff2195f3;
// mc++     public static  int orange = 0xffffc107;
// mc++ 
// mc++     /**
// mc++      * 短显示Snackbar，自定义颜色
// mc++      * @param view
// mc++      * @param message
// mc++      * @param messageColor
// mc++      * @param backgroundColor
// mc++      * @return
// mc++      */
// mc++     public static Snackbar ShortSnackbar(View view, String message, int messageColor, int backgroundColor){
// mc++         Snackbar snackbar = Snackbar.make(view,message, Snackbar.LENGTH_SHORT);
// mc++         setSnackbarColor(snackbar,messageColor,backgroundColor);
// mc++         return snackbar;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 长显示Snackbar，自定义颜色
// mc++      * @param view
// mc++      * @param message
// mc++      * @param messageColor
// mc++      * @param backgroundColor
// mc++      * @return
// mc++      */
// mc++     public static Snackbar LongSnackbar(View view, String message, int messageColor, int backgroundColor){
// mc++         Snackbar snackbar = Snackbar.make(view,message, Snackbar.LENGTH_LONG);
// mc++         setSnackbarColor(snackbar,messageColor,backgroundColor);
// mc++         return snackbar;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 自定义时常显示Snackbar，自定义颜色
// mc++      * @param view
// mc++      * @param message
// mc++      * @param messageColor
// mc++      * @param backgroundColor
// mc++      * @return
// mc++      */
// mc++     public static Snackbar IndefiniteSnackbar(View view, String message, int duration, int messageColor, int backgroundColor){
// mc++         Snackbar snackbar = Snackbar.make(view,message, Snackbar.LENGTH_INDEFINITE).setDuration(duration);
// mc++         setSnackbarColor(snackbar,messageColor,backgroundColor);
// mc++         return snackbar;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 短显示Snackbar，可选预设类型
// mc++      * @param view
// mc++      * @param message
// mc++      * @param type
// mc++      * @return
// mc++      */
// mc++     public static Snackbar ShortSnackbar(View view, String message, int type){
// mc++         Snackbar snackbar = Snackbar.make(view,message, Snackbar.LENGTH_SHORT);
// mc++         switchType(snackbar,type);
// mc++         return snackbar;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 长显示Snackbar，可选预设类型
// mc++      * @param view
// mc++      * @param message
// mc++      * @param type
// mc++      * @return
// mc++      */
// mc++     public static Snackbar LongSnackbar(View view, String message, int type){
// mc++         Snackbar snackbar = Snackbar.make(view,message, Snackbar.LENGTH_LONG);
// mc++         switchType(snackbar,type);
// mc++         return snackbar;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 自定义时常显示Snackbar，可选预设类型
// mc++      * @param view
// mc++      * @param message
// mc++      * @param type
// mc++      * @return
// mc++      */
// mc++     public static Snackbar IndefiniteSnackbar(View view, String message, int duration, int type){
// mc++         Snackbar snackbar = Snackbar.make(view,message, Snackbar.LENGTH_INDEFINITE).setDuration(duration);
// mc++         switchType(snackbar,type);
// mc++         return snackbar;
// mc++     }
// mc++ 
// mc++     //选择预设类型
// mc++     private static void switchType(Snackbar snackbar, int type){
// mc++         switch (type){
// mc++             case Info:
// mc++                 setSnackbarColor(snackbar,blue);
// mc++                 break;
// mc++             case Confirm:
// mc++                 setSnackbarColor(snackbar,green);
// mc++                 break;
// mc++             case Warning:
// mc++                 setSnackbarColor(snackbar,orange);
// mc++                 break;
// mc++             case Alert:
// mc++                 setSnackbarColor(snackbar, Color.YELLOW,red);
// mc++                 break;
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 设置Snackbar背景颜色
// mc++      * @param snackbar
// mc++      * @param backgroundColor
// mc++      */
// mc++     public static void setSnackbarColor(Snackbar snackbar, int backgroundColor) {
// mc++         View view = snackbar.getView();
// mc++         if(view!=null){
// mc++             view.setBackgroundColor(backgroundColor);
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 设置Snackbar文字和背景颜色
// mc++      * @param snackbar
// mc++      * @param messageColor
// mc++      * @param backgroundColor
// mc++      */
// mc++     public static void setSnackbarColor(Snackbar snackbar, int messageColor, int backgroundColor) {
// mc++         View view = snackbar.getView();
// mc++         if(view!=null){
// mc++             view.setBackgroundColor(backgroundColor);
// mc++             ((TextView) view.findViewById(R.id.snackbar_text)).setTextColor(messageColor);
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 向Snackbar中添加view
// mc++      * @param snackbar
// mc++      * @param layoutId
// mc++      * @param index 新加布局在Snackbar中的位置
// mc++      */
// mc++     public static void SnackbarAddView(Snackbar snackbar, int layoutId, int index) {
// mc++         View snackbarview = snackbar.getView();
// mc++         Snackbar.SnackbarLayout snackbarLayout=(Snackbar.SnackbarLayout)snackbarview;
// mc++ 
// mc++         View add_view = LayoutInflater.from(snackbarview.getContext()).inflate(layoutId,null);
// mc++ 
// mc++         LinearLayout.LayoutParams p = new LinearLayout.LayoutParams( LinearLayout.LayoutParams.WRAP_CONTENT, LinearLayout.LayoutParams.WRAP_CONTENT);
// mc++         p.gravity= Gravity.CENTER_VERTICAL;
// mc++ 
// mc++         snackbarLayout.addView(add_view,index,p);
// mc++     }
// mc++ }
