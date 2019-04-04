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
// mc++ import android.support.design.widget.AppBarLayout;
// mc++ import android.support.v4.view.ViewCompat;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.View;
// mc++ 
// mc++ public class FlexibleSpaceExampleActivity extends AppCompatActivity
// mc++ 	implements AppBarLayout.OnOffsetChangedListener {
// mc++ 	private static final int PERCENTAGE_TO_SHOW_IMAGE = 20;
// mc++ 	private View mFab;
// mc++ 	private int mMaxScrollSize;
// mc++ 	private boolean mIsImageHidden;
// mc++ 
// mc++ 	@Override
// mc++ 	protected void onCreate(Bundle savedInstanceState) {
// mc++ 		super.onCreate(savedInstanceState);
// mc++ 		setContentView(R.layout.activity_flexible_space);
// mc++ 
// mc++ 		mFab = findViewById(R.id.flexible_example_fab);
// mc++ 
// mc++ 		Toolbar toolbar = (Toolbar) findViewById(R.id.flexible_example_toolbar);
// mc++ 		toolbar.setNavigationOnClickListener(new View.OnClickListener() {
// mc++ 			@Override public void onClick(View v) {
// mc++ 				onBackPressed();
// mc++ 			}
// mc++ 		});
// mc++ 
// mc++ 		AppBarLayout appbar = (AppBarLayout) findViewById(R.id.flexible_example_appbar);
// mc++ 		appbar.addOnOffsetChangedListener(this);
// mc++ 	}
// mc++ 
// mc++ 	@Override
// mc++ 	public void onOffsetChanged(AppBarLayout appBarLayout, int i) {
// mc++ 		if (mMaxScrollSize == 0)
// mc++ 			mMaxScrollSize = appBarLayout.getTotalScrollRange();
// mc++ 
// mc++ 		int currentScrollPercentage = (Math.abs(i)) * 100
// mc++ 			/ mMaxScrollSize;
// mc++ 
// mc++ 		if (currentScrollPercentage >= PERCENTAGE_TO_SHOW_IMAGE) {
// mc++ 			if (!mIsImageHidden) {
// mc++ 				mIsImageHidden = true;
// mc++ 
// mc++ 				ViewCompat.animate(mFab).scaleY(0).scaleX(0).start();
// mc++ 			}
// mc++ 		}
// mc++ 
// mc++ 		if (currentScrollPercentage < PERCENTAGE_TO_SHOW_IMAGE) {
// mc++ 			if (mIsImageHidden) {
// mc++ 				mIsImageHidden = false;
// mc++ 				ViewCompat.animate(mFab).scaleY(1).scaleX(1).start();
// mc++ 			}
// mc++ 		}
// mc++ 	}
// mc++ 
// mc++ 	public static void start(Context c) {
// mc++ 		c.startActivity(new Intent(c, FlexibleSpaceExampleActivity.class));
// mc++ 	}
// mc++ }
