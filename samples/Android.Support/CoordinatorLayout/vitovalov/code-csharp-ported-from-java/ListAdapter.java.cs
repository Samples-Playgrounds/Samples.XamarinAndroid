// mc++ package io.github.vitovalov.tabbedcoordinator;
// mc++ 
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ import android.widget.TextView;
// mc++ 
// mc++ import java.util.*;
// mc++ 
// mc++ /**
// mc++  * Created by @vitovalov on 30/9/15.
// mc++  */
// mc++ public class ListAdapter extends RecyclerView.Adapter<ListAdapter.MyViewHolder> {
// mc++ 
// mc++     List<String> mListData;
// mc++ 
// mc++     public ListAdapter(List<String> mListData) {
// mc++         this.mListData = mListData;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public MyViewHolder onCreateViewHolder(ViewGroup viewGroup, int i) {
// mc++         View view = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.list_item,
// mc++                 viewGroup, false);
// mc++         return new MyViewHolder(view);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onBindViewHolder(MyViewHolder myViewHolder, int i) {
// mc++         myViewHolder.title.setText(mListData.get(i));
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getItemCount() {
// mc++         return mListData == null ? 0 : mListData.size();
// mc++     }
// mc++ 
// mc++     class MyViewHolder extends RecyclerView.ViewHolder {
// mc++ 
// mc++         TextView title;
// mc++ 
// mc++         public MyViewHolder(View itemView) {
// mc++             super(itemView);
// mc++ 
// mc++             title = (TextView) itemView.findViewById(R.id.listitem_name);
// mc++         }
// mc++     }
// mc++ 
// mc++ }
// mc++ 
