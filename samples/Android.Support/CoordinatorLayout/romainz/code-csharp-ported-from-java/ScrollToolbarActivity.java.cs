// mc++ package com.zanon.sample.coordinatorlayout;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.content.Intent;
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.MenuItem;
// mc++ 
// mc++ import com.zanon.coordinatorlayout.R;
// mc++ 
// mc++ /**
// mc++  * Created by romain on 15/10/15.
// mc++  */
// mc++ public class ScrollToolbarActivity extends AppCompatActivity {
// mc++ 
// mc++     public static void start(Context context) {
// mc++         Intent intent = new Intent(context, ScrollToolbarActivity.class);
// mc++         context.startActivity(intent);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_scroll_toolbar);
// mc++ 
// mc++         Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
// mc++         setSupportActionBar(toolbar);
// mc++         getSupportActionBar().setDisplayHomeAsUpEnabled(true);
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
// mc++ }
