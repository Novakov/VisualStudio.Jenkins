using System.ComponentModel.Composition;
namespace JenkinsBuilds.Properties
{    
    internal sealed partial class Settings
    {
        protected override void OnSettingsLoaded(object sender, System.Configuration.SettingsLoadedEventArgs e)
        {
            base.OnSettingsLoaded(sender, e);

            if (this.FavouriteJobs == null)
            {
                this.FavouriteJobs = new Configuration.FavouriteJobs();
            }

            if (this.Instances == null)
            {
                this.Instances = new Configuration.JenkinsInstances();
            }
        }
    }
}
