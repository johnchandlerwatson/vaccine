using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vaccine.core
{
    public class ComparisonResults
    {
        public ComparisonResults(int matchingCodons, int totalCodons, Dictionary<string, int> nonMatchingCodon, Rna candidate, Rna vaccine, Rna virus)
        {
            MatchingCodons = matchingCodons;
            TotalCodons = totalCodons;
            NonmatchingCodons = nonMatchingCodon;
            Candidate = candidate;
            Vaccine = vaccine;
            Virus = virus;
        }
        public int MatchingCodons { get; }
        public int TotalCodons { get; }
        public decimal PercentMatching => Math.Round(((decimal) MatchingCodons) / ((decimal) TotalCodons) * 100);
        public Dictionary<string, int> NonmatchingCodons { get; }
        public Rna Candidate { get; }
        public Rna Vaccine { get; }
        public Rna Virus { get; }
        public KeyValuePair<string, int> MostIncorrentCandidateCodon => NonmatchingCodons.OrderByDescending(x => x.Value).First();

        public override string ToString()
        {
            var summary = new StringBuilder();
            summary.AppendLine();
            summary.AppendLine($"Candidate was {PercentMatching}% match with {MatchingCodons} matching out of {TotalCodons}.");
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