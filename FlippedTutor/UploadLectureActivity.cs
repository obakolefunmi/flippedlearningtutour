using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Gms.Tasks;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Balysv.Material.Drawable.Menu;
using Firebase.Storage;
using FlippedTutor.Adapters;
using FlippedTutor.Class;
using Java.Lang;
using static Android.Views.View;

namespace FlippedTutor
{
    [Activity(Label = "Add New Lecture", Theme = "@style/Theme.Custom")]
    public class UploadLectureActivity : AppCompatActivity, IOnClickListener, IOnProgressListener, IOnFailureListener, IOnSuccessListener, IOnCompleteListener
    {
        string course;
        string Vidname, Vidpath, Vidsize;
        TextView Course_name, done_btn, uploadName, uploadPath, uploadSize;
        EditText uploadEditLectureName;
        FloatingActionButton add_new;
        LinearLayout infoHolder, itemholder;
        // Firebase nd stuff
        ProgressDialog progressDialog;
        FirebaseStorage storage;
        StorageReference Storageref;

        private Android.Net.Uri filePath;
        private const int PICK_IMAGE_REQUEST = 71;
        enum stroke
        {
            REGULAR = 3,

            THIN = 2,
            EXTRA_THIN = 1


        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Uploadlecture);
            var toolbarup = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbaruploadvid);
            SetSupportActionBar(toolbarup);
            MaterialMenuDrawable materialMenu = new MaterialMenuDrawable(this, Color.Purple, (int)stroke.EXTRA_THIN, MaterialMenuDrawable.DefaultScale, MaterialMenuDrawable.DefaultTransformDuration);
            materialMenu.SetIconState(MaterialMenuDrawable.IconState.Arrow);
            toolbarup.NavigationIcon = materialMenu;
            toolbarup.NavigationClick += delegate {
                OnBackPressed();
            };
            Course_name = FindViewById<TextView>(Resource.Id.UploadCourse);
            done_btn = FindViewById<TextView>(Resource.Id.UploadDone);
            uploadName = FindViewById<TextView>(Resource.Id.uploadName);
            uploadPath = FindViewById<TextView>(Resource.Id.uploadPath);
            uploadSize = FindViewById<TextView>(Resource.Id.uploadSize);
            uploadEditLectureName = FindViewById<EditText>(Resource.Id.uploadTitleEdit);
            add_new = FindViewById<FloatingActionButton>(Resource.Id.UploadFab);
            infoHolder = FindViewById<LinearLayout>(Resource.Id.UploadInfoHolder);
            itemholder = FindViewById<LinearLayout>(Resource.Id.uploaditemholder);
            storage = FirebaseStorage.Instance;
            Storageref = storage.GetReferenceFromUrl("gs://flippedcu.appspot.com").Child("Lectures");


            course = Intent.GetStringExtra("course") ?? "";
            Course_name.Text = course;

            done_btn.SetOnClickListener(this);
            add_new.SetOnClickListener(this);


            // Create your application here
        }
        private void UploadVideo()
        {
            progressDialog = new ProgressDialog(this);
            progressDialog.SetTitle("Uploading Video........");
            progressDialog.Window.SetType(Android.Views.WindowManagerTypes.SystemAlert);
            progressDialog.Show();

            var video = Storageref.Child("Video/" + Vidname);

            video.PutFile(filePath).AddOnProgressListener(this).AddOnFailureListener(this).AddOnSuccessListener(this);
        }
        public void OnSuccess(Java.Lang.Object result)
        {
            Storageref.Child("Video/" + Vidname).DownloadUrl.AddOnCompleteListener(this);
           
        }
        public void OnFailure(Java.Lang.Exception e)
        {
            progressDialog.Dismiss();
            string LectureTitle = uploadEditLectureName.Text;
            Intent gotoNote = new Intent(this, typeof(UploadNoteActivity));
            gotoNote.PutExtra("lectureVidName", Vidname);
            gotoNote.PutExtra("lectureVidPath", Vidpath);
            gotoNote.PutExtra("lectureVidSize", Vidsize);
            gotoNote.PutExtra("lectureTitle", LectureTitle);
            gotoNote.PutExtra("course", course);
            StartActivity(gotoNote);
            Finish();
            Toast.MakeText(this, "Failed to upload lecture please check internet connection and try again", ToastLength.Short).Show();
        }

        public void OnProgress(Java.Lang.Object snapshot)
        {
            var taskSnapshot = (UploadTask.TaskSnapshot)snapshot;
            double progress = (100.0 * taskSnapshot.BytesTransferred / taskSnapshot.TotalByteCount);
            progressDialog.SetMessage("Uploaded " + (int)progress + "%");
        }
        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.UploadFab:
                    {
                        chooseVideo();
                        break;
                    }
                case Resource.Id.UploadDone:
                    {
                        if (itemholder.Visibility == ViewStates.Visible)
                        {
                            string LectureTitle = uploadEditLectureName.Text;
                            if (LectureTitle == null)
                            {
                                uploadEditLectureName.SetError("Name this lecture", null);
                            }
                            else
                            {
                                UploadVideo();
                            }

                        }
                        else
                        {
                            Toast.MakeText(this, "Select a Video", ToastLength.Short).Show();
                        }
                        break;
                    }

            }
        }

        private void chooseVideo()
        {
            Intent intent = new Intent();
            intent.SetType("video/*");
            intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(intent, "Select Lecture"), PICK_IMAGE_REQUEST);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == PICK_IMAGE_REQUEST && resultCode == Result.Ok && data != null && data.Data != null)
            {
                filePath = data.Data;
                ICursor returnCursor = ContentResolver.Query(filePath, null, null, null, null);

                try
                {
                    itemholder.Visibility = ViewStates.Visible;
                    infoHolder.Visibility = ViewStates.Gone;

                    int nameIndex = returnCursor.GetColumnIndex(OpenableColumns.DisplayName);
                    int sizeIndex = returnCursor.GetColumnIndex(OpenableColumns.Size);
                    returnCursor.MoveToFirst();

                   Vidname = returnCursor.GetString(nameIndex);
                //   Vidpath = filePath.ToString();
                   Vidsize = Long.ToString(returnCursor.GetLong(sizeIndex)).ToString();
                    
                 
    
                    uploadName.Text = Vidname;
                    uploadPath.Text = Vidpath;
                    uploadSize.Text = Vidsize;
                }
                catch (System.Exception ex)
                {
                    itemholder.Visibility = ViewStates.Gone;
                    infoHolder.Visibility = ViewStates.Visible;
                    Toast.MakeText(this, "Error:"+ex.Message.ToString(), ToastLength.Short).Show();

                }
            }
            else
            {

                itemholder.Visibility = ViewStates.Gone;
                infoHolder.Visibility = ViewStates.Visible;
                Toast.MakeText(this, "Error:", ToastLength.Short).Show();
            }
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                Vidpath=  task.Result.ToString();
                progressDialog.Dismiss();

                string LectureTitle = uploadEditLectureName.Text;
                Intent gotoNote = new Intent(this, typeof(UploadNoteActivity));
                gotoNote.PutExtra("lectureVidName", Vidname);
                gotoNote.PutExtra("lectureVidPath", Vidpath);
                gotoNote.PutExtra("lectureVidSize", Vidsize);
                gotoNote.PutExtra("lectureTitle", LectureTitle);
                gotoNote.PutExtra("course", course);
                StartActivity(gotoNote);
                Finish();
            }
            else
            {
                progressDialog.Dismiss();
            }
        }
    }
}