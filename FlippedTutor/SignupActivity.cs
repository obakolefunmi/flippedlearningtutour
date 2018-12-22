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
    [Activity(Label = "Signup", Theme = "@style/Theme.Custom")]
    public class SignupActivity : AppCompatActivity,IOnClickListener  
    {
        EditText email, password ,Staffid,fullName;
        TextView login;
        Spinner signupsSpinner;
        Button signupButton;
        ProgressBar signupProgressBar;
        LinearLayout signupHolder;
        ConnectivityManager connectivityManager;
        FirebaseAuth auth;
        string title;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Signup);
            auth = FirebaseAuth.Instance;
            email = FindViewById<EditText>(Resource.Id.signupEmail);
            password = FindViewById<EditText>(Resource.Id.signupPassword);
            Staffid = FindViewById<EditText>(Resource.Id.signupStaffId);
            fullName = FindViewById<EditText>(Resource.Id.signupName);
            login  = FindViewById<TextView>(Resource.Id.signupLogin);
            signupButton = FindViewById<Button>(Resource.Id.signupButt);
            signupProgressBar = FindViewById<ProgressBar>(Resource.Id.signuppgb);
            signupHolder = FindViewById<LinearLayout>(Resource.Id.signupHolder);

            connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            signupsSpinner = FindViewById<Spinner>(Resource.Id.spinner1);

            signupsSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.Title_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            signupsSpinner.Adapter = adapter;

            signupButton.SetOnClickListener(this);
            login.SetOnClickListener(this);
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            title = spinner.GetItemAtPosition(e.Position).ToString();
            //string toast = string.Format("The planet is {0}", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private void Signup(string mail, string Password)
        {
            if (email.Text.ToString().Trim() != "" && password.Text.ToString().Trim() != "" && Staffid.Text.ToString().Trim() != "" && fullName.Text.ToString().Trim() != "" && email.Text.Contains("@")&& email.Text.Contains("@cu.edu.ng"))
            {
                 //signupHolder.Visibility = ViewStates.Gone;
                //login.Visibility = ViewStates.Gone;
               //signupProgressBar.Visibility = ViewStates.Visible;
                Intent gotocoll = new Intent(this, typeof(SignupCollegeActivity));
                gotocoll.PutExtra("title", title);
                gotocoll.PutExtra("staffid", Staffid.Text.ToString());
                gotocoll.PutExtra("email", email.Text.ToString());
                gotocoll.PutExtra("name", fullName.Text.ToString());
                gotocoll.PutExtra("password", password.Text.ToString());
                StartActivity(gotocoll);
                }
            else
            {
                if (Staffid.Text.ToString().Trim() == "")
                {
                    Staffid.SetError("Required", null);
                }
                 if (email.Text.ToString().Trim() == "")
                {
                    email.SetError("Required", null);

                }
                 if (!email.Text.ToString().Contains("@"))
                {
                    email.SetError("Email Not Valid", null);
                }
                 if (!email.Text.ToString().Contains("@cu.edu.ng"))
                {
                    email.SetError("Please use your Covenant University E-mail", null);
                }
                 if (password.Text.ToString().Trim() == "")
                {
                    password.SetError("Required", null);
                }
                 if (fullName.Text.ToString().Trim() == "")
                {
                    fullName.SetError("Required", null);
                }              
            }
        }



        public void OnClick(View v)
        {
            switch(v.Id)
            {
                case Resource.Id.signupLogin:
                    StartActivity(typeof(LoginActivity));
                    break;
                case Resource.Id.signupButt:
                    Signup(email.Text, password.Text);
                    break;
            }
                
        }

    }
}