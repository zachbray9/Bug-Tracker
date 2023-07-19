using Bug_Tracker.State;
using Bug_Tracker.State.Model_States;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
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
        private readonly IDataService<Ticket> TicketDataService;

        public Ticket CurrentTicket => ProjectContainer.CurrentTicket;

        public TicketDetailsPageViewModel(IProjectContainer projectContainer, IDataService<Ticket> ticketDataService)
        {
            ProjectContainer = projectContainer;
            TicketDataService = ticketDataService;

            TicketTitle = CurrentTicket.Title;
            TicketDescription = CurrentTicket.Description;
        }

        private string ticketTitle;
        public string TicketTitle
        {
            get { return ticketTitle; }
            set
            {
                ticketTitle = value;
                OnPropertyChanged(nameof(TicketTitle));

                if(TicketTitle != CurrentTicket.Title)
                {
                    SaveChanges();
                }
            }
        }

        private string ticketDescription;
        public string TicketDescription
        {
            get { return ticketDescription; }
            set
            {
                ticketDescription = value;
                OnPropertyChanged(nameof(TicketDescription));

                if(TicketDescription != CurrentTicket.Description)
                {
                    SaveChanges();
                }
            }
        }


        private async void SaveChanges()
        {
            CurrentTicket.Title = TicketTitle;
            CurrentTicket.Description = TicketDescription;

            await TicketDataService.Update(CurrentTicket.Id, CurrentTicket);
        }

    }
}
