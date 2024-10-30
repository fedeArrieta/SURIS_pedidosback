using System.Text;

namespace PedidosSurisApp.Models
{
    public class Pedido
    {
        public string Vendedor { get; set; }
        public List<Articulo> Articulos { get; set; }
        public DateTime FechaPedido { get; set; }
        public decimal TotalPedido { get; set; }
        public List<string> Errores { get; set; }
        public string strError { get; set; }

    }
}
