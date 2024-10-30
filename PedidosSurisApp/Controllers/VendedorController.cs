using Microsoft.AspNetCore.Mvc;
using PedidosSurisApp.Models;
using Newtonsoft.Json;

namespace PedidosSurisApp.Controllers
{
    public class VendedoresController : ControllerBase
    {
        private List<Vendedor> ObtenerVendedores()
        {
            var jsonData = System.IO.File.ReadAllText("vendedores_challenge.json");
            
            var wrapper = JsonConvert.DeserializeObject<VendedoresWrapper>(jsonData);
            var listaVendedores = wrapper.vendedores;
            
            return listaVendedores; 
        }

        [HttpGet("api/vendedores")]
        public IActionResult GetVendedores()
        {
            var vendedores = ObtenerVendedores();
            return Ok(vendedores); 
        }
    }
    public class VendedoresWrapper
    {
        public List<Vendedor> vendedores { get; set; }
    }
}
