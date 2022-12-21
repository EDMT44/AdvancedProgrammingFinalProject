using LINQtoCSV;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
    public class PipelineClass
    {
        #region Fields
        /// <summary>
        /// Direccion a donde se encontrara el archivo .csv
        /// </summary>
        private string path = @"C:\Users\edgar\source\repos\AdvancedProgrammingFinalProject\Data.csv";
        /// <summary>
        /// Datos a analizar y analizados distribuidos en una lista de Tuplas, donde el item1 es el 
        /// nombre de la variable y el item2 es su valor
        /// </summary>
        private List<Tuple<string,double>> _context;
        /// <summary>
        /// Lista con todos los procesadores del pipeline
        /// </summary>
        private List<Processor> _processors;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public PipelineClass()
        {
            _processors = new List<Processor>();
            _context = new List<Tuple<string,double>>();
        }
        #endregion

        #region Methods
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
        public List<Tuple<string, double>> Execute(List<Tuple<string, double>> data)
        {
            //Escribo los datos en el archivo .csv
            WriteToCsvFile(data);
            //Guardo las variables diponibles hasta el momento
            var originalFields = File.ReadLines(path).Select(x => x.Split(',')[0]).ToList();
            originalFields.Remove("");//Para no coger el 1er campo que aparece vacio debido al AppendLine()
            //Selecciono los procesadores uqe se puedenejecutar con las variables disponibles
            var processorsToExecute = _processors.Where(x => x.OriginalFields.All(
                y => originalFields.Contains(y) && x.State == ProcessorState.Idle)).ToList();
            // While mientras que existan procesadores que puedan ejecutarse
            while (processorsToExecute.Any())
            {
                foreach (var processor in processorsToExecute)
                {
                    //Seleccionar las variables que el procesador puede procesar
                    List<Tuple<string, double>> tuples = new List<Tuple<string, double>>();
                    tuples = ReadFromCsvFile(processor.OriginalFields);
                    //Verificar que se hayan recogido la cantidad de variables que el processador necesita 
                    if (tuples.Count == processor.OriginalFields.Count)
                    {
                        //Ejecutar el procesador
                        var temp = processor.Execute(tuples);
                        //Actualizar el contexto
                        WriteToCsvFile(temp);
                    }
                }
                //Actualizar las variables disponibles
                originalFields = File.ReadLines(path).Select(x => x.Split(',')[0]).ToList();
                originalFields.Remove("");
                //Actualizar los procesadores que se pueden ejecutar
                processorsToExecute = _processors.Where(x => x.OriginalFields.All(
                    y => originalFields.Contains(y) && x.State == ProcessorState.Idle)).ToList();
            }
            return ReadFromCsvFile(originalFields);
        }
        #region Helpers
        /// <summary>
        /// Metodo Para leer del .csv file
        /// </summary>
        /// <param name="originFields">variables que queremos leer</param>
        /// <returns></returns>
        private List<Tuple<string, double>> ReadFromCsvFile(List<string> originFields)
        {
            List<Tuple<string, double>> fields = new List<Tuple<string, double>>();

            foreach (var field in originFields)
            {
                var temp = File.ReadLines(path).FirstOrDefault(x => x.StartsWith(field));
                if (temp != null)
                {
                    var tuple = new Tuple<string, double>(field, double.Parse(temp.Split(',')[1]));
                    fields.Add(tuple);
                }
            }
            return fields;
        }
        /// <summary>
        /// Metodo para escribir en el archivo .csv
        /// </summary>
        /// <param name="data">Los datos que queremos escribir</param>
        private void WriteToCsvFile(List<Tuple<string, double>> data)
        {
            List<string> list = new List<string>();
            foreach (var tuple in data)
            {
                string temp = tuple.Item1 + ',' + tuple.Item2.ToString();
                list.Add(temp);
            }

            File.AppendAllLines(path, list);
        }
        #endregion
        #endregion

    }
}
