using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Controls;

namespace VisualStudio.TeamExplorer
{
    public abstract class TeamExplorerBase : IDisposable, INotifyPropertyChanged
    {        
        private IServiceProvider serviceProvider;
        private bool contextSubscribed;

        public event PropertyChangedEventHandler PropertyChanged;

        [ImportServiceProvider]
        public IServiceProvider ServiceProvider
        {
            get { return this.serviceProvider; }
            set
            {
                if (this.serviceProvider != null)
                {
                    this.UnsubscribeContextChanges();
                }

                this.serviceProvider = value;

                if (this.serviceProvider != null)
                {
                    SubscribeContextChanges();
                }
            }
        }

        public T GetService<T>()
        {
            if (this.ServiceProvider != null)
            {
                return (T)this.ServiceProvider.GetService(typeof(T));
            }

            return default(T);
        }

        protected void SubscribeContextChanges()
        {
            if (this.ServiceProvider == null || this.contextSubscribed)
            {
                return;
            }

            var tfContextManager = GetService<ITeamFoundationContextManager>();
            if (tfContextManager != null)
            {
                tfContextManager.ContextChanged += ContextChanged;
                this.contextSubscribed = true;
            }
        }

        protected void UnsubscribeContextChanges()
        {
            if (this.ServiceProvider == null || !this.contextSubscribed)
            {
                return;
            }

            var tfContextManager = GetService<ITeamFoundationContextManager>();
            if (tfContextManager != null)
            {
                tfContextManager.ContextChanged -= ContextChanged;
            }
        }

        protected virtual void ContextChanged(object sender, ContextChangedEventArgs e)
        {
        }

        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void ShowError(string message, params object[] args)
        {
            this.GetService<ITeamExplorer>().ShowNotification(string.Format(message, args), NotificationType.Error, NotificationFlags.None, null, Guid.NewGuid());
        }
        
        public virtual void Dispose()
        {
            this.UnsubscribeContextChanges();
        }
    }
}
