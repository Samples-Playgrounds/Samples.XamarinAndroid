// mc++ package com.xujun.contralayout.utils;
// mc++ 
// mc++ import android.content.Context;
// mc++ 
// mc++ import com.orhanobut.logger.Logger;
// mc++ 
// mc++ import java.io.BufferedWriter;
// mc++ import java.io.File;
// mc++ import java.io.FileWriter;
// mc++ import java.io.IOException;
// mc++ 
// mc++ /**
// mc++  * <Pre>
// mc++  *     将日志文件输出在本地日志
// mc++  * </Pre>
// mc++  *
// mc++  * @author 刘阳
// mc++  * @version 1.0
// mc++  *          <p/>
// mc++  *          Create by 2016/3/21 11:49
// mc++  */
// mc++ public class WriteLogUtil {
// mc++ 
// mc++     private static final String TAG = "xujun";
// mc++ 
// mc++     public static String cacheDir = "";
// mc++     public static String PATH = cacheDir + "/Log";
// mc++     public static final String LOG_FILE_NAME = "log.txt";
// mc++ 
// mc++     /**
// mc++      * 是否写入日志文件
// mc++      */
// mc++     public static final boolean LOG_WRITE_TO_FILE = true;
// mc++ 
// mc++ 
// mc++     public static final boolean isIShow=true;
// mc++     public static final boolean isDShow=true;
// mc++     public static final boolean isWShow=true;
// mc++     public static final boolean isEShow=true;
// mc++ 
// mc++ 
// mc++     public static void init(Context context){
// mc++         Context applicationContext = context.getApplicationContext();
// mc++         if (applicationContext.getExternalCacheDir() != null && isExistSDCard()) {
// mc++             cacheDir = applicationContext.getExternalCacheDir().toString();
// mc++ 
// mc++         }
// mc++         else {
// mc++             cacheDir = applicationContext.getCacheDir().toString();
// mc++         }
// mc++         Logger.init(TAG);
// mc++         PATH = cacheDir + "/Log";
// mc++     }
// mc++ 
// mc++ 
// mc++     /**
// mc++      * 错误信息
// mc++      *
// mc++      * @param TAG
// mc++      * @param msg
// mc++      */
// mc++     public final static void e(String TAG, String msg) {
// mc++         if(isEShow){
// mc++             Logger.e(TAG, msg);
// mc++             if (LOG_WRITE_TO_FILE)
// mc++                 writeLogtoFile("e", TAG, msg);
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     public final static void e(String msg){
// mc++         e(TAG,msg);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 警告信息
// mc++      *
// mc++      * @param TAG
// mc++      * @param msg
// mc++      */
// mc++     public final static void w(String TAG, String msg) {
// mc++         if(!isWShow){
// mc++             return;
// mc++         }
// mc++         Logger.w(TAG, msg);
// mc++         if (LOG_WRITE_TO_FILE)
// mc++             writeLogtoFile("w", TAG, msg);
// mc++     }
// mc++ 
// mc++     public final static void w(String msg){
// mc++       w(TAG,msg);
// mc++     }
// mc++ 
// mc++ 
// mc++     /**
// mc++      * 调试信息
// mc++      *
// mc++      * @param TAG
// mc++      * @param msg
// mc++      */
// mc++     public final static void d(String TAG, String msg) {
// mc++         if(!isDShow){
// mc++             return;
// mc++         }
// mc++         Logger.d(TAG, msg);
// mc++         if (LOG_WRITE_TO_FILE)
// mc++             writeLogtoFile("d", TAG, msg);
// mc++     }
// mc++ 
// mc++     public final static void d(String msg){
// mc++        d(TAG,msg);
// mc++     }
// mc++ 
// mc++ 
// mc++     /**
// mc++      * 提示信息
// mc++      *
// mc++      * @param TAG
// mc++      * @param msg
// mc++      */
// mc++     public final static void i(String TAG, String msg) {
// mc++ 
// mc++ 
// mc++         if(!isIShow){
// mc++             return;
// mc++         }
// mc++         Logger.i(TAG, msg);
// mc++         if (LOG_WRITE_TO_FILE)
// mc++             writeLogtoFile("i", TAG, msg);
// mc++     }
// mc++ 
// mc++     public final static void i(String msg){
// mc++        i(TAG,msg);
// mc++     }
// mc++ 
// mc++ 
// mc++ 
// mc++     /**
// mc++      * 写入日志到文件中
// mc++      *
// mc++      * @param logType
// mc++      * @param tag
// mc++      * @param msg
// mc++      */
// mc++     private static void writeLogtoFile(String logType, String tag, String msg) {
// mc++         isExist(PATH);
// mc++         //isDel();
// mc++         String needWriteMessage = "\r\n"
// mc++                 + TimeUtil.getNowMDHMSTime()
// mc++                 + "\r\n"
// mc++                 + logType
// mc++                 + "    "
// mc++                 + tag
// mc++                 + "\r\n"
// mc++                 + msg;
// mc++         File file = new File(PATH, LOG_FILE_NAME);
// mc++         try {
// mc++             FileWriter filerWriter = new FileWriter(file, true);
// mc++             BufferedWriter bufWriter = new BufferedWriter(filerWriter);
// mc++             bufWriter.write(needWriteMessage);
// mc++             bufWriter.newLine();
// mc++             bufWriter.close();
// mc++             filerWriter.close();
// mc++         } catch (IOException e) {
// mc++             e.printStackTrace();
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 删除日志文件
// mc++      */
// mc++     public static void delFile() {
// mc++ 
// mc++         File file = new File(PATH, LOG_FILE_NAME);
// mc++         if (file.exists()) {
// mc++             file.delete();
// mc++         }
// mc++     }
// mc++ 
// mc++     /**
// mc++      * 判断文件夹是否存在,如果不存在则创建文件夹
// mc++      *
// mc++      * @param path
// mc++      */
// mc++     public static void isExist(String path) {
// mc++         File file = new File(path);
// mc++         if (!file.exists()) {
// mc++             file.mkdirs();
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     private static boolean isExistSDCard() {
// mc++         if (android.os.Environment.getExternalStorageState().equals(android.os.Environment.MEDIA_MOUNTED)) {
// mc++             return true;
// mc++         }
// mc++         else {
// mc++             return false;
// mc++         }
// mc++     }
// mc++ 
// mc++ }
