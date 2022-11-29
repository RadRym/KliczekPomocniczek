using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace KliczekPomocniczek.Skills
{
    internal class CSVlist
    {
        public static void filesToCSV(string LocalizationOfFiles, string LocalizationOfSavedList, string ListName, bool OpenListAfterCreating)
        {
            try
            {
                string[] files = Directory.GetFiles(LocalizationOfFiles);
                string[] dirs = Directory.GetDirectories(LocalizationOfFiles);
                string fileName = LocalizationOfSavedList + "/" + ListName + ".csv";
                if (files.Length > 0 || dirs.Length > 0)
                {
                    using (var w = new StreamWriter(fileName))
                    {
                        foreach (string file in files)
                        {
                            w.WriteLine(System.IO.Path.GetFileNameWithoutExtension(file) + "\t" + System.IO.Path.GetExtension(file));
                            w.Flush();
                        }
                        foreach (string dir in dirs)
                        {
                            w.WriteLine(System.IO.Path.GetFileNameWithoutExtension(dir) + ";" + System.IO.Path.GetExtension(dir));
                            w.Flush();
                        }
                    }
                    if (OpenListAfterCreating == true)
                        Process.Start("notepad.exe", fileName);
                }
            }
            catch
            {
                MessageBox.Show(" Coś nie wyszło, jest to oczywiście twoja wina, nie programu. \n Spróbuj wypełnić komórki! ");
            }
        }
    }
}
