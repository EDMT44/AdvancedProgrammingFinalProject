using Pipeline;
using System.Data;
using System.Diagnostics;

namespace ConsoleApplication
{
    public class Program
    {
        static void Main()
        {
            var Data = new List<Tuple<string, double>>();
            Data.Add(new Tuple<string, double>("numero1", 18));
            Data.Add(new Tuple<string, double>("numero2", 27));
            Data.Add(new Tuple<string, double>("numero3", 100));
            
            Suma suma1 = new Suma(new List<string>(){"numero1","numero2","numero3" },new List<string>() { "suma1"});
            Suma suma2 = new Suma(new List<string>() { "suma4","mult1" }, new List<string>() { "suma2" });
            Suma suma3 = new Suma(new List<string>() { "mult1", "suma1" }, new List<string>() { "suma3" });
            
            Suma suma4 = new Suma(new List<string>() { "numero1", "numero2"}, new List<string>() { "suma4" });
            Resta resta2 = new Resta(new List<string>() { "suma2", "suma3" }, new List<string>() { "resta2" });
            
            Multiplicador mult1 = new Multiplicador(new List<string>() { "numero1", "numero2", "numero3" }, new List<string>() { "mult1" });

            PipelineClass pipeline = new PipelineClass ();
            pipeline.AddProcessor(suma1)
                    .AddProcessor(suma2)
                    .AddProcessor(suma3)
                    .AddProcessor(suma4)
                    .AddProcessor(resta2)
                    .AddProcessor(mult1);
            List<Tuple<string, double>> result = pipeline.Execute(Data);
            foreach (Tuple<string, double> item in result)
            {
                Console.WriteLine(item.Item1+"   "+item.Item2.ToString());
            }
            //Console.WriteLine(new Tuple<string, float>("numero 1", 18).ToList())
        }
      
    }
}