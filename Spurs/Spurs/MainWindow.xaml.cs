using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Spurs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCLose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }
        private void move(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnimport_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();
            Graph mw = new Graph();
            mw.ShowDialog();
        }

        private void btnStoredata(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Storedata st = new Storedata();
            st.ShowDialog();
        }
        private void btnReport(object sender, RoutedEventArgs e)
        {
            this.Hide();
            calmenu st = new calmenu();
            st.ShowDialog();
        }
    }
}
