using IsbutikDataClasses;
using IsbutikDataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IsbutikFuncLayer
{
    public class IsButikFunc : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        IsButikData IsButikData { get; set; } = new IsButikData();

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public ObservableCollection<Bestilling> BestillingsListe
        {
            get
            {
                return IsButikData.BestillingsListe;
            }
        }

        public List<Vare> Vareoversigt
        {
            get
            {
                return IsButikData.Vareoversigt;
            }
        }

        public decimal TotalPris
        {
            get
            {
                decimal totalSum = 0;
                for (int i = 0; i < BestillingsListe.Count; i++)
                {
                    totalSum = totalSum + BestillingsListe[i].PrisMedMoms;
                }
                return totalSum;
            }
        }


        public void OpretEllerOpdaterBestilling(Vare vare, int antal, string bem)
        {
            bool IsAlreadyInBestillingsListe = false;
            foreach (Bestilling b in BestillingsListe)
            {
                if (b.Vare == vare)
                {
                    b.Antal = b.Antal + antal;
                    IsAlreadyInBestillingsListe = true;
                }
            }
            if (!IsAlreadyInBestillingsListe)
            {
                //string tbkBemærkninger2 = $"{tbkBemærkninger.Text}";
                Bestilling bestilling = new Bestilling(vare, antal, bem);
                BestillingsListe.Add(bestilling);
            }
            RaisePropertyChanged(nameof(TotalPris));
        }

        public void SletEllerOpdaterBestilling(Bestilling bestilling, int antal)
        {
            if (antal < 0)
            {
                throw new Exception("Minus er ugyldig");
            }
            if (antal > bestilling.Antal)
            {
                throw new Exception($"du kan højst slette {bestilling.Antal}");
            }

            if (antal == bestilling.Antal)
            {
                SletBestiiling(bestilling);
            }
            else if (antal < bestilling.Antal)
            {
                SletBestillingAntal(bestilling, antal);
            }

        }

        void SletBestillingAntal(Bestilling bestilling, int antal)
        {
            bestilling.Antal = bestilling.Antal - antal;
        }

        void SletBestiiling(Bestilling bestilling)
        {
            IsButikData.SletBestilling(bestilling);
        }

        public void OpretVare(string navn, string beskrivelse, decimal Pris, decimal Indkøbspris)
        {
            IsButikData.OpretVare(navn, beskrivelse, Pris, Indkøbspris);
        }

        public void OpdaterVare(Vare vare, string navn, string beskrivelse, decimal Pris, decimal Indkøbspris)
        {
            IsButikData.OpdaterVare(vare, navn, beskrivelse, Pris, Indkøbspris);
        }

        public void SletVare(Vare vare)
        {
            IsButikData.SletVare(vare);
        }
    }
}
