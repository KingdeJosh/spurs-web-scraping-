using FileHelpers;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Interaction logic for suptypes.xaml
    /// </summary>
    public partial class suptypes : Window
    {
        static FolderBrowserDialog foilderdialogs = new FolderBrowserDialog();

        ArrayList Names = new ArrayList();
       
       
       // OpenFileDialog fld = new OpenFileDialog();
        int no = 0;
        public suptypes()
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Storedata sd = new Storedata();
            sd.ShowDialog();
        }
        //spliter
        [DelimitedRecord(",")]
        private class Suppliertype
        {
            public string Supplier;
            public string Suptype;
            public double cost;

        }
        public FolderBrowserDialog Filedialogs()
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

            foreach (String fileName in Directory.GetFiles(textbox_csv.Text, "*.csv"))
             {
                FileHelperEngine engine = new FileHelperEngine(typeof(Suppliertype));
                //Read csv file
                Suppliertype[] result = engine.ReadFile(fileName) as Suppliertype[];
                //create datatables
                DataTable table = new DataTable();
                DataTable table1 = new DataTable();
                //iterate and store values in arraylist
                 foreach (Suppliertype row in result)
                {
                        Names.Add(row);
                }
                //add columns to table
                table.Columns.Add("No", typeof(string));
               
                table.Columns.Add("Supplier Types", typeof(string));
                table1.Columns.Add("No", typeof(string));
                
                table1.Columns.Add("Supplier Types", typeof(string));
                //iterate and add values of Arraylist to table
                foreach (Suppliertype row in Names.AsParallel())
                {
                    Console.WriteLine(row.Suptype);
                   

                    table.Rows.Add(no, row.Suptype);
                }
               
                //linq query to get only Supplier column 
                var suptype = (from DataRow type in table.AsEnumerable()  select type["Supplier Types"]).Distinct().ToList();
                //iterate and add distict values to table1
                 foreach (string row in suptype.AsParallel())
                {
                    Console.WriteLine(row);
                    no += 1;
                    table1.Rows.Add(no, row);
                }
                 //add table 1 to datagrid
                dataGridvs.DataContext = table1.DefaultView;
                Totalno.Content = Convert.ToString(no);
            }
        }
    }
}
