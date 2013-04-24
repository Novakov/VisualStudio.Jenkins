﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Niles.Model;
//using Niles.Model;

namespace JenkinsBuilds.Pages
{
    /// <summary>
    /// Interaction logic for BuildsPageView.xaml
    /// </summary>
    public partial class BuildsPageView : UserControl
    {
        public BuildsPageView()
        {
            InitializeComponent();
        }

        public void LoadJobs(IEnumerable<Job> jobs)
        {
            this.jobsList.ItemsSource = jobs;
        }
    }
}