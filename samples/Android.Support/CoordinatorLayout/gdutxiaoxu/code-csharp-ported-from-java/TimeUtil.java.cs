// mc++ package com.xujun.contralayout.utils;
// mc++ 
// mc++ import android.annotation.SuppressLint;
// mc++ 
// mc++ import java.text.SimpleDateFormat;
// mc++ import java.util.Date;
// mc++ 
// mc++ public class TimeUtil {
// mc++ 
// mc++ 	/**
// mc++ 	 * yyyy-MM-dd HH:mm:ss
// mc++ 	 * @return
// mc++ 	 */
// mc++ 	@SuppressLint("SimpleDateFormat")
// mc++ 	public static String getNowYMDHMSTime(){
// mc++ 		
// mc++ 		
// mc++ 		SimpleDateFormat mDateFormat = new SimpleDateFormat(
// mc++ 				"yyyy-MM-dd HH:mm:ss");
// mc++ 		String date = mDateFormat.format(new Date());
// mc++ 		return date;
// mc++ 	}
// mc++ 	/**
// mc++ 	 * MM-dd HH:mm:ss
// mc++ 	 * @return
// mc++ 	 */
// mc++ 	@SuppressLint("SimpleDateFormat")
// mc++ 	public static String getNowMDHMSTime(){
// mc++ 		
// mc++ 		SimpleDateFormat mDateFormat = new SimpleDateFormat(
// mc++ 				"MM-dd HH:mm:ss");
// mc++ 		String date = mDateFormat.format(new Date());
// mc++ 		return date;
// mc++ 	}
// mc++ 	/**
// mc++ 	 * MM-dd
// mc++ 	 * @return
// mc++ 	 */
// mc++ 	@SuppressLint("SimpleDateFormat")
// mc++ 	public static String getNowYMD(){
// mc++ 
// mc++ 		SimpleDateFormat mDateFormat = new SimpleDateFormat(
// mc++ 				"yyyy-MM-dd");
// mc++ 		String date = mDateFormat.format(new Date());
// mc++ 		return date;
// mc++ 	}
// mc++ 
// mc++ 	/**
// mc++ 	 * yyyy-MM-dd
// mc++ 	 * @param date
// mc++ 	 * @return
// mc++ 	 */
// mc++ 	@SuppressLint("SimpleDateFormat")
// mc++ 	public static String getYMD(Date date){
// mc++ 
// mc++ 		SimpleDateFormat mDateFormat = new SimpleDateFormat(
// mc++ 				"yyyy-MM-dd");
// mc++ 		String dateS = mDateFormat.format(date);
// mc++ 		return dateS;
// mc++ 	}
// mc++ }
