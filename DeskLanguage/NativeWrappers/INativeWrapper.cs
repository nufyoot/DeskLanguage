using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskLanguage.NativeWrappers
{
    public interface INativeWrapper
    {
        IEnumerable<Models.Window> ListWindows();
    }
}
