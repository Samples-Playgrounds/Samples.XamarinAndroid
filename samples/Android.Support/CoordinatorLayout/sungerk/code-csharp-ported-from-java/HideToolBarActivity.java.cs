// mc++ package sunger.net.org.coordinatorlayoutdemos.activity;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.v7.widget.LinearLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ 
// mc++ import sunger.net.org.coordinatorlayoutdemos.R;
// mc++ import sunger.net.org.coordinatorlayoutdemos.adapter.RecyclerAdapter;
// mc++ 
// mc++ /**
// mc++  * Created by Administrator on 2015/12/17.
// mc++  */
// mc++ public class HideToolBarActivity extends  BaseCompatActivity {
// mc++     private ArrayList<String> stringArrayList;
// mc++     private RecyclerView recyclerView;
// mc++     private RecyclerAdapter adapter;
// mc++ 
// mc++     @SuppressWarnings("ConstantConditions")
// mc++     @Override
// mc++     protected void onCreate(Bundle savedInstanceState) {
// mc++         super.onCreate(savedInstanceState);
// mc++         setContentView(R.layout.activity_recycler_hidden_toolbar);
// mc++ 
// mc++         Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
// mc++         setSupportActionBar(toolbar);
// mc++         getSupportActionBar().setDisplayHomeAsUpEnabled(true);
// mc++ 
// mc++         recyclerView = (RecyclerView) findViewById(R.id.list);
// mc++         recyclerView.setHasFixedSize(true);
// mc++         LinearLayoutManager layoutManager = new LinearLayoutManager(this);
// mc++         recyclerView.setLayoutManager(layoutManager);
// mc++ 
// mc++         setData(); //adding data to array list
// mc++         adapter = new RecyclerAdapter(this, stringArrayList);
// mc++         recyclerView.setAdapter(adapter);
// mc++ 
// mc++     }
// mc++ 
// mc++     private void setData() {
// mc++         stringArrayList = new ArrayList<>();
// mc++ 
// mc++         for (int i = 0; i < 100; i++) {
// mc++             stringArrayList.add("Item " + (i + 1));
// mc++         }
// mc++     }
// mc++ 
// mc++ }
