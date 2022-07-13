using System.Diagnostics;
using System.Windows.Input;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Windows.Controls;
using qFiles;
namespace qLib
{

    
    public class Files
    {
        public string[] GetFilesInDirectory(string path, bool rootDisk)
        {



            string[] files = Directory.GetFiles(path);

            if (rootDisk)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    files[i] = files[i].Replace(path, "");
                }
            }
            else
            {

                for (int i = 0; i < files.Length; i++)
                {
                    files[i] = files[i].Replace(path + @"\", "");
                }
            }
            return files;

        }


        public string[] GetFoldersInDirectory(string path, bool rootDisk)
        {
            List<string> foldersList = new List<string>();
            IEnumerable<string> IEfolders = Directory.GetDirectories(path);

            foreach (string folder in IEfolders)
            {
                foldersList.Add(folder);


            }
            string[] folders = foldersList.ToArray();

            if (rootDisk)
            {
                for (int i = 0; i < folders.Length; i++)
                {
                    folders[i] = folders[i].Replace(path, "");
                }
            }
            else
            {
                for (int i = 0; i < folders.Length; i++)
                {
                    folders[i] = folders[i].Replace(path + @"\", "");
                }
            }

            return folders;
        }


        public string[] GetDrives(bool f)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            if (f == true) {

                List<string> drivesList = new List<string>();
                
                foreach (DriveInfo drive in allDrives)
                {
                    string dirveName = drive.Name.ToString();
                    drivesList.Add(dirveName);
                }

                return drivesList.ToArray();
            }
            
            else
            {
                List<string> drivesList = new List<string>();

                foreach (DriveInfo drive in allDrives)
                {
                    string dirveName = drive.VolumeLabel.ToString();
                    drivesList.Add(dirveName);
                }

                return drivesList.ToArray();
            }
        }

    }

    


}