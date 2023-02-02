using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfApp1.Infrastructure;
using WpfApp1.MVVM.Commands;
using WpfApp1.MVVM.Models;

namespace WpfApp1.MVVM.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private DialogManagment dialogManagment;
        private BaseCommand selectFile;
        private BaseCommand selectFilesFromDir;
        private BaseCommand selectImage;
        private BaseCommand selectFolder;
        private BaseCommand createFile;
        private BaseCommand selectMultiFiles;
        private BaseCommand pasteFromClipboard;
        private BaseCommand selectMultiImages;
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
                      var path = fileManagement.OpenFolderDialog();
                      if (!string.IsNullOrEmpty(path))
                      {
                          dialogManagment.FolderPath = path;
                      }

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

        public BaseCommand SelectFilesFromDir
        {
            get
            {
                return selectFilesFromDir ??
                  (selectFilesFromDir = new BaseCommand(obj =>
                  {
                      var path = fileManagement.OpenFolderDialog();
                      if (!string.IsNullOrEmpty(path))
                      {
                          dialogManagment.FilesInSelectedFolder = fileManagement.GetFilesFromDirectory(path);
                      }


                  }));
            }
        }

        public BaseCommand SelectMultiFiles
        {
            get
            {
                return selectMultiFiles ??
                  (selectMultiFiles = new BaseCommand(obj =>
                  {
                      var filesFromDialog = fileManagement.SelectMultiFileDialog().ToList();
                      if (dialogManagment.SelectedFiles is null)
                      {
                          dialogManagment.SelectedFiles = filesFromDialog;
                      }
                      else
                      {
                          var intersect = filesFromDialog.Union(dialogManagment.SelectedFiles).ToList();
                          dialogManagment.SelectedFiles = intersect;
                      }


                  }));
            }
        }

        public BaseCommand SelectMultiImages
        {
            get
            {
                return selectMultiImages ??
                  (selectMultiImages = new BaseCommand(obj =>
                  {
                      var images = fileManagement
                      .SelectMultiFileDialogForImages()
                      .Select(i => new ImageView { Image = i.Item2 })
                      .ToList();
                      if (dialogManagment.SelectedImages is null)
                      {
                          dialogManagment.SelectedImages = images;
                      }
                      else
                      {
                          var intersect = images.Union(dialogManagment.SelectedImages).ToList();
                          dialogManagment.SelectedImages = intersect;
                      }

                  }));
            }
        }

        public BaseCommand PasteFromClipboard
        {
            get
            {
                return pasteFromClipboard ??
                    (pasteFromClipboard = new BaseCommand(obj =>
                    {
                       
                        if (Clipboard.ContainsImage() || Clipboard.ContainsFileDropList())
                        {
                            var files = ((string[])Clipboard.GetData(DataFormats.FileDrop))
                            .Where(i => i.isImage())
                            .Select(i => new ImageView { Image = i }).ToList();
                            if (dialogManagment.SelectedImages != null)
                            {
                                dialogManagment.SelectedImages = dialogManagment.SelectedImages.Union(files).ToList();
                                return;
                            }

                            this.dialogManagment.SelectedImages = files;
                        }


                    }));
            }
        }

        public void DropHandle(object sender, DragEventArgs e)
        {
            var files = ((string[])e.Data.GetData(DataFormats.FileDrop))
                .Where(i => i.isImage())
                .Select(i => new ImageView { Image = i }).ToList();
            // var files = fileManagement.GetImagesFromPath(e.Data).ToList();
            if (dialogManagment.SelectedImages != null)
            {
                dialogManagment.SelectedImages = dialogManagment.SelectedImages.Union(files).ToList();
                return;
            }

            this.dialogManagment.SelectedImages = files;
        }






        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
