// mc++ package sunger.net.org.coordinatorlayoutdemos.adapter;
// mc++ 
// mc++ import android.app.Activity;
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ import android.widget.TextView;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ public class RecyclerAdapter extends RecyclerView.Adapter<RecyclerAdapter.ViewHolder> {
// mc++ 
// mc++     private List<String> friends;
// mc++     private Activity activity;
// mc++ 
// mc++     public RecyclerAdapter(Activity activity, List<String> friends) {
// mc++         this.friends = friends;
// mc++         this.activity = activity;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public ViewHolder onCreateViewHolder(ViewGroup viewGroup, int viewType) {
// mc++ 
// mc++         //inflate your layout and pass it to view holder
// mc++         LayoutInflater inflater = activity.getLayoutInflater();
// mc++         View view = inflater.inflate(android.R.layout.simple_list_item_1, viewGroup, false);
// mc++         ViewHolder viewHolder = new ViewHolder(view);
// mc++ 
// mc++         return viewHolder;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onBindViewHolder(ViewHolder viewHolder, int position) {
// mc++ 
// mc++         viewHolder.item.setText(friends.get(position));
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getItemCount() {
// mc++         return (null != friends ? friends.size() : 0);
// mc++     }
// mc++ 
// mc++     /**
// mc++      * View holder to display each RecylerView item
// mc++      */
// mc++     protected class ViewHolder extends RecyclerView.ViewHolder {
// mc++         private TextView item;
// mc++ 
// mc++         public ViewHolder(View view) {
// mc++             super(view);
// mc++             item = (TextView) view.findViewById(android.R.id.text1);
// mc++         }
// mc++     }
// mc++ }
