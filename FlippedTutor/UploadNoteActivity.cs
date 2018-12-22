using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Gms.Tasks;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Balysv.Material.Drawable.Menu;
using Firebase;
using Firebase.Storage;
using Java.Lang;
using static Android.Views.View;

namespace FlippedTutor
{
    [Activity(Label = "Upload Note", Theme = "@style/Theme.Custom")]
    public class UploadNoteActivity : AppCompatActivity, IOnClickListener, IOnProgressListener, IOnFailureListener, IOnSuccessListener, IOnCompleteListener
    {

        string course;
        string Vidname, Vidpath, Vidsize, LecTitle, Lecname, Lecpath, Lecsize;
        TextView NoteCourse_name, Notedone_btn, uploadnoteName, uploadnotePath, uploadnoteSize, uploadNoteTitle;
        //  EditText uploadEditLectureName;
        FloatingActionButton Noteadd_new;
        LinearLayout NoteinfoHolder, Noteitemholder;
        private Android.Net.Uri filePath;
        // Firebase nd stuff
        ProgressDialog progressDialog;
        FirebaseStorage storage;
        StorageReference Storageref;

        private const int PICK_DOCS_REQUEST = 71;
        enum stroke
        {
            REGULAR = 3,

            THIN = 2,
            EXTRA_THIN = 1


        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.uploadNoteLecture);
            var toolbarup = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbaruploadnote);
            SetSupportActionBar(toolbarup);
            MaterialMenuDrawable materialMenu = new MaterialMenuDrawable(this, Color.Purple, (int)stroke.EXTRA_THIN, MaterialMenuDrawable.DefaultScale, MaterialMenuDrawable.DefaultTransformDuration);
            materialMenu.SetIconState(MaterialMenuDrawable.IconState.Arrow);
            toolbarup.NavigationIcon = materialMenu;
            toolbarup.NavigationClick += delegate {
                OnBackPressed();
            };
            NoteCourse_name = FindViewById<TextView>(Resource.Id.UploadNoteCourse);
            Notedone_btn = FindViewById<TextView>(Resource.Id.UploadNoteDone);
            uploadnoteName = FindViewById<TextView>(Resource.Id.uploadNoteName);
            uploadnotePath = FindViewById<TextView>(Resource.Id.uploadNotePath);
            uploadnoteSize = FindViewById<TextView>(Resource.Id.uploadNoteSize);
            uploadNoteTitle = FindViewById<TextView>(Resource.Id.uploadNoteLectureTitle);
            Noteadd_new = FindViewById<FloatingActionButton>(Resource.Id.UploadNotesFab);
            NoteinfoHolder = FindViewById<LinearLayout>(Resource.Id.UploadInfoNotesHolder);
            Noteitemholder = FindViewById<LinearLayout>(Resource.Id.uploaditemNoteholder);

            FirebaseApp.InitializeApp(this);
            storage = FirebaseStorage.Instance;
            Storageref = storage.GetReferenceFromUrl("gs://flippedcu.appspot.com").Child("Lectures");


            Vidname = Intent.GetStringExtra("lectureVidName") ?? "";
            Vidpath = Intent.GetStringExtra("lectureVidPath") ?? "";
            Vidsize = Intent.GetStringExtra("lectureVidSize") ?? "";
            LecTitle = Intent.GetStringExtra("lectureTitle") ?? "";
            course = Intent.GetStringExtra("course") ?? "";
            NoteCourse_name.Text = course;
            uploadNoteTitle.Text = LecTitle;

            Notedone_btn.SetOnClickListener(this);
            Noteadd_new.SetOnClickListener(this);


            // Create your application here
        }

        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.UploadNotesFab:
                    {
                        chooseNote();
                        break;
                    }
                case Resource.Id.UploadNoteDone:
                    {
                        if (Noteitemholder.Visibility == ViewStates.Visible)
                        {

                            UploadNote();

                        }
                        else
                        {
                            Toast.MakeText(this, "Select a Note", ToastLength.Short).Show();
                        }



                        break;
                    }

            }
        }
        private void UploadNote()
        {
            progressDialog = new ProgressDialog(this);
            progressDialog.SetTitle("Uploading Note........");
            progressDialog.Window.SetType(Android.Views.WindowManagerTypes.SystemAlert);
            progressDialog.Show();

            var video = Storageref.Child("Notes/" + Lecname);
            video.PutFile(filePath).AddOnProgressListener(this).AddOnFailureListener(this).AddOnSuccessListener(this);
        }
        public void OnSuccess(Java.Lang.Object result)
        {
            progressDialog.Dismiss();
            Storageref.Child("Notes/" + Lecname).DownloadUrl.AddOnCompleteListener(this);

           
        }
        public void OnFailure(Java.Lang.Exception e)
        {
            progressDialog.Dismiss();
            Intent gotoEval = new Intent(this, typeof(SetEvaluationActivity));
            gotoEval.PutExtra("lectureVidName", Vidname);
            gotoEval.PutExtra("lectureVidPath", Vidpath);
            gotoEval.PutExtra("lectureVidSize", Vidsize);
            gotoEval.PutExtra("lectureLecName", Lecname);
            gotoEval.PutExtra("lectureLecPath", Lecpath);
            gotoEval.PutExtra("lectureLecSize", Lecsize);
            gotoEval.PutExtra("lectureTitle", LecTitle);
            gotoEval.PutExtra("course", course);
            StartActivity(gotoEval);
            Finish();
            Toast.MakeText(this, "Failed to upload lecture please check internet connection and try again", ToastLength.Short).Show();
        }

        public void OnProgress(Java.Lang.Object snapshot)
        {
            var taskSnapshot = (UploadTask.TaskSnapshot)snapshot;
            double progress = (100.0 * taskSnapshot.BytesTransferred / taskSnapshot.TotalByteCount);
            progressDialog.SetMessage("Uploaded " + (int)progress + "%");
        }


        private void chooseNote()
        {
            Intent intent = new Intent();
            intent.SetType("application/*");
            intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(intent, "Select Lecture"), PICK_DOCS_REQUEST);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == PICK_DOCS_REQUEST && resultCode == Result.Ok && data != null && data.Data != null)
            {
                filePath = data.Data;
                ICursor returnCursor = ContentResolver.Query(filePath, null, null, null, null);

                try
                {
                    Noteitemholder.Visibility = ViewStates.Visible;
                    NoteinfoHolder.Visibility = ViewStates.Gone;

                    int nameIndex = returnCursor.GetColumnIndex(OpenableColumns.DisplayName);
                    int sizeIndex = returnCursor.GetColumnIndex(OpenableColumns.Size);
                    returnCursor.MoveToFirst();

                    string name = returnCursor.GetString(nameIndex);
                    string path = filePath.ToString();
                    string size = Long.ToString(returnCursor.GetLong(sizeIndex)).ToString();
                    string type = data.Data.GetType().ToString();




                    uploadnoteName.Text = name;
                    uploadnotePath.Text = path;
                    uploadnoteSize.Text = size;
                    uploadNoteTitle.Text = type;

                }
                catch (System.Exception ex)
                {
                    Noteitemholder.Visibility = ViewStates.Gone;
                    NoteinfoHolder.Visibility = ViewStates.Visible;
                    Toast.MakeText(this, "Error:" + ex.Message.ToString(), ToastLength.Short).Show();

                }
            }
        }
        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                Lecpath = task.Result.ToString();
                progressDialog.Dismiss();
                Intent gotoEval = new Intent(this, typeof(SetEvaluationActivity));
                gotoEval.PutExtra("lectureVidName", Vidname);
                gotoEval.PutExtra("lectureVidPath", Vidpath);
                gotoEval.PutExtra("lectureVidSize", Vidsize);
                gotoEval.PutExtra("lectureLecName", Lecname);
                gotoEval.PutExtra("lectureLecPath", Lecpath);
                gotoEval.PutExtra("lectureLecSize", Lecsize);
                gotoEval.PutExtra("lectureTitle", LecTitle);
                gotoEval.PutExtra("course", course);
                StartActivity(gotoEval);
                Finish();

                Finish();
            }
            else
            {
                progressDialog.Dismiss();
            }
        }

    }
}
