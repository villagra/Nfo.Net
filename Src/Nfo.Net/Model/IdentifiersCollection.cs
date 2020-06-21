using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nfo.Net.Model
{
    public class IdentifiersCollection : List<Identifier> 
    {
        public Identifier Imdb { get => this.FirstOrDefault(p => p.Type == "imdb"); }

        public IdentifiersCollection()
        { }
    }
}
