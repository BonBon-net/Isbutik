using IsbutikDataClasses;
using IsbutikDataLayer;
using IsbutikFuncLayer;

namespace Unit_test
{
    [TestClass]
    public class UnitTest1
    {
        IsButikFunc Func = new();
        //Bestilling Bestilling = new();
        //IsButikFunc IsButikFunc { get; set; } = new IsButikFunc();

        [TestMethod]
        public void TestVareoprettelse()
        {
            Func.OpretVare("TestIs", "Dårlig is", 19.5m, 2.34m);
            foreach (Vare v in Func.Vareoversigt)
            {
                if (v.Navn == "TestIs")
                {
                    Assert.IsTrue(true);
                    return;
                }
            }
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void TestTotalPris()
        {
            //int Count = IsButikFunc.BestillingsListeCount();
            decimal totalSum = 0;
            for (int i = 0; i < Func.BestillingsListe.Count; i++)
            {
                totalSum = totalSum + Func.BestillingsListe[i].PrisMedMoms;
            }
            Assert.AreEqual(totalSum, Func.TotalPris);

            Vare vare = Func.OpretVare("TestIs", "Dårlig is", 20m, 2.34m);
            Func.OpretEllerOpdaterBestilling(vare, 1, "TEST");
            Assert.AreEqual(totalSum + 20 * 1.25m, Func.TotalPris);
        }
        [TestMethod]
        public void fortjenesten()
        {
            Vare vare = Func.OpretVare("TestIs", "Dårlig is", 20m, 2.34m); 
            Bestilling bestilling = Func.OpretEllerOpdaterBestilling(vare, 1, "TEST");

            decimal fortjenesten_reel = bestilling.Fortjeneste;
            decimal fortjenesten_forventet = bestilling.Antal * vare.FortjenestePrIs;

            Assert.AreEqual(fortjenesten_forventet, fortjenesten_reel);
        }
    }
}