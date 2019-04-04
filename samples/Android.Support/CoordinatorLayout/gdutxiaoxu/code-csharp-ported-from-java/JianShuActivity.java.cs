// mc++ package com.xujun.contralayout.UI;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.LinearLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.adapter.ItemAdapter;
// mc++ import com.xujun.contralayout.recyclerView.divider.DividerItemDecoration;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ 
// mc++ public class JianShuActivity extends AppCompatActivity {
// mc++ 
// mc++     private RecyclerView mRecyclerView;
// mc++     private ArrayList<String> mDatas;
// mc++     private ItemAdapter mItemAdapter;
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_jian_shu);
// mc++         mRecyclerView=(RecyclerView) findViewById(R.id.recyclerView);
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
// mc++         mItemAdapter = new ItemAdapter(this, mDatas);
// mc++         mRecyclerView.setAdapter(mItemAdapter);
// mc++     }
// mc++ }
