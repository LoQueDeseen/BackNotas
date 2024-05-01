
using BackNotas.Data;
using BackNotas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 
namespace BackNotas.Controllers{
    [Route ("api/[controller]")]
    [ApiController]

    public class CategoriesController:Controller{

        public readonly  NotasContext _context;

        public CategoriesController(NotasContext context){
            _context= context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetUser(){ //Ienumerable trae una coleccion de categorías 
            return await _context.Categories.ToListAsync();// trae la lista de categorias en la base de datos 
        }


    }
}