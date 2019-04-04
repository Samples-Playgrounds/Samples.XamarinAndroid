// mc++ package com.xujun.contralayout.UI.bottomsheet;
// mc++ 
// mc++ import java.io.IOException;
// mc++ import java.util.concurrent.TimeUnit;
// mc++ 
// mc++ import okhttp3.OkHttpClient;
// mc++ import okhttp3.Request;
// mc++ import okhttp3.Response;
// mc++ 
// mc++ /**
// mc++  * Created by 赵晨璞 on 2016/6/16.
// mc++  */
// mc++ public class MyOkhttp {
// mc++ 
// mc++     public static OkHttpClient client = new OkHttpClient();
// mc++ 
// mc++     public static String get(String url){
// mc++         try {
// mc++          client.newBuilder().connectTimeout(10000, TimeUnit.MILLISECONDS);
// mc++         Request request = new Request.Builder().url(url).build();
// mc++ 
// mc++         Response response = client.newCall(request).execute();
// mc++         if (response.isSuccessful()) {
// mc++             return response.body().string();
// mc++         } else {
// mc++             throw new IOException("Unexpected code " + response);
// mc++         }
// mc++         } catch (IOException e) {
// mc++             // TODO Auto-generated catch block
// mc++             e.printStackTrace();
// mc++         }
// mc++         return null;
// mc++     }
// mc++ 
// mc++ 
// mc++ }
