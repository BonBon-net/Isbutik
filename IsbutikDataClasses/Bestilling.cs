using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsbutikDataClasses
{
    public class Bestilling : INotifyPropertyChanged
    {
        public Bestilling(Vare vare, int antal, string bemærkninger)
        {
            if (vare == null)
            {
                throw new Exception("Manglende is i bestilling");
            }
            Vare = vare;
            Antal = antal;
            Bemærkninger = bemærkninger;
            //PriceIs = priceIs;
            //Price = priceIs * antal;
        }

        public string Bemærkninger
        {
            get;
            set;
        }

        public decimal Price
        {
            get
            {
                return Vare.Price;
            }
        }

        private int antal;
        public int Antal
        {
            get
            {
                return antal;
            }
            set
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Antal)));
                }
                antal = value;
            }
        }

        public Vare Vare
        {
            get;
            set;
        }

        public string Navn
        {
            get
            {
                return Vare.Navn;
            }
        }

        public decimal PriceBestilling
        {
            get { return Vare.Price * Antal; }
        }

        public decimal PrisMedMoms
        {
            get
            {
                return PriceBestilling + Moms;
            }
        }

        public decimal Fortjeneste
        {
            get
            {
                return Antal * Vare.FortjenestePrIs;
            }
        }

        public decimal Moms
        {
            get { return PriceBestilling * 0.25m; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
