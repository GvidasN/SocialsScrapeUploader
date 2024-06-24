using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialsScrapeUploader.helpers
{
    public static class FileHelpers
    {
        public static void DeleteFilesFromDir(string videosDirectoryPath)
        {
            if (!Directory.Exists(videosDirectoryPath))
            {
                Directory.CreateDirectory(videosDirectoryPath);
                Messages.GeneralMessage($"New directory '{videosDirectoryPath}' created.");
                return;
            }

            string[] files = Directory.GetFiles(videosDirectoryPath);

            foreach (string filePath in files)
            {
                try
                {
                    DeleteFile(filePath);
                }
                catch (Exception ex)
                {
                    Messages.Error(ex, MethodBase.GetCurrentMethod().Name);
                }
            }

            Messages.GeneralMessage($"Directory '{videosDirectoryPath}' cleared.");
        }

        public static void DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath)) { File.Delete(filePath); }
            }
            catch (Exception ex)
            {
                Messages.Error(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public static void CreateDirectory(string directoryPath)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    Messages.GeneralMessage($"Directory '{directoryPath}' cleared.");
                }
            }
            catch (Exception ex)
            {
                Messages.Error(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public static void CopyFile(string sourceFilePath, string destinationDirectory)
        {
            try
            {
                string destinationFilePath = Path.Combine(destinationDirectory, Path.GetFileName(sourceFilePath));

                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                File.Copy(sourceFilePath, destinationFilePath, true);

                Messages.GeneralMessage("Video copy succesfully added.");
            }
            catch (Exception ex)
            {
                Messages.Error(ex, MethodBase.GetCurrentMethod().Name);
            }
        }
        public static TimeSpan GetVideoDuration(string filePath)
        {
            var inputFile = new MediaFile { Filename = filePath };

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);
            }

            return inputFile.Metadata.Duration;
        }
    }
}
