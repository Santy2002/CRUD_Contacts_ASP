using CRUD_ASP.Datos;
using CRUD_ASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CRUD_ASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _contexto;

        public HomeController(ApplicationDBContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Usuario.ToListAsync());
        }

        [HttpGet]
        public IActionResult Crear()    
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Usuario u)
        {
            if (ModelState.IsValid)
            {
                /* _contexto es la conexion con la base de datos por medio
                    de AppDbContext que tambien es donde esta seteado el modelo Usuario 
                Al estar heredando de DbContext puedo usar los metodos para el CRUD */
                _contexto.Usuario.Add(u);
                await _contexto.SaveChangesAsync();

                //Cuando termina te manda al index
                return RedirectToAction(nameof(Index));
            }

            //Si no se cumple la condicion le va a devolver la View con los mensajes de error
            //de las validaciones
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Usa el id que le llego, y busca el registro que tenga ese id en la base de datos
            var usuario = _contexto.Usuario.Find(id);

            if(usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Usuario u)
        {
            if (ModelState.IsValid)
            {
                _contexto.Update(u);
                await _contexto.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            //Si no se cumple la condicion le va a devolver la View con los mensajes de error
            //de las validaciones
            return View(u);
        }

        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Usa el id que le llego, y busca el registro que tenga ese id en la base de datos
            var usuario = _contexto.Usuario.Find(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Usa el id que le llego, y busca el registro que tenga ese id en la base de datos
            var usuario = _contexto.Usuario.Find(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarUsuario(int? id)
        {
            var usuario = await _contexto.Usuario.FindAsync(id);
            
            if(usuario == null)
            {
                return NotFound();
            }

            _contexto.Usuario.Remove(usuario);

            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}