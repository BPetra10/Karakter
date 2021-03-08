using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LootBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public class Karakter
    {
        public uint xp;
        public byte level;
        //List<string> targylista;

        public Karakter(string bevittxp)
        {
            xp = Convert.ToUInt32(bevittxp);
            SzamolSzint();
        }

        public uint mennyiXP(byte level)
        {
            return Convert.ToUInt32(((level - 1) * 1000.0) * level / 2.0);
        }

        public uint kovSzint //Tulajdonság, amely kiszámolja, hogy mennyi XP kell a következő szinthez.
        {
            get
            {
                return mennyiXP(Convert.ToByte(level + 1)) - xp;
            }
        }

        public void SzamolSzint()
        {
            level = Convert.ToByte(Math.Floor((500 + Math.Sqrt(250000 + 2000 * xp)) / 1000.0));
            //level = Convert.ToByte(Math.Floor((500 + Math.Sqrt(250000 + 2000 * xp)) / (2.0 * 500.0)));
        }

        public void AddXp(uint plusxp)
        {
            xp += plusxp; //Ponthozzáadás
            SzamolSzint();
        }

    }

    public partial class MainWindow : Window
    {
        Karakter uj;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bevitel_Click(object sender, RoutedEventArgs e)
        {
            arany.IsEnabled = true;
            ezust.IsEnabled = true;
            bronz.IsEnabled = true;
            bevitel.IsEnabled = false;
            xp.IsEnabled = false;
        }

		private void bronz_Click(object sender, RoutedEventArgs e)
		{
            Random r = new Random();
            int szazalek = r.Next(70, 101); //70-100% között
            uint kapott = Convert.ToUInt32(uj.kovSzint*szazalek/100.0);
            uj.AddXp(kapott);
            xp.Text = uj.xp.ToString();
		}

		private void xp_TextChanged(object sender, TextChangedEventArgs e)
		{
			    string experience = xp.Text; //Kiolvassuk a mezőből az XP-t
                uj = new Karakter(experience);
                level.Text = uj.level.ToString();
                kovXP.Content = uj.kovSzint + " XP";
                kov_xp_vonal.Value = 100.0 * uj.xp / uj.mennyiXP(Convert.ToByte(uj.level + 1));    
        }

		private void ezust_Click(object sender, RoutedEventArgs e)
		{
            Random r = new Random();
            int szazalek = r.Next(120, 181); //120-180% között
            uint kapott = Convert.ToUInt32(uj.kovSzint * szazalek / 100.0);
            uj.AddXp(kapott);
            xp.Text = uj.xp.ToString();
        }

		private void arany_Click(object sender, RoutedEventArgs e)
		{
            Random r = new Random();
            int szazalek = r.Next(250, 301); //250-300% között
            uint kapott = Convert.ToUInt32(uj.kovSzint * szazalek / 100.0);
            uj.AddXp(kapott);
            xp.Text = uj.xp.ToString();
            targyLista.Items.Add("Epikus tárgy: "+uj.level+" szintű.");
			if (r.Next(1,6)==1)
			{
                targyLista.Items.Add("Legendás tárgy: " + uj.level + " szintű.");
            }
        }
	}
}


