using IsbutikDataClasses;
using IsbutikFuncLayer;
using System;
using System.ComponentModel;
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

namespace Isbutik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public IsButikFunc IsButikFunc { get; set; } = new IsButikFunc();


        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private Vare _ValgteVare;
        public Vare ValgteVare
        {
            get
            {
                return _ValgteVare;
            }
            set
            {
                _ValgteVare = value;
                RaisePropertyChanged(nameof(ValgteVare));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            NulstilKundedialog();
            //TotalPrisInt.Text = $"{IsButikFunc.TotalPris}";
            tbkBemærkninger.Text = "claus 1";

            VareKnapper();
        }


        public void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VareKnapper();
        }

        private void Vælg_is_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Vælg_is.SelectedIndex >= 0)
            {
                ValgteVare = Vælg_is.SelectedItem as Vare;
            }
        }

        public void btnTilføj_Click(object sender, RoutedEventArgs e)
        {
            // Den is du har bestilt er (Vælg_is.SelectedItem as Vare) 
            // Nu skal vi finde ud af om du i forvejen har en bestilling med den samme is (Vare) 
            try
            {
                // Kig i listen
                int Antal = int.Parse(tbxAntal.Text);
                IsButikFunc.OpretEllerOpdaterBestilling(Vælg_is.SelectedItem as Vare, Antal, $"{tbkBemærkninger.Text}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fejl i bestilling");
            }

            NulstilKundedialog();
            //TotalPrisInt.Text = $"{IsButikFunc.TotalPris}";
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
                //RaisePropertyChanged(nameof(IsButikFunc.TotalPris));
                //TotalPrisInt.Text = $"{IsButikFunc.TotalPris}";
                //dgbBestilling.Items.Refresh();
            }
        }

        public void btnFjern_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((dgbBestillingInfomation.SelectedItem) != null)
                {
                    MessageBoxResult answer = MessageBox.Show("Er du sikker",
                        $"Slet varen {(dgbBestillingInfomation.SelectedItem as Vare).Navn}",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (answer == MessageBoxResult.Yes)
                    {
                        IsButikFunc.SletVare(dgbBestillingInfomation.SelectedItem as Vare);
                        //dgbBestilling.Items.Refresh();
                    }

                    NulstilVareFelter();
                    NulstilKundedialog();
                }
                else
                {
                    throw new Exception("Der er ikke valgt en vare");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fejl i sletning af vare");
            }
            finally
            {
                //dgbBestillingInfomation.Items.Refresh();
            }
        }

        public void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbkIsbodenBeskrivelse.Text != "" && decimal.Parse(tbkPriseCost.Text) > 0 && tbkIsNavn.Text != "")
                {
                    IsButikFunc.OpretVare(tbkIsNavn.Text, tbkIsbodenBeskrivelse.Text, decimal.Parse(tbkPriseCost.Text), decimal.Parse(tbkIndkøbspris.Text));

                    NulstilVareFelter();
                    NulstilKundedialog();
                }
                else
                {
                    throw new Exception("Fejl af tilføjing af vare");
                }
            }
            catch (Exception ex)
            {
                CatchMessageBox_btnAdd_Click(ex);
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            Vare vare = (dgbBestillingInfomation.SelectedItem as Vare);
            IsButikFunc.OpdaterVare(vare, tbkBemærkninger.Text, tbkIsbodenBeskrivelse.Text, decimal.Parse(tbkPriseCost.Text), int.Parse(tbkIndkøbspris.Text));

            NulstilVareFelter();
            NulstilKundedialog();
        }

        private void VareKnapper()
        {
            Vare vare = (dgbBestillingInfomation.SelectedItem as Vare);

            if (vare != null)
            {
                btnChange.Visibility = Visibility.Visible;
                btnAdd.Visibility = Visibility.Hidden;
                tbkIsbodenBeskrivelse.Text = vare.Beskrivelse.ToString();
                tbkIsNavn.Text = vare.Navn.ToString();
                tbkPriseCost.Text = vare.Price.ToString();
                tbkIndkøbspris.Text = vare.Indkøbspris.ToString();
            }
            else
            {
                btnChange.Visibility = Visibility.Hidden;
                btnAdd.Visibility = Visibility.Visible;
            }

        }

        public void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NulstilVareFelter();
        }

        public void tbxAntal_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void tbxAntal_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        public void NulstilVareFelter()
        {
            tbkIsbodenBeskrivelse.Text = "";
            tbkPriseCost.Text = "";
            tbkIsNavn.Text = "";
            tbkIndkøbspris.Text = "";
            dgbBestillingInfomation.SelectedItem = null;
            //dgbBestillingInfomation.Items.Refresh();
            //dgbBestillingInfomation.Items.Refresh();
        }

        public void NulstilKundedialog()
        {
            FjernIs.IsChecked = false;
            //tbkPrice.Text = "0";
            //tbkIsboden.Text = "";
            tbxAntal.Text = "";
            tbkBemærkninger.Text = "";
            Vælg_is.SelectedItem = null;
            //Vælg_is.Items.Refresh();
            //dgbBestilling.Items.Refresh();
        }

        public void CatchMessageBox_btnAdd_Click(Exception ex)
        {
            bool BoolKendtFejl = false;
            if (tbkIsbodenBeskrivelse.Text == "")
            {
                BoolKendtFejl = true;
                string message = "Fejl i: Beskrivelse";
                message = $"{message}" + " - der finde's ikke txt i Beskrivelse'n";
                MessageBox.Show($"{message}.", ex.Message);
            }
            if (decimal.Parse(tbkPriseCost.Text) <= 0)
            {
                BoolKendtFejl = true;
                string message = "Fejl i: Sælg af is";
                message = $"{message}" + " - Is cost skal være mist 1 i box'en";
                MessageBox.Show($"{message}.", ex.Message);
            }
            if (tbkIsNavn.Text == "")
            {
                BoolKendtFejl = true;
                string message = "Fejl i: Navngivning af Is";
                message = $"{message}" + " - Is'en skal have et navn";
                MessageBox.Show($"{message}.", ex.Message);
            }
            if (decimal.Parse(tbkIndkøbspris.Text) <= 0)
            {
                BoolKendtFejl = true;
                string message = "Fejl i: Indkøbspris";
                message = $"{message}" + " - der skal være en Indkøbspris til is'en";
                MessageBox.Show($"{message}.", ex.Message);
            }
            if (!BoolKendtFejl)
            {
                string message = "ukendt fejl";
                MessageBox.Show($"{message}.", ex.Message);
            }
        }
    }
}