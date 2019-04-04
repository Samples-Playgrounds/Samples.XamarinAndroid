// mc++ package com.zanon.sample.coordinatorlayout;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.content.Intent;
// mc++ import android.graphics.Color;
// mc++ import android.graphics.PorterDuff;
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.AppBarLayout;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.view.View;
// mc++ import android.widget.ImageView;
// mc++ 
// mc++ import com.zanon.coordinatorlayout.R;
// mc++ 
// mc++ /**
// mc++  * Created by romain on 20/10/15.
// mc++  */
// mc++ public class CustomBehaviorActivity extends AppCompatActivity
// mc++         implements AppBarLayout.OnOffsetChangedListener {
// mc++ 
// mc++     private View mHeader;
// mc++ 
// mc++     public static void start(Context context) {
// mc++         Intent intent = new Intent(context, CustomBehaviorActivity.class);
// mc++         context.startActivity(intent);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_custom_behavior);
// mc++ 
// mc++         mHeader = findViewById(R.id.activity_custom_behavior_header);
// mc++ 
// mc++         AppBarLayout appBarLayout = (AppBarLayout) findViewById(R.id.appbar);
// mc++         appBarLayout.addOnOffsetChangedListener(this);
// mc++ 
// mc++         ImageView teamImage = (ImageView) findViewById(R.id.custom_behavior_image_teamA);
// mc++         teamImage.setColorFilter(Color.BLUE, PorterDuff.Mode.SRC_IN);
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onOffsetChanged(AppBarLayout appBarLayout, int verticalOffset) {
// mc++         int maxScroll = appBarLayout.getTotalScrollRange();
// mc++         float percentage = (float) Math.abs(verticalOffset) / (float) (maxScroll - mHeader.getHeight());
// mc++         mHeader.setAlpha(percentage);
// mc++     }
// mc++ }
