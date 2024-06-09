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
using System.Windows.Shapes;

namespace Projekt_Logik_2
{
    /// <summary>
    /// Interakční logika pro FinalWindow.xaml
    /// </summary>
    public partial class FinalWindow : Window
    {
        public FinalWindow(bool win = false)
        {
            InitializeComponent();
            ResultMessage.Text = win ? "Bitch u win >:(" : "Nigga u lost >:DDD";
            Application.Current.Shutdown();
            Show();
        }
    }
}
