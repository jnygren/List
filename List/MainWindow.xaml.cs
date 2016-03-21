using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace List
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _listDisplay;

        public string ListDisplay { get { return _listDisplay; } set { _listDisplay = value; OnPropertyChanged("ListDisplay"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListDisplay += "Bob\r\n";
        }


        private void About_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Owner = this;
            about.ShowInTaskbar = false;
            about.ShowDialog();
        }


        /// <summary>
        /// Support for INotifyPropertyChanged interface implementation
        /// </summary>
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string listFilePath = "";
            string oneLine = "";
            OpenFileDialog ofn = new OpenFileDialog();

            ofn.Title = "Select file to list";
            if ((bool)ofn.ShowDialog(this))
            {
                listFilePath = ofn.FileName;
                using (BinaryReader br = new BinaryReader(File.Open(listFilePath, FileMode.Open)))
                {
                    byte b = (byte)br.PeekChar();
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        b = br.ReadByte();
                        oneLine = string.Format("{0:X2} {1}\r\n", b, Convert.ToChar(b));
                        ListDisplay += oneLine;
                    }
                }
            }
        }
    }
}
