// mc++ package com.xujun.contralayout.UI.zhihu;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.os.Bundle;
// mc++ import android.support.annotation.Nullable;
// mc++ import android.support.design.widget.CollapsingToolbarLayout;
// mc++ import android.support.design.widget.FloatingActionButton;
// mc++ import android.support.v4.app.Fragment;
// mc++ import android.view.LayoutInflater;
// mc++ import android.view.View;
// mc++ import android.view.ViewGroup;
// mc++ import android.widget.Toast;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ 
// mc++ import de.hdodenhof.circleimageview.CircleImageView;
// mc++ 
// mc++ 
// mc++ /**
// mc++  * Created by engineer on 2016/9/21.
// mc++  */
// mc++ 
// mc++ public class FourFragment extends Fragment {
// mc++     private View rootView;
// mc++     private Context mContext;
// mc++     private CollapsingToolbarLayout collapsing_toolbar;
// mc++     private FloatingActionButton fab;
// mc++     private static final String picUrl = "http://img1.imgtn.bdimg.com/it/u=3743691986,2983459167&fm=21&gp=0.jpg";
// mc++ 
// mc++     @Nullable
// mc++     @Override
// mc++     public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
// mc++         rootView = inflater.inflate(R.layout.fragment_four, container, false);
// mc++         InitView();
// mc++         return rootView;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onAttach(Context context) {
// mc++         super.onAttach(context);
// mc++         mContext = context;
// mc++     }
// mc++ 
// mc++     private void InitView() {
// mc++         collapsing_toolbar = (CollapsingToolbarLayout) rootView.findViewById(R.id.collapsing_toolbar);
// mc++         collapsing_toolbar.setTitle("个人中心");
// mc++         fab = (FloatingActionButton) rootView.findViewById(R.id.btn);
// mc++         fab.setOnClickListener(new View.OnClickListener() {
// mc++             @Override
// mc++             public void onClick(View view) {
// mc++                 Toast.makeText(getContext(), "编辑", Toast.LENGTH_SHORT).show();
// mc++             }
// mc++         });
// mc++         CircleImageView view = (CircleImageView) rootView.findViewById(R.id.headview);
// mc++ //        Glide.with(mContext).load(picUrl).placeholder(R.drawable.profile).into(view);
// mc++ 
// mc++     }
// mc++ }
