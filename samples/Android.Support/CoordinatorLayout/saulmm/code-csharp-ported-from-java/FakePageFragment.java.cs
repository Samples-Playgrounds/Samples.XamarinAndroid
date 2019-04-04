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
// mc++ import android.os.Bundle;
// mc++ import android.support.annotation.Nullable;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ public class FakePageFragment extends Fragment {
// mc++ 	private RecyclerView mRootView;
// mc++ 
// mc++ 	@Override
// mc++ 	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
// mc++ 		mRootView = (RecyclerView) inflater.inflate(R.layout.fragment_page, container, false);
// mc++ 		return mRootView;
// mc++ 	}
// mc++ 
// mc++ 	@Override
// mc++ 	public void onActivityCreated(@Nullable Bundle savedInstanceState) {
// mc++ 		super.onActivityCreated(savedInstanceState);
// mc++ 		initRecyclerView();
// mc++ 	}
// mc++ 
// mc++ 	private void initRecyclerView() {
// mc++ 		mRootView.setAdapter(new FakePageAdapter(20));
// mc++ 	}
// mc++ 
// mc++ 	public static Fragment newInstance() {
// mc++ 		return new FakePageFragment();
// mc++ 	}
// mc++ 
// mc++ 
// mc++ 	private static class FakePageAdapter extends RecyclerView.Adapter<FakePageVH> {
// mc++ 		private final int numItems;
// mc++ 
// mc++ 		FakePageAdapter(int numItems) {
// mc++ 			this.numItems = numItems;
// mc++ 		}
// mc++ 
// mc++ 		@Override
// mc++ 		public FakePageVH onCreateViewHolder(ViewGroup viewGroup, int i) {
// mc++ 			View itemView = LayoutInflater.from(viewGroup.getContext())
// mc++ 				.inflate(R.layout.list_item_card, viewGroup, false);
// mc++ 
// mc++ 			return new FakePageVH(itemView);
// mc++ 		}
// mc++ 
// mc++ 		@Override
// mc++ 		public void onBindViewHolder(FakePageVH fakePageVH, int i) {
// mc++ 			// do nothing
// mc++ 		}
// mc++ 
// mc++ 		@Override
// mc++ 		public int getItemCount() {
// mc++ 			return numItems;
// mc++ 		}
// mc++ 	}
// mc++ 
// mc++ 	private static class FakePageVH extends RecyclerView.ViewHolder {
// mc++ 		FakePageVH(View itemView) {
// mc++ 			super(itemView);
// mc++ 		}
// mc++ 	}
// mc++ }
