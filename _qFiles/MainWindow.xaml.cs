using qLib;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime;
using System.Linq;
namespace qFiles
{
    

    public partial class MainWindow : Window
    {
        Files Files = new Files();
        List<string> places = new List<string>();
        
        public Style? gFolderIconStyle;
        public MainWindow()
        {
            InitializeComponent();
            string[] drives = Files.GetDrives(true);
            
            string[] folders = Files.GetFoldersInDirectory(@"C:\", true);
            string[] files = Files.GetFilesInDirectory(@"C:\", true);
            Style styleText = this.FindResource("textStyle") as Style;
            Style folderIcon = this.FindResource("folderIcon") as Style;

            places.Add(@"C:\");
            ShowFiles(folders, files, @"C:\", styleText, folderIcon);


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

            Style? folderIcon = this.FindResource("folderIcon") as Style;
            Style? styleText = this.FindResource("textStyle") as Style;



            FrameworkElement? tag = e.Source as FrameworkElement;
            string[] folders = Files.GetFoldersInDirectory(tag.Tag.ToString(), false);
            string[] files = Files.GetFilesInDirectory(tag.Tag.ToString(), false);
            

            ShowFiles(folders, files, tag.Tag.ToString(), styleText, folderIcon);
            
        }

        public void Back(object sender, RoutedEventArgs e)
        {

            
            if (places.Count > 2) {

                try
                {
                    filesWindow.Children.Clear();
                    string path = places[places.Count - 2];
                    Trace.WriteLine(path);
                    string[] folders = Files.GetFoldersInDirectory(path, false);
                    string[] files = Files.GetFilesInDirectory(path, false);

                    Style? folderIcon = this.FindResource("folderIcon") as Style;
                    Style? styleText = this.FindResource("textStyle") as Style;

                    places.RemoveAt(places.Count - 1);
                    places.RemoveAt(places.Count - 1);
                    ShowFiles(folders, files, path, styleText, folderIcon);
                }
                catch (Exception ex)
                {
                    filesWindow.Children.Clear();
                    string path = places[places.Count - 2];
                    Trace.WriteLine(path);
                    string[] folders = Files.GetFoldersInDirectory(path, true);
                    string[] files = Files.GetFilesInDirectory(path, true);

                    Style? folderIcon = this.FindResource("folderIcon") as Style;
                    Style? styleText = this.FindResource("textStyle") as Style;

                    places.RemoveAt(places.Count - 1);

                    ShowFiles(folders, files, path, styleText, folderIcon);
                }



            }

            else
            {

            }

                
            

            



        }


        void ShowDrives(string[] drives)
        {
            filesWindow.Children.Clear();
            RowDefinition rowDefinition = new RowDefinition();
            filesWindow.RowDefinitions.Add(rowDefinition);

        }

       
        void ShowFiles(string[] folders, string[] files, string path, Style textStyle, Style folderIconStyle)
        {
            places.Add(path);
            path = path + @"\";
            Trace.WriteLine(path);
            int i = 0;
            foreach (string folder in folders)
            {

                RowDefinition rowDefinition = new RowDefinition();
                filesWindow.RowDefinitions.Add(rowDefinition);

                TextBlock folderLabel = new TextBlock();
                folderLabel.Text = folder;
                folderLabel.Style = textStyle;
                filesWindow.Children.Add(folderLabel);
                folderLabel.SetValue(Grid.RowProperty, i);
                folderLabel.SetValue(Grid.ColumnProperty, 1);
                folderLabel.SetValue(Grid.ColumnSpanProperty, 11);
                folderLabel.MouseDown += new MouseButtonEventHandler(SelectFolder);
                folderLabel.Tag = path + folder;

                System.Windows.Controls.Image folderIcon = new System.Windows.Controls.Image();
                folderIcon.Margin = new Thickness(10);
                folderIcon.Style = folderIconStyle;
                filesWindow.Children.Add(folderIcon);
                folderIcon.SetValue(Grid.RowProperty, i);



                i++;


            }

            foreach (string file in files)
            {
                RowDefinition rowDefinition = new RowDefinition();
                filesWindow.RowDefinitions.Add(rowDefinition);

                TextBlock fileLabel = new TextBlock();
                fileLabel.Text = file;
                fileLabel.Style = textStyle;
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

        

        void CloseWindow(object sender, RoutedEventArgs eventArguments)
        {
            Close();
        }

        void MinimizeWindow(object sender, RoutedEventArgs eventArguments)
        {
            WindowState = WindowState.Minimized;

        }

        void MaximizeWindow(object sender, RoutedEventArgs eventArguments)
        {
            WindowState = WindowState.Maximized;

        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }


}
