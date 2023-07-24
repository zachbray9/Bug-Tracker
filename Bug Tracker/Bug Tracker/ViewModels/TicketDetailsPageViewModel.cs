﻿using Bug_Tracker.State;
using Bug_Tracker.State.Model_States;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Bug_Tracker.ViewModels
{
    public class TicketDetailsPageViewModel : ViewModelBase, IDisposable
    {
        public IProjectContainer ProjectContainer { get; }
        private readonly IDataService<Ticket> TicketDataService;

        public Ticket CurrentTicket => ProjectContainer.CurrentTicket;

        //DebounceTimer is so that changes are only saved to the database after a few seconds of inactivity so the thread isn't overloaded with db requests.
        private DispatcherTimer DebounceTimer { get; }

        public TicketDetailsPageViewModel(IProjectContainer projectContainer, IDataService<Ticket> ticketDataService)
        {
            ProjectContainer = projectContainer;
            TicketDataService = ticketDataService;

            DebounceTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(500),
                IsEnabled = false  
            };

            DebounceTimer.Tick += DebounceTimer_Tick;

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
                StartDebounceTimer();   
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
                StartDebounceTimer();
            }
        }


        private async Task SaveChangesAsync()
        {
            CurrentTicket.Title = TicketTitle;
            CurrentTicket.Description = TicketDescription;

            await TicketDataService.Update(CurrentTicket.Id, CurrentTicket);
            System.Diagnostics.Debug.WriteLine("Changes have been saved");
        }

        private void StartDebounceTimer()
        {
            if(DebounceTimer.IsEnabled)
            {
                DebounceTimer.Stop();
            }

            DebounceTimer.Start();
        }

        private async void DebounceTimer_Tick(object sender, EventArgs e)
        {
            DebounceTimer.Stop();
            await SaveChangesAsync();
        }

        public void Dispose()
        {
            DebounceTimer.Tick -= DebounceTimer_Tick;
        }
    }
}
