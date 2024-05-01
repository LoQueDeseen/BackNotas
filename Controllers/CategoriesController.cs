
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
        // listar 
        [HttpGet]// traigo lista de categorías
        public async Task<ActionResult<IEnumerable<Category>>> GetUser(){ //Ienumerable trae una coleccion de categorías 
            return await _context.Categories.ToListAsync();// trae la lista de categorias en la base de datos 
        }

        //buscar por id

        [HttpGet("{Id}")] 
        public async Task<ActionResult<Category>> GetCategory(int Id){ 
            var category= await _context.Categories.FindAsync();// trae la que encuantre por id

            if (category==null){ // si no llega datos, retorna no encontrado 
                return NotFound();

            }
            return category;
        }

        // Crear categoría 

        [HttpPost] 

        public async Task<ActionResult<Category>> PostCategory( Category  category){
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCategory", new {id = category.Id}, category); // crea un objeto con un solo campo llamdo id (para la URL) y category es el objeto que se devuelve en respuesta :)
        }

        // elminar categoría 

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCategory(int Id){
            var category = await _context.Categories.FindAsync(Id); 
            if (category==null){
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        



    }
}