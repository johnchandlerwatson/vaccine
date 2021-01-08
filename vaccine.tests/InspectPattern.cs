using System;
using System.Linq.Expressions;
using vaccine.core;

namespace vaccine.tests
{
    public class EqualsPattern : InspectPattern
    {
        public EqualsPattern(string protein) : base($"that equals {protein}", x => x.Nucleotides == protein) {}
    }

    public class InspectPattern 
    {
        public InspectPattern(string name, Expression<Func<Codon, bool>> whereClause)
        {
            Name = name;
            WhereClause = whereClause;
        }
        public string Name { get; protected set; }
        public Expression<Func<Codon, bool>> WhereClause { get; protected set; }
    }
}