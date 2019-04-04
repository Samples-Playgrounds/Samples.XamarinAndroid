// mc++ /*
// mc++  * Copyright (C) 2017
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
// mc++ package saulmm.coordinatorexamples;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.content.Intent;
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.View;
// mc++ 
// mc++ public class IOActivityExample extends AppCompatActivity {
// mc++ 
// mc++ 	@Override
// mc++ 	public void onCreate(Bundle savedInstanceState) {
// mc++ 		super.onCreate(savedInstanceState);
// mc++ 		setContentView(R.layout.activity_io_detail);
// mc++ 
// mc++ 		Toolbar toolbar = (Toolbar) findViewById(R.id.ioexample_toolbar);
// mc++ 		toolbar.setNavigationOnClickListener(new View.OnClickListener() {
// mc++ 			@Override public void onClick(View v) {
// mc++ 				onBackPressed();
// mc++ 			}
// mc++ 		});
// mc++ 	}
// mc++ 
// mc++ 	public static void start(Context c) {
// mc++ 		c.startActivity(new Intent(c, IOActivityExample.class));
// mc++ 	}}
