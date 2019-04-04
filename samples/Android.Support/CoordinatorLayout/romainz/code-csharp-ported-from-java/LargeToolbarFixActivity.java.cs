// mc++ package com.zanon.sample.coordinatorlayout;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.content.Intent;
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.FloatingActionButton;
// mc++ import android.support.design.widget.Snackbar;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.View;
// mc++ import android.view.Menu;
// mc++ import android.view.MenuItem;
// mc++ 
// mc++ import com.zanon.coordinatorlayout.R;
// mc++ 
// mc++ public class LargeToolbarFixActivity extends AppCompatActivity {
// mc++ 
// mc++     public static void start(Context context) {
// mc++         Intent intent = new Intent(context, LargeToolbarFixActivity.class);
// mc++         context.startActivity(intent);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_large_toolbar_fix);
// mc++         Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
// mc++         setSupportActionBar(toolbar);
// mc++         getSupportActionBar().setDisplayHomeAsUpEnabled(true);
// mc++ 
// mc++         FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
// mc++         fab.setOnClickListener(new View.OnClickListener() {
// mc++             @Override
// mc++             public void onClick(View view) {
// mc++                 Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
// mc++                         .setAction("Action", null).show();
// mc++             }
// mc++         });
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onCreateOptionsMenu(Menu menu) {
// mc++         // Inflate the menu; this adds items to the action bar if it is present.
// mc++         getMenuInflater().inflate(R.menu.menu_scrolling, menu);
// mc++         return true;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onOptionsItemSelected(MenuItem item) {
// mc++         // Handle action bar item clicks here. The action bar will
// mc++         // automatically handle clicks on the Home/Up button, so long
// mc++         // as you specify a parent activity in AndroidManifest.xml.
// mc++         int id = item.getItemId();
// mc++ 
// mc++         //noinspection SimplifiableIfStatement
// mc++         if (id == R.id.action_settings) {
// mc++             return true;
// mc++         } else if (id == android.R.id.home) {
// mc++             finish();
// mc++             return true;
// mc++         }
// mc++         return super.onOptionsItemSelected(item);
// mc++     }
// mc++ 
// mc++ }
