
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
using Firebase.Auth;
using FlippedTutor.Class;
using static Android.Views.View;
using static Android.Widget.AdapterView;

namespace FlippedTutor
{
    [Activity(Label = "LecturesQuestionActivity", Theme = "@style/Theme.Custom")]
    public class LecturesQuestionActivity : AppCompatActivity, IOnItemClickListener
    {
        public ListView lectureListview;
        public List<Lectures> lectureslistss = new List<Lectures>();
        List<int> positions = new List<int>();

        public LinearLayout delitemHolder;
        public ProgressBar delpgb;
        string curruser, cours, course, student;
        FirebaseAuth auth;

        Lectures lectureselected = null;
        enum stroke
        {
            REGULAR = 3,

            THIN = 2,
            EXTRA_THIN = 1


        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LectureQuestionsActivity);
            auth = FirebaseAuth.Instance;
            curruser = "\"" + auth.CurrentUser.Email.ToString() + "\"";
            cours = Intent.GetStringExtra("course") ?? "";
            student = Intent.GetStringExtra("student") ?? "";

            course = "\"" + cours.ToString() + "\"";

            var toolbarquestlecture = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbarquestLecture);
            SetSupportActionBar(toolbarquestlecture);
            MaterialMenuDrawable materialMenu = new MaterialMenuDrawable(this, Color.Purple, (int)stroke.EXTRA_THIN, MaterialMenuDrawable.DefaultScale, MaterialMenuDrawable.DefaultTransformDuration);
            materialMenu.SetIconState(MaterialMenuDrawable.IconState.Arrow);
            toolbarquestlecture.NavigationIcon = materialMenu;
            toolbarquestlecture.NavigationClick += delegate {
                OnBackPressed();
                Finish();
            };
            // Create your application here
            delitemHolder = FindViewById<LinearLayout>(Resource.Id.questLectureInfoHolder);
            delpgb = FindViewById<ProgressBar>(Resource.Id.questLecpgb);
            lectureListview = FindViewById<ListView>(Resource.Id.questLectureListview);
            //mlab code
            new GetLectureSpecifictDataLectures(this).Execute(Common.getAddresApiLecturesspecificdelete(course));

            lectureListview.OnItemClickListener = this;

        }
        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            lectureselected = lectureslistss[position];
            positions.Add(position);
            var t = lectureselected.title.ToString();
            var vurl = lectureselected.vidurl.ToString();
            var nurl = lectureselected.noteurl.ToString();
            var vname = lectureselected.vidname.ToString();
            var nname = lectureselected.notename.ToString();
            Intent intent = new Intent(this, typeof(QuestionsActivity));

            intent.PutExtra("course", cours);
            intent.PutExtra("title", t);
           

            StartActivity(intent);
            Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Short).Show();
        }

    }
}