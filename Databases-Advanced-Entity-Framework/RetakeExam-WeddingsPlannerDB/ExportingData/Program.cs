using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using WeddingsPlanner.Data;

namespace ExportingData
{
    public class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork unit = new UnitOfWork();
            ExportingJson(unit);
            //ExportingXml(unit);
        }

        private static void ExportingXml(UnitOfWork unit)
        {
            throw new NotImplementedException();
        }

        private static void ExportingJson(UnitOfWork unit)
        {
            OrderedAgencies(unit);
        }

        private static void OrderedAgencies(UnitOfWork unit)
        {
            var agencies = unit.Agencies
                .GetAll()
                .Select(a => new
                {
                    Name = a.Name,
                    EmployeesCount = a.EmployeesCount,
                    Town = a.Town
                })
                .OrderByDescending(a => a.EmployeesCount)
                .ThenBy(a => a.Name);

            var agenciesAsJson = JsonConvert.SerializeObject(agencies, Formatting.Indented);
            File.WriteAllText("../../../exportedData/agencies-ordered.json", agenciesAsJson);
        }
    }
}
