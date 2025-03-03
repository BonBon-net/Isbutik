//using IsbutikDataClasses;
using IsbutikDataClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace IsbutikDataLayer
{
    public class IsButikData
    {
        public ObservableCollection<Bestilling> BestillingsListe { get; set; } = new ObservableCollection<Bestilling>();
        public ObservableCollection<Vare> Vareoversigt { get; set; } = new ObservableCollection<Vare>();

        public IsButikData()
        {
            IntializeVareOversigt();
        }

        public void IntializeVareOversigt()
        {
            Vare vare;

            Vareoversigt.Clear();

            int MagnumKøbsPris = 6;
            vare = OpretVare("Magnum", "En dejlig flødeis med mørk chokoladeovertræk", MagnumKøbsPris * 1.25m, MagnumKøbsPris);
            Bestilling bestilling = new Bestilling(vare, 12, vare.Navn);
            BestillingsListe.Add(bestilling);

            int KungFuKøbsPris = 4;
            vare = OpretVare("Kung Fu", "Kung Fu Is", KungFuKøbsPris * 1.25m, KungFuKøbsPris);
            bestilling = new Bestilling(vare, 2, vare.Navn);
            BestillingsListe.Add(bestilling);

            int LillebrorKøbsPris = 2;
            vare = OpretVare("Lillebror", "Lillebror Is", LillebrorKøbsPris * 1.25m, LillebrorKøbsPris);
            bestilling = new Bestilling(vare, 5, vare.Navn);
            BestillingsListe.Add(bestilling);

            int AstronautKøbsPris = 5;
            vare = OpretVare("Astronaut", "Astronaut Is", AstronautKøbsPris * 1.25m, AstronautKøbsPris);
            bestilling = new Bestilling(vare, 9, vare.Navn);
            BestillingsListe.Add(bestilling);
        }

        public void SletBestilling(Bestilling bestilling)
        {
            BestillingsListe.Remove(bestilling);
        }

        public Vare OpretVare(string navn, string beskrivelse, decimal Pris, decimal Indkøbspris)
        {
            Vare vare = new Vare(navn, Pris, beskrivelse, Indkøbspris);
            Vareoversigt.Add(vare);
            return vare;
        }

        public void OpdaterVare(Vare vare, string navn, string beskrivelse, decimal Pris, decimal Indkøbspris)
        {
            vare.Navn = navn;
            vare.Beskrivelse = beskrivelse;
            vare.Price = Pris;
            vare.Indkøbspris = Indkøbspris;
        }

        public void SletVare(Vare vare)
        {
            Vareoversigt.Remove(vare);
        }

    }
}