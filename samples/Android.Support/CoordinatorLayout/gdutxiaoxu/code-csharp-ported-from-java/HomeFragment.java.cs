// mc++ package com.xujun.contralayout.UI.zhihu;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.widget.LinearLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.util.Log;
// mc++ import android.view.View;
// mc++ 
// mc++ import com.github.clans.fab.FloatingActionButton;
// mc++ import com.github.clans.fab.FloatingActionMenu;
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.adapter.ItemAdapter;
// mc++ import com.xujun.contralayout.base.BaseFragment;
// mc++ import com.xujun.contralayout.recyclerView.divider.DividerItemDecoration;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ import java.util.List;
// mc++ 
// mc++ /**
// mc++  * @author xujun  on 2016/12/3.
// mc++  * @email gdutxiaoxu@163.com
// mc++  */
// mc++ 
// mc++ public class HomeFragment extends BaseFragment {
// mc++ 
// mc++     private RecyclerView mRecyclerView;
// mc++     private FloatingActionMenu mMenu;
// mc++     private FloatingActionButton mMenuItemCollect;
// mc++     private FloatingActionButton mMenuItemComment;
// mc++     private FloatingActionButton mMenuItemStar;
// mc++ 
// mc++     private static final String KEY = "key";
// mc++     private String title = "测试";
// mc++     public static final String TAG = "xujun";
// mc++ 
// mc++     List<String> mDatas = new ArrayList<>();
// mc++     private ItemAdapter mAdapter;
// mc++     private RecyclerViewDisabler mRecyclerViewDisabler;
// mc++ 
// mc++     public static HomeFragment newInstance(String text) {
// mc++         HomeFragment homeFragment = new HomeFragment();
// mc++         Bundle bundle = new Bundle();
// mc++         if (text != null) {
// mc++             bundle.putString(KEY, text);
// mc++             homeFragment.setArguments(bundle);
// mc++         }
// mc++         return homeFragment;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void initView(View view) {
// mc++         mRecyclerView = (RecyclerView) mView.findViewById(R.id.recyclerView);
// mc++         mMenu = (FloatingActionMenu) mView.findViewById(R.id.menu);
// mc++         mMenuItemCollect = (FloatingActionButton) mView.findViewById(R.id.menu_item_collect);
// mc++         mMenuItemComment = (FloatingActionButton) mView.findViewById(R.id.menu_item_comment);
// mc++         mMenuItemStar = (FloatingActionButton) mView.findViewById(R.id.menu_item_star);
// mc++ 
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void initListener() {
// mc++         mMenu.setOnMenuToggleListener(new FloatingActionMenu.OnMenuToggleListener() {
// mc++             @Override
// mc++             public void onMenuToggle(boolean opened) {
// mc++                 if (opened) {
// mc++                     mRecyclerView.addOnItemTouchListener(mRecyclerViewDisabler);
// mc++                 } else {
// mc++                     mRecyclerView.removeOnItemTouchListener(mRecyclerViewDisabler);
// mc++                 }
// mc++             }
// mc++         });
// mc++         super.initListener();
// mc++         mRecyclerView.addOnScrollListener(new RecyclerView.OnScrollListener() {
// mc++             @Override
// mc++             public void onScrollStateChanged(RecyclerView recyclerView, int newState) {
// mc++                 super.onScrollStateChanged(recyclerView, newState);
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onScrolled(RecyclerView recyclerView, int dx, int dy) {
// mc++                 super.onScrolled(recyclerView, dx, dy);
// mc++                 if (isHandleScroll(dy)) {
// mc++                     // 表示是否打开菜单
// mc++                     boolean opened = mMenu.isOpened();
// mc++                     //   表示菜单Menu是否隐藏
// mc++                     boolean menuHidden = mMenu.isMenuHidden();
// mc++                     //  表示菜单menu Item是否隐藏
// mc++                     boolean menuButtonHidden = mMenu.isMenuButtonHidden();
// mc++                     Log.i(TAG, "onScrolled: opened=" + opened);
// mc++                     Log.i(TAG, "onScrolled: menuHidden=" + menuHidden);
// mc++                     Log.i(TAG, "onScrolled: menuButtonHidden=" + menuButtonHidden);
// mc++                     if (dy > 0) {//向下滑动
// mc++                         if (menuHidden == false) {
// mc++                             mMenu.hideMenu(true);
// mc++                         }
// mc++ 
// mc++                     } else {//想上滑动
// mc++                         if (menuHidden == true) {
// mc++                             mMenu.showMenu(true);
// mc++                         }
// mc++                     }
// mc++                 }
// mc++             }
// mc++         });
// mc++     }
// mc++ 
// mc++     private boolean isHandleScroll(int dy) {
// mc++         return Math.abs(dy) > 10;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected int getLayoutId() {
// mc++         return R.layout.fragment_home;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void initData() {
// mc++         super.initData();
// mc++ 
// mc++         mRecyclerViewDisabler = new RecyclerViewDisabler();
// mc++ 
// mc++         LinearLayoutManager layoutManager = new LinearLayoutManager(mContext);
// mc++         DividerItemDecoration itemDecoration = new DividerItemDecoration(mContext,
// mc++                 LinearLayoutManager.VERTICAL);
// mc++         mRecyclerView.addItemDecoration(itemDecoration);
// mc++         mRecyclerView.setLayoutManager(layoutManager);
// mc++ 
// mc++         for (int i = 0; i < 30; i++) {
// mc++             String s = String.format("我是第%d个" + title, i);
// mc++             mDatas.add(s);
// mc++         }
// mc++ 
// mc++         mAdapter = new ItemAdapter(mContext, mDatas);
// mc++         mRecyclerView.setAdapter(mAdapter);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void fetchData() {
// mc++ 
// mc++     }
// mc++ }
