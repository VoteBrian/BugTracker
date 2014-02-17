using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BugTracker
{
    class Error
    {
        /*****************************************************************************/
        public void Report_Error(String error,
            [CallerMemberName] String function = "",
            [CallerFilePath] String file = "",
            [CallerLineNumber] int line = 0)
        /*****************************************************************************/
        {
            String title = "Error";
            String message = String.Format("Message: {0}\nFunction: {1}\nFile: {2}\nLine: {3}",
                error, function, file, line);
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBoxButton btn = MessageBoxButton.OK;
            MessageBox.Show(message, title, btn, icon);
        }
    }
}
