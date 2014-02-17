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

namespace BugTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SolidColorBrush brush_selected;
        SolidColorBrush brush_idle;
        DbConnect gDb = new DbConnect();
        String error_msg = "";
        Error err;

        /*****************************************************************************/
        public MainWindow()
        /*****************************************************************************/
        {
            int status;
            String projectName = "";
            String[] users_list = { };
            err = new Error();
            // ----------------------------------------

            // Initialize Brushes
            brush_selected = new SolidColorBrush();
            brush_selected.Color = (Color)FindResource(SystemColors.HighlightColorKey);

            brush_idle = new SolidColorBrush();
            brush_idle.Color = (Color)FindResource(SystemColors.ControlLightColorKey);

            InitializeComponent();

            // TODO: Check for database, create if it doesn't exist

            // Set project name
            status = gDb.GetProjectName(ref projectName);
            if (status != 0)
            {
                projectName = "[Initialize Project]";
            }
            Txt_ProjectName.Text = projectName;

            // Set user dropdown list
            status = gDb.Get_UserList(ref users_list);
            if (status != 0)
            {
                gDb.Get_DbError_String(status, ref error_msg);
                err.Report_Error(error_msg);
            }
            foreach (String user in users_list)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = user;
                CB_Users.Items.Add(item);
            }

            // TODO: Populate bug list
        }

        /*****************************************************************************/
        private void OpenForm_CreateNew(object sender, RoutedEventArgs e)
        /*****************************************************************************/
        {
            Window_CreateNew cn = new Window_CreateNew();
            cn.Owner = Application.Current.MainWindow;
            cn.Show();
        }

        /*****************************************************************************/
        private void Show_Open(object sender, RoutedEventArgs e)
        /*****************************************************************************/
        {
            // highlight selected color
            Btn_ShowOpen.Background = brush_selected;
            Btn_ShowOpen.Foreground = Brushes.White;

            // dim the other
            Btn_ShowClosed.Background = brush_idle;
            Btn_ShowClosed.Foreground = Brushes.Black;

            // refresh bug list showing open bugs
        }

        /*****************************************************************************/
        private void Show_Closed(object sender, RoutedEventArgs e)
        /*****************************************************************************/
        {
            // highlight selected color
            Btn_ShowClosed.Background = brush_selected;
            Btn_ShowClosed.Foreground = Brushes.White;

            // dim the other
            Btn_ShowOpen.Background = brush_idle;
            Btn_ShowOpen.Foreground = Brushes.Black;

            // refresh bug list showing closed bugs
        }

        /*****************************************************************************/
        private void UpdateProjectTitle(object sender, RoutedEventArgs e)
        /*****************************************************************************/
        {
            int status;
            String title = "";
            // ----------------------------------------

            gDb.QueryUser_ProjectTitle();

            status = gDb.GetProjectName(ref title);
            if (status != 0)
            {
                gDb.Get_DbError_String(status, ref error_msg);
                err.Report_Error(error_msg);
            }

            Txt_ProjectName.Text = title;
        }
    }
}
