// mc++ package com.xujun.contralayout;
// mc++ 
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.view.View;
// mc++ import android.widget.ImageView;
// mc++ 
// mc++ /**
// mc++  * @author xujun  on 2016/11/30.
// mc++  * @email gdutxiaoxu@163.com
// mc++  */
// mc++ 
// mc++ public class CustomBevior extends CoordinatorLayout.Behavior<ImageView> {
// mc++ 
// mc++     @Override
// mc++     public boolean layoutDependsOn(CoordinatorLayout parent,
// mc++                                    ImageView child, View dependency) {
// mc++         return super.layoutDependsOn(parent, child, dependency);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onDependentViewChanged(CoordinatorLayout parent, ImageView child, View
// mc++             dependency) {
// mc++         return super.onDependentViewChanged(parent, child, dependency);
// mc++     }
// mc++ }
