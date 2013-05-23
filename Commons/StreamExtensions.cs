using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public static class StreamExtensions
    {
        public static async Task CopyToAsync(this Stream @this, Stream destination, IProgress<ProgressReport> progress, long? totalSize = null)
        {
            var bufferSize = 81920;

            var array = new byte[bufferSize];

            int count = 0;
            long total = 0;

            progress.Report(new ProgressReport(0, totalSize ?? @this.Length));

            while ((count = await @this.ReadAsync(array, 0, array.Length)) != 0)
            {
                await destination.WriteAsync(array, 0, count);

                total += count;

                progress.Report(new ProgressReport(total, totalSize ?? @this.Length));
            }
        }   
    }
}
