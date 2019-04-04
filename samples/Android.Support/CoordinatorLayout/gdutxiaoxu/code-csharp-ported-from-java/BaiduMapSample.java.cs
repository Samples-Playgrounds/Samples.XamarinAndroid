// mc++ package com.xujun.contralayout.UI.bottomsheet;
// mc++ 
// mc++ import android.os.AsyncTask;
// mc++ import android.os.Bundle;
// mc++ import android.support.annotation.NonNull;
// mc++ import android.support.design.widget.BottomSheetBehavior;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.v4.widget.SwipeRefreshLayout;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.support.v7.widget.StaggeredGridLayoutManager;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.text.TextUtils;
// mc++ import android.util.Log;
// mc++ import android.util.TypedValue;
// mc++ import android.view.View;
// mc++ import android.widget.ImageView;
// mc++ import android.widget.RelativeLayout;
// mc++ import android.widget.TextView;
// mc++ 
// mc++ import com.google.gson.Gson;
// mc++ import com.google.gson.reflect.TypeToken;
// mc++ import com.squareup.picasso.Picasso;
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.utils.StatusBarUtil;
// mc++ 
// mc++ import org.json.JSONException;
// mc++ import org.json.JSONObject;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ public class BaiduMapSample extends AppCompatActivity implements GridAdapter.OnRecyclerViewItemClickListener {
// mc++ 
// mc++     private static RecyclerView recyclerview;
// mc++     private CoordinatorLayout coordinatorLayout;
// mc++     private GridAdapter mAdapter;//recyclerView适配器
// mc++     private List<Meizi> meizis;
// mc++     private StaggeredGridLayoutManager mLayoutManager;
// mc++     private int lastVisibleItem ;//recyclerview最后显示的Item,用于判断recyclerview自动加载下一页
// mc++     private int page=1;
// mc++     private SwipeRefreshLayout swipeRefreshLayout;
// mc++     private RelativeLayout design_bottom_sheet,design_bottom_sheet_bar;
// mc++     private  BottomSheetBehavior behavior;
// mc++     private ImageView bottom_sheet_iv;
// mc++     private TextView bottom_sheet_tv;
// mc++ 
// mc++     public static  final String TAG="xujun";
// mc++ 
// mc++     /**
// mc++      * 标识初始化时是否修改了底栏高度
// mc++      */
// mc++     private boolean isSetBottomSheetHeight;
// mc++ 
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_bottom_sheet_demo);
// mc++ 
// mc++         StatusBarUtil.setStatusBarColor(BaiduMapSample.this,R.color.colorPrimaryDark);//设置状态栏颜色
// mc++ 
// mc++         initView();
// mc++         setListener();
// mc++ 
// mc++         new GetData().execute("http://gank.io/api/data/福利/10/1");//初始化数据
// mc++ 
// mc++     }
// mc++ 
// mc++     private void initView(){
// mc++         Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
// mc++         setSupportActionBar(toolbar);
// mc++ 
// mc++         coordinatorLayout=(CoordinatorLayout)findViewById(R.id.bottom_sheet_demo_coordinatorLayout);
// mc++ 
// mc++         recyclerview=(RecyclerView)findViewById(R.id.bottom_sheet_demo_recycler);
// mc++         mLayoutManager=new StaggeredGridLayoutManager(2, StaggeredGridLayoutManager.VERTICAL);
// mc++         recyclerview.setLayoutManager(mLayoutManager);
// mc++ 
// mc++         swipeRefreshLayout=(SwipeRefreshLayout) findViewById(R.id.bottom_sheet_demo_swipe_refresh) ;
// mc++         swipeRefreshLayout.setProgressViewOffset(false, 0,  (int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, 24, getResources().getDisplayMetrics()));//调整下拉控件位置
// mc++ 
// mc++         design_bottom_sheet_bar=(RelativeLayout) findViewById(R.id.design_bottom_sheet_bar);
// mc++ 
// mc++         design_bottom_sheet=(RelativeLayout) findViewById(R.id.design_bottom_sheet);
// mc++         bottom_sheet_iv=(ImageView) findViewById(R.id.bottom_sheet_iv);
// mc++         bottom_sheet_tv=(TextView) findViewById(R.id.bottom_sheet_tv);
// mc++ 
// mc++         behavior = BottomSheetBehavior.from(design_bottom_sheet);
// mc++ 
// mc++     }
// mc++ 
// mc++     public void onWindowFocusChanged(boolean hasFocus) {
// mc++         // TODO Auto-generated method stub
// mc++         super.onWindowFocusChanged(hasFocus);
// mc++ 
// mc++         //修改SetBottomSheet的高度 留出顶部工具栏的位置
// mc++         if(!isSetBottomSheetHeight){
// mc++             CoordinatorLayout.LayoutParams linearParams =(CoordinatorLayout.LayoutParams) design_bottom_sheet.getLayoutParams();
// mc++             linearParams.height=coordinatorLayout.getHeight()-design_bottom_sheet_bar.getHeight();
// mc++             design_bottom_sheet.setLayoutParams(linearParams);
// mc++             isSetBottomSheetHeight=true;
// mc++         }
// mc++ 
// mc++     }
// mc++ 
// mc++     private void setListener(){
// mc++ 
// mc++         //底栏状态改变的监听
// mc++         behavior.setBottomSheetCallback(new BottomSheetBehavior.BottomSheetCallback() {
// mc++             @Override
// mc++             public void onStateChanged(@NonNull View bottomSheet, int newState) {
// mc++                 if(newState!=BottomSheetBehavior.STATE_COLLAPSED&&bottom_sheet_tv.getVisibility()==View.VISIBLE){
// mc++                     bottom_sheet_tv.setVisibility(View.GONE);
// mc++                     bottom_sheet_iv.setVisibility(View.VISIBLE);
// mc++                     mAdapter.setOnItemClickListener(null);//底栏展开状态下屏蔽RecyclerView item的点击
// mc++                 }else if(newState==BottomSheetBehavior.STATE_COLLAPSED&&bottom_sheet_tv.getVisibility()==View.GONE){
// mc++                     bottom_sheet_tv.setVisibility(View.VISIBLE);
// mc++                     bottom_sheet_iv.setVisibility(View.GONE);
// mc++                     mAdapter.setOnItemClickListener(BaiduMapSample.this);//底栏折叠状态下恢复RecyclerView item的点击
// mc++                 }
// mc++             }
// mc++             @Override
// mc++             public void onSlide(@NonNull View bottomSheet, float slideOffset) {
// mc++ 
// mc++                 if(bottomSheet.getTop()<2*design_bottom_sheet_bar.getHeight()){
// mc++                     //设置底栏完全展开时，出现的顶部工具栏的动画
// mc++                     design_bottom_sheet_bar.setVisibility(View.VISIBLE);
// mc++                     design_bottom_sheet_bar.setAlpha(slideOffset);
// mc++                     design_bottom_sheet_bar.setTranslationY(bottomSheet.getTop()-2*design_bottom_sheet_bar.getHeight());
// mc++                 }
// mc++                 else{
// mc++                     design_bottom_sheet_bar.setVisibility(View.INVISIBLE);
// mc++                 }
// mc++             }
// mc++         });
// mc++ 
// mc++ 
// mc++ 
// mc++         design_bottom_sheet_bar.setOnClickListener(new View.OnClickListener() {
// mc++             @Override
// mc++             public void onClick(View v) {
// mc++                 Log.i(TAG, "onClick: =" );
// mc++ 
// mc++                 behavior.setState(BottomSheetBehavior.STATE_COLLAPSED);//点击顶部工具栏 将底栏变为折叠状态
// mc++             }
// mc++         });
// mc++ 
// mc++         swipeRefreshLayout.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
// mc++             @Override
// mc++             public void onRefresh() {
// mc++                 page=1;
// mc++                 new GetData().execute("http://gank.io/api/data/福利/10/1");
// mc++             }
// mc++         });
// mc++ 
// mc++         //recyclerView滑动监听
// mc++         recyclerview.addOnScrollListener(new RecyclerView.OnScrollListener() {
// mc++             @Override
// mc++             public void onScrollStateChanged(RecyclerView recyclerView, int newState) {
// mc++                 super.onScrollStateChanged(recyclerView, newState);
// mc++ 
// mc++                 if(behavior.getState()!=BottomSheetBehavior.STATE_COLLAPSED){
// mc++                     //recyclerview滚动时  如果BottomSheetBehavior不是折叠状态就置为折叠
// mc++                     behavior.setState(BottomSheetBehavior.STATE_COLLAPSED);
// mc++                 }
// mc++ 
// mc++                 //0：当前屏幕停止滚动；1时：屏幕在滚动 且 用户仍在触碰或手指还在屏幕上；2时：随用户的操作，屏幕上产生的惯性滑动；
// mc++                 //用来判断recyclerview自动加载
// mc++                 if (newState == RecyclerView.SCROLL_STATE_IDLE
// mc++                         && lastVisibleItem +2>=mLayoutManager.getItemCount()) {
// mc++                     new GetData().execute("http://gank.io/api/data/福利/10/"+(++page));
// mc++                 }
// mc++             }
// mc++ 
// mc++             @Override
// mc++             public void onScrolled(RecyclerView recyclerView, int dx, int dy) {
// mc++                 super.onScrolled(recyclerView, dx, dy);
// mc++                 //用来判断recyclerview自动加载
// mc++                 int[] positions= mLayoutManager.findLastVisibleItemPositions(null);
// mc++                 lastVisibleItem = Math.max(positions[0],positions[1]);
// mc++             }
// mc++         });
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onItemClick(View view) {
// mc++         //recyclerview item点击事件处理
// mc++ 
// mc++         int position=recyclerview.getChildAdapterPosition(view);
// mc++ 
// mc++ 
// mc++ 
// mc++         Picasso.with(BaiduMapSample.this).load(meizis.get(position).getUrl()).into(bottom_sheet_iv);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onItemLongClick(View view) {
// mc++ 
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
// mc++ 
// mc++                     Picasso.with(BaiduMapSample.this).load(meizis.get(0).getUrl()).into(bottom_sheet_iv);
// mc++                 }else{
// mc++                     List<Meizi> more= gson.fromJson(jsonData, new TypeToken<List<Meizi>>() {}.getType());
// mc++                     meizis.addAll(more);
// mc++                     Meizi pages=new Meizi();
// mc++                     pages.setPage(page);
// mc++                     meizis.add(pages);
// mc++                 }
// mc++ 
// mc++                 if(mAdapter==null){
// mc++                     recyclerview.setAdapter(mAdapter = new GridAdapter(BaiduMapSample.this,meizis));
// mc++                     mAdapter.setOnItemClickListener(BaiduMapSample.this);
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
