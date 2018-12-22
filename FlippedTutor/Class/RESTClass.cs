using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FlippedTutor.Adapters;
using FlippedTutor.Helper;
using Newtonsoft.Json;
using static FlippedTutor.MainActivity;

namespace FlippedTutor.Class
{
    class RESTClass
    {
        
    }
    //get the lecturers courses
    public class GetCourseSpecifictData : AsyncTask<string, Java.Lang.Void, string>
    {

        private NavFragment activity;
        private ListView courselist;
        private ProgressBar pgb;
        private TextView label;
        private CardView holder;
        public GetCourseSpecifictData(NavFragment activity, ListView courselist, ProgressBar pgb, TextView label, CardView holder)
        {
            this.activity = activity;
            this.courselist = courselist;
            this.pgb = pgb;
            this.label = label;
            this.holder = holder;
        }
        protected override void OnPreExecute()
        {
            base.OnPreExecute();
            label.Visibility = ViewStates.Gone;
            holder.Visibility = ViewStates.Gone;
            pgb.Visibility = ViewStates.Visible;


        }

        protected override string RunInBackground(params string[] @params)
        {
            string stream = null;
            string urlstring = @params[0];
            HttpHandler http = new HttpHandler();
            stream = http.GetHttpData(urlstring);
            return stream;
        }
        protected override void OnPostExecute(string result)
        {
            base.OnPostExecute(result);
            try
            {
                activity.CoursesList = JsonConvert.DeserializeObject<List<Courses>>(result);
                CoursesListAdapter adapter = new CoursesListAdapter(Application.Context, activity.CoursesList);
                courselist.Adapter = adapter;
                pgb.Visibility = ViewStates.Gone;
                label.Visibility = ViewStates.Visible;
                holder.Visibility = ViewStates.Visible;
            }
            catch (System.Exception ex)
            {
                //activity.topicacctpull.Visibility = ViewStates.Visible;
            }




        }
    }
    public class GetCourseSpecifictDataAddandDrop : AsyncTask<string, Java.Lang.Void, string>
    {

        private AddandDropActivity activity;
        public GetCourseSpecifictDataAddandDrop(AddandDropActivity activity)
        {
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
            string stream = null;
            string urlstring = @params[0];
            HttpHandler http = new HttpHandler();
            stream = http.GetHttpData(urlstring);
            return stream;
        }
        protected override void OnPostExecute(string result)
        {
            base.OnPostExecute(result);
            try
            {
                activity.coursesleList = JsonConvert.DeserializeObject<List<Courses>>(result);
                for (int i = 0; i < activity.coursesleList.Count(); i++)
                {
                    new DelCoursesData(activity.coursesleList[i], activity).Execute(Common.getAddressSingleCourses(activity.coursesleList[i]));
                }

            }
            catch (System.Exception ex)
            {
               // new GetProfileSpecifictDataAddandDrop(activity).Execute(Common.getAddresApiProfilespecifictitle(activity.curruser));

                //activity.topicacctpull.Visibility = ViewStates.Visible;
            }




        }
    }

    //get the lecturers profile
    public class GetProfileSpecifictData : AsyncTask<string, Java.Lang.Void, string>
    {

        private NavFragment activity;
        
        public GetProfileSpecifictData(NavFragment activity )
        {
            this.activity = activity;
           
        }
        protected override void OnPreExecute()
        {
            base.OnPreExecute();

            activity.ProfileCard.Visibility = ViewStates.Gone;
            activity.Profilepgb.Visibility = ViewStates.Visible;
        }

        protected override string RunInBackground(params string[] @params)
        {
            string stream = null;
            string urlstring = @params[0];
            HttpHandler http = new HttpHandler();
            stream = http.GetHttpData(urlstring);
            return stream;
        }
        protected override void OnPostExecute(string result)
        {
            base.OnPostExecute(result);
            try
            {
                activity.profileList = JsonConvert.DeserializeObject<List<Profile>>(result);
                activity.ProfileEmail.Text = activity.profileList[0].email.ToString();
                activity.ProfileName.Text = activity.profileList[0].name.ToString();
                activity.ProfileStaffId.Text = activity.profileList[0].staffid.ToString();
                activity.ProfileCollege.Text = activity.profileList[0].college.ToString();
                activity.ProfileDepartment.Text = activity.profileList[0].department.ToString();
                activity.ProfileTitle.Text = activity.profileList[0].title.ToString();
                activity.ProfileCard.Visibility = ViewStates.Visible;
                activity.Profilepgb.Visibility = ViewStates.Gone;
                // CoursesListAdapter adapter = new CoursesListAdapter(Application.Context, activity.CoursesList);

            }
            catch (System.Exception ex)
            {
                //activity.topicacctpull.Visibility = ViewStates.Visible;
            }




        }
    }
    public class GetProfileSpecifictDataEdit : AsyncTask<string, Java.Lang.Void, string>
    {

        private EditProfileActivity activity;

        public GetProfileSpecifictDataEdit(EditProfileActivity activity)
        {
            this.activity = activity;

        }
        protected override void OnPreExecute()
        {
            base.OnPreExecute();

            activity.editHolder.Visibility = ViewStates.Gone;
            activity.editpgb.Visibility = ViewStates.Visible;
            activity.edittextEdit.Visibility = ViewStates.Gone;
        }

        protected override string RunInBackground(params string[] @params)
        {
            string stream = null;
            string urlstring = @params[0];
            HttpHandler http = new HttpHandler();
            stream = http.GetHttpData(urlstring);
            return stream;
        }
        protected override void OnPostExecute(string result)
        {
            base.OnPostExecute(result);
            try
            {
                activity.profileList = JsonConvert.DeserializeObject<List<Profile>>(result);
                activity.editemail.Text = activity.profileList[0].email.ToString();
                activity.editname.Text = activity.profileList[0].name.ToString();
                activity.editstaffid.Text = activity.profileList[0].staffid.ToString();
                activity.editcollege.Text = activity.profileList[0].college.ToString();
                activity.editdepartment.Text = activity.profileList[0].department.ToString();
                activity.edittitle.Text = activity.profileList[0].title.ToString();
                activity.editHolder .Visibility = ViewStates.Visible;
                activity.editpgb.Visibility = ViewStates.Gone;
                activity.edittextEdit.Visibility = ViewStates.Visible;


            }
            catch (System.Exception ex)
            {
                Toast.MakeText(activity, "No Internet Connection", ToastLength.Short).Show();
            }




        }
    }

    public class GetProfileSpecifictDataMain : AsyncTask<string, Java.Lang.Void, string>
    {

        private MainActivity activity;
        TextView title, name;
        List<Profile> list;

        public GetProfileSpecifictDataMain(MainActivity activity,List<Profile> list, TextView title,TextView name)
        {
            this.activity = activity;
            this.title = title;
            this.name = name;
            this.list = list;

        }
        protected override void OnPreExecute()
        {
            base.OnPreExecute();

           
        }

        protected override string RunInBackground(params string[] @params)
        {
            string stream = null;
            string urlstring = @params[0];
            HttpHandler http = new HttpHandler();
            stream = http.GetHttpData(urlstring);
            return stream;
        }
        protected override void OnPostExecute(string result)
        {
            base.OnPostExecute(result);
            try
            {
                list = JsonConvert.DeserializeObject<List<Profile>>(result);
                title.Text = list[0].title.ToString();
                name.Text = list[0].name.ToString();
               
                // CoursesListAdapter adapter = new CoursesListAdapter(Application.Context, activity.CoursesList);

            }
            catch (System.Exception ex)
            {
                //activity.topicacctpull.Visibility = ViewStates.Visible;
            }




        }
    }
    public class GetProfileSpecifictDataAddandDrop : AsyncTask<string, Java.Lang.Void, string>
    {

        private AddandDropActivity activity;
       
        

        public GetProfileSpecifictDataAddandDrop(AddandDropActivity activity)
        {
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
            string stream = null;
            string urlstring = @params[0];
            HttpHandler http = new HttpHandler();
            stream = http.GetHttpData(urlstring);
            return stream;
        }
        protected override void OnPostExecute(string result)
        {
            base.OnPostExecute(result);
            try
            {
                activity.profileList = JsonConvert.DeserializeObject<List<Profile>>(result);
                activity.department = activity.profileList[0].department.ToString();
                activity.college = activity.profileList[0].college.ToString();
               // string collss = "\"" + activity.profileList[0].college.ToString() + "\"";
                //string depss = "\"" + activity.profileList[0].department.ToString() + "\"";
               

                activity.adddroppgb.Visibility = ViewStates.Gone;
                activity.adddropholder.Visibility = ViewStates.Visible;


            }
            catch (System.Exception ex)
            {
            }




        }
    }

    //Get Home Data

    public class GetLectureSpecifictDataHome : AsyncTask<string, Java.Lang.Void, string>
    {

        private NavFragment activity;

        public GetLectureSpecifictDataHome(NavFragment activity )
        {
            this.activity = activity;

        }
        protected override void OnPreExecute()
        {
            base.OnPreExecute();

            activity.HomeCard.Visibility = ViewStates.Gone;
            activity.Homepgb.Visibility = ViewStates.Visible;
        }

        protected override string RunInBackground(params string[] @params)
        {
            string stream = null;
            string urlstring = @params[0];
            HttpHandler http = new HttpHandler();
            stream = http.GetHttpData(urlstring);
            return stream;
        }
        protected override void OnPostExecute(string result)
        {
            base.OnPostExecute(result);
            try
            {
                if (result == null)
                {
                    activity.Homeinfoholder.Visibility = ViewStates.Visible;
                    activity.HomeCard.Visibility = ViewStates.Visible;
                    activity.Homepgb.Visibility = ViewStates.Gone;
                    activity.HomeRecycView.Visibility = ViewStates.Gone;

                }
                else
                {
                    activity.lectuerList = JsonConvert.DeserializeObject<List<Lectures>>(result);
                    activity.HomeCard.Visibility = ViewStates.Visible;
                    activity.Homeinfoholder.Visibility = ViewStates.Gone;
                    activity.Homepgb.Visibility = ViewStates.Gone;
                    activity.HomeRecycView.Visibility = ViewStates.Visible;
                    activity.HomeRecycManager = new LinearLayoutManager(Application.Context);
                    activity.HomeRecycView.SetLayoutManager(activity.HomeRecycManager);
                    activity.HomeRecycViewAdapter = new HomeRecycAdapter(activity.lectuerList);
                    activity.HomeRecycView.SetAdapter(activity.HomeRecycViewAdapter);
                    activity.HomeRecycViewAdapter.ItemClick += HomeRecycViewAdapter_ItemClick;


                }

                if (activity.lectuerList != null)
                {

                    activity.HomeCard.Visibility = ViewStates.Visible;
                    activity.Homeinfoholder.Visibility = ViewStates.Gone;
                    activity.Homepgb.Visibility = ViewStates.Gone;
                    activity.HomeRecycView.Visibility = ViewStates.Visible;
                   }
                else
                {
                    activity.Homeinfoholder.Visibility = ViewStates.Visible;
                    activity.HomeCard.Visibility = ViewStates.Visible;
                    activity.Homepgb.Visibility = ViewStates.Gone;
                   activity.HomeRecycView.Visibility = ViewStates.Gone;
                }
            }
            catch (System.Exception ex)
            {
                activity.HomeCard.Visibility = ViewStates.Visible;
                activity.Homepgb.Visibility = ViewStates.Gone;
            }




        }
        public void HomeRecycViewAdapter_ItemClick(object sender, int e)
        {
            Toast.MakeText(Application.Context, "you Clicked position" + e + 1, ToastLength.Short).Show();
        }

    }

    public class GetLectureSpecifictDataDelete : AsyncTask<string, Java.Lang.Void, string>
    {

        private DeleteLectureActivity activity;

        public GetLectureSpecifictDataDelete(DeleteLectureActivity activity)
        {
            this.activity = activity;

        }
        protected override void OnPreExecute()
        {
            base.OnPreExecute();

            activity.delitemHolder.Visibility = ViewStates.Gone;
            activity.lectureListview.Visibility = ViewStates.Gone;
            activity.delpgb.Visibility = ViewStates.Visible;
        }

        protected override string RunInBackground(params string[] @params)
        {
            string stream = null;
            string urlstring = @params[0];
            HttpHandler http = new HttpHandler();
            stream = http.GetHttpData(urlstring);
            return stream;
        }
        protected override void OnPostExecute(string result)
        {
            base.OnPostExecute(result);
            try
            {
                activity.lectureslistss = JsonConvert.DeserializeObject<List<Lectures>>(result);

                if (activity.lectureslistss.Count() == 0)
                {

                    activity.delitemHolder.Visibility = ViewStates.Visible;
                    activity.lectureListview.Visibility = ViewStates.Gone;
                    activity.delpgb.Visibility = ViewStates.Gone;

                }
                else
                {
                    activity.delitemHolder.Visibility = ViewStates.Gone;
                    activity.lectureListview.Visibility = ViewStates.Visible;
                    activity.delpgb.Visibility = ViewStates.Gone;

                    for (int i = 0; i < activity.lectureslistss.Count(); i++)
                    {
                        activity.lecturelist.Add(activity.lectureslistss[i].title);
                    }
                    ArrayAdapter<string> corseadapt = new ArrayAdapter<string>(activity, Android.Resource.Layout.SimpleListItemMultipleChoice, activity.lecturelist);
                    activity.lectureListview.ChoiceMode = ChoiceMode.Multiple;
                    activity.lectureListview.Adapter = corseadapt;


                }
            }
            catch (System.Exception ex)
            {
                activity.delitemHolder.Visibility = ViewStates.Visible;
                activity.lectureListview.Visibility = ViewStates.Gone;
                activity.delpgb.Visibility = ViewStates.Gone;
            }




        }

    }

    public class DelLectureData : AsyncTask<string, Java.Lang.Void, string>
    {
        string newannoun = "";
//
        DeleteLectureActivity activity = new DeleteLectureActivity();
        Lectures lec = null;

        public DelLectureData(Lectures lec, DeleteLectureActivity activity)
        {

            this.activity = activity;
            this.lec = lec;
        }

        protected override void OnPreExecute()
        {
            base.OnPreExecute();
            activity.delitemHolder.Visibility = ViewStates.Gone;
            activity.lectureListview.Visibility = ViewStates.Gone;
            activity.delpgb.Visibility = ViewStates.Visible;


        }

        protected override string RunInBackground(params string[] @params)
        {
            string url = @params[0];
            HttpHandler http = new HttpHandler();
            lec.course = newannoun;
            string json = JsonConvert.SerializeObject(lec);
            http.DeleteHttpData(url, json);
            return string.Empty;
        }
        protected override void OnPostExecute(string result)
        {
            base.OnPostExecute(result);

            activity.delitemHolder.Visibility = ViewStates.Gone;
            activity.lectureListview.Visibility = ViewStates.Visible;
            activity.delpgb.Visibility = ViewStates.Gone;


        }

    }
    public class GetStudentsSpecifictData : AsyncTask<string, Java.Lang.Void, string>
    {

        private StudentEvaluationActivity activity;
        private ListView StudentEvalList;
        private ProgressBar pgb;
        private LinearLayout holder;
        public GetStudentsSpecifictData(StudentEvaluationActivity activity, ListView StudentEvalList, ProgressBar pgb,  LinearLayout holder)
        {
            this.activity = activity;
            this.StudentEvalList = StudentEvalList;
            this.pgb = pgb;
            this.holder = holder;
        }
        protected override void OnPreExecute()
        {
            base.OnPreExecute();
            pgb.Visibility = ViewStates.Visible;
            StudentEvalList.Visibility = ViewStates.Gone;
            holder.Visibility = ViewStates.Gone;
        }

        protected override string RunInBackground(params string[] @params)
        {
            string stream = null;
            string urlstring = @params[0];
            HttpHandler http = new HttpHandler();
            stream = http.GetHttpData(urlstring);
            return stream;
        }
        protected override void OnPostExecute(string result)
        {
            base.OnPostExecute(result);
            try
            {
                activity.studdentEvalList = JsonConvert.DeserializeObject<List<Students>>(result);
                if (activity.studdentEvalList.Count() == 0)
                {
                    pgb.Visibility = ViewStates.Gone;
                    StudentEvalList.Visibility = ViewStates.Gone;
                    holder.Visibility = ViewStates.Visible;

                }
                else
                {
                 
                   StudentListAdapter  adapter = new StudentListAdapter(Application.Context, activity.studdentEvalList);
                    StudentEvalList.Adapter = adapter;
                    pgb.Visibility = ViewStates.Gone;
                    StudentEvalList.Visibility = ViewStates.Visible;
                    holder.Visibility = ViewStates.Gone;
                }
            }
            catch (System.Exception ex)
            {
                pgb.Visibility = ViewStates.Gone;
                StudentEvalList.Visibility = ViewStates.Gone;
                holder.Visibility = ViewStates.Visible;
                Toast.MakeText(activity, "Failed to pull students", ToastLength.Short).Show();

            }




        }
    }
    public class GetStudentsLectureSpecifictData : AsyncTask<string, Java.Lang.Void, string>
    {

        private LectureEvaluationActivity activity;
        private ListView lectureEvalList;
        private ProgressBar pgb;
        private LinearLayout holder;
        public GetStudentsLectureSpecifictData(LectureEvaluationActivity activity, ListView lectureEvalList, ProgressBar pgb, LinearLayout holder)
        {
            this.activity = activity;
            this.lectureEvalList = lectureEvalList;
            this.pgb = pgb;
            this.holder = holder;
        }
        protected override void OnPreExecute()
        {
            base.OnPreExecute();
            pgb.Visibility = ViewStates.Visible;
            lectureEvalList.Visibility = ViewStates.Gone;
            holder.Visibility = ViewStates.Gone;
        }

        protected override string RunInBackground(params string[] @params)
        {
            string stream = null;
            string urlstring = @params[0];
            HttpHandler http = new HttpHandler();
            stream = http.GetHttpData(urlstring);
            return stream;
        }
        protected override void OnPostExecute(string result)
        {
            base.OnPostExecute(result);
            try
            {
                activity.studdentEvalList = JsonConvert.DeserializeObject<List<StudentEval>>(result);
                if (activity.studdentEvalList.Count() == 0)
                {
                    pgb.Visibility = ViewStates.Gone;
                    lectureEvalList.Visibility = ViewStates.Gone;
                    holder.Visibility = ViewStates.Visible;

                }
                else
                {
                    int sum = 0;
                    TwoDataClass data = new TwoDataClass();
                    for (int i = 0; i < activity.studdentEvalList.Count(); i++)
                    {
                        data.Info = activity.studdentEvalList[i].lecture.ToString();
                        sum = sum + activity.studdentEvalList[i].score;
                        data.Infotwo = activity.studdentEvalList[i].score;
                        activity.listdata.Add(data);
                    }
                    activity.setEvalTotal.Text = "Total: " + (sum/ (activity.studdentEvalList.Count()*100)*100+"%");
                    BasicTwoItemListAdapter adapter = new BasicTwoItemListAdapter(Application.Context, activity.listdata);
                    lectureEvalList.Adapter = adapter;
                    pgb.Visibility = ViewStates.Gone;
                    lectureEvalList.Visibility = ViewStates.Visible;
                    holder.Visibility = ViewStates.Gone;
                }
            }
            catch (System.Exception ex)
            {
                //activity.topicacctpull.Visibility = ViewStates.Visible;
            }




        }
    }
    public class EditProfileData : AsyncTask<string, Java.Lang.Void, string>
    {
        string name = "";
        string title = "";

        // string user = EditProfileActivity.currmail;
        EditProfileActivity activity = new EditProfileActivity();
        Profile profileselected = null;

        public EditProfileData(Profile profileselected, string name, string title, EditProfileActivity activity)
        {
            this.name = name;
            this.title = title;

            this.activity = activity;
            this.profileselected = profileselected;
        }

        protected override void OnPreExecute()
        {
            base.OnPreExecute();
            activity.editpgb.Visibility = ViewStates.Visible;
            activity.editHolder.Visibility = ViewStates.Gone;


        }

        protected override string RunInBackground(params string[] @params)
        {
            string url = @params[0];
            HttpHandler http = new HttpHandler();
            profileselected.title = title;
            profileselected.name = name;

            string json = JsonConvert.SerializeObject(profileselected);
            http.PutHttpData(url, json);
            return string.Empty;
        }
        protected override void OnPostExecute(string result)
        {
            base.OnPostExecute(result);

            activity.editpgb.Visibility = ViewStates.Gone;
            activity.editHolder.Visibility = ViewStates.Visible;

        }

    }
    public class DelCoursesData : AsyncTask<string, Java.Lang.Void, string>
    {
        string newannoun = "";
        //
        AddandDropActivity activity = new AddandDropActivity();
        Courses lec = null;

        public DelCoursesData(Courses lec, AddandDropActivity activity)
        {

            this.activity = activity;
            this.lec = lec;
        }

        protected override void OnPreExecute()
        {
            base.OnPreExecute();
            


        }

        protected override string RunInBackground(params string[] @params)
        {
            string url = @params[0];
            HttpHandler http = new HttpHandler();
            lec.course = newannoun;
            string json = JsonConvert.SerializeObject(lec);
            http.DeleteHttpData(url, json);
            return string.Empty;
        }
        protected override void OnPostExecute(string result)
        {
            base.OnPostExecute(result);




        }

    }
    public class GetLectureSpecifictDataLectures : AsyncTask<string, Java.Lang.Void, string>
    {

        private LecturesQuestionActivity activity;

        public GetLectureSpecifictDataLectures(LecturesQuestionActivity activity)
        {
            this.activity = activity;

        }
        protected override void OnPreExecute()
        {
            base.OnPreExecute();

            activity.delitemHolder.Visibility = ViewStates.Gone;
            activity.lectureListview.Visibility = ViewStates.Gone;
            activity.delpgb.Visibility = ViewStates.Visible;
        }

        protected override string RunInBackground(params string[] @params)
        {
            string stream = null;
            string urlstring = @params[0];
            HttpHandler http = new HttpHandler();
            stream = http.GetHttpData(urlstring);
            return stream;
        }
        protected override void OnPostExecute(string result)
        {
            base.OnPostExecute(result);
            try
            {
                activity.lectureslistss = JsonConvert.DeserializeObject<List<Lectures>>(result);

                if (activity.lectureslistss.Count() == 0)
                {

                    activity.delitemHolder.Visibility = ViewStates.Visible;
                    activity.lectureListview.Visibility = ViewStates.Gone;
                    activity.delpgb.Visibility = ViewStates.Gone;

                }
                else
                {
                    activity.delitemHolder.Visibility = ViewStates.Gone;
                    activity.lectureListview.Visibility = ViewStates.Visible;
                    activity.delpgb.Visibility = ViewStates.Gone;
                    LectureListAdapter corseadapt = new LectureListAdapter(Application.Context, activity.lectureslistss);
                    activity.lectureListview.Adapter = corseadapt;


                }
            }
            catch (System.Exception ex)
            {
                activity.delitemHolder.Visibility = ViewStates.Visible;
                activity.lectureListview.Visibility = ViewStates.Gone;
                activity.delpgb.Visibility = ViewStates.Gone;
            }




        }

    }
    public class GetQuestionSpecifictData : AsyncTask<string, Java.Lang.Void, string>
    {

        private QuestionsActivity activity;
        private ListView list;
        private ProgressBar pgb;
        private LinearLayout holder , holder2;
        public GetQuestionSpecifictData(QuestionsActivity activity, ListView list, ProgressBar pgb, LinearLayout holder, LinearLayout holder2)
        {
            this.activity = activity;
            this.list = list;
            this.pgb = pgb;          
            this.holder = holder;
            this.holder2 = holder2;

        }
        protected override void OnPreExecute()
        {
            base.OnPreExecute();
            pgb.Visibility = ViewStates.Visible;            
            holder.Visibility = ViewStates.Gone;
            holder2.Visibility = ViewStates.Gone;

        }

        protected override string RunInBackground(params string[] @params)
        {
            string stream = null;
            string urlstring = @params[0];
            HttpHandler http = new HttpHandler();
            stream = http.GetHttpData(urlstring);
            return stream;
        }
        protected override void OnPostExecute(string result)
        {
            base.OnPostExecute(result);
            try
            {
                activity.questionList = JsonConvert.DeserializeObject<List<Question>>(result);
                if (activity.questionList.Count != 0)
                {
                    QuestionListAdapter adapter = new QuestionListAdapter(Application.Context, activity.questionList);
                    list.Adapter = adapter;
                    pgb.Visibility = ViewStates.Gone;
                    holder.Visibility = ViewStates.Visible;
                    holder2.Visibility = ViewStates.Gone;

                }
                else
                {
                    pgb.Visibility = ViewStates.Gone;
                    holder.Visibility = ViewStates.Gone;
                    holder2.Visibility = ViewStates.Visible;

                }
            }
            catch (System.Exception ex)
            {
                //activity.topicacctpull.Visibility = ViewStates.Visible;
            }




        }
    }






}