// mc++ package sunger.net.org.coordinatorlayoutdemos.fragment;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.annotation.Nullable;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ 
// mc++ import sunger.net.org.coordinatorlayoutdemos.R;
// mc++ import sunger.net.org.coordinatorlayoutdemos.adapter.RecyclerAdapter;
// mc++ 
// mc++ /**
// mc++  * Created by sunger on 2015/12/15.
// mc++  */
// mc++ public class NestedscrollFragment extends Fragment {
// mc++ 
// mc++     private ArrayList<String> stringArrayList;
// mc++     private RecyclerView recyclerView;
// mc++     private RecyclerAdapter adapter;
// mc++     @Nullable
// mc++     @Override
// mc++     public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
// mc++         View view = inflater.inflate(R.layout.fragment_nestedscroll, container, false);
// mc++         return view;
// mc++     }
// mc++ }
