// mc++ package github.hellocsl.ucmainpager.adapter;
// mc++ 
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ import android.widget.TextView;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ import github.hellocsl.ucmainpager.R;
// mc++ 
// mc++ 
// mc++ /**
// mc++  * Created by HelloCsl(cslgogogo@gmail.com) on 2016/2/29 0029.
// mc++  */
// mc++ public class RecyclerViewAdapter extends RecyclerView.Adapter<RecyclerViewAdapter.ViewHolder> implements View.OnClickListener {
// mc++ 
// mc++     private List<String> items;
// mc++     private OnItemClickListener mOnItemClickListener;
// mc++ 
// mc++     public RecyclerViewAdapter(List<String> items) {
// mc++         this.items = items;
// mc++     }
// mc++ 
// mc++     public RecyclerViewAdapter setOnItemClickListener(OnItemClickListener onItemClickListener) {
// mc++         this.mOnItemClickListener = onItemClickListener;
// mc++         return this;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
// mc++         View v = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_recycler, parent, false);
// mc++         v.setOnClickListener(this);
// mc++         return new ViewHolder(v);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onBindViewHolder(ViewHolder holder, int position) {
// mc++         String item = items.get(position);
// mc++         holder.text.setText(item);
// mc++         holder.itemView.setTag(position);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getItemCount() {
// mc++         return items.size();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onClick(final View v) {
// mc++         if (mOnItemClickListener != null) {
// mc++             mOnItemClickListener.onItemClick(v, (int) v.getTag());
// mc++         }
// mc++     }
// mc++ 
// mc++     protected static class ViewHolder extends RecyclerView.ViewHolder {
// mc++         public TextView text;
// mc++ 
// mc++         public ViewHolder(View itemView) {
// mc++             super(itemView);
// mc++             text = (TextView) itemView.findViewById(R.id.item_tv_title);
// mc++         }
// mc++     }
// mc++ 
// mc++     public interface OnItemClickListener {
// mc++ 
// mc++         void onItemClick(View view, int position);
// mc++ 
// mc++     }
// mc++ }
