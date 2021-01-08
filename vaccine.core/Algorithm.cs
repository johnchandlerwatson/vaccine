using System.Collections.Generic;
using System.Linq;

namespace vaccine.core
{
    public static class Algorithm
    {
        public static Rna GetVaccineCandidate()
        {
            var vaccineCandidate = new Rna();
            foreach(var virusCodon in Protein.VirusProtein.Codons)
            {
                var fullReplacement = FullReplacements.FirstOrDefault(x => x.VirusCodons.Contains(virusCodon.Nucleotides));
                if (fullReplacement != null)
                {
                    vaccineCandidate.Codons.Add(new Codon(virusCodon.Index, fullReplacement.VaccineCodon));
                }
                else if (virusCodon.Nucleotides.EndsWith('T')) 
                {
                    vaccineCandidate.Codons.Add(virusCodon.OverrideLastNucleotide('C'));
                } 
                else if (virusCodon.Nucleotides.EndsWith('A'))
                {
                    vaccineCandidate.Codons.Add(virusCodon.OverrideLastNucleotide('G'));
                }
                else 
                {
                    vaccineCandidate.Codons.Add(virusCodon);
                }
            }
            return vaccineCandidate;
        }

        private static List<CodonReplacement> FullReplacements => new List<CodonReplacement>
        {
            new CodonReplacement("CTG", "TTA", "TTG", "CTT"),
            new CodonReplacement("GTG", "GTT", "GTC"),
            new CodonReplacement("ACC", "ACG")
        };
    }
}