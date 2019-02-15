using Projekat.HelperClasses;
using Projekat.Models;
using System;
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

namespace Projekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NetworkModel nm = new NetworkModel();

        public MainWindow()
        {
            InitializeComponent();
            InitializeNetworkModel();
        }

        private void InitializeNetworkModel()
        {
            DataIO dio = new DataIO();
            nm = dio.DeSerializeObject<NetworkModel>(@"../../XMLData/Geographic.xml");
        }
    }
}
