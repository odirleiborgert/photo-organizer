using System;
using System.IO;
using System.Linq;

namespace PhotoOrganizer
{
    class Program
    {

        public static string PATH_FROM = @"C:\Users\xyz";
        public static string PATH_TO = @"C:\www\photo-organizer\PhotoOrganizer\PhotoOrganizer\";
        public static string PATH_TYPE = "photos";

        static void Main(string[] args)
        {
            RunProcessPath(PATH_FROM);
        }


        public static bool RunProcessPath(string path_from)
        {

            path_from = path_from + @"\";

            RunProcessImages(path_from);

            DirectoryInfo info = new DirectoryInfo(path_from);

            var paths = info.GetDirectories().OrderBy(p => p.CreationTime).ToArray();

            if (paths.Length == 0)
            {
                return false;
            }

            foreach (var path in paths)
            {
                Console.WriteLine($"Path {path_from}{path.Name} .");
                RunProcessPath(path_from + path.Name);
            }

            return true;

        }



        public static void RunProcessImages(string path_from)
        {

            DirectoryInfo info = new DirectoryInfo(path_from);

            FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();

            int count = 0;

            foreach (var file in files)
            {

                DateTime date = DateTime.Parse(file.LastWriteTime.ToString());

                string pathYear = PATH_TO + PATH_TYPE + "/" + date.Year.ToString();
                string pathMonth = pathYear + "/" + date.Month.ToString();
                string pathDay = date.Day.ToString();

                if (!Directory.Exists(pathYear))
                {
                    Directory.CreateDirectory(pathYear);
                }

                if (!Directory.Exists(pathMonth))
                {
                    Directory.CreateDirectory(pathMonth);
                }

                string newFile = pathMonth + "/" + pathDay + "-" + count.ToString("D4") + file.Extension.ToLower();

                while (File.Exists(newFile))
                {
                    count++;
                    newFile = pathMonth + "/" + pathDay + "-" + count.ToString("D4") + file.Extension.ToLower();
                }

                if (PATH_TYPE == "photos")
                {
                    if (file.Extension.ToUpper().Contains("JPG") || file.Extension.ToUpper().Contains("JPEG"))
                    {
                        File.Move(file.FullName, newFile);
                        Console.WriteLine($"Foto {file.Name} copiada com sucesso.");
                    }
                }

                if (PATH_TYPE == "videos")
                {
                    if (file.Extension.ToUpper().Contains("MOV") || file.Extension.ToUpper().Contains("MP4"))
                    {
                        File.Move(file.FullName, newFile);
                        Console.WriteLine($"Vídeos {file.Name} copiada com sucesso.");
                    }
                }


                count = 0;

            }

        }


    }
}
