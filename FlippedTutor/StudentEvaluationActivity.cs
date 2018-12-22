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
using FlippedTutor.Class;

namespace FlippedTutor
{
    [Activity(Label = "Evaluation", Theme = "@style/Theme.Custom1")]
    public class StudentEvaluationActivity : AppCompatActivity
    {
        public TextView StudentEvalcourse;
        public LinearLayout StudentEvalHolder;
        public ListView StudentEvalListView;
        public ProgressBar studentEvalpgb;
        public List<Students> studdentEvalList = new List<Students>();
        public List<DataClass>  listdata = new List<DataClass>();
        Students studentselected;
        string cours, course;

        Android.Support.V7.Widget.Toolbar studentEvaltoolbar;
        enum stroke
        {
            REGULAR = 3,

            THIN = 2,
            EXTRA_THIN = 1


        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.StudentEvalActivity);
           studentEvaltoolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbarstudentEval);
            SetSupportActionBar(studentEvaltoolbar);
            MaterialMenuDrawable materialMenu = new MaterialMenuDrawable(this, Color.Purple, (int)stroke.EXTRA_THIN, MaterialMenuDrawable.DefaultScale, MaterialMenuDrawable.DefaultTransformDuration);
            materialMenu.SetIconState(MaterialMenuDrawable.IconState.Arrow);
            studentEvaltoolbar.NavigationIcon = materialMenu;
            studentEvaltoolbar.NavigationClick += delegate {
                OnBackPressed();
                Finish();
            };
            cours = Intent.GetStringExtra("course") ?? "";
            course = "\"" + cours.ToString() + "\"";


            StudentEvalHolder = FindViewById<LinearLayout>(Resource.Id.studentEvalInfoHolder);
            StudentEvalListView = FindViewById<ListView>(Resource.Id.studentEvalList);
            studentEvalpgb = FindViewById<ProgressBar>(Resource.Id.studentEvalpgb);
            StudentEvalcourse = FindViewById<TextView>(Resource.Id.studentEvalCourse);
            StudentEvalcourse.Text = cours;
            new GetStudentsSpecifictData(this,StudentEvalListView,studentEvalpgb,StudentEvalHolder ).Execute(Common.getAddresApiStudentEvalspecific(course));
            StudentEvalListView.ItemClick += StudentEvalListView_ItemClick;
            // Create your application here
        }

        private void StudentEvalListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string selectedFromList = StudentEvalListView.GetItemAtPosition(e.Position).ToString();
            studentselected = studdentEvalList[e.Position];
            string student = studentselected.student;
            string course = studentselected.course;
            Intent gotodellec = new Intent(Application.Context, typeof(LectureEvaluationActivity));
            gotodellec.PutExtra("course", course);
            gotodellec.PutExtra("student", student);
            StartActivity(gotodellec); 
        }
    }
}