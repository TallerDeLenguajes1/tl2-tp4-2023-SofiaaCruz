using EspacioCadete;
using System.Text.Json;

namespace EspacioAccesoADatosCadete;
public abstract class AccesoADatosCadete
{
    public virtual List<Cadete>? Obtener()
    {
        return null;
    }
}

public class ArchivosCsvCade : AccesoADatosCadete
{
    public override List<Cadete>? Obtener()
    {
        List<Cadete>? nuevalistaCadete = null;
        List<Cadete> ListaAux = new List<Cadete>();
        if(File.Exists("Cadetes.csv"))
        {
            StreamReader strCadeteria = new StreamReader("Cadetes.csv");
            string? linea;
            int a;
            while ((linea = strCadeteria.ReadLine()) != null)
            {
                string[] aux = linea.Split(",").ToArray();
                int.TryParse(aux[0], out  a);
                Cadete CadeteAux = new Cadete(a, aux[1], aux[2], aux[3]);
                ListaAux.Add(CadeteAux);
            }
            nuevalistaCadete = ListaAux;
        }
        return nuevalistaCadete;
    }
}

public class ArchivosJsonCadete : AccesoADatosCadete
{
    public override List<Cadete>? Obtener()
    {
        List<Cadete>? nuevaListaCadete = null;
        if(File.Exists("Cadetes.json"))
        {
            string jsonCadeteria = File.ReadAllText("Cadetes.json");
            List<Cadete>? ListaAux = JsonSerializer.Deserialize<List<Cadete>>(jsonCadeteria);
            if(ListaAux != null)
            {
                nuevaListaCadete = ListaAux;
            }
        }
        return nuevaListaCadete;
    }
}