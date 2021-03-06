﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsBuilds.Configuration;
using JenkinsBuilds.Model;
using JenkinsBuilds.Properties;
using Microsoft.TeamFoundation.Controls;
using Niles.Client;
using Niles.Model;
using Jenkins;
using VisualStudio.TeamExplorer;
using Commons.Wpf;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerPage(JobsPage.PageId, ParentPageId = BuildsPage.PageId)]
    public class JobsPage : TeamExplorerPageBase<JobsPageView>
    {
        public const string PageId = "{074B6E0F-2690-4BAF-9CD3-F98C917DA15C}";        

        private JenkinsInstance instance;

        private IClientFactory clientFactory;

        public new JobsPageViewModel ViewModel { get { return (JobsPageViewModel)base.ViewModel; } }

        [ImportingConstructor]
        public JobsPage(IClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        private void AddToFavorites(JobModel job)
        {   
            Properties.Settings.Default.AddJob(new Uri(this.instance.Url), job.Url);
            Properties.Settings.Default.Save();

            this.Refresh();
        }

        public override void Initialize(object sender, PageInitializeEventArgs e)
        {
            base.Initialize(sender, e);

            this.instance = (JenkinsInstance)e.Context;

            this.Title = "Jobs on " + instance.DisplayName;
        }

        protected override ViewModelBase CreateViewModel()
        {
            return new JobsPageViewModel
            {
                AddToFavoritesCommand = new DelegateCommand<JobModel>(this.AddToFavorites),
                RemoveFromFavoritesCommand = new DelegateCommand<JobModel>(this.RemoveFromFavorites)
            };
        }

        public override void Loaded(object sender, PageLoadedEventArgs e)
        {
            this.DuringBusy(async () => await this.LoadAsync());
        }

        public override void SaveContext(object sender, PageSaveContextEventArgs e)
        {
            e.Context = this.instance;
        }

        public override void Refresh()
        {
            this.DuringBusy(async () => await this.LoadAsync());
        }

        private async Task LoadAsync()
        {
            var client = this.clientFactory.GetClient(this.instance.Url);

            Node node;

            try
            {
                node = await client.GetResourceAsync<Node>(this.instance.Url, "jobs[" + JobModel.FetchTree + "]");
            }
            catch (Exception e)
            {
                this.ShowError("Unable to contact Jenkins instance at {0}: {1}", this.instance.Url, e.Message);

                return;
            }

            this.ViewModel.Jobs = new ObservableCollection<JobModel>(node.Jobs.Select(x => new JobModel().LoadFrom(x)).ToArray());
        }

        protected override JobsPageView CreateView()
        {
            return new JobsPageView();
        }        

        private void RemoveFromFavorites(JobModel job)
        {            
            Settings.Default.RemoveJob(job.Url);
            Settings.Default.Save();

            job.IsFavorite = false;
        }
    }
}
