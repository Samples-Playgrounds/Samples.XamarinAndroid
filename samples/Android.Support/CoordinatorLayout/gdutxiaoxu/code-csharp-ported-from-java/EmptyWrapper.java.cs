// mc++ package com.xujun.contralayout.recyclerView;
// mc++ 
// mc++ import android.support.v7.widget.GridLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ /**
// mc++  * Created by xujun on 16/6/23.
// mc++  */
// mc++ public class EmptyWrapper<T> extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
// mc++     public static final int ITEM_TYPE_EMPTY = Integer.MAX_VALUE - 1;
// mc++ 
// mc++     private RecyclerView.Adapter mInnerAdapter;
// mc++     private View mEmptyView;
// mc++     private int mEmptyLayoutId;
// mc++ 
// mc++     public EmptyWrapper(RecyclerView.Adapter adapter) {
// mc++         mInnerAdapter = adapter;
// mc++     }
// mc++ 
// mc++     private boolean isEmpty() {
// mc++         return (mEmptyView != null || mEmptyLayoutId != 0) && mInnerAdapter.getItemCount() == 0;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
// mc++         if (isEmpty()) {
// mc++             RecyclerView.ViewHolder holder;
// mc++             if (mEmptyView != null) {
// mc++                 holder = BaseRecyclerHolder.createViewHolder(mEmptyView);
// mc++             } else {
// mc++                 holder = BaseRecyclerHolder.createViewHolder(parent.getContext(), parent,
// mc++                         mEmptyLayoutId);
// mc++             }
// mc++             return holder;
// mc++         }
// mc++         return mInnerAdapter.onCreateViewHolder(parent, viewType);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onAttachedToRecyclerView(RecyclerView recyclerView) {
// mc++         WrapperUtils.onAttachedToRecyclerView(mInnerAdapter, recyclerView, new WrapperUtils
// mc++                 .SpanSizeCallback() {
// mc++             @Override
// mc++             public int getSpanSize(GridLayoutManager gridLayoutManager, GridLayoutManager
// mc++                     .SpanSizeLookup oldLookup, int position) {
// mc++                 if (isEmpty()) {
// mc++                     return gridLayoutManager.getSpanCount();
// mc++                 }
// mc++                 if (oldLookup != null) {
// mc++                     return oldLookup.getSpanSize(position);
// mc++                 }
// mc++                 return 1;
// mc++             }
// mc++         });
// mc++ 
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onViewAttachedToWindow(RecyclerView.ViewHolder holder) {
// mc++         mInnerAdapter.onViewAttachedToWindow(holder);
// mc++         if (isEmpty()) {
// mc++             WrapperUtils.setFullSpan(holder);
// mc++         }
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getItemViewType(int position) {
// mc++         if (isEmpty()) {
// mc++             return ITEM_TYPE_EMPTY;
// mc++         }
// mc++         return mInnerAdapter.getItemViewType(position);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onBindViewHolder(RecyclerView.ViewHolder holder, int position) {
// mc++         if (isEmpty()) {
// mc++             return;
// mc++         }
// mc++         mInnerAdapter.onBindViewHolder(holder, position);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getItemCount() {
// mc++         if (isEmpty()) return 1;
// mc++         return mInnerAdapter.getItemCount();
// mc++     }
// mc++ 
// mc++     public void setEmptyView(View emptyView) {
// mc++         mEmptyView = emptyView;
// mc++     }
// mc++ 
// mc++     public void setEmptyView(int layoutId) {
// mc++         mEmptyLayoutId = layoutId;
// mc++     }
// mc++ 
// mc++ }
