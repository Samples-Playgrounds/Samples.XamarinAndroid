// mc++ package com.xujun.contralayout.UI;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.v4.widget.SwipeRefreshLayout;
// mc++ import android.support.v7.widget.LinearLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.View;
// mc++ import android.widget.Toast;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.adapter.ItemAdapter;
// mc++ import com.xujun.contralayout.base.BaseFragment;
// mc++ import com.xujun.contralayout.recyclerView.divider.DividerItemDecoration;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ import java.util.List;
// mc++ 
// mc++ /**
// mc++  * @ explain:
// mc++  * @ author：xujun on 2016/10/18 17:21
// mc++  * @ email：gdutxiaoxu@163.com
// mc++  */
// mc++ public class ListFragment extends BaseFragment {
// mc++ 
// mc++     RecyclerView mRecyclerView;
// mc++     private static final String KEY = "key";
// mc++     private String title = "测试";
// mc++ 
// mc++     List<String> mDatas = new ArrayList<>();
// mc++     private ItemAdapter mAdapter;
// mc++     private SwipeRefreshLayout mSwipeRefreshLayout;
// mc++ 
// mc++     public static ListFragment newInstance(String title) {
// mc++         ListFragment fragment = new ListFragment();
// mc++         Bundle bundle = new Bundle();
// mc++         bundle.putString(KEY, title);
// mc++         fragment.setArguments(bundle);
// mc++         return fragment;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void initView(View view) {
// mc++         Bundle arguments = getArguments();
// mc++         if (arguments != null) {
// mc++             title = arguments.getString(KEY);
// mc++         }
// mc++         mRecyclerView = (RecyclerView) view.findViewById(R.id.recyclerView);
// mc++         mSwipeRefreshLayout = (SwipeRefreshLayout) view.findViewById(R.id.swipeRefreshLayout);
// mc++         mSwipeRefreshLayout.setEnabled(false);
// mc++ 
// mc++         LinearLayoutManager layoutManager = new LinearLayoutManager(mContext);
// mc++         DividerItemDecoration itemDecoration = new DividerItemDecoration(mContext,
// mc++                 LinearLayoutManager.VERTICAL);
// mc++         mRecyclerView.addItemDecoration(itemDecoration);
// mc++         mRecyclerView.setLayoutManager(layoutManager);
// mc++ 
// mc++         for (int i = 0; i < 50; i++) {
// mc++             String s = String.format("我是第%d个" + title, i);
// mc++             mDatas.add(s);
// mc++         }
// mc++ 
// mc++         mAdapter = new ItemAdapter(mContext, mDatas);
// mc++         mRecyclerView.setAdapter(mAdapter);
// mc++         mSwipeRefreshLayout.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
// mc++             @Override
// mc++             public void onRefresh() {
// mc++                 mSwipeRefreshLayout.postDelayed(new Runnable() {
// mc++                     @Override
// mc++                     public void run() {
// mc++                         mSwipeRefreshLayout.setRefreshing(false);
// mc++                         Toast.makeText(mContext, "刷新完成", Toast.LENGTH_SHORT).show();
// mc++                     }
// mc++                 }, 1200);
// mc++             }
// mc++         });
// mc++ 
// mc++ 
// mc++     }
// mc++ 
// mc++     public void tooglePager(boolean isOpen) {
// mc++         if (isOpen) {
// mc++             setRefreshEnable(false);
// mc++             scrollToFirst(false);
// mc++         } else {
// mc++             setRefreshEnable(true);
// mc++         }
// mc++     }
// mc++ 
// mc++     public void scrollToFirst(boolean isSmooth) {
// mc++         if (mRecyclerView == null) {
// mc++             return;
// mc++         }
// mc++         if (isSmooth) {
// mc++             mRecyclerView.smoothScrollToPosition(0);
// mc++         } else {
// mc++             mRecyclerView.scrollToPosition(0);
// mc++         }
// mc++     }
// mc++ 
// mc++     public void setRefreshEnable(boolean enabled) {
// mc++         if (mSwipeRefreshLayout != null) {
// mc++             mSwipeRefreshLayout.setEnabled(enabled);
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected int getLayoutId() {
// mc++         return R.layout.fragment_list;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void fetchData() {
// mc++ 
// mc++     }
// mc++ }
