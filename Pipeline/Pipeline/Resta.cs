using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
   public class Resta : Processor
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="originalFields">Lista de los nombres de las variables de entrada</param>
        /// <param name="derivedFields">Lista de los nombres de las variables de salida</param>
        public Resta(List<string> originalFields, List<string> derivedFields) : base(originalFields, derivedFields)
        {
        }
        /// <summary>
        /// Metodo que 
        /// </summary>
        /// <param name="data">Variables de entrada</param>
        /// <returns></returns>
        public override List<Tuple<string, double>> Execute(List<Tuple<string, double>> data)
        {
            State = ProcessorState.Processing;

            List<Tuple<string, double>> list = new List<Tuple<string, double>>();
            double resta = 0;
            foreach (var field in data)
            {
                resta -= field.Item2;
            }
            list.Add(new Tuple<string, double>(DerivedFields[0], resta));
            State = ProcessorState.Finished;

            return list;
        }
    }
}
