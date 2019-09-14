using System;
using System.IO;
using System.Linq;

namespace PhotoOrganizer
{
    class Program
    {
        static void Main(string[] args)
        {

            string root = @"C:\www\photo-organizer\PhotoOrganizer\PhotoOrganizer\photos\";

            DirectoryInfo info = new DirectoryInfo(root);

            FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();

            int count = 0;

            foreach (FileInfo file in files)
            {


                DateTime date = DateTime.Parse(file.LastWriteTime.ToString());

                string pathYear = root + date.Year.ToString();
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

                if (file.Extension.ToUpper().Contains("JPG") || file.Extension.ToUpper().Contains("JPEG"))
                {
                    File.Move(file.FullName, newFile);
                }

                Console.WriteLine($"Foto {file.Name} copiada com sucesso.");

                count = 0;

            }

        }
    }
}
