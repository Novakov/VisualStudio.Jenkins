using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Niles.Client;
using Niles.Model;

namespace Jenkins
{
    public static class JenkinsClientExtensions
    {
        public static async Task<T> GetResourceAsync<T>(this IJenkinsClient @this, string url, string tree = "")
        {
            return await Task.Factory.StartNew(() => @this.GetResource<T>(new Uri(url), tree));
        }

        public static async Task<T> GetResourceAsync<T>(this IJenkinsClient @this, Uri url, string tree = "")
        {
            return await Task.Factory.StartNew(() => @this.GetResource<T>(url, tree));
        }

        public static async Task<T> ExpandAsync<T>(this IJenkinsClient @this, T @object, string tree = "")
            where T : class, IResource
        {
            return await Task.Factory.StartNew(() => @this.Expand<T>(@object, tree));
        }
    }
}