using System;
using System.Collections.Generic;
using System.IO;
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

namespace TC
{
    /// <summary>
    /// Logika interakcji dla klasy PanelTC.xaml
    /// </summary>
    public partial class PanelTC : UserControl
    {
        string sciezka = "";
        string staraSciezka = "";
        void PobDyski()
        {
            string[] drives = Directory.GetLogicalDrives();
            foreach (string s in drives)
            {
                comboBoxDysk.Items.Add(s);
            }
        }
        string[,]PobPliki(string sciezka)
        {


            string nazwa;
            string[] pliki = Directory.GetFiles(sciezka);
            string[,] nazwaSciezka = new string[pliki.Length, 2];

            for (int i = 0; i < pliki.Length; i++)
            {
                nazwa = pliki[i].Substring(pliki[i].LastIndexOfAny(new char[] { '\\', '/' }) + 1);
                nazwaSciezka[i, 0] = nazwa;
                nazwaSciezka[i, 1] = pliki[i];
            }

            return nazwaSciezka;



        }
        string[,] PobFoldery(string sciezka)
        {
            
            string nazwa;
            string[] foldery = Directory.GetDirectories(sciezka);
            string[,] nazwaSciezka= new string[foldery.Length,2];
         
            for(int i = 0; i < foldery.Length;i++)
            {
                nazwa="<D>"+foldery[i].Substring(foldery[i].LastIndexOfAny(new char[] { '\\', '/' }) + 1);
                nazwaSciezka[i, 0] =nazwa;
                nazwaSciezka[i, 1] = foldery[i];
            }

            return nazwaSciezka;
        }
       
        

        public PanelTC()
        {

            InitializeComponent();
            PobDyski();
            
        }
        

        
            
        

        private void ListBoxLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            ListBox listBox = (ListBox)sender;
            string[,] foldery = PobFoldery(sciezka);
            string[,] pliki = PobPliki(sciezka);
            if (listBox.SelectedItem != null)
            {
                if (listBox.SelectedItem.ToString() == "...")
                {
                    textBoxSciezka.Text = staraSciezka;
                }
            }
           
            for (int i = 0; i <foldery.Length/2; i++)
            {
                if (listBox.SelectedItem != null)
                {
                    if (foldery[i, 0] == listBox.SelectedItem.ToString())
                    {
                        textBoxSciezka.Text = foldery[i, 1];
                    }
                }
            }
            for (int j = 0; j < pliki.Length/2; j++)
            {
                if (listBox.SelectedItem!= null)
                {
                    if (pliki[j, 0] == listBox.SelectedItem.ToString())
                    {

                        textBoxSciezka.Text = pliki[j, 1];

                    }
                }
            }
            
        }
        void listaDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxLista.UnselectAll();
            ListBoxLista.Items.Clear();
            ListBox listBox = (ListBox)sender;
            staraSciezka=textBoxSciezka.Text.Substring(0, textBoxSciezka.Text.LastIndexOfAny(new char[] { '\\', '/' }));
            if (staraSciezka.Length < 3)
            {
                staraSciezka=staraSciezka+ "\\";
            }
                ListBoxLista.Items.Add("...");
            if (textBoxSciezka.Text.Length <= 3)
                ListBoxLista.Items.Remove("...");
            string nowasciezka = textBoxSciezka.Text;
            if (listBox.ToString() == "...")
            {
                string[,] foldery = PobFoldery(staraSciezka);
                for (int i = 0; i < foldery.Length / 2; i++)
                {
                    ListBoxLista.Items.Add(foldery[i, 0]);
                }
                string[,] pliki = PobPliki(staraSciezka);
                for (int i = 0; i < pliki.Length / 2; i++)
                {
                    ListBoxLista.Items.Add(pliki[i, 0]);
                }
            }
            else
            {
                string[,] foldery = PobFoldery(nowasciezka);
                for (int i = 0; i < foldery.Length / 2; i++)
                {
                    ListBoxLista.Items.Add(foldery[i, 0]);
                }
                string[,] pliki = PobPliki(nowasciezka);
                for (int i = 0; i < pliki.Length / 2; i++)
                {
                    ListBoxLista.Items.Add(pliki[i, 0]);
                }
            }
            
            sciezka = nowasciezka;

        }

        private void comboBoxDysk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxLista.UnselectAll();
            ListBoxLista.Items.Clear();
            ComboBox comboBox = (ComboBox)sender;
            string dysk = (string)comboBoxDysk.SelectedItem;
            sciezka = dysk;
            textBoxSciezka.Text = sciezka;
            string[,] foldery = PobFoldery(sciezka);
            for(int i = 0; i < foldery.Length/2; i++)
            {
                ListBoxLista.Items.Add(foldery[i,0]);
            }
            string[,] pliki = PobPliki(sciezka);
            for (int i = 0; i < pliki.Length / 2; i++)
            {
                ListBoxLista.Items.Add(pliki[i, 0]);
            }

        }
        

    }
}
