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
    /// Interaction logic for COWeeekSupplierStore.xaml
    /// </summary>
    public partial class COWeeekSupplierStore : Window
    {
        ArrayList Allst = new ArrayList();
        ArrayList supNames = new ArrayList();

        OpenFileDialog fld = new OpenFileDialog();


        string File_Name;
        string abbrevation;
        double sumall;
        calmenu cal = new calmenu();
        public COWeeekSupplierStore()
        {
            InitializeComponent();
            combo();
        }
        private void combo()
        {
           
            try
            {
                for (int i = 1; i <= 52; i++)
                {
                    comboBox3.Items.Add(i);
                }
                comboBox4.Items.Add(2013);
                comboBox4.Items.Add(2014);

                bool fileExists1 = false;
                string thePath1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                thePath1 = cal.storepath();
                string[] myPaths1 = Directory.GetFiles(thePath1, "*.csv");
                foreach (string fname in myPaths1)
                {
                    string theFile = @fname;

                    fileExists1 = File.Exists(theFile);
                    if (fileExists1)
                    {
                        File_Name = System.IO.Path.GetFileName(theFile);
                        FileHelperEngine engine = new FileHelperEngine(typeof(Stores));

                        //Read
                        Stores[] result = engine.ReadFile(fname) as Stores[];

                        foreach (Stores row in result)
                        {

                            comboBox2.Items.Add(row.StoreName);

                        }

                    }
                }

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
                        FileHelperEngine engine = new FileHelperEngine(typeof(Suppliertype));

                        //Read
                        Suppliertype[] result = engine.ReadFile(fname) as Suppliertype[];

                        DataTable table = new DataTable();
                        DataTable table1 = new DataTable();
                        //iterate and store values in arraylist
                        foreach (Suppliertype row in result)
                        {
                            supNames.Add(row);
                        }
                        //add columns to table

                        table.Columns.Add("Supplier type", typeof(string));


                        table1.Columns.Add("Supplier type", typeof(string));

                        //iterate and add values of Arraylist to table
                        foreach (Suppliertype row in supNames.AsParallel())
                        {
                            Console.WriteLine(row.Suptype);


                            table.Rows.Add(row.Suptype);
                        }
                        //linq query to get only Supplier column 
                        var supply = (from DataRow type in table.AsEnumerable() select type["Supplier type"]).Distinct().ToList();
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
                MessageBox.Show("Please select a store and supplier path first");
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
        private class Suppliertype
        {
            public string Suppliers;
            public string Suptype;
            public double cost;

        }

        [DelimitedRecord(",")]
        private class Stores
        {
            public string Abb;

            public string StoreName;
        }



        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            string cobobox = comboBox3.Text;
            string cobobox1 = comboBox.Text;
            string cobobox2 = comboBox2.Text;
            string cobobox3 = comboBox4.Text;
            Task t1 = new Task(() =>
            {
                bool fileExists = false;
                string thePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                thePath = cal.supplierpath();
                string thePath1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                thePath1 = cal.storepath();
                string[] myPaths = Directory.GetFiles(thePath1, "*.csv");
                foreach (string fname in myPaths)
                {
                    string theFile = @fname;

                    fileExists = File.Exists(theFile);
                    if (fileExists)
                    {
                        File_Name = System.IO.Path.GetFileName(theFile);
                        FileHelperEngine engine = new FileHelperEngine(typeof(Stores));

                        //Read
                        Stores[] result = engine.ReadFile(fname) as Stores[];

                        foreach (Stores row in result)
                        {
                            if (row.StoreName.Equals(cobobox2))
                            {
                                abbrevation = row.Abb;
                                System.Windows.MessageBox.Show(abbrevation);
                            }

                        }

                    }

                }
                string compiler = abbrevation + "_" + cobobox + "_" + cobobox3 + ".csv";
                if (Directory.GetFiles(thePath, "*.csv").Length > 0)
                {

                    foreach (string fname in Directory.GetFiles(thePath, "*.csv"))
                    {
                        if (fname.Contains(compiler))
                        {
                            string theFile = @fname;

                            fileExists = File.Exists(theFile);
                            if (fileExists)
                            {
                                File_Name = System.IO.Path.GetFileName(theFile);
                                FileHelperEngine engine = new FileHelperEngine(typeof(Suppliertype));
                                DataTable dtt = new DataTable();
                                //Read
                                Suppliertype[] result = engine.ReadFile(fname) as Suppliertype[];

                                foreach (Suppliertype row in result.AsParallel())
                                {
                                    if (row.Suptype.Equals(cobobox1))
                                    {
                                        Allst.Add(row);
                                        sumall += row.cost;

                                        // dtt.Rows.Add(row.Suppliers, row.Suptype, row.cost);
                                    }

                                }

                                dtt.Columns.Add("Suplier Type", typeof(string));
                                dtt.Columns.Add("Price", typeof(string));
                                dtt.Columns.Add("Suplier", typeof(string));

                                foreach (Suppliertype c in Allst.AsParallel())
                                {

                                    dtt.Rows.Add(c.Suptype, c.cost, c.Suppliers);

                                }


                            }

                        }

                    }
                    MessageBox.Show("The cost of order for a week for a supplier for a store is" + sumall);


                }
                else
                {
                    System.Windows.MessageBox.Show("You have not selected a path yet");
                }
            });
            t1.Start();
            Totalno.Content = Convert.ToString(sumall);
            sumall = 0;
        }
    }
}
