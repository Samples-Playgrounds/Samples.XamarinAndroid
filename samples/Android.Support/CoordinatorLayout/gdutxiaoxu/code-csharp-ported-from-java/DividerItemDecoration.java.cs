// mc++ package com.xujun.contralayout.recyclerView.divider;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.content.res.TypedArray;
// mc++ import android.graphics.Canvas;
// mc++ import android.graphics.Rect;
// mc++ import android.graphics.drawable.Drawable;
// mc++ import android.support.v7.widget.LinearLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.View;
// mc++ 
// mc++ /**
// mc++  * @author xujun
// mc++  * @explain RecyclerView LinearLayout的分割线
// mc++  * @time 2016/6/16 17:39.
// mc++  */
// mc++ 
// mc++ public class DividerItemDecoration extends RecyclerView.ItemDecoration {
// mc++ 
// mc++     private static final int[] ATTRS = new int[]{
// mc++             android.R.attr.listDivider
// mc++     };
// mc++ 
// mc++     public static final int HORIZONTAL_LIST = LinearLayoutManager.HORIZONTAL;
// mc++ 
// mc++     public static final int VERTICAL_LIST = LinearLayoutManager.VERTICAL;
// mc++ 
// mc++     private Drawable mDivider;
// mc++ 
// mc++     private int mOrientation;
// mc++ 
// mc++     public DividerItemDecoration(Context context, int orientation) {
// mc++         final TypedArray a = context.obtainStyledAttributes(ATTRS);
// mc++         mDivider = a.getDrawable(0);
// mc++         a.recycle();
// mc++         setOrientation(orientation);
// mc++     }
// mc++ 
// mc++     public void setOrientation(int orientation) {
// mc++         if (orientation != HORIZONTAL_LIST && orientation != VERTICAL_LIST) {
// mc++             throw new IllegalArgumentException("invalid orientation");
// mc++         }
// mc++         mOrientation = orientation;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onDraw(Canvas c, RecyclerView parent, RecyclerView.State state) {
// mc++         if (mOrientation == VERTICAL_LIST) {
// mc++             drawVertical(c, parent);
// mc++         } else {
// mc++             drawHorizontal(c, parent);
// mc++         }
// mc++     }
// mc++ 
// mc++     public void drawVertical(Canvas c, RecyclerView parent) {
// mc++         final int left = parent.getPaddingLeft();
// mc++         final int right = parent.getWidth() - parent.getPaddingRight();
// mc++ 
// mc++         final int childCount = parent.getChildCount();
// mc++         for (int i = 0; i < childCount; i++) {
// mc++             final View child = parent.getChildAt(i);
// mc++             final RecyclerView.LayoutParams params = (RecyclerView.LayoutParams) child
// mc++                     .getLayoutParams();
// mc++             final int top = child.getBottom() + params.bottomMargin;
// mc++             final int bottom = top + mDivider.getIntrinsicHeight();
// mc++             mDivider.setBounds(left, top, right, bottom);
// mc++             mDivider.draw(c);
// mc++         }
// mc++     }
// mc++ 
// mc++     public void drawHorizontal(Canvas c, RecyclerView parent) {
// mc++         final int top = parent.getPaddingTop();
// mc++         final int bottom = parent.getHeight() - parent.getPaddingBottom();
// mc++ 
// mc++         final int childCount = parent.getChildCount();
// mc++         for (int i = 0; i < childCount; i++) {
// mc++             final View child = parent.getChildAt(i);
// mc++             final RecyclerView.LayoutParams params = (RecyclerView.LayoutParams) child
// mc++                     .getLayoutParams();
// mc++             final int left = child.getRight() + params.rightMargin;
// mc++ //            绘制
// mc++             final int right = left + mDivider.getIntrinsicHeight();
// mc++             mDivider.setBounds(left, top, right, bottom);
// mc++             mDivider.draw(c);
// mc++         }
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void getItemOffsets(Rect outRect, int itemPosition, RecyclerView parent) {
// mc++         if (mOrientation == VERTICAL_LIST) {
// mc++             outRect.set(0, 0, 0, mDivider.getIntrinsicHeight());
// mc++         } else {
// mc++             outRect.set(0, 0, mDivider.getIntrinsicWidth(), 0);
// mc++         }
// mc++     }
// mc++ }
