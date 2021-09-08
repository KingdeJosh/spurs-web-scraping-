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
using System.Windows.Shapes;

namespace Spurs
{
    /// <summary>
    /// Interaction logic for Storedata.xaml
    /// </summary>
    public partial class Storedata : Window
    {
        public Storedata()
        {
            InitializeComponent();
        }
        //go back to previous window
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow back = new MainWindow();
            back.ShowDialog();
        }
        //drag window double click window 
        private void move(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        //close window
        private void btnCLose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //minimize window
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void btnViewstores(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AllStores store = new AllStores();
            store.ShowDialog();
        }
        private void btnViewtype(object sender, RoutedEventArgs e)
        {
            this.Hide();
            suptypes sup = new suptypes();
            sup.ShowDialog();
        }
        private void btnViewsup(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AllSup allstore = new AllSup();
            allstore.ShowDialog();
        }


    }
}
