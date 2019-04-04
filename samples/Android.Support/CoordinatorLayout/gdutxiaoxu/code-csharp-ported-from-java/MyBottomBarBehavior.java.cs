// mc++ package com.example.zcp.coordinatorlayoutdemo.behavior;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.design.widget.AppBarLayout;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.util.AttributeSet;
// mc++ import android.view.View;
// mc++ 
// mc++ /**
// mc++  * Created by 赵晨璞 on 2016/7/4.
// mc++  */
// mc++ 
// mc++ public class MyBottomBarBehavior extends CoordinatorLayout.Behavior<View> {
// mc++ 
// mc++ 
// mc++     public MyBottomBarBehavior(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean layoutDependsOn(CoordinatorLayout parent, View child, View dependency) {
// mc++         //这个方法是说明这个子控件是依赖AppBarLayout的
// mc++         return dependency instanceof AppBarLayout;
// mc++     }
// mc++ 
// mc++ 
// mc++     @Override
// mc++     public boolean onDependentViewChanged(CoordinatorLayout parent, View child, View dependency) {
// mc++ 
// mc++         float translationY = Math.abs(dependency.getTop());//获取更随布局的顶部位置
// mc++         child.setTranslationY(translationY);
// mc++         return true;
// mc++     }
// mc++ 
// mc++ }
