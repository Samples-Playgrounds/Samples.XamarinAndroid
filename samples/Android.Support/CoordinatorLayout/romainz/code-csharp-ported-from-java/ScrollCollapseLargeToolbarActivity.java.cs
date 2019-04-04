// mc++ package com.zanon.sample.coordinatorlayout;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.content.Intent;
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.CollapsingToolbarLayout;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.v4.widget.NestedScrollView;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.MenuItem;
// mc++ import android.view.View;
// mc++ 
// mc++ import com.zanon.coordinatorlayout.R;
// mc++ 
// mc++ /**
// mc++  * Created by romain on 15/10/15.
// mc++  */
// mc++ public class ScrollCollapseLargeToolbarActivity extends AppCompatActivity {
// mc++ 
// mc++     private View mFab;
// mc++     private int mFabTranslationSize = 0;
// mc++     private boolean mFabIsVisible = true;
// mc++ 
// mc++     public static void start(Context context) {
// mc++         Intent intent = new Intent(context, ScrollCollapseLargeToolbarActivity.class);
// mc++         context.startActivity(intent);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_scroll_collapse_large_toolbar);
// mc++ 
// mc++         Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
// mc++         setSupportActionBar(toolbar);
// mc++         getSupportActionBar().setDisplayHomeAsUpEnabled(true);
// mc++ 
// mc++         String toolbarTitle = getResources().getString(R.string.sample_collapse_scroll_toolbar);
// mc++         CollapsingToolbarLayout collapsingToolbar =
// mc++                 (CollapsingToolbarLayout) findViewById(R.id.collapsing_toolbar);
// mc++         collapsingToolbar.setTitle(toolbarTitle);
// mc++ 
// mc++         mFab = findViewById(R.id.fab);
// mc++ 
// mc++         NestedScrollView scrollView = (NestedScrollView) findViewById(R.id.nested_scrollview);
// mc++         scrollView.setOnScrollChangeListener(new NestedScrollView.OnScrollChangeListener() {
// mc++             @Override
// mc++             public void onScrollChange(NestedScrollView v, int scrollX, int scrollY, int oldScrollX, int oldScrollY) {
// mc++                 if (scrollY > oldScrollY) {
// mc++                     animateFab(false);
// mc++                 } else {
// mc++                     animateFab(true);
// mc++                 }
// mc++             }
// mc++         });
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onOptionsItemSelected(MenuItem item) {
// mc++         switch (item.getItemId()) {
// mc++             case android.R.id.home:
// mc++                 finish();
// mc++                 return true;
// mc++         }
// mc++         return super.onOptionsItemSelected(item);
// mc++     }
// mc++ 
// mc++     private void animateFab(boolean show) {
// mc++         if (show && !mFabIsVisible) {
// mc++             //Show
// mc++             mFabIsVisible = true;
// mc++             mFab.animate().translationY(0);
// mc++         } else if (!show && mFabIsVisible) {
// mc++             //Hide
// mc++             mFabIsVisible = false;
// mc++             if (mFabTranslationSize == 0) {
// mc++                 int margin = ((CoordinatorLayout.LayoutParams) mFab.getLayoutParams()).bottomMargin;
// mc++                 mFabTranslationSize = mFab.getHeight() + margin * 2;
// mc++             }
// mc++             mFab.animate().translationY(mFabTranslationSize);
// mc++         }
// mc++     }
// mc++ }
