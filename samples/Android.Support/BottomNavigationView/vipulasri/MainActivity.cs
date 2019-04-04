using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace vipulasri
{
    [Activity(Label = "vipulasri", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity 
                : 
                    Android.Support.V7.App.AppCompatActivity, 
                    Android.Support.Design.Widget.BottomNavigationView.IOnNavigationItemSelectedListener
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        private Android.Support.Design.Widget.BottomNavigationView bottomNavigationView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            bottomNavigationView = FindViewById<Android.Support.Design.Widget.BottomNavigationView>(Resource.Id.bottom_navigation);

            int icon_size = bottomNavigationView.ItemIconSize;

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            bottomNavigationView = FindViewById<Android.Support.Design.Widget.BottomNavigationView>(Resource.Id.bottom_navigation);
            bottomNavigationView.SetOnNavigationItemSelectedListener(this);

            return;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.bottomHome: 
                    bottomNavigationView.ItemBackgroundResource = Resource.Color.colorPrimary; 
                    break;
                case Resource.Id.bottomFavorites: 
                    bottomNavigationView.ItemBackgroundResource = Resource.Color.colorRed500; 
                    break;
                case Resource.Id.bottomAbout: 
                    bottomNavigationView.ItemBackgroundResource = Resource.Color.colorBrown500; 
                    break;
            }

            return true;
        }

    }
}

