// mc++ package com.xujun.contralayout.UI.toolBar;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.annotation.NonNull;
// mc++ import android.support.design.widget.BottomSheetBehavior;
// mc++ import android.support.design.widget.Snackbar;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.LinearLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.util.Log;
// mc++ import android.view.View;
// mc++ import android.widget.RelativeLayout;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.adapter.ItemAdapter;
// mc++ import com.xujun.contralayout.recyclerView.divider.DividerItemDecoration;
// mc++ import com.xujun.contralayout.utils.WriteLogUtil;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ import java.util.List;
// mc++ 
// mc++ public class ToolBarSampleSnar extends AppCompatActivity {
// mc++ 
// mc++     RecyclerView mRecyclerView;
// mc++     List<String> mDatas;
// mc++     private ItemAdapter mAdapter;
// mc++ 
// mc++     Toolbar mToolbar;
// mc++ 
// mc++     public static  final String TAG="xujun";
// mc++ 
// mc++     private RelativeLayout mRlBottomSheet;
// mc++     private BottomSheetBehavior<RelativeLayout> mFrom;
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_first);
// mc++         mRecyclerView = (RecyclerView) findViewById(R.id.recyclerView);
// mc++         mRlBottomSheet = (RelativeLayout) findViewById(R.id.rl_bottom_sheet);
// mc++         mFrom = BottomSheetBehavior.from(mRlBottomSheet);
// mc++ 
// mc++         mToolbar = (Toolbar) findViewById(R.id.toolbar);
// mc++         // 该属性必须在setSupportActionBar之前 调用
// mc++         mToolbar.setTitle("toolBar");
// mc++         setSupportActionBar(mToolbar);
// mc++ 
// mc++         initListener();
// mc++         initData();
// mc++     }
// mc++ 
// mc++     private void initData() {
// mc++         WriteLogUtil.init(this);
// mc++ 
// mc++ 
// mc++ 
// mc++ 
// mc++         LinearLayoutManager layoutManager = new LinearLayoutManager(this);
// mc++         mRecyclerView.setLayoutManager(layoutManager);
// mc++         DividerItemDecoration itemDecoration = new DividerItemDecoration(this,
// mc++                 LinearLayoutManager.VERTICAL);
// mc++         mRecyclerView.addItemDecoration(itemDecoration);
// mc++ 
// mc++         mDatas = new ArrayList<>();
// mc++         for (int i = 0; i < 50; i++) {
// mc++             String s = String.format("我是第%d个item", i);
// mc++             mDatas.add(s);
// mc++         }
// mc++         mAdapter = new ItemAdapter(this, mDatas);
// mc++         mRecyclerView.setAdapter(mAdapter);
// mc++     }
// mc++ 
// mc++     private void initListener() {
// mc++ 
// mc++         mFrom.setBottomSheetCallback(new BottomSheetBehavior.BottomSheetCallback() {
// mc++             @Override
// mc++             public void onStateChanged(@NonNull View bottomSheet, int newState) {
// mc++                 Log.i(TAG, "onStateChanged: newState=" +newState);
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onSlide(@NonNull View bottomSheet, float slideOffset) {
// mc++                 Log.i(TAG, "onStateChanged: slideOffset=" +slideOffset);
// mc++             }
// mc++         });
// mc++         findViewById(R.id.fab).setOnClickListener(new View.OnClickListener() {
// mc++             @Override
// mc++             public void onClick(View view) {
// mc++ 
// mc++                 Snackbar.make(view,"FAB",Snackbar.LENGTH_LONG)
// mc++                         .setAction("cancel", new View.OnClickListener() {
// mc++                             @Override
// mc++                             public void onClick(View v) {
// mc++                                 //这里的单击事件代表点击消除Action后的响应事件
// mc++                                 WriteLogUtil.i("Snackbar");
// mc++ 
// mc++                             }
// mc++                         })
// mc++                         .show();
// mc++             }
// mc++         });
// mc++        mRecyclerView.setOnScrollListener(new RecyclerView.OnScrollListener() {
// mc++            @Override
// mc++            public void onScrollStateChanged(RecyclerView recyclerView, int newState) {
// mc++                super.onScrollStateChanged(recyclerView, newState);
// mc++            }
// mc++ 
// mc++            @Override
// mc++            public void onScrolled(RecyclerView recyclerView, int dx, int dy) {
// mc++                super.onScrolled(recyclerView, dx, dy);
// mc++                Log.i(TAG, "onScrolled: dy=" +dy);
// mc++            }
// mc++        });
// mc++     }
// mc++ 
// mc++ 
// mc++ 
// mc++ }
