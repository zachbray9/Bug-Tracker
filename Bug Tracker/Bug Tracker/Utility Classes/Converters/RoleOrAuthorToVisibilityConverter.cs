using BugTracker.Domain.Enumerables;
using BugTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Bug_Tracker.Utility_Classes.Converters
{
    public class RoleOrAuthorToVisibilityConverter : IValueConverter
    {
        //this class checks if the Current User is an Admin or the author of a ticket/comment and returns visibility based on that
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is ProjectUser projectUser))
                return Visibility.Collapsed;

            bool isAdmin = projectUser.Role == ProjectRole.Administrator;
            bool isAuthor = false;

            //checks if parameter is a ticket or comment
            if (parameter is Ticket ticket)
                isAuthor = projectUser.Id == ticket.AuthorId;
            if (parameter is Comment comment)
                isAuthor = projectUser.Id == comment.AuthorId;

            if (isAdmin || isAuthor)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
