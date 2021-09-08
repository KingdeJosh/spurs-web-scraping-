using FileHelpers;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for AllStores.xaml
    /// </summary>
    public partial class AllStores : Window
    {
        ArrayList Allstores = new ArrayList();
       
        OpenFileDialog Filedialog = new OpenFileDialog();
        int no = 0;
        public AllStores()
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
        [DelimitedRecord(",")]
        private class Stores
        {
            
            public string Abb;

            public string StoreName;

            
        }
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            Filedialog.DefaultExt = ".csv";
            Filedialog.Filter = "Comma Separated Values(.csv)|*.csv";

            Filedialog.Multiselect = true;
            if (Filedialog.ShowDialog() == true)
            {

                textbox_csv.Text = Filedialog.FileName;

            }
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try {
                
                foreach (String fileName in Filedialog.FileNames)
                {

                    FileHelperEngine engine = new FileHelperEngine(typeof(Stores));

                    //Read
                    Stores[] result = engine.ReadFile(fileName) as Stores[];
                    DataTable table = new DataTable();
                    foreach (Stores row in result.AsParallel())
                    {
                        Allstores.Add(row);

                    }

                    table.Columns.Add("Store Abberviation", typeof(string));
                    table.Columns.Add("Store Name", typeof(string));

                    //for (int x = 0; x < Name.Count; x++)
                    //{
                    //    CSV[] yeild = engine.ReadFile(Name[x].ToString()) as CSV[];
                    //}
                    //IEnumerable<Stores> suptype = from Stores type in Allst.AsParallel() select type;
                    foreach (Stores row in Allstores.AsParallel())
                    {
                        Console.WriteLine(row.Abb + " " + row.StoreName);

                        table.Rows.Add(row.Abb, row.StoreName);


                    }
                    dataGridvs.DataContext = table.DefaultView;

                }
            }
            catch
            {
                MessageBox.Show("Incorrect File Type. Please Import Correct File");
            }
            }

      
    }
}
