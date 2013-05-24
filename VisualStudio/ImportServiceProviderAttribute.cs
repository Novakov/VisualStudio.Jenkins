using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace VisualStudio
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    public class ImportServiceProviderAttribute : ImportAttribute
    {
        public ImportServiceProviderAttribute()
            : base(typeof(SVsServiceProvider))
        {

        }
    }
}
