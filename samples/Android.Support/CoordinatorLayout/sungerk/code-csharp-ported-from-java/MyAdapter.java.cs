// mc++ package sunger.net.org.coordinatorlayoutdemos.adapter;
// mc++ 
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ import android.widget.TextView;
// mc++ 
// mc++ import java.util.ArrayList;
// mc++ 
// mc++ import sunger.net.org.coordinatorlayoutdemos.R;
// mc++ 
// mc++ /**
// mc++  * Created by jianghejie on 15/11/26.
// mc++  */
// mc++ public class MyAdapter extends RecyclerView.Adapter<MyAdapter.ViewHolder> {
// mc++     public ArrayList<String> datas = null;
// mc++ 
// mc++     public MyAdapter(ArrayList<String> datas) {
// mc++         this.datas = datas;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public ViewHolder onCreateViewHolder(ViewGroup viewGroup, int viewType) {
// mc++         View view = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.item, viewGroup, false);
// mc++         ViewHolder vh = new ViewHolder(view);
// mc++         return vh;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onBindViewHolder(ViewHolder viewHolder, int position) {
// mc++         viewHolder.mTextView.setText(datas.get(position));
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getItemCount() {
// mc++         return datas.size();
// mc++     }
// mc++ 
// mc++     public static class ViewHolder extends RecyclerView.ViewHolder {
// mc++         public TextView mTextView;
// mc++ 
// mc++         public ViewHolder(View view) {
// mc++             super(view);
// mc++             mTextView = (TextView) view.findViewById(R.id.text);
// mc++         }
// mc++     }
// mc++ }
