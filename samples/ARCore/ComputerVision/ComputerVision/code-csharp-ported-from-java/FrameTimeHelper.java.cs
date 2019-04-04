// mc++ package com.google.ar.core.examples.java.computervision;
// mc++ 
// mc++ import android.arch.lifecycle.DefaultLifecycleObserver;
// mc++ import android.arch.lifecycle.LifecycleOwner;
// mc++ import android.support.annotation.NonNull;
// mc++ 
// mc++ /** Helper to measure frame-to-frame timing and frame rate. */
// mc++ public class FrameTimeHelper implements DefaultLifecycleObserver {
// mc++ 
// mc++   // Number of milliseconds in one second.
// mc++   private static final float MILLISECONDS_PER_SECOND = 1000f;
// mc++ 
// mc++   // Rate by which smoothed frame rate should approach momentary frame rate.
// mc++   private static final float SMOOTHING_FACTOR = .03f;
// mc++ 
// mc++   // System time of last frame, or zero if no time has been recorded.
// mc++   private long previousFrameTime;
// mc++ 
// mc++   // Smoothed frame time, or zero if frame time has not yet been recorded.
// mc++   private float smoothedFrameTime;
// mc++ 
// mc++   @Override
// mc++   public void onResume(@NonNull LifecycleOwner owner) {
// mc++     // Reset timing data during initialization and after app pause.
// mc++     previousFrameTime = 0;
// mc++     smoothedFrameTime = 0f;
// mc++   }
// mc++ 
// mc++   /** Capture current frame timestamp and calcuate smoothed frame-to-frame time. */
// mc++   public void nextFrame() {
// mc++     long now = System.currentTimeMillis();
// mc++ 
// mc++     // Is nextFrame() being called for the first time?
// mc++     if (previousFrameTime == 0) {
// mc++       previousFrameTime = now;
// mc++ 
// mc++       // Unable to calculate frame time based on single timestamp.
// mc++       smoothedFrameTime = 0f;
// mc++       return;
// mc++     }
// mc++ 
// mc++     // Determine momentary frame-to-frame time.
// mc++     long frameTime = now - previousFrameTime;
// mc++ 
// mc++     // Use current frame time as previous frame time during next invocation.
// mc++     previousFrameTime = now;
// mc++ 
// mc++     // Is nextFrame() being called for the second time, in which case we have only one measurement.
// mc++     if (smoothedFrameTime == 0f) {
// mc++       smoothedFrameTime = frameTime;
// mc++       return;
// mc++     }
// mc++ 
// mc++     // In all subsequent calls to nextFrame(), calculated a smoothed frame rate.
// mc++     smoothedFrameTime += SMOOTHING_FACTOR * (frameTime - smoothedFrameTime);
// mc++   }
// mc++ 
// mc++   /** Determine smoothed frame-to-frame time. Returns zero if frame time cannot be determined. */
// mc++   public float getSmoothedFrameTime() {
// mc++     return smoothedFrameTime;
// mc++   }
// mc++ 
// mc++   /** Determine smoothed frame rate. Returns zero if frame rate cannot be determined. */
// mc++   public float getSmoothedFrameRate() {
// mc++     return smoothedFrameTime == 0f ? 0f : MILLISECONDS_PER_SECOND / smoothedFrameTime;
// mc++   }
// mc++ }
