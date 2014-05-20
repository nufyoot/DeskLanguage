using DeskLanguage.NativeWrappers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DeskLanguage
{
    public class WindowController : ApiController
    {
        private INativeWrapper nativeWrapper;

        public WindowController(INativeWrapper nativeWrapper)
        {
            this.nativeWrapper = nativeWrapper;
        }

        [Route("window/list")]
        public IEnumerable<Models.Window> GetWindowList()
        {
            return nativeWrapper.ListWindows();
        }

        [HttpGet]
        [Route("window/find")]
        public IEnumerable<Models.Window> FindByName(string name)
        {
            return nativeWrapper.ListWindows()
                .Where(w => w.WindowName.IndexOf(name, System.StringComparison.OrdinalIgnoreCase) != -1);
        }
    }
}