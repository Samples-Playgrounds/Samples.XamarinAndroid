// mc++ package com.loonggg.coordinatorlayoutdemo;
// mc++ 
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ import android.widget.TextView;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ /**
// mc++  * Created by loongggdroid on 2016/5/19.
// mc++  */
// mc++ public class MyAdapter extends RecyclerView.Adapter<MyAdapter.ViewHolder> {
// mc++     // 数据集
// mc++     private List<String> mDataset;
// mc++ 
// mc++     public MyAdapter(List<String> dataset) {
// mc++         super();
// mc++         mDataset = dataset;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public ViewHolder onCreateViewHolder(ViewGroup viewGroup, int i) {
// mc++ 
// mc++         // 创建一个View，简单起见直接使用系统提供的布局，就是一个TextView
// mc++ 
// mc++         View view = View.inflate(viewGroup.getContext(), android.R.layout.simple_list_item_1, null);
// mc++ 
// mc++         // 创建一个ViewHolder
// mc++ 
// mc++         ViewHolder holder = new ViewHolder(view);
// mc++ 
// mc++         return holder;
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onBindViewHolder(ViewHolder viewHolder, int i) {
// mc++ 
// mc++         // 绑定数据到ViewHolder上
// mc++ 
// mc++         viewHolder.mTextView.setText(mDataset.get(i));
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getItemCount() {
// mc++ 
// mc++         return mDataset.size();
// mc++ 
// mc++     }
// mc++ 
// mc++     public static class ViewHolder extends RecyclerView.ViewHolder {
// mc++ 
// mc++         public TextView mTextView;
// mc++ 
// mc++         public ViewHolder(View itemView) {
// mc++ 
// mc++             super(itemView);
// mc++ 
// mc++             mTextView = (TextView) itemView;
// mc++ 
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++ }
// mc++ 
// mc++ 
// mc++ 
