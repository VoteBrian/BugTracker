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
    /// Interaction logic for Window_CreateNew.xaml
    /// </summary>
    public partial class Window_CreateNew : Window
    {
        public Window_CreateNew()
        {
            InitializeComponent();
        }

        private void Update_Priority_Label(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            String priority = "";

            switch((int)slider_priority.Value) {
                case 1:
                    priority = "LOW";
                    break;
                case 2:
                    priority = "MEDIUM";
                    break;
                case 3:
                    priority = "HIGH";
                    break;
                default:
                    priority = "ERROR";
                    break;
            }

            // Update label text
            // On window initialization, Update_Priority_Label is called before label_priority is intialized
            // It will therefore be null here and fail setting Content.
            if (label_priority != null)
            {
                label_priority.Content = priority;
            }
        }
    }
}
