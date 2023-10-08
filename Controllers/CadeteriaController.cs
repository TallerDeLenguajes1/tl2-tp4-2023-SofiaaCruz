using EspacioCadete;
using EmpresaDeCadeteria;
using EspacioPedidos;
using Microsoft.AspNetCore.Mvc;
using EspacioCliente;
namespace EspacioCadeteriaControlle;

[ApiController]
[Route("[controller]")]

public class CadeteriaController : ControllerBase
{
    private Cadeteria cadeteria;
    private readonly ILogger<CadeteriaController> logger;

    public CadeteriaController(ILogger<CadeteriaController> logger)
    {
        cadeteria = Cadeteria.GetInstance();
        this.logger = logger;
    }

    [HttpGet("GetCadeteria")]
    public ActionResult<string> GetNombreCadeteria()
    {
        string info = cadeteria.NombreCadeteria();
        return Ok(info);
    }
    [HttpGet("GetPedidos")]
    public ActionResult<List<Pedido>> GetPedidos()
    {
        List<Pedido>? listPedidos = cadeteria.GetPedidos();
        return Ok(listPedidos);
    }
    [HttpGet("GetCadetes")]
    public ActionResult<List<Cadete>> GetCadetes()
    {
        List<Cadete>? listCadetes = cadeteria.GetCadetes();
        return Ok(listCadetes);
    }

    [HttpGet("GetInforme")]
    public ActionResult<string> GetInforme()
    {
        return Ok(cadeteria.GetInforme());
    } 
    
    [HttpPost("CargarDatos")]
    public ActionResult<string> CargaDeDatos(string tipoDeAcceso)
    {
        if(cadeteria.CargaDeDatos(tipoDeAcceso))
        {
            return Ok("Datos cargados correctamente");
        }
        else
        {
            return BadRequest("ERROR, no se cargaron los datos correctamente");
        }
    }

    [HttpPost("AgregarPedido")]
    public ActionResult<string> AgregarPedido(Pedido pedido)
    {
        pedido.IdCadete = -9999;
        if(cadeteria.AgregarPedido(pedido))
        {
            return Ok("El Pedido fue agregado de manera exitosa");
        }
        else
        {
            return BadRequest("Error, no se pudo agregar el pedido");
        }
    }

    [HttpPut("AsignarPedido")]
    public ActionResult<string> AsignarPedido(int idPedido, int idCadete)
    {
        if(cadeteria.AsignarCadeteAPedido(idCadete, idPedido))
        {
            return Ok($"Pedidos asignado al cadete numero {idCadete}");
        }else
        {
            return Ok("El pedidos no pudo ser asignado");
        }
    }

    [HttpPut("CambiarEstadoPedido")]
    public ActionResult<string> CambiarEstadoPEdido(int idPedido, int nuevoEstado)
    {
        string estado;
        if(nuevoEstado == 1)
        {
            estado = "entregado";
        }else
        {
            estado = "cancelado";
        }
        if(cadeteria.CambiarEstadoPedido(idPedido, nuevoEstado))
        {
            return Ok($"El pedido fue {estado}");
        }else
        {
            return Ok($"El pedidos no fue {estado}");
        }
    }

    [HttpPut("CambiarCadetePedido")]
    public ActionResult<string> CambiarCadetePedido(int idPedido, int idNuevoCadete)
    {
        if(cadeteria.AsignarCadeteAPedido(idNuevoCadete, idPedido))
        {
            return Ok("El cadete fue cambiado");
        }
        else
        {
            return Ok("El cadete no fue cambiado");
        }
    }
}