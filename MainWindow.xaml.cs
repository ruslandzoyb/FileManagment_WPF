using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Infrastructure;
using WpfApp1.MVVM.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public event DragEventHandler OnGridDrop;


        public MainWindow()
        {
            InitializeComponent();
            var vm = new ApplicationViewModel();
            DataContext = vm;
            OnGridDrop += vm.DropHandle;
           
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            OnGridDrop?.Invoke(sender, e);
        }

       
    }
}