// mc++ package saulmm.myapplication;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.v7.widget.Toolbar;
// mc++ import android.util.AttributeSet;
// mc++ import android.view.View;
// mc++ 
// mc++ import de.hdodenhof.circleimageview.CircleImageView;
// mc++ 
// mc++ @SuppressWarnings("unused")
// mc++ public class AvatarImageBehavior extends CoordinatorLayout.Behavior<CircleImageView> {
// mc++ 
// mc++     private final static float MIN_AVATAR_PERCENTAGE_SIZE   = 0.3f;
// mc++     private final static int EXTRA_FINAL_AVATAR_PADDING     = 80;
// mc++ 
// mc++     private final static String TAG = "behavior";
// mc++     private final Context mContext;
// mc++     private float mAvatarMaxSize;
// mc++ 
// mc++     private float mFinalLeftAvatarPadding;
// mc++     private float mStartPosition;
// mc++     private int mStartXPosition;
// mc++     private float mStartToolbarPosition;
// mc++ 
// mc++     public AvatarImageBehavior(Context context, AttributeSet attrs) {
// mc++         mContext = context;
// mc++         init();
// mc++ 
// mc++         mFinalLeftAvatarPadding = context.getResources().getDimension(
// mc++             R.dimen.abc_action_bar_navigation_padding_start_material);
// mc++     }
// mc++ 
// mc++     private void init() {
// mc++         bindDimensions();
// mc++     }
// mc++ 
// mc++     private void bindDimensions() {
// mc++         mAvatarMaxSize = mContext.getResources().getDimension(R.dimen.image_width);
// mc++     }
// mc++ 
// mc++     private int mStartYPosition;
// mc++ 
// mc++     private int mFinalYPosition;
// mc++     private int finalHeight;
// mc++     private int mStartHeight;
// mc++     private int mFinalXPosition;
// mc++     @Override
// mc++     public boolean layoutDependsOn(CoordinatorLayout parent, CircleImageView child, View dependency) {
// mc++         return dependency instanceof Toolbar;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onDependentViewChanged(CoordinatorLayout parent, CircleImageView child, View dependency) {
// mc++ 
// mc++         // Called once
// mc++         if (mStartYPosition == 0)
// mc++             mStartYPosition = (int) (child.getY() + (child.getHeight() / 2));
// mc++ 
// mc++         if (mFinalYPosition == 0)
// mc++             mFinalYPosition = (dependency.getHeight() /2);
// mc++ 
// mc++         if (mStartHeight == 0)
// mc++             mStartHeight = child.getHeight();
// mc++ 
// mc++         if (finalHeight == 0)
// mc++             finalHeight = mContext.getResources().getDimensionPixelOffset(R.dimen.image_final_width);
// mc++ 
// mc++         if (mStartXPosition == 0)
// mc++             mStartXPosition = (int) (child.getX() + (child.getWidth() / 2));
// mc++ 
// mc++         if (mFinalXPosition == 0)
// mc++             mFinalXPosition = mContext.getResources().getDimensionPixelOffset(R.dimen.abc_action_bar_content_inset_material) + (finalHeight / 2);
// mc++ 
// mc++         if (mStartToolbarPosition == 0)
// mc++             mStartToolbarPosition = dependency.getY() + (dependency.getHeight()/2);
// mc++ 
// mc++         final int maxScrollDistance = (int) (mStartToolbarPosition - getStatusBarHeight());
// mc++         float expandedPercentageFactor = dependency.getY() / maxScrollDistance;
// mc++ 
// mc++         float distanceYToSubtract = ((mStartYPosition - mFinalYPosition)
// mc++             * (1f - expandedPercentageFactor)) + (child.getHeight()/2);
// mc++ 
// mc++         float distanceXToSubtract = ((mStartXPosition - mFinalXPosition)
// mc++             * (1f - expandedPercentageFactor)) + (child.getWidth()/2);
// mc++ 
// mc++         float heightToSubtract = ((mStartHeight - finalHeight) * (1f - expandedPercentageFactor));
// mc++ 
// mc++         child.setY(mStartYPosition - distanceYToSubtract);
// mc++         child.setX(mStartXPosition - distanceXToSubtract);
// mc++ 
// mc++         int proportionalAvatarSize = (int) (mAvatarMaxSize * (expandedPercentageFactor));
// mc++ 
// mc++         CoordinatorLayout.LayoutParams lp = (CoordinatorLayout.LayoutParams) child.getLayoutParams();
// mc++         lp.width = (int) (mStartHeight - heightToSubtract);
// mc++         lp.height = (int) (mStartHeight - heightToSubtract);
// mc++         child.setLayoutParams(lp);
// mc++         return true;
// mc++     }
// mc++ 
// mc++     public int getStatusBarHeight() {
// mc++         int result = 0;
// mc++         int resourceId = mContext.getResources().getIdentifier("status_bar_height", "dimen", "android");
// mc++ 
// mc++         if (resourceId > 0) {
// mc++             result = mContext.getResources().getDimensionPixelSize(resourceId);
// mc++         }
// mc++         return result;
// mc++     }
// mc++ }
