using System.Web.Mvc;

namespace ToDoListTarea2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction(
                "Index",
                "Tareas"
            );
        }
    }
}