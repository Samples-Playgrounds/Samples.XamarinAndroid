// mc++ package com.example.zcp.coordinatorlayoutdemo.activity;
// mc++ 
// mc++ import android.content.Intent;
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.BottomSheetBehavior;
// mc++ import android.support.design.widget.FloatingActionButton;
// mc++ import android.support.design.widget.Snackbar;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.View;
// mc++ import android.view.Menu;
// mc++ import android.view.MenuItem;
// mc++ 
// mc++ import com.example.zcp.coordinatorlayoutdemo.R;
// mc++ 
// mc++ public class MainActivity extends AppCompatActivity {
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_main);
// mc++         Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
// mc++         setSupportActionBar(toolbar);
// mc++ 
// mc++        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
// mc++         fab.setOnClickListener(new View.OnClickListener() {
// mc++             @Override
// mc++             public void onClick(View view) {
// mc++                 Snackbar.make(view, "Snackbar可以左滑取消哦~", Snackbar.LENGTH_LONG)
// mc++                         .setAction("Action", null).show();
// mc++             }
// mc++         });
// mc++ 
// mc++         findViewById(R.id.my_behavior_bt).setOnClickListener(new View.OnClickListener() {
// mc++             @Override
// mc++             public void onClick(View v) {
// mc++                 Intent intent=new Intent(MainActivity.this,MyBehaviorActivity.class);
// mc++                 startActivity(intent);
// mc++             }
// mc++         });
// mc++ 
// mc++         findViewById(R.id.bottom_sheet_bt).setOnClickListener(new View.OnClickListener() {
// mc++             @Override
// mc++             public void onClick(View v) {
// mc++                 Intent intent=new Intent(MainActivity.this,BottomSheetActivity.class);
// mc++                 startActivity(intent);
// mc++             }
// mc++         });
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onCreateOptionsMenu(Menu menu) {
// mc++         // Inflate the menu; this adds items to the action bar if it is present.
// mc++         getMenuInflater().inflate(R.menu.menu_main, menu);
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
// mc++         }
// mc++ 
// mc++         return super.onOptionsItemSelected(item);
// mc++     }
// mc++ }
