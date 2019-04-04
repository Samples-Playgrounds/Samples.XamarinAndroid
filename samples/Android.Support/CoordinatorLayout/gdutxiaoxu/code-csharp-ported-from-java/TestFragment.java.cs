// mc++ package github.hellocsl.ucmainpager;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.os.Bundle;
// mc++ import android.os.Handler;
// mc++ import android.support.annotation.Nullable;
// mc++ import android.support.design.widget.Snackbar;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v4.widget.SwipeRefreshLayout;
// mc++ import android.support.v7.widget.DefaultItemAnimator;
// mc++ import android.support.v7.widget.LinearLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ import android.widget.Toast;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ 
// mc++ import github.hellocsl.ucmainpager.adapter.RecyclerViewAdapter;
// mc++ 
// mc++ 
// mc++ /**
// mc++  * Created by HelloCsl(cslgogogo@gmail.com) on 2016/3/1 0001.
// mc++  */
// mc++ public class TestFragment extends Fragment implements RecyclerViewAdapter.OnItemClickListener {
// mc++     private RecyclerView mRecyclerView;
// mc++     private SwipeRefreshLayout mSwipeRefreshLayout;
// mc++     private final static String KEY = "key";
// mc++     private final static String REFRESH_SUPPORT = "refresh_support";
// mc++     private Context mContext;
// mc++ 
// mc++     public static TestFragment newInstance(String desc, boolean refreshSupport) {
// mc++         Bundle args = new Bundle();
// mc++         args.putString(KEY, desc);
// mc++         args.putBoolean(REFRESH_SUPPORT, refreshSupport);
// mc++         TestFragment fragment = new TestFragment();
// mc++         fragment.setArguments(args);
// mc++         return fragment;
// mc++     }
// mc++ 
// mc++     public static TestFragment newInstance(String desc) {
// mc++         return newInstance(desc, true);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onCreate(@Nullable Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         mContext = getContext();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
// mc++         ViewGroup root = (ViewGroup) inflater.inflate(R.layout.fragment_test, container, false);
// mc++         initView(root);
// mc++         initData();
// mc++         return root;
// mc++     }
// mc++ 
// mc++ 
// mc++     private void initView(ViewGroup root) {
// mc++         mRecyclerView = (RecyclerView) root.findViewById(R.id.test_recycler);
// mc++         mSwipeRefreshLayout = (SwipeRefreshLayout) root.findViewById(R.id.refresh_layout);
// mc++         mSwipeRefreshLayout.setEnabled(getArguments().getBoolean(REFRESH_SUPPORT));
// mc++         mRecyclerView.setLayoutManager(new LinearLayoutManager(getActivity(), LinearLayoutManager.VERTICAL, false));
// mc++         mRecyclerView.setItemAnimator(new DefaultItemAnimator());
// mc++         mSwipeRefreshLayout.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
// mc++             @Override
// mc++             public void onRefresh() {
// mc++                 new Handler().postDelayed(new Runnable() {
// mc++                     @Override
// mc++                     public void run() {
// mc++                         mSwipeRefreshLayout.setRefreshing(false);
// mc++                         Toast.makeText(mContext, "刷新完成", Toast.LENGTH_SHORT).show();
// mc++                     }
// mc++                 }, 2000);
// mc++             }
// mc++         });
// mc++     }
// mc++ 
// mc++     public void setRefreshEnable(boolean refreshEnable) {
// mc++         if (mSwipeRefreshLayout != null) {
// mc++             mSwipeRefreshLayout.setEnabled(refreshEnable);
// mc++         }
// mc++     }
// mc++ 
// mc++     private void initData() {
// mc++         String key = getArguments().getString(KEY, "default");
// mc++         ArrayList<String> res = new ArrayList<>();
// mc++         for (int i = 0; i < 30; i++) {
// mc++             res.add(key + ":Fragment item :" + i);
// mc++         }
// mc++         mRecyclerView.setAdapter(new RecyclerViewAdapter(res).setOnItemClickListener(this));
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onItemClick(View view, int position) {
// mc++         Snackbar.make(mRecyclerView, position + ":click", Snackbar.LENGTH_SHORT).show();
// mc++     }
// mc++ }
