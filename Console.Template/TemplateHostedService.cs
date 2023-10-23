
using FastExcel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.Template
{
    public class TemplateHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        public TemplateHostedService(ILogger<TemplateHostedService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(StartAsync)}");
            TestKeyVault();
            TestFastExcel();
            return Task.CompletedTask;
        }

        private void TestFastExcel()
        {
            // Get the input file path
            var inputFile = new FileInfo("C:\\Temp\\result_fields.xlsx");
            var outputFile = new FileInfo("C:\\Temp\\result_fields_output.xlsx");
            var templateFile = new FileInfo("C:\\Temp\\result_fields_template.xlsx");

            //Create a worksheet
            Worksheet worksheet = null; 
            var worksheetwr = new Worksheet();
            var rows = new List<Row>();
            IDictionary<string, List<object>> dict = new Dictionary<string, List<object>>();
            if(outputFile.Exists)
            {
                outputFile.Delete();
            }
            //// Create an instance of Fast Excel
            using (FastExcel.FastExcel fastExcelWrite = new FastExcel.FastExcel(templateFile, outputFile))
            {
                using (FastExcel.FastExcel fastExcelRead = new FastExcel.FastExcel(inputFile, false))
                {
                    // Read the rows using worksheet name
                    worksheet = fastExcelRead.Read("fields");

                    foreach(var row in worksheet.Rows)
                    {
                        var list = row.Cells.ToList();
                        if (row.RowNumber != 1) {
                            //check falso positivo
                            //call API 
                            //find field
                            //if positivo e score divero da 100 -> TRUE
                            var cell = new Cell(25, "False");
                            list.Add(cell);
                        }
                        else
                        {
                            var cell = new Cell(25, "Falsi positivi");
                            list.Add(cell);
                        }
                        var rowwr = new Row(row.RowNumber, list);
                        rows.Add(rowwr);
                    }
                    worksheetwr.Rows = rows;
                    
                }
                fastExcelWrite.Write(worksheetwr, "fields");
            }
        }
        
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(StopAsync)}");
            return Task.CompletedTask;
        }

        protected void TestKeyVault()
        {
            var pwd = _config["pwd"];
        }
    }


}
