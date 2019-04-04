// mc++ package com.xujun.contralayout.UI.toolBar;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.LinearLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.adapter.ItemAdapter;
// mc++ import com.xujun.contralayout.recyclerView.divider.DividerItemDecoration;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ import java.util.List;
// mc++ 
// mc++ public class ToolBarSample extends AppCompatActivity {
// mc++ 
// mc++     RecyclerView mRecyclerView;
// mc++     List<String> mDatas;
// mc++     private ItemAdapter mAdapter;
// mc++ 
// mc++     Toolbar mToolbar;
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_second);
// mc++         mRecyclerView = (RecyclerView) findViewById(R.id.recyclerView);
// mc++         /**
// mc++          * 设置 toolBar
// mc++          */
// mc++         mToolbar = (Toolbar) findViewById(R.id.toolbar);
// mc++         // 该属性必须在setSupportActionBar之前 调用
// mc++         mToolbar.setTitle("toolBar");
// mc++         setSupportActionBar(mToolbar);
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
// mc++ }
