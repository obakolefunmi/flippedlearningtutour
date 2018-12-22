using System;
using System.IO;
using Android.Content;
using Android.Database.Sqlite;

namespace FlippedTutor.Helper
{
    class DbHelper : SQLiteOpenHelper
    {
        private static string DB_PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        private static string DB_NAME = "School.db";
        private static int VERSION = 1;
        private Context context;


        public DbHelper(Context context) : base(context, DB_NAME, null, VERSION)
        {
            this.context = context;
        }
        private string GetSQLiteDBPath()
        {
            return Path.Combine(DB_PATH, DB_NAME);
        }

        public override SQLiteDatabase WritableDatabase
        {
            get
            {
                return CreateSQLiteDB();
            }
        }

        private SQLiteDatabase CreateSQLiteDB()
        {
            SQLiteDatabase sqllitedb = null;
            string path = GetSQLiteDBPath();
            Stream streamSqlite = null;
            FileStream streamWriter = null;
            Boolean IsSQLiteInit = false;
            try
            {
                if (File.Exists(path))
                {
                    IsSQLiteInit = true;
                }
                else
                {
                    streamSqlite = context.Resources.OpenRawResource(Resource.Raw.School);
                    streamWriter = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                    if (streamSqlite != null && streamWriter != null)
                    {
                        if (CopySQLiteDB(streamSqlite, streamWriter))
                        {
                            IsSQLiteInit = true;
                        }
                    }
                }
                if (IsSQLiteInit)
                    sqllitedb = SQLiteDatabase.OpenDatabase(path, null, DatabaseOpenFlags.OpenReadonly);

            }
            catch { }
            return sqllitedb;
        }

        private bool CopySQLiteDB(Stream streamSqlite, FileStream streamWriter)
        {
            bool isSuccess = false;
            int length = 256;
            Byte[] buffer = new Byte[256];
            try
            {
                int bytesRead = streamSqlite.Read(buffer, 0, length);
                while (bytesRead > 0)
                {
                    streamWriter.Write(buffer, 0, bytesRead);
                    bytesRead = streamSqlite.Read(buffer, 0, length);

                }
                isSuccess = true;


            }
            catch { }
            finally
            {
                streamSqlite.Close();
                streamWriter.Close();
            }
            return isSuccess;
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            throw new NotImplementedException();
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            throw new NotImplementedException();
        }
    }
}