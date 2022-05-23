// See https://aka.ms/new-console-template for more information
using WebServer;
using System.Net;

static string SendResponse(HttpListenerRequest request)
{
    var context = new Request(request);

    var type = TypeExtension.GetControllerType(context.ControllerName);
    if (type == null)
    {
        return "";
    }

    var controller = (Controller?)Activator.CreateInstance(type);
    var method = type.FindMethod(context.ActionName ?? "Index");
    if (method == null)
    {
        return "";
    }

    controller.RequestContext = context;
    var obj = method.Invoke(controller, new object[] { });
    if (obj == null)
    {
        return "";
    }
    return obj.ToString();


    //var buffer = new byte[request.ContentLength64];
    //request.InputStream.Read(buffer, 0, buffer.Length);

    //var obj = Document.Parse(buffer.UTF8());

    //return new Document
    //{
    //    Code = 0,
    //    Message = "OK",
    //    Value = obj,
    //}.ToString();
    //return string.Format("<HTML><BODY>My web page.<br>{0}</BODY></HTML>", DateTime.Now);
}

var ws = new MyWebServer(SendResponse, "http://localhost:8080/test/");
TypeExtension.LoadTypes(ws.GetType().Assembly, null);

ws.Run();

Console.WriteLine("A simple webserver. Press a key to quit.");
Console.ReadKey();
ws.Stop();
