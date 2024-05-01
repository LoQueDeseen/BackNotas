using BackNotas.Data;
using Microsoft.AspNetCore.Mvc;
 
namespace BackNotas.Controllers{

    public class CategoriesController:Controller{

        public readonly  NotasContext _context;

        public CategoriesController(NotasContext context){
            _context= context;
        }


    }
}