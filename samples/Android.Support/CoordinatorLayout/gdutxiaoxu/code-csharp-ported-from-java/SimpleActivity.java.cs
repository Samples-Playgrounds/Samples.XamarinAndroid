// mc++ package com.jay.supportlibrarydemo;
// mc++ 
// mc++ import android.app.Activity;
// mc++ import android.content.Intent;
// mc++ import android.support.annotation.IdRes;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.view.View;
// mc++ import android.widget.Toast;
// mc++ 
// mc++ /**
// mc++  * Created by Q.Jay on 2016/4/30 15:21
// mc++  *
// mc++  * @version 1.0.0
// mc++  */
// mc++ public class SimpleActivity extends AppCompatActivity {
// mc++ 
// mc++     public void openActivity(Class<? extends Activity> cls) {
// mc++         startActivity(new Intent(this, cls));
// mc++     }
// mc++ 
// mc++     public void setOnClickListener(View.OnClickListener listener,@IdRes int... ids) {
// mc++         for (int id : ids) {
// mc++             findViewById(id).setOnClickListener(listener);
// mc++         }
// mc++     }
// mc++     public void toast(String text){
// mc++         Toast.makeText(SimpleActivity.this, text, Toast.LENGTH_SHORT).show();
// mc++     }
// mc++ }
