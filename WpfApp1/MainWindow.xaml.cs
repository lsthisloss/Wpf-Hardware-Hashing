using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.Management;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
               string hardwareID = HardwareChecker.GetHardwareID();
               var hardwareInfo = HardwareChecker.GetDetailedHardwareInfo();
               textBlock.Text = "";
               textBlock.Text += $"\n CPU Type: {hardwareInfo["CPU_Type"]}";
               textBlock.Text += $"\n ID: {hardwareInfo["CPU_ID"]}";
               textBlock.Text += $"\n Cores: {hardwareInfo["CPU_Cores"]}";
               textBlock.Text += $"\n Threads: {hardwareInfo["CPU_Threads"]}";
               textBlock.Text += $"\n Manufacturer: {hardwareInfo["BIOS_Manufacturer"]}";
               textBlock.Text += $"\n BIOS_Manufacturer: {hardwareInfo["BIOS_Manufacturer"]}";
               textBlock.Text += $"\n GPU NAME: {hardwareInfo["GPU_Name"]}";
               textBlock.Text += $"\n Network MAC: {hardwareInfo["Network_MAC"]}"; 
                textBlock.Text += $"\n Hardware ID: {hardwareID}";
                
                bool isValid = HardwareChecker.ValidateHardwareID(hardwareID);
                textBlock.Text += $"\nHardware is {(isValid ? "valid" : "invalid")}";

            }
            catch (Exception ex)
            {
                textBlock.Text = $"Error: {ex.Message}";
            }

        }
    
        private void CopyKey_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Authenticate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void keyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
