﻿using System;
using GazellaMobile.Interfaces;
using GazellaMobile.Droid;
using Xamarin.Forms;
using System.IO;

[assembly:Dependency(typeof(SaveAndLoad))]
namespace GazellaMobile.Droid
{
    public class SaveAndLoad: ISaveAndLoad
    {
        public void SaveText(string filename, string text)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            System.IO.File.WriteAllText(filePath, text);
        }
        public string LoadText(string filename)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            return System.IO.File.ReadAllText(filePath);
        }
    }
}
