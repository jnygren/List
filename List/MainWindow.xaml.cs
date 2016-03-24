using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
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


        /// <summary>
        /// c'tor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        /// <summary>
        /// Window Loaded event handler
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListDisplay += "(Open a file to list)\r\n";
        }


        /// <summary>
        /// 'About' menu item click event handler
        /// </summary>
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

        #region ICommand implementation for 'Open' command.

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
                ListDisplay = "";
                using (BinaryReader br = new BinaryReader(File.Open(listFilePath, FileMode.Open, FileAccess.Read)))
                {
                    byte[] b = new byte[16];
                    int pos;
                    int loc = 0;
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        pos = (int)br.BaseStream.Position % 16;
                        if (pos == 0) loc = (int)br.BaseStream.Position;
                        b[pos] = br.ReadByte();
                        if (pos % 16 == 15 || br.BaseStream.Position == br.BaseStream.Length)
                        {
                            oneLine = string.Format("{0:D6}: {1:X2} {2:X2} {3:X2} {4:X2} {5:X2} {6:X2} {7:X2} {8:X2} " +
                                                    "{9:X2} {10:X2} {11:X2} {12:X2} {13:X2} {14:X2} {15:X2} {16:X2} | " +
                                                    "{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}\r\n",
                                                    loc, b[0], b[1], b[2], b[3], b[4], b[5], b[6], b[7], b[8], b[9], b[10], b[11], b[12], b[13], b[14], b[15],
                                                    prt(b[0]), prt(b[1]), prt(b[2]), prt(b[3]), prt(b[4]), prt(b[5]), prt(b[6]), prt(b[7]), 
                                                    prt(b[8]), prt(b[9]), prt(b[10]), prt(b[11]), prt(b[12]), prt(b[13]), prt(b[14]), prt(b[15]) );
                            ListDisplay += oneLine;
                            b[0] = b[1] = b[2] = b[3] = b[4] = b[5] = b[6] = b[7] = b[8] = b[9] = b[10] = b[11] = b[12] = b[13] = b[14] = b[15] = 0;
                        }
                    }
                    ListDisplay += string.Format("\r\n\tFile Length: {0}\r\n", br.BaseStream.Length);
                }
            }
        }

        #endregion


        /// <summary>
        /// Return a printable character representation of byte b
        /// </summary>
        private char prt(byte b)
        {
            return b > 31 && b < 127 ? Convert.ToChar(b) : '.';
        }
    }
}
