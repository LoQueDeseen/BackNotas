
using BackNotas.Data;
using Microsoft.AspNetCore.Mvc;

namespace BackNotas.Controllers
{
    [Route("api/[controller]")] //ruta de l API (api/Notes);
    [ApiController] //Simplifica el proceso de creacion de APIs RestFULL
    public class NotesController : Controller
    {
        public  readonly NotasContext _context;

        public NotesController(NotasContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

       
    }
}