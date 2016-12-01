using System;
using Android.App;
using Android.OS;
using System.IO;
using Android.Content;
using SQLite;
using System.Collections.Generic;

namespace App08
{
    [Activity(MainLauncher = true)]
    public class MainActivity : Activity
    {
        private const string PrefsName = "PrefsName";
        private const string PrefKey = "PrefKey";
        private string dbPath = Path.Combine(System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Personal), "myDatabase.db");

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            #region SharedPreferences

           // var prefs = Application.Context.GetSharedPreferences(
           //     PrefsName, FileCreationMode.Private);

            //ReadSharedPreferences(prefs);

            //WriteSharedPreferences(prefs);

            //ReadSharedPreferences(prefs);

            

            #endregion

            #region Files

            //WriteToFile();

            //ReadFromAssets();

            #endregion

            #region Database

            var connection = new SQLiteConnection(dbPath);
            connection.CreateTable<Item>();
            connection.CreateTable<Subitem>();

            InsertToDatabase(connection);

            ReadFromDatabase(connection);

            #endregion
        }

        #region SharedPreferences

        private void WriteSharedPreferences(ISharedPreferences prefs)
        {
            var prefsEditor = prefs.Edit();
            prefsEditor.PutString(PrefKey, "Value");
            prefsEditor.Commit();
        }

        private void ReadSharedPreferences(ISharedPreferences prefs)
        {
            var pref = prefs.GetString(PrefKey, null);
            System.Diagnostics.Debug.WriteLine(pref);
        }

        #endregion

        #region Files

        private void WriteToFile()
        {
            //string filePath = Path.Combine(GetExternalFilesDir(null).AbsolutePath, "example.txt");
            //string filePath = Path.Combine(GetExternalFilesDir(null).AbsoutePath,"example.txt") --> alternatywne
            string filePath = GetFileStreamPath("example.txt").AbsolutePath;

            // Compose a string that consists of three lines.
            string lines = "First line.\r\nSecond line.\r\nThird line.";

            try
            {
                // Write the string to a file.
                using (var streamWriter = new StreamWriter(filePath, true))
                {
                    streamWriter.WriteLine(lines);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void ReadFromAssets()
        {
            using (var stream = Assets.Open("AboutAssets.txt"))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var text = streamReader.ReadToEnd();
                    System.Diagnostics.Debug.WriteLine(text);
                }
            }
        }

        #endregion

        #region Database

        private void InsertToDatabase(SQLiteConnection connection)
        {

            //var li = new List<Subitem>();
            for (int i = 0; i < 1; i++)
            {
                var item = new Item()
                {
                    Name = "name " + i,
                    Description = "description " + i,
                    ShowImage = i % 2 == 0,
                    itemy = new List<Subitem>()
                };
                connection.Insert(item);
                for (int j = 0; j < 4; j++)
                {
                    var subi = new Subitem()
                    {
                        Name = "Imie" + j,
                        ParentId = item.Id
                    };
                    item.itemy.Add(subi);
                    connection.Insert(subi);
                    
                }


                //connection.Insert(item);
            }

        }

        private void ReadFromDatabase(SQLiteConnection connection)
        {

            // DOPISAć wczytywanie!
            var tab = connection.Table<Item>();
            var li = connection.Table<Subitem>();
            
            foreach ( var item in tab)
            {
                foreach(var sub in li)
                {
                    if (sub.ParentId == item.Id)
                    {
                        System.Diagnostics.Debug.WriteLine("{0}",
                    sub.Name);
                    }
                }
            }




          /*  var table = connection.Table<Item>();

            foreach (var item in table)
            {
                System.Diagnostics.Debug.WriteLine("{0} {1}",
                    item.Name, item.Description);
            } */
        }

        #endregion
    }
}
