// mc++ package com.xujun.contralayout.recyclerView;
// mc++ 
// mc++ import android.support.v7.widget.LinearLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.support.v7.widget.StaggeredGridLayoutManager;
// mc++ 
// mc++ /**
// mc++  * Created by xujun、on 2016/5/16.
// mc++  */
// mc++ public class LoadMoreScrollerListener extends RecyclerView.OnScrollListener {
// mc++ 
// mc++     private boolean mDown;
// mc++     private int[] mPositions;
// mc++ 
// mc++     public LoadMoreScrollerListener(OnLoadingListener loadingListener) {
// mc++         mLoadingListener = loadingListener;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onScrolled(RecyclerView recyclerView, int dx, int dy) {
// mc++         super.onScrolled(recyclerView, dx, dy);
// mc++         mDown = dy >= 0;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onScrollStateChanged(RecyclerView recyclerView, int newState) {
// mc++         super.onScrollStateChanged(recyclerView, newState);
// mc++         if (newState != RecyclerView.SCROLL_STATE_IDLE)
// mc++             return;
// mc++ //        LUtils.w("down:" + mDown);
// mc++         if (mDown && mLoadingListener != null && isLastItem(recyclerView.getLayoutManager())) {
// mc++             mLoadingListener.onLoadMore();
// mc++ //            Log.w("LoadMoreScrollerListener", "onScrollStateChanged:");
// mc++         }
// mc++     }
// mc++ 
// mc++     private boolean isLastItem(RecyclerView.LayoutManager layoutManager) {
// mc++         int totalItemCount = layoutManager.getItemCount();
// mc++         if (layoutManager instanceof StaggeredGridLayoutManager) {
// mc++             StaggeredGridLayoutManager staggeredGridLayoutManager = (StaggeredGridLayoutManager)
// mc++                     layoutManager;
// mc++             if (null == mPositions) {
// mc++                 mPositions = new int[staggeredGridLayoutManager.getSpanCount()];
// mc++             }
// mc++             staggeredGridLayoutManager.findLastCompletelyVisibleItemPositions(mPositions);
// mc++             return totalItemCount == getLast(mPositions) + 1;
// mc++         } else if (layoutManager instanceof LinearLayoutManager){
// mc++             return totalItemCount - 1 == ((LinearLayoutManager) layoutManager).findLastCompletelyVisibleItemPosition();
// mc++         }
// mc++         return false;
// mc++     }
// mc++ 
// mc++     private int getLast(int[] mPositions) {
// mc++         int last = mPositions[0];
// mc++         for (int value : mPositions) {
// mc++             if (value > last) {
// mc++                 last = value;
// mc++             }
// mc++         }
// mc++         return last;
// mc++     }
// mc++ 
// mc++     private OnLoadingListener mLoadingListener;
// mc++ 
// mc++     public interface OnLoadingListener {
// mc++         void onLoadMore();
// mc++     }
// mc++ }
