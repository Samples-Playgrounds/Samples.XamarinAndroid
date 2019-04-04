// mc++ package com.xujun.contralayout.UI;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.widget.LinearLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.View;
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
// mc++  * @ author：xujun on 2016/10/15 11:09
// mc++  * @ email：gdutxiaoxu@163.com
// mc++  */
// mc++ public class ItemFragement extends BaseFragment {
// mc++ 
// mc++ 
// mc++     RecyclerView mRecyclerView;
// mc++     private static final String KEY="key";
// mc++     private String title="测试";
// mc++ 
// mc++     List<String> mDatas=new ArrayList<>();
// mc++     private ItemAdapter mAdapter;
// mc++ 
// mc++     public static ItemFragement newInstance(String text){
// mc++         ItemFragement itemFragement = new ItemFragement();
// mc++         Bundle bundle = new Bundle();
// mc++         if(text!=null){
// mc++             bundle.putString(KEY,text);
// mc++             itemFragement.setArguments(bundle);
// mc++         }
// mc++         return itemFragement;
// mc++     }
// mc++     private static final String TAG = "xujun";
// mc++ 
// mc++     @Override
// mc++     protected void initView(View view) {
// mc++         Bundle arguments = getArguments();
// mc++         if(arguments!=null){
// mc++             title=arguments.getString(KEY);
// mc++         }
// mc++         mRecyclerView= (RecyclerView) view.findViewById(R.id.recyclerView);
// mc++ 
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void initData() {
// mc++         LinearLayoutManager layoutManager = new LinearLayoutManager(mContext);
// mc++         DividerItemDecoration itemDecoration = new DividerItemDecoration(mContext,
// mc++                 LinearLayoutManager.VERTICAL);
// mc++         mRecyclerView.addItemDecoration(itemDecoration);
// mc++         mRecyclerView.setLayoutManager(layoutManager);
// mc++ 
// mc++         for(int i=0;i<30;i++){
// mc++             String s = String.format("I am %d " + title, i);
// mc++             mDatas.add(s);
// mc++         }
// mc++ 
// mc++         mAdapter = new ItemAdapter(mContext, mDatas);
// mc++         mRecyclerView.setAdapter(mAdapter);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected int getLayoutId() {
// mc++         return R.layout.fragment_item;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void fetchData() {
// mc++ 
// mc++     }
// mc++ }
