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
using FlippedTutor.Class;
using Java.Lang;

namespace FlippedTutor.Adapters
{
    class Adapters
    {
    }
    public class BasicListAdapter : BaseAdapter
    {
        private Context mContext;
        private List<DataClass> colleges;


        public BasicListAdapter(Context mContext, List<DataClass> colleges)
        {
            this.mContext = mContext;
            this.colleges = colleges;
        }
        public override int Count
        {
            get
            {
                return colleges.Count();
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);
            View collvw = inflater.Inflate(Resource.Layout.item, null);
            TextView booktext = collvw.FindViewById<TextView>(Resource.Id.itemText);
            booktext.Text = colleges[position].Info.ToString();
            return collvw;

        }
    }
    public class CoursesListAdapter : BaseAdapter
    {
        private Context mContext;
        private List<Courses> course;


        public CoursesListAdapter(Context mContext, List<Courses> course)
        {
            this.mContext = mContext;
            this.course = course;
        }
        public override int Count
        {
            get
            {
                return course.Count();
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);
            View collvw = inflater.Inflate(Resource.Layout.item, null);
            TextView booktext = collvw.FindViewById<TextView>(Resource.Id.itemText);
            booktext.Text = course[position].course.ToString();
            return collvw;

        }
    }
    public class StudentListAdapter : BaseAdapter
    {
        private Context mContext;
        private List<Students> course;


        public StudentListAdapter(Context mContext, List<Students> course)
        {
            this.mContext = mContext;
            this.course = course;
        }
        public override int Count
        {
            get
            {
                return course.Count();
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);
            View collvw = inflater.Inflate(Resource.Layout.item, null);
            TextView booktext = collvw.FindViewById<TextView>(Resource.Id.itemText);
            booktext.Text = course[position].student.ToString();
            return collvw;

        }
    }
    public class LectureListAdapter : BaseAdapter
    {
        private Context mContext;
        private List<Lectures> course;


        public LectureListAdapter(Context mContext, List<Lectures> course)
        {
            this.mContext = mContext;
            this.course = course;
        }
        public override int Count
        {
            get
            {
                return course.Count();
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);
            View collvw = inflater.Inflate(Resource.Layout.item, null);
            TextView booktext = collvw.FindViewById<TextView>(Resource.Id.itemText);
            booktext.Text = course[position].title.ToString();
            return collvw;

        }
    }
    public class QuestionListAdapter : BaseAdapter
    {
        private Context mContext;
        private List<Question> course;


        public QuestionListAdapter(Context mContext, List<Question> course)
        {
            this.mContext = mContext;
            this.course = course;
        }
        public override int Count
        {
            get
            {
                return course.Count();
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);
            View collvw = inflater.Inflate(Resource.Layout.questionItem, null);
            TextView question = collvw.FindViewById<TextView>(Resource.Id.questionitem);
            TextView author = collvw.FindViewById<TextView>(Resource.Id.author);
            question.Text = course[position].question.ToString();
            author.Text = course[position].student.ToString();
            return collvw;

        }
    }

    public class HomeRecycHolder : RecyclerView.ViewHolder
    {
        public TextView Course { get; set; }
        public TextView Title { get; set; }
        public TextView Lecturer { get; set; }
        public HomeRecycHolder(View itemView,Action<int> listener) : base(itemView)
        {
            Course = itemView.FindViewById<TextView>(Resource.Id.homeCourseTitle);
            Title = itemView.FindViewById<TextView>(Resource.Id.homeLectureTopic);
            Lecturer = itemView.FindViewById<TextView>(Resource.Id.homeLecturer);

            itemView.LongClick += (sender, e) => listener(base.Position);
        }

        protected HomeRecycHolder(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
    }
    
    public class HomeRecycAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public List<Lectures> LectureList;
        public HomeRecycAdapter(List<Lectures> LectureList)
        {
            this.LectureList = LectureList; 
        }

        public override int ItemCount => LectureList.Count();
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            HomeRecycHolder vh = holder as HomeRecycHolder;
            vh.Course.Text = LectureList[position].course;
            vh.Lecturer.Text = LectureList[position].lecturer;
            vh.Title.Text = LectureList[position].title;

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
             View uploadvid = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.homeItem,parent ,false);
            HomeRecycHolder vh = new HomeRecycHolder(uploadvid, OnClick);
            return vh;
        }

        private void OnClick(int obj)
        {
            if (ItemClick != null)            
                ItemClick(this, obj);
            
        }
    }

    public class BasicTwoItemListAdapter : BaseAdapter
    {
        private Context mContext;
        private List<TwoDataClass> twodata;


        public BasicTwoItemListAdapter(Context mContext, List<TwoDataClass> twodata)
        {
            this.mContext = mContext;
            this.twodata = twodata;
        }
        public override int Count
        {
            get
            {
                return twodata.Count();
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);
            View collvw = inflater.Inflate(Resource.Layout.ItemTwo, null);
            TextView item = collvw.FindViewById<TextView>(Resource.Id.name);
            TextView itemtwo = collvw.FindViewById<TextView>(Resource.Id.score);
            itemtwo.Text = twodata[position].Infotwo.ToString();
            item.Text = twodata[position].Info.ToString();
            return collvw;

        }
    }

}