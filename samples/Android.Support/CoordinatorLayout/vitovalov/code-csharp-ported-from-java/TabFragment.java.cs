// mc++ package io.github.vitovalov.tabbedcoordinator;
// mc++ 
// mc++ import android.os.Bundle;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.support.v7.widget.LinearLayoutManager;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ 
// mc++ import java.util.*;
// mc++ 
// mc++ /**
// mc++  * Created by @vitovalov on 30/9/15.
// mc++  */
// mc++ public class TabFragment extends Fragment {
// mc++ 
// mc++     private ListAdapter mAdapter;
// mc++ 
// mc++     private String mItemData = "Lorem Ipsum is simply dummy text of the printing and "
// mc++             + "typesetting industry Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
// mc++ 
// mc++     @Override
// mc++     public View onCreateView(LayoutInflater inflater, ViewGroup container,
// mc++             Bundle savedInstanceState) {
// mc++         View view = inflater.inflate(R.layout.fragment, container, false);
// mc++ 
// mc++         RecyclerView recyclerView = (RecyclerView) view.findViewById(
// mc++                 R.id.fragment_list_rv);
// mc++ 
// mc++         LinearLayoutManager linearLayoutManager = new LinearLayoutManager(getActivity());
// mc++         recyclerView.setLayoutManager(linearLayoutManager);
// mc++         recyclerView.setHasFixedSize(true);
// mc++ 
// mc++         String[] listItems = mItemData.split(" ");
// mc++ 
// mc++         List<String> list = new ArrayList<String>();
// mc++         Collections.addAll(list, listItems);
// mc++ 
// mc++         mAdapter = new ListAdapter(list);
// mc++         recyclerView.setAdapter(mAdapter);
// mc++ 
// mc++         return view;
// mc++     }
// mc++ }
