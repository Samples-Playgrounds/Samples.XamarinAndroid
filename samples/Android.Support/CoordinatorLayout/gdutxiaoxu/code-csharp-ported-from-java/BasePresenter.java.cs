// mc++ package com.xujun.contralayout.base.mvp;
// mc++ 
// mc++ import java.lang.ref.SoftReference;
// mc++ 
// mc++ /**
// mc++  * @author mobile.xujun on 2017/3/14 17:59
// mc++  * @version 0.1
// mc++  */
// mc++ 
// mc++ @SuppressWarnings("rawtypes")
// mc++ public  class BasePresenter<V extends IBaseView> implements IBasePresenter {
// mc++ 
// mc++     protected final SoftReference<V> mReference;
// mc++ 
// mc++ 
// mc++     protected BasePresenter(V view) {
// mc++         mReference = new SoftReference<>(view);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onDestroy() {
// mc++         mReference.clear();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onCreate() {
// mc++ 
// mc++     }
// mc++ 
// mc++     public V getView(){
// mc++         return mReference.get();
// mc++     }
// mc++ }
