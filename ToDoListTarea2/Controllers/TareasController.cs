using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ToDoListTarea2.Models;

namespace ToDoListTarea2.Controllers
{
    public class TareasController : Controller
    {
        private const string ClaveSession = "ListaTareas";


        private List<Tarea> ObtenerTareas()
        {
            if (Session[ClaveSession] == null)
            {
                Session[ClaveSession] =
                    new List<Tarea>();
            }

            return (List<Tarea>)Session[ClaveSession];
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Lista(string filtro = "todas")
        {
            List<Tarea> tareas =
                ObtenerTareas();

            IEnumerable<Tarea> resultado =
                tareas;


            if (filtro == "pendientes")
            {
                resultado =
                    tareas.Where(t =>
                        !t.Completada);
            }


            if (filtro == "completadas")
            {
                resultado =
                    tareas.Where(t =>
                        t.Completada);
            }


            return PartialView(
                "_ListaTareas",
                resultado.ToList()
            );
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Crear(
            string titulo,
            string descripcion)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                return Json(new
                {
                    ok = false,
                    mensaje =
                        "El título es obligatorio."
                });
            }


            List<Tarea> tareas =
                ObtenerTareas();


            int nuevoId =
                tareas.Count == 0
                    ? 1
                    : tareas.Max(t => t.Id) + 1;


            Tarea nuevaTarea =
                new Tarea
                {
                    Id = nuevoId,
                    Titulo = titulo.Trim(),
                    Descripcion =
                        descripcion == null
                            ? ""
                            : descripcion.Trim(),
                    Completada = false
                };


            tareas.Add(nuevaTarea);

            Session[ClaveSession] =
                tareas;


            return Json(new
            {
                ok = true,
                mensaje =
                    "Tarea creada correctamente."
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Editar(
            int id,
            string titulo,
            string descripcion)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                return Json(new
                {
                    ok = false,
                    mensaje =
                        "El título es obligatorio."
                });
            }


            List<Tarea> tareas =
                ObtenerTareas();


            Tarea tarea =
                tareas.FirstOrDefault(t =>
                    t.Id == id);


            if (tarea == null)
            {
                return Json(new
                {
                    ok = false,
                    mensaje =
                        "La tarea no existe."
                });
            }


            tarea.Titulo =
                titulo.Trim();

            tarea.Descripcion =
                descripcion == null
                    ? ""
                    : descripcion.Trim();


            Session[ClaveSession] =
                tareas;


            return Json(new
            {
                ok = true,
                mensaje =
                    "Tarea actualizada correctamente."
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CambiarEstado(
            int id)
        {
            List<Tarea> tareas =
                ObtenerTareas();


            Tarea tarea =
                tareas.FirstOrDefault(t =>
                    t.Id == id);


            if (tarea == null)
            {
                return Json(new
                {
                    ok = false,
                    mensaje =
                        "La tarea no existe."
                });
            }


            tarea.Completada =
                !tarea.Completada;


            Session[ClaveSession] =
                tareas;


            return Json(new
            {
                ok = true,
                mensaje =
                    "Estado actualizado correctamente."
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Eliminar(
            int id)
        {
            List<Tarea> tareas =
                ObtenerTareas();


            Tarea tarea =
                tareas.FirstOrDefault(t =>
                    t.Id == id);


            if (tarea == null)
            {
                return Json(new
                {
                    ok = false,
                    mensaje =
                        "La tarea no existe."
                });
            }


            tareas.Remove(tarea);

            Session[ClaveSession] =
                tareas;


            return Json(new
            {
                ok = true,
                mensaje =
                    "Tarea eliminada correctamente."
            });
        }
    }
}