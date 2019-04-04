// mc++ package com.xujun.contralayout.UI.bottomsheet;
// mc++ 
// mc++ import android.support.design.widget.BottomSheetBehavior;
// mc++ import android.support.design.widget.BottomSheetDialog;
// mc++ import android.view.View;
// mc++ 
// mc++ import com.xujun.contralayout.R;
// mc++ import com.xujun.contralayout.base.BaseMVPActivity;
// mc++ import com.xujun.contralayout.base.mvp.IBasePresenter;
// mc++ 
// mc++ /**
// mc++  * Created by Q.Jay on 2016/4/30 15:27
// mc++  *
// mc++  * @version 1.0.0
// mc++  */
// mc++ public class BottomSheetActivity extends BaseMVPActivity implements View.OnClickListener {
// mc++     private static final String TAG = "BottomSheetActivity";
// mc++     private BottomSheetBehavior<View> behavior;
// mc++ 
// mc++ 
// mc++ 
// mc++     @Override
// mc++     protected IBasePresenter setPresenter() {
// mc++         return null;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected int getContentViewLayoutID() {
// mc++         return R.layout.bottom_sheet_activity;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     protected void initView() {
// mc++         setOnClickListener(this, R.id.btnBehavior, R.id.btnDialog,R.id.btn_baidumap);
// mc++ 
// mc++         View bottomSheet = findViewById(R.id.bottom_sheet);
// mc++         if (bottomSheet != null) {
// mc++             behavior = BottomSheetBehavior.from(bottomSheet);
// mc++             behavior.setState(BottomSheetBehavior.STATE_COLLAPSED);
// mc++         }
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public void onClick(View v) {
// mc++         switch (v.getId()) {
// mc++             case R.id.btnBehavior:
// mc++                 int state = behavior.getState();
// mc++                 if (state == BottomSheetBehavior.STATE_EXPANDED) {
// mc++                     behavior.setState(BottomSheetBehavior.STATE_COLLAPSED);
// mc++                 } else if(state == BottomSheetBehavior.STATE_COLLAPSED){
// mc++                     behavior.setState(BottomSheetBehavior.STATE_HIDDEN);
// mc++                 }else if(state == BottomSheetBehavior.STATE_HIDDEN){
// mc++                     behavior.setState(BottomSheetBehavior.STATE_EXPANDED);
// mc++                 }
// mc++                 break;
// mc++             case R.id.btnDialog:
// mc++                 BottomSheetDialog bottomSheetDialog = new BottomSheetDialog(this);
// mc++                 bottomSheetDialog.setContentView(R.layout.include_bottom_sheet_layout);
// mc++                 bottomSheetDialog.show();
// mc++                 break;
// mc++             case R.id.btn_baidumap:
// mc++                 readyGo(BaiduMapSample.class);
// mc++                 break;
// mc++         }
// mc++     }
// mc++ }
