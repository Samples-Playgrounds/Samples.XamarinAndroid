// mc++ package com.loonggg.coordinatorlayoutdemo;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.graphics.Bitmap;
// mc++ 
// mc++ /**
// mc++  * Created by loongggdroid on 2016/5/12.
// mc++  */
// mc++ public class BlurUtil {
// mc++     public static Bitmap fastblur(Context context, Bitmap sentBitmap, int radius) {
// mc++         Bitmap bitmap = sentBitmap.copy(sentBitmap.getConfig(), true);
// mc++         if (radius < 1) {
// mc++             return (null);
// mc++         }
// mc++         int w = bitmap.getWidth();
// mc++         int h = bitmap.getHeight();
// mc++         int[] pix = new int[w * h];
// mc++         bitmap.getPixels(pix, 0, w, 0, 0, w, h);
// mc++         int wm = w - 1;
// mc++         int hm = h - 1;
// mc++         int wh = w * h;
// mc++         int div = radius + radius + 1;
// mc++ 
// mc++         int r[] = new int[wh];
// mc++         int g[] = new int[wh];
// mc++         int b[] = new int[wh];
// mc++         int rsum, gsum, bsum, x, y, i, p, yp, yi, yw;
// mc++ 
// mc++         int vmin[] = new int[Math.max(w, h)];
// mc++         int divsum = (div + 1) >> 1;
// mc++         divsum *= divsum;
// mc++         int temp = 256 * divsum;
// mc++         int dv[] = new int[temp];
// mc++         for (i = 0; i < temp; i++) {
// mc++             dv[i] = (i / divsum);
// mc++         }
// mc++ 
// mc++         yw = yi = 0;
// mc++ 
// mc++         int[][] stack = new int[div][3];
// mc++ 
// mc++         int stackpointer;
// mc++ 
// mc++         int stackstart;
// mc++ 
// mc++         int[] sir;
// mc++ 
// mc++         int rbs;
// mc++ 
// mc++         int r1 = radius + 1;
// mc++ 
// mc++         int routsum, goutsum, boutsum;
// mc++ 
// mc++         int rinsum, ginsum, binsum;
// mc++ 
// mc++         for (y = 0; y < h; y++) {
// mc++ 
// mc++             rinsum = ginsum = binsum = routsum = goutsum = boutsum = rsum = gsum = bsum = 0;
// mc++ 
// mc++             for (i = -radius; i <= radius; i++) {
// mc++ 
// mc++                 p = pix[yi + Math.min(wm, Math.max(i, 0))];
// mc++ 
// mc++                 sir = stack[i + radius];
// mc++ 
// mc++                 sir[0] = (p & 0xff0000) >> 16;
// mc++ 
// mc++                 sir[1] = (p & 0x00ff00) >> 8;
// mc++ 
// mc++                 sir[2] = (p & 0x0000ff);
// mc++ 
// mc++                 rbs = r1 - Math.abs(i);
// mc++ 
// mc++                 rsum += sir[0] * rbs;
// mc++ 
// mc++                 gsum += sir[1] * rbs;
// mc++ 
// mc++                 bsum += sir[2] * rbs;
// mc++ 
// mc++                 if (i > 0) {
// mc++ 
// mc++                     rinsum += sir[0];
// mc++ 
// mc++                     ginsum += sir[1];
// mc++ 
// mc++                     binsum += sir[2];
// mc++ 
// mc++                 } else {
// mc++ 
// mc++                     routsum += sir[0];
// mc++ 
// mc++                     goutsum += sir[1];
// mc++ 
// mc++                     boutsum += sir[2];
// mc++ 
// mc++                 }
// mc++ 
// mc++             }
// mc++ 
// mc++             stackpointer = radius;
// mc++ 
// mc++ 
// mc++             for (x = 0; x < w; x++) {
// mc++ 
// mc++ 
// mc++                 r[yi] = dv[rsum];
// mc++ 
// mc++                 g[yi] = dv[gsum];
// mc++ 
// mc++                 b[yi] = dv[bsum];
// mc++ 
// mc++                 rsum -= routsum;
// mc++ 
// mc++                 gsum -= goutsum;
// mc++ 
// mc++                 bsum -= boutsum;
// mc++ 
// mc++                 stackstart = stackpointer - radius + div;
// mc++ 
// mc++                 sir = stack[stackstart % div];
// mc++ 
// mc++                 routsum -= sir[0];
// mc++ 
// mc++                 goutsum -= sir[1];
// mc++ 
// mc++                 boutsum -= sir[2];
// mc++ 
// mc++                 if (y == 0) {
// mc++ 
// mc++                     vmin[x] = Math.min(x + radius + 1, wm);
// mc++ 
// mc++                 }
// mc++ 
// mc++                 p = pix[yw + vmin[x]];
// mc++ 
// mc++                 sir[0] = (p & 0xff0000) >> 16;
// mc++ 
// mc++                 sir[1] = (p & 0x00ff00) >> 8;
// mc++ 
// mc++                 sir[2] = (p & 0x0000ff);
// mc++ 
// mc++                 rinsum += sir[0];
// mc++ 
// mc++                 ginsum += sir[1];
// mc++ 
// mc++                 binsum += sir[2];
// mc++ 
// mc++                 rsum += rinsum;
// mc++ 
// mc++                 gsum += ginsum;
// mc++ 
// mc++                 bsum += binsum;
// mc++ 
// mc++                 stackpointer = (stackpointer + 1) % div;
// mc++ 
// mc++                 sir = stack[(stackpointer) % div];
// mc++ 
// mc++                 routsum += sir[0];
// mc++ 
// mc++                 goutsum += sir[1];
// mc++ 
// mc++                 boutsum += sir[2];
// mc++ 
// mc++                 rinsum -= sir[0];
// mc++ 
// mc++                 ginsum -= sir[1];
// mc++ 
// mc++                 binsum -= sir[2];
// mc++ 
// mc++                 yi++;
// mc++ 
// mc++             }
// mc++ 
// mc++             yw += w;
// mc++ 
// mc++         }
// mc++ 
// mc++         for (x = 0; x < w; x++) {
// mc++ 
// mc++             rinsum = ginsum = binsum = routsum = goutsum = boutsum = rsum = gsum = bsum = 0;
// mc++ 
// mc++             yp = -radius * w;
// mc++ 
// mc++             for (i = -radius; i <= radius; i++) {
// mc++ 
// mc++                 yi = Math.max(0, yp) + x;
// mc++ 
// mc++ 
// mc++                 sir = stack[i + radius];
// mc++ 
// mc++ 
// mc++                 sir[0] = r[yi];
// mc++ 
// mc++                 sir[1] = g[yi];
// mc++ 
// mc++                 sir[2] = b[yi];
// mc++ 
// mc++ 
// mc++                 rbs = r1 - Math.abs(i);
// mc++ 
// mc++ 
// mc++                 rsum += r[yi] * rbs;
// mc++ 
// mc++                 gsum += g[yi] * rbs;
// mc++ 
// mc++                 bsum += b[yi] * rbs;
// mc++ 
// mc++ 
// mc++                 if (i > 0) {
// mc++ 
// mc++                     rinsum += sir[0];
// mc++ 
// mc++                     ginsum += sir[1];
// mc++ 
// mc++                     binsum += sir[2];
// mc++ 
// mc++                 } else {
// mc++ 
// mc++                     routsum += sir[0];
// mc++ 
// mc++                     goutsum += sir[1];
// mc++ 
// mc++                     boutsum += sir[2];
// mc++ 
// mc++                 }
// mc++ 
// mc++ 
// mc++                 if (i < hm) {
// mc++ 
// mc++                     yp += w;
// mc++ 
// mc++                 }
// mc++ 
// mc++             }
// mc++ 
// mc++             yi = x;
// mc++ 
// mc++             stackpointer = radius;
// mc++ 
// mc++             for (y = 0; y < h; y++) {
// mc++ 
// mc++                 pix[yi] = (0xff000000 & pix[yi]) | (dv[rsum] << 16)
// mc++ 
// mc++                         | (dv[gsum] << 8) | dv[bsum];
// mc++ 
// mc++ 
// mc++                 rsum -= routsum;
// mc++ 
// mc++                 gsum -= goutsum;
// mc++ 
// mc++                 bsum -= boutsum;
// mc++ 
// mc++ 
// mc++                 stackstart = stackpointer - radius + div;
// mc++ 
// mc++                 sir = stack[stackstart % div];
// mc++ 
// mc++ 
// mc++                 routsum -= sir[0];
// mc++ 
// mc++                 goutsum -= sir[1];
// mc++ 
// mc++                 boutsum -= sir[2];
// mc++ 
// mc++ 
// mc++                 if (x == 0) {
// mc++ 
// mc++                     vmin[y] = Math.min(y + r1, hm) * w;
// mc++ 
// mc++                 }
// mc++ 
// mc++                 p = x + vmin[y];
// mc++ 
// mc++ 
// mc++                 sir[0] = r[p];
// mc++ 
// mc++                 sir[1] = g[p];
// mc++ 
// mc++                 sir[2] = b[p];
// mc++ 
// mc++ 
// mc++                 rinsum += sir[0];
// mc++ 
// mc++                 ginsum += sir[1];
// mc++ 
// mc++                 binsum += sir[2];
// mc++ 
// mc++ 
// mc++                 rsum += rinsum;
// mc++ 
// mc++                 gsum += ginsum;
// mc++ 
// mc++                 bsum += binsum;
// mc++ 
// mc++ 
// mc++                 stackpointer = (stackpointer + 1) % div;
// mc++ 
// mc++                 sir = stack[stackpointer];
// mc++ 
// mc++ 
// mc++                 routsum += sir[0];
// mc++ 
// mc++                 goutsum += sir[1];
// mc++ 
// mc++                 boutsum += sir[2];
// mc++ 
// mc++ 
// mc++                 rinsum -= sir[0];
// mc++ 
// mc++                 ginsum -= sir[1];
// mc++ 
// mc++                 binsum -= sir[2];
// mc++ 
// mc++ 
// mc++                 yi += w;
// mc++ 
// mc++             }
// mc++ 
// mc++         }
// mc++ 
// mc++ 
// mc++         bitmap.setPixels(pix, 0, w, 0, 0, w, h);
// mc++ 
// mc++         return (bitmap);
// mc++ 
// mc++     }
// mc++ }
