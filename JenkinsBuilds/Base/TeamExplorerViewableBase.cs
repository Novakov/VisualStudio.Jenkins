using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JenkinsBuilds.Base
{
    public abstract class TeamExplorerViewableBase<TView> : TeamExplorerBase
        where TView : UserControl
    {
        private bool isBusy;
        public bool IsBusy
        {
            get { return this.isBusy; }
            set { this.isBusy = value; this.RaisePropertyChanged(); }
        }

        private string title;
        public string Title
        {
            get { return this.title; }
            set { this.title = value; this.RaisePropertyChanged(); }
        }

        private TView view = null;
        public TView View
        {
            get { return this.view; }
            set { this.view = value; this.RaisePropertyChanged(); }
        }

        private ViewModelBase viewModel;
        public ViewModelBase ViewModel
        {
            get { return this.viewModel; }
            set { this.viewModel = value; this.RaisePropertyChanged(); }
        }

        protected abstract TView CreateView();

        protected virtual ViewModelBase CreateViewModel()
        {
            return null;
        }

        protected virtual void InitializeBase()
        {
            this.View = this.CreateView();
            this.ViewModel = this.CreateViewModel();

            if (this.View != null && this.View is FrameworkElement)
            {
                ((FrameworkElement)this.view).DataContext = this.ViewModel;
            }
        }

        protected void DuringBusy(Action action)
        {
            this.IsBusy = true;

            action();

            this.IsBusy = false;
        }

        protected async void DuringBusy(Func<Task> action)
        {
            this.IsBusy = true;

            await action();

            this.IsBusy = false;
        }
    }
}
