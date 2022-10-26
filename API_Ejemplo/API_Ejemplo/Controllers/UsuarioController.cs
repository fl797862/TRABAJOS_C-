using MongoDB.Driver;
using API_Ejemplo.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace API_Ejemplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        MongoClient cliente;
        IMongoDatabase db;
        public UsuarioController()
        {
            cliente = new MongoClient("mongodb+srv://" +
                "fl797862:0lB1fhLUDdbwcppf@lecturas0.ofekenk.mongodb.net/" +
                "?retryWrites=true&w=majority");
            db = cliente.GetDatabase("Lecturas0");
        }

        private IMongoCollection<Usuario> coleccion() =>
            db.GetCollection<Usuario>("API_Ejemplo");

        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            return coleccion().AsQueryable();
        }

        [HttpGet("{id}")]
        public Usuario Get(string id)
        {

            return coleccion().Find(d => d.Id == id).SingleOrDefault();
        }


        [HttpPost]
        public Usuario Post([FromBody] Usuario value)
        {
            try
            {

                value.Id = Guid.NewGuid().ToString();
                coleccion().InsertOne(value);
                return value;
            }
            catch (Exception)
            {
                return value;
            }
        }


        [HttpPut("{id}")]
        public Usuario Put(string id, [FromBody] Usuario value)
        {
            try
            {
                coleccion().ReplaceOne(d => d.Id == id, value);
                return value;
            }
            catch (Exception)
            {
                return value;
            }

        }


        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            try
            {
                coleccion().DeleteOne(d => d.Id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
