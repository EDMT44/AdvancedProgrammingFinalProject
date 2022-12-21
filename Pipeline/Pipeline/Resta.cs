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
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="originalFields">Lista de los nombres de las variables de entrada</param>
        /// <param name="derivedFields">Lista de los nombres de las variables de salida</param>
        public Resta(List<string> originalFields, List<string> derivedFields) : base(originalFields, derivedFields)
        {
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metodo que 
        /// </summary>
        /// <param name="data">Variables de entrada</param>
        /// <returns></returns>
        public override List<Tuple<string, double>> Execute(List<Tuple<string, double>> data)
        {
            State = ProcessorState.Processing;
            var temp = data;
            List<Tuple<string, double>> list = new List<Tuple<string, double>>();
            double resta = temp[0].Item2;
            temp.Remove(temp[0]);
            foreach (var field in temp)
            {
                resta -= field.Item2;
            }
            list.Add(new Tuple<string, double>(DerivedFields[0], resta));
            State = ProcessorState.Finished;

            return list;
        }
        #endregion
    }
}
