using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
    public class PipelineClass
    {
        /// <summary>
        /// Datos a analizar y analizados distribuidos en una lista de Tuplas, donde el item1 es el nombre de la variable y el item2 es su valor
        /// </summary>
        private List<Tuple<string,double>> _context;
        /// <summary>
        /// Lista con todos los procesadores del pipeline
        /// </summary>
        private List<Processor> _processors;

        public PipelineClass()
        {
            _processors = new List<Processor>();
            _context = new List<Tuple<string,double>>();
        }
        /// <summary>
        /// Metodo para añadir un procesador al Pipeline
        /// </summary>
        /// <param name="processor"></param>
        public PipelineClass AddProcessor(Processor processor)
        {
            _processors.Add(processor);
            return this;
        }
        /// <summary>
        /// Metodo para limpiar el context
        /// </summary>
        public void ClearContext()
        {
            _context.Clear();
        }
        /// <summary>
        /// Metodo que va ejecutando los procesadores a medida que procesadores padres terminen su ejecucion
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
       public  List<Tuple<string, double>> Execute(List<Tuple<string, double>> data) {
            _context = new List<Tuple<string, double>>(data);
            //Guardo las variables diponibles hasta el momento
            var originalFields = _context.Select(x => x.Item1).ToList();
            //Selecciono los procesadores uqe se puedenejecutar con las variables disponibles
            var processorsToExecute = _processors.Where(x => x.OriginalFields.All(y => originalFields.Contains(y) && x.State == ProcessorState.Idle)).ToList();
            // While mientras que existan procesadores que puedan ejecutarse
            while(processorsToExecute.Any())
            {
                foreach (var processor in processorsToExecute)
                { 
                    //Seleccionar las variables que el procesador puede procesar
                    List<Tuple<string,double>> tuples = new List<Tuple<string,double>>();
                    foreach (var tuple in _context)
                    {
                        if (processor.OriginalFields.Contains(tuple.Item1)){
                            tuples.Add(tuple);
                        }
                    }
                    //Verificar que se hayan recogido la cantidad de variables que el processador necesita 
                    if (tuples.Count == processor.OriginalFields.Count)
                    {
                        //Ejecutar el procesador
                        var temp = processor.Execute(tuples);
                        //Actualizar el contexto
                        _context.AddRange(temp);
                    }
                }
                //Actualizar las variables disponibles
                originalFields = _context.Select(x => x.Item1).ToList();
                //Actualizar los procesadores que se pueden ejecutar
                processorsToExecute = _processors.Where(x => x.OriginalFields.All(y => originalFields.Contains(y)&&x.State == ProcessorState.Idle)).ToList();
            }
            return _context;
        }
    }
}
