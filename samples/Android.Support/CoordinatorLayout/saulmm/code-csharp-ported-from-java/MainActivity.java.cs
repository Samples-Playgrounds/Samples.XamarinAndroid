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
// mc++ import android.content.Intent;
// mc++ import android.net.Uri;
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.MenuItem;
// mc++ import android.view.View;
// mc++ 
// mc++ public class MainActivity extends AppCompatActivity implements View.OnClickListener {
// mc++ 	private static final String GITHUB_REPO_URL = "https://github.com/saulmm/CoordinatorExamples";
// mc++ 
// mc++ 	@Override
// mc++ 	protected void onCreate(Bundle savedInstanceState) {
// mc++ 		super.onCreate(savedInstanceState);
// mc++ 		setContentView(R.layout.activity_main);
// mc++ 
// mc++ 		Toolbar toolbar = (Toolbar) findViewById(R.id.main_toolbar);
// mc++ 		toolbar.inflateMenu(R.menu.main);
// mc++ 		toolbar.setOnMenuItemClickListener(new Toolbar.OnMenuItemClickListener() {
// mc++ 			@Override public boolean onMenuItemClick(MenuItem item) {
// mc++ 				Intent browserIntent = new Intent(Intent.ACTION_VIEW, Uri.parse(GITHUB_REPO_URL));
// mc++ 				startActivity(browserIntent);
// mc++ 				return true;
// mc++ 			}
// mc++ 		});
// mc++ 
// mc++ 		findViewById(R.id.main_coordinator_textview).setOnClickListener(this);
// mc++ 		findViewById(R.id.main_materialup_textview).setOnClickListener(this);
// mc++ 		findViewById(R.id.main_ioexample_textview).setOnClickListener(this);
// mc++ 		findViewById(R.id.main_space_textview).setOnClickListener(this);
// mc++ 		findViewById(R.id.main_swipebehavior_textview).setOnClickListener(this);
// mc++ 	}
// mc++ 
// mc++ 	@Override
// mc++ 	public void onClick(View v) {
// mc++ 		switch (v.getId()) {
// mc++ 			case R.id.main_coordinator_textview:
// mc++ 				SimpleCoordinatorActivity.start(this);
// mc++ 				break;
// mc++ 
// mc++ 			case R.id.main_ioexample_textview:
// mc++ 				IOActivityExample.start(this);
// mc++ 				break;
// mc++ 
// mc++ 			case R.id.main_space_textview:
// mc++ 				FlexibleSpaceExampleActivity.start(this);
// mc++ 				break;
// mc++ 
// mc++ 			case R.id.main_materialup_textview:
// mc++ 				MaterialUpConceptActivity.start(this);
// mc++ 				break;
// mc++ 
// mc++ 			case R.id.main_swipebehavior_textview:
// mc++ 				SwipeBehaviorExampleActivity.start(this);
// mc++ 				break;
// mc++ 		}
// mc++ 	}
// mc++ }
