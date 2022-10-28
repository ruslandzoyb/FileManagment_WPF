﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfApp1.MVVM.Models
{
    public class DialogManagment :  INotifyPropertyChanged
    {
        private string folderPath, text, selectedFile, selectedImage;

    
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

        public string SelectedImage
        {
            get => selectedImage;
            set
            {
                selectedImage = value;
                OnPropertyChanged("SelectedImage");
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
