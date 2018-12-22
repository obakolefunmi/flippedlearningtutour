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
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using static Android.Views.View;

namespace FlippedTutor
{
    [Activity(Label = "Flipped CU (lecturer)", MainLauncher = true, Theme = "@style/Theme.Custom")]
    public class LoginActivity : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        ProgressBar pgblogin;
        TextInputLayout emaillay, passlay;
        Button loginbutt;
        EditText loginmail, loginpass;
        TextView forgotpass, signup;
        LinearLayout loginHolder;
        //public static FirebaseApp app;
        ConnectivityManager connectivityManager;
       public static FirebaseAuth auth;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);
            auth = FirebaseAuth.Instance;

           // InitFirebaseAuth();
            loginbutt = FindViewById<Button>(Resource.Id.loginButt);
            loginmail = FindViewById<EditText>(Resource.Id.LoginMatno);
            loginpass = FindViewById<EditText>(Resource.Id.loginPass);
            forgotpass = FindViewById<TextView>(Resource.Id.loginForgotpass);
            signup = FindViewById<TextView>(Resource.Id.loginSignup);
            pgblogin = FindViewById<ProgressBar>(Resource.Id.loginpgb);
            emaillay = FindViewById<TextInputLayout>(Resource.Id.textInputLayout1);
            passlay = FindViewById<TextInputLayout>(Resource.Id.textInputLayout2);
            loginHolder = FindViewById<LinearLayout>(Resource.Id.loginHolder);
            loginbutt.SetOnClickListener(this);
            forgotpass.SetOnClickListener(this);
            signup.SetOnClickListener(this);
            connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);


        }

   /*     private void InitFirebaseAuth()
       {

            var options = new FirebaseOptions.Builder()
             .SetApplicationId("1:9956222987a72:android:40aceca67a9677a7")
             . SetApiKey("AIzaSyCWP-WzQWdWZPYxrgs43SJUCEQI8lDpUzo")
             .Build();

            if (app == null)
            {
                app = FirebaseApp.InitializeApp(this, options);
            }
            auth = FirebaseAuth.GetInstance(app);
        }*/

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.loginForgotpass)
            {
                StartActivity(typeof(ForgotpasswordActivity));
                Finish();
            }
            else if (v.Id == Resource.Id.loginSignup)
            {
                StartActivity(typeof(SignupActivity));
                Finish();
            }
            else if (v.Id == Resource.Id.loginButt)
            {
                LoginUser(loginmail.Text, loginpass.Text);
            }

        }

        private void LoginUser(string mail, string password)
        {

            if (mail.ToString().Trim() == "")
            {
                loginmail.SetError("Required", null);

            }
            if (!mail.ToString().Contains("@"))
            {
                loginmail.SetError("Email Not Valid", null);
            }
            if (password.ToString().Trim() == "")
            {
                loginpass.SetError("Required", null);

            }
            else
            {
                pgblogin.Visibility = ViewStates.Visible;
                /* loginbutt.Visibility = ViewStates.Gone;
                 loginmail.Visibility = ViewStates.Gone;
                 loginpass.Visibility = ViewStates.Gone;
                 forgotpass.Visibility = ViewStates.Gone;
                 emaillay.Visibility = ViewStates.Gone;
                 passlay.Visibility = ViewStates.Gone;*/
                 signup.Visibility = ViewStates.Gone;                 
                loginHolder.Visibility = ViewStates.Gone;
                auth.SignInWithEmailAndPassword(mail, password)
                    .AddOnCompleteListener(this, this);
            }

        }
        protected override void OnStart()
        {
            base.OnStart();
            if (auth.CurrentUser != null)
            {
                string m = auth.CurrentUser.Email.ToString();
                //auth.CurrentUser.Uid.ToString();
                Intent gotomain = new Intent(this, typeof(MainActivity));
                gotomain.PutExtra("email",m);
                StartActivity(gotomain);
                Finish();
            }
        }
        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                pgblogin.Visibility = ViewStates.Gone;
                /*loginbutt.Visibility = ViewStates.Visible;
                loginmail.Visibility = ViewStates.Visible;
                loginpass.Visibility = ViewStates.Visible;
                forgotpass.Visibility = ViewStates.Visible;
                emaillay.Visibility = ViewStates.Visible;
                passlay.Visibility = ViewStates.Visible;*/
                signup.Visibility = ViewStates.Visible;
                loginHolder.Visibility = ViewStates.Visible;
                Intent gotomain = new Intent(this, typeof(MainActivity));
                gotomain.PutExtra("email", loginmail.Text);
                StartActivity(gotomain);
                Finish();

            }
            else
            {
                pgblogin.Visibility = ViewStates.Gone;
                /*loginbutt.Visibility = ViewStates.Visible;
                loginmail.Visibility = ViewStates.Visible;
                loginpass.Visibility = ViewStates.Visible;
                forgotpass.Visibility = ViewStates.Visible;
                emaillay.Visibility = ViewStates.Visible;
                passlay.Visibility = ViewStates.Visible;*/
                signup.Visibility = ViewStates.Visible;
                loginpass.Text = "";
                loginHolder.Visibility = ViewStates.Visible;
                NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
                try
                {
                    bool isOnline = networkInfo.IsConnected;

                    if (isOnline == true)
                    {
                        Toast.MakeText(this, "Invalid Email or password!", Android.Widget.ToastLength.Long).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, "Connect to the internet and try again!", Android.Widget.ToastLength.Long).Show();
                    }
                }catch(Exception e)
                {
                    Toast.MakeText(this, "Connect to the internet and try again!", Android.Widget.ToastLength.Long).Show();

                }

            }
        }
    }
}