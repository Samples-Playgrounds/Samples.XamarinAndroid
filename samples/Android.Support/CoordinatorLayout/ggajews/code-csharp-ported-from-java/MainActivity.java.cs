// mc++ package com.getbase.coordinatorlayoutdemo;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.Snackbar;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.view.View;
// mc++ 
// mc++ public class MainActivity extends AppCompatActivity {
// mc++ 
// mc++   @Override
// mc++   protected void onCreate(Bundle savedInstanceState) {
// mc++     super.onCreate(savedInstanceState);
// mc++     setContentView(R.layout.activity_main);
// mc++     findViewById(R.id.fab).setOnClickListener(new View.OnClickListener() {
// mc++       @Override
// mc++       public void onClick(View view) {
// mc++         Snackbar.make(view, "Hello Snackbar", Snackbar.LENGTH_LONG).show();
// mc++       }
// mc++     });
// mc++   }
// mc++ }
