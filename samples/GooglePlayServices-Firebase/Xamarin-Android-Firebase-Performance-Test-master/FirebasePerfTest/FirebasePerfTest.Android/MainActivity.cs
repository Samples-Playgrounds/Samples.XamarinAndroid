using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using System;

namespace FirebasePerfTest.Droid
{
    [Activity(Label = "FirebasePerfTest", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var button = FindViewById<Button>(Resource.Id.button_execute);
            button.Click += (send, args) => {

                var uri = new Uri("https://www.google.com/");
                NetworkHelper.GetRequestAsync(uri).ContinueWith(task => {
                    if (task.IsFaulted) {
                        button.Post(() => {
                            Toast.MakeText(this, "Call failed", ToastLength.Long).Show();
                        });
                        
                        return;
                    }
                    button.Post(() => {
                        Toast.MakeText(this, "Call succeeded", ToastLength.Long).Show();
                    });
                });
            };

            Firebase.FirebaseApp fbapp = null;
            Firebase.Perf.FirebasePerformance fbp = new Firebase.Perf.FirebasePerformance(fbapp);

            Firebase.Perf.Metrics.Trace t1 = Firebase.Perf.FirebasePerformance.Instance.NewTrace("t1");
            Firebase.Perf.Metrics.Trace t2 = fbp.NewTrace("t2");

            return;
        }
    }
}