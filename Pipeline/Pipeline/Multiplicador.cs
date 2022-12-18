using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
    public class Multiplicador : Processor
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="originalFields">Lista de los nombres de las variables de entrada</param>
        /// <param name="derivedFields">Lista de los nombres de las variables de salida</param>
        public Multiplicador(List<string> originalFields, List<string> derivedFields) : base( originalFields, derivedFields)
        {
        }
        /// <summary>
        /// Metodo que ejecuta la multiplicacion de los valores de las variables de entrada y devuelve el mismo
        /// </summary>
        /// <param name="data">Variables de entrada</param>
        /// <returns></returns>
        public override List<Tuple<string, double>> Execute(List<Tuple<string, double>> data)
        {
            State = ProcessorState.Processing;
            List<Tuple<string, double>> list = new List<Tuple<string, double>>();
            double mult = 1;
            foreach (var field in data)
            {
             mult *= field.Item2;
            }
            list.Add(new Tuple<string, double>(DerivedFields[0], mult));
            State = ProcessorState.Finished;

            return list;
        }
    }
}
