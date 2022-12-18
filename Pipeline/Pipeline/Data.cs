using LINQtoCSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
    [Serializable]
    public class Data
    {
        [CsvColumn(Name ="name", FieldIndex = 1)]
        public string Name { get; set; }
        [CsvColumn(Name ="value", FieldIndex = 2)]
        public string Value { get; set; }
    }
}
