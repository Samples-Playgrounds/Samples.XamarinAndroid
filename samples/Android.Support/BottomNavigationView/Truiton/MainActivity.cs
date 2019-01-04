using Android.App;
using Android.Widget;
using Android.OS;

namespace Truiton
{
    [Activity(Label = "Truiton", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme")]
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

        }
    }
}

