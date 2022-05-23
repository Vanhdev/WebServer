using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    public class Controller
    {
        public Request? RequestContext { get; set; }
        public object View(string? masterName, string? viewName, object? model)
        {
            if (masterName == null)
            {
                masterName = "Views/master.html";
            }
            if (viewName == null)
            {
                viewName = "Views/" + RequestContext.ControllerName + "/" + RequestContext.ActionName + ".html";
            }
            try
            {
                var layout = File.ReadAllText(masterName);
                var body = File.ReadAllText(viewName);

                return string.Format(layout, body);
            }
            catch
            {
                return "<html><head/><body></body></html>";
            }
        }
        public object View() => View(null, null, null);
        public object View(object? model) => View(null, null, model);
        public object View(string? viewName, object? model) => View(null, viewName, model);
        public object ApiResult(int code, string message, object value)
        {
            return new Document { 
                Url = RequestContext?.ControllerName + "_" + RequestContext?.ActionName,
                Code = code,
                Value = value,
            };
        }
    }
}
