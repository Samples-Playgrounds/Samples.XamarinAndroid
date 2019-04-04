// mc++ package com.xujun.contralayout.recyclerView;
// mc++ 
// mc++ import android.support.v7.widget.GridLayoutManager;
// mc++ import android.support.v7.widget.LinearLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.support.v7.widget.StaggeredGridLayoutManager;
// mc++ import android.view.View;
// mc++ 
// mc++ /**
// mc++  * Created by cundong on 2015/10/9.
// mc++  * <p/>
// mc++  * 继承自RecyclerView.OnScrollListener，可以监听到是否滑动到页面最低部
// mc++  */
// mc++ public class EndlessRecyclerOnScrollListener extends RecyclerView.OnScrollListener implements OnListLoadNextPageListener {
// mc++ 
// mc++     /**
// mc++      * 当前RecyclerView类型
// mc++      */
// mc++     protected LayoutManagerType layoutManagerType;
// mc++ 
// mc++     /**
// mc++      * 最后一个的位置
// mc++      */
// mc++     private int[] lastPositions;
// mc++ 
// mc++     /**
// mc++      * 最后一个可见的item的位置
// mc++      */
// mc++     private int lastVisibleItemPosition;
// mc++ 
// mc++     /**
// mc++      * 当前滑动的状态
// mc++      */
// mc++     private int currentScrollState = 0;
// mc++ 
// mc++     @Override
// mc++     public void onScrolled(RecyclerView recyclerView, int dx, int dy) {
// mc++         super.onScrolled(recyclerView, dx, dy);
// mc++ 
// mc++         RecyclerView.LayoutManager layoutManager = recyclerView.getLayoutManager();
// mc++ 
// mc++         if (layoutManagerType == null) {
// mc++             if (layoutManager instanceof LinearLayoutManager) {
// mc++                 layoutManagerType = LayoutManagerType.LinearLayout;
// mc++             } else if (layoutManager instanceof GridLayoutManager) {
// mc++                 layoutManagerType = LayoutManagerType.GridLayout;
// mc++             } else if (layoutManager instanceof StaggeredGridLayoutManager) {
// mc++                 layoutManagerType = LayoutManagerType.StaggeredGridLayout;
// mc++             } else {
// mc++                 throw new RuntimeException(
// mc++                         "Unsupported LayoutManager used. Valid ones are LinearLayoutManager, GridLayoutManager and StaggeredGridLayoutManager");
// mc++             }
// mc++         }
// mc++ 
// mc++         switch (layoutManagerType) {
// mc++             case LinearLayout:
// mc++                 lastVisibleItemPosition = ((LinearLayoutManager) layoutManager).findLastVisibleItemPosition();
// mc++                 break;
// mc++             case GridLayout:
// mc++                 lastVisibleItemPosition = ((GridLayoutManager) layoutManager).findLastVisibleItemPosition();
// mc++                 break;
// mc++             case StaggeredGridLayout:
// mc++                 StaggeredGridLayoutManager staggeredGridLayoutManager = (StaggeredGridLayoutManager) layoutManager;
// mc++                 if (lastPositions == null) {
// mc++                     lastPositions = new int[staggeredGridLayoutManager.getSpanCount()];
// mc++                 }
// mc++                 staggeredGridLayoutManager.findLastVisibleItemPositions(lastPositions);
// mc++                 lastVisibleItemPosition = findMax(lastPositions);
// mc++                 break;
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onScrollStateChanged(RecyclerView recyclerView, int newState) {
// mc++         super.onScrollStateChanged(recyclerView, newState);
// mc++         currentScrollState = newState;
// mc++         RecyclerView.LayoutManager layoutManager = recyclerView.getLayoutManager();
// mc++         int visibleItemCount = layoutManager.getChildCount();
// mc++         int totalItemCount = layoutManager.getItemCount();
// mc++         if ((visibleItemCount > 0 && currentScrollState == RecyclerView.SCROLL_STATE_IDLE && (lastVisibleItemPosition) >= totalItemCount - 1)) {
// mc++             onLoadNextPage(recyclerView);
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 取数组中最大值
// mc++      *
// mc++      * @param lastPositions
// mc++      * @return
// mc++      */
// mc++     private int findMax(int[] lastPositions) {
// mc++         int max = lastPositions[0];
// mc++         for (int value : lastPositions) {
// mc++             if (value > max) {
// mc++                 max = value;
// mc++             }
// mc++         }
// mc++ 
// mc++         return max;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onLoadNextPage(final View view) {
// mc++     }
// mc++ 
// mc++     public static enum LayoutManagerType {
// mc++         LinearLayout,
// mc++         StaggeredGridLayout,
// mc++         GridLayout
// mc++     }
// mc++ }
