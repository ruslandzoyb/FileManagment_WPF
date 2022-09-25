using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.MVVM.Models
{
    public class DialogManagment :  INotifyPropertyChanged
    {
        private string filePath, folderPath, text, selectedFile;

        public string FilePath { get => filePath; set
            {
                filePath = value;
                OnPropertyChanged("FilePath");
            }
        }
        public string FolderPath { get => folderPath; set
            {
                folderPath = value;
                OnPropertyChanged("FolderPath");
            }
        }
        public string Text { get => text; set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }
        public string SelectedFile
        {
            get => selectedFile;
            set
            {
                selectedFile = value;
                OnPropertyChanged("SelectedFile");
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
