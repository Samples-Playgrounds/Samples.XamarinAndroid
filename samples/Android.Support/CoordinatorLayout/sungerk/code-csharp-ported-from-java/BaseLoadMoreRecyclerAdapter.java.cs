// mc++ package sunger.net.org.coordinatorlayoutdemos.refresh;
// mc++ 
// mc++ import android.support.v7.widget.RecyclerView;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ import android.widget.TextView;
// mc++ 
// mc++ import java.util.LinkedList;
// mc++ import java.util.List;
// mc++ 
// mc++ import sunger.net.org.coordinatorlayoutdemos.R;
// mc++ import sunger.net.org.coordinatorlayoutdemos.widget.ProgressWheel;
// mc++ 
// mc++ 
// mc++ /**
// mc++  * Created on 15/8/23.
// mc++  */
// mc++ public abstract class BaseLoadMoreRecyclerAdapter<T, VH extends RecyclerView.ViewHolder> extends RecyclerView.Adapter {
// mc++     public static final int TYPE_ITEM = 0;
// mc++     public static final int TYPE_FOOTER = 1;
// mc++     public static final int TYPE_HEADER = 2;
// mc++     private boolean hasHeader = false;
// mc++     private boolean hasFooter;
// mc++     private boolean hasMoreData;
// mc++ 
// mc++     private final List<T> mList = new LinkedList<T>();
// mc++ 
// mc++     public static class FooterViewHolder extends RecyclerView.ViewHolder {
// mc++         public final ProgressWheel mProgressView;
// mc++         public final TextView mTextView;
// mc++ 
// mc++         public FooterViewHolder(View view) {
// mc++             super(view);
// mc++             mProgressView = (ProgressWheel) view.findViewById(R.id.progress_view);
// mc++             mTextView = (TextView) view.findViewById(R.id.tv_content);
// mc++         }
// mc++     }
// mc++ 
// mc++ 
// mc++     public static class HeaderViewHolder extends RecyclerView.ViewHolder {
// mc++         public final TextView mTextView;
// mc++ 
// mc++         public HeaderViewHolder(View view) {
// mc++             super(view);
// mc++             mTextView = (TextView) view.findViewById(R.id.tv_content);
// mc++         }
// mc++     }
// mc++ 
// mc++     public abstract VH onCreateItemViewHolder(ViewGroup parent, int viewType);
// mc++ 
// mc++     @Override
// mc++     public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
// mc++         if (viewType == TYPE_FOOTER) {//底部 加载view
// mc++             View view = LayoutInflater.from(parent.getContext())
// mc++                     .inflate(R.layout.item_view_load_more, parent, false);
// mc++             return new FooterViewHolder(view);
// mc++         } else if (viewType == TYPE_HEADER) {
// mc++             View view = LayoutInflater.from(parent.getContext())
// mc++                     .inflate(R.layout.item_view_load_more, parent, false);
// mc++             return new HeaderViewHolder(view);
// mc++         } else {
// mc++             return onCreateItemViewHolder(parent, viewType);
// mc++         }
// mc++     }
// mc++ 
// mc++     public abstract void onBindItemViewHolder(final VH holder, int position);
// mc++ 
// mc++     @Override
// mc++     @SuppressWarnings("unchecked")
// mc++     public void onBindViewHolder(final RecyclerView.ViewHolder holder, int position) {
// mc++         if (holder instanceof FooterViewHolder) {
// mc++             //没有更多数据
// mc++             if (hasMoreData) {
// mc++                 ((FooterViewHolder) holder).mProgressView.setVisibility(View.VISIBLE);
// mc++                 ((FooterViewHolder) holder).mTextView.setText("正在加载。。。");
// mc++             } else {
// mc++                 ((FooterViewHolder) holder).mProgressView.setVisibility(View.GONE);
// mc++                 ((FooterViewHolder) holder).mTextView.setText("没有更多数据了。。。。");
// mc++             }
// mc++         } else if (holder instanceof HeaderViewHolder) {
// mc++             ((HeaderViewHolder) holder).mTextView.setText("正在加载。。。");
// mc++         } else {
// mc++             onBindItemViewHolder((VH) holder, position);
// mc++         }
// mc++     }
// mc++ 
// mc++ 
// mc++     @Override
// mc++     public int getItemViewType(int position) {
// mc++ 
// mc++         if (position == getBasicItemCount() && hasFooter) {
// mc++             return TYPE_FOOTER;
// mc++         }
// mc++         if (position == 0 && hasHeader)
// mc++             return TYPE_HEADER;
// mc++         return TYPE_ITEM;
// mc++     }
// mc++ 
// mc++     public List<T> getList() {
// mc++         return mList;
// mc++     }
// mc++ 
// mc++     public void appendToList(List<T> list) {
// mc++         if (list == null) {
// mc++             return;
// mc++         }
// mc++         mList.addAll(list);
// mc++     }
// mc++ 
// mc++     public void append(T t) {
// mc++         if (t == null) {
// mc++             return;
// mc++         }
// mc++         mList.add(t);
// mc++     }
// mc++ 
// mc++     public void appendToTop(T item) {
// mc++         if (item == null) {
// mc++             return;
// mc++         }
// mc++         mList.add(0, item);
// mc++     }
// mc++ 
// mc++     public void appendToTopList(List<T> list) {
// mc++         if (list == null) {
// mc++             return;
// mc++         }
// mc++         mList.addAll(0, list);
// mc++     }
// mc++ 
// mc++ 
// mc++     public void remove(int position) {
// mc++         if (position < mList.size() - 1 && position >= 0) {
// mc++             mList.remove(position);
// mc++         }
// mc++     }
// mc++ 
// mc++     public void clear() {
// mc++         mList.clear();
// mc++     }
// mc++ 
// mc++     private int getBasicItemCount() {
// mc++         return mList.size();
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public int getItemCount() {
// mc++         return getBasicItemCount() + (hasFooter ? 1 : 0) + (hasHeader ? 1 : 0);
// mc++     }
// mc++ 
// mc++     public T getItem(int position) {
// mc++         if (position > mList.size() - 1) {
// mc++             return null;
// mc++         }
// mc++         return mList.get(position);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public long getItemId(int position) {
// mc++         return position;
// mc++     }
// mc++ 
// mc++ 
// mc++     public void setHasFooter(boolean hasFooter) {
// mc++         if (this.hasFooter != hasFooter) {
// mc++             this.hasFooter = hasFooter;
// mc++             notifyDataSetChanged();
// mc++         }
// mc++     }
// mc++ 
// mc++     public void showHeader() {
// mc++         if (!hasHeader) {
// mc++             this.hasHeader = true;
// mc++             this.hasFooter = false;
// mc++             this.hasMoreData=true;
// mc++             notifyDataSetChanged();
// mc++         }
// mc++     }
// mc++ 
// mc++ 
// mc++     public void hideHeader() {
// mc++         if (hasHeader) {
// mc++             this.hasHeader = false;
// mc++             this.hasFooter = false;
// mc++             notifyDataSetChanged();
// mc++         }
// mc++     }
// mc++ 
// mc++     public void setHasMoreData(boolean isMoreData) {
// mc++         if (this.hasMoreData != isMoreData) {
// mc++             this.hasMoreData = isMoreData;
// mc++             notifyDataSetChanged();
// mc++         }
// mc++     }
// mc++ 
// mc++     public void setHasMoreDataAndFooter(boolean hasMoreData, boolean hasFooter) {
// mc++         if (this.hasMoreData != hasMoreData || this.hasFooter != hasFooter) {
// mc++             this.hasMoreData = hasMoreData;
// mc++             this.hasFooter = hasFooter;
// mc++             notifyDataSetChanged();
// mc++         }
// mc++     }
// mc++ }
