// mc++ /*
// mc++  * Copyright (C) 2015 takahirom
// mc++  *
// mc++  * Licensed under the Apache License, Version 2.0 (the "License");
// mc++  * you may not use this file except in compliance with the License.
// mc++  * You may obtain a copy of the License at
// mc++  *
// mc++  *      http://www.apache.org/licenses/LICENSE-2.0
// mc++  *
// mc++  * Unless required by applicable law or agreed to in writing, software
// mc++  * distributed under the License is distributed on an "AS IS" BASIS,
// mc++  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// mc++  * See the License for the specific language governing permissions and
// mc++  * limitations under the License.
// mc++  */
// mc++ 
// mc++ package com.github.takahirom.webview_in_coodinator_layout;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.design.widget.AppBarLayout;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.v4.view.ViewCompat;
// mc++ import android.util.AttributeSet;
// mc++ import android.view.View;
// mc++ import android.widget.LinearLayout;
// mc++ 
// mc++ public class BottomBarBehavior extends CoordinatorLayout.Behavior<LinearLayout> {
// mc++ 
// mc++     private int defaultDependencyTop = -1;
// mc++ 
// mc++     public BottomBarBehavior(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean layoutDependsOn(CoordinatorLayout parent, LinearLayout child, View dependency) {
// mc++         return dependency instanceof AppBarLayout;
// mc++     }
// mc++ 
// mc++ 
// mc++     @Override
// mc++     public boolean onDependentViewChanged(CoordinatorLayout parent, LinearLayout child, View dependency) {
// mc++         if (defaultDependencyTop == -1) {
// mc++             defaultDependencyTop = dependency.getTop();
// mc++         }
// mc++         child.setTranslationY(-dependency.getTop() + defaultDependencyTop);
// mc++         return true;
// mc++     }
// mc++ 
// mc++ }
