using System.Collections.Generic;

namespace PdmCode.Interface
{
    public interface IGenerator
    {
        string OutputFolder { get; set; }

        void Generate(IList<TableModel> tables);
    }
}
