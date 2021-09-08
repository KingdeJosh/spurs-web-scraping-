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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Spurs
{
    
    /// <summary>
    /// Interaction logic for TCAOsinglestore.xaml
    /// </summary>
    public partial class TCAOsinglestore : Window
    {
        ArrayList Allst = new ArrayList();
       
        OpenFileDialog fld = new OpenFileDialog();
        
        Stores1[] result =null;
        string File_Name;
        string abbrevation;
        double sumall;
        public TCAOsinglestore()
        {
            InitializeComponent();
            combo();
        }
        calmenu cal = new calmenu();
        private void combo()
        {
            try {
                bool fileExists = false;
                string thePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                thePath = cal.storepath();
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
                System.Windows.MessageBox.Show("Please select a store and supplier path first");
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
        private class Stores
        {

            public string Abb;

            public string StoreName;



        }
        [DelimitedRecord(",")]
        private class Stores1
        {

            
            public string Supplier;

            public string Product;

            public double Price;


        }
       

        private void btnImport_Click(object sender, RoutedEventArgs e)
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
                        if (row.StoreName.Equals(comboBox.Text))
                        {
                            abbrevation = row.Abb;
                            System.Windows.MessageBox.Show(abbrevation);
                        }

                    }
                   
                }

            }
            if (Directory.GetFiles(thePath, "*.csv").Length > 0)
            {

                foreach (string file in Directory.GetFiles(thePath, "*.csv"))
                {
                    if (file.Contains(abbrevation)) {
                        DataTable dtt = new DataTable();
                        FileHelperEngine engine = new FileHelperEngine(typeof(Stores1));
                        result = engine.ReadFile(file) as Stores1[];


                        foreach (Stores1 c in result.AsParallel())
                        {
                            Allst.Add(c);
                            sumall += c.Price; 
                        }
                        dtt.Columns.Add("Suplier", typeof(string));
                        dtt.Columns.Add("Suplier Type", typeof(string));
                        dtt.Columns.Add("Price", typeof(string));

                        foreach (Stores1 c in Allst.AsParallel())
                        {
                          
                                dtt.Rows.Add(c.Supplier, c.Product, c.Price);

                        }

                        dataGridvs.DataContext = dtt.DefaultView;
                    }
                    else
                    {
                        //System.Windows.MessageBox.Show("Error 404 Found");
                    }
                }
                Totalno.Content = Convert.ToString(sumall);
            }
            else
            {
                System.Windows.MessageBox.Show("You have not selected a path yet");
            }
        }

    }
}
