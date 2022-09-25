using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Infrastructure
{
    public static class DialogCreator
    {
        public static FileDialog CreateSaveFileDialog() => new SaveFileDialog();

        public static FileDialog CreateOpenFileDialog() => new OpenFileDialog();

        public static System.Windows.Forms.FolderBrowserDialog CreateFolderDialog() => new System.Windows.Forms.FolderBrowserDialog();

    }
}
