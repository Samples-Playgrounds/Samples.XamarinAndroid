// mc++ package sunger.net.org.coordinatorlayoutdemos.behavior;
// mc++ 
// mc++ import android.content.Context;
// mc++ import android.support.design.widget.AppBarLayout;
// mc++ import android.support.design.widget.CoordinatorLayout;
// mc++ import android.support.design.widget.FloatingActionButton;
// mc++ import android.util.AttributeSet;
// mc++ import android.view.View;
// mc++ 
// mc++ import sunger.net.org.coordinatorlayoutdemos.utils.Utils;
// mc++ 
// mc++ /**
// mc++  * Created by sunger on 2015/12/15.
// mc++  */
// mc++ public class ScrollingFabBehavior extends CoordinatorLayout.Behavior<FloatingActionButton> {
// mc++     private int toolbarHeight;
// mc++ 
// mc++     public ScrollingFabBehavior(Context context, AttributeSet attrs) {
// mc++         super(context, attrs);
// mc++         this.toolbarHeight = Utils.getToolbarHeight(context);
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean layoutDependsOn(CoordinatorLayout parent, FloatingActionButton fab, View dependency) {
// mc++         return dependency instanceof AppBarLayout;
// mc++     }
// mc++ 
// mc++     @Override
// mc++     public boolean onDependentViewChanged(CoordinatorLayout parent, FloatingActionButton fab, View dependency) {
// mc++         if (dependency instanceof AppBarLayout) {
// mc++             CoordinatorLayout.LayoutParams lp = (CoordinatorLayout.LayoutParams) fab.getLayoutParams();
// mc++             int fabBottomMargin = lp.bottomMargin;
// mc++             int distanceToScroll = fab.getHeight() + fabBottomMargin;
// mc++             float ratio = (float)dependency.getY()/(float)toolbarHeight;
// mc++             fab.setTranslationY(-distanceToScroll * ratio);
// mc++         }
// mc++         return true;
// mc++     }
// mc++ }
