using Microsoft.AspNetCore.Mvc;
using PedidosSurisApp.Models;
using Newtonsoft.Json;

namespace PedidosSurisApp.Controllers
{
    public class ArticulosController : ControllerBase
    {
        public List<Articulo> ObtenerArticulos()
        {
            var jsonData = System.IO.File.ReadAllText("articulos_challenge.json");
            var wrapper = JsonConvert.DeserializeObject<ArticulosWrapper>(jsonData);
            var listaArticulos = wrapper.articulos;

            return listaArticulos;
        }

        [HttpGet("api/articulos")]
        public IActionResult GetArticulos()
        {
            var articulos = ObtenerArticulos();
            return Ok(articulos);
        }
    }
    public class ArticulosWrapper
    {
        public List<Articulo> articulos { get; set; }
    }
}
