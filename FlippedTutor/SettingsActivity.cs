using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Balysv.Material.Drawable.Menu;
using static Android.Views.View;

namespace FlippedTutor
{
    [Activity(Label = "Settings", Theme = "@style/Theme.Custom1")]
    public class SettingsActivity : AppCompatActivity,IOnClickListener
    {
        TextView editProfile, addndDrop, Credits, Changepassword;
        enum stroke
        {
            REGULAR = 3,

            THIN = 2,
            EXTRA_THIN = 1


        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Settingslayout);
            var toolbardSettings = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbarSettings);
            SetSupportActionBar(toolbardSettings);
            MaterialMenuDrawable materialMenu = new MaterialMenuDrawable(this, Color.Purple, (int)stroke.EXTRA_THIN, MaterialMenuDrawable.DefaultScale, MaterialMenuDrawable.DefaultTransformDuration);
            materialMenu.SetIconState(MaterialMenuDrawable.IconState.Arrow);
            toolbardSettings.NavigationIcon = materialMenu;
            toolbardSettings.NavigationClick += delegate {
                OnBackPressed();
                Finish();
            };



            editProfile = FindViewById<TextView>(Resource.Id.settingseditProfile);
            addndDrop = FindViewById<TextView>(Resource.Id.settingsaddndDrop);
            Credits = FindViewById<TextView>(Resource.Id.settingsCredits);
            Changepassword = FindViewById<TextView>(Resource.Id.settingsChangePassword);
             
            editProfile.SetOnClickListener(this);
            addndDrop.SetOnClickListener(this);
            Credits.SetOnClickListener(this);
            Changepassword.SetOnClickListener(this);
            // Create your application here
        }

        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.settingseditProfile:
                    StartActivity(typeof(EditProfileActivity));
                    break;
                case Resource.Id.settingsaddndDrop:
                    StartActivity(typeof(AddandDropActivity));
                    break;
                case Resource.Id.settingsCredits:
                    Toast.MakeText(this, ";)", ToastLength.Short).Show();
                    break;
                case Resource.Id.settingsChangePassword:
                    StartActivity(typeof(ChangePasswordActivity));
                    break;
            }
        }
    }
}