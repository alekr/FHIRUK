using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHIRUK.Resources
{
    public class Period
    {
        public Period(DateTime start, DateTime end)
        {
            if (DateTime.Compare(start, end) >= 0)
                throw new Exception("The specified start date must be earlier than the end date.");

            this.Start = start;
            this.End = end;
        }

        public DateTime Start { get; private set; } //  <!-- ?? 0..1 Starting time with inclusive boundary -->
        public DateTime End { get; private set; }   //  <!-- ?? 0..1 End time with inclusive boundary, if not ongoing -->
    }
}
