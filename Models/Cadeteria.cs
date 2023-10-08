using EspacioCadete;
using EspacioInforme;
using EspacioPedidos;
using EspacioAccesoADatosCadeteria;
using EspacioAccesoADatosCadete;
using EspacioAccesoADatosPedidos;
namespace EmpresaDeCadeteria;

public class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> listadoCadetes;
    private List<Pedido> listadoPedidos;

    private static Cadeteria? instance; //evita que cualquier clase diferente a Singleton cree un objeto de tipo Singleton 
    public string Nombre { get => nombre; set => nombre = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
    public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

    public static Cadeteria GetInstance()
    {
        if(instance == null)
        {
            instance = new Cadeteria("\0","\0");
        }
        return instance;
    }
 
    //public Cadeteria(){}
    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        listadoCadetes = new List<Cadete>();
        AccesoADatosPedido archivoPedido = new AccesoADatosPedido();
        List<Pedido>? aux = archivoPedido.Obtener();
        if(aux != null)
        {
            listadoPedidos = aux;
        }else
        {
            ListadoPedidos = new List<Pedido>();
        }
    }

    public bool CargaDeDatos(string tipoDeAcceso)
    {
        bool aux = false;
        if (tipoDeAcceso == "csv")
        {
            ArchivosCsvCadeteria ArchivoCadeteria = new ArchivosCsvCadeteria();
            ArchivosCsvCade ArchivoCadete = new ArchivosCsvCade();
            Cadeteria? AuxCadeteria = ArchivoCadeteria.Obtener();
            List<Cadete>? AuxCadete = ArchivoCadete.Obtener();
            if(AuxCadete != null && AuxCadeteria != null)
            {
                instance = AuxCadeteria;
                instance.agregarCadetes(AuxCadete);
                aux = true;
            }
        }
        else 
        {
            if(tipoDeAcceso == "json")
            {
                ArchivosJsonCadeteria ArchivoCadeteria = new ArchivosJsonCadeteria();
                ArchivosJsonCadete ArchivoCadete = new ArchivosJsonCadete();
                Cadeteria? AuxCadeteria = ArchivoCadeteria.Obtener();
                List<Cadete>? AuxCadete = ArchivoCadete.Obtener();
                if(AuxCadete != null && AuxCadeteria != null)
                {
                    instance = AuxCadeteria;
                    instance.agregarCadetes(AuxCadete);
                    aux = true;
                }
            }
            
        }
        return aux;
    }
    public string NombreCadeteria()
    {
        return nombre;
    }
    public List<Pedido>? GetPedidos()
    {
        return listadoPedidos;
    }

    public List<Cadete>? GetCadetes()
    {
        return listadoCadetes;
    }

    public Informe GetInforme()
    {
        List<int> idsCadetes = listadoCadetes.Select(cad => cad.Id).ToList();
        List<string> nomCadetes = listadoCadetes.Select(cad => cad.Nombre).ToList();
        List<int> cantidadPedidosEntregadosCad = new List<int>();
        List<float> jornalAcobrar = new List<float>();
        foreach(var cad in listadoCadetes)
        {
            cantidadPedidosEntregadosCad.Add(pedidosEntregados(cad.Id));
            jornalAcobrar.Add(JornalACobrar(cad.Id));
        }
        Informe informe = new Informe(idsCadetes, nomCadetes, cantidadPedidosEntregadosCad, jornalAcobrar, NominaTotal());

        return informe;
    }
    public bool AgregarPedido(Pedido nuevoPedido)
    {
        bool aux = false;
        nuevoPedido.IdCadete = -9999;
        nuevoPedido.Numero = listadoPedidos.Count +1;
        if(nuevoPedido != null){
            listadoPedidos.Add(nuevoPedido);
            AccesoADatosPedido guardarPed = new AccesoADatosPedido();
            guardarPed.Guardar(listadoPedidos);
            aux = true;
        }
        
        return aux;
    }

    public void EliminarPedido(int idPed)
    {   
        if(listadoPedidos != null)
        {
            Pedido? PedidoAEliminar = listadoPedidos.Find(ped => ped.Numero == idPed);
            if(PedidoAEliminar != null)
            {
                listadoPedidos.Remove(PedidoAEliminar);
            }
        }
    }

    public bool CambiarEstadoPedido (int numPedido, int estado)
    {
        AccesoADatosPedido pedidoGuardar = new AccesoADatosPedido();
        Pedido? PedidoACambiarEstado = listadoPedidos.Find(ped => ped.Numero == numPedido);
        if(PedidoACambiarEstado != null)
        {
            if(estado == 1)
            {
                PedidoACambiarEstado.Estado = estadoPedido.entregado;
                if(PedidoACambiarEstado.Estado == estadoPedido.entregado)
                {
                    pedidoGuardar.Guardar(listadoPedidos);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else 
            {
                PedidoACambiarEstado.Estado = estadoPedido.cancelado;
                if(PedidoACambiarEstado.Estado == estadoPedido.cancelado)
                {
                    pedidoGuardar.Guardar(listadoPedidos);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else{
            return false;
        }
        
    }
    public bool AsignarCadeteAPedido(int idCadete, int idPedido)
    {
        AccesoADatosPedido pedidoGuardar = new AccesoADatosPedido();
        Cadete? cadeteElegido = listadoCadetes.Find(cad => cad.Id == idCadete);
        Pedido? pedidoElegido = listadoPedidos.Find(ped => ped.Numero == idPedido);
        if(cadeteElegido != null && pedidoElegido != null)
        {
            pedidoElegido.IdCadete = cadeteElegido.Id;
            if(pedidoElegido.IdCadete == cadeteElegido.Id)
            {
                pedidoGuardar.Guardar(listadoPedidos);
                return true;
            }else
            {
                return false;
            }
        }
        else{
            return false;
        }

    }

    public float JornalACobrar(int idCad)
    {   
        float total = 0;
        foreach(var ped in listadoPedidos)
        {
            if(ped.IdCadete == idCad)
            {
                if(ped.Estado == estadoPedido.entregado)
                {
                    total+=500;
                }
            }
        }
        return total;
    }
    public int pedidosEntregados(int idCad)
    {
        int total = 0;
        foreach(var ped in listadoPedidos)
        {
            if(ped.IdCadete == idCad)
            {
                if(ped.Estado == estadoPedido.entregado)
                {
                    total++;
                }
            }
        }
        return total;
    }

    public float NominaTotal()
    {
        float total = 0;
        foreach(var ped in listadoPedidos)
        {
            if(ped.Estado == estadoPedido.entregado)
            {
                total+=500;
            }
        }
        return total;
    }

    private void agregarCadetes(List<Cadete> listadoCadetes)
    {
        this.listadoCadetes = listadoCadetes;
    }

    public Pedido? buscarPedido(int id)
    {
        Pedido? pedidoEncontrado = listadoPedidos.FirstOrDefault(ped => ped.Numero == id);//FirstOrDefault retorna el primer elemento que cumpla con la condicion dada.
        return pedidoEncontrado;
    }

    public Cadete? buscarCadete(int id)
    {
        Cadete? encontrado = listadoCadetes.FirstOrDefault(cad => cad.Id == id);
        return encontrado;
    }

    public bool AgregarCadete(string nombre, string direccion, string telefono)
    {
        bool aux = false;
        ArchivosCsvCade archivoCsvCad = new ArchivosCsvCade();
        ArchivosJsonCadete archivosJsonCad = new ArchivosJsonCadete();
        int num = listadoCadetes.Count +1;
        Cadete nuevoCadete = new Cadete(num, nombre, direccion, telefono);
        if(nuevoCadete != null)
        {
            listadoCadetes.Add(nuevoCadete);
            archivoCsvCad.Guardar(listadoCadetes);
            archivosJsonCad.Guardar(listadoCadetes);
            aux = true;
        }
        return aux;
    }
}