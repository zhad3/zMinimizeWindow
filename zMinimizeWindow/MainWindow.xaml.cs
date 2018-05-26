using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace zMinimizeWindow
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ZProcess> Procs;

        public MainWindow()
        {
            Procs = new ObservableCollection<ZProcess>();
            InitializeComponent();
        }

        private void btnGetWindows_Click(object sender, RoutedEventArgs e)
        {
            Utils.UpdateProcessesList(ref Procs);
            listViewWindows.ItemsSource = Procs;
            foreach(ZProcess proc in Procs)
            {
                Console.WriteLine(proc);
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            ZProcess selectedProc = (ZProcess)listViewWindows.SelectedItem;
            Utils.MinimizeWindow(selectedProc);
        }

        private void btnForceMinimize_Click(object sender, RoutedEventArgs e)
        {
            ZProcess selectedProc = (ZProcess)listViewWindows.SelectedItem;
            Utils.ForceMinimizeWindow(selectedProc);
        }

        private void btnHideWindow_Click(object sender, RoutedEventArgs e)
        {
            ZProcess selectedProc = (ZProcess)listViewWindows.SelectedItem;
            Utils.HideWindow(selectedProc);
        }

        private void btnSysHide_Click(object sender, RoutedEventArgs e)
        {
            ZProcess selectedProc = (ZProcess)listViewWindows.SelectedItem;
            Utils.SysHideWindow(selectedProc);
        }
    }
}
