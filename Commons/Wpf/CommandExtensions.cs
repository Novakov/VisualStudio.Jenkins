using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Commons.Wpf
{
    public static class CommandExtensions
    {
        public static void ExecuteIfCan(this ICommand command, object parameter)
        {
            if (command != null && command.CanExecute(parameter))
            {
                command.Execute(parameter);
            }
        }

        public static ICommand AllowWhenNoErrors(this ICommand command, IDataErrorInfo viewModel)
        {
            return new DelegateCommand(command.Execute, x => string.IsNullOrWhiteSpace(viewModel.Error) && command.CanExecute(x));
        }
    }
}
