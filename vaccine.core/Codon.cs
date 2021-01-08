namespace vaccine.core
{
    public class Codon
    {
        public Codon(int index, string nucleotides)
        {
            Index = index;
            Nucleotides = nucleotides;
        }
        
        public int Index { get; }
        public string Nucleotides { get; }
        public Codon OverrideLastNucleotide(char nucleotide)
        {
            return new Codon(this.Index, this.Nucleotides.Substring(0, 2) + nucleotide);
        }
    }
}