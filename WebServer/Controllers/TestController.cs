using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Controllers
{
    internal class TestController : Controller
    {
        public Object Index()
        {
            return View();
        }
    }
}
