using System;
using System.Linq;
using NUnit.Framework;
using vaccine.core;

namespace vaccine.tests
{
    [TestFixture]
    public class PatternInspectorSpecs
    {
        [Test]
        public void ending_in_tt()
        {
            RunPattern(new InspectPattern("ending in TT", x => x.Nucleotides.EndsWith("TT") && x.Nucleotides != "TTT"));
            // Distinct vaccine alternatives for a virus codon ending in TT:
            // GTG: 32
            // ATC: 29
            // CTG: 28
            // GTC: 4
            // ATT: 4
            // CCT: 0
        }

        [Test]
        public void equals_GTT()
        {
            RunPattern(new EqualsPattern("GTT"));
            // Distinct vaccine alternatives for a virus codon that equals GTT:
            // GTG: 85
            // GTC: 12
            // CCT: 2
        }

        [Test]
        public void equals_GTC()
        {
            RunPattern(new EqualsPattern("GTC"));
            // Distinct vaccine alternatives for a virus codon that equals GTC:
            // GTG: 90
            // GTT: 4
            // GTC: 4
        }

        [Test]
        public void equals_AGG()
        {
            RunPattern(new EqualsPattern("AGG"));
            // Distinct vaccine alternatives for a virus codon that equals AGG:
            // CGG: 60
            // AGA: 30
            // CGC: 10
        }

        [Test]
        public void equals_TTA()
        {
            RunPattern(new EqualsPattern("TTA"));
            // Distinct vaccine alternatives for a virus codon that equals TTA:
            // CTG: 96
            // CTC: 3
        }

        [Test]
        public void equals_TTG()
        {
            RunPattern(new EqualsPattern("TTG"));
            // Distinct vaccine alternatives for a virus codon that equals TTG:
            // CTG: 90
            // CTC: 10
        }

        [Test]
        public void equals_CTT()
        {
            RunPattern(new EqualsPattern("CTT"));
            // Distinct vaccine alternatives for a virus codon that equals CTT:
            // CTG: 100
        }

        [Test]
        public void equals_ACG()
        {
            RunPattern(new EqualsPattern("ACG"));
            // Distinct vaccine alternatives for a virus codon that equals ACG:
            // ACC: 100
        }

        [Test]
        public void equals_ACA()
        {
            RunPattern(new EqualsPattern("ACA"));
            // Distinct vaccine alternatives for a virus codon that equals ACA:
            // ACC: 67
            // ACA: 32
        }

        private void RunPattern(InspectPattern pattern)
        {
            var ttCodons = Protein.VirusProtein.Codons.Where(pattern.WhereClause.Compile());
            var vaccineTtCodons = Protein.VaccineProtein.Codons.Where(x => ttCodons.Select(x => x.Index).Contains(x.Index));
            var groups = vaccineTtCodons.GroupBy(x => x.Nucleotides).Select(x => new {x.Key, Value = x.Count() * 100 / vaccineTtCodons.Count()}).ToList();
            var orderedOutput = groups.OrderByDescending(x => x.Value).Select(x => $"{x.Key}: {x.Value}").ToList();
            Console.WriteLine($"Distinct vaccine alternatives for a virus codon {pattern.Name}:{Environment.NewLine} {string.Join(Environment.NewLine, orderedOutput)}");
        }
    }
}