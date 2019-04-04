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
// mc++ import android.support.design.widget.FloatingActionButton;
// mc++ import android.support.design.widget.Snackbar;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.View;
// mc++ import android.webkit.WebChromeClient;
// mc++ import android.webkit.WebSettings;
// mc++ import android.webkit.WebView;
// mc++ import android.webkit.WebViewClient;
// mc++ 
// mc++ public class ScrollingActivity extends AppCompatActivity {
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_scrolling);
// mc++         Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
// mc++         setSupportActionBar(toolbar);
// mc++ 
// mc++         FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
// mc++         fab.setOnClickListener(new View.OnClickListener() {
// mc++             @Override
// mc++             public void onClick(View view) {
// mc++                 Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
// mc++                         .setAction("Action", null).show();
// mc++             }
// mc++         });
// mc++ 
// mc++         final WebView webView = (WebView) findViewById(R.id.web_view);
// mc++         webView.setWebViewClient(new WebViewClient() {
// mc++ 
// mc++         });
// mc++         final WebSettings settings = webView.getSettings();
// mc++         settings.setJavaScriptEnabled(true);
// mc++ 
// mc++         webView.setWebChromeClient(new WebChromeClient());
// mc++         webView.loadUrl("https://android.googlesource.com/");
// mc++ 
// mc++     }
// mc++ 
// mc++ }
