using Android.App;
using Android.Widget;
using Android.OS;
    
namespace hitherejoe
{
    [Activity(Label = "hitherejoe", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme")]
    public class MainActivity : Android.Support.V7.App.AppCompatActivity
    {
        private TextView textFavorites;
        private TextView textSchedules;
        private TextView textMusic;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Android.Support.Design.Widget.BottomNavigationView bottomNavigationView = null;
            bottomNavigationView = FindViewById<Android.Support.Design.Widget.BottomNavigationView>(Resource.Id.bottom_navigation);

            int icon_size = bottomNavigationView.ItemIconSize;

            textFavorites = FindViewById<TextView>(R.id.text_favorites);
            textSchedules = FindViewById<TextView>(R.id.text_schedules);
            textMusic = FindViewById<TextView>(R.id.text_music);

            bottomNavigationView.NavigationItemSelected += (sender, e) =>
            {
                //switch (e.Item.ItemId)
                //{
                //case Resources.Id.action_favorites:
                //    textFavorites.Visibility = Android.Views.ViewStates.Visible;
                //    textSchedules.Visibility = Android.Views.ViewStates.Gone;
                //    textMusic.Visibility = Android.Views.ViewStates.Gone;
                //    break;
                //case Resources.Id.action_schedules:
                //    textFavorites.SetVisibility(Android.Views.ViewStates.Gone);
                //    textSchedules.SetVisibility(Android.Views.ViewStates.Visible);
                //    textMusic.SetVisibility(Android.Views.ViewStates.Gone);
                //    break;
                //case R.id.action_music:
                //textFavorites.setVisibility(Android.Views.ViewStates.Gone);
                //textSchedules.setVisibility(Android.Views.ViewStates.Gone);
                //textMusic.setVisibility(Android.Views.ViewStates.Visible);
                //break;
                //}

                return;
            };

            return;
        }
    }
}

