using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace FlippedTutor.Class
{
    class MlabClass
    {
    }
    public class ID
    {
        [JsonProperty(PropertyName = "$oid")]

        public String oid { get; set; }
    }
    public class Profile
    {
        public ID _id { get; set; }
        
        public string title { get; set;}
        public string name { get; set; }
        public string college { get; set; }
        public string department { get; set; }
        public string email { get; set; }
        public string staffid { get; set; }


    }
    public class Courses
    {
        public ID _id { get; set; }
        public string course { get; set; }
        public string lecturer { get; set; }

    }

    public class Evaluation
    {
        public ID _id { get; set; }
        public string title { get; set; }
        public string lecturer { get; set; }
        public string question { get; set; }
        public string a { get; set; }
        public string b { get; set; }
        public string c { get; set; }
        public string d { get; set; }
        public string answer { get; set; }







    }

    public class Lectures
    {
        public ID _id { get; set; }
        public string course { get; set; }
        public string lecturer { get; set; }
        public string title { get; set; }
        public string vidname { get; set; }

        public string vidurl { get; set; }
        public string notename { get; set; }

        public string noteurl { get; set; }   
    }
    public class StudentEval
    {
        public ID _id { get; set; }
        public string course { get; set; }
        public string lecture { get; set; }
        public string studentname { get; set; }
        public int score { get; set; }

    }
    public class Students
    {
        public ID _id { get; set; }
        public string course { get; set; }
        public string student { get; set; }

    }
    public class Question
    {
        public ID _id { get; set; }
        public string question { get; set; }
        public string student { get; set; }
        public string course { get; set; }
        public string lecture { get; set; }



    }


}