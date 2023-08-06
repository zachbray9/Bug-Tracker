using Bug_Tracker.Commands.TicketDetailsPageCommands;
using Bug_Tracker.State;
using Bug_Tracker.State.Authenticators;
using Bug_Tracker.State.Model_States;
using BugTracker.Domain.Models;
using BugTracker.Domain.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Bug_Tracker.ViewModels
{
    public class TicketDetailsPageViewModel : ViewModelBase, IDisposable
    {
        private readonly IAuthenticator Authenticator;
        public IProjectContainer ProjectContainer { get; }
        private readonly IDataService<Ticket> TicketDataService;
        private readonly IDataService<Comment> CommentDataService;

        public Ticket CurrentTicket => ProjectContainer.CurrentTicket;
        public bool DoesCommentTextBoxContainText { get => !CommentTextBoxText.IsNullOrEmpty(); }


        //DebounceTimer is so that changes are only saved to the database after a few seconds of inactivity so the thread isn't overloaded with db requests.
        private DispatcherTimer DebounceTimer;

        public TicketDetailsPageViewModel(IAuthenticator authenticator, IProjectContainer projectContainer, IDataService<Ticket> ticketDataService, IDataService<Comment> commentDataService, DispatcherTimer debounceTimer)
        {
            Authenticator = authenticator;
            ProjectContainer = projectContainer;
            TicketDataService = ticketDataService;
            CommentDataService = commentDataService;
            DebounceTimer = debounceTimer;

            DebounceTimer.Interval = TimeSpan.FromMilliseconds(500);
            DebounceTimer.IsEnabled = false;

            DebounceTimer.Tick += DebounceTimer_Tick;

            ticketTitle = CurrentTicket.Title;
            ticketDescription = CurrentTicket.Description;

            Comments = new ObservableCollection<Comment>(CurrentTicket.Comments);

            AddCommentToDbCommand = new AddCommentToDbCommand(Authenticator, TicketDataService, CommentDataService, this);

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

        private ObservableCollection<Comment> comments;
        public ObservableCollection<Comment> Comments 
        {
            get { return comments; }
            set
            {
                comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        private string commentTextBoxText;
        public string CommentTextBoxText
        {
            get { return commentTextBoxText; }
            set
            {
                commentTextBoxText = value;
                OnPropertyChanged(nameof(CommentTextBoxText));
                OnPropertyChanged(nameof(DoesCommentTextBoxContainText));
            }
        }


        private async Task SaveChangesAsync()
        {
            CurrentTicket.Title = TicketTitle;
            CurrentTicket.Description = TicketDescription;

            try
            {
                await TicketDataService.Update(CurrentTicket.Id, CurrentTicket);
                System.Diagnostics.Debug.WriteLine("Changes have been saved");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex}");
            }
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

        public ICommand AddCommentToDbCommand { get; }
    }
}
