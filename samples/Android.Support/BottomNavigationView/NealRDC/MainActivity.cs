using Android.App;
using Android.Widget;
using Android.OS;

namespace NealRDC
{
    [Activity(Label = "NealRDC", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme")]
    public class MainActivity : Android.Support.V7.App.AppCompatActivity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Android.Support.Design.Widget.BottomNavigationView bottomNavigationView = null;
            bottomNavigationView = FindViewById<Android.Support.Design.Widget.BottomNavigationView>(Resource.Id.navigation);

            int icon_size = bottomNavigationView.ItemIconSize;

            bottomNavigationView.NavigationItemSelected += (sender, e) =>
            {
                Android.Support.V4.App.Fragment selectedFragment = null;

                switch (e.Item.ItemId)
                {
                    case Resource.Id.action_item1:
                        selectedFragment = ItemOneFragment.NewInstance();
                        break;
                    case Resource.Id.action_item2:
                        selectedFragment = ItemTwoFragment.NewInstance();
                        break;
                    case Resource.Id.action_item3:
                        selectedFragment = ItemThreeFragment.NewInstance();
                        break;
                }

                Android.Support.V4.App.FragmentTransaction transaction = this.SupportFragmentManager.BeginTransaction();
                transaction.Replace(Resource.Id.frame_layout, selectedFragment);
                transaction.Commit();
            };

            return;
        }
    }
}

