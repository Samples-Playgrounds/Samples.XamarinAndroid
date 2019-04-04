// mc++ package sunger.net.org.coordinatorlayoutdemos.fragment;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v7.widget.LinearLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ 
// mc++ import sunger.net.org.coordinatorlayoutdemos.R;
// mc++ import sunger.net.org.coordinatorlayoutdemos.adapter.LoadAdatper;
// mc++ import sunger.net.org.coordinatorlayoutdemos.refresh.OnRecycleViewScrollListener;
// mc++ 
// mc++ /**
// mc++  * Created by sunger on 15/12/16.
// mc++  */
// mc++ public class RecyclerFragment extends Fragment {
// mc++     private RecyclerView recyclerView;
// mc++     private LoadAdatper adapter;
// mc++ 
// mc++     @Override
// mc++     public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
// mc++         return inflater.inflate(R.layout.fragment_recyclerview2, container, false);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onViewCreated(View view, Bundle savedInstanceState) {
// mc++         super.onViewCreated(view, savedInstanceState);
// mc++         recyclerView = (RecyclerView) getView().findViewById(R.id.recyclerview);
// mc++         recyclerView.setHasFixedSize(true);
// mc++         LinearLayoutManager layoutManager = new LinearLayoutManager(getContext());
// mc++         recyclerView.setLayoutManager(layoutManager);
// mc++         adapter = new LoadAdatper();
// mc++         recyclerView.setAdapter(adapter);
// mc++         adapter.setHasMoreData(true);
// mc++         recyclerView.addOnScrollListener(new OnRecycleViewScrollListener() {
// mc++             @Override
// mc++             public void onLoadMore() {
// mc++                 adapter.setHasFooter(true);
// mc++                 recyclerView.scrollToPosition(adapter.getItemCount() - 1);
// mc++                 recyclerView.postDelayed(new Runnable() {
// mc++                     @Override
// mc++                     public void run() {
// mc++                         ArrayList<String> dataList = new ArrayList<String>();
// mc++                         for (int i = 0; i < 10; i++) {
// mc++                             dataList.add("Item " + (i + 1));
// mc++                         }
// mc++                         if (adapter.getItemCount() > 30) {
// mc++                             adapter.setHasMoreDataAndFooter(false, true);
// mc++                         } else {
// mc++                             adapter.appendToList(dataList);
// mc++                             adapter.setHasMoreDataAndFooter(true, true);
// mc++                             adapter.notifyDataSetChanged();
// mc++                         }
// mc++                     }
// mc++                 }, 2000);
// mc++             }
// mc++         });
// mc++         refresh();
// mc++     }
// mc++ 
// mc++ 
// mc++     public void refresh() {
// mc++         adapter.showHeader();
// mc++         recyclerView.postDelayed(new Runnable() {
// mc++             @Override
// mc++             public void run() {
// mc++                 ArrayList<String> dataList = new ArrayList<String>();
// mc++                 for (int i = 0; i < 10; i++) {
// mc++                     dataList.add("Item " + (i + 1));
// mc++                 }
// mc++                 adapter.getList().clear();
// mc++                 adapter.appendToList(dataList);
// mc++                 adapter.hideHeader();
// mc++             }
// mc++         }, 1000);
// mc++     }
// mc++ 
// mc++ 
// mc++ }
