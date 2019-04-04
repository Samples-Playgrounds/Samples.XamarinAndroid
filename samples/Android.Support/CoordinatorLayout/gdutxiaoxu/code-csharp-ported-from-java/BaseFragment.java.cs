// mc++ package com.xujun.contralayout.base;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.os.Bundle;
// mc++ import android.support.annotation.Nullable;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ import com.xujun.contralayout.utils.LUtils;
// mc++ 
// mc++ /**
// mc++  * @ explain:
// mc++  * @ author：xujun on 2016-8-11 16:16
// mc++  * @ email：gdutxiaoxu@163.com
// mc++  */
// mc++ public abstract class BaseFragment extends Fragment {
// mc++ 
// mc++     protected View mView;
// mc++ 
// mc++     /**
// mc++      * 表示View是否被初始化
// mc++      */
// mc++     protected boolean isViewInitiated;
// mc++     /**
// mc++      * 表示对用户是否可见
// mc++      */
// mc++     protected boolean isVisibleToUser;
// mc++     /**
// mc++      * 表示数据是否初始化
// mc++      */
// mc++     protected boolean isDataInitiated;
// mc++     protected Context mContext;
// mc++ 
// mc++     @Override
// mc++     public void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         LUtils.i(getClass().getSimpleName() + ">>>>>>>>>>>　　onCreate");
// mc++     }
// mc++ 
// mc++     @Nullable
// mc++     @Override
// mc++     public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable
// mc++             Bundle savedInstanceState) {
// mc++         LUtils.i(getClass().getSimpleName() + ">>>>>>>>>>>　　onCreateView");
// mc++         if (mView == null) {
// mc++             mContext = getContext();
// mc++             mView = View.inflate(mContext, getLayoutId(), null);
// mc++             initView(mView);
// mc++             LUtils.i(getClass().getSimpleName() + ">>>>>>>>>>>　　initView");
// mc++         } else {
// mc++             // 缓存的rootView需要判断是否已经被加过parent，如果有parent需要从parent删除，
// mc++             // 要不然会发生这个rootview已经有parent的错误。
// mc++             ViewGroup parent = (ViewGroup) mView.getParent();
// mc++             if (parent != null) {
// mc++                 parent.removeView(mView);
// mc++             }
// mc++             LUtils.i(getClass().getSimpleName() + ">>>>>>>>>>>　　removeView");
// mc++         }
// mc++         return mView;
// mc++     }
// mc++ 
// mc++     protected abstract void initView(View view);
// mc++ 
// mc++     protected abstract int getLayoutId();
// mc++ 
// mc++     @Override
// mc++     public void onActivityCreated(Bundle savedInstanceState) {
// mc++         super.onActivityCreated(savedInstanceState);
// mc++         LUtils.i(getClass().getSimpleName() + ">>>>>>>>>>>　　onActivityCreated");
// mc++         isViewInitiated = true;
// mc++         initListener();
// mc++         initData();
// mc++         prepareFetchData();
// mc++     }
// mc++ 
// mc++     protected void initListener() {
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void setUserVisibleHint(boolean isVisibleToUser) {
// mc++         super.setUserVisibleHint(isVisibleToUser);
// mc++         this.isVisibleToUser = isVisibleToUser;
// mc++         prepareFetchData();
// mc++     }
// mc++ 
// mc++     public abstract void fetchData();
// mc++ 
// mc++     public boolean prepareFetchData() {
// mc++         return prepareFetchData(false);
// mc++     }
// mc++ 
// mc++     /***
// mc++      *
// mc++      * @param forceUpdate 表示是否在界面可见的时候是否强制刷新数据
// mc++      * @return
// mc++      */
// mc++     public boolean prepareFetchData(boolean forceUpdate) {
// mc++         if (isVisibleToUser && isViewInitiated && (!isDataInitiated || forceUpdate)) {
// mc++             //  界面可见的时候再去加载数据
// mc++             fetchData();
// mc++             isDataInitiated = true;
// mc++             return true;
// mc++         }
// mc++         return false;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onDestroyView() {
// mc++         super.onDestroyView();
// mc++         LUtils.i(getClass().getSimpleName() + ">>>>>>>>>>>　　onDestroyView");
// mc++     }
// mc++ 
// mc++     protected void initData() {
// mc++     }
// mc++ 
// mc++ }
