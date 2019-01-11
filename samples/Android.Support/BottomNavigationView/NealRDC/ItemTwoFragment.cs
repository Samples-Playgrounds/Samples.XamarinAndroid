using System;
namespace NealRDC
{
    public class ItemTwoFragment : Android.Support.V4.App.Fragment
    {
        public static ItemTwoFragment NewInstance()
        {
            ItemTwoFragment fragment = new ItemTwoFragment();
            return fragment;
        }

        //@Override
        public void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        //@Override
        public Android.Views.View OnCreateView
                                        (
                                            Android.Views.LayoutInflater inflater, 
                                            Android.Views.ViewGroup container,
                                            Android.OS.Bundle savedInstanceState
                                        )
        {
            return inflater.Inflate(Resource.Layout.fragment_item_two, container, false);
        }
}
}
