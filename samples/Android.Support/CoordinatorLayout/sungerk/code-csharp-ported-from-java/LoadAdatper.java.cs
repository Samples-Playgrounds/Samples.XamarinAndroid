// mc++ package sunger.net.org.coordinatorlayoutdemos.adapter;
// mc++ 
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ import android.widget.TextView;
// mc++ 
// mc++ import sunger.net.org.coordinatorlayoutdemos.R;
// mc++ import sunger.net.org.coordinatorlayoutdemos.refresh.BaseLoadMoreRecyclerAdapter;
// mc++ 
// mc++ /**
// mc++  * Created by sunger on 2015/12/17.
// mc++  */
// mc++ public class LoadAdatper extends BaseLoadMoreRecyclerAdapter<String, LoadAdatper.ViewHolder> {
// mc++ 
// mc++     public LoadAdatper( ) {
// mc++      }
// mc++ 
// mc++     @Override
// mc++     public ViewHolder onCreateItemViewHolder(ViewGroup parent, int viewType) {
// mc++         View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item, parent, false);
// mc++         return new ViewHolder(view);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onBindItemViewHolder(ViewHolder holder, int position) {
// mc++         try{
// mc++             holder.mTextView.setText(getItem(position));
// mc++         }catch (Exception e){
// mc++             e.printStackTrace();
// mc++         }
// mc++     }
// mc++ 
// mc++     public static class ViewHolder extends RecyclerView.ViewHolder {
// mc++         public TextView mTextView;
// mc++         public ViewHolder(View view) {
// mc++             super(view);
// mc++             mTextView = (TextView) view.findViewById(R.id.text);
// mc++         }
// mc++     }
// mc++ 
// mc++ }
