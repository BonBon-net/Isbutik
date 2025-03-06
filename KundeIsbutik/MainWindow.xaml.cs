using IsbutikDataClasses;
using IsbutikFuncLayer;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KundeIsbutik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IsButikFunc IsButikFunc { get; set; } = new IsButikFunc();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            NulstilKundedialog();
        }

        public void btnTilføj_Click(object sender, RoutedEventArgs e)
        {
            // Den is du har bestilt er (Vælg_is.SelectedItem as Vare) 
            // Nu skal vi finde ud af om du i forvejen har en bestilling med den samme is (Vare) 
            try
            {
                int Antal = int.Parse(tbxAntal.Text);
                IsButikFunc.OpretEllerOpdaterBestilling(Vælg_is.SelectedItem as Vare, Antal, $"{tbkBemærkninger.Text}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fejl i bestilling");
                //CatchMessageBox(ex);
            }

            NulstilKundedialog();
            //TotalPrisInt.Text = $"{IsButikFunc.TotalPris}";
        }

        // ItemsSource="{Binding ElementName=textBlock, Mode=OneWay}"
        public void Vælg_is_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Vare vare = (Vælg_is.SelectedItem as Vare);

            if (vare != null)
            {
                //tbkBemærkninger.Text = $"{}";
                tbkPrice.Text = vare.Price.ToString();
                tbkIsboden.Text = vare.Beskrivelse;
            }
        }

        public void btnFjern_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FjernIs.IsChecked == true)
                {
                    int antal = int.Parse(tbxAntal.Text);

                    IsButikFunc.SletEllerOpdaterBestilling(dgbBestilling.SelectedItem as Bestilling, antal);
                }
                else
                {
                    if ((dgbBestilling.SelectedItem as Bestilling) != null)
                    {
                        IsButikFunc.SletEllerOpdaterBestilling(dgbBestilling.SelectedItem as Bestilling,
                            (dgbBestilling.SelectedItem as Bestilling).Antal);
                    }
                    else
                    {
                        throw new Exception("Der er ikke valgt en bestilling");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fejl i sletning af bestilling");
            }
            finally
            {
                //dgbBestilling.Items.Refresh();
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tbxAntal_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        void NulstilKundedialog()
        {
            FjernIs.IsChecked = false;
            tbkPrice.Text = "0";
            tbkIsboden.Text = "";
            tbxAntal.Text = "1";
            tbkBemærkninger.Text = "";
            Vælg_is.SelectedItem = null;
            //Vælg_is.Items.Refresh();
            //dgbBestilling.Items.Refresh();
        }


    }
}