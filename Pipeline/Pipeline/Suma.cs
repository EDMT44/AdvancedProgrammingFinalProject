using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
    public class Suma : Processor
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="originalFields">Lista de los nombres de las variables de entrada</param>
        /// <param name="derivedFields">Lista de los nombres de las variables de salida</param>
        public Suma(List<string> originalFields, List<string> derivedFields) : base(originalFields, derivedFields)
        {
        }
        /// <summary>
        /// Metodo que ejecuta la suma de las variables de entrada y devuelve la misma
        /// </summary>
        /// <param name="data">Lista de las variables de entrada</param>
        /// <returns></returns>
        public override List<Tuple<string, double>> Execute(List<Tuple<string, double>> data)
        {
            State = ProcessorState.Processing;
            List<Tuple<string,double>> list = new List<Tuple<string, double>>();
            double suma = 0;
            foreach (var field in data) {
                suma += field.Item2;
            }
            list.Add(new Tuple<string, double>(DerivedFields[0],suma));
            State = ProcessorState.Finished;
            return list;
        }    
    }
}
