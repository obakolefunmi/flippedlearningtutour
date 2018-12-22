using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Balysv.Material.Drawable.Menu;
using Firebase;
using Firebase.Auth;
using Firebase.Storage;
using FlippedTutor.Class;
using FlippedTutor.Helper;
using Java.Lang;
using Newtonsoft.Json;
using static Android.Views.View;

namespace FlippedTutor
{
    [Activity(Label = "Set Evaluation", Theme = "@style/Theme.Custom")]
    public class SetEvaluationActivity :AppCompatActivity, IOnClickListener 
    {
        enum stroke
        {
            REGULAR = 3,

            THIN = 2,
            EXTRA_THIN = 1


        }
        FirebaseAuth mAuth;
        EditText SetEval_Question, SetEval_OptionA, SetEval_OptionB, SetEval_OptionC, SetEval_OptionD ;
        TextView NumOfCoursesAdded, setEvalDone;
        FloatingActionButton AddEvalFab;
        Evaluation eval = new Evaluation();
        List<Evaluation> evalList = new List<Evaluation>();
        ProgressBar setEvalpgb;
        ScrollView setEvalscroll;
        Spinner Answers;
        string Answer, uemail, Course, Vidname, Vidpath, Vidsize, LecTitle, notename, notepath, notesize;
        private Android.Net.Uri Vidfilepath, notefilepath;

        int count = 0;
        // Firebase nd stuff
        ProgressDialog progressDialog;
        FirebaseStorage storage;
        StorageReference Storageref;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SetEvaluation);
            var toolbarup = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbarsetEval);
            SetSupportActionBar(toolbarup);
            MaterialMenuDrawable materialMenu = new MaterialMenuDrawable(this, Color.Purple, (int)stroke.EXTRA_THIN, MaterialMenuDrawable.DefaultScale, MaterialMenuDrawable.DefaultTransformDuration);
            materialMenu.SetIconState(MaterialMenuDrawable.IconState.Arrow);
            toolbarup.NavigationIcon = materialMenu;
            toolbarup.NavigationClick += delegate {
                OnBackPressed();
                Finish();
            };
            // firebase Initioalization.
            FirebaseApp.InitializeApp(this);
            mAuth = FirebaseAuth.Instance;
            uemail = mAuth.CurrentUser.Email.ToString();
            NumOfCoursesAdded = FindViewById<TextView>(Resource.Id.setEvalNumofQuests);
            SetEval_Question = FindViewById<EditText>(Resource.Id.setEvalQuestion);
            SetEval_OptionA= FindViewById<EditText>(Resource.Id.setEvalOptionA);
            SetEval_OptionB = FindViewById<EditText>(Resource.Id.setEvalOptionB);
            SetEval_OptionC = FindViewById<EditText>(Resource.Id.setEvalOptionC);
            SetEval_OptionD = FindViewById<EditText>(Resource.Id.setEvalOptionD);
            AddEvalFab = FindViewById<FloatingActionButton>(Resource.Id.setEvalFab);
            setEvalpgb = FindViewById<ProgressBar>(Resource.Id.SetEvalpgb);
            setEvalscroll = FindViewById<ScrollView>(Resource.Id.setEvalScroll);
            AddEvalFab.SetOnClickListener(this);
            setEvalDone = FindViewById<TextView>(Resource.Id.setEvalDone);
            setEvalDone.SetOnClickListener(this);
            // Create your application here
            Answers = FindViewById<Spinner>(Resource.Id.setEvalSpinner);

            Answers.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.Options, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            Answers.Adapter = adapter;
            // Get extras
            Vidname = Intent.GetStringExtra("lectureVidName") ?? "";
            Vidpath = Intent.GetStringExtra("lectureVidPath") ?? "";
            Vidsize = Intent.GetStringExtra("lectureVidSize") ?? "";
            LecTitle = Intent.GetStringExtra("lectureTitle") ?? "";
            notename = Intent.GetStringExtra("lectureLecName") ?? "";
            notepath = Intent.GetStringExtra("lectureLecPath") ?? "";
            notesize = Intent.GetStringExtra("lectureLecSize") ?? "";
            Course = Intent.GetStringExtra("course") ?? "";
            //Vidfilepath = (Android.Net.Uri)Vidpath;
            //notefilepath = (Android.Net.Uri)notepath;
        }

        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.setEvalFab:
                    {
                            AddQuestion();
                        
                        break;
                    }
                case Resource.Id.setEvalDone:
                    {

                        
                        
                            if (count < 1)
                            {
                                Toast.MakeText(this, "Please upload at least 1 question", ToastLength.Short).Show();

                            }
                            else
                            {
                                UploadLecture();

                            }

                        
                        break;
                    }
            }
        }

        private void UploadLecture()
        {

            setEvalscroll.Visibility = ViewStates.Gone;
            setEvalpgb.Visibility = ViewStates.Visible;
            new AddLecture(this, uemail, Course, LecTitle, Vidpath.ToString(), Vidname.ToString(),notepath.ToString(),notename.ToString()).Execute(Common.getAddresApiLectures());

            for (int i = 0; i < evalList.Count(); i++)
            {
                new AddEvaluation(evalList[i].lecturer, evalList[i].title, evalList[i].question, evalList[i].a, evalList[i].b, evalList[i].c, evalList[i].d, evalList[i].answer, this).Execute(Common.getAddresApiEvaluation());
            }
            setEvalscroll.Visibility = ViewStates.Visible;
            setEvalpgb.Visibility = ViewStates.Gone;
            StartActivity(typeof(MainActivity));
            Finish();

        }




        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            Answer = spinner.GetItemAtPosition(e.Position).ToString();
        }
        private void AddQuestion()
        {
         

            if (SetEval_Question.Text.ToString().Trim() == "")
            {
                SetEval_Question.SetError("Required", null);
                SetEval_Question.RequestFocus();
            }
            else if (SetEval_OptionA.Text.ToString().Trim() == "")
            {
                SetEval_OptionA.SetError("Required", null);
                SetEval_OptionA.RequestFocus();
            }
           else if (SetEval_OptionB.Text.ToString().Trim() == "")
            {
                SetEval_OptionB.SetError("Required", null);
                SetEval_OptionB.RequestFocus();
            }
       
           else if (SetEval_OptionC.Text.ToString().Trim() == "")
            {
                SetEval_OptionC.SetError("Required", null);
                SetEval_OptionC.RequestFocus();
            }
            else if (SetEval_OptionD.Text.ToString().Trim() == "")
            {
                SetEval_OptionD.SetError("Required", null);
                SetEval_OptionD.RequestFocus();
            }
            else
            {
                eval.question = SetEval_Question.Text.ToString();
                eval.a = SetEval_OptionA.Text.ToString();
                eval.b = SetEval_OptionB.Text.ToString();
                eval.c = SetEval_OptionC.Text.ToString();
                eval.d = SetEval_OptionD.Text.ToString();
                eval.answer = Answer;
                eval.lecturer = uemail;
                eval.title = LecTitle;
                evalList.Add(eval);
                count++;
                NumOfCoursesAdded.Text = "You have added " + evalList.Count + " question(s)";

                SetEval_Question.Text = "";
                SetEval_OptionA.Text = "";
                SetEval_OptionB.Text = "";
                SetEval_OptionC.Text = "";
                SetEval_OptionD.Text = "";
                SetEval_Question.RequestFocus();
 
            }


        }

    


        private class AddLecture : AsyncTask<string, Java.Lang.Void, string>
        {
            string course = "";
            string lecturer = "";
            string title = "";
            string noteurl = "";
            string notename = "";
            string vidurl = "";
            string vidname = "";

            SetEvaluationActivity activity = new SetEvaluationActivity();
            public AddLecture( SetEvaluationActivity activity, string lecturer, string course,string title,string vidurl,string vidname,string noteurl, string notename)
            {
                this.activity = activity;
                this.lecturer = lecturer;
                this.course = course;
                this.title = title;
                this.vidurl = vidurl;
                this.noteurl = noteurl;
                this.vidname = vidname;
                this.notename = notename;
            }

            protected override void OnPreExecute()
            {
                base.OnPreExecute();

            }
            protected override string RunInBackground(params string[] @params)
            {
                string url = @params[0];
                HttpHandler http = new HttpHandler();
                Lectures lectures = new Lectures();
                lectures.course = course;
                lectures.lecturer = lecturer;
                lectures.title = title;
                lectures.vidurl = vidurl;
                lectures.notename = notename;
                lectures.vidname = vidname;
                lectures.noteurl = noteurl;
               string json = JsonConvert.SerializeObject(lectures);
                http.PostHttpData(url, json);
                return string.Empty;
            }
            protected override void OnPostExecute(string result)
            {
                base.OnPostExecute(result);

            }

        }
        private class AddEvaluation : AsyncTask<string, Java.Lang.Void, string>
        {
            string lecturer = "";
            string title = "";
            string question = "";
            string a = "";
            string b = "";
            string c = "";
            string d = "";
            string answer = "";

            SetEvaluationActivity activity = new SetEvaluationActivity();
            public AddEvaluation(string lecturer, string title, string question, string a, string b, string c,string d, string answer, SetEvaluationActivity activity)
            {

                this.lecturer = lecturer;
                this.title = title;
                this.question = question;
                this.a = a;
                this.b = b;
                this.c = c;
                this.d = d;
                this.answer = answer;
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
                Evaluation evaluation = new Evaluation();
                evaluation.lecturer = lecturer;
                evaluation.title = title;
                evaluation.question = question;
                evaluation.answer = answer;
                evaluation.a = a;
                evaluation.b = b;
                evaluation.c = c;
                evaluation.d = d;

                string json = JsonConvert.SerializeObject(evaluation);
                http.PostHttpData(url, json);
                return string.Empty;
            }
            protected override void OnPostExecute(string result)
            {
                base.OnPostExecute(result);

            }

        }

    }
}