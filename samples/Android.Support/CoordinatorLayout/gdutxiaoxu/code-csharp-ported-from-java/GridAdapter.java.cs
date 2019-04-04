// mc++ package com.xujun.contralayout.UI.bottomsheet;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ import android.widget.ImageButton;
// mc++ 
// mc++ import com.squareup.picasso.Picasso;
// mc++ import com.xujun.contralayout.R;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ /**
// mc++  * 博客地址：http://blog.csdn.net/gdutxiaoxu
// mc++  * @author  xujun  on 2017/5/1 10:50
// mc++  * @version 0.1
// mc++  */
// mc++ 
// mc++ 
// mc++ public  class GridAdapter extends RecyclerView.Adapter<GridAdapter.MyViewHolder> implements View.OnClickListener, View.OnLongClickListener {
// mc++ 
// mc++     private Context mContext;
// mc++     private List<Meizi> datas;
// mc++ 
// mc++     public  interface OnRecyclerViewItemClickListener {
// mc++         void onItemClick(View view);
// mc++         void onItemLongClick(View view);
// mc++     }
// mc++ 
// mc++     private OnRecyclerViewItemClickListener mOnItemClickListener = null;
// mc++ 
// mc++     public void setOnItemClickListener(OnRecyclerViewItemClickListener listener) {
// mc++         mOnItemClickListener = listener;
// mc++     }
// mc++ 
// mc++ 
// mc++     public GridAdapter(Context context, List<Meizi> datas) {
// mc++         mContext=context;
// mc++         this.datas=datas;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public GridAdapter.MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType)
// mc++     {
// mc++             View view = LayoutInflater.from(mContext
// mc++                     ).inflate(R.layout.grid_meizi_item, parent,
// mc++                     false);
// mc++             MyViewHolder holder = new MyViewHolder(view);
// mc++ 
// mc++             view.setOnClickListener(this);
// mc++             view.setOnLongClickListener(this);
// mc++ 
// mc++             return holder;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onBindViewHolder(GridAdapter.MyViewHolder holder, int position) {
// mc++             Picasso.with(mContext).load(datas.get(position).getUrl()).into(holder.iv);
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getItemCount()
// mc++     {
// mc++         return datas.size();
// mc++     }
// mc++ 
// mc++ 
// mc++     @Override
// mc++     public void onClick(View v) {
// mc++         if (mOnItemClickListener != null) {
// mc++ 
// mc++             mOnItemClickListener.onItemClick(v);
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++ 
// mc++     @Override
// mc++     public boolean onLongClick(View v) {
// mc++         if (mOnItemClickListener != null) {
// mc++             mOnItemClickListener.onItemLongClick(v);
// mc++         }
// mc++         return false;
// mc++     }
// mc++ 
// mc++     class MyViewHolder extends RecyclerView.ViewHolder
// mc++     {
// mc++         private ImageButton iv;
// mc++ 
// mc++         public MyViewHolder(View view)
// mc++         {
// mc++             super(view);
// mc++             iv = (ImageButton) view.findViewById(R.id.iv);
// mc++         }
// mc++     }
// mc++ 
// mc++ }
