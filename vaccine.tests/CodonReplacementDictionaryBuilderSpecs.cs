using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using NUnit.Framework;
using vaccine.core;

namespace vaccine.tests
{
    [TestFixture]
    public class CodonReplacementDictionaryBuilderSpecs
    {
        private Rna _virus = Protein.VirusProtein;
        private Rna _vaccine = Protein.VaccineProtein;

        [Test]
        public void build()
        {
            var replacmentsAnaylsis = new List<VirusCodonReplacementAnalysis>();

            var distinctVirusCodons = _virus.Codons.Select(x => x.Nucleotides).Distinct();
            foreach (var codon in distinctVirusCodons)
            {
                var codonOccuranceIndexes = _virus.Codons.Where(x => x.Nucleotides == codon).Select(x => x.Index).ToList();
                var matchingVaccineCodons = _vaccine.Codons.Where(x => codonOccuranceIndexes.Contains(x.Index));
                var percentages = matchingVaccineCodons.GroupBy(x => x.Nucleotides).Select(x => new CodonOccurances(x.Key, x.Count() * 100 / matchingVaccineCodons.Count())).ToList();
                replacmentsAnaylsis.Add(new VirusCodonReplacementAnalysis(codon, percentages));
            }
            
            var percentThreshold = 50;
            var replacementCandidates = replacmentsAnaylsis.Where(x => x.CodonOccurances.Any(y => y.Percentage >= percentThreshold)).ToList();
            var replacements = replacementCandidates.ToDictionary(x => x.VirusCodon, x => x.CodonOccurances.OrderByDescending(y => y.Percentage).First().VaccineCodon);
            var json = JsonSerializer.Serialize(replacements); 
            Console.WriteLine(json); //used in Algorithm
        }
    }

    public class VirusCodonReplacementAnalysis
    {
        public VirusCodonReplacementAnalysis(string virusCodon, List<CodonOccurances> codonOccurances)
        {
            VirusCodon = virusCodon;
            CodonOccurances = codonOccurances;
        }
        public string VirusCodon { get; }
        public List<CodonOccurances> CodonOccurances { get; }
    }

    public class CodonOccurances
    {
        public CodonOccurances(string vaccineCodon, int percentage)
        {
            VaccineCodon = vaccineCodon;
            Percentage = percentage;
        }
        public string VaccineCodon { get; }
        public int Percentage { get; }
    }
}