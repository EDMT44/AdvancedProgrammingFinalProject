using Pipeline;
using System.Data;
using System.Diagnostics;

namespace ConsoleApplication
{
    public class Program
    {
        static void Main()
        {
            //creando datos iniciales
            var Data = new List<Tuple<string, double>>();
            Data.Add(new Tuple<string, double>("numero 1", 5));
            Data.Add(new Tuple<string, double>("numero 2", 15));
            Data.Add(new Tuple<string, double>("numero 3", 20));
            //declarando procesadores
            Suma suma = new Suma(new List<string>(){"numero 1","numero 2","numero 3" },
                new List<string>() { "suma total"});
            Multiplicador mult = new Multiplicador(new List<string>() { "numero 1", "numero 3" },
                new List<string>() { "multiplicacion" });
            Resta resta = new Resta(new List<string>() { "multiplicacion", "suma total" },
                new List<string>() { "resta" });

            // creando pipeline y agregandole procesadores
            PipelineClass pipeline = new PipelineClass ();
            pipeline.AddProcessor(suma)
                    .AddProcessor(resta)
                    .AddProcessor(mult);
            List<Tuple<string, double>> result = pipeline.Execute(Data);
            foreach (Tuple<string, double> item in result)
            {
                Console.WriteLine(item.Item1+"   "+item.Item2.ToString());
            }
        }
      
    }
}