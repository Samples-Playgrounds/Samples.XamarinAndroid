// mc++ package com.zanon.sample.coordinatorlayout;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.content.Intent;
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.Snackbar;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.view.MenuItem;
// mc++ import android.view.View;
// mc++ 
// mc++ import com.zanon.coordinatorlayout.R;
// mc++ 
// mc++ /**
// mc++  * Created by romain on 15/10/15.
// mc++  */
// mc++ public class SnackbarActivity extends AppCompatActivity implements View.OnClickListener {
// mc++ 
// mc++     private Snackbar mSnackbar;
// mc++ 
// mc++     public static void start(Context context) {
// mc++         Intent intent = new Intent(context, SnackbarActivity.class);
// mc++         context.startActivity(intent);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_snackbar);
// mc++ 
// mc++         View fab = findViewById(R.id.fab);
// mc++         fab.setOnClickListener(this);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onClick(View v) {
// mc++         switch (v.getId()) {
// mc++             case R.id.fab:
// mc++                 showSnackbar(v, "Click on FAB");
// mc++                 break;
// mc++             default:
// mc++                 break;
// mc++         }
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
// mc++     private void showSnackbar(View view, String message) {
// mc++         if (mSnackbar != null && mSnackbar.isShown()) {
// mc++             mSnackbar.dismiss();
// mc++             mSnackbar = null;
// mc++         } else {
// mc++             mSnackbar = Snackbar.make(view, message, Snackbar.LENGTH_SHORT);
// mc++             mSnackbar.show();
// mc++         }
// mc++     }
// mc++ }
