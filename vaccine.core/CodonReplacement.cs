using System.Collections.Generic;
using System.Linq;

namespace vaccine.core
{
    public class CodonReplacement
    {
        public CodonReplacement(string vaccineCodon, params string[] virusCodons)
        {
            VaccineCodon = vaccineCodon;
            VirusCodons = virusCodons.ToList();
        }
        public string VaccineCodon { get; set; }
        public List<string> VirusCodons { get; set; } = new List<string>();
    }
}