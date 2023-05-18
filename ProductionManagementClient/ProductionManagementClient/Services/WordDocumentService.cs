using Aspose.Words;
using Aspose.Words.Tables;
using ProductionManagementClient.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductionManagementClient.Services
{
    public class WordDocumentService : IDocumentService
    {
        private readonly Document _document;
        private readonly DocumentBuilder _builder;
        private Table _table;

        public Font Font
        {
            get
            {
                return _builder.Font;
            }
        }

        public ParagraphAlignment Aligment
        {
            set
            {
                _builder.ParagraphFormat.Alignment = value;
            }
        }

        public WordDocumentService()
        {
            _document = new Document();
            _builder = new DocumentBuilder(_document);
        }

        public void WriteLine(string line)
        {
            _builder.Writeln(line);
            
        }

        public void Write(string text)
        {
            _builder.Write(text);
        }

        public void StartTable()
        {
            _table = _builder.StartTable();
        }

        public void InsertCell(string cellContent)
        {
            _builder.InsertCell();
            _table.AutoFit(AutoFitBehavior.AutoFitToContents);
            _builder.Write(cellContent);
        }

        public void InsertLastCellInRow(string cellContent)
        {
            _builder.InsertCell();
            _builder.Write(cellContent);
            _builder.EndRow();
        }

        public void SkipLines(int count)
        {
            for (var i = 0; i < count; i++)
            {
                _builder.Writeln();
            }
        }

        public void EndTable()
        {
            _builder.EndTable();
        }

        public void Save(string path)
        {
            _document.Save(path);
        }

        public void Show(string path)
        {
            Process.Start(path);
        }
    }
}
