using System.Linq;
using NUnit.Framework;
using vaccine.core;

namespace vaccine.tests
{
    [TestFixture]
    public class Tests
    {
        private Rna _virus = Protein.VirusProtein;

        [Test]
        public void correct_first_codon()
        {
            CompareCodons(_virus.Codons.First(), "ATG");
        }

        [Test]
        public void correct_last_codon()
        {
            CompareCodons(_virus.Codons.Last(), "TAA");
        }

        private void CompareCodons(Codon codon, string stringCodon)
        {
            Assert.AreEqual(stringCodon, codon.Nucleotides);
        }
    }
}