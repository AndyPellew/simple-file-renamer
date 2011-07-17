using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SimpleFileRenamer
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader file = null;
            string line;
            try
            {
                file = new StreamReader(Directory.GetCurrentDirectory() + @"\Settings.txt");
                while ((line = file.ReadLine()) != null)
                {
                    string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory());
                    foreach (string OldName in filePaths)
                        if (Path.GetFileName(OldName) != "SimpleFileRenamer.exe" &&
                            Path.GetFileName(OldName) != "Settings.txt")
                        {
                            string[] sRename = line.Split('|');
                            string NewName = Path.GetFileName(OldName).Replace(sRename[0], sRename[1]);
                            if (Path.GetFileName(OldName) != NewName)
                            {
                                string TempFileName = Guid.NewGuid().ToString();
                                File.Move(OldName,
                                    TempFileName);
                                File.Move(TempFileName,
                                   Path.GetDirectoryName(OldName) + @"\" + NewName);
                            }
                        }
                }
            }
            finally
            {
                if (file != null)
                    file.Close();
            }
        }
    }
}
