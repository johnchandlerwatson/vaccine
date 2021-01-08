using System.Collections.Generic;
using System.Text.Json;

namespace vaccine.core
{
    public static class Algorithm
    {
        public static Rna GetVaccineCandidate()
        {
            var vaccineCandidate = new Rna();
            foreach(var virusCodon in Protein.VirusProtein.Codons)
            {
                if (FullReplacements.ContainsKey(virusCodon.Nucleotides))
                {
                    vaccineCandidate.Codons.Add(new Codon(virusCodon.Index, FullReplacements[virusCodon.Nucleotides]));
                }
                else 
                {
                    vaccineCandidate.Codons.Add(virusCodon);
                }
            }
            return vaccineCandidate;
        }

        private static Dictionary<string, string> FullReplacements => JsonSerializer.Deserialize<Dictionary<string, string>>(ReplacementDictionary);
        private static string ReplacementDictionary => "{\"ATG\":\"ATG\",\"TTT\":\"TTC\",\"GTT\":\"GTG\",\"CTT\":\"CTG\",\"TTA\":\"CTG\",\"TTG\":\"CTG\",\"CCA\":\"CCC\",\"CTA\":\"CTG\",\"GTC\":\"GTG\",\"TCT\":\"AGC\",\"AGT\":\"AGC\",\"CAG\":\"CAG\",\"TGT\":\"TGC\",\"AAT\":\"AAC\",\"ACA\":\"ACC\",\"ACC\":\"ACC\",\"AGA\":\"AGA\",\"ACT\":\"ACC\",\"CAA\":\"CAG\",\"CCC\":\"CCC\",\"CCT\":\"CCT\",\"GCA\":\"GCC\",\"TAC\":\"TAC\",\"TTC\":\"TTC\",\"CGT\":\"AGA\",\"GGT\":\"GGC\",\"TAT\":\"TAC\",\"GAC\":\"GAC\",\"AAA\":\"AAG\",\"TCC\":\"AGC\",\"TCA\":\"AGC\",\"CAT\":\"CAC\",\"TGG\":\"TGG\",\"GCT\":\"GCC\",\"ATA\":\"ATC\",\"GGG\":\"GGA\",\"AAG\":\"AAG\",\"AGG\":\"CGG\",\"GAT\":\"GAC\",\"AAC\":\"AAC\",\"GAG\":\"GAG\",\"GGC\":\"GGC\",\"ATT\":\"ATC\",\"TCG\":\"AGC\",\"GAA\":\"GAG\",\"CAC\":\"CAC\",\"GCG\":\"GCC\",\"TGC\":\"TGC\",\"GGA\":\"GGC\",\"GTG\":\"GTG\",\"ACG\":\"ACC\",\"CTC\":\"CTG\",\"GTA\":\"GTG\",\"ATC\":\"ATC\",\"GCC\":\"GCC\",\"AGC\":\"AGC\",\"CTG\":\"CTG\",\"CGG\":\"CGG\",\"CGC\":\"CGG\",\"TAA\":\"TGA\"}"; 
    }
}