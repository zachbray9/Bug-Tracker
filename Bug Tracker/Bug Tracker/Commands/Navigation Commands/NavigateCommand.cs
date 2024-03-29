﻿using Bug_Tracker.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bug_Tracker.Commands.Navigation_Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly INavigator Navigator;

        public NavigateCommand(INavigator navigator)
        {
            Navigator = navigator;
        }

        public override void Execute(object parameter)
        {
            ViewType viewType = (ViewType)parameter;
            Navigator.Navigate(viewType);
        }
    }
}
