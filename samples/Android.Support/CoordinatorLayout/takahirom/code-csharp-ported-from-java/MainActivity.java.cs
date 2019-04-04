// mc++ /*
// mc++  * Copyright (C) 2015 takahirom
// mc++  *
// mc++  * Licensed under the Apache License, Version 2.0 (the "License");
// mc++  * you may not use this file except in compliance with the License.
// mc++  * You may obtain a copy of the License at
// mc++  *
// mc++  *      http://www.apache.org/licenses/LICENSE-2.0
// mc++  *
// mc++  * Unless required by applicable law or agreed to in writing, software
// mc++  * distributed under the License is distributed on an "AS IS" BASIS,
// mc++  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// mc++  * See the License for the specific language governing permissions and
// mc++  * limitations under the License.
// mc++  */
// mc++ 
// mc++ package com.github.takahirom.webview_in_coodinator_layout;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.Menu;
// mc++ import android.view.MenuItem;
// mc++ import android.webkit.WebChromeClient;
// mc++ import android.webkit.WebView;
// mc++ import android.webkit.WebViewClient;
// mc++ 
// mc++ public class MainActivity extends AppCompatActivity {
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_main);
// mc++         Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
// mc++         setSupportActionBar(toolbar);
// mc++         final WebView webView = (WebView) findViewById(R.id.web_view);
// mc++         webView.setWebViewClient(new WebViewClient() {
// mc++ 
// mc++         });
// mc++         webView.getSettings().setJavaScriptEnabled(true);
// mc++         webView.setWebChromeClient(new WebChromeClient());
// mc++         webView.loadUrl("https://en.wikipedia.org/w/index.php?title=Android_(operating_system)&mobileaction=toggle_view_desktop");
// mc++     }
// mc++ 
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
