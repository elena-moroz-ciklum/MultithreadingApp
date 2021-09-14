using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingApp
{
    public class Program
    {
        public static readonly Guid TaxReport2019 = Guid.Parse("24d92ed1-bb15-471f-b539-0cffb4b57335");
        public static readonly Guid TaxReport2020 = Guid.Parse("19dfedc0-7ed7-4288-a4d3-248a93e04dc7");

        public static readonly ConcurrentDictionary<Guid, TaxReport> TaxReports = new(); // concurrencyLevel & capacity

        public static async Task Main(string[] args)
        {
            // Add key/value pairs from multiple threads
            await Task.WhenAll(
                Task.Run(() => GetOrAddTaxReport(TaxReport2020)),
                Task.Run(() => GetOrAddTaxReport(TaxReport2020)),
                Task.Run(() => GetOrAddTaxReport(TaxReport2020)),
                Task.Run(() => GetOrAddTaxReport(TaxReport2020)),
                Task.Run(() => GetOrAddTaxReport(TaxReport2020)),
                Task.Run(() => GetOrAddTaxReport(TaxReport2020)));

            Console.ReadLine();
        }

        private static void GetOrAddTaxReport(Guid taxReportId)
        {
            // GetOrAdd and AddOrUpdate are thread-safe but not atomic
            var report = TaxReports.GetOrAdd(taxReportId, GetTaxReportFromDataStore);
            Console.WriteLine(report.CreatedByThreadId);
        }

        private static TaxReport GetTaxReportFromDataStore(Guid taxReportId)
        {
            Console.WriteLine($"Thread '{Thread.CurrentThread.ManagedThreadId}' is executing GetTaxReportFromDataStore");

            return new TaxReport
            {
                Id = taxReportId,
                CreatedByThreadId = Thread.CurrentThread.ManagedThreadId,
                ObligationId = Guid.NewGuid(),
                TaxYearId = Guid.NewGuid(),
                TaxLines = new[]
                {
                    new TaxLine
                    {
                        Id = Guid.NewGuid(),
                        Code = "1",
                        Description = "1 CASH",
                    },
                },
            };
        }
    }
}
