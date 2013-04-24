using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Niles.Client;

namespace JenkinsBuilds
{
    public static class JenkinsClientExtensions
    {
        public static async Task<T> GetResourceAsync<T>(this IJenkinsClient @this, string url, string tree = "")
        {
            return await Task.Factory.StartNew(() => @this.GetResource<T>(new Uri(url), tree));
        }
    }
}