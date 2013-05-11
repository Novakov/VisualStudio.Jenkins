using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Niles.Model;

namespace JenkinsBuilds.Model
{
    [PropertyChanged.ImplementPropertyChanged]
    public class ChangeSetModel
    {
        public const string FetchTree = "changeSet[items[date,msg,author[fullName]]]";

        public string Author { get; set; }

        public DateTime Date { get; set; }

        public string Message { get; set; }

        public ChangeSetModel LoadFrom(ChangeSetItem set)
        {
            this.Author = set.Author.FullName;
            this.Date = set.Date;
            this.Message = set.Message;                

            return this;
        }
    }
}
