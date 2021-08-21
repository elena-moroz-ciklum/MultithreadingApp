using System;

namespace MultithreadingApp
{
    public class TaxReport
    {
        public Guid Id { get; set; }

        public int CreatedByThreadId { get; set; } // for debug needs

        public Guid ObligationId { get; set; }

        public Guid TaxYearId { get; set; }

        public TaxLine[] TaxLines { get; set; }
    }

    public class TaxLine
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }
    }
}
