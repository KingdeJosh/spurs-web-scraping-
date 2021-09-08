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
    /// Interaction logic for AllSup.xaml
    /// </summary>
    public partial class AllSup : Window
    {
       static ArrayList arrysupname = new ArrayList();
        static FolderBrowserDialog foilderdialogs = new FolderBrowserDialog();

        int no = 0;
       
     //static   OpenFileDialog FileDialog = new OpenFileDialog();
        public AllSup()
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
        private class Supplier
        {
            public string Suppliers;
            public string Suptype;
            public double cost;

        }
        
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            foilderdialogs.ShowNewFolderButton = true;

            DialogResult dr = foilderdialogs.ShowDialog();


            textbox_csv.Text = foilderdialogs.SelectedPath;
            Environment.SpecialFolder rootF = foilderdialogs.RootFolder;
        }



        private  void btnImport_Click(object sender, RoutedEventArgs e)
        {
            
            dataGridvs.DataContext = null;
            foreach (String fileName in Directory.GetFiles(textbox_csv.Text, "*.csv").AsParallel())
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(Supplier));
                //Read csv file
                Supplier[] result = engine.ReadFile(fileName) as Supplier[];
                //create datatables
                DataTable datatbl = new DataTable();
                DataTable table1 = new DataTable();
                //iterate and store values in arraylist
                foreach (Supplier row in result)
                {
                    arrysupname.Add(row);
                }
                //add columns to table
                datatbl.Columns.Add("No", typeof(string));
                datatbl.Columns.Add("Supplier", typeof(string));
                
                table1.Columns.Add("No", typeof(string));
                table1.Columns.Add("Supplier", typeof(string));
               
                //iterate and add values of Arraylist to table
                foreach (Supplier row in arrysupname.AsParallel())
                {
                    Console.WriteLine(row.Suppliers);


                    datatbl.Rows.Add(no, row.Suppliers);
                }
                //linq query to get only Supplier column 
                var query = (from DataRow type in datatbl.AsEnumerable() select type["Supplier"]).Distinct().ToList();
                //iterate and add distict values to table1
                foreach (string row in query.AsParallel())
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
