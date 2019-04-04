// mc++ package com.xujun.contralayout.recyclerView;
// mc++ 
// mc++ import android.support.v4.util.SparseArrayCompat;
// mc++ import android.support.v7.widget.GridLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.util.Log;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ /**
// mc++  * 博客地址：http://blog.csdn.net/gdutxiaoxu
// mc++  *
// mc++  * @author xujun
// mc++  * @time 2016/7/7 17:29.
// mc++  */
// mc++ public class HeaderAndFooterWrapper<T> extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
// mc++ 
// mc++     private static final int BASE_ITEM_TYPE_HEADER = 100000;
// mc++     private static final int BASE_ITEM_TYPE_FOOTER = 200000;
// mc++     private boolean mIsShowHeader = true;
// mc++     private boolean mIsShowFooter = true;
// mc++     public static final String TAG = "tag";
// mc++ 
// mc++     private SparseArrayCompat<View> mHeaderViews = new SparseArrayCompat<>();
// mc++     private SparseArrayCompat<View> mFootViews = new SparseArrayCompat<>();
// mc++ 
// mc++     private RecyclerView.Adapter mInnerAdapter;
// mc++ 
// mc++     public HeaderAndFooterWrapper(RecyclerView.Adapter adapter) {
// mc++         mInnerAdapter = adapter;
// mc++     }
// mc++ 
// mc++     /***
// mc++      * 返回
// mc++      *
// mc++      * @return
// mc++      */
// mc++     @Override
// mc++     public int getItemCount() {
// mc++         return getHeadersCount() + getRealItemCount() + getFootersCount();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getItemViewType(int position) {
// mc++         if (isHeaderViewPos(position)) {
// mc++             return mHeaderViews.keyAt(position);
// mc++         } else if (isFooterViewPos(position)) {
// mc++             return mFootViews.keyAt(position - getRealItemCount() - getHeadersCount());
// mc++         }
// mc++         return mInnerAdapter.getItemViewType(position - getHeadersCount());
// mc++     }
// mc++ 
// mc++ 
// mc++     @Override
// mc++     public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
// mc++         if (mHeaderViews.get(viewType) != null) {
// mc++             BaseRecyclerHolder holder = BaseRecyclerHolder.createViewHolder(mHeaderViews.get(viewType));
// mc++             return holder;
// mc++ 
// mc++         } else if (mFootViews.get(viewType) != null) {
// mc++             BaseRecyclerHolder holder = BaseRecyclerHolder.createViewHolder( mFootViews.get
// mc++                     (viewType));
// mc++             return holder;
// mc++         }
// mc++         return mInnerAdapter.onCreateViewHolder(parent, viewType);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onBindViewHolder(RecyclerView.ViewHolder holder, int position) {
// mc++         if (isHeaderViewPos(position)) {
// mc++             return;
// mc++         }
// mc++         if (isFooterViewPos(position)) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         mInnerAdapter.onBindViewHolder(holder, position - getHeadersCount());
// mc++ 
// mc++     }
// mc++ 
// mc++ 
// mc++ 
// mc++ 
// mc++     private int getRealItemCount() {
// mc++         return mInnerAdapter.getItemCount();
// mc++     }
// mc++ 
// mc++ 
// mc++     @Override
// mc++     public void onAttachedToRecyclerView(RecyclerView recyclerView) {
// mc++         WrapperUtils.onAttachedToRecyclerView(mInnerAdapter, recyclerView, new WrapperUtils
// mc++                 .SpanSizeCallback() {
// mc++             @Override
// mc++             public int getSpanSize(GridLayoutManager layoutManager, GridLayoutManager
// mc++                     .SpanSizeLookup oldLookup, int position) {
// mc++                 int viewType = getItemViewType(position);
// mc++                 if (mHeaderViews.get(viewType) != null) {
// mc++                     return layoutManager.getSpanCount();
// mc++                 } else if (mFootViews.get(viewType) != null) {
// mc++                     return layoutManager.getSpanCount();
// mc++                 }
// mc++                 if (oldLookup != null)
// mc++                     return oldLookup.getSpanSize(position);
// mc++                 return 1;
// mc++             }
// mc++         });
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onViewAttachedToWindow(RecyclerView.ViewHolder holder) {
// mc++         mInnerAdapter.onViewAttachedToWindow(holder);
// mc++         int position = holder.getLayoutPosition();
// mc++         if (isHeaderViewPos(position) || isFooterViewPos(position)) {
// mc++             WrapperUtils.setFullSpan(holder);
// mc++         }
// mc++     }
// mc++ 
// mc++     private boolean isHeaderViewPos(int position) {
// mc++         if (mIsShowFooter) {
// mc++             return position < getHeadersCount();
// mc++         } else {
// mc++             return false;
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     private boolean isFooterViewPos(int position) {
// mc++ 
// mc++         return position >= getHeadersCount() + getRealItemCount();
// mc++     }
// mc++ 
// mc++     public void addHeaderView(View view) {
// mc++         mHeaderViews.put(getRealHeaderViewCounts() + BASE_ITEM_TYPE_HEADER, view);
// mc++     }
// mc++ 
// mc++     public void addFootView(View view) {
// mc++         mFootViews.put(getRealFooterViewCounts() + BASE_ITEM_TYPE_FOOTER, view);
// mc++     }
// mc++ 
// mc++     public int getHeadersCount() {
// mc++ 
// mc++         return mIsShowHeader ? getRealHeaderViewCounts() : 0;
// mc++     }
// mc++ 
// mc++     public int getFootersCount() {
// mc++         return mIsShowFooter ? getRealFooterViewCounts() : 0;
// mc++     }
// mc++ 
// mc++     public void showHeader(boolean isShowHeader) {
// mc++         mIsShowHeader = isShowHeader;
// mc++         //        notifyDataSetChanged();
// mc++         if (isShowHeader) {
// mc++             notifyItemRangeInserted(0, getRealHeaderViewCounts());
// mc++         } else {
// mc++             notifyItemRangeRemoved(0, getRealHeaderViewCounts());
// mc++         }
// mc++     }
// mc++ 
// mc++     private int getRealHeaderViewCounts() {
// mc++         return mHeaderViews.size();
// mc++     }
// mc++ 
// mc++     public void showFooter(boolean isShowFooter) {
// mc++         mIsShowFooter = isShowFooter;
// mc++         //        notifyDataSetChanged();
// mc++         Log.i(TAG, "HeaderAndFooterWrapper.class:showFooter(): 146:" + getItemCount());
// mc++         if (isShowFooter) {
// mc++             notifyItemRangeInserted(getItemCount() - 1, getRealFooterViewCounts());
// mc++         } else {
// mc++             notifyItemRangeRemoved(getItemCount() - 1, getRealFooterViewCounts());
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     private int getRealFooterViewCounts() {
// mc++         return mFootViews.size();
// mc++     }
// mc++ 
// mc++ 
// mc++ }
