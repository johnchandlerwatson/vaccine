using System;
using NUnit.Framework;
using vaccine.core;

namespace vaccine.tests
{
    [TestFixture]
    public class AlgorithmSpecs
    {
        [Test]
        public void candidate_should_have_same_number_of_codons()
        {
            var candidate = Algorithm.GetVaccineCandidate();
            Assert.AreEqual(candidate.Codons.Count, Protein.VirusProtein.Codons.Count);
        }

        [Test]
        public void run_comparison()
        {
            var candidate = Algorithm.GetVaccineCandidate();
            var results = RnaComparer.Compare(candidate, Protein.VaccineProtein, Protein.VirusProtein);
            Console.WriteLine(results.ToString());
        }
    }
}