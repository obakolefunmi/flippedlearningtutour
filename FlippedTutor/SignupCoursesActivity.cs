using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.Gms.Tasks;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using FlippedTutor.Adapters;
using FlippedTutor.Class;
using FlippedTutor.Helper;
using Newtonsoft.Json;
using static Android.Views.View;
using static Android.Widget.AdapterView;

namespace FlippedTutor
{
    [Activity(Label = "Signup", Theme = "@style/Theme.Custom")]
    public class SignupCoursesActivity : AppCompatActivity, IOnItemClickListener, IOnClickListener, IOnCompleteListener
    {
        public static string college, collss, name, department, depss , email,title, password, staffid;
        EditText searchcourse;
        TextView courseWelcmMsg, prevButt, nextButt;
        ListView courseListView;
        ProgressBar signupCoursespgb;
        LinearLayout signupCoursesHolder;
        DbHelper db;
        SQLiteDatabase sqliteDB = null;
        ConnectivityManager connectivityManager;

        List<string> courselist = new List<string>();
        string courseselected = null;
        FirebaseAuth auth;
        protected override void OnPause()
        {
            base.OnPause();
            Finish();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.signupcourses);
            college = Intent.GetStringExtra("college") ?? "";
            department = Intent.GetStringExtra("department") ?? "";
            name = Intent.GetStringExtra("name") ?? "";
            title = Intent.GetStringExtra("title") ?? "";
            password = Intent.GetStringExtra("password") ?? "";
            email = Intent.GetStringExtra("email") ?? "";
            staffid = Intent.GetStringExtra("staffid") ?? "";
            name = Intent.GetStringExtra("name") ?? "";
            collss = "\"" + college + "\"";
            depss = "\"" + department + "\"";
            auth = FirebaseAuth.Instance;
            connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            courseListView = FindViewById<ListView>(Resource.Id.courseList);
            searchcourse = FindViewById<EditText>(Resource.Id.coursesSearch);
            courseWelcmMsg = FindViewById<TextView>(Resource.Id.courseWelcome);
            prevButt = FindViewById<TextView>(Resource.Id.coursesPrevious);
            nextButt = FindViewById<TextView>(Resource.Id.coursesNext);
            signupCoursespgb = FindViewById<ProgressBar>(Resource.Id.signupCoursespgb);
            signupCoursesHolder = FindViewById<LinearLayout>(Resource.Id.signupCoursesHolder);
            searchcourse.ClearFocus();
            db = new DbHelper(this);
            sqliteDB = db.WritableDatabase;
            AddData();
            ArrayAdapter<string> corseadapt = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemMultipleChoice, courselist);
            courseListView.ChoiceMode = ChoiceMode.Multiple;
            courseListView.Adapter = corseadapt;
            courseListView.OnItemClickListener = this;                                 
            prevButt.SetOnClickListener(this);
            nextButt.SetOnClickListener(this);
            courseWelcmMsg.Text = "Thank You " + name + "! This is the last stage";
        }
        private void AddData()
        {
            ICursor selectData = sqliteDB.RawQuery("SELECT Course FROM courses WHERE College LIKE " + collss + " AND Department LIKE "+depss+" ORDER BY Department", new string[] { });
            if (selectData.Count > 0)
            {
                selectData.MoveToFirst();
                do
                {                    
                    string value = selectData.GetString(selectData.GetColumnIndex("Course"));                  
                    courselist.Add(value);
                }
                while (selectData.MoveToNext());
                selectData.Close();
            }


        }
        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.coursesPrevious:
                    Intent gotodep = new Intent(this, typeof(SignupDepartmentActivity));
                    gotodep.PutExtra("name", name);
                    gotodep.PutExtra("college", college);
                    gotodep.PutExtra("title", title);
                    gotodep.PutExtra("staffid", staffid);
                    gotodep.PutExtra("email", email);
                    gotodep.PutExtra("password", password);
                    StartActivity(gotodep);
                    Finish();
                    break;
                case Resource.Id.coursesNext:

                    Signup(email,password);
                  
                    break;
            }
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            courseselected = courselist[position];

            var j = courseselected.ToString();
            Android.Widget.Toast.MakeText(this, j, Android.Widget.ToastLength.Short).Show();
        }

        private class AddCourse : AsyncTask<string, Java.Lang.Void, string>
        {
            string course = "";
            string lecturer = SignupCoursesActivity.email;
            SignupCoursesActivity activity = new SignupCoursesActivity();
            public AddCourse(string course, SignupCoursesActivity activity)
            {
                this.course = course;
                this.activity = activity;
            }

            protected override void OnPreExecute()
            {
                base.OnPreExecute();

            }
            protected override string RunInBackground(params string[] @params)
            {
                string url = @params[0];
                HttpHandler http = new HttpHandler();
                Courses courses = new Courses();
                courses.course = course;
                courses.lecturer = lecturer;
                string json = JsonConvert.SerializeObject(courses);
                http.PostHttpData(url, json);
                return String.Empty;
            }
            protected override void OnPostExecute(string result)
            {
                base.OnPostExecute(result);

            }

        }
        private class AddProfile : AsyncTask<string, Java.Lang.Void, string>
        {
            string title = "";
            string email = "";
            string staffid = "";
            string college = "";
            string department = "";
            string name = "";
            SignupCoursesActivity activity = new SignupCoursesActivity();
            public AddProfile(string title ,string email,string staffid,string college ,string department,string name, SignupCoursesActivity activity)
            {

                this.title = title;
                this.name = name;
                this.email = email;
                this.staffid = staffid;
                this.college = college;
                this.department = department;
                this.activity = activity;
            }

            protected override void OnPreExecute()
            {
                base.OnPreExecute();

            }
            protected override string RunInBackground(params string[] @params)
            {
                string url = @params[0];
                HttpHandler http = new HttpHandler();
                Profile profile = new Profile();
                profile.name = name;
                profile.college = college;
                profile.department = department;
                profile.email = email;
                profile.staffid = staffid;
                profile.title = title;
                string json = JsonConvert.SerializeObject(profile);
                http.PostHttpData(url, json);
                return String.Empty;
            }
            protected override void OnPostExecute(string result)
            {
                base.OnPostExecute(result);

            }

        }
        private void Signup(string mail, string Password)
        {
            signupCoursespgb.Visibility = ViewStates.Visible;
            signupCoursesHolder.Visibility = ViewStates.Gone;
             auth.CreateUserWithEmailAndPassword(mail, Password)
            .AddOnCompleteListener(this, this);
        }
        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                // add the profile stuff here
                new AddProfile(title, email, staffid, college, department, name, this).Execute(Common.getAddresApiProfile());

                String selected = "";
                int cntChoice = courseListView.Count;
                SparseBooleanArray sparseBooleanArray = courseListView.CheckedItemPositions;
                for (int i = 0; i < cntChoice; i++)
                {
                    if (sparseBooleanArray.Get(i))
                    {
                        // upload to mongo db one after the oder 
                        new AddCourse(courseListView.GetItemAtPosition(i).ToString(), this).Execute(Common.getAddresApiCourses());

                        selected += courseListView.GetItemAtPosition(i).ToString() + "\n";

                    }
                }
                Android.Widget.Toast.MakeText(this, "Your registration is now complete", Android.Widget.ToastLength.Short).Show();
                signupCoursespgb.Visibility = ViewStates.Gone;
                signupCoursesHolder.Visibility = ViewStates.Visible;
                StartActivity(typeof(MainActivity));
                Finish();
            }
            else
            {
                NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
                try
                {
                    bool isOnline = networkInfo.IsConnected;

                    if (isOnline == true)
                    {
                        Toast.MakeText(this, "Sorry Could not signup try again", Android.Widget.ToastLength.Short).Show();
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