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
    [Activity(Label = "", Theme = "@style/Theme.Custom1")]
    public class LectureEvaluationActivity : AppCompatActivity
    {
        public TextView StudentEvalcourse, setEvalTotal;
        public LinearLayout StudentEvalHolder;
        public ListView StudentEvalListView;
        public ProgressBar studentEvalpgb;
        public List<StudentEval> studdentEvalList = new List<StudentEval>();
        public List<TwoDataClass> listdata = new List<TwoDataClass>();
        StudentEval studentselected = null;
        string cours, course, stud,student;

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
            SetContentView(Resource.Layout.LectureEvalActivity);
            studentEvaltoolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbarlectureEval);
            SetSupportActionBar(studentEvaltoolbar);
            MaterialMenuDrawable materialMenu = new MaterialMenuDrawable(this, Color.Purple, (int)stroke.EXTRA_THIN, MaterialMenuDrawable.DefaultScale, MaterialMenuDrawable.DefaultTransformDuration);
            materialMenu.SetIconState(MaterialMenuDrawable.IconState.Arrow);
            studentEvaltoolbar.NavigationIcon = materialMenu;
            studentEvaltoolbar.NavigationClick += delegate {
                OnBackPressed();
                Finish();
            };
            cours = Intent.GetStringExtra("course") ?? "";
            stud = Intent.GetStringExtra("student") ?? "";
            student = "\"" + stud.ToString() + "\"";

            course = "\"" + cours.ToString() + "\"";


            StudentEvalHolder = FindViewById<LinearLayout>(Resource.Id.lectureEvalInfoHolder);
            StudentEvalListView = FindViewById<ListView>(Resource.Id.lectureEvalList);
            studentEvalpgb = FindViewById<ProgressBar>(Resource.Id.lectureEvalpgb);
            StudentEvalcourse = FindViewById<TextView>(Resource.Id.lectureEvalCourse);
            setEvalTotal= FindViewById<TextView>(Resource.Id.setEvalTotal);
            StudentEvalcourse.Text = cours;
            new GetStudentsLectureSpecifictData(this, StudentEvalListView, studentEvalpgb, StudentEvalHolder).Execute(Common.getAddresApiStudentEvalspecifics(student, course));
            // Create your application here
        }

    }
}