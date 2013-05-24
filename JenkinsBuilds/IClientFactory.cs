using System;
namespace JenkinsBuilds
{
    public interface IClientFactory
    {
        JenkinsClient GetClient(Uri serverUri);
    }
}
