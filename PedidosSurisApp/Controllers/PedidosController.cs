using Microsoft.AspNetCore.Mvc;
using PedidosSurisApp.Models;
using System.Text;

namespace PedidosSurisApp.Controllers
{
    [ApiController]
    [Route("")]
    public class PedidosController : ControllerBase
    {
        [HttpPost("api/pedido")]
        public IActionResult GuardarPedido([FromBody] Pedido pedido)
        {
            if (!ValidarPedido(pedido))
            {
                string erroresPedidos = "";
                foreach (string strError in pedido.Errores)
                {
                    erroresPedidos += new StringBuilder("- " + strError);
                }
                pedido.strError = erroresPedidos;
                return BadRequest(new { strError = pedido.strError }); 
            }
            else
            {
                pedido.FechaPedido = DateTime.Now;
                pedido.TotalPedido = pedido.Articulos.Sum(a => a.Precio);
                return Ok(new { success = true, mensaje = "Pedido guardado con éxito.", fecha = pedido.FechaPedido, total = pedido.TotalPedido });
            }
        }

        private bool DescripcionValida(string descripcion)
        {
            return descripcion.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c));
        }

        private bool ValidarPedido(Pedido objPedido)
        {
            ArticulosController ctrlArticulo = new ArticulosController();
            objPedido.Errores = new List<string>();

            // Validar que se haya seleccionado al menos un artículo
            if (objPedido.Articulos == null || objPedido.Articulos.Count == 0)
            {
                objPedido.Errores.Add("Debe seleccionar al menos un artículo.");
            }

            // Validar que el vendedor esté presente
            if (string.IsNullOrEmpty(objPedido.Vendedor))
            {
                objPedido.Errores.Add("Debe seleccionar un vendedor.");
            }

            // Obtener todos los artículos disponibles
            var articulosDisponibles = ctrlArticulo.ObtenerArticulos();

            // Validar que los artículos seleccionados existan en la lista de artículos disponibles y que cumplan con las reglas
            var articulosValidos = objPedido.Articulos
                .Where(a => articulosDisponibles.Any(ad => ad.Codigo == a.Codigo && ad.Deposito == 1 && ad.Precio > 0 && DescripcionValida(a.Descripcion)))
                .ToList();

            // Verificar si todos los artículos seleccionados son válidos
            if (articulosValidos.Count != objPedido.Articulos.Count)
            {
                objPedido.Errores.Add("Algunos de los artículos seleccionados no son válidos.");
            }

            return objPedido.Errores.Count == 0;
        }
    }
}
