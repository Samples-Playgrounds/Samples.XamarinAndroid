/*
 * Copyright (c) 2017. Truiton (http://www.truiton.com/).
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * Contributors:
 * Mohit Gupt (https://github.com/mohitgupt)
 *
 */

//package com.truiton.bottomnavigation;
namespace Truiton
{

using Android.OS;
using Android.Support.V4.App;
using Android.Views;

    public class ItemTwoFragment : Android.Support.V4.App.Fragment
    {
        public static ItemTwoFragment NewInstance()
        {
            ItemTwoFragment fragment = new ItemTwoFragment();
            return fragment;
        }

        //@Override
        public override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        //@Override
        public override Android.Views.View OnCreateView
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
