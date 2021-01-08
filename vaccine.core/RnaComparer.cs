using System.Collections.Generic;

namespace vaccine.core
{
    public static class RnaComparer
    {
        public static ComparisonResults Compare(Rna candidate, Rna vaccine, Rna virus)
        {
            var equalCount = 0;
            var codonIndex = 0;
            var incorrect = new Dictionary<string, int>();
            foreach (var candidateCodon in candidate.Codons)
            {
                var vaccineCodon = vaccine.Codons[codonIndex].Nucleotides;
                if (candidateCodon.Nucleotides == vaccineCodon)
                {
                    equalCount++;
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
            return new ComparisonResults(equalCount, candidate.Codons.Count, incorrect, candidate, vaccine, virus);
        }
    }
}