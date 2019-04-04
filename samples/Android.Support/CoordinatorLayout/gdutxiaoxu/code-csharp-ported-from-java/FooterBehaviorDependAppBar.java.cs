// mc++ package com.xujun.contralayout.behavior;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.design.widget.AppBarLayout;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.util.AttributeSet;
// mc++ import android.util.Log;
// mc++ import android.view.View;
// mc++ 
// mc++ /**
// mc++  * 知乎效果底部behavior 依赖于 AppBarLayout
// mc++  *
// mc++  * @author xujun  on 2016/11/30.
// mc++  * @email gdutxiaoxu@163.com
// mc++  */
// mc++ 
// mc++ public class FooterBehaviorDependAppBar extends CoordinatorLayout.Behavior<View> {
// mc++ 
// mc++     public static final String TAG = "xujun";
// mc++ 
// mc++     public FooterBehaviorDependAppBar(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++     }
// mc++ 
// mc++     //当 dependency instanceof AppBarLayout 返回TRUE，将会调用onDependentViewChanged（）方法
// mc++     @Override
// mc++     public boolean layoutDependsOn(CoordinatorLayout parent, View child, View dependency) {
// mc++         return dependency instanceof AppBarLayout;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onDependentViewChanged(CoordinatorLayout parent, View child, View dependency) {
// mc++         //根据dependency top值的变化改变 child 的 translationY
// mc++         float translationY = Math.abs(dependency.getTop());
// mc++         child.setTranslationY(translationY);
// mc++         Log.i(TAG, "onDependentViewChanged: " + translationY);
// mc++         return true;
// mc++ 
// mc++     }
// mc++ }
