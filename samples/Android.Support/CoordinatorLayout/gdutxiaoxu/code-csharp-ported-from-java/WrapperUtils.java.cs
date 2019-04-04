// mc++ package com.xujun.contralayout.recyclerView;
// mc++ 
// mc++ import android.support.v7.widget.GridLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.support.v7.widget.StaggeredGridLayoutManager;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ /**
// mc++  * Created by xujun on 16/6/28.
// mc++  */
// mc++ public class WrapperUtils {
// mc++     public interface SpanSizeCallback {
// mc++         int getSpanSize(GridLayoutManager layoutManager, GridLayoutManager.SpanSizeLookup
// mc++                 oldLookup, int position);
// mc++     }
// mc++ 
// mc++     public static void onAttachedToRecyclerView(RecyclerView.Adapter innerAdapter, RecyclerView
// mc++             recyclerView, final SpanSizeCallback callback) {
// mc++         innerAdapter.onAttachedToRecyclerView(recyclerView);
// mc++ 
// mc++         RecyclerView.LayoutManager layoutManager = recyclerView.getLayoutManager();
// mc++         if (layoutManager instanceof GridLayoutManager) {
// mc++             final GridLayoutManager gridLayoutManager = (GridLayoutManager) layoutManager;
// mc++             final GridLayoutManager.SpanSizeLookup spanSizeLookup = gridLayoutManager
// mc++                     .getSpanSizeLookup();
// mc++ 
// mc++             gridLayoutManager.setSpanSizeLookup(new GridLayoutManager.SpanSizeLookup() {
// mc++                 @Override
// mc++                 public int getSpanSize(int position) {
// mc++                     return callback.getSpanSize(gridLayoutManager, spanSizeLookup, position);
// mc++                 }
// mc++             });
// mc++             gridLayoutManager.setSpanCount(gridLayoutManager.getSpanCount());
// mc++         }
// mc++     }
// mc++ 
// mc++     public static void setFullSpan(RecyclerView.ViewHolder holder) {
// mc++         ViewGroup.LayoutParams lp = holder.itemView.getLayoutParams();
// mc++ 
// mc++         if (lp != null
// mc++                 && lp instanceof StaggeredGridLayoutManager.LayoutParams) {
// mc++ 
// mc++             StaggeredGridLayoutManager.LayoutParams p = (StaggeredGridLayoutManager.LayoutParams)
// mc++                     lp;
// mc++ 
// mc++             p.setFullSpan(true);
// mc++         }
// mc++     }
// mc++ }
