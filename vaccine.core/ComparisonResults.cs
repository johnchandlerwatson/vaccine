using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vaccine.core
{
    public class ComparisonResults
    {
        public ComparisonResults(int matchingCodons, int totalCodons, int matchingNucleotides, int totalNucleotides, Dictionary<string, int> nonMatchingCodon, Rna candidate, Rna vaccine, Rna virus)
        {
            MatchingCodons = matchingCodons;
            TotalCodons = totalCodons;
            MatchingNucleotides = matchingNucleotides;
            TotalNucleotides = totalNucleotides;
            NonmatchingCodons = nonMatchingCodon;
            Candidate = candidate;
            Vaccine = vaccine;
            Virus = virus;
        }
        public int MatchingCodons { get; }
        public int MatchingNucleotides { get; }
        public int TotalCodons { get; }
        public int TotalNucleotides { get; }
        public decimal CodonPercentMatching => Math.Round(((decimal) MatchingCodons) / ((decimal) TotalCodons) * 100, 2);
        public decimal NucleotidePercentMatching => Math.Round(((decimal) MatchingNucleotides) / ((decimal) TotalNucleotides) * 100, 2);
        public Dictionary<string, int> NonmatchingCodons { get; }
        public Rna Candidate { get; }
        public Rna Vaccine { get; }
        public Rna Virus { get; }
        public KeyValuePair<string, int> MostIncorrentCandidateCodon => NonmatchingCodons.OrderByDescending(x => x.Value).First();

        public override string ToString()
        {
            var summary = new StringBuilder();
            summary.AppendLine();
            summary.AppendLine($"Candidate was {CodonPercentMatching}% codon match with {MatchingCodons} matching out of {TotalCodons}.");
            summary.AppendLine($"Candidate was {NucleotidePercentMatching}% nucleotide match with {MatchingNucleotides} matching out of {TotalNucleotides}.");
            summary.AppendLine($"The most incorrect candidate codon was {MostIncorrentCandidateCodon.Key} with {MostIncorrentCandidateCodon.Value} incorrect matches.");
            summary.AppendLine();
            
            summary.AppendLine("- Candidate - Vaccine - Virus");
            for (var i = 0; i < Candidate.Codons.Count; i++)
            {
                summary.AppendLine($"#{i} - {(Candidate.Codons[i].Nucleotides)} - {Vaccine.Codons[i].Nucleotides} - {Virus.Codons[i].Nucleotides}");
            }
            return summary.ToString();
        }
    }
}