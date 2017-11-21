using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System.IO;
using Xamarin.Forms;
using GazellaMobile.Droid;
using GazellaMobile.Interfaces;

[assembly:Dependency(typeof(SQLConnection_Android))]
namespace GazellaMobile.Droid
{
     public class SQLConnection_Android : ISQLConnection
    {
        public SQLiteConnection GetConnection()
        {
            var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsFolder, App.DbFileName);
            CopyIfNotExist(path);
            return new SQLiteConnection(path);

        }

        private static void CopyIfNotExist(string dbPath)
        {
            if (!File.Exists(dbPath))
            {
                using (var br = new BinaryReader(Android.App.Application.Context.Assets.Open(App.DbFileName)))
                {
                    using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int length = 0;
                        while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, length);
                        }
                    }

                }
            }


        }
    }
}