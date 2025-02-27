using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsbutikDataClasses
{
    public class Vare
    {
        public Vare(decimal indkøbspris) 
        {
            Indkøbspris = indkøbspris;
        }

        public Vare(string navn, decimal price, string beskrivelse, decimal indkøbspris)
        {
            Navn = navn;
            Price = price;
            Beskrivelse = beskrivelse;
            Indkøbspris = indkøbspris;
        }

        public string Navn { get; set; } 

        public decimal Price { get; set; } 

        public string Beskrivelse { get; set; }

        public decimal Indkøbspris { get; set; }
        
        public decimal FortjenestePrIs 
        {
            get
            {
                return Price - Indkøbspris; 
            } 
        } 
    } 
}
