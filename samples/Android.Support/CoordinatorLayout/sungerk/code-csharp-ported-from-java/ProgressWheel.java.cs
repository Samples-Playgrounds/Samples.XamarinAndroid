// mc++ package sunger.net.org.coordinatorlayoutdemos.widget;
// mc++ 
// mc++ import android.annotation.TargetApi;
// mc++ import android.content.Context;
// mc++ import android.content.res.TypedArray;
// mc++ import android.graphics.Canvas;
// mc++ import android.graphics.Paint;
// mc++ import android.graphics.Paint.Style;
// mc++ import android.graphics.RectF;
// mc++ import android.os.Build;
// mc++ import android.os.Parcel;
// mc++ import android.os.Parcelable;
// mc++ import android.os.SystemClock;
// mc++ import android.provider.Settings;
// mc++ import android.util.AttributeSet;
// mc++ import android.util.DisplayMetrics;
// mc++ import android.util.TypedValue;
// mc++ import android.view.View;
// mc++ 
// mc++ import sunger.net.org.coordinatorlayoutdemos.R;
// mc++ 
// mc++ 
// mc++ /**
// mc++  * Created by sunger on 2015/10/30.
// mc++  */
// mc++ public class ProgressWheel  extends View {
// mc++     private static final String TAG = ProgressWheel.class.getSimpleName();
// mc++     private final int barLength = 16;
// mc++     private final int barMaxLength = 270;
// mc++     private final long pauseGrowingTime = 200;
// mc++     /**
// mc++      * *********
// mc++      * DEFAULTS *
// mc++      * **********
// mc++      */
// mc++     //Sizes (with defaults in DP)
// mc++     private int circleRadius = 28;
// mc++     private int barWidth = 4;
// mc++     private int rimWidth = 4;
// mc++     private boolean fillRadius = false;
// mc++     private double timeStartGrowing = 0;
// mc++     private double barSpinCycleTime = 460;
// mc++     private float barExtraLength = 0;
// mc++     private boolean barGrowingFromFront = true;
// mc++     private long pausedTimeWithoutGrowing = 0;
// mc++     //Colors (with defaults)
// mc++     private int barColor = 0xAA000000;
// mc++     private int rimColor = 0x00FFFFFF;
// mc++ 
// mc++     //Paints
// mc++     private Paint barPaint = new Paint();
// mc++     private Paint rimPaint = new Paint();
// mc++ 
// mc++     //Rectangles
// mc++     private RectF circleBounds = new RectF();
// mc++ 
// mc++     //Animation
// mc++     //The amount of degrees per second
// mc++     private float spinSpeed = 230.0f;
// mc++     //private float spinSpeed = 120.0f;
// mc++     // The last time the spinner was animated
// mc++     private long lastTimeAnimated = 0;
// mc++ 
// mc++     private boolean linearProgress;
// mc++ 
// mc++     private float mProgress = 0.0f;
// mc++     private float mTargetProgress = 0.0f;
// mc++     private boolean isSpinning = false;
// mc++ 
// mc++     private ProgressCallback callback;
// mc++ 
// mc++     private boolean shouldAnimate;
// mc++ 
// mc++     /**
// mc++      * The constructor for the ProgressWheel
// mc++      */
// mc++     public ProgressWheel(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++ 
// mc++         parseAttributes(context.obtainStyledAttributes(attrs, R.styleable.ProgressWheel));
// mc++ 
// mc++         setAnimationEnabled();
// mc++     }
// mc++ 
// mc++     /**
// mc++      * The constructor for the ProgressWheel
// mc++      */
// mc++     public ProgressWheel(Context context) {
// mc++         super(context);
// mc++         setAnimationEnabled();
// mc++     }
// mc++ 
// mc++     @TargetApi(Build.VERSION_CODES.JELLY_BEAN_MR1) private void setAnimationEnabled() {
// mc++         int currentApiVersion = Build.VERSION.SDK_INT;
// mc++ 
// mc++         float animationValue;
// mc++         if (currentApiVersion >= Build.VERSION_CODES.JELLY_BEAN_MR1) {
// mc++             animationValue = Settings.Global.getFloat(getContext().getContentResolver(),
// mc++                     Settings.Global.ANIMATOR_DURATION_SCALE, 1);
// mc++         } else {
// mc++             animationValue = Settings.System.getFloat(getContext().getContentResolver(),
// mc++                     Settings.System.ANIMATOR_DURATION_SCALE, 1);
// mc++         }
// mc++ 
// mc++         shouldAnimate = animationValue != 0;
// mc++     }
// mc++ 
// mc++     //----------------------------------
// mc++     //Setting up stuff
// mc++     //----------------------------------
// mc++ 
// mc++     @Override protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec) {
// mc++         super.onMeasure(widthMeasureSpec, heightMeasureSpec);
// mc++ 
// mc++         int viewWidth = circleRadius + this.getPaddingLeft() + this.getPaddingRight();
// mc++         int viewHeight = circleRadius + this.getPaddingTop() + this.getPaddingBottom();
// mc++ 
// mc++         int widthMode = MeasureSpec.getMode(widthMeasureSpec);
// mc++         int widthSize = MeasureSpec.getSize(widthMeasureSpec);
// mc++         int heightMode = MeasureSpec.getMode(heightMeasureSpec);
// mc++         int heightSize = MeasureSpec.getSize(heightMeasureSpec);
// mc++ 
// mc++         int width;
// mc++         int height;
// mc++ 
// mc++         //Measure Width
// mc++         if (widthMode == MeasureSpec.EXACTLY) {
// mc++             //Must be this size
// mc++             width = widthSize;
// mc++         } else if (widthMode == MeasureSpec.AT_MOST) {
// mc++             //Can't be bigger than...
// mc++             width = Math.min(viewWidth, widthSize);
// mc++         } else {
// mc++             //Be whatever you want
// mc++             width = viewWidth;
// mc++         }
// mc++ 
// mc++         //Measure Height
// mc++         if (heightMode == MeasureSpec.EXACTLY || widthMode == MeasureSpec.EXACTLY) {
// mc++             //Must be this size
// mc++             height = heightSize;
// mc++         } else if (heightMode == MeasureSpec.AT_MOST) {
// mc++             //Can't be bigger than...
// mc++             height = Math.min(viewHeight, heightSize);
// mc++         } else {
// mc++             //Be whatever you want
// mc++             height = viewHeight;
// mc++         }
// mc++ 
// mc++         setMeasuredDimension(width, height);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Use onSizeChanged instead of onAttachedToWindow to get the dimensions of the view,
// mc++      * because this method is called after measuring the dimensions of MATCH_PARENT & WRAP_CONTENT.
// mc++      * Use this dimensions to setup the bounds and paints.
// mc++      */
// mc++     @Override protected void onSizeChanged(int w, int h, int oldw, int oldh) {
// mc++         super.onSizeChanged(w, h, oldw, oldh);
// mc++ 
// mc++         setupBounds(w, h);
// mc++         setupPaints();
// mc++         invalidate();
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Set the properties of the paints we're using to
// mc++      * draw the progress wheel
// mc++      */
// mc++     private void setupPaints() {
// mc++         barPaint.setColor(barColor);
// mc++         barPaint.setAntiAlias(true);
// mc++         barPaint.setStyle(Style.STROKE);
// mc++         barPaint.setStrokeWidth(barWidth);
// mc++ 
// mc++         rimPaint.setColor(rimColor);
// mc++         rimPaint.setAntiAlias(true);
// mc++         rimPaint.setStyle(Style.STROKE);
// mc++         rimPaint.setStrokeWidth(rimWidth);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Set the bounds of the component
// mc++      */
// mc++     private void setupBounds(int layout_width, int layout_height) {
// mc++         int paddingTop = getPaddingTop();
// mc++         int paddingBottom = getPaddingBottom();
// mc++         int paddingLeft = getPaddingLeft();
// mc++         int paddingRight = getPaddingRight();
// mc++ 
// mc++         if (!fillRadius) {
// mc++             // Width should equal to Height, find the min value to setup the circle
// mc++             int minValue = Math.min(layout_width - paddingLeft - paddingRight,
// mc++                     layout_height - paddingBottom - paddingTop);
// mc++ 
// mc++             int circleDiameter = Math.min(minValue, circleRadius * 2 - barWidth * 2);
// mc++ 
// mc++             // Calc the Offset if needed for centering the wheel in the available space
// mc++             int xOffset = (layout_width - paddingLeft - paddingRight - circleDiameter) / 2 + paddingLeft;
// mc++             int yOffset = (layout_height - paddingTop - paddingBottom - circleDiameter) / 2 + paddingTop;
// mc++ 
// mc++             circleBounds =
// mc++                     new RectF(xOffset + barWidth, yOffset + barWidth, xOffset + circleDiameter - barWidth,
// mc++                             yOffset + circleDiameter - barWidth);
// mc++         } else {
// mc++             circleBounds = new RectF(paddingLeft + barWidth, paddingTop + barWidth,
// mc++                     layout_width - paddingRight - barWidth, layout_height - paddingBottom - barWidth);
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Parse the attributes passed to the view from the XML
// mc++      *
// mc++      * @param a the attributes to parse
// mc++      */
// mc++     private void parseAttributes(TypedArray a) {
// mc++         // We transform the default values from DIP to pixels
// mc++         DisplayMetrics metrics = getContext().getResources().getDisplayMetrics();
// mc++         barWidth = (int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, barWidth, metrics);
// mc++         rimWidth = (int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, rimWidth, metrics);
// mc++         circleRadius =
// mc++                 (int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, circleRadius, metrics);
// mc++ 
// mc++         circleRadius =
// mc++                 (int) a.getDimension(R.styleable.ProgressWheel_matProg_circleRadius, circleRadius);
// mc++ 
// mc++         fillRadius = a.getBoolean(R.styleable.ProgressWheel_matProg_fillRadius, false);
// mc++ 
// mc++         barWidth = (int) a.getDimension(R.styleable.ProgressWheel_matProg_barWidth, barWidth);
// mc++ 
// mc++         rimWidth = (int) a.getDimension(R.styleable.ProgressWheel_matProg_rimWidth, rimWidth);
// mc++ 
// mc++         float baseSpinSpeed =
// mc++                 a.getFloat(R.styleable.ProgressWheel_matProg_spinSpeed, spinSpeed / 360.0f);
// mc++         spinSpeed = baseSpinSpeed * 360;
// mc++ 
// mc++         barSpinCycleTime =
// mc++                 a.getInt(R.styleable.ProgressWheel_matProg_barSpinCycleTime, (int) barSpinCycleTime);
// mc++ 
// mc++         barColor = a.getColor(R.styleable.ProgressWheel_matProg_barColor, barColor);
// mc++ 
// mc++         rimColor = a.getColor(R.styleable.ProgressWheel_matProg_rimColor, rimColor);
// mc++ 
// mc++         linearProgress = a.getBoolean(R.styleable.ProgressWheel_matProg_linearProgress, false);
// mc++ 
// mc++         if (a.getBoolean(R.styleable.ProgressWheel_matProg_progressIndeterminate, false)) {
// mc++             spin();
// mc++         }
// mc++ 
// mc++         // Recycle
// mc++         a.recycle();
// mc++     }
// mc++ 
// mc++     public void setCallback(ProgressCallback progressCallback) {
// mc++         callback = progressCallback;
// mc++ 
// mc++         if (!isSpinning) {
// mc++             runCallback();
// mc++         }
// mc++     }
// mc++ 
// mc++     //----------------------------------
// mc++     //Animation stuff
// mc++     //----------------------------------
// mc++ 
// mc++     protected void onDraw(Canvas canvas) {
// mc++         super.onDraw(canvas);
// mc++ 
// mc++         canvas.drawArc(circleBounds, 360, 360, false, rimPaint);
// mc++ 
// mc++         boolean mustInvalidate = false;
// mc++ 
// mc++         if (!shouldAnimate) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         if (isSpinning) {
// mc++             //Draw the spinning bar
// mc++             mustInvalidate = true;
// mc++ 
// mc++             long deltaTime = (SystemClock.uptimeMillis() - lastTimeAnimated);
// mc++             float deltaNormalized = deltaTime * spinSpeed / 1000.0f;
// mc++ 
// mc++             updateBarLength(deltaTime);
// mc++ 
// mc++             mProgress += deltaNormalized;
// mc++             if (mProgress > 360) {
// mc++                 mProgress -= 360f;
// mc++ 
// mc++                 // A full turn has been completed
// mc++                 // we run the callback with -1 in case we want to
// mc++                 // do something, like changing the color
// mc++                 runCallback(-1.0f);
// mc++             }
// mc++             lastTimeAnimated = SystemClock.uptimeMillis();
// mc++ 
// mc++             float from = mProgress - 90;
// mc++             float length = barLength + barExtraLength;
// mc++ 
// mc++             if (isInEditMode()) {
// mc++                 from = 0;
// mc++                 length = 135;
// mc++             }
// mc++ 
// mc++             canvas.drawArc(circleBounds, from, length, false, barPaint);
// mc++         } else {
// mc++             float oldProgress = mProgress;
// mc++ 
// mc++             if (mProgress != mTargetProgress) {
// mc++                 //We smoothly increase the progress bar
// mc++                 mustInvalidate = true;
// mc++ 
// mc++                 float deltaTime = (float) (SystemClock.uptimeMillis() - lastTimeAnimated) / 1000;
// mc++                 float deltaNormalized = deltaTime * spinSpeed;
// mc++ 
// mc++                 mProgress = Math.min(mProgress + deltaNormalized, mTargetProgress);
// mc++                 lastTimeAnimated = SystemClock.uptimeMillis();
// mc++             }
// mc++ 
// mc++             if (oldProgress != mProgress) {
// mc++                 runCallback();
// mc++             }
// mc++ 
// mc++             float offset = 0.0f;
// mc++             float progress = mProgress;
// mc++             if (!linearProgress) {
// mc++                 float factor = 2.0f;
// mc++                 offset = (float) (1.0f - Math.pow(1.0f - mProgress / 360.0f, 2.0f * factor)) * 360.0f;
// mc++                 progress = (float) (1.0f - Math.pow(1.0f - mProgress / 360.0f, factor)) * 360.0f;
// mc++             }
// mc++ 
// mc++             if (isInEditMode()) {
// mc++                 progress = 360;
// mc++             }
// mc++ 
// mc++             canvas.drawArc(circleBounds, offset - 90, progress, false, barPaint);
// mc++         }
// mc++ 
// mc++         if (mustInvalidate) {
// mc++             invalidate();
// mc++         }
// mc++     }
// mc++ 
// mc++     @Override protected void onVisibilityChanged(View changedView, int visibility) {
// mc++         super.onVisibilityChanged(changedView, visibility);
// mc++ 
// mc++         if (visibility == VISIBLE) {
// mc++             lastTimeAnimated = SystemClock.uptimeMillis();
// mc++         }
// mc++     }
// mc++ 
// mc++     private void updateBarLength(long deltaTimeInMilliSeconds) {
// mc++         if (pausedTimeWithoutGrowing >= pauseGrowingTime) {
// mc++             timeStartGrowing += deltaTimeInMilliSeconds;
// mc++ 
// mc++             if (timeStartGrowing > barSpinCycleTime) {
// mc++                 // We completed a size change cycle
// mc++                 // (growing or shrinking)
// mc++                 timeStartGrowing -= barSpinCycleTime;
// mc++                 //if(barGrowingFromFront) {
// mc++                 pausedTimeWithoutGrowing = 0;
// mc++                 //}
// mc++                 barGrowingFromFront = !barGrowingFromFront;
// mc++             }
// mc++ 
// mc++             float distance =
// mc++                     (float) Math.cos((timeStartGrowing / barSpinCycleTime + 1) * Math.PI) / 2 + 0.5f;
// mc++             float destLength = (barMaxLength - barLength);
// mc++ 
// mc++             if (barGrowingFromFront) {
// mc++                 barExtraLength = distance * destLength;
// mc++             } else {
// mc++                 float newLength = destLength * (1 - distance);
// mc++                 mProgress += (barExtraLength - newLength);
// mc++                 barExtraLength = newLength;
// mc++             }
// mc++         } else {
// mc++             pausedTimeWithoutGrowing += deltaTimeInMilliSeconds;
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Check if the wheel is currently spinning
// mc++      */
// mc++ 
// mc++     public boolean isSpinning() {
// mc++         return isSpinning;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Reset the count (in increment mode)
// mc++      */
// mc++     public void resetCount() {
// mc++         mProgress = 0.0f;
// mc++         mTargetProgress = 0.0f;
// mc++         invalidate();
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Turn off spin mode
// mc++      */
// mc++     public void stopSpinning() {
// mc++         isSpinning = false;
// mc++         mProgress = 0.0f;
// mc++         mTargetProgress = 0.0f;
// mc++         invalidate();
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Puts the view on spin mode
// mc++      */
// mc++     public void spin() {
// mc++         lastTimeAnimated = SystemClock.uptimeMillis();
// mc++         isSpinning = true;
// mc++         invalidate();
// mc++     }
// mc++ 
// mc++     private void runCallback(float value) {
// mc++         if (callback != null) {
// mc++             callback.onProgressUpdate(value);
// mc++         }
// mc++     }
// mc++ 
// mc++     private void runCallback() {
// mc++         if (callback != null) {
// mc++             float normalizedProgress = (float) Math.round(mProgress * 100 / 360.0f) / 100;
// mc++             callback.onProgressUpdate(normalizedProgress);
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Set the progress to a specific value,
// mc++      * the bar will be set instantly to that value
// mc++      *
// mc++      * @param progress the progress between 0 and 1
// mc++      */
// mc++     public void setInstantProgress(float progress) {
// mc++         if (isSpinning) {
// mc++             mProgress = 0.0f;
// mc++             isSpinning = false;
// mc++         }
// mc++ 
// mc++         if (progress > 1.0f) {
// mc++             progress -= 1.0f;
// mc++         } else if (progress < 0) {
// mc++             progress = 0;
// mc++         }
// mc++ 
// mc++         if (progress == mTargetProgress) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         mTargetProgress = Math.min(progress * 360.0f, 360.0f);
// mc++         mProgress = mTargetProgress;
// mc++         lastTimeAnimated = SystemClock.uptimeMillis();
// mc++         invalidate();
// mc++     }
// mc++ 
// mc++     // Great way to save a view's state http://stackoverflow.com/a/7089687/1991053
// mc++     @Override public Parcelable onSaveInstanceState() {
// mc++         Parcelable superState = super.onSaveInstanceState();
// mc++ 
// mc++         WheelSavedState ss = new WheelSavedState(superState);
// mc++ 
// mc++         // We save everything that can be changed at runtime
// mc++         ss.mProgress = this.mProgress;
// mc++         ss.mTargetProgress = this.mTargetProgress;
// mc++         ss.isSpinning = this.isSpinning;
// mc++         ss.spinSpeed = this.spinSpeed;
// mc++         ss.barWidth = this.barWidth;
// mc++         ss.barColor = this.barColor;
// mc++         ss.rimWidth = this.rimWidth;
// mc++         ss.rimColor = this.rimColor;
// mc++         ss.circleRadius = this.circleRadius;
// mc++         ss.linearProgress = this.linearProgress;
// mc++         ss.fillRadius = this.fillRadius;
// mc++ 
// mc++         return ss;
// mc++     }
// mc++ 
// mc++     @Override public void onRestoreInstanceState(Parcelable state) {
// mc++         if (!(state instanceof WheelSavedState)) {
// mc++             super.onRestoreInstanceState(state);
// mc++             return;
// mc++         }
// mc++ 
// mc++         WheelSavedState ss = (WheelSavedState) state;
// mc++         super.onRestoreInstanceState(ss.getSuperState());
// mc++ 
// mc++         this.mProgress = ss.mProgress;
// mc++         this.mTargetProgress = ss.mTargetProgress;
// mc++         this.isSpinning = ss.isSpinning;
// mc++         this.spinSpeed = ss.spinSpeed;
// mc++         this.barWidth = ss.barWidth;
// mc++         this.barColor = ss.barColor;
// mc++         this.rimWidth = ss.rimWidth;
// mc++         this.rimColor = ss.rimColor;
// mc++         this.circleRadius = ss.circleRadius;
// mc++         this.linearProgress = ss.linearProgress;
// mc++         this.fillRadius = ss.fillRadius;
// mc++ 
// mc++         this.lastTimeAnimated = SystemClock.uptimeMillis();
// mc++     }
// mc++ 
// mc++     /**
// mc++      * @return the current progress between 0.0 and 1.0,
// mc++      * if the wheel is indeterminate, then the result is -1
// mc++      */
// mc++     public float getProgress() {
// mc++         return isSpinning ? -1 : mProgress / 360.0f;
// mc++     }
// mc++ 
// mc++     //----------------------------------
// mc++     //Getters + setters
// mc++     //----------------------------------
// mc++ 
// mc++     /**
// mc++      * Set the progress to a specific value,
// mc++      * the bar will smoothly animate until that value
// mc++      *
// mc++      * @param progress the progress between 0 and 1
// mc++      */
// mc++     public void setProgress(float progress) {
// mc++         if (isSpinning) {
// mc++             mProgress = 0.0f;
// mc++             isSpinning = false;
// mc++ 
// mc++             runCallback();
// mc++         }
// mc++ 
// mc++         if (progress > 1.0f) {
// mc++             progress -= 1.0f;
// mc++         } else if (progress < 0) {
// mc++             progress = 0;
// mc++         }
// mc++ 
// mc++         if (progress == mTargetProgress) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         // If we are currently in the right position
// mc++         // we set again the last time animated so the
// mc++         // animation starts smooth from here
// mc++         if (mProgress == mTargetProgress) {
// mc++             lastTimeAnimated = SystemClock.uptimeMillis();
// mc++         }
// mc++ 
// mc++         mTargetProgress = Math.min(progress * 360.0f, 360.0f);
// mc++ 
// mc++         invalidate();
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Sets the determinate progress mode
// mc++      *
// mc++      * @param isLinear if the progress should increase linearly
// mc++      */
// mc++     public void setLinearProgress(boolean isLinear) {
// mc++         linearProgress = isLinear;
// mc++         if (!isSpinning) {
// mc++             invalidate();
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * @return the radius of the wheel in pixels
// mc++      */
// mc++     public int getCircleRadius() {
// mc++         return circleRadius;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Sets the radius of the wheel
// mc++      *
// mc++      * @param circleRadius the expected radius, in pixels
// mc++      */
// mc++     public void setCircleRadius(int circleRadius) {
// mc++         this.circleRadius = circleRadius;
// mc++         if (!isSpinning) {
// mc++             invalidate();
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * @return the width of the spinning bar
// mc++      */
// mc++     public int getBarWidth() {
// mc++         return barWidth;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Sets the width of the spinning bar
// mc++      *
// mc++      * @param barWidth the spinning bar width in pixels
// mc++      */
// mc++     public void setBarWidth(int barWidth) {
// mc++         this.barWidth = barWidth;
// mc++         if (!isSpinning) {
// mc++             invalidate();
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * @return the color of the spinning bar
// mc++      */
// mc++     public int getBarColor() {
// mc++         return barColor;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Sets the color of the spinning bar
// mc++      *
// mc++      * @param barColor The spinning bar color
// mc++      */
// mc++     public void setBarColor(int barColor) {
// mc++         this.barColor = barColor;
// mc++         setupPaints();
// mc++         if (!isSpinning) {
// mc++             invalidate();
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * @return the color of the wheel's contour
// mc++      */
// mc++     public int getRimColor() {
// mc++         return rimColor;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Sets the color of the wheel's contour
// mc++      *
// mc++      * @param rimColor the color for the wheel
// mc++      */
// mc++     public void setRimColor(int rimColor) {
// mc++         this.rimColor = rimColor;
// mc++         setupPaints();
// mc++         if (!isSpinning) {
// mc++             invalidate();
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * @return the base spinning speed, in full circle turns per second
// mc++      * (1.0 equals on full turn in one second), this value also is applied for
// mc++      * the smoothness when setting a progress
// mc++      */
// mc++     public float getSpinSpeed() {
// mc++         return spinSpeed / 360.0f;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Sets the base spinning speed, in full circle turns per second
// mc++      * (1.0 equals on full turn in one second), this value also is applied for
// mc++      * the smoothness when setting a progress
// mc++      *
// mc++      * @param spinSpeed the desired base speed in full turns per second
// mc++      */
// mc++     public void setSpinSpeed(float spinSpeed) {
// mc++         this.spinSpeed = spinSpeed * 360.0f;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * @return the width of the wheel's contour in pixels
// mc++      */
// mc++     public int getRimWidth() {
// mc++         return rimWidth;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * Sets the width of the wheel's contour
// mc++      *
// mc++      * @param rimWidth the width in pixels
// mc++      */
// mc++     public void setRimWidth(int rimWidth) {
// mc++         this.rimWidth = rimWidth;
// mc++         if (!isSpinning) {
// mc++             invalidate();
// mc++         }
// mc++     }
// mc++ 
// mc++     public interface ProgressCallback {
// mc++         /**
// mc++          * Method to call when the progress reaches a value
// mc++          * in order to avoid float precision issues, the progress
// mc++          * is rounded to a float with two decimals.
// mc++          *
// mc++          * In indeterminate mode, the callback is called each time
// mc++          * the wheel completes an animation cycle, with, the progress value is -1.0f
// mc++          *
// mc++          * @param progress a double value between 0.00 and 1.00 both included
// mc++          */
// mc++         public void onProgressUpdate(float progress);
// mc++     }
// mc++ 
// mc++     static class WheelSavedState extends BaseSavedState {
// mc++         //required field that makes Parcelables from a Parcel
// mc++         public static final Creator<WheelSavedState> CREATOR =
// mc++                 new Creator<WheelSavedState>() {
// mc++                     public WheelSavedState createFromParcel(Parcel in) {
// mc++                         return new WheelSavedState(in);
// mc++                     }
// mc++ 
// mc++                     public WheelSavedState[] newArray(int size) {
// mc++                         return new WheelSavedState[size];
// mc++                     }
// mc++                 };
// mc++         float mProgress;
// mc++         float mTargetProgress;
// mc++         boolean isSpinning;
// mc++         float spinSpeed;
// mc++         int barWidth;
// mc++         int barColor;
// mc++         int rimWidth;
// mc++         int rimColor;
// mc++         int circleRadius;
// mc++         boolean linearProgress;
// mc++         boolean fillRadius;
// mc++ 
// mc++         WheelSavedState(Parcelable superState) {
// mc++             super(superState);
// mc++         }
// mc++ 
// mc++         private WheelSavedState(Parcel in) {
// mc++             super(in);
// mc++             this.mProgress = in.readFloat();
// mc++             this.mTargetProgress = in.readFloat();
// mc++             this.isSpinning = in.readByte() != 0;
// mc++             this.spinSpeed = in.readFloat();
// mc++             this.barWidth = in.readInt();
// mc++             this.barColor = in.readInt();
// mc++             this.rimWidth = in.readInt();
// mc++             this.rimColor = in.readInt();
// mc++             this.circleRadius = in.readInt();
// mc++             this.linearProgress = in.readByte() != 0;
// mc++             this.fillRadius = in.readByte() != 0;
// mc++         }
// mc++ 
// mc++         @Override public void writeToParcel(Parcel out, int flags) {
// mc++             super.writeToParcel(out, flags);
// mc++             out.writeFloat(this.mProgress);
// mc++             out.writeFloat(this.mTargetProgress);
// mc++             out.writeByte((byte) (isSpinning ? 1 : 0));
// mc++             out.writeFloat(this.spinSpeed);
// mc++             out.writeInt(this.barWidth);
// mc++             out.writeInt(this.barColor);
// mc++             out.writeInt(this.rimWidth);
// mc++             out.writeInt(this.rimColor);
// mc++             out.writeInt(this.circleRadius);
// mc++             out.writeByte((byte) (linearProgress ? 1 : 0));
// mc++             out.writeByte((byte) (fillRadius ? 1 : 0));
// mc++         }
// mc++     }
// mc++ }
