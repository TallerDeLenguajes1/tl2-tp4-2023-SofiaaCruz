using EspacioPedidos;
using System.Text.Json;

namespace EspacioAccesoADatosPedidos;
public  class AccesoADatosPedido
{
    public  List<Pedido>? Obtener()
    {
        List<Pedido>? nuevaListaPedido = null;
        if(File.Exists("Pedidos.json"))
        {
            string jsonPedidos = File.ReadAllText("Pedidos.json");
            List<Pedido>? ListaAux = JsonSerializer.Deserialize<List<Pedido>>(jsonPedidos);
            if(ListaAux != null)
            {
                nuevaListaPedido = ListaAux;
            }
        }
        return nuevaListaPedido;
    }
    public virtual void Guardar(List<Pedido> pedidos)
    {
        string info = JsonSerializer.Serialize(pedidos);
        File.WriteAllText("Pedidos.json", info);
    }
}
