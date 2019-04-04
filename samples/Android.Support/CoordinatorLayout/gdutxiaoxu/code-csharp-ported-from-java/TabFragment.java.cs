// mc++ package com.zhy.stickynavlayout;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v7.widget.LinearLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ import com.zhy.base.adapter.ViewHolder;
// mc++ import com.zhy.base.adapter.recyclerview.CommonAdapter;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ import java.util.List;
// mc++ 
// mc++ public class TabFragment extends Fragment
// mc++ {
// mc++     public static final String TITLE = "title";
// mc++     private String mTitle = "Defaut Value";
// mc++     private RecyclerView mRecyclerView;
// mc++     // private TextView mTextView;
// mc++     private List<String> mDatas = new ArrayList<String>();
// mc++ 
// mc++     @Override
// mc++     public void onCreate(Bundle savedInstanceState)
// mc++     {
// mc++         super.onCreate(savedInstanceState);
// mc++         if (getArguments() != null)
// mc++         {
// mc++             mTitle = getArguments().getString(TITLE);
// mc++         }
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public View onCreateView(LayoutInflater inflater, ViewGroup container,
// mc++                              Bundle savedInstanceState)
// mc++     {
// mc++         View view = inflater.inflate(R.layout.fragment_tab, container, false);
// mc++         mRecyclerView = (RecyclerView) view
// mc++                 .findViewById(R.id.id_stickynavlayout_innerscrollview);
// mc++         mRecyclerView.setLayoutManager(new LinearLayoutManager(getActivity()));
// mc++         // mTextView = (TextView) view.findViewById(R.id.id_info);
// mc++         // mTextView.setText(mTitle);
// mc++         for (int i = 0; i < 50; i++)
// mc++         {
// mc++             mDatas.add(mTitle + " -> " + i);
// mc++         }
// mc++         mRecyclerView.setAdapter(new CommonAdapter<String>(getActivity(), R.layout.item, mDatas)
// mc++         {
// mc++             @Override
// mc++             public void convert(ViewHolder holder, String o)
// mc++             {
// mc++                 holder.setText(R.id.id_info, o);
// mc++             }
// mc++         });
// mc++ 
// mc++         return view;
// mc++ 
// mc++     }
// mc++ 
// mc++     public static TabFragment newInstance(String title)
// mc++     {
// mc++         TabFragment tabFragment = new TabFragment();
// mc++         Bundle bundle = new Bundle();
// mc++         bundle.putString(TITLE, title);
// mc++         tabFragment.setArguments(bundle);
// mc++         return tabFragment;
// mc++     }
// mc++ 
// mc++ }
