using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Infrastructure;
using WpfApp1.MVVM.Commands;
using WpfApp1.MVVM.Models;

namespace WpfApp1.MVVM.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private DialogManagment dialogManagment;
        private BaseCommand selectFile;
        private BaseCommand selectImage;
        private BaseCommand selectFolder;
        private BaseCommand createFile;
        private FileManagement fileManagement;
        public ApplicationViewModel()
        {
            this.DialogManagment = new DialogManagment();
            fileManagement = new FileManagement();
                      
        }

        public DialogManagment DialogManagment { get => dialogManagment; set => dialogManagment = value; }

        public BaseCommand SelectFileCommand
        {
            get
            {
                return selectFile ??
                  (selectFile = new BaseCommand(obj =>
                  {
                     dialogManagment.SelectedFile = fileManagement.SelectFileDialog();
                  }));
            }
        }

        public BaseCommand SelectImageCommand
        {
            get
            {
                return selectImage ??
                  (selectImage = new BaseCommand(obj =>
                  {                      
                      dialogManagment.SelectedImage = fileManagement.SelectImageDialog();                      
                     
                  }));
            }
        }

        public BaseCommand SelectFolderPath
        {
            get
            {
                return selectFolder ??
                  (selectFolder = new BaseCommand(obj =>
                  {
                      dialogManagment.FolderPath = fileManagement.OpenFolderDialog();
                  }));
            }
        }

        public BaseCommand CreateFile
        {
            get
            {
                return createFile ??
                  (createFile = new BaseCommand(obj =>
                  {
                       fileManagement.HandleFileManagement(dialogManagment);
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }      
    }
}
