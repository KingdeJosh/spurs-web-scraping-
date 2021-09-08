using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Spurs
{
    /// <summary>
    /// Interaction logic for calmenu.xaml
    /// </summary>
    public partial class calmenu : Window
    {
        static FolderBrowserDialog foilderdialogs = new FolderBrowserDialog();
        static FolderBrowserDialog foilderdialogs1 = new FolderBrowserDialog();
      static  DialogResult dr;
    static    DialogResult dr1;
        static string j;
        static string i;
        public calmenu()
        {
            InitializeComponent();
        }
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

        private void btnallsupplieddata_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Import nn = new Import();
            nn.ShowDialog();
        }

        private void btnsinglestore_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            TCAOsinglestore nn = new TCAOsinglestore();
            nn.ShowDialog();
        }

        private void btnweeksinglestore_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            TCOWeekAStores nn = new TCOWeekAStores();
            nn.ShowDialog();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow st = new MainWindow();
            st.ShowDialog();
        }

        private void btnweekstore_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            TCOWeekSingleStore nn = new TCOWeekSingleStore();
            nn.ShowDialog();
        }

        private void btnasupplier_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            TCOSupplier nn = new TCOSupplier();
            nn.ShowDialog();
        }

        private void btnasuppliedtype_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CAOSuppliertype nn = new CAOSuppliertype();
            nn.ShowDialog();
        }

        private void btnweeksupplieidtype_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            COWeekSupplierType nn = new COWeekSupplierType();
            nn.ShowDialog();
        }

        private void btnsuppliertostore_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            COSuppliertypeStore nn = new COSuppliertypeStore();
            nn.ShowDialog();
        }

        private void btnweeksuppliertostore_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
           COWeeekSupplierStore nn = new COWeeekSupplierStore();
            nn.ShowDialog();
        }

        public string storepath()
        {
            j = foilderdialogs1.SelectedPath;
            return j;
        }
        public  string supplierpath()
        {
             i = foilderdialogs.SelectedPath;
            return i;
        }
       
    

        private void btnBrows1e_Click(object sender, RoutedEventArgs e)
        {

            foilderdialogs.ShowNewFolderButton = true;

            DialogResult dr = foilderdialogs.ShowDialog();


           
            Environment.SpecialFolder rootF = foilderdialogs.RootFolder;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            foilderdialogs1.ShowNewFolderButton = true;

            dr = foilderdialogs1.ShowDialog();



            Environment.SpecialFolder rootF = foilderdialogs1.RootFolder;
        }
    }
}
