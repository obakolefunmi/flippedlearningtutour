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
    [Activity(Label = "Questions", Theme = "@style/Theme.Custom")]
    public class QuestionsActivity : AppCompatActivity
    {
        public List<Question> questionList = new List<Question>();
        ProgressBar questpgb;
        LinearLayout questholder;
        LinearLayout questholder2;
        ListView questlist;
        TextView questcourse, questlecture;
        string titl, title, course;
        enum stroke
        {
            REGULAR = 3,

            THIN = 2,
            EXTRA_THIN = 1


        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QuestionsActivity);
            var toolbarquestlecture = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbarquests);
            SetSupportActionBar(toolbarquestlecture);
            MaterialMenuDrawable materialMenu = new MaterialMenuDrawable(this, Color.Purple, (int)stroke.EXTRA_THIN, MaterialMenuDrawable.DefaultScale, MaterialMenuDrawable.DefaultTransformDuration);
            materialMenu.SetIconState(MaterialMenuDrawable.IconState.Arrow);
            toolbarquestlecture.NavigationIcon = materialMenu;
            toolbarquestlecture.NavigationClick += delegate {
                OnBackPressed();
                Finish();
            };
            questpgb = FindViewById<ProgressBar>(Resource.Id.questpgb);
            questholder = FindViewById<LinearLayout>(Resource.Id.questholder);
            questholder2 = FindViewById<LinearLayout>(Resource.Id.questholder2);
            questlist = FindViewById<ListView>(Resource.Id.questlistvw);
            questcourse = FindViewById<TextView>(Resource.Id.questcourse);
            questlecture = FindViewById<TextView>(Resource.Id.questlecture);
            course = Intent.GetStringExtra("course") ?? "";
            titl = Intent.GetStringExtra("title") ?? "";
            title = "\"" + titl.ToString() + "\"";
            questlecture.Text = titl;
            questcourse.Text = course;

            new GetQuestionSpecifictData(this, questlist, questpgb, questholder, questholder2).Execute(Common.getAddresApiQuestionsspecific(title));
        }
    }
}