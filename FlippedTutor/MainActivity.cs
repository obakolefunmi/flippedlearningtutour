using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Content;
using Android.Support.V4.View;
using Firebase.Auth;
using FlippedTutor.Class;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using static Android.Widget.AdapterView;
using Com.Balysv.Material.Drawable.Menu;
using Android.Graphics;
using FlippedTutor.Adapters;

namespace FlippedTutor
{
    [Activity(Label = "Flipped CU (Lecturer)", Theme = "@style/Theme.Custom1")]
    public class MainActivity : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        FirebaseAuth auth;
        List<Profile> profilemain;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create UI
            SetContentView(Resource.Layout.Main);
            auth = FirebaseAuth.Instance;
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            // Init toolbar
            var toolbarmain = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbarmain);
            SetSupportActionBar(toolbarmain);


            // Attach item selected handler to navigation view
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavItemSelected;
            View header = navigationView.InflateHeaderView(Resource.Layout.Header);
            TextView headerTitle = header.FindViewById<TextView>(Resource.Id.headerTitle);
            TextView headerName = header.FindViewById<TextView>(Resource.Id.headerName);
            string curruser = "\"" + auth.CurrentUser.Email.ToString() + "\"";

            new GetProfileSpecifictDataMain(this, profilemain, headerTitle, headerName).Execute(Common.getAddresApiProfilespecifictitle(curruser));
            // Create ActionBarDrawerToggle button and add it to the toolbar
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbarmain, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.AddDrawerListener(drawerToggle);
            drawerToggle.SyncState();
            if (savedInstanceState == null) //first launch
                SelectItem(1);

        }


        private void SelectItem(int position)
        {
            // update the main content by replacing fragments
            var fragment = NavFragment.NewInstance(position);

            var fragmentManager = this.FragmentManager;
            var ft = fragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.content_frame, fragment);
            ft.Commit();

            // update selected item title, then close the drawer

            drawerLayout.CloseDrawers();
        }
        public class NavFragment : Fragment
        {// declarations
            public const string ARG_POSITION = "position";
            public ListView CoursesListView, NewLectureListView, DeleteLectureListView, QuestionListView;
            public TextView Courselabel, NewLectureLabel, QuestionLabel, DeleteLectureLabel, ProfileEmail, ProfileTitle, ProfileName, ProfileStaffId, ProfileCollege, ProfileDepartment;
            public CardView courseCard, NewLectureCard, QuestionCard,DeleteLectureCard, ProfileCard, HomeCard;
            public ProgressBar coursepgb, NewLecturepgb, Questionpgb,DeleteLecturepgb, Profilepgb, Homepgb;
            public List<Courses> CoursesList;
            public List<Profile> profileList;
            public List<Lectures> lectuerList;
            public LinearLayout Homeinfoholder;
            public Lectures lectures;
            public HomeRecycAdapter HomeRecycViewAdapter;
            public RecyclerView HomeRecycView;
            public RecyclerView.LayoutManager HomeRecycManager;

            public Courses coursesselected = null;

            FirebaseAuth auth;
            // public Courses selectedcourses;
            public string curruser;



            public NavFragment()
            {
                // Empty constructor required for fragment subclasses
            }

            public static Fragment NewInstance(int position)
            {
                Fragment fragment = new NavFragment();
                Bundle args = new Bundle();
                args.PutInt(NavFragment.ARG_POSITION, position);
                fragment.Arguments = args;
                return fragment;
            }
            // onitem clicked..
            private void NewLectureListView_ItemClick(object sender, ItemClickEventArgs e)
            {
                string selectedFromList = NewLectureListView.GetItemAtPosition(e.Position).ToString();
                coursesselected = CoursesList[e.Position];
                string course = coursesselected.course;
                Intent gotovidup = new Intent(Application.Context, typeof(UploadLectureActivity));
                gotovidup.PutExtra("course", course);
                StartActivity(gotovidup);
            }
            private void DeleteLectureListView_ItemClick(object sender, ItemClickEventArgs e)
            {
                string selectedFromList = DeleteLectureListView.GetItemAtPosition(e.Position).ToString();
                coursesselected = CoursesList[e.Position];
                string course = coursesselected.course;
                Intent gotodellec = new Intent(Application.Context, typeof(DeleteLectureActivity));
                gotodellec.PutExtra("course", course);
                StartActivity(gotodellec);
            }

            private void CoursesListView_ItemClick(object sender, ItemClickEventArgs e)
            {
                string selectedFromList = CoursesListView.GetItemAtPosition(e.Position).ToString();
                coursesselected = CoursesList[e.Position];
                string course = coursesselected.course;
                Intent gotodellec = new Intent(Application.Context, typeof(StudentEvaluationActivity));
                gotodellec.PutExtra("course", course);
                StartActivity(gotodellec);
            }

            private void QuestionListView_ItemClick(object sender, ItemClickEventArgs e)
            {
                string selectedFromList = QuestionListView.GetItemAtPosition(e.Position).ToString();
                coursesselected = CoursesList[e.Position];
                string course = coursesselected.course;
                Intent gotovidup = new Intent(Application.Context, typeof(LecturesQuestionActivity));
                gotovidup.PutExtra("course", course);
                StartActivity(gotovidup);
            }

            public override View OnCreateView(LayoutInflater inflater, ViewGroup container,
                                               Bundle savedInstanceState)
            {
                auth = FirebaseAuth.Instance;
                curruser = "\"" + auth.CurrentUser.Email.ToString() + "\"";
                //Views
                View homeView = inflater.Inflate(Resource.Layout.Home, container, false);
                View newLectureView = inflater.Inflate(Resource.Layout.Newlecture, container, false);
                View profileView = inflater.Inflate(Resource.Layout.Profile, container, false);
                View courseView = inflater.Inflate(Resource.Layout.Courses, container, false);
                View deleteLectureView = inflater.Inflate(Resource.Layout.Deletelecture, container, false);
                View questionview = inflater.Inflate(Resource.Layout.questions, container, false);



                //Initialization
                // Home
                HomeRecycView = homeView.FindViewById<RecyclerView>(Resource.Id.HomeRecyler);
                Homepgb = homeView.FindViewById<ProgressBar>(Resource.Id.Homepgb);
                HomeCard = homeView.FindViewById<CardView>(Resource.Id.Homecard);
                Homeinfoholder = homeView.FindViewById<LinearLayout>(Resource.Id.Homeinfoholder);

                // profile
                ProfileTitle = profileView.FindViewById<TextView>(Resource.Id.profileTitle);
                ProfileName = profileView.FindViewById<TextView>(Resource.Id.profileName);
                ProfileStaffId = profileView.FindViewById<TextView>(Resource.Id.profileStaffID);
                ProfileCollege = profileView.FindViewById<TextView>(Resource.Id.profileCollege);
                ProfileDepartment = profileView.FindViewById<TextView>(Resource.Id.profileDepartment);
                ProfileEmail = profileView.FindViewById<TextView>(Resource.Id.profileEmail);
                Profilepgb = profileView.FindViewById<ProgressBar>(Resource.Id.profilepgb);
                ProfileCard = profileView.FindViewById<CardView>(Resource.Id.profileCard);
                // My Courses 
                CoursesListView = courseView.FindViewById<ListView>(Resource.Id.coursesList);
                courseCard = courseView.FindViewById<CardView>(Resource.Id.courseCard);
                Courselabel = courseView.FindViewById<TextView>(Resource.Id.coursesLabel);
                coursepgb = courseView.FindViewById<ProgressBar>(Resource.Id.coursespgb);
                // New Lecture                 
                NewLectureListView = newLectureView.FindViewById<ListView>(Resource.Id.newlecturelistView);
                NewLectureCard = newLectureView.FindViewById<CardView>(Resource.Id.newlectureCard);
                NewLectureLabel = newLectureView.FindViewById<TextView>(Resource.Id.newlectureLabel);
                NewLecturepgb = newLectureView.FindViewById<ProgressBar>(Resource.Id.newlecturepgb);
                // Questions                 
                QuestionListView = questionview.FindViewById<ListView>(Resource.Id.questionlistView);
                QuestionCard = questionview.FindViewById<CardView>(Resource.Id.questionCard);
                QuestionLabel = questionview.FindViewById<TextView>(Resource.Id.questionLabel);
                Questionpgb = questionview.FindViewById<ProgressBar>(Resource.Id.questionpgb);


                // delete lecture
                DeleteLectureListView = deleteLectureView.FindViewById<ListView>(Resource.Id.deletelecturelistView);
                DeleteLectureCard = deleteLectureView.FindViewById<CardView>(Resource.Id.deletelectureCard);
                DeleteLectureLabel = deleteLectureView.FindViewById<TextView>(Resource.Id.deletelectureLabel);
                DeleteLecturepgb = deleteLectureView.FindViewById<ProgressBar>(Resource.Id.deletelecturepgb);

               
                MainActivity main = new MainActivity();
                var i = this.Arguments.GetInt(ARG_POSITION);
                switch (i)
                {
                    case 0:
                        //dynamic code here
                        new GetLectureSpecifictDataHome(this).Execute(Common.getAddresApiLecturesspecifictitle(curruser));
                        return homeView;
                    case 1:
                        //dynamic code here
                        new GetProfileSpecifictData(this).Execute(Common.getAddresApiProfilespecifictitle(curruser));
                        return profileView;
                    case 2:
                        //dynamic code here
                        new GetCourseSpecifictData(this, NewLectureListView, NewLecturepgb, NewLectureLabel, NewLectureCard).Execute(Common.getAddresApiCoursesspecifictitle(curruser));
                        NewLectureListView.ItemClick += NewLectureListView_ItemClick;
                        return newLectureView;
                    case 3:
                        //dynamic code here    
                        new GetCourseSpecifictData(this, DeleteLectureListView, DeleteLecturepgb, DeleteLectureLabel, DeleteLectureCard).Execute(Common.getAddresApiCoursesspecifictitle(curruser));
                        DeleteLectureListView.ItemClick += DeleteLectureListView_ItemClick;
                        return deleteLectureView;
                    case 4:
                        //dynamic code here
                        new GetCourseSpecifictData(this, CoursesListView, coursepgb, Courselabel, courseCard).Execute(Common.getAddresApiCoursesspecifictitle(curruser));
                        CoursesListView.ItemClick += CoursesListView_ItemClick;
                        return courseView;
                    case 5:
                        //dynamic code here
                        new GetCourseSpecifictData(this, QuestionListView, Questionpgb, QuestionLabel, QuestionCard).Execute(Common.getAddresApiCoursesspecifictitle(curruser));
                        QuestionListView.ItemClick += QuestionListView_ItemClick;
                        return questionview;
                    default:
                        //dynamic code here
                        return profileView;
                }



            }

          
        }

        void NavItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            LayoutInflater inflater = (LayoutInflater)this.GetSystemService(Context.LayoutInflaterService);
            View home = inflater.Inflate(Resource.Layout.Home, null);
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_home):
                    SelectItem(0);
                    // 'Home fragment selection
                    break;
                case (Resource.Id.nav_profile):

                    SelectItem(1);
                    // New laundary fragment selection
                    break;
                case (Resource.Id.nav_new):
                    SelectItem(2);
                    // cancel fragment' selection
                    break;
                case (Resource.Id.nav_cancel):
                    SelectItem(3);
                    // billing info selection
                    break;
                case (Resource.Id.nav_courses):
                    SelectItem(4);
                    break;
                case (Resource.Id.nav_question):
                    SelectItem(5);
                    break;

                case (Resource.Id.nav_settings):
                    StartActivity(typeof(SettingsActivity));
                    break;
                //
                case (Resource.Id.nav_signout):
                    auth.SignOut();
                    StartActivity(typeof(LoginActivity));
                    Finish();
                    break;
            }
            // Close drawer
            drawerLayout.CloseDrawers();
        }




    }
}