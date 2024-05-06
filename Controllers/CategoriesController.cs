
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
            // var UserId =idLogueado();
           // Console.WriteLine(UserId);

            return await _context.Categories.ToListAsync();// trae la lista de categorias en la base de datos 
        }

        //buscar por id

        [HttpGet("{Id}")] 
        public async Task<ActionResult<Category>> GetCategory(int Id){ 
            var category= await _context.Categories.FindAsync(Id);// trae la que encuantre por id

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

        // actualizar siguendo la anterior logica

        [HttpPut("{id}")]

        public async Task <IActionResult> PutCategory(int id, Category category){ // no me va a traer nada 

            if(id != category.Id){
                return BadRequest();
            }
            _context.Entry(category).State = EntityState.Modified; // ingresa a  la categoria en la base de datos y se establece el estado a modificado
            
            try{
                await _context.SaveChangesAsync();  
            }catch(DbUpdateConcurrencyException){

                
            }

            return NoContent(); // respuesta 204
        }

    // [HttpGet("idLogueado")]
    // public IActionResult idLogueado()
    // {
       
    //     var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        
    //     // Decodificar 
    //     var tokenHandler = new JwtSecurityTokenHandler(); //instancia para poder manipúlar tokends   
    //     var key = Encoding.ASCII.GetBytes(_secretKey); // la clave es usada para firmar los tokens 
    //     tokenHandler.ValidateToken(token, new TokenValidationParameters
    //     {
    //         ValidateIssuerSigningKey = true, // se debe de validadr la cklave 
    //         IssuerSigningKey = new SymmetricSecurityKey(key), // la usa para firmar 
    //         ValidateIssuer = false,
    //         ValidateAudience = false,
    //         ClockSkew = TimeSpan.Zero // no permite margen de tiempo con la hora actual
    //     }, out SecurityToken validatedToken);

    //     var jwtToken = (JwtSecurityToken)validatedToken;
        

    //     var userId = jwtToken.Claims.First(x => x.Type == "UserId").Value; //lo reclamo

  

    //     return Ok(userId);
    // }





    }
}