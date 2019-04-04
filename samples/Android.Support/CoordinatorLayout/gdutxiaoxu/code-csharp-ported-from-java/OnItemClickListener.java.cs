// mc++ package com.xujun.contralayout.recyclerView;
// mc++ 
// mc++ import android.support.v4.view.GestureDetectorCompat;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.GestureDetector;
// mc++ import android.view.MotionEvent;
// mc++ import android.view.View;
// mc++ 
// mc++ /**
// mc++  * Created by xujun、on 2016/5/16.
// mc++  */
// mc++ public abstract class OnItemClickListener extends RecyclerView.SimpleOnItemTouchListener {
// mc++ 
// mc++     private GestureDetectorCompat mGestureDetectorCompat;
// mc++     private RecyclerView mRecyclerView;
// mc++ 
// mc++     public OnItemClickListener(RecyclerView recyclerView) {
// mc++         mRecyclerView = recyclerView;
// mc++         mGestureDetectorCompat = new GestureDetectorCompat(recyclerView.getContext(), new
// mc++                 ItemTouchHelperGestureListener());
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onInterceptTouchEvent(RecyclerView rv, MotionEvent e) {
// mc++         mGestureDetectorCompat.onTouchEvent(e);
// mc++         return false;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onTouchEvent(RecyclerView rv, MotionEvent e) {
// mc++         mGestureDetectorCompat.onTouchEvent(e);
// mc++     }
// mc++ 
// mc++     private class ItemTouchHelperGestureListener extends GestureDetector.SimpleOnGestureListener {
// mc++         @Override
// mc++         public boolean onSingleTapUp(MotionEvent e) {
// mc++             View child = mRecyclerView.findChildViewUnder(e.getX(), e.getY());
// mc++             if (child != null) {
// mc++                 onItemClick(mRecyclerView.getChildViewHolder(child));
// mc++             }
// mc++             return true;
// mc++         }
// mc++ 
// mc++         @Override
// mc++         public void onLongPress(MotionEvent e) {
// mc++             View child = mRecyclerView.findChildViewUnder(e.getX(), e.getY());
// mc++             if (child != null) {
// mc++                 onItemLongClick(mRecyclerView.getChildViewHolder(child));
// mc++             }
// mc++         }
// mc++     }
// mc++ 
// mc++     public void onItemClick(RecyclerView.ViewHolder viewHolder) {
// mc++     }
// mc++ 
// mc++     public void onItemLongClick(RecyclerView.ViewHolder viewHolder) {
// mc++     }
// mc++ }
