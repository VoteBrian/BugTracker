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
        DbConnect gDb = new DbConnect();

        /*****************************************************************************/
        public Window_CreateNew()
        /*****************************************************************************/
        {
            InitializeComponent();
        }

        /*****************************************************************************/
        private void Update_Priority_Label(object sender, RoutedPropertyChangedEventArgs<double> e)
        /*****************************************************************************/
        {
            String priority_string = "";
            Brush priority_color;

            switch((int)slider_priority.Value) {
                case 1:
                    priority_string = "LOW";
                    priority_color = Brushes.LightGray;
                    break;
                case 2:
                    priority_string = "MEDIUM";
                    priority_color = Brushes.Yellow;
                    break;
                case 3:
                    priority_string = "HIGH";
                    priority_color = Brushes.Red;
                    break;
                default:
                    priority_string = "ERROR";
                    priority_color = Brushes.Red;
                    break;
            }

            // Update label text
            // On window initialization, Update_Priority_Label is called before label_priority is intialized
            // It will therefore be null here and fail setting Content.
            if (label_priority != null)
            {
                label_priority.Content = priority_string;
                circle_priority.Fill = priority_color;
            }
        }

        private void CreateNewBug(object sender, RoutedEventArgs e)
        {
            int priority;
            String title, desc;
            int status;
            // ----------------------------------------

            // Bug title
            title = Txt_BugTitle.Text;
            if (String.Compare(title, "") == 0)
            {
                // Highlight the Title field
                Txt_BugTitle.BorderBrush = Brushes.Red;
                return;
            }

            // Bug description
            desc = Txt_BugDesc.Text;
            
            // Bug priority
            priority = (int)slider_priority.Value;

            // Insert new bug to database
            status = gDb.CreateNewBug(title, desc, priority);
            if (status != 0)
            {
                // error handling
            }

            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
