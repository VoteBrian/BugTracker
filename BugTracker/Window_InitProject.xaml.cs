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

namespace BugTracker
{
    /// <summary>
    /// Interaction logic for Window_InitProject.xaml
    /// </summary>
    public partial class Window_InitProject : Window
    {
        public Window_InitProject()
        {
            InitializeComponent();
        }

        public void SetProjectName(object sender, RoutedEventArgs e)
        {
            int status;
            String name = Txt_ProjectName.Text;

            DbConnect db = new DbConnect();
            status = db.Update_ProjectName(name);
            if (status != 0)
            {
                // error handling
            }

            this.Close();
        }

        public void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
