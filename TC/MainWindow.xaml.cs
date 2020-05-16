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
using Path = System.IO.Path;

namespace TC
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
        }

        static public void KopiujFolder(string folderZ, string folderDo)
        {
            try
            {
                if (!Directory.Exists(folderDo))
                    Directory.CreateDirectory(folderDo);
                string[] pliki = Directory.GetFiles(folderZ);
                foreach (string plik in pliki)
                {
                    string nazwa = Path.GetFileName(plik);
                    string cel = Path.Combine(folderDo, nazwa);
                    File.Copy(plik, cel, true);
                }
                string[] foldery = Directory.GetDirectories(folderZ);
                foreach (string folder in foldery)
                {
                    string nazwa = Path.GetFileName(folder);
                    string cel = Path.Combine(folderDo, nazwa);
                    KopiujFolder(folder, cel);
                }
            }
            catch { }
        }

        private void clickKopiuj(object sender, RoutedEventArgs e)
        {
            string sciezkaZ = PanelLewy.textBoxSciezka.Text;
            string SciezkaDo = PanelPrawy.textBoxSciezka.Text;
            string nazwa;
            nazwa= sciezkaZ.Substring(SciezkaDo.LastIndexOfAny(new char[] { '\\', '/' }) + 1);
            try
            {
                FileAttributes czyfile = File.GetAttributes(sciezkaZ);




                if (czyfile.HasFlag(FileAttributes.Directory))
                {
                    KopiujFolder(sciezkaZ, SciezkaDo + "\\" + nazwa);
                  
                }
                else
                {
                    File.Copy(sciezkaZ, SciezkaDo + "\\" + nazwa, true);
                    
                }
            }
            catch { }
            }
        }
    }

