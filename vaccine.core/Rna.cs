using System.Collections.Generic;
using System.Linq;

namespace vaccine.core
{
    public class Rna
    {
        public Rna() {}
        public Rna(string sequence)
        {
            Codons = Split(sequence);    
        }

        public List<Codon> Codons { get; } = new List<Codon>();
        private List<Codon> Split(string sequence)
        {
            return Enumerable.Range(0, sequence.Length / 3)
                .Select(i => new Codon(i, sequence.Substring(i * 3, 3)))
                .ToList();
        }
    }
}
