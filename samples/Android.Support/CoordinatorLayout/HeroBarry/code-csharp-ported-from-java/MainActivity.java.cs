// mc++ package saulmm.myapplication;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.AppBarLayout;
// mc++ import android.support.design.widget.CollapsingToolbarLayout;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.Menu;
// mc++ import android.view.View;
// mc++ import android.view.animation.AlphaAnimation;
// mc++ import android.widget.FrameLayout;
// mc++ import android.widget.ImageView;
// mc++ import android.widget.LinearLayout;
// mc++ import android.widget.TextView;
// mc++ 
// mc++ public class MainActivity extends AppCompatActivity
// mc++     implements AppBarLayout.OnOffsetChangedListener {
// mc++ 
// mc++     private static final float PERCENTAGE_TO_SHOW_TITLE_AT_TOOLBAR  = 0.9f;
// mc++     private static final float PERCENTAGE_TO_HIDE_TITLE_DETAILS     = 0.3f;
// mc++     private static final int ALPHA_ANIMATIONS_DURATION              = 200;
// mc++ 
// mc++     private boolean mIsTheTitleVisible          = false;
// mc++     private boolean mIsTheTitleContainerVisible = true;
// mc++ 
// mc++     private LinearLayout mTitleContainer;
// mc++     private TextView mTitle;
// mc++     private AppBarLayout mAppBarLayout;
// mc++     private ImageView mImageparallax;
// mc++     private FrameLayout mFrameParallax;
// mc++     private Toolbar mToolbar;
// mc++ 
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++ 
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_main);
// mc++ 
// mc++         bindActivity();
// mc++ 
// mc++         mToolbar.setTitle("");
// mc++         setSupportActionBar(mToolbar);
// mc++         startAlphaAnimation(mTitle, 0, View.INVISIBLE);
// mc++         mAppBarLayout.addOnOffsetChangedListener(this);
// mc++         initParallaxValues();
// mc++     }
// mc++ 
// mc++     private void bindActivity() {
// mc++ 
// mc++         mToolbar        = (Toolbar) findViewById(R.id.main_toolbar);
// mc++         mTitle          = (TextView) findViewById(R.id.main_textview_title);
// mc++         mTitleContainer = (LinearLayout) findViewById(R.id.main_linearlayout_title);
// mc++         mAppBarLayout   = (AppBarLayout) findViewById(R.id.main_appbar);
// mc++         mImageparallax  = (ImageView) findViewById(R.id.main_imageview_placeholder);
// mc++         mFrameParallax  = (FrameLayout) findViewById(R.id.main_framelayout_title);
// mc++ 
// mc++     }
// mc++ 
// mc++     private void initParallaxValues() {
// mc++ 
// mc++         CollapsingToolbarLayout.LayoutParams petDetailsLp =
// mc++             (CollapsingToolbarLayout.LayoutParams) mImageparallax.getLayoutParams();
// mc++ 
// mc++         CollapsingToolbarLayout.LayoutParams petBackgroundLp =
// mc++             (CollapsingToolbarLayout.LayoutParams) mFrameParallax.getLayoutParams();
// mc++ 
// mc++         petDetailsLp.setParallaxMultiplier(0.9f);
// mc++         petBackgroundLp.setParallaxMultiplier(0.3f);
// mc++ 
// mc++         mImageparallax.setLayoutParams(petDetailsLp);
// mc++         mFrameParallax.setLayoutParams(petBackgroundLp);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onCreateOptionsMenu(Menu menu) {
// mc++ 
// mc++         getMenuInflater().inflate(R.menu.menu_main, menu);
// mc++         return true;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onOffsetChanged(AppBarLayout appBarLayout, int offset) {
// mc++ 
// mc++         int maxScroll = appBarLayout.getTotalScrollRange();
// mc++         float percentage = (float) Math.abs(offset) / (float) maxScroll;
// mc++ 
// mc++         handleAlphaOnTitle(percentage);
// mc++         handleToolbarTitleVisibility(percentage);
// mc++ 
// mc++     }
// mc++ 
// mc++     private void handleToolbarTitleVisibility(float percentage) {
// mc++ 
// mc++             if (percentage >= PERCENTAGE_TO_SHOW_TITLE_AT_TOOLBAR) {
// mc++ 
// mc++                 if(!mIsTheTitleVisible) {
// mc++                     startAlphaAnimation(mTitle, ALPHA_ANIMATIONS_DURATION, View.VISIBLE);
// mc++                     mIsTheTitleVisible = true;
// mc++                 }
// mc++ 
// mc++             } else {
// mc++ 
// mc++                 if (mIsTheTitleVisible) {
// mc++                     startAlphaAnimation(mTitle, ALPHA_ANIMATIONS_DURATION, View.INVISIBLE);
// mc++                     mIsTheTitleVisible = false;
// mc++                 }
// mc++             }
// mc++     }
// mc++ 
// mc++     private void handleAlphaOnTitle(float percentage) {
// mc++ 
// mc++         if (percentage >= PERCENTAGE_TO_HIDE_TITLE_DETAILS) {
// mc++ 
// mc++             if(mIsTheTitleContainerVisible) {
// mc++                 startAlphaAnimation(mTitleContainer, ALPHA_ANIMATIONS_DURATION, View.INVISIBLE);
// mc++                 mIsTheTitleContainerVisible = false;
// mc++             }
// mc++ 
// mc++         } else {
// mc++ 
// mc++             if (!mIsTheTitleContainerVisible) {
// mc++                 startAlphaAnimation(mTitleContainer, ALPHA_ANIMATIONS_DURATION, View.VISIBLE);
// mc++                 mIsTheTitleContainerVisible = true;
// mc++             }
// mc++         }
// mc++     }
// mc++ 
// mc++     public static void startAlphaAnimation (View v, long duration, int visibility) {
// mc++ 
// mc++         AlphaAnimation alphaAnimation = (visibility == View.VISIBLE)
// mc++             ? new AlphaAnimation(0f, 1f)
// mc++             : new AlphaAnimation(1f, 0f);
// mc++ 
// mc++         alphaAnimation.setDuration(duration);
// mc++         alphaAnimation.setFillAfter(true);
// mc++         v.startAnimation(alphaAnimation);
// mc++     }
// mc++ }
