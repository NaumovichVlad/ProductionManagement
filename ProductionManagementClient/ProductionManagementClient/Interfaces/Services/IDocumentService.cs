using Aspose.Words;

namespace ProductionManagementClient.Interfaces.Services
{
    public interface IDocumentService
    {
        Font Font { get; }
        ParagraphAlignment Aligment { set; }

        void WriteLine(string line);

        void Write(string text);

        void StartTable();

        void SkipLines(int count);

        void InsertCell(string cellContent);

        void InsertLastCellInRow(string cellContent);

        void EndTable();

        void Save(string path);

        void Show(string path);
    }
}
