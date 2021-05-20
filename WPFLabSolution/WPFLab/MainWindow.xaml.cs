using System;
using System.IO;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibraryWPFLab;

namespace WPFLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string filePath = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\students.csv";

        static int countLines = GetCount(filePath);

        Student[] stud = GetStud(filePath, countLines);

        public MainWindow()
        {
            InitializeComponent();
        }

        static Student[] GetStud(string filePath, int count)
        {
            string strRead; //holds line that is read

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            StreamReader readerStud = new StreamReader(fs);

            strRead = readerStud.ReadLine();

            Student[] studCreate = new Student[count];

            for (int counter = 1; counter < count; ++counter)
            {
                strRead = readerStud.ReadLine();

                string[] Parts = strRead.Split(',');

                studCreate[counter] = new Student();

                studCreate[counter].Year = Parts[0];
                studCreate[counter].Major = Parts[1];
                studCreate[counter].First = Parts[2];
                studCreate[counter].Last = Parts[3];
                studCreate[counter].Occupation = Parts[4];
            }

            readerStud.Close();
            fs.Close();

            return studCreate;
        }

        private void btnYear_Click(object sender, RoutedEventArgs e)
        {
            string chosen = "";
            try
            {
                ListBoxItem chosenItem = (ListBoxItem)lboYear.SelectedItem;
                chosen = chosenItem.Content.ToString();

                //MessageBox.Show(chosen);
            }
            catch
            {
                MessageBox.Show("Choose a year", "Students");
                return;
            }

            string display = "The Student with " + chosen + " graduation year:\n";

            var queryResults = from s in stud
                               where s?.Year == chosen //  ?, allow this variable to be null
                               select s; //grab the whole object

            foreach (var Name in queryResults)
            {
                display += Name.First + " " + Name.Last + "\n";
            }
            MessageBox.Show(display);
        }

        static int GetCount(string file)
        {
            int count = 0;
            string strRead;
            FileStream sr = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader readerCount = new StreamReader(sr);

            strRead = readerCount.ReadLine();

            while (strRead != null)
            {
                ++count;
                strRead = readerCount.ReadLine();
            }

            readerCount.Close();
            sr.Close();

            return count;
        }

        private void btnMajor_Click(object sender, RoutedEventArgs e)
        {
            string chosen;
            try
            {
                //MessageBox.Show("You selected " + lboMajor.SelectedItem);
                ListBoxItem lboItem = lboMajor.SelectedItem as ListBoxItem;
                chosen = lboItem.Content.ToString();
            }
            catch
            {
                MessageBox.Show("Choose a major", "Students");
                return;
            }

            string display = "The Student who studied " + chosen + ":\n";

            var queryResults = from s in stud
                               where s?.Major == chosen //  ?, allow this variable to be null
                               select s; //grab the whole object

            foreach (var Name in queryResults)
            {
                display += Name.First + " " + Name.Last + "\n";
            }
            MessageBox.Show(display);
        }

        //private void Grid_Loaded(object sender, RoutedEventArgs e)
        //{
        //    string display = "";
        //    for (int counter = 1; counter < countLines; ++counter)
        //    {
        //        display += stud[counter].Year;
        //        display += " Year: ";
        //        display += stud[counter].Major;
        //        display += " Major: ";
        //        display += stud[counter].First;
        //        display += " Name: ";
        //        display += stud[counter].Last;
        //        display += " ";
        //        display += stud[counter].Occupation;
        //       display += " Occupation: ";
        //    }
        //    MessageBox.Show(display);
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
