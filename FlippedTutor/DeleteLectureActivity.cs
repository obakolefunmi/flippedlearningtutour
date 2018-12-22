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
using Firebase.Auth;
using FlippedTutor.Class;
using static Android.Views.View;
using static Android.Widget.AdapterView;

namespace FlippedTutor
{
    [Activity(Label = "Delete Lecture", Theme = "@style/Theme.Custom1")]
    public class DeleteLectureActivity : AppCompatActivity, IOnItemClickListener,IOnClickListener
    {
       public ListView lectureListview;
        public List<Lectures> lectureslistss = new List<Lectures>();
       public List<string> lecturelist = new List<string>();
        List<int> positions = new List<int>();

        public LinearLayout delitemHolder;
        public ProgressBar delpgb;
        TextView delDelete;
        string curruser, cours, course;
        FirebaseAuth auth;

        string lectureselected = null;
        enum stroke
        {
            REGULAR = 3,

            THIN = 2,
            EXTRA_THIN = 1


        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DeleteLecturelayout);
            auth = FirebaseAuth.Instance;
            curruser = "\"" + auth.CurrentUser.Email.ToString() + "\"";
            cours = Intent.GetStringExtra("course") ?? "";
            course = "\"" +cours.ToString()+ "\"";

            var toolbardellecture = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbardelLecture);
            SetSupportActionBar(toolbardellecture);
            MaterialMenuDrawable materialMenu = new MaterialMenuDrawable(this, Color.Purple, (int)stroke.EXTRA_THIN, MaterialMenuDrawable.DefaultScale, MaterialMenuDrawable.DefaultTransformDuration);
            materialMenu.SetIconState(MaterialMenuDrawable.IconState.Arrow);
            toolbardellecture.NavigationIcon = materialMenu;
            toolbardellecture.NavigationClick += delegate {
                OnBackPressed();
                Finish();
            };
            delDelete = FindViewById<TextView>(Resource.Id.deleteLectureDelete);
            delDelete.SetOnClickListener(this);
            // Create your application here
            delitemHolder = FindViewById<LinearLayout>(Resource.Id.DeleteLectureInfoHolder);
            delpgb = FindViewById<ProgressBar>(Resource.Id.deleteLecpgb);
            lectureListview = FindViewById<ListView>(Resource.Id.DeleteLectureListview);
            //mlab code
            new GetLectureSpecifictDataDelete(this).Execute(Common.getAddresApiLecturesspecificdelete(curruser,course));

            lectureListview.OnItemClickListener = this;
            
        }
        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            lectureselected = lecturelist[position];
            positions.Add(position);

            var j = lectureselected.ToString();
            Android.Widget.Toast.MakeText(this, j, Android.Widget.ToastLength.Short).Show();
        }

        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.deleteLectureDelete:
                    for (int i = 0; i < positions.Count(); i++)
                    {
                        new DelLectureData(lectureslistss[positions[i]], this).Execute(Common.getAddressSingleLectures(lectureslistss[positions[i]]));
                    }
                    break;
            }
        }
    }
}