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
using Java.Net;
using Java.IO;
using System.IO;

namespace FlippedTutor.Helper
{
    class HttpHandler
    {

        static String stream = null;
        public HttpHandler() { }

        public String GetHttpData(String UrlString)
        {
            try
            {
                URL url = new URL(UrlString);
                HttpURLConnection urlConnection = (HttpURLConnection)url.OpenConnection();
                if (urlConnection.ResponseCode == HttpStatus.Ok)
                {
                    BufferedReader r = new BufferedReader(new InputStreamReader(urlConnection.InputStream));
                    StringBuilder sb = new StringBuilder();
                    String line;
                    while ((line = r.ReadLine()) != null)
                        sb.Append(line);
                    stream = sb.ToString();
                    urlConnection.Disconnect();
                }
            }
            catch (Exception ex) { }
            return stream;

        }

        public void PostHttpData(String urlstring, String json)
        {
            try
            {
                URL url = new URL(urlstring);
                HttpURLConnection urlConnection = (HttpURLConnection)url.OpenConnection();
                urlConnection.RequestMethod = "POST";
                urlConnection.DoOutput = true;

                byte[] _out = Encoding.UTF8.GetBytes(json);
                int length = _out.Length;
                urlConnection.SetFixedLengthStreamingMode(length);
                urlConnection.SetRequestProperty("Content-Type", "application/json");
                urlConnection.SetRequestProperty("charset", "utf-8");
                urlConnection.Connect();
                try
                {
                    Stream str = urlConnection.OutputStream;
                    str.Write(_out, 0, length);
                }
                catch (Exception ex) { }
                var status = urlConnection.ResponseCode;

            }
            catch (Exception ex) { }
        }
        public void PutHttpData(String urlstring, String json)
        {
            try
            {
                URL url = new URL(urlstring);
                HttpURLConnection urlConnection = (HttpURLConnection)url.OpenConnection();
                urlConnection.RequestMethod = "PUT";
                urlConnection.DoOutput = true;

                byte[] _out = Encoding.UTF8.GetBytes(json);
                int length = _out.Length;
                urlConnection.SetFixedLengthStreamingMode(length);
                urlConnection.SetRequestProperty("Content-Type", "application/json");
                urlConnection.SetRequestProperty("charset", "utf-8");
                urlConnection.Connect();
                try
                {
                    Stream str = urlConnection.OutputStream;
                    str.Write(_out, 0, length);
                }
                catch (Exception ex) { }
                var status = urlConnection.ResponseCode;

            }
            catch (Exception ex) { }
        }

        public void DeleteHttpData(String urlstring, String json)
        {
            try
            {
                URL url = new URL(urlstring);
                HttpURLConnection urlConnection = (HttpURLConnection)url.OpenConnection();
                urlConnection.RequestMethod = "DELETE";
                urlConnection.DoOutput = true;

                byte[] _out = Encoding.UTF8.GetBytes(json);
                int length = _out.Length;
                urlConnection.SetFixedLengthStreamingMode(length);
                urlConnection.SetRequestProperty("Content-Type", "application/json");
                urlConnection.SetRequestProperty("charset", "utf-8");
                urlConnection.Connect();
                try
                {
                    Stream str = urlConnection.OutputStream;
                    str.Write(_out, 0, length);
                }
                catch (Exception ex) { }
                var status = urlConnection.ResponseCode;

            }
            catch (Exception ex) { }
        }


    }
}
    
