using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Commons.Wpf;
using FluentValidation;
using VisualStudio.TeamExplorer;

namespace JenkinsBuilds.Pages
{
    [InjectValidation]
    [PropertyChanged.ImplementPropertyChanged]
    public class AddJenkinsPageViewModel : ViewModelBase
    {
        public string DisplayName { get; set; }
        public string Url { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public bool RequiresAuthentication { get; set; }
    }

    public class AddJenkinsPageViewModelValidator : AbstractValidator<AddJenkinsPageViewModel>
    {
        public AddJenkinsPageViewModelValidator()
        {
            this.RuleFor(x => x.DisplayName).NotEmpty();
            this.RuleFor(x => x.Url)
                .NotEmpty()
                .Must(s => Uri.IsWellFormedUriString(s, UriKind.Absolute));
        }
    }
}
