﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using SQLite;
using System.IO;
using Xamarin.Forms;
using GazellaMobile.iOS;
using GazellaMobile.Interfaces;

[assembly:Dependency(typeof(SQLConnection_iOS))]
namespace GazellaMobile.iOS
{
    public class SQLConnection_iOS : ISQLConnection
    {
        public SQLiteConnection GetConnection()
        {
            var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryFolder = Path.Combine(documentsFolder, "..", "Library");

            string dbPath = Path.Combine(libraryFolder, App.DbFileName);
            CopyIfNotExist(dbPath);
            return new SQLiteConnection(dbPath);

        }

        private static void CopyIfNotExist(string dbPath)
        {
            if (!File.Exists(dbPath))
            {
                var sourcePath = NSBundle.MainBundle.PathForResource(App.DbName, ".db3");
                File.Copy(sourcePath, dbPath);
            }
        }
    }
}