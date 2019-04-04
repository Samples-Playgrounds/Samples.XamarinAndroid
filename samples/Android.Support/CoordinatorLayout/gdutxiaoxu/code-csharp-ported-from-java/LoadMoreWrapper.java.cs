// mc++ package com.xujun.contralayout.recyclerView;
// mc++ 
// mc++ import android.support.v7.widget.GridLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.support.v7.widget.StaggeredGridLayoutManager;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ /**
// mc++  * @ explain:
// mc++  * @ author：xujun on 2016-8-11 09:32
// mc++  * @ email：gdutxiaoxu@163.com
// mc++  */
// mc++ public class LoadMoreWrapper<T> extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
// mc++     public static final int ITEM_TYPE_LOAD_MORE = Integer.MAX_VALUE - 2;
// mc++ 
// mc++     private RecyclerView.Adapter mInnerAdapter;
// mc++     private View mLoadMoreView;
// mc++     private int mLoadMoreLayoutId;
// mc++ 
// mc++     private boolean hasLoadMore = true;
// mc++     private boolean enableLoadMore = true;
// mc++ 
// mc++     public LoadMoreWrapper(RecyclerView.Adapter adapter) {
// mc++         mInnerAdapter = adapter;
// mc++     }
// mc++ 
// mc++     private boolean hasLoadMore() {
// mc++         return enableLoadMore&&hasLoadMore && (mLoadMoreView != null || mLoadMoreLayoutId != 0);
// mc++     }
// mc++ 
// mc++     private boolean isShowLoadMore(int position) {
// mc++         return hasLoadMore() && (position >= mInnerAdapter.getItemCount());
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getItemViewType(int position) {
// mc++         if (isShowLoadMore(position)) {
// mc++             return ITEM_TYPE_LOAD_MORE;
// mc++         }
// mc++         return mInnerAdapter.getItemViewType(position);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
// mc++         if (viewType == ITEM_TYPE_LOAD_MORE) {
// mc++             BaseRecyclerHolder holder;
// mc++             if (mLoadMoreView != null) {
// mc++                 holder = BaseRecyclerHolder.createViewHolder(parent.getContext(), mLoadMoreView);
// mc++             } else {
// mc++                 holder = BaseRecyclerHolder.createViewHolder(parent.getContext(), parent,
// mc++                         mLoadMoreLayoutId);
// mc++             }
// mc++             return holder;
// mc++         }
// mc++         return mInnerAdapter.onCreateViewHolder(parent, viewType);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onBindViewHolder(RecyclerView.ViewHolder holder, int position) {
// mc++         if (isShowLoadMore(position)) {
// mc++             if (mOnLoadMoreListener != null) {
// mc++                 mOnLoadMoreListener.onLoadMoreRequested();
// mc++             }
// mc++             return;
// mc++         }
// mc++         mInnerAdapter.onBindViewHolder(holder, position);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onAttachedToRecyclerView(RecyclerView recyclerView) {
// mc++         WrapperUtils.onAttachedToRecyclerView(mInnerAdapter, recyclerView, new WrapperUtils
// mc++                 .SpanSizeCallback() {
// mc++             @Override
// mc++             public int getSpanSize(GridLayoutManager layoutManager, GridLayoutManager
// mc++                     .SpanSizeLookup oldLookup, int position) {
// mc++                 if (isShowLoadMore(position)) {
// mc++                     return layoutManager.getSpanCount();
// mc++                 }
// mc++                 if (oldLookup != null) {
// mc++                     return oldLookup.getSpanSize(position);
// mc++                 }
// mc++                 return 1;
// mc++             }
// mc++         });
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onViewAttachedToWindow(RecyclerView.ViewHolder holder) {
// mc++         mInnerAdapter.onViewAttachedToWindow(holder);
// mc++ 
// mc++         if (isShowLoadMore(holder.getLayoutPosition())) {
// mc++             setFullSpan(holder);
// mc++         }
// mc++     }
// mc++ 
// mc++     private void setFullSpan(RecyclerView.ViewHolder holder) {
// mc++         ViewGroup.LayoutParams lp = holder.itemView.getLayoutParams();
// mc++ 
// mc++         if (lp != null
// mc++                 && lp instanceof StaggeredGridLayoutManager.LayoutParams) {
// mc++             StaggeredGridLayoutManager.LayoutParams p = (StaggeredGridLayoutManager.LayoutParams)
// mc++                     lp;
// mc++ 
// mc++             p.setFullSpan(true);
// mc++         }
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getItemCount() {
// mc++         return mInnerAdapter.getItemCount() + (hasLoadMore() ? 1 : 0);
// mc++     }
// mc++ 
// mc++     public int getRealItemCount(){
// mc++         return mInnerAdapter.getItemCount();
// mc++     }
// mc++ 
// mc++     public interface OnLoadMoreListener {
// mc++         void onLoadMoreRequested();
// mc++     }
// mc++ 
// mc++     private OnLoadMoreListener mOnLoadMoreListener;
// mc++ 
// mc++     public LoadMoreWrapper setOnLoadMoreListener(OnLoadMoreListener loadMoreListener) {
// mc++         if (loadMoreListener != null) {
// mc++             mOnLoadMoreListener = loadMoreListener;
// mc++         }
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public LoadMoreWrapper setLoadMoreView(View loadMoreView) {
// mc++         mLoadMoreView = loadMoreView;
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public LoadMoreWrapper setLoadMoreView(int layoutId) {
// mc++         mLoadMoreLayoutId = layoutId;
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public void showLoadMore(boolean hasLoadMore){
// mc++         this.hasLoadMore=hasLoadMore;
// mc++         if(hasLoadMore){
// mc++             notifyItemInserted(getRealItemCount());
// mc++         }else{
// mc++             notifyItemRemoved(getRealItemCount());
// mc++         }
// mc++     }
// mc++ 
// mc++     public void setEnableLoadMore(boolean enableLoadMore){
// mc++         this.enableLoadMore=enableLoadMore;
// mc++     }
// mc++ 
// mc++     public RecyclerView.Adapter getInnerAdapter(){
// mc++         return mInnerAdapter;
// mc++     }
// mc++ 
// mc++ 
// mc++ }
