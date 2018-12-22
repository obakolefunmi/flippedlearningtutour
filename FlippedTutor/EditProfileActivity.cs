using System;
using System.Collections.Generic;

using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Balysv.Material.Drawable.Menu;
using Firebase.Auth;
using FlippedTutor.Class;

namespace FlippedTutor
{
    [Activity(Label = "Edit Profile", Theme = "@style/Theme.Custom")]
    public class EditProfileActivity : AppCompatActivity
    {
        public List<Profile> profileList = new List<Profile>();
        public EditText  editname, edittitle;
        public TextView editemail, editstaffid, editcollege, editdepartment,edittextEdit;
        public CardView editHolder;
        public ProgressBar editpgb;
        string curruser;
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
            SetContentView(Resource.Layout.Editprofile);
            var toolbareditprofile = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbarEditProfile);
            SetSupportActionBar(toolbareditprofile);
            MaterialMenuDrawable materialMenu = new MaterialMenuDrawable(this, Color.Purple, (int)stroke.EXTRA_THIN, MaterialMenuDrawable.DefaultScale, MaterialMenuDrawable.DefaultTransformDuration);
            materialMenu.SetIconState(MaterialMenuDrawable.IconState.Arrow);
            toolbareditprofile.NavigationIcon = materialMenu;
            toolbareditprofile.NavigationClick += delegate {
                OnBackPressed();
                Finish();
            };

            editname = FindViewById<EditText>(Resource.Id.editname);
            edittitle = FindViewById<EditText>(Resource.Id.edittitle);
            editemail = FindViewById<TextView>(Resource.Id.editEmail);
            editstaffid = FindViewById<TextView>(Resource.Id.editStaffID);
            editcollege = FindViewById<TextView>(Resource.Id.editCollege);
            editdepartment = FindViewById <TextView>(Resource.Id.editDepartment);
            editHolder = FindViewById<CardView>(Resource.Id.editCard);
            editpgb = FindViewById<ProgressBar>(Resource.Id.editprofilepgb);
            edittextEdit = FindViewById<TextView>(Resource.Id.editprofileEdit);
            auth = FirebaseAuth.Instance;
            curruser = "\"" + auth.CurrentUser.Email.ToString() + "\"";

            new GetProfileSpecifictDataEdit(this).Execute(Common.getAddresApiProfilespecifictitle(curruser));

            edittextEdit.Click += delegate
            {
                new EditProfileData(profileList[0], editname.Text.ToString(), edittitle.Text.ToString(), this).Execute(Common.getAddressSingleProfile(profileList[0]));
                StartActivity(typeof(SettingsActivity));
                Finish();

            };


            // Create your application here
        }
    }
}