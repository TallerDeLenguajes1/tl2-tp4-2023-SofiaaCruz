using System.Text.Json;
using EmpresaDeCadeteria;
using EspacioCadete;
using EspacioPedidos;

namespace EspacioLecturaArchivos;

public abstract class LecturaArchivos
{
    public virtual List<Cadete>? ArchivoCadete(string nombreArchivo){
        return null;
    }
    public virtual List<Cadeteria>? ArchivoCadeteria(string nombreArchivo)
    {
        return null;
    }
}

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
}