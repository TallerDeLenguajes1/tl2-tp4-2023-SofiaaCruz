namespace EspacioCliente;

public class Cliente{

    private string nombre;
    private string direccion;
    private string telefono;
    private string datosReferenciaDireccion;

    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public string DatosReferenciaDireccion { get => datosReferenciaDireccion; set => datosReferenciaDireccion = value; }

    //constructor
    public Cliente(string nombre, string direccion, string telefono, string datosReferenciaDireccion){
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.datosReferenciaDireccion = datosReferenciaDireccion;
    }

    public string? Datos(){
        return $"\nNombre: {nombre}\nDirección: {direccion}\nTeléfono: {telefono}\nDatos de referencia: {datosReferenciaDireccion}";
    }
}