namespace EspacioInforme;

public class Informe{
    private List<int> idsCadetes;
    private List<string> nombreCadetes;
    private List<int> cantidadPedidosEntregadosCadete;
    private List<float> jornalACobrarCadete;
    private double nominaTotal;

    public List<int> IdsCadetes { get => idsCadetes; set => idsCadetes = value; }
    public List<string> NombreCadetes { get => nombreCadetes; set => nombreCadetes = value; }
    public List<int> CantidadPedidosEntregadosCadete { get => cantidadPedidosEntregadosCadete; set => cantidadPedidosEntregadosCadete = value; }
    public List<float> JornalACobrarCadete { get => jornalACobrarCadete; set => jornalACobrarCadete = value; }
    public double NominaTotal { get => nominaTotal; set => nominaTotal = value; }

    public Informe(List<int> idsCadetes, List<string> nombreCadetes, List<int> cantidadPedidosEntregadosCadete, List<float> jornalACobrarCadete, double nominaTotal)
    {
        this.idsCadetes = idsCadetes;
        this.nombreCadetes = nombreCadetes;
        this.cantidadPedidosEntregadosCadete = cantidadPedidosEntregadosCadete;
        this.jornalACobrarCadete = jornalACobrarCadete;
        this.nominaTotal = nominaTotal;
    }
}