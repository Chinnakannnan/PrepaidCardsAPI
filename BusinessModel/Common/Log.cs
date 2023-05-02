using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;

namespace BusinessModel.Common
{
    public class Log
    {
        public string LogPath = AppDomain.CurrentDomain.BaseDirectory + "\\";
        public string Curtime = DateTime.Now.ToString();

        public static string LogPath1 = AppDomain.CurrentDomain.BaseDirectory + "\\";
        public static string Curtime1 = DateTime.Now.ToString();

        public void WriteAppLog(string content, string FolderName)
        {
            string FilePath = string.Empty;
            CultureInfo ci = new CultureInfo("en-US");
            //CultureInfo ci = new CultureInfo("en-GB");
            try
            {
                bool append = true;


                string path = LogPath + "\\APPLOG\\" + FolderName + "\\" + string.Format("{0:dd_MM_yyyy}", DateTime.Now) + "\\";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (DateTime.Parse(DateTime.Now.ToString(), ci) <= DateTime.Parse(DateTime.Parse(Curtime).AddMinutes(1).ToString(), ci))
                {
                    FilePath = path + DateTime.Parse(Curtime).ToString("ddMMMyyyy_HH_mm_tt") + ".txt";
                }
                else
                {
                    Curtime = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    FilePath = path + DateTime.Parse(Curtime).ToString("ddMMMyyyy_HH_mm_tt") + ".txt";
                }

                if (!File.Exists(FilePath))
                {
                    using (FileStream fs = File.Create(FilePath)) { append = false; }
                }

                using (StreamWriter file = new System.IO.StreamWriter(FilePath, append))
                {
                    file.WriteLine(DateTime.Now.ToString() + ":" + Environment.NewLine + content);
                }
            }
            catch (Exception ex)
            {

            }
        }



        public void WriteErrorLog(string content, string FolderName)
        {
            string FilePath = string.Empty;
            // ci = new CultureInfo("en-US");
            CultureInfo ci = new CultureInfo("en-GB");
            try
            {
                bool append = true;
                string path = LogPath + "\\ERRORLOG\\" + FolderName + "\\" + string.Format("{0:dd_MM_yyyy}", DateTime.Now) + "\\";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //DateTime dt = DateTime.ParseExact(str, "dd/MM/yyyy HH:mm:ss", null); DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")
                if (DateTime.Parse(DateTime.Now.ToString(), ci) <= DateTime.Parse(DateTime.Parse(Curtime).AddMinutes(1).ToString(), ci))
                {
                    FilePath = path + DateTime.Parse(Curtime).ToString("ddMMMyyyy_HH_mm_tt") + ".txt";
                }
                else
                {
                    Curtime = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    FilePath = path + DateTime.Parse(Curtime).ToString("ddMMMyyyy_HH_mm_tt") + ".txt";
                }

                if (!File.Exists(FilePath))
                {
                    using (FileStream fs = File.Create(FilePath))
                    {
                        append = false;
                    }
                }
                using (StreamWriter file = new System.IO.StreamWriter(FilePath, append))
                {
                    file.WriteLine(DateTime.Now.ToString() + ":" + Environment.NewLine + content);
                }
            }
            catch (Exception)
            {
            }
        }


        public static void WriteAppLog(string content)
        {
            string FilePath = string.Empty;
            CultureInfo ci = new CultureInfo("en-US");
            try
            {
                bool append = true;
                string path = LogPath1 + "\\APPLOG\\" + string.Format("{0:dd_MM_yyyy}", DateTime.Now) + "\\";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);


                if (DateTime.Parse(DateTime.Now.ToString(), ci) <= DateTime.Parse(DateTime.Parse(Curtime1).AddMinutes(1).ToString(), ci))
                {
                    FilePath = path + DateTime.Parse(Curtime1).ToString("ddMMMyyyy_HH_mm_tt") + ".txt";
                }
                else
                {
                    Curtime1 = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
                    FilePath = path + DateTime.Parse(Curtime1).ToString("ddMMMyyyy_HH_mm_tt") + ".txt";
                }

                if (!File.Exists(FilePath))
                {
                    using (FileStream fs = File.Create(FilePath)) { append = false; }
                }

                using (StreamWriter file = new System.IO.StreamWriter(FilePath, append))
                {
                    file.WriteLine(DateTime.Now.ToString() + ":" + Environment.NewLine + content);
                }
            }
            catch (Exception)
            {

            }
        }



        public static void WriteErrorLog(string content)
        {
            string FilePath = string.Empty;
            CultureInfo ci = new CultureInfo("en-US");
            try
            {
                bool append = true;
                string path = LogPath1 + "\\ERRORLOG\\" + string.Format("{0:dd_MM_yyyy}", DateTime.Now) + "\\";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);


                if (DateTime.Parse(DateTime.Now.ToString(), ci) <= DateTime.Parse(DateTime.Parse(Curtime1).AddMinutes(1).ToString(), ci))
                {
                    FilePath = path + DateTime.Parse(Curtime1).ToString("ddMMMyyyy_HH_mm_tt") + ".txt";
                }
                else
                {
                    Curtime1 = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
                    FilePath = path + DateTime.Parse(Curtime1).ToString("ddMMMyyyy_HH_mm_tt") + ".txt";
                }

                if (!File.Exists(FilePath))
                {
                    using (FileStream fs = File.Create(FilePath))
                    {
                        append = false;
                    }
                }
                using (StreamWriter file = new System.IO.StreamWriter(FilePath, append))
                {
                    file.WriteLine(DateTime.Now.ToString() + ":" + Environment.NewLine + content);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

