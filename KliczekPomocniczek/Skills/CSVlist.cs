//using System.Diagnostics;
//using System.IO;

//namespace KliczekPomocniczek.Skills
//{
//    internal class CSVlist
//    {
//        public static void filesToCSV()
//        {
//            putSomeDataNieChceMiSie putSomeDataNieChceMiSie = new putSomeDataNieChceMiSie();
//            putSomeDataNieChceMiSie.Show();
//            string what = putSomeDataNieChceMiSie.What.ToString();
//            string Where = putSomeDataNieChceMiSie.Where.ToString();
//            string[] files = Directory.GetFiles(what);
//            string[] dirs = Directory.GetDirectories(what);
//            string fileName = Where;
//            if(files.Length > 0 || dirs.Length > 0)
//            {
//            using (var w = new StreamWriter(fileName))
//                {
//                    foreach (string file in files)
//                    {
//                        w.WriteLine(System.IO.Path.GetFileNameWithoutExtension(file) + "\t" + System.IO.Path.GetExtension(file));
//                        w.Flush();
//                    }
//                    foreach (string dir in dirs)
//                    {
//                        w.WriteLine(System.IO.Path.GetFileNameWithoutExtension(dir) + ";" + System.IO.Path.GetExtension(dir));
//                        w.Flush();
//                    }
//                }
//                Process.Start("notepad.exe", fileName);
//            }
//        }
//    }
//}
