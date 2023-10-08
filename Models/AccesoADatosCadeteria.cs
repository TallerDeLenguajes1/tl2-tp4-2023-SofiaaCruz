using System.Text.Json;
using EmpresaDeCadeteria;

namespace EspacioAccesoADatosCadeteria;

public abstract class AccesoADatosCadeteria
{
    public virtual Cadeteria? Obtener()
    {
        return null;
    }
}

public class ArchivosCsvCadeteria : AccesoADatosCadeteria
{
    public override Cadeteria? Obtener()
    {
        Cadeteria? nuevaCadeteria = null;
        List<Cadeteria> ListaAux = new List<Cadeteria>();
        if(File.Exists("Cadeteria.csv"))
        {
            StreamReader strCadeteria = new StreamReader("Cadeteria.csv");
            string? linea;
            while ((linea = strCadeteria.ReadLine()) != null)
            {
                string[] aux = linea.Split(",");
                Cadeteria CadeteriaAux = new Cadeteria(aux[0], aux[1]);
                ListaAux.Add(CadeteriaAux);
            }
            nuevaCadeteria = ListaAux[0];
        }
        return nuevaCadeteria;
    }
}

public class ArchivosJsonCadeteria : AccesoADatosCadeteria
{
    public override Cadeteria? Obtener()
    {
        Cadeteria? nuevaCadeteria = null;
        if(File.Exists("Cadeteria.json"))
        {
            string jsonCadeteria = File.ReadAllText("Cadeteria.json");
            List<Cadeteria>? ListaAux = JsonSerializer.Deserialize<List<Cadeteria>>(jsonCadeteria);
            if(ListaAux != null)
            {
                nuevaCadeteria = ListaAux[0];
            }
        }
        return nuevaCadeteria;
    }
}
/* 

public class ArchivosCsv : LecturaArchivos
{
    public override List<Cadete>? ArchivoCadete(string nombreArchivo)
    {
        List<Cadete> ListaCadete = new List<Cadete>();
        StreamReader cadete = new StreamReader(nombreArchivo);
        string? linea;
        int a;
        while((linea = cadete.ReadLine()) != null)
        {
            string[] aux = linea.Split(",").ToArray();
            int.TryParse(aux[0], out a);
            Cadete nuevoCadete = new Cadete(a, aux[1], aux[2], aux[3]);
            ListaCadete.Add(nuevoCadete);
        }
        return ListaCadete;
    }

    public override List<Cadeteria>? ArchivoCadeteria(string nombreArchivo)
    {
        Cadeteria nuevaCadeteria; 
        List<Cadeteria> listaCadeteria = new List<Cadeteria>();
        StreamReader Cadeteria = new StreamReader(nombreArchivo);
        string? linea;
        string[]? aux = null;
        while((linea = Cadeteria.ReadLine()) != null)
        {
            aux = linea.Split(",").ToArray();
        }
        if(aux != null)
        {
            nuevaCadeteria = new Cadeteria(aux[0], aux[1]);
            listaCadeteria.Add(nuevaCadeteria);
            return listaCadeteria;
        }
        else 
        {
            return null;
        }
    }
}

public class ArchivosJson : LecturaArchivos
{
    public override List<Cadete>? ArchivoCadete(string nombreArchivo)
    {
        if(File.Exists(nombreArchivo))
        {
            string? aux = File.ReadAllText(nombreArchivo);
            List<Cadete>? listaCadete = JsonSerializer.Deserialize<List<Cadete>>(aux);
            return listaCadete;
        }
        else
        {
            return null;
        }
    }

    public override List<Cadeteria>? ArchivoCadeteria(string nombreArchivo)
    {
        if(File.Exists(nombreArchivo))
        {
            string? aux = File.ReadAllText(nombreArchivo);
            List<Cadeteria>? ListaCadeteria = JsonSerializer.Deserialize<List<Cadeteria>>(aux);
            return ListaCadeteria;
        }
        else
        {
            return null;
        }
    }
} */