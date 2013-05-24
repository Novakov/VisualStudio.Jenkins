using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudio
{
    public interface IStatusBar
    {
        void Progress(string text, bool isCompleted, uint completed, uint total);
    }
}
