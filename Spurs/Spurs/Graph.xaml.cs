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
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Spurs
{
    /// <summary>
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class Graph : Window
    {
        ArrayList distct = new ArrayList();
        ArrayList distct1 = new ArrayList();
        ArrayList supNames = new ArrayList();
        ArrayList abbrevation = new ArrayList();
        public Graph()
        {
            InitializeComponent();
            loadpie();
        }
        private void move(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow st = new MainWindow();
            st.ShowDialog();
        }
        [DelimitedRecord(",")]
        private class Supplier
        {
            public string Suppliers;
            public string Suptype;
            public double cost;

        }
        calmenu cal = new calmenu();
        private void loadpie()
        {
            try
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

                            FileHelperEngine engine = new FileHelperEngine(typeof(Supplier));
                            //Read csv file
                            Supplier[] result = engine.ReadFile(fname) as Supplier[];
                            //create datatables
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



                                table.Rows.Add(row.Suptype);
                            }
                            //linq query to get only Supplier column 
                            var supply = (from DataRow type in table.AsEnumerable() select type["Supplier"]).Distinct().ToList();
                            //iterate and add distict values to table1
                            foreach (string row in supply)
                            {
                                Console.WriteLine(row);
                                distct.Add(row);


                            }
                        }

                    }

                }
                foreach (string grph in distct)
                {
                    if (Directory.GetFiles(thePath, "*.csv").Length > 0)
                    {

                        foreach (string fname in myPaths)
                        {
                            string theFile = @fname;

                            fileExists = File.Exists(theFile);
                            if (fileExists)
                            {

                                FileHelperEngine engine = new FileHelperEngine(typeof(Supplier));
                                DataTable dtt = new DataTable();
                                //Read
                                Supplier[] result = engine.ReadFile(fname) as Supplier[];
                                double sumall = 0;
                                foreach (Supplier row in result.AsParallel())
                                {
                                    if (row.Suptype.Equals(grph))
                                    {

                                        sumall += row.cost;
                                        // dtt.Rows.Add(row.Suppliers, row.Suptype, row.cost);
                                    }

                                }
                                abbrevation.Add(sumall);
                            }

                        }
                    }
                }


                string[] getnames = distct.ToArray(typeof(string)) as string[];
                double[] getcosts = abbrevation.ToArray(typeof(double)) as double[];


                ((ColumnSeries)Mychart.Series[0]).ItemsSource = new KeyValuePair<string, int>[]
                               {

                new KeyValuePair<string, int>(getnames[0],Convert.ToInt32(getcosts[0])),
                new KeyValuePair<string, int>(getnames[1],Convert.ToInt32(getcosts[1])),
                new KeyValuePair<string, int>(getnames[2],Convert.ToInt32(getcosts[2])),
                new KeyValuePair<string, int>(getnames[3],Convert.ToInt32(getcosts[3])),
                new KeyValuePair<string, int>(getnames[4],Convert.ToInt32(getcosts[4])),
                new KeyValuePair<string, int>(getnames[5],Convert.ToInt32(getcosts[5])),
                new KeyValuePair<string, int>(getnames[6],Convert.ToInt32(getcosts[6])),
                new KeyValuePair<string, int>(getnames[7],Convert.ToInt32(getcosts[7])),
                new KeyValuePair<string, int>(getnames[8],Convert.ToInt32(getcosts[8]))

                               };


            }
            catch
            {
                MessageBox.Show("Please select a store and supplier path first or incorrect path");
                Hide();
                cal.ShowDialog();
            }

        }
    }
}
