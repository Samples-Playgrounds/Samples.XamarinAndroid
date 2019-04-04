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
// mc++ import android.support.design.widget.TabLayout;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v4.app.FragmentManager;
// mc++ import android.support.v4.app.FragmentPagerAdapter;
// mc++ import android.support.v4.view.ViewPager;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.view.View;
// mc++ import android.widget.ImageView;
// mc++ 
// mc++ public class MaterialUpConceptActivity extends AppCompatActivity
// mc++ 	implements AppBarLayout.OnOffsetChangedListener {
// mc++ 
// mc++ 	private static final int PERCENTAGE_TO_ANIMATE_AVATAR = 20;
// mc++ 	private boolean mIsAvatarShown = true;
// mc++ 
// mc++ 	private ImageView mProfileImage;
// mc++ 	private int mMaxScrollSize;
// mc++ 
// mc++ 	@Override
// mc++ 	public void onCreate(Bundle savedInstanceState) {
// mc++ 		super.onCreate(savedInstanceState);
// mc++ 		setContentView(R.layout.activity_material_up_concept);
// mc++ 
// mc++ 		TabLayout tabLayout = (TabLayout) findViewById(R.id.materialup_tabs);
// mc++ 		ViewPager viewPager  = (ViewPager) findViewById(R.id.materialup_viewpager);
// mc++ 		AppBarLayout appbarLayout = (AppBarLayout) findViewById(R.id.materialup_appbar);
// mc++ 		mProfileImage = (ImageView) findViewById(R.id.materialup_profile_image);
// mc++ 
// mc++ 		Toolbar toolbar = (Toolbar) findViewById(R.id.materialup_toolbar);
// mc++ 		toolbar.setNavigationOnClickListener(new View.OnClickListener() {
// mc++ 			@Override public void onClick(View v) {
// mc++ 				onBackPressed();
// mc++ 			}
// mc++ 		});
// mc++ 
// mc++ 		appbarLayout.addOnOffsetChangedListener(this);
// mc++ 		mMaxScrollSize = appbarLayout.getTotalScrollRange();
// mc++ 
// mc++ 		viewPager.setAdapter(new TabsAdapter(getSupportFragmentManager()));
// mc++ 		tabLayout.setupWithViewPager(viewPager);
// mc++ 	}
// mc++ 
// mc++ 	public static void start(Context c) {
// mc++ 		c.startActivity(new Intent(c, MaterialUpConceptActivity.class));
// mc++ 	}
// mc++ 
// mc++ 	@Override
// mc++ 	public void onOffsetChanged(AppBarLayout appBarLayout, int i) {
// mc++ 		if (mMaxScrollSize == 0)
// mc++ 			mMaxScrollSize = appBarLayout.getTotalScrollRange();
// mc++ 
// mc++ 		int percentage = (Math.abs(i)) * 100 / mMaxScrollSize;
// mc++ 
// mc++ 		if (percentage >= PERCENTAGE_TO_ANIMATE_AVATAR && mIsAvatarShown) {
// mc++ 			mIsAvatarShown = false;
// mc++ 
// mc++ 			mProfileImage.animate()
// mc++ 				.scaleY(0).scaleX(0)
// mc++ 				.setDuration(200)
// mc++ 				.start();
// mc++ 		}
// mc++ 
// mc++ 		if (percentage <= PERCENTAGE_TO_ANIMATE_AVATAR && !mIsAvatarShown) {
// mc++ 			mIsAvatarShown = true;
// mc++ 
// mc++ 			mProfileImage.animate()
// mc++ 				.scaleY(1).scaleX(1)
// mc++ 				.start();
// mc++ 		}
// mc++ 	}
// mc++ 
// mc++ 	private static class TabsAdapter extends FragmentPagerAdapter {
// mc++ 		private static final int TAB_COUNT = 2;
// mc++ 
// mc++ 		TabsAdapter(FragmentManager fm) {
// mc++ 			super(fm);
// mc++ 		}
// mc++ 
// mc++ 		@Override
// mc++ 		public int getCount() {
// mc++ 			return TAB_COUNT;
// mc++ 		}
// mc++ 
// mc++ 		@Override
// mc++ 		public Fragment getItem(int i) {
// mc++ 			return FakePageFragment.newInstance();
// mc++ 		}
// mc++ 
// mc++ 		@Override
// mc++ 		public CharSequence getPageTitle(int position) {
// mc++ 			return "Tab " + String.valueOf(position);
// mc++ 		}
// mc++ 	}
// mc++ }
