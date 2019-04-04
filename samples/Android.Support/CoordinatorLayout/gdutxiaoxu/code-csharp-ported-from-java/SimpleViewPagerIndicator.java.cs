// mc++ package com.zhy.stickynavlayout.view;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.graphics.Canvas;
// mc++ import android.graphics.Color;
// mc++ import android.graphics.Paint;
// mc++ import android.util.AttributeSet;
// mc++ import android.util.TypedValue;
// mc++ import android.view.Gravity;
// mc++ import android.view.View;
// mc++ import android.widget.LinearLayout;
// mc++ import android.widget.TextView;
// mc++ 
// mc++ public class SimpleViewPagerIndicator extends LinearLayout {
// mc++ 
// mc++     private static final int COLOR_TEXT_NORMAL = 0xFF000000;
// mc++     private static final int COLOR_INDICATOR_COLOR = Color.GREEN;
// mc++ 
// mc++     private String[] mTitles;
// mc++     private int mIndicatorColor = COLOR_INDICATOR_COLOR;
// mc++     private float mTranslationX;
// mc++     private Paint mPaint = new Paint();
// mc++     private int mTabWidth;
// mc++ 
// mc++     public SimpleViewPagerIndicator(Context context) {
// mc++         this(context, null);
// mc++     }
// mc++ 
// mc++     public SimpleViewPagerIndicator(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++         mPaint.setColor(mIndicatorColor);
// mc++         mPaint.setStrokeWidth(9.0F);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void onSizeChanged(int w, int h, int oldw, int oldh) {
// mc++         super.onSizeChanged(w, h, oldw, oldh);
// mc++         mTabWidth = w / mTitles.length;//tab宽度平分indicator
// mc++     }
// mc++ 
// mc++ 
// mc++     //绘制横线
// mc++     @Override
// mc++     protected void dispatchDraw(Canvas canvas) {
// mc++         super.dispatchDraw(canvas);
// mc++ 
// mc++         canvas.save();
// mc++         canvas.translate(mTranslationX, getHeight() - 2);//移动canvas
// mc++         canvas.drawLine(0, 0, mTabWidth, 0, mPaint);//绘制
// mc++         canvas.restore();
// mc++     }
// mc++ 
// mc++     //在指定的位置绘制横线
// mc++     public void scroll(int position, float offset) {
// mc++         mTranslationX = getWidth() / mTitles.length * (position + offset);
// mc++         invalidate();//会触发重绘，进而触发dispatchDraw
// mc++     }
// mc++ 
// mc++     private void generateTitleView() {
// mc++         if (getChildCount() > 0)
// mc++             this.removeAllViews();
// mc++         int count = mTitles.length;
// mc++ 
// mc++         setWeightSum(count);
// mc++         for (int i = 0; i < count; i++) {
// mc++             TextView tv = new TextView(getContext());
// mc++             LayoutParams lp = new LayoutParams(0, LayoutParams.MATCH_PARENT);
// mc++             lp.weight = 1;
// mc++             tv.setGravity(Gravity.CENTER);
// mc++             tv.setTextColor(COLOR_TEXT_NORMAL);
// mc++             tv.setText(mTitles[i]);
// mc++             tv.setTextSize(TypedValue.COMPLEX_UNIT_SP, 16);
// mc++             tv.setLayoutParams(lp);
// mc++             tv.setOnClickListener(new OnClickListener() {
// mc++                 @Override
// mc++                 public void onClick(View v) {
// mc++ 
// mc++                 }
// mc++             });
// mc++             addView(tv);
// mc++         }
// mc++     }
// mc++ 
// mc++ //public--------------------------------------------------------------------------------------------
// mc++ 
// mc++     public void setTitles(String[] titles) {
// mc++         mTitles = titles;
// mc++         generateTitleView();
// mc++     }
// mc++ 
// mc++     public void setIndicatorColor(int indicatorColor) {
// mc++         this.mIndicatorColor = indicatorColor;
// mc++     }
// mc++ }
