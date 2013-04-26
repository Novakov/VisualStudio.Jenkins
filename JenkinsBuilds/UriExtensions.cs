using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenkinsBuilds
{
    public static class UriExtensions
    {
        public static Uri AppendPath(this Uri uri, string path)
        {
            var builder = new UriBuilder(uri);
            builder.Path += path;

            return builder.Uri;
        }
    }
}
