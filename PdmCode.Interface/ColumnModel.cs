using System;

namespace PdmCode.Interface
{
    public class ColumnModel
    {
        public string ObjectID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public DateTime CreationDate { get; set; }

        public string Creator { get; set; }

        public DateTime ModificationDate { get; set; }

        public string Modifier { get; set; }

        public string Comment { get; set; }

        public string DataType { get; set; }

        public int? Length { get; set; }

        public string Mandatory { get; set; }

        public bool AutoMigrated { get; set; }

        public bool Computed { get; set; }

        public bool Identity { get; set; }
    }
}
