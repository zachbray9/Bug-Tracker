using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.ViewModels.Factories
{
    public class CreateAccountPageViewModelFactory : IViewModelFactory<CreateAccountPageViewModel>
    {
        public CreateAccountPageViewModel CreateViewModel()
        {
            return new CreateAccountPageViewModel();
        }
    }
}
