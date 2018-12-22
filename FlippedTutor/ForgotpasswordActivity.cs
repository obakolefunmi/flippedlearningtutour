using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using static Android.Views.View;

namespace FlippedTutor
{
    [Activity(Label = "Forgot password",Theme = "@style/Theme.Custom")]
    public class ForgotpasswordActivity : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {

        EditText resmail;
        Button resbutt;
        TextView reslogin;
        ProgressBar forgotpgb;
        LinearLayout forgotHolder;
        ConnectivityManager connectivityManager;

        FirebaseAuth auth;
        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.forgotLogin)
            {
                StartActivity(typeof(LoginActivity));
                Finish();
            }
            else if (v.Id == Resource.Id.forgotButt)
            {
                ResetPassword(resmail.Text);
            }
        }

        private void ResetPassword(string mail)
        {
            if (resmail.Text.Trim() != "")
            {
                forgotHolder.Visibility = ViewStates.Gone;
                forgotpgb.Visibility = ViewStates.Visible;
                reslogin.Visibility = ViewStates.Gone;
                auth.SendPasswordResetEmail(mail)
                    .AddOnCompleteListener(this, this);
            }
            else
            {
                resmail.SetError("Required", null);
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ForgotPassword);
            auth = FirebaseAuth.Instance;
            resmail = FindViewById<EditText>(Resource.Id.forgotEmail);
            reslogin = FindViewById<TextView>(Resource.Id.forgotLogin);
            resbutt = FindViewById<Button>(Resource.Id.forgotButt);
            forgotpgb = FindViewById<ProgressBar>(Resource.Id.forgotpgb);
            forgotHolder = FindViewById<LinearLayout>(Resource.Id.forgotHolder);
            connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);

            resbutt.SetOnClickListener(this);
            reslogin.SetOnClickListener(this);

        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {

                forgotHolder.Visibility = ViewStates.Visible;
                forgotpgb.Visibility = ViewStates.Gone;
                reslogin.Visibility = ViewStates.Visible;
                Toast.MakeText(this, "Reset Link has been sent to your Email", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(LoginActivity));
                Finish();
            }
            else
            {
                forgotHolder.Visibility = ViewStates.Visible;
                forgotpgb.Visibility = ViewStates.Gone;
                reslogin.Visibility = ViewStates.Visible;
                NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
                try
                {
                    bool isOnline = networkInfo.IsConnected;

                    if (isOnline == true)
                    {
                        Toast.MakeText(this, "Reset Not Succesfull", Android.Widget.ToastLength.Short).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, "Connect to the internet and try again!", Android.Widget.ToastLength.Long).Show();
                    }
                }
                catch (Exception e)
                {
                    Toast.MakeText(this, "Connect to the internet and try again!", Android.Widget.ToastLength.Long).Show();

                }
            }
        }
    }
}