using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows.Input;

namespace Commons.Wpf
{
    public abstract class ViewModelBase
    {
        
    }

    public static class ViewModelBaseExtensions
    {
        public static T AllowWhenNoErrors<T>(this T @this, Expression<Func<T, ICommand>> commandProperty)
            where T : ViewModelBase
        {
            var property = (PropertyInfo)((MemberExpression)commandProperty.Body).Member;

            var command = (ICommand)property.GetValue(@this, null);

            command = command.AllowWhenNoErrors((IDataErrorInfo)@this);

            property.SetValue(@this, command);

            return @this;
        }
    }
}
