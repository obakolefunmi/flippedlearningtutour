using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Balysv.Material.Drawable.Menu;
using Firebase.Auth;
using FlippedTutor.Class;
using FlippedTutor.Helper;
using Newtonsoft.Json;
using static Android.Views.View;
using static Android.Widget.AdapterView;

namespace FlippedTutor
{
    [Activity(Label = "Add and Drop", Theme = "@style/Theme.Custom")]
    public class AddandDropActivity : AppCompatActivity, IOnItemClickListener, IOnClickListener
    {
        public  string college,  department ;
        public static string collss, depss;
        public ProgressBar adddroppgb;
        public LinearLayout adddropholder;
        public ListView courseListView;
        public List<Profile> profileList = new List<Profile>();
        public List<Courses> coursesleList;// = new List<Courses>();
        TextView addanddropDone;
        DbHelper db;
        SQLiteDatabase sqliteDB = null;
        List<string> courselist = new List<string>();
        string courseselected = null;
        public static string email;
        public string curruser;
        FirebaseAuth auth;
        enum stroke
        {
            REGULAR = 3,

            THIN = 2,
            EXTRA_THIN = 1


        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //
            SetContentView(Resource.Layout.AddandDrop);
            adddroppgb = FindViewById<ProgressBar>(Resource.Id.addandadroppgb);
            adddropholder = FindViewById<LinearLayout>(Resource.Id.addanddropholder);
            courseListView = FindViewById<ListView>(Resource.Id.addanddroplist);
            addanddropDone = FindViewById<TextView>(Resource.Id.addanddropdone);
            var toolbareditprofile = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.addanddroptoolbaar);
            SetSupportActionBar(toolbareditprofile);
            MaterialMenuDrawable materialMenu = new MaterialMenuDrawable(this, Color.Purple, (int)stroke.EXTRA_THIN, MaterialMenuDrawable.DefaultScale, MaterialMenuDrawable.DefaultTransformDuration);
            materialMenu.SetIconState(MaterialMenuDrawable.IconState.Arrow);
            toolbareditprofile.NavigationIcon = materialMenu;
            toolbareditprofile.NavigationClick += delegate {
                OnBackPressed();
                Finish();
            };
            //
            auth = FirebaseAuth.Instance;
            string curruser = "\"" + auth.CurrentUser.Email.ToString() + "\"";
            new GetCourseSpecifictDataAddandDrop(this).Execute(Common.getAddresApiCoursesspecifictitle(curruser));
            new GetProfileSpecifictDataAddandDrop(this).Execute(Common.getAddresApiProfilespecifictitle(curruser));


            db = new DbHelper(this);
            sqliteDB = db.WritableDatabase;
            collss = "\"" + college + "\"";
            depss = "\"" + department + "\"";
            email = auth.CurrentUser.Email;
            AddData();
            ArrayAdapter<string> corseadapt = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemMultipleChoice, courselist);
            courseListView.ChoiceMode = ChoiceMode.Multiple;
            courseListView.Adapter = corseadapt;
            courseListView.OnItemClickListener = this;
            

            addanddropDone.SetOnClickListener(this);
            ;
            // Create your application here
        }
        private class AddCourse : AsyncTask<string, Java.Lang.Void, string>
        {
            string course = "";
            string lecturer = AddandDropActivity.email;
            AddandDropActivity activity = new AddandDropActivity();
            public AddCourse(string course, AddandDropActivity activity)
            {
                this.course = course;
                this.activity = activity;
            }

            protected override void OnPreExecute()
            {
                base.OnPreExecute();

                activity.adddroppgb.Visibility = ViewStates.Visible;
                activity.adddropholder.Visibility = ViewStates.Gone;

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

                activity.adddroppgb.Visibility = ViewStates.Gone;
                activity.adddropholder.Visibility = ViewStates.Visible;

            }

        }

        public void AddData()
        {
            //ICursor selectData = sqliteDB.RawQuery("SELECT Course FROM courses WHERE College LIKE " + collss + " AND Department LIKE " + depss + " ORDER BY Department", new string[] { });

            ICursor selectData = sqliteDB.RawQuery("SELECT Course FROM courses WHERE College LIKE \"College of Science and Technology\" AND Department LIKE  \"Department of Computer and Information Sciences\" ORDER BY Department", new string[] { });
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
        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            courseselected = courselist[position];

            var j = courseselected.ToString();
            Android.Widget.Toast.MakeText(this, j, Android.Widget.ToastLength.Short).Show();
        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.addanddropdone)
            {
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

            }
        }
    }
}