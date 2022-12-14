using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.MVVM.Models;

namespace WpfApp1.Infrastructure
{
    public class FileManagement
    {

      
        private FileContentWriter writer;
        public FileManagement()
        {
            writer = new FileContentWriter();
        }

        public void HandleFileManagement(DialogManagment dialog)
        {
           var filePath = CreateFilePath(dialog.FolderPath);
            writer.CreateFile(filePath);
            writer.WriteToFile(filePath, dialog.Text);
        }

        private string CreateFilePath(string folderPath)
        {
            var saveFileDialog = DialogCreator.CreateSaveFileDialog();
            SetDefaultSaveDialogSettings(saveFileDialog, folderPath);

            if (saveFileDialog.ShowDialog() == true)
            {
                return saveFileDialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        public string OpenFolderDialog()
        {
            var dialog = DialogCreator.CreateFolderDialog();
            var result = dialog.ShowDialog();           
            return dialog.SelectedPath;           
        }

        public FileInfo[] GetFilesFromDirectory(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            return d.GetFiles();
        }

        public string SelectFileDialog()
        {
            var dialog = DialogCreator.CreateOpenFileDialog();            
            dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.FileName))
            {
                return dialog.FileName;
            }
            else
            {
                return string.Empty;
            }


        }

        public IEnumerable<Tuple<string, string>> SelectMultiFileDialog()
        {
            var dialog =  (OpenFileDialog)DialogCreator.CreateOpenFileDialog();
            dialog.Multiselect = true;
            dialog.ShowDialog();
            if (dialog.FileNames != null)
            {
             return dialog.FileNames
                    .Select(file => new Tuple<string, string>(file.Split('\\').Last(), file));
               
            }
            else
            {
                return Array.Empty<Tuple<string, string>>();
            }

        }

        public string SelectImageDialog()
        {
            var dialog = DialogCreator.CreateOpenFileDialog();
            SetDefaultImageDialogSettings(dialog);
                dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.FileName))
            {
                return dialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        private void SetDefaultSaveDialogSettings(FileDialog dialog, string folderPath)
        {
            dialog.InitialDirectory = folderPath;
            dialog.DefaultExt = "txt";
        }

        private void SetDefaultImageDialogSettings(FileDialog dialog)
        {           
            dialog.Filter = "Image Files|*.jpeg;*.png;*.bmp;*...";
        }
    }
}