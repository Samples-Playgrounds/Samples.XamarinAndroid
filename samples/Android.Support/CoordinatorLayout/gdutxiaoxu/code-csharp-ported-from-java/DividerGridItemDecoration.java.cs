// mc++ package com.xujun.contralayout.recyclerView.divider;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.content.res.TypedArray;
// mc++ import android.graphics.Canvas;
// mc++ import android.graphics.Rect;
// mc++ import android.graphics.drawable.Drawable;
// mc++ import android.support.v7.widget.GridLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.support.v7.widget.RecyclerView.LayoutManager;
// mc++ import android.support.v7.widget.StaggeredGridLayoutManager;
// mc++ import android.view.View;
// mc++ 
// mc++ /**
// mc++  * @ explain:针对GridLayoutManger的分割线
// mc++  * @ author：xujun on 2016-7-13 14:30
// mc++  * @ email：gdutxiaoxu@163.com
// mc++  */
// mc++ public class DividerGridItemDecoration extends RecyclerView.ItemDecoration {
// mc++ 
// mc++     private static final int[] ATTRS = new int[]{android.R.attr.listDivider};
// mc++     private Drawable mDivider;
// mc++ 
// mc++     public DividerGridItemDecoration(Context context) {
// mc++         final TypedArray a = context.obtainStyledAttributes(ATTRS);
// mc++         mDivider = a.getDrawable(0);
// mc++         a.recycle();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onDraw(Canvas c, RecyclerView parent, RecyclerView.State state) {
// mc++ 
// mc++         drawHorizontal(c, parent);
// mc++         drawVertical(c, parent);
// mc++ 
// mc++     }
// mc++ 
// mc++     private int getSpanCount(RecyclerView parent) {
// mc++         // 列数
// mc++         int spanCount = -1;
// mc++         LayoutManager layoutManager = parent.getLayoutManager();
// mc++         if (layoutManager instanceof GridLayoutManager) {
// mc++ 
// mc++             spanCount = ((GridLayoutManager) layoutManager).getSpanCount();
// mc++         } else if (layoutManager instanceof StaggeredGridLayoutManager) {
// mc++             spanCount = ((StaggeredGridLayoutManager) layoutManager)
// mc++                     .getSpanCount();
// mc++         }
// mc++         return spanCount;
// mc++     }
// mc++ 
// mc++     public void drawHorizontal(Canvas c, RecyclerView parent) {
// mc++         int childCount = parent.getChildCount();
// mc++         for (int i = 0; i < childCount; i++) {
// mc++             final View child = parent.getChildAt(i);
// mc++             final RecyclerView.LayoutParams params = (RecyclerView.LayoutParams) child
// mc++                     .getLayoutParams();
// mc++             final int left = child.getLeft() - params.leftMargin;
// mc++             final int right = child.getRight() + params.rightMargin
// mc++                     + mDivider.getIntrinsicWidth();
// mc++             final int top = child.getBottom() + params.bottomMargin;
// mc++             final int bottom = top + mDivider.getIntrinsicHeight();
// mc++             mDivider.setBounds(left, top, right, bottom);
// mc++             mDivider.draw(c);
// mc++         }
// mc++     }
// mc++ 
// mc++     public void drawVertical(Canvas c, RecyclerView parent) {
// mc++         final int childCount = parent.getChildCount();
// mc++         for (int i = 0; i < childCount; i++) {
// mc++             final View child = parent.getChildAt(i);
// mc++ 
// mc++             final RecyclerView.LayoutParams params = (RecyclerView.LayoutParams) child
// mc++                     .getLayoutParams();
// mc++             final int top = child.getTop() - params.topMargin;
// mc++             final int bottom = child.getBottom() + params.bottomMargin;
// mc++             final int left = child.getRight() + params.rightMargin;
// mc++             final int right = left + mDivider.getIntrinsicWidth();
// mc++ 
// mc++             mDivider.setBounds(left, top, right, bottom);
// mc++             mDivider.draw(c);
// mc++         }
// mc++     }
// mc++ 
// mc++     private boolean isLastColum(RecyclerView parent, int pos, int spanCount,
// mc++                                 int childCount) {
// mc++         LayoutManager layoutManager = parent.getLayoutManager();
// mc++         if (layoutManager instanceof GridLayoutManager) {
// mc++             if ((pos + 1) % spanCount == 0)
// mc++             // 如果是最后一列，则不需要绘制右边
// mc++             {
// mc++                 return true;
// mc++             }
// mc++         } else if (layoutManager instanceof StaggeredGridLayoutManager) {
// mc++             int orientation = ((StaggeredGridLayoutManager) layoutManager)
// mc++                     .getOrientation();
// mc++             if (orientation == StaggeredGridLayoutManager.VERTICAL) {
// mc++                 if ((pos + 1) % spanCount == 0)// 如果是最后一列，则不需要绘制右边
// mc++                 {
// mc++                     return true;
// mc++                 }
// mc++             } else {
// mc++                 childCount = childCount - childCount % spanCount;
// mc++                 if (pos >= childCount)// 如果是最后一列，则不需要绘制右边
// mc++                     return true;
// mc++             }
// mc++         }
// mc++         return false;
// mc++     }
// mc++ 
// mc++     private boolean isLastRaw(RecyclerView parent, int pos, int spanCount,
// mc++                               int childCount) {
// mc++         LayoutManager layoutManager = parent.getLayoutManager();
// mc++         if (layoutManager instanceof GridLayoutManager) {
// mc++             childCount = childCount - childCount % spanCount;
// mc++             if (pos >= childCount)// 如果是最后一行，则不需要绘制底部
// mc++                 return true;
// mc++         } else if (layoutManager instanceof StaggeredGridLayoutManager) {
// mc++             int orientation = ((StaggeredGridLayoutManager) layoutManager)
// mc++                     .getOrientation();
// mc++             // StaggeredGridLayoutManager 且纵向滚动
// mc++             if (orientation == StaggeredGridLayoutManager.VERTICAL) {
// mc++                 childCount = childCount - childCount % spanCount;
// mc++                 // 如果是最后一行，则不需要绘制底部
// mc++                 if (pos >= childCount)
// mc++                     return true;
// mc++                 // StaggeredGridLayoutManager 且横向滚动
// mc++             } else {
// mc++                 // 如果是最后一行，则不需要绘制底部
// mc++                 if ((pos + 1) % spanCount == 0) {
// mc++                     return true;
// mc++                 }
// mc++             }
// mc++         }
// mc++         return false;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void getItemOffsets(Rect outRect, int itemPosition,
// mc++                                RecyclerView parent) {
// mc++         int spanCount = getSpanCount(parent);
// mc++         int childCount = parent.getAdapter().getItemCount();
// mc++         // 如果是最后一行，则不需要绘制底部
// mc++         if (isLastRaw(parent, itemPosition, spanCount, childCount)) {
// mc++             outRect.set(0, 0, mDivider.getIntrinsicWidth(), 0);
// mc++             // 如果是最后一列，则不需要绘制右边
// mc++         } else if (isLastColum(parent, itemPosition, spanCount, childCount)) {
// mc++             outRect.set(0, 0, 0, mDivider.getIntrinsicHeight());
// mc++         } else {
// mc++             outRect.set(0, 0, mDivider.getIntrinsicWidth(),
// mc++                     mDivider.getIntrinsicHeight());
// mc++         }
// mc++     }
// mc++ }
