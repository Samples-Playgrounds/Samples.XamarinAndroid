// mc++ package com.xujun.contralayout.recyclerView;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ import java.util.List;
// mc++ 
// mc++ /**
// mc++  * @ explain:
// mc++  * @ author：xujun on 2016/5/13 15:45
// mc++  * @ email：gdutxiaoxu@163.com
// mc++  */
// mc++ public abstract class BaseRecyclerAdapter<T> extends DefaultAdapter<T> {
// mc++ 
// mc++     protected Context mContext;
// mc++     protected final int mItemLayoutId;
// mc++ 
// mc++     public BaseRecyclerAdapter(Context context, int itemLayoutId) {
// mc++         mContext = context;
// mc++         mItemLayoutId = itemLayoutId;
// mc++         mDatas = new ArrayList<>();
// mc++     }
// mc++ 
// mc++     public BaseRecyclerAdapter(Context context, int itemLayoutId, List<T> datas) {
// mc++         mContext = context;
// mc++         mItemLayoutId = itemLayoutId;
// mc++         mDatas = datas;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
// mc++ 
// mc++ 
// mc++         BaseRecyclerHolder holder = new BaseRecyclerHolder(LayoutInflater.from
// mc++                 (mContext).
// mc++                 inflate(mItemLayoutId, parent, false));
// mc++         setListener(parent, holder, viewType);
// mc++         return holder;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onBindViewHolder(final RecyclerView.ViewHolder holder, int position) {
// mc++         BaseRecyclerHolder baseHolder = (BaseRecyclerHolder) holder;
// mc++ 
// mc++         convert(baseHolder, (T) mDatas.get(position), position);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * @param holder   自定义的ViewHolder对象，可以getView获取控件
// mc++      * @param item     对应的数据
// mc++      * @param position
// mc++      */
// mc++     public abstract void convert(BaseRecyclerHolder holder, T item, int position);
// mc++ 
// mc++     @Override
// mc++     public int getItemCount() {
// mc++         return isEmpty() ? 0 : mDatas.size();
// mc++     }
// mc++ 
// mc++     protected boolean isEnabled(int viewType) {
// mc++         return true;
// mc++     }
// mc++ 
// mc++     public void setClickListener(BaseRecyclerHolder holder, int id, View.OnClickListener
// mc++             onClickListener) {
// mc++         holder.getView(id).setOnClickListener(onClickListener);
// mc++     }
// mc++ 
// mc++     protected void setListener(final ViewGroup parent, final BaseRecyclerHolder viewHolder, int
// mc++             viewType) {
// mc++         if (!isEnabled(viewType)) return;
// mc++         viewHolder.getConvertView().setOnClickListener(new View.OnClickListener() {
// mc++             @Override
// mc++             public void onClick(View v) {
// mc++                 if (mOnItemClickListener != null) {
// mc++                     //这个方法是获取在holder里面真正的位置，而不是对应list的位置
// mc++                     int position = viewHolder.getAdapterPosition()-1;
// mc++                     T t = mDatas.get(position);
// mc++                     mOnItemClickListener.onClick(v, viewHolder, t, position);
// mc++                 }
// mc++             }
// mc++         });
// mc++ 
// mc++         viewHolder.getConvertView().setOnLongClickListener(new View.OnLongClickListener() {
// mc++             @Override
// mc++             public boolean onLongClick(View v) {
// mc++                 if (mOnLongItemClickListener != null) {
// mc++                     int position = viewHolder.getAdapterPosition();
// mc++                     return mOnLongItemClickListener.onItemLongClick(v, viewHolder, mDatas.get
// mc++                             (position), position);
// mc++                 }
// mc++                 return false;
// mc++             }
// mc++         });
// mc++ 
// mc++     }
// mc++ 
// mc++ 
// mc++ }
// mc++ 
