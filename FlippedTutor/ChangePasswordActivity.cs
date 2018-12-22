using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Balysv.Material.Drawable.Menu;
using Firebase.Auth;
using Java.Lang;

namespace FlippedTutor
{
    [Activity(Label = "Change Password", Theme = "@style/Theme.Custom")]
    public class ChangePasswordActivity : AppCompatActivity, IOnSuccessListener, IOnFailureListener
    {
        EditText editpass;
        Button Changebutton;
        ProgressBar Changepgb;
        public static FirebaseAuth auth;
        enum stroke
        {
            REGULAR = 3,

            THIN = 2,
            EXTRA_THIN = 1


        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.changepassword);
            var toolbareditprofile = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbarchange);
            SetSupportActionBar(toolbareditprofile);
            MaterialMenuDrawable materialMenu = new MaterialMenuDrawable(this, Color.Purple, (int)stroke.EXTRA_THIN, MaterialMenuDrawable.DefaultScale, MaterialMenuDrawable.DefaultTransformDuration);
            materialMenu.SetIconState(MaterialMenuDrawable.IconState.Arrow);
            toolbareditprofile.NavigationIcon = materialMenu;
            toolbareditprofile.NavigationClick += delegate {
                OnBackPressed();
                Finish();
            };
            auth = FirebaseAuth.Instance;

            editpass = FindViewById<EditText>(Resource.Id.changepasswordedit);
            Changebutton = FindViewById<Button>(Resource.Id.changeButton);
            Changepgb = FindViewById<ProgressBar>(Resource.Id.changepgb);

            Changebutton.Click += Changebutton_Click;
            // Create your application here
        }

        private void Changebutton_Click(object sender, EventArgs e)
        {
            if (editpass.Text.Trim().ToString() == "")
            {
                editpass.SetError("Required",null);
            }
            else
            {
                editpass.Visibility = ViewStates.Gone;
                Changebutton.Visibility = ViewStates.Gone;
                Changepgb.Visibility = ViewStates.Visible;
                auth.CurrentUser.UpdatePassword(editpass.Text).AddOnSuccessListener(this).AddOnFailureListener(this);
            }
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            editpass.Visibility = ViewStates.Visible;
            Changebutton.Visibility = ViewStates.Visible;
            Changepgb.Visibility = ViewStates.Gone;
            StartActivity(typeof(SettingsActivity));
        }

        public void OnFailure(Java.Lang.Exception e)
        {
            Toast.MakeText(this, "Sorry failed to change your password", ToastLength.Short).Show();
        }
    }
}