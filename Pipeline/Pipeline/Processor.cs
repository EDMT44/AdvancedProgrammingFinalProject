using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
    public abstract class Processor : IProcessor
    {
        #region Properties
        /// <summary>
        /// Estado del procesador
        /// </summary>
        public ProcessorState State { get; set; }
        /// <summary>
        /// Lista de los nombres de las variables de entrada
        /// </summary>
        public List<string> OriginalFields { get; set; }
        /// <summary>
        /// Lista de los nombres de las variables de salida
        /// </summary>
        public List<string> DerivedFields { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Metodo de ejecucion del procesador
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        virtual public List<Tuple<string,double>> Execute(List<Tuple<string,double>> data)
        {
            return data;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor base del Procesador
        /// </summary>
        /// <param name="originalFields">Lista de los nombres de las variables de entrada</param>
        /// <param name="derivedFields">Lista de los nombres de las variables de salida</param>
        public Processor( List<string> originalFields, List<string> derivedFields) {
            State = ProcessorState.Idle;
            OriginalFields = originalFields;
            DerivedFields = derivedFields;
        }
        #endregion
    }
}
