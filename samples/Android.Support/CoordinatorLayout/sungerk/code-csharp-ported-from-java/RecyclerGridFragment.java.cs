// mc++ package sunger.net.org.coordinatorlayoutdemos.fragment;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.os.Handler;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v7.widget.GridLayoutManager;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ import com.jcodecraeer.xrecyclerview.ProgressStyle;
// mc++ import com.jcodecraeer.xrecyclerview.XRecyclerView;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ 
// mc++ import sunger.net.org.coordinatorlayoutdemos.R;
// mc++ import sunger.net.org.coordinatorlayoutdemos.adapter.MyAdapter;
// mc++ 
// mc++ /**
// mc++  * Created by sunger on 2015/12/15.
// mc++  */
// mc++ public class RecyclerGridFragment extends Fragment {
// mc++     private XRecyclerView mRecyclerView;
// mc++     private MyAdapter mAdapter;
// mc++     private ArrayList<String> listData;
// mc++     private int refreshTime = 0;
// mc++     private int times = 0;
// mc++     private View header;
// mc++ 
// mc++     @Override
// mc++     public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
// mc++         header = inflater.inflate(R.layout.recyclerview_header, container, false);
// mc++         return inflater.inflate(R.layout.fragment_recyclerview, container, false);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onViewCreated(View view, Bundle savedInstanceState) {
// mc++         super.onViewCreated(view, savedInstanceState);
// mc++         mRecyclerView = (XRecyclerView) getView().findViewById(R.id.recyclerview);
// mc++         GridLayoutManager layoutManager = new GridLayoutManager(getActivity(), 3);
// mc++ 
// mc++         mRecyclerView.setLayoutManager(layoutManager);
// mc++ 
// mc++         mRecyclerView.setRefreshProgressStyle(ProgressStyle.BallSpinFadeLoader);
// mc++         mRecyclerView.setLaodingMoreProgressStyle(ProgressStyle.BallRotate);
// mc++         mRecyclerView.setArrowImageView(R.drawable.iconfont_downgrey);
// mc++         mRecyclerView.addHeaderView(header);
// mc++ 
// mc++         mRecyclerView.setLoadingListener(new XRecyclerView.LoadingListener() {
// mc++             @Override
// mc++             public void onRefresh() {
// mc++                 refreshTime++;
// mc++                 times = 0;
// mc++                 new Handler().postDelayed(new Runnable() {
// mc++                     public void run() {
// mc++ 
// mc++                         listData.clear();
// mc++                         for (int i = 0; i < 20; i++) {
// mc++                             listData.add("item" + i + "after " + refreshTime + " times of refresh");
// mc++                         }
// mc++                         mAdapter.notifyDataSetChanged();
// mc++                         mRecyclerView.refreshComplete();
// mc++                     }
// mc++ 
// mc++                 }, 1000);            //refresh data here
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onLoadMore() {
// mc++                 if (times < 2) {
// mc++                     new Handler().postDelayed(new Runnable() {
// mc++                         public void run() {
// mc++                             mRecyclerView.loadMoreComplete();
// mc++                             for (int i = 0; i < 20; i++) {
// mc++                                 listData.add("item" + (i + listData.size()));
// mc++                             }
// mc++                             mAdapter.notifyDataSetChanged();
// mc++                             mRecyclerView.refreshComplete();
// mc++                         }
// mc++                     }, 1000);
// mc++                 } else {
// mc++                     new Handler().postDelayed(new Runnable() {
// mc++                         public void run() {
// mc++ 
// mc++                             mAdapter.notifyDataSetChanged();
// mc++                             mRecyclerView.loadMoreComplete();
// mc++                         }
// mc++                     }, 1000);
// mc++                 }
// mc++                 times++;
// mc++             }
// mc++         });
// mc++ 
// mc++         listData = new ArrayList<String>();
// mc++         for (int i = 0; i < 20; i++) {
// mc++             listData.add("item" + (i + listData.size()));
// mc++         }
// mc++         mAdapter = new MyAdapter(listData);
// mc++ 
// mc++         mRecyclerView.setAdapter(mAdapter);
// mc++     }
// mc++ }
