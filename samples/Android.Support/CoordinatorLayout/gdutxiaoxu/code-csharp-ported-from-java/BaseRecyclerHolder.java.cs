// mc++ package com.xujun.contralayout.recyclerView;
// mc++ 
// mc++ import android.annotation.SuppressLint;
// mc++ import android.content.Context;
// mc++ import android.graphics.Bitmap;
// mc++ import android.graphics.Paint;
// mc++ import android.graphics.Typeface;
// mc++ import android.graphics.drawable.Drawable;
// mc++ import android.os.Build;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.text.util.Linkify;
// mc++ import android.util.SparseArray;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ import android.view.animation.AlphaAnimation;
// mc++ import android.widget.Checkable;
// mc++ import android.widget.ImageView;
// mc++ import android.widget.ProgressBar;
// mc++ import android.widget.TextView;
// mc++ 
// mc++ /**
// mc++  * Created by xujun on 2016/5/9.
// mc++  */
// mc++ public class BaseRecyclerHolder extends RecyclerView.ViewHolder {
// mc++ 
// mc++     private SparseArray<View> mViews;
// mc++     private View mConvertView;
// mc++     Context mContext;
// mc++ 
// mc++     public BaseRecyclerHolder(View itemView) {
// mc++         this(itemView,itemView.getContext());
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder(View itemView,Context context) {
// mc++         super(itemView);
// mc++         mContext=context;
// mc++         mConvertView = itemView;
// mc++         mViews = new SparseArray<>();
// mc++         mConvertView.setTag(this);
// mc++     }
// mc++ 
// mc++     public static BaseRecyclerHolder createViewHolder(View itemView) {
// mc++        return createViewHolder(itemView.getContext(),itemView);
// mc++     }
// mc++ 
// mc++     public static BaseRecyclerHolder createViewHolder(Context context,View itemView) {
// mc++ 
// mc++         BaseRecyclerHolder holder = new BaseRecyclerHolder(itemView,context);
// mc++         return holder;
// mc++     }
// mc++ 
// mc++     public static BaseRecyclerHolder createViewHolder(Context context,ViewGroup parent, int layoutId) {
// mc++         View itemView = LayoutInflater.from(context).inflate(layoutId, parent,
// mc++                 false);
// mc++ 
// mc++         return createViewHolder(context,itemView);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 通过viewId获取控件
// mc++      */
// mc++     public <T extends View> T getView(int viewId) {
// mc++         View view = mViews.get(viewId);
// mc++         if (view == null) {
// mc++             view = mConvertView.findViewById(viewId);
// mc++             mViews.put(viewId, view);
// mc++         }
// mc++         return (T) view;
// mc++     }
// mc++ 
// mc++     public View getConvertView() {
// mc++         return mConvertView;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 设置TextView的值
// mc++      */
// mc++     public BaseRecyclerHolder setText(int viewId, String text) {
// mc++         TextView tv = getView(viewId);
// mc++         tv.setText(text);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder setImageResource(int viewId, int resId) {
// mc++         ImageView view = getView(viewId);
// mc++         view.setImageResource(resId);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder setImageBitmap(int viewId, Bitmap bitmap) {
// mc++         ImageView view = getView(viewId);
// mc++         view.setImageBitmap(bitmap);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder setImageDrawable(int viewId, Drawable drawable) {
// mc++         ImageView view = getView(viewId);
// mc++         view.setImageDrawable(drawable);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder setBackgroundColor(int viewId, int color) {
// mc++         View view = getView(viewId);
// mc++         view.setBackgroundColor(color);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder setBackgroundRes(int viewId, int backgroundRes) {
// mc++         View view = getView(viewId);
// mc++         view.setBackgroundResource(backgroundRes);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder setTextColor(int viewId, int textColor) {
// mc++         TextView view = getView(viewId);
// mc++         view.setTextColor(textColor);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     @SuppressLint("NewApi")
// mc++     public BaseRecyclerHolder setAlpha(int viewId, float value) {
// mc++         if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.HONEYCOMB) {
// mc++             getView(viewId).setAlpha(value);
// mc++         } else {
// mc++             // Pre-honeycomb hack to set Alpha value
// mc++             AlphaAnimation alpha = new AlphaAnimation(value, value);
// mc++             alpha.setDuration(0);
// mc++             alpha.setFillAfter(true);
// mc++             getView(viewId).startAnimation(alpha);
// mc++         }
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder setVisible(int viewId, boolean visible) {
// mc++         View view = getView(viewId);
// mc++         view.setVisibility(visible ? View.VISIBLE : View.GONE);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder linkify(int viewId) {
// mc++         TextView view = getView(viewId);
// mc++         Linkify.addLinks(view, Linkify.ALL);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder setTypeface(Typeface typeface, int... viewIds) {
// mc++         for (int viewId : viewIds) {
// mc++             TextView view = getView(viewId);
// mc++             view.setTypeface(typeface);
// mc++             view.setPaintFlags(view.getPaintFlags() | Paint.SUBPIXEL_TEXT_FLAG);
// mc++         }
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder setProgress(int viewId, int progress) {
// mc++         ProgressBar view = getView(viewId);
// mc++         view.setProgress(progress);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder setProgress(int viewId, int progress, int max) {
// mc++         ProgressBar view = getView(viewId);
// mc++         view.setMax(max);
// mc++         view.setProgress(progress);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder setMax(int viewId, int max) {
// mc++         ProgressBar view = getView(viewId);
// mc++         view.setMax(max);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder setTag(int viewId, Object tag) {
// mc++         View view = getView(viewId);
// mc++         view.setTag(tag);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder setTag(int viewId, int key, Object tag) {
// mc++         View view = getView(viewId);
// mc++         view.setTag(key, tag);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder setChecked(int viewId, boolean checked) {
// mc++         Checkable view = getView(viewId);
// mc++         view.setChecked(checked);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 关于事件的
// mc++      */
// mc++     public BaseRecyclerHolder setOnClickListener(int viewId, View.OnClickListener listener) {
// mc++         View view = getView(viewId);
// mc++         view.setOnClickListener(listener);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder setOnTouchListener(int viewId, View.OnTouchListener listener) {
// mc++         View view = getView(viewId);
// mc++         view.setOnTouchListener(listener);
// mc++         return this;
// mc++     }
// mc++ 
// mc++     public BaseRecyclerHolder setOnLongClickListener(int viewId, View.OnLongClickListener
// mc++             listener) {
// mc++         View view = getView(viewId);
// mc++         view.setOnLongClickListener(listener);
// mc++         return this;
// mc++     }
// mc++ }
