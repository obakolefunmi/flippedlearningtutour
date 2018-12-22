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
using SQLite.Net.Attributes;

namespace FlippedTutor.Class
{
    class FtClass
    {
    }
    public class DataClass
    {
        [PrimaryKey]
        public string Info { get; set; }
    }
    public class TwoDataClass
    {
        [PrimaryKey]
        public string Info { get; set; }
        public int Infotwo { get; set; }

    }

}