// mc++ package com.xujun.contralayout.utils;
// mc++ 
// mc++ /**
// mc++  * Created by Domen、on 2016/4/28.
// mc++  */
// mc++ 
// mc++ import android.os.Environment;
// mc++ import android.text.TextUtils;
// mc++ import android.util.Log;
// mc++ 
// mc++ import java.io.BufferedWriter;
// mc++ import java.io.File;
// mc++ import java.io.FileOutputStream;
// mc++ import java.io.IOException;
// mc++ import java.io.OutputStreamWriter;
// mc++ import java.text.DateFormat;
// mc++ import java.text.SimpleDateFormat;
// mc++ import java.util.Date;
// mc++ import java.util.Formatter;
// mc++ import java.util.Locale;
// mc++ 
// mc++ /**
// mc++  * Log工具，类似android.util.Log。 tag自动产生，格式:
// mc++  * customTagPrefix:className.methodName(Line:lineNumber),
// mc++  * customTagPrefix为空时只输出：className.methodName(Line:lineNumber)。
// mc++  */
// mc++ public class LUtils {
// mc++ 
// mc++     public static String customTagPrefix = "xujun";  // 自定义Tag的前缀，可以是作者名
// mc++     public static boolean isSaveLog = false;    // 是否把保存日志到SD卡中
// mc++     public static final String LOG_PATH = Environment.getExternalStorageDirectory().getPath(); // SD卡中的根目录
// mc++ 
// mc++     private static DateFormat formatter = new SimpleDateFormat("yyyy-MM-dd", Locale.SIMPLIFIED_CHINESE);
// mc++ 
// mc++     private LUtils() {
// mc++     }
// mc++ 
// mc++     // 容许打印日志的类型，默认是true，设置为false则不打印
// mc++     public static boolean allowD = true;
// mc++     public static boolean allowE = true;
// mc++     public static boolean allowI = true;
// mc++     public static boolean allowV = true;
// mc++     public static boolean allowW = true;
// mc++     public static boolean allowWtf = true;
// mc++ 
// mc++     private static String generateTag(StackTraceElement caller) {
// mc++         String tag = "%s.%s(Line:%d)"; // 占位符
// mc++         String callerClazzName = caller.getClassName(); // 获取到类名
// mc++         callerClazzName = callerClazzName.substring(callerClazzName.lastIndexOf(".") + 1);
// mc++         tag = String.format(Locale.ENGLISH, tag, callerClazzName, caller.getMethodName(), caller.getLineNumber()); // 替换
// mc++         tag = TextUtils.isEmpty(customTagPrefix) ? tag : customTagPrefix + ":" + tag;
// mc++ 
// mc++         return tag;
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 自定义的logger
// mc++      */
// mc++     public static CustomLogger customLogger;
// mc++ 
// mc++     public interface CustomLogger {
// mc++         void d(String tag, String content);
// mc++         void d(String tag, String content, Throwable e);
// mc++ 
// mc++         void e(String tag, String content);
// mc++         void e(String tag, String content, Throwable e);
// mc++ 
// mc++         void i(String tag, String content);
// mc++         void i(String tag, String content, Throwable e);
// mc++ 
// mc++         void v(String tag, String content);
// mc++         void v(String tag, String content, Throwable e);
// mc++ 
// mc++         void w(String tag, String content);
// mc++         void w(String tag, String content, Throwable e);
// mc++         void w(String tag, Throwable tr);
// mc++ 
// mc++         void wtf(String tag, String content);
// mc++         void wtf(String tag, String content, Throwable e);
// mc++         void wtf(String tag, Throwable tr);
// mc++     }
// mc++ 
// mc++     public static void d(String content) {
// mc++         if (!allowD) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         StackTraceElement caller = getCallerStackTraceElement();
// mc++         String tag = generateTag(caller);
// mc++ 
// mc++         if (customLogger != null) {
// mc++             customLogger.d(tag, content);
// mc++         } else {
// mc++             Log.d(tag, content);
// mc++         }
// mc++     }
// mc++ 
// mc++     public static void d(String content, Throwable e) {
// mc++         if (!allowD) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         StackTraceElement caller = getCallerStackTraceElement();
// mc++         String tag = generateTag(caller);
// mc++ 
// mc++         if (customLogger != null) {
// mc++             customLogger.d(tag, content, e);
// mc++         } else {
// mc++             Log.d(tag, content, e);
// mc++         }
// mc++     }
// mc++ 
// mc++     public static void e(String content) {
// mc++         if (!allowE) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         StackTraceElement caller = getCallerStackTraceElement();
// mc++         String tag = generateTag(caller);
// mc++ 
// mc++         if (customLogger != null) {
// mc++             customLogger.e(tag, content);
// mc++         } else {
// mc++             Log.e(tag, content);
// mc++         }
// mc++         if (isSaveLog) {
// mc++             point(LOG_PATH, tag, content);
// mc++         }
// mc++     }
// mc++ 
// mc++     public static void e(Throwable e) {
// mc++         if (!allowE) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         StackTraceElement caller = getCallerStackTraceElement();
// mc++         String tag = generateTag(caller);
// mc++ 
// mc++         if (customLogger != null) {
// mc++             customLogger.e(tag, "error", e);
// mc++         } else {
// mc++             Log.e(tag, e.getMessage(), e);
// mc++         }
// mc++         if (isSaveLog) {
// mc++             point(LOG_PATH, tag, e.getMessage());
// mc++         }
// mc++     }
// mc++ 
// mc++     public static void e(String content, Throwable e) {
// mc++         if (!allowE) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         StackTraceElement caller = getCallerStackTraceElement();
// mc++         String tag = generateTag(caller);
// mc++ 
// mc++         if (customLogger != null) {
// mc++             customLogger.e(tag, content, e);
// mc++         } else {
// mc++             Log.e(tag, content, e);
// mc++         }
// mc++         if (isSaveLog) {
// mc++             point(LOG_PATH, tag, e.getMessage());
// mc++         }
// mc++     }
// mc++ 
// mc++     public static void i(String content) {
// mc++         if (!allowI) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         StackTraceElement caller = getCallerStackTraceElement();
// mc++         String tag = generateTag(caller);
// mc++ 
// mc++         if (customLogger != null) {
// mc++             customLogger.i(tag, content);
// mc++         } else {
// mc++             Log.i(tag, content);
// mc++         }
// mc++         if (isSaveLog) {
// mc++             point(LOG_PATH, tag, content);
// mc++         }
// mc++     }
// mc++ 
// mc++     public static void i(String content, Throwable e) {
// mc++         if (!allowI) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         StackTraceElement caller = getCallerStackTraceElement();
// mc++         String tag = generateTag(caller);
// mc++ 
// mc++         if (customLogger != null) {
// mc++             customLogger.i(tag, content, e);
// mc++         } else {
// mc++             Log.i(tag, content, e);
// mc++         }
// mc++         if (isSaveLog) {
// mc++             point(LOG_PATH, tag, content);
// mc++         }
// mc++     }
// mc++ 
// mc++     public static void v(String content) {
// mc++         if (!allowV) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         StackTraceElement caller = getCallerStackTraceElement();
// mc++         String tag = generateTag(caller);
// mc++ 
// mc++         if (customLogger != null) {
// mc++             customLogger.v(tag, content);
// mc++         } else {
// mc++             Log.v(tag, content);
// mc++         }
// mc++         if (isSaveLog) {
// mc++             point(LOG_PATH, tag, content);
// mc++         }
// mc++     }
// mc++ 
// mc++     public static void v(String content, Throwable e) {
// mc++         if (!allowV) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         StackTraceElement caller = getCallerStackTraceElement();
// mc++         String tag = generateTag(caller);
// mc++ 
// mc++         if (customLogger != null) {
// mc++             customLogger.v(tag, content, e);
// mc++         } else {
// mc++             Log.v(tag, content, e);
// mc++         }
// mc++         if (isSaveLog) {
// mc++             point(LOG_PATH, tag, content);
// mc++         }
// mc++     }
// mc++ 
// mc++     public static void w(String content) {
// mc++         if (!allowW) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         StackTraceElement caller = getCallerStackTraceElement();
// mc++         String tag = generateTag(caller);
// mc++ 
// mc++         if (customLogger != null) {
// mc++             customLogger.w(tag, content);
// mc++         } else {
// mc++             Log.w(tag, content);
// mc++         }
// mc++         if (isSaveLog) {
// mc++             point(LOG_PATH, tag, content);
// mc++         }
// mc++     }
// mc++ 
// mc++     public static void w(String content, Throwable e) {
// mc++         if (!allowW) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         StackTraceElement caller = getCallerStackTraceElement();
// mc++         String tag = generateTag(caller);
// mc++ 
// mc++         if (customLogger != null) {
// mc++             customLogger.w(tag, content, e);
// mc++         } else {
// mc++             Log.w(tag, content, e);
// mc++         }
// mc++         if (isSaveLog) {
// mc++             point(LOG_PATH, tag, content);
// mc++         }
// mc++     }
// mc++ 
// mc++     public static void w(Throwable e) {
// mc++         if (!allowW) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         StackTraceElement caller = getCallerStackTraceElement();
// mc++         String tag = generateTag(caller);
// mc++ 
// mc++         if (customLogger != null) {
// mc++             customLogger.w(tag, e);
// mc++         } else {
// mc++             Log.w(tag, e);
// mc++         }
// mc++         if (isSaveLog) {
// mc++             point(LOG_PATH, tag, e.toString());
// mc++         }
// mc++     }
// mc++ 
// mc++     public static void wtf(String content) {
// mc++         if (!allowWtf) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         StackTraceElement caller = getCallerStackTraceElement();
// mc++         String tag = generateTag(caller);
// mc++ 
// mc++         if (customLogger != null) {
// mc++             customLogger.wtf(tag, content);
// mc++         } else {
// mc++             Log.wtf(tag, content);
// mc++         }
// mc++         if (isSaveLog) {
// mc++             point(LOG_PATH, tag, content);
// mc++         }
// mc++     }
// mc++ 
// mc++     public static void wtf(String content, Throwable e) {
// mc++         if (!allowWtf) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         StackTraceElement caller = getCallerStackTraceElement();
// mc++         String tag = generateTag(caller);
// mc++ 
// mc++         if (customLogger != null) {
// mc++             customLogger.wtf(tag, content, e);
// mc++         } else {
// mc++             Log.wtf(tag, content, e);
// mc++         }
// mc++         if (isSaveLog) {
// mc++             point(LOG_PATH, tag, content);
// mc++         }
// mc++     }
// mc++ 
// mc++     public static void wtf(Throwable e) {
// mc++         if (!allowWtf) {
// mc++             return;
// mc++         }
// mc++ 
// mc++         StackTraceElement caller = getCallerStackTraceElement();
// mc++         String tag = generateTag(caller);
// mc++ 
// mc++         if (customLogger != null) {
// mc++             customLogger.wtf(tag, e);
// mc++         } else {
// mc++             Log.wtf(tag, e);
// mc++         }
// mc++         if (isSaveLog) {
// mc++             point(LOG_PATH, tag, e.toString());
// mc++         }
// mc++     }
// mc++ 
// mc++     private static StackTraceElement getCallerStackTraceElement() {
// mc++         return Thread.currentThread().getStackTrace()[4];
// mc++     }
// mc++ 
// mc++     public static void point(String path, String tag, String msg) {
// mc++         if (isSDAva()) {
// mc++             long timestamp = System.currentTimeMillis();
// mc++             String time = formatter.format(new Date());
// mc++             path = path + "/logs/log-" + time + "-" + timestamp + ".log";
// mc++ 
// mc++             File file = new File(path);
// mc++             if (!file.exists()) {
// mc++                 createDipPath(path);
// mc++             }
// mc++             BufferedWriter out = null;
// mc++             try {
// mc++                 out = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(file, true)));
// mc++                 out.write(time + " " + tag + " " + msg + "\r\n");
// mc++             } catch (Exception e) {
// mc++                 e.printStackTrace();
// mc++             } finally {
// mc++                 try {
// mc++                     if (out != null) {
// mc++                         out.close();
// mc++                     }
// mc++                 } catch (IOException e) {
// mc++                     e.printStackTrace();
// mc++                 }
// mc++             }
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 根据文件路径 递归创建文件
// mc++      */
// mc++     public static void createDipPath(String file) {
// mc++         String parentFile = file.substring(0, file.lastIndexOf("/"));
// mc++         File file1 = new File(file);
// mc++         File parent = new File(parentFile);
// mc++         if (!file1.exists()) {
// mc++             parent.mkdirs();
// mc++             try {
// mc++                 file1.createNewFile();
// mc++             } catch (IOException e) {
// mc++                 e.printStackTrace();
// mc++             }
// mc++        }
// mc++     }
// mc++ 
// mc++     private static class ReusableFormatter {
// mc++ 
// mc++         private Formatter formatter;
// mc++         private StringBuilder builder;
// mc++ 
// mc++         public ReusableFormatter() {
// mc++             builder = new StringBuilder();
// mc++             formatter = new Formatter(builder);
// mc++         }
// mc++ 
// mc++         public String format(String msg, Object... args) {
// mc++             formatter.format(msg, args);
// mc++             String s = builder.toString();
// mc++             builder.setLength(0);
// mc++             return s;
// mc++         }
// mc++     }
// mc++ 
// mc++     private static final ThreadLocal<ReusableFormatter> thread_local_formatter = new ThreadLocal<ReusableFormatter>() {
// mc++         protected ReusableFormatter initialValue() {
// mc++             return new ReusableFormatter();
// mc++         }
// mc++     };
// mc++ 
// mc++     public static String format(String msg, Object... args) {
// mc++         ReusableFormatter formatter = thread_local_formatter.get();
// mc++         return formatter.format(msg, args);
// mc++     }
// mc++ 
// mc++     private static boolean isSDAva() {
// mc++         return Environment.getExternalStorageState().equals(Environment.MEDIA_MOUNTED) || Environment.getExternalStorageDirectory().exists();
// mc++     }
// mc++ }
