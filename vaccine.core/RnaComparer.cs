using System.Collections.Generic;
using System.Linq;

namespace vaccine.core
{
    public static class RnaComparer
    {
        public static ComparisonResults Compare(Rna candidate, Rna vaccine, Rna virus)
        {
            var codonEqualCount = 0;
            var codonIndex = 0;
            var incorrect = new Dictionary<string, int>();
            foreach (var candidateCodon in candidate.Codons)
            {
                var vaccineCodon = vaccine.Codons[codonIndex].Nucleotides;
                if (candidateCodon.Nucleotides == vaccineCodon)
                {
                    codonEqualCount++;
                }
                else
                {
                    if (incorrect.ContainsKey(candidateCodon.Nucleotides))
                    {
                        incorrect[candidateCodon.Nucleotides] += 1;
                    }
                    else 
                    {
                        incorrect.Add(candidateCodon.Nucleotides, 1);
                    }
                }
                codonIndex++;
            }
            var candidateNucleotides = candidate.Codons.SelectMany(x => x.Nucleotides).ToList(); 
            var nucleotideEqualCount = GetCorrectNucleotides(candidate, vaccine, candidateNucleotides);
            return new ComparisonResults(codonEqualCount, candidate.Codons.Count, nucleotideEqualCount, candidateNucleotides.Count(), incorrect, candidate, vaccine, virus);
        }

        private static int GetCorrectNucleotides(Rna candidate, Rna vaccine, List<char> candidateNucleotides)
        {
            var vaccineNucleotides = vaccine.Codons.SelectMany(x => x.Nucleotides).ToList();
            var nucleotideIndex = 0;
            var nucleotideEqualCount = 0;
            foreach (var nucleotide in candidateNucleotides)
            {
                if (nucleotide == vaccineNucleotides[nucleotideIndex])
                {
                    nucleotideEqualCount++;
                }
                nucleotideIndex++;
            }
            return nucleotideEqualCount;
        }
    }
}