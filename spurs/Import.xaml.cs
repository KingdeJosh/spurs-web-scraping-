using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
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
using FileHelpers;
using System.IO;
using System.Windows.Forms;
using System.Collections;

namespace Spurs
{
    /// <summary>
    /// Interaction logic for Import.xaml
    /// </summary>
    public partial class Import : Window
    {
      static  FolderBrowserDialog foilderdialogs = new FolderBrowserDialog();
        // OpenFileDialog ofd = new OpenFileDialog();
        ArrayList allenter = new ArrayList();
        double sumall=0;

        public Import()
        {
            InitializeComponent();
        }
        private void move(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
       

        private void btnCLose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        [DelimitedRecord(",")]
        private class myCSVFile
        {
            public string Supplier;

            public string Product;

            public double Price;
        }
        public FolderBrowserDialog dialogs()
        {
            return foilderdialogs;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {

           
            foilderdialogs.ShowNewFolderButton = true;

            DialogResult dr = foilderdialogs.ShowDialog();


            textbox_csv.Text = foilderdialogs.SelectedPath;
            Environment.SpecialFolder rootF = foilderdialogs.RootFolder;
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
          
           

            if (Directory.GetFiles(textbox_csv.Text, "*.csv").Length > 0)
            {
                
                foreach (string file in Directory.GetFiles(textbox_csv.Text, "*.csv"))
                {
                    DataTable dtt = new DataTable();
                    FileHelperEngine engine = new FileHelperEngine(typeof(myCSVFile));
                    myCSVFile[] result = engine.ReadFile(file) as myCSVFile[];

                    
                    foreach (myCSVFile c in result)
                    {
                        allenter.Add(c);
                        sumall += c.Price;
                    }
                    dtt.Columns.Add("Suplier", typeof(string));
                    dtt.Columns.Add("Suplier Type", typeof(string));
                    dtt.Columns.Add("Price", typeof(string));
                    
                    foreach (myCSVFile c in allenter)
                    {
                        Console.WriteLine(c.Supplier + " " + c.Product + " " + c.Price);
                        
                        dtt.Rows.Add(c.Supplier, c.Product, c.Price);
                    }
                    
                    dataGridv.DataContext = dtt.DefaultView;
                }
                textbox_sum.Text = Convert.ToString(sumall);
            }
            else
            {
                System.Windows.MessageBox.Show("You have not selected a path yet");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            sumall = 0;
            calmenu st = new calmenu();
            st.ShowDialog();
        }
    }
}
