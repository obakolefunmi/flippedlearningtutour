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

namespace FlippedTutor.Class
{
    class Common
    {
        private static String DB_NAME = "flippedcu";
        private static String API_KEY = "rHl3vntM5CRZRgRRyjKsXVtyXUlwKddr";
        private static String PROFILE_COLLECTION_NAME = "profile";
        private static String COURSES_COLLECTION_NAME = "courses";
        private static String EVALUATION_COLLECTION_NAME = "evaluation";
        private static String LECTURES_COLLECTION_NAME = "lectures";
        private static String STUDENT_EVAL_COLLECTION_NAME = "studentevaluation";
        private static String STUDENT_COLLECTION_NAME = "studentcourses";
        private static String QUESTIONS_COLLECTION_NAME = "questions";


        //Profile
        public static string getAddressSingleProfile(Profile profile)
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{PROFILE_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("/" + profile._id.oid + "?apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }
        public static string getAddresApiProfile()
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{PROFILE_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("?apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }
        public static string getAddresApiProfilespecifictitle(string email)
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{PROFILE_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("?q={email:" + email + "}&apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }
        //Courses
        public static string getAddressSingleCourses(Courses courses)
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{COURSES_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("/" + courses._id.oid + "?apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }
        public static string getAddresApiCourses()
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{COURSES_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("?apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }
        public static string getAddresApiCoursesspecifictitle(string lecturer)
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{COURSES_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("?q={lecturer:" + lecturer + "}&apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }

        // Lectures
        public static string getAddressSingleLectures(Lectures courses)
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{LECTURES_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("/" + courses._id.oid + "?apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }
        public static string getAddresApiLectures()
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{LECTURES_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("?apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }
        public static string getAddresApiLecturesspecifictitle(string lecturer)
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{LECTURES_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("?q={lecturer:" + lecturer + "}&apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }
        public static string getAddresApiLecturesspecificdelete(string lecturer,string course)
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{LECTURES_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("?q={lecturer:" + lecturer + ",course:"+course+"}&apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }

        // Evaluation
        public static string getAddressSingleEvaluation(Evaluation courses)
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{EVALUATION_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("/" + courses._id.oid + "?apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }
        public static string getAddresApiEvaluation()
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{EVALUATION_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("?apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }
        public static string getAddresApiEvaluationspecifictitle(string lecturer)
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{EVALUATION_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("?q={lecturer:" + lecturer + "}&apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }

        // Student Eval
        public static string getAddresApiStudentEvalspecifics(string name, string course)
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{STUDENT_EVAL_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("?q={studentname:" + name + ",course:" + course + "}&apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }
        public static string getAddresApiStudentEvalspecific(string course)
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{STUDENT_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("?q={course:" + course + "}&apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }

        //Lectures
        
        public static string getAddresApiLecturesspecifictitle()
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{LECTURES_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("?apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }
        public static string getAddresApiLecturesspecificdelete(string course)
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{LECTURES_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("?q={course:" + course + "}&apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }
        //Question
        public static string getAddressSingleQuestions(Profile profile)
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{QUESTIONS_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("/" + profile._id.oid + "?apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }
        public static string getAddresApiQuestions()
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{QUESTIONS_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("?apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }
        public static string getAddresApiQuestionsspecific(string lecture)
        {
            String baseUrl = $"https://api.mlab.com/api/1/databases/{DB_NAME}/collections/{QUESTIONS_COLLECTION_NAME}";
            StringBuilder stringBuilder = new StringBuilder(baseUrl);
            stringBuilder.Append("?q={lecture:" + lecture + "}&apiKey=" + API_KEY);
            return stringBuilder.ToString();
        }

    }
}