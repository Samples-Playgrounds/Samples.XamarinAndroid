// mc++ package com.example.zcp.coordinatorlayoutdemo.activity;
// mc++ 
// mc++ import android.os.AsyncTask;
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.design.widget.FloatingActionButton;
// mc++ import android.support.v4.widget.SwipeRefreshLayout;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.support.v7.widget.StaggeredGridLayoutManager;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.text.TextUtils;
// mc++ import android.util.TypedValue;
// mc++ import com.example.zcp.coordinatorlayoutdemo.ui.GridAdapter;
// mc++ import com.example.zcp.coordinatorlayoutdemo.net.Meizi;
// mc++ import com.example.zcp.coordinatorlayoutdemo.net.MyOkhttp;
// mc++ import com.example.zcp.coordinatorlayoutdemo.R;
// mc++ import com.example.zcp.coordinatorlayoutdemo.ui.StatusBarUtil;
// mc++ import com.google.gson.Gson;
// mc++ import com.google.gson.reflect.TypeToken;
// mc++ import org.json.JSONException;
// mc++ import org.json.JSONObject;
// mc++ import java.util.List;
// mc++ 
// mc++ public class MyBehaviorActivity extends AppCompatActivity {
// mc++ 
// mc++     private static RecyclerView recyclerview;
// mc++     private CoordinatorLayout coordinatorLayout;
// mc++     private GridAdapter mAdapter;
// mc++     private List<Meizi> meizis;
// mc++     private StaggeredGridLayoutManager mLayoutManager;
// mc++     private int lastVisibleItem ;//recyclerview最后显示的Item,用于判断recyclerview自动加载下一页
// mc++     private int page=1;
// mc++     private SwipeRefreshLayout swipeRefreshLayout;
// mc++     private FloatingActionButton fab;
// mc++ 
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_my_behavior_demo);
// mc++ 
// mc++         StatusBarUtil.setStatusBarColor(MyBehaviorActivity.this,R.color.colorPrimaryDark);//设置状态栏颜色
// mc++ 
// mc++         initView();
// mc++         setListener();
// mc++ 
// mc++         new GetData().execute("http://gank.io/api/data/福利/10/1");
// mc++ 
// mc++     }
// mc++ 
// mc++     private void initView(){
// mc++         Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
// mc++         setSupportActionBar(toolbar);
// mc++ 
// mc++         coordinatorLayout=(CoordinatorLayout)findViewById(R.id.behavior_demo_coordinatorLayout);
// mc++ 
// mc++         recyclerview=(RecyclerView)findViewById(R.id.behavior_demo_recycler);
// mc++         mLayoutManager=new StaggeredGridLayoutManager(2, StaggeredGridLayoutManager.VERTICAL);
// mc++         recyclerview.setLayoutManager(mLayoutManager);
// mc++ 
// mc++         swipeRefreshLayout=(SwipeRefreshLayout) findViewById(R.id.behavior_demo_swipe_refresh) ;
// mc++         swipeRefreshLayout.setProgressViewOffset(false, 0,  (int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, 24, getResources().getDisplayMetrics()));//调整下拉控件位置
// mc++ 
// mc++         fab=(FloatingActionButton) findViewById(R.id.fab);
// mc++     }
// mc++ 
// mc++ 
// mc++ 
// mc++     private void setListener(){
// mc++ 
// mc++         swipeRefreshLayout.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
// mc++             @Override
// mc++             public void onRefresh() {
// mc++                 page=1;
// mc++                 new GetData().execute("http://gank.io/api/data/福利/10/1");
// mc++             }
// mc++         });
// mc++ 
// mc++         //recyclerview滚动监听实现自动加载
// mc++         recyclerview.addOnScrollListener(new RecyclerView.OnScrollListener() {
// mc++             @Override
// mc++             public void onScrollStateChanged(RecyclerView recyclerView, int newState) {
// mc++                 super.onScrollStateChanged(recyclerView, newState);
// mc++ //                0：当前屏幕停止滚动；1时：屏幕在滚动 且 用户仍在触碰或手指还在屏幕上；2时：随用户的操作，屏幕上产生的惯性滑动；
// mc++                 if (newState == RecyclerView.SCROLL_STATE_IDLE
// mc++                         && lastVisibleItem +2>=mLayoutManager.getItemCount()) {
// mc++                     new GetData().execute("http://gank.io/api/data/福利/10/"+(++page));
// mc++                 }
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onScrolled(RecyclerView recyclerView, int dx, int dy) {
// mc++                 super.onScrolled(recyclerView, dx, dy);
// mc++                 int[] positions= mLayoutManager.findLastVisibleItemPositions(null);
// mc++                 lastVisibleItem = Math.max(positions[0],positions[1]);
// mc++             }
// mc++         });
// mc++     }
// mc++ 
// mc++     //获取图片列表数据
// mc++     private class GetData extends AsyncTask<String, Integer, String> {
// mc++         @Override
// mc++         protected void onPreExecute() {
// mc++             super.onPreExecute();
// mc++ 
// mc++             swipeRefreshLayout.setRefreshing(true);
// mc++         }
// mc++ 
// mc++         @Override
// mc++         protected String doInBackground(String... params) {
// mc++ 
// mc++             return MyOkhttp.get(params[0]);
// mc++         }
// mc++ 
// mc++         protected void onPostExecute(String result) {
// mc++             super.onPostExecute(result);
// mc++             if(!TextUtils.isEmpty(result)){
// mc++ 
// mc++                 JSONObject jsonObject;
// mc++                 Gson gson=new Gson();
// mc++                 String jsonData=null;
// mc++ 
// mc++                 try {
// mc++                     jsonObject = new JSONObject(result);
// mc++                     jsonData = jsonObject.getString("results");
// mc++                 } catch (JSONException e) {
// mc++                     e.printStackTrace();
// mc++                 }
// mc++                 if(meizis==null||meizis.size()==0){
// mc++                     meizis= gson.fromJson(jsonData, new TypeToken<List<Meizi>>() {}.getType());
// mc++                     Meizi pages=new Meizi();
// mc++                     pages.setPage(page);
// mc++                     meizis.add(pages);
// mc++                 }else{
// mc++                     List<Meizi> more= gson.fromJson(jsonData, new TypeToken<List<Meizi>>() {}.getType());
// mc++                     meizis.addAll(more);
// mc++                     Meizi pages=new Meizi();
// mc++                     pages.setPage(page);
// mc++                     meizis.add(pages);
// mc++                 }
// mc++ 
// mc++                 if(mAdapter==null){
// mc++                     recyclerview.setAdapter(mAdapter = new GridAdapter(MyBehaviorActivity.this,meizis));
// mc++ 
// mc++                 }else{
// mc++                     mAdapter.notifyDataSetChanged();
// mc++                 }
// mc++             }
// mc++             swipeRefreshLayout.setRefreshing(false);
// mc++         }
// mc++     }
// mc++ 
// mc++ }
