// mc++ package com.zanon.sample.coordinatorlayout;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.view.View;
// mc++ 
// mc++ import com.zanon.coordinatorlayout.R;
// mc++ 
// mc++ /**
// mc++  * Created by romain on 15/10/15.
// mc++  */
// mc++ public class MainActivity extends AppCompatActivity implements View.OnClickListener {
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_main);
// mc++ 
// mc++         findViewById(R.id.large_toolbar_fix).setOnClickListener(this);
// mc++         findViewById(R.id.scroll_toolbar).setOnClickListener(this);
// mc++         findViewById(R.id.collapse_large_toolbar).setOnClickListener(this);
// mc++         findViewById(R.id.scroll_collapse_large_toolbar).setOnClickListener(this);
// mc++         findViewById(R.id.parallax_image_toolbar).setOnClickListener(this);
// mc++         findViewById(R.id.custom_behavior).setOnClickListener(this);
// mc++         findViewById(R.id.snackbar).setOnClickListener(this);
// mc++         findViewById(R.id.pager_tabs).setOnClickListener(this);
// mc++         findViewById(R.id.pager_tabs_fixed_image).setOnClickListener(this);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onClick(View v) {
// mc++         switch (v.getId()) {
// mc++             case R.id.large_toolbar_fix:
// mc++                 LargeToolbarFixActivity.start(this);
// mc++                 break;
// mc++             case R.id.scroll_toolbar:
// mc++                 ScrollToolbarActivity.start(this);
// mc++                 break;
// mc++             case R.id.collapse_large_toolbar:
// mc++                 CollapseLargeToolbarActivity.start(this);
// mc++                 break;
// mc++             case R.id.scroll_collapse_large_toolbar:
// mc++                 ScrollCollapseLargeToolbarActivity.start(this);
// mc++                 break;
// mc++             case R.id.parallax_image_toolbar:
// mc++                 ParallaxImageToolbarActivity.start(this);
// mc++                 break;
// mc++             case R.id.custom_behavior:
// mc++                 CustomBehaviorActivity.start(this);
// mc++                 break;
// mc++             case R.id.snackbar:
// mc++                 SnackbarActivity.start(this);
// mc++                 break;
// mc++             case R.id.pager_tabs:
// mc++                 PagerTabsActivity.start(this);
// mc++                 break;
// mc++             case R.id.pager_tabs_fixed_image:
// mc++                 PagerTabsParallaxImageActivity.start(this);
// mc++                 break;
// mc++             default:
// mc++                 break;
// mc++         }
// mc++     }
// mc++ }
