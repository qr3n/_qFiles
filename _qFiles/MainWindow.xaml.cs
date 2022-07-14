using qLib;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Drawing;
namespace qFiles
{
    public partial class MainWindow : Window
    {
        Files Files = new Files();
        List<string> places = new List<string>();
        Style gFolderIcon;
        Style? styleText;
        
        public Style? gFolderIconStyle;
        public MainWindow()
        {
            InitializeComponent();
            string[] drives = Files.GetDrives(true);
            
            string[] folders = Files.GetFoldersInDirectory(@"C:\", true);
            string[] files = Files.GetFilesInDirectory(@"C:\", true);
            

            places.Add(@"C:\");

            Style folderIcon = this.FindResource("folderIcon") as Style;
            gFolderIcon = folderIcon;
            styleText = this.FindResource("labelText") as Style;
            
            ShowFiles(folders, files, @"C:\");

        }

        public void SelectFolder(object sender, MouseButtonEventArgs e)
        {
            
            if (e.ClickCount == 2)
            {
                CdInFolder(sender, e);
            }
        }

        public void CdInFolder(object sender, MouseButtonEventArgs e)
        {
            
            filesWindow.Children.Clear();


            FrameworkElement tag = e.Source as FrameworkElement;
            string[] folders = Files.GetFoldersInDirectory(tag.Tag.ToString(), false);
            string[] files = Files.GetFilesInDirectory(tag.Tag.ToString(), false);
            

            ShowFiles(folders, files, tag.Tag.ToString());
            
        }

        public void Back(object sender, RoutedEventArgs e)
        {

            
            if (places.Count > 2) {

                try
                {
                    filesWindow.Children.Clear();
                    string path = places[places.Count - 2];
                    
                    string[] folders = Files.GetFoldersInDirectory(path, false);
                    string[] files = Files.GetFilesInDirectory(path, false);

                    Style? folderIcon = this.FindResource("folderIcon") as Style;
                    Style? styleText = this.FindResource("textStyle") as Style;

                    places.RemoveAt(places.Count - 1);
                    places.RemoveAt(places.Count - 1);
                    ShowFiles(folders, files, path);
                }
                catch (Exception ex)
                {
                    filesWindow.Children.Clear();
                    string path = places[places.Count - 2];
                    
                    string[] folders = Files.GetFoldersInDirectory(path, true);
                    string[] files = Files.GetFilesInDirectory(path, true);

                    places.RemoveAt(places.Count - 1);

                    ShowFiles(folders, files, path);
                }



            }

            else
            {

            }
        }


        void ShowDrives(Style textStyle, Style folderIconStyle)
        {
            filesWindow.Children.Clear();
            string[] drivesPaths = Files.GetDrives(true);
            string[] drivesLabels = Files.GetDrives(false);
        }

       
        void ShowFiles(string[] folders, string[] files, string path)
        {
            filesWindow.RowDefinitions.Clear();
            places.Add(path);
            path = path + @"\";
            int i = 0;
            foreach (string folder in folders)
            {

                RowDefinition rowDefinition = new RowDefinition();
                filesWindow.RowDefinitions.Add(rowDefinition);
                rowDefinition.MaxHeight = 60;

                Label folderLabel = new Label();
                folderLabel.Content = folder;
                folderLabel.Style = styleText;
                filesWindow.Children.Add(folderLabel);
                folderLabel.SetValue(Grid.RowProperty, i);
                folderLabel.SetValue(Grid.ColumnProperty, 1);
                folderLabel.SetValue(Grid.ColumnSpanProperty, 11);
                folderLabel.MouseDown += new MouseButtonEventHandler(SelectFolder);
                folderLabel.Tag = path + folder;

                System.Windows.Controls.Image folderIcon = new System.Windows.Controls.Image();
                folderIcon.Style = gFolderIcon;
                folderIcon.Margin = new Thickness(10);
                filesWindow.Children.Add(folderIcon);
                folderIcon.SetValue(Grid.RowProperty, i);



                i++;


            }

            foreach (string file in files)
            {
                RowDefinition rowDefinition = new RowDefinition();
                filesWindow.RowDefinitions.Add(rowDefinition);
                rowDefinition.MaxHeight = 60;

                Label fileLabel = new Label();
                fileLabel.Content = file;
                fileLabel.Style = styleText;
                filesWindow.Children.Add(fileLabel);
                fileLabel.SetValue(Grid.RowProperty, i);
                fileLabel.SetValue(Grid.ColumnProperty, 1);
                fileLabel.SetValue(Grid.ColumnSpanProperty, 11);

                System.Windows.Controls.Image fileIcon = new System.Windows.Controls.Image();
                Icon? icon = System.Drawing.Icon.ExtractAssociatedIcon(path + file);
                fileIcon.Source = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                fileIcon.SetValue(Grid.RowProperty, i);
                fileIcon.Margin = new Thickness(12);
                fileIcon.MaxHeight = 30;
                filesWindow.Children.Add(fileIcon);

                i++;
            }

            




        }

    }


}
