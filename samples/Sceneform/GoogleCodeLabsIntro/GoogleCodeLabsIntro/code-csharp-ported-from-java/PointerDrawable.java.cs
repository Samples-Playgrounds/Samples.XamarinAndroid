// mc++ /*
// mc++ Copyright 2018 Google LLC
// mc++ 
// mc++ Licensed under the Apache License, Version 2.0 (the "License");
// mc++ you may not use this file except in compliance with the License.
// mc++ You may obtain a copy of the License at
// mc++ 
// mc++     https://www.apache.org/licenses/LICENSE-2.0
// mc++ 
// mc++ Unless required by applicable law or agreed to in writing, software
// mc++ distributed under the License is distributed on an "AS IS" BASIS,
// mc++ WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// mc++ See the License for the specific language governing permissions and
// mc++ limitations under the License.
// mc++ */
// mc++ package com.google.devrel.ar.codelab;
// mc++ 
// mc++ import android.graphics.Canvas;
// mc++ import android.graphics.Color;
// mc++ import android.graphics.ColorFilter;
// mc++ import android.graphics.Paint;
// mc++ import android.graphics.drawable.Drawable;
// mc++ import android.support.annotation.NonNull;
// mc++ import android.support.annotation.Nullable;
// mc++ 
// mc++ public class PointerDrawable extends Drawable {
// mc++     private final Paint paint = new Paint();
// mc++     private boolean enabled;
// mc++     @Override
// mc++     public void draw(@NonNull Canvas canvas) {
// mc++         float cx = canvas.getWidth()/2;
// mc++         float cy = canvas.getHeight()/2;
// mc++         if (enabled) {
// mc++             paint.setColor(Color.GREEN);
// mc++             canvas.drawCircle(cx, cy, 10, paint);
// mc++         }else {
// mc++             paint.setColor(Color.GRAY);
// mc++             canvas.drawText("X", cx, cy, paint);
// mc++         }
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void setAlpha(int i) {
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void setColorFilter(@Nullable ColorFilter colorFilter) {
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getOpacity() {
// mc++         return 0;
// mc++     }
// mc++     public boolean isEnabled() {
// mc++         return enabled;
// mc++     }
// mc++ 
// mc++     public void setEnabled(boolean enabled) {
// mc++         this.enabled = enabled;
// mc++     }
// mc++ }
