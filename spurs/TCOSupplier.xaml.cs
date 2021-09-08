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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Spurs
{
    /// <summary>
    /// Interaction logic for TCOSupplier.xaml
    /// </summary>
    public partial class TCOSupplier : Window
    {
        ArrayList Allst = new ArrayList();
        ArrayList supNames = new ArrayList();

        OpenFileDialog fld = new OpenFileDialog();


        string File_Name;
        ArrayList abbrevation = new ArrayList();
        double sumall;

        public TCOSupplier()
        {
            InitializeComponent();
            combo();
        }
        calmenu cal = new calmenu();
        private void combo()
        {
            try
            {

                bool fileExists = false;
                string thePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                thePath = cal.supplierpath();
                string[] myPaths = Directory.GetFiles(thePath, "*.csv");

                foreach (string fname in myPaths)
                {
                    string theFile = @fname;

                    fileExists = File.Exists(theFile);
                    if (fileExists)
                    {
                        File_Name = System.IO.Path.GetFileName(theFile);
                        FileHelperEngine engine = new FileHelperEngine(typeof(Supplier));

                        //Read
                        Supplier[] result = engine.ReadFile(fname) as Supplier[];

                        DataTable table = new DataTable();
                        DataTable table1 = new DataTable();
                        //iterate and store values in arraylist
                        foreach (Supplier row in result)
                        {
                            supNames.Add(row);
                        }
                        //add columns to table

                        table.Columns.Add("Supplier", typeof(string));


                        table1.Columns.Add("Supplier", typeof(string));

                        //iterate and add values of Arraylist to table
                        foreach (Supplier row in supNames.AsParallel())
                        {
                            Console.WriteLine(row.Suppliers);


                            table.Rows.Add(row.Suppliers);
                        }
                        //linq query to get only Supplier column 
                        var supply = (from DataRow type in table.AsEnumerable() select type["Supplier"]).Distinct().ToList();
                        //iterate and add distict values to table1
                        comboBox.Items.Clear();
                        foreach (String row in supply.AsParallel())
                        {

                            comboBox.Items.Add(row);

                        }

                    }

                }
            }
            catch
            {
                MessageBox.Show("Please select a store and supplier path first or path inncorrect");
                Hide();
                cal.ShowDialog();
            }
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
            sumall = 0;
            calmenu st = new calmenu();
            st.ShowDialog();
        }

        [DelimitedRecord(",")]
        private class Supplier
        {
            public string Suppliers;
            public string Suptype;
            public double cost;

        }



        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            string cobobox = comboBox.Text;
            


            Task t1 = new Task(() =>
            {
                bool fileExists = false;
                string thePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                thePath = cal.supplierpath();

                string[] myPaths = Directory.GetFiles(thePath, "*.csv");

                if (Directory.GetFiles(thePath, "*.csv").Length > 0)
                {

                    foreach (string fname in myPaths)
                    {
                        string theFile = @fname;

                        fileExists = File.Exists(theFile);
                        if (fileExists)
                        {
                            File_Name = System.IO.Path.GetFileName(theFile);
                            FileHelperEngine engine = new FileHelperEngine(typeof(Supplier));
                            DataTable dtt = new DataTable();
                            //Read
                            Supplier[] result = engine.ReadFile(fname) as Supplier[];

                            foreach (Supplier row in result)
                            {
                                if (row.Suppliers.Equals(cobobox))
                                {
                                    abbrevation.Add(row);
                                    sumall += row.cost;
                                    // dtt.Rows.Add(row.Suppliers, row.Suptype, row.cost);
                                }

                            }
                            dtt.Columns.Add("Suplier", typeof(string));
                            dtt.Columns.Add("Suplier Type", typeof(string));
                            dtt.Columns.Add("Price", typeof(string));

                            foreach (Supplier c in abbrevation.AsParallel())
                            {

                                dtt.Rows.Add(c.Suppliers, c.Suptype, c.cost);

                            }




                        }

                    }
                    MessageBox.Show("Total cost of order for Supplier is: " + sumall);
                }
                else
                {
                    System.Windows.MessageBox.Show("You have not selected a path yet");
                }
            });
            t1.Start();
            
        }
    }
}
