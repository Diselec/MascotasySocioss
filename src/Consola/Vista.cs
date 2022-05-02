using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using static System.Console;

namespace Consola
{
    public class Vista
    {
        // String de cancelación de la entrada de datos.
        const string CANCELINPUT = "fin";
        // Helpers
        public List<T> EnumToList<T>() => new List<T>(Enum.GetValues(typeof(T)).Cast<T>());

        // ===== METODOS DE PRESNTACION =====
        public void LimpiarPantalla() => Clear();
        public void MostrarYReturn(Object obj, ConsoleColor color = ConsoleColor.White)
        {
            ForegroundColor = color;
            Write(obj.ToString() + " ");
            ForegroundColor = ConsoleColor.White;
            ReadLine();
        }
        public void Mostrar(Object obj, ConsoleColor color = ConsoleColor.White)
        {
            ForegroundColor = color;
            WriteLine(obj.ToString());
            ForegroundColor = ConsoleColor.White;
        }
        public bool Confirmar(string titulo, ConsoleColor color = ConsoleColor.DarkYellow){
            ForegroundColor = color;
            WriteLine($"{titulo} (S/N)");
            string entrada = ReadLine();
            if (entrada.Equals("S") || entrada.Equals("s")){
                return true;
            }
            return false;
        }
        public void MostrarListaEnumerada<T>(string titulo, List<T> datos)
        {
            Mostrar(titulo, ConsoleColor.Yellow);
            WriteLine();
            for (int i = 0; i < datos.Count; i++)
            {
                WriteLine($"  {i + 1,3:###}.- {datos[i].ToString()}");
            }
            WriteLine();
        }
       
        // ===== METODOS DE CAPTURA DE INFORMACION =====
        // Refactoring C# Generics, Reflexion, PatternMaching, Tuples,
        public T TryObtenerDatoDeTipo<T>(string prompt, string @default = "")
        {
            var msg = prompt.Trim() + ": ";
            if (@default != "") msg += "\b\b (" + @default + "): ";

            while (true)
            {
                Write(msg);
                var input = ReadLine();
                // c# throw new Exception: Lanzamos una Excepción para indicar que el usuario ha cancelado la entrada
                if (input.ToLower().Trim() == CANCELINPUT) throw new Exception("Entrada cancelada por el usuario");
                if (input == "") input = @default;
                try
                {
                    // c# Reflexion
                    // https://stackoverflow.com/questions/2961656/generic-tryparse?rq=1
                    var valor = TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(input);
                    return (T)valor;
                }
                catch (Exception)
                {
                    //MostrarMensaje($"Error: {input} no reconocido como: {typeof(T).ToString()}");
                    if (input != "")
                        Mostrar($"Error: '{input}' no reconocido como entrada permitida", ConsoleColor.DarkRed);
                }
            }
        }
        public int TryObtenerValorEnRangoInt(int min, int max, string prompt)
        {
            int input = int.MaxValue;
            while (input < min || input > max)
                try
                {
                    input = TryObtenerDatoDeTipo<int>(prompt);
                }
                catch (Exception)
                {
                    throw; // e;
                };
            return input;
        }
        public T TryObtenerElementoDeLista<T>(string titulo, List<T> datos, string prompt)
        {
            MostrarListaEnumerada(titulo, datos);
            try
            {
                var input = TryObtenerValorEnRangoInt(1, datos.Count, prompt);
                return datos[input - 1];
            }
            catch (Exception)// e)
            {
                throw; // e;
            };
        }
        public int[] TryObtenerArrayInt(string prompt, int size, char separador = ',')
        {
            var msg = prompt.Trim() + ": ";
            while (true)
            {
                Write(msg);
                var input = ReadLine();
                // c# throw new Exception: Lanzamos una Excepción para indicar que el usuario ha cancelado la entrada
                if (input.ToLower().Trim() == CANCELINPUT) throw new Exception("Entrada cancelada por el usuario");
                try
                {
                    var valores = input.Split(separador);
                    if (valores.Length != size) throw new Exception();
                    var ints = new int[size];
                    for (var i = 0; i < valores.Length; i++)
                        ints[i] = Int16.Parse(valores[i]);
                    return ints;
                }
                catch (Exception)
                {
                    if (input != "")
                        Mostrar($"Error: '{input}' no reconocido como entrada permitida", ConsoleColor.DarkRed);
                }
            }
        }
        public DateTime TryObtenerFecha(string prompt)
        {
            var promptF = prompt.Trim() + " (d/m/a4): ";
            while (true)
            {
                var input = TryObtenerArrayInt(promptF, 3, '/');
                try
                {
                    return new DateTime(input[2], input[1], input[0], 0, 0, 0);
                }
                catch (Exception)
                {
                    Mostrar($"Error: '{input[0]}/{input[1]}/{input[2]}' no reconocido como fecha permitida", ConsoleColor.DarkRed);
                }
            }
        }   
    }
}