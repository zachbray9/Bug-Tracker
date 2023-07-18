using Bug_Tracker.State;
using Bug_Tracker.State.Model_States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.ViewModels
{
    public class TicketDetailsPageViewModel : ViewModelBase
    {
        public IProjectContainer ProjectContainer { get; }

        public TicketDetailsPageViewModel(IProjectContainer projectContainer)
        {
            ProjectContainer = projectContainer;
        }
    }
}
