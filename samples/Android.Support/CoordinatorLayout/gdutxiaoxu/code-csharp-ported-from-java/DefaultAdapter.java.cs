// mc++ package com.xujun.contralayout.recyclerView;
// mc++ 
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.View;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ /**
// mc++  * @ explain:
// mc++  * @ author：xujun on 2016-8-9 15:52
// mc++  * @ email：gdutxiaoxu@163.com
// mc++  */
// mc++ public abstract class DefaultAdapter<T> extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
// mc++ 
// mc++     protected List<T> mDatas;
// mc++     protected OnItemClickListener mOnItemClickListener;
// mc++     protected OnLongItemClickListener mOnLongItemClickListener;
// mc++ 
// mc++     public boolean isEmpty() {
// mc++         return mDatas == null || mDatas.size() == 0;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 设置列表中的数据
// mc++      */
// mc++     public void setDatas(List<T> datas) {
// mc++         if (datas == null) {
// mc++             return;
// mc++         }
// mc++         this.mDatas = datas;
// mc++         notifyDataSetChanged();
// mc++     }
// mc++ 
// mc++     public List<T> getDatas() {
// mc++         return mDatas;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 将单个数据添加到列表中
// mc++      */
// mc++     public void addSingleDate(T t) {
// mc++         if (t == null) {
// mc++             return;
// mc++         }
// mc++         this.mDatas.add(t);
// mc++         notifyItemInserted(mDatas.size() - 1);
// mc++     }
// mc++ 
// mc++     public void addDates(List<T> dates, int position) {
// mc++         if (dates == null || dates.size() == 0)
// mc++             return;
// mc++         mDatas.addAll(position, dates);
// mc++         notifyItemRangeInserted(position, dates.size());
// mc++     }
// mc++ 
// mc++     public void removeDatas(List<T> dates, int position) {
// mc++         if (dates == null || dates.size() == 0)
// mc++             return;
// mc++         mDatas.removeAll(dates);
// mc++         notifyItemRangeRemoved(position, dates.size());
// mc++     }
// mc++ 
// mc++     public void addSingleDate(T t, int position) {
// mc++         mDatas.add(position, t);
// mc++         notifyItemInserted(position);
// mc++         // notifyItemRangeChanged(position, mDatas.size());
// mc++     }
// mc++ 
// mc++     public void removeData(int position) {
// mc++         mDatas.remove(position);
// mc++         notifyItemRemoved(position);
// mc++         // notifyItemRangeChanged(position, mDatas.size());
// mc++     }
// mc++ 
// mc++     public void removeData(T t) {
// mc++         int index = mDatas.indexOf(t);
// mc++         notifyItemRemoved(index);
// mc++         // notifyItemRangeChanged(index, mDatas.size());
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 将一个List添加到列表中
// mc++      */
// mc++     public void addDates(List<T> dates,boolean isNotify) {
// mc++         if (dates == null || dates.size() == 0) {
// mc++             return;
// mc++         }
// mc++ //        int oldSize = this.mDatas.size();
// mc++ //        int newSize = dates.size();
// mc++ //        this.mDatas.addAll(dates);
// mc++ //        notifyItemRangeInserted(oldSize, newSize);
// mc++         this.mDatas.addAll(dates);
// mc++         if(true){
// mc++             notifyDataSetChanged();
// mc++         }
// mc++ 
// mc++ 
// mc++ 
// mc++     }
// mc++ 
// mc++ 
// mc++     public void addDates(List<T> dates) {
// mc++       this.addDates(dates,false);
// mc++ 
// mc++ 
// mc++     }
// mc++ 
// mc++     public void clearDates() {
// mc++         if (!isEmpty()) {
// mc++             this.mDatas.clear();
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     public interface OnItemClickListener<T> {
// mc++         void onClick(View view, RecyclerView.ViewHolder holder, T o, int position);
// mc++ 
// mc++     }
// mc++ 
// mc++     public interface OnLongItemClickListener<T> {
// mc++         boolean onItemLongClick(View view, RecyclerView.ViewHolder holder, T o, int position);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 设置点击事件
// mc++      *
// mc++      * @param onItemClickListener
// mc++      */
// mc++     public void setOnItemClickListener(OnItemClickListener onItemClickListener) {
// mc++         this.mOnItemClickListener = onItemClickListener;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 设置长按点击事件
// mc++      *
// mc++      * @param onLongItemClickListener
// mc++      */
// mc++     public void setonLongItemClickListener(OnLongItemClickListener onLongItemClickListener) {
// mc++         this.mOnLongItemClickListener = onLongItemClickListener;
// mc++     }
// mc++ }
