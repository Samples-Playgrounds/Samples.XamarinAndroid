// mc++ package com.xujun.contralayout.adapter;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.widget.TextView;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.recyclerView.BaseRecyclerAdapter;
// mc++ import com.xujun.contralayout.recyclerView.BaseRecyclerHolder;
// mc++ 
// mc++ import java.util.List;
// mc++ 
// mc++ /**
// mc++  * @ explain:
// mc++  * @ author：xujun on 2016/10/18 16:42
// mc++  * @ email：gdutxiaoxu@163.com
// mc++  */
// mc++ public class ItemAdapter extends BaseRecyclerAdapter<String> {
// mc++ 
// mc++     public ItemAdapter(Context context,List<String> datas) {
// mc++         super(context, R.layout.item_string, datas);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void convert(BaseRecyclerHolder holder, String item, int position) {
// mc++         TextView  tv=holder.getView(R.id.tv);
// mc++         tv.setText(item);
// mc++ 
// mc++     }
// mc++ }
