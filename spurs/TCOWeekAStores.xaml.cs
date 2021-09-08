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
    /// Interaction logic for TCOWeekAStores.xaml
    /// </summary>
    public partial class TCOWeekAStores : Window
    {
        ArrayList Allst = new ArrayList();
        double sumall;
        public TCOWeekAStores()
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
        private void combo()
        {
            for (int i = 1; i <= 52; i++)
            {
                comboBox.Items.Add(i);
            }
            comboBox1.Items.Add(2013);
            comboBox1.Items.Add(2014);
           
        }

        [DelimitedRecord(",")]
        private class Total
        {


            public string Supplier;

            public string Product;

            public double Price;


        }
        calmenu cal = new calmenu();
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try {
                string cobobox = comboBox.Text;
                string cobobox1 = comboBox1.Text;



                Task t1 = new Task(() =>
                {
                    string thePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    thePath = cal.supplierpath();
                    string complile = "_" + cobobox + "_" + cobobox1 + ".csv";
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

                        MessageBox.Show("Total cost of order for a week for all stores is: " + sumall);

                    }
                    else
                    {
                        System.Windows.MessageBox.Show("You have not selected a path yet");
                    }
                });
                t1.Start();
                sumall = 0;
            }
            catch
            {
                MessageBox.Show("Please select a store and supplier path first or file path inncorrect");
                Hide();
                cal.ShowDialog();
            }
            }
    }
}
