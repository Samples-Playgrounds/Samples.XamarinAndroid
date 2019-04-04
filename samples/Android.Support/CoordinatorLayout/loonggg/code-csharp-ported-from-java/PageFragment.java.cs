// mc++ package com.loonggg.coordinatorlayoutdemo;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.net.Uri;
// mc++ import android.os.Bundle;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v7.widget.LinearLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ import android.widget.ArrayAdapter;
// mc++ import android.widget.ListView;
// mc++ import android.widget.SimpleAdapter;
// mc++ import android.widget.TextView;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ import java.util.List;
// mc++ 
// mc++ 
// mc++ public class PageFragment extends Fragment {
// mc++     public static final String ARG_PAGE = "ARG_PAGE";
// mc++     private int mPage;
// mc++     private RecyclerView lv;
// mc++ 
// mc++     public static PageFragment newInstance(int page) {
// mc++         Bundle args = new Bundle();
// mc++         args.putInt(ARG_PAGE, page);
// mc++         PageFragment pageFragment = new PageFragment();
// mc++         pageFragment.setArguments(args);
// mc++         return pageFragment;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         mPage = getArguments().getInt(ARG_PAGE);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle
// mc++             savedInstanceState) {
// mc++         View view = inflater.inflate(R.layout.fragment_page, null);
// mc++         lv = (RecyclerView) view.findViewById(R.id.lv);
// mc++         // 创建一个线性布局管理器
// mc++ 
// mc++         LinearLayoutManager layoutManager = new LinearLayoutManager(getActivity());
// mc++ 
// mc++         // 设置布局管理器
// mc++ 
// mc++         lv.setLayoutManager(layoutManager);
// mc++ 
// mc++         List<String> list = new ArrayList<String>();
// mc++         for (int i = 0; i < 100; i++) {
// mc++             list.add(i + "");
// mc++         }
// mc++         lv.setAdapter(new MyAdapter(list));
// mc++         return view;
// mc++     }
// mc++ }
