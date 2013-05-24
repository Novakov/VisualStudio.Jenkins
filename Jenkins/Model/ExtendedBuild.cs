using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Niles.Model;

namespace Jenkins.Model
{
    public class ExtendedBuild : Build
    {
        public BuildActions Actions { get; set; }
    }
}
