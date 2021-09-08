using FileHelpers;
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
    /// Interaction logic for TCOWeekSingleStore.xaml
    /// </summary>
    public partial class TCOWeekSingleStore : Window
    {
        ArrayList Allst = new ArrayList();
        double sumall;
        
       
        string File_Name;
        string abbrevation;
        
        public TCOWeekSingleStore()
        {
            InitializeComponent();
            combo();
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
        calmenu cal =new calmenu();
        private void combo()
        {

            try {
                for (int i = 1; i <= 52; i++)
                {
                    comboBox1.Items.Add(i);
                }
                comboBox2.Items.Add(2013);
                comboBox2.Items.Add(2014);


                bool fileExists = false;
                string thePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                thePath =cal.storepath();
                string[] myPaths = Directory.GetFiles(thePath, "*.csv");
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

                            comboBox.Items.Add(row.StoreName);

                        }

                    }
                }
            }
            catch
            {
                MessageBox.Show("Please select a store and supplier path first or incorrect path");
                Hide();
                cal.ShowDialog();
            }

    }
        [DelimitedRecord(",")]
        private class Stores
        {
            public string Abb;

            public string StoreName;
        }
        [DelimitedRecord(",")]
        private class Total
        {


            public string Supplier;

            public string Product;

            public double Price;


        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            string cobobox = comboBox.Text;
            string cobobox1 = comboBox1.Text;
            string cobobox2 = comboBox2.Text;


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
                        if (row.StoreName.Equals(cobobox))
                        {
                            abbrevation = row.Abb;
                            System.Windows.MessageBox.Show(abbrevation);
                        }

                    }

                }

            }
            string complile = abbrevation+ "_" + cobobox1 + "_" + cobobox2 + ".csv";
            if (Directory.GetFiles(thePath, "*.csv").Length > 0)
            {

                foreach (string file in Directory.GetFiles(thePath, "*.csv"))
                {
                    if (file.Contains(complile))
                    {
                        DataTable dtt = new DataTable();
                        FileHelperEngine engine = new FileHelperEngine(typeof(Total));
                        Total[] result = engine.ReadFile(file) as Total[];


                        foreach (Total c in result.AsParallel())
                        {
                            Allst.Add(c);
                            sumall += c.Price;
                        }
                        dtt.Columns.Add("Suplier", typeof(string));
                        dtt.Columns.Add("Suplier Type", typeof(string));
                        dtt.Columns.Add("Price", typeof(string));

                        foreach (Total c in Allst.AsParallel())
                        {

                            dtt.Rows.Add(c.Supplier, c.Product, c.Price);

                        }

                    }
                    else
                    {
                        //System.Windows.MessageBox.Show("Error 404 Found");
                    }
                }

                    MessageBox.Show("Total cost of order for a week for a single stores is: " + sumall);

                }
                else
            {
                System.Windows.MessageBox.Show("You have not selected a path yet");
            }
        });
            t1.Start();
            sumall = 0;
        }
        
    }
}

