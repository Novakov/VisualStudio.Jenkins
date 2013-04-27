﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JenkinsBuilds.Pages
{
    [PropertyChanged.ImplementPropertyChanged]
    public class BuildsSectionViewModel : Base.ViewModelBase
    {
        public ObservableCollection<JobViewModel> Jobs { get; set; }

        public ICommand RemoveFromFavourites { get; set; }

        public ICommand BuildNowCommand { get; set; }
    }
}