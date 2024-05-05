
using BackNotas.Data;
using BackNotas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet] //método que trae elementos de las notas
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes(){

            return await _context.Notes.ToListAsync();
        }

        //Detalles de notas 
        [HttpGet]
        public async Task<ActionResult<Note>> GetNote(int id){
            var note = await _context.Notes.FindAsync(id);

            if(note == null){
                return NotFound();
            }
            return note;
        }

        //Crear nota
        [HttpPost]
        public async Task <ActionResult<Note>> CreateNote(Note note){

             _context.Add(note);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetNote", new {id = note.Id}, note); //redicion a la action de get note
        }

        //Eliminar Nota 
        [HttpDelete("{id}")] //eliminar por el id 
        public async Task<IActionResult> DeleteNote(int id){

            var note = await _context.Notes.FindAsync(id); //buscar nota por su id
            if(note == null){
                return  NotFound();
            }
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        //Editar la nota
        [HttpPut("{Id}")]

        public async Task <IActionResult> PutNote(int Id, Note note){ 
            _context.Entry(note).State = EntityState.Modified; // ingresa a  la categoria en la base de datos y se establece el estado a modificado
            await _context.SaveChangesAsync();

            return NoContent(); // respuesta 204
        }
       
    }
}