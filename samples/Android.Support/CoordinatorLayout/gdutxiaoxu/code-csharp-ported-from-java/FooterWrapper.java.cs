// mc++ package com.xujun.contralayout.recyclerView;
// mc++ 
// mc++ import android.support.v4.util.SparseArrayCompat;
// mc++ import android.support.v7.widget.GridLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ /**
// mc++  * 博客地址：http://blog.csdn.net/gdutxiaoxu
// mc++  *
// mc++  * @author xujun
// mc++  * @time 2016/7/7 17:29.
// mc++  */
// mc++ public class FooterWrapper<T> extends DefaultAdapter<T> {
// mc++ 
// mc++     private static final int BASE_ITEM_TYPE_FOOTER = 200000;
// mc++     private boolean mIsShowFooter = false;
// mc++ 
// mc++     public static final String TAG = "tag";
// mc++ 
// mc++     private SparseArrayCompat<ViewGroup> mFooterViews = new SparseArrayCompat<>();
// mc++ 
// mc++     private RecyclerView.Adapter mInnerAdapter;
// mc++     private View mFoot;
// mc++ 
// mc++     public FooterWrapper(RecyclerView.Adapter adapter) {
// mc++         super();
// mc++         mInnerAdapter = adapter;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getItemViewType(int position) {
// mc++         if (isFooterPos(position)) {
// mc++             return mFooterViews.keyAt(position - getRealItemCount());
// mc++         }
// mc++ 
// mc++         return mInnerAdapter.getItemViewType(position);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
// mc++         if (mFooterViews.get(viewType) != null) {
// mc++             BaseRecyclerHolder holder = BaseRecyclerHolder.createViewHolder(mFoot);
// mc++             return holder;
// mc++ 
// mc++         }
// mc++         return (BaseRecyclerHolder) mInnerAdapter.onCreateViewHolder(parent, viewType);
// mc++     }
// mc++ 
// mc++     private int getRealItemCount() {
// mc++         return mInnerAdapter.getItemCount();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onBindViewHolder(RecyclerView.ViewHolder holder, int position) {
// mc++ 
// mc++         /***
// mc++          * 如果是footerView的话直接返回
// mc++          */
// mc++         if (isFooterPos(position)) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         mInnerAdapter.onBindViewHolder(holder, position);
// mc++ 
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getItemCount() {
// mc++         return getRealItemCount() + getFootViewCounts();
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onAttachedToRecyclerView(RecyclerView recyclerView) {
// mc++         WrapperUtils.onAttachedToRecyclerView(mInnerAdapter, recyclerView, new WrapperUtils
// mc++                 .SpanSizeCallback() {
// mc++             @Override
// mc++             public int getSpanSize(GridLayoutManager layoutManager, GridLayoutManager
// mc++                     .SpanSizeLookup oldLookup, int position) {
// mc++                 int viewType = getItemViewType(position);
// mc++ 
// mc++                 if (mFooterViews.get(viewType) != null) {
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
// mc++         if (isFooterPos(position)) {
// mc++             WrapperUtils.setFullSpan(holder);
// mc++         }
// mc++     }
// mc++ 
// mc++     private boolean isFooterPos(int position) {
// mc++ 
// mc++         return position >= getRealItemCount();
// mc++     }
// mc++ 
// mc++     public int getFootViewCounts() {
// mc++         if (mIsShowFooter) {
// mc++             return getRealFooterViewCounts();
// mc++         } else {
// mc++             return 0;
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     private int getRealFooterViewCounts() {
// mc++         return mFooterViews.size();
// mc++     }
// mc++ 
// mc++     public void addFootView(View view) {
// mc++         if (!(view instanceof ViewGroup)) {
// mc++             throw new IllegalStateException("the view must be instanceOf ViewGroup");
// mc++         }
// mc++         mFoot = view;
// mc++         mFooterViews.put(mFooterViews.size() + BASE_ITEM_TYPE_FOOTER, (ViewGroup) view);
// mc++     }
// mc++ 
// mc++ 
// mc++ 
// mc++     public void showFooter(boolean isShowFooter) {
// mc++         mIsShowFooter = isShowFooter;
// mc++         //        notifyDataSetChanged();
// mc++         //        if (isShowFooter) {
// mc++         //            notifyItemRangeInserted(getRealItemCount(), getRealFooterViewCounts());
// mc++         //        } else {
// mc++         //            notifyItemRangeRemoved(getRealItemCount(), getRealFooterViewCounts());
// mc++         //        }
// mc++         if (isShowFooter) {
// mc++             notifyItemInserted(getRealItemCount());
// mc++         } else {
// mc++             notifyItemRemoved(getRealItemCount());
// mc++         }
// mc++     }
// mc++ 
// mc++     public void showFooter() {
// mc++         mIsShowFooter = true;
// mc++         //        notifyDataSetChanged();
// mc++         notifyItemRangeInserted(getRealItemCount(), getFootViewCounts());
// mc++ 
// mc++     }
// mc++ 
// mc++     public void hideFooterView() {
// mc++         mIsShowFooter = false;
// mc++         notifyItemRangeRemoved(getRealItemCount(), getFootViewCounts());
// mc++     }
// mc++ 
// mc++     public RecyclerView.Adapter getInnerAdapter() {
// mc++         return mInnerAdapter;
// mc++     }
// mc++ 
// mc++ 
// mc++ }
