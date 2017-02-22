using System;
using System.Collections.Generic;

namespace PdmCode.Interface
{
    public class TableModel
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Comment { get; set; }

        public string Modifier { get; set; }

        public DateTime ModificationDate { get; set; }

        public string Creator { get; set; }

        public DateTime CreationDate { get; set; }

        public string ObjectID { get; set; }

        public IList<ColumnModel> Columns { get; set; }
    }
}
