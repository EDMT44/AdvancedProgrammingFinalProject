using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
    /// <summary>
    /// Estados del procesador
    /// </summary>
    public enum ProcessorState { Idle, Processing, Finished, Error }
    public interface IProcessor
    {
        /// <summary>
        /// Estado en el que esta el procesadr
        /// </summary>
        ProcessorState State  { get; set; }
        /// <summary>
        /// Identificador del procesador
        /// </summary>
        //int Id { get; set; }
        /// <summary>
        /// Metodo para ejecutar el procesamiento de datos del core
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        List<Tuple<string,double>> Execute(List<Tuple<string,double>> data);
        /// <summary>
        /// Nombres de variables de entrada
        /// </summary>
        List<string> OriginalFields { get; set; }
        /// <summary>
        /// nombre de variables de salida
        /// </summary>
        List<string> DerivedFields { get; set; }

    }
}
