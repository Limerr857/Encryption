using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slutprojekt_Kryptering_Georg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Skapa ett objekt från klassen Krypteringsmetoder som innehåller alla krypteringsmetoder
        Krypteringsmetoder krypt = new Krypteringsmetoder();

        /*                       Metoder                       */
        // bool kryptera bestämmer om funktionen ska kalla på avkrytperingsmetoder
        // eller krypteringsmetoder, true = krytperingsmetoder
        string Kryptera(bool kryptera)
        {
            // TODO: Se till så att inga värden hämtas inuti metoden?
            // de borde kanske hämtas utanför eller iallafall på en samlad plats

            // Dela upp beroende på om det är avkryptering eller kryptering som gäller
            if (kryptera)
            {
                // Ett meddelande ska krypteras

                // Initiera det krypterade meddelandet, ge det ett värde senare
                string krypteratMeddelande;

                // Hämta meddelandet som ska krypteras
                string meddelande = tbxMeddelandeKryptera.Text;

                // Hämta krypteringsmetoden som ska användas
                string metod = cmbxKrypteringsmetodKryptera.Text;

                switch (metod)
                {
                    case "Ceasar Rot3":
                        krypteratMeddelande = krypt.KrypteraRot(meddelande, false, 3);
                        break;
                    case "Ceasar Rot7":
                        krypteratMeddelande = krypt.KrypteraRot(meddelande, false, 7);
                        break;
                    case "Ceasar Rot13":
                        krypteratMeddelande = krypt.KrypteraRot(meddelande, false, 13);
                        break;
                    case "Ceasar Rot7 & Rot13; 5 längd":
                        krypteratMeddelande = krypt.KrypteraRotVäxlande(meddelande, false, 5, 7, 13);
                        break;
                    case "Ceasar Rot3 & Rot13; 2 längd":
                        krypteratMeddelande = krypt.KrypteraRotVäxlande(meddelande, false, 2, 3, 13);
                        break;
                    case "Ceasar Rot3 & Rot7 & Rot13; 5 längd":
                        krypteratMeddelande = krypt.KrypteraRotVäxlande(meddelande, false, 5, 3, 7, 13);
                        break;
                    case "Nyckelkryptering":
                        // Hämta nyckeln som ska användas och skicka ett error till användaren
                        // om det inte har satts till något eller är väldigt kort.
                        string nyckel = tbxNyckel.Text;
                        if (nyckel.Length < 5)
                        {
                            MessageBox.Show("Var vänlig skriv in en nyckel i inställningsmenyn som är längre än 4 tecken");
                            krypteratMeddelande = "";
                        }
                        else
                        {
                            krypteratMeddelande = krypt.Nyckelkryptering(meddelande, false, nyckel);
                        }
                        break;
                    default:
                        // Användaren har inte valt en metod
                        krypteratMeddelande = "Var vänlig välj en krypteringsmetod.";
                        break;
                }

                return krypteratMeddelande;
            }
            else
            {
                // Ett meddelande ska avkrypteras

                // Initiera det avkrypterade meddelandet, ge det ett värde senare
                string avkrypteratMeddelande;

                // Hämta meddelandet som ska avkrypteras
                string krypteratMeddelande = tbxKrypteratAvkryptera.Text;

                // Hämta krypteringsmetoden som ska användas
                string metod = cmbxKrypteringsmetodAvkryptera.Text;

                switch (metod)
                {
                    case "Ceasar Rot3":
                        avkrypteratMeddelande = krypt.KrypteraRot(krypteratMeddelande, true, 3);
                        break;
                    case "Ceasar Rot7":
                        avkrypteratMeddelande = krypt.KrypteraRot(krypteratMeddelande, true, 7);
                        break;
                    case "Ceasar Rot13":
                        avkrypteratMeddelande = krypt.KrypteraRot(krypteratMeddelande, true, 13);
                        break;
                    case "Ceasar Rot7 & Rot13; 5 längd":
                        avkrypteratMeddelande = krypt.KrypteraRotVäxlande(krypteratMeddelande, true, 5, 7, 13);
                        break;
                    case "Ceasar Rot3 & Rot13; 2 längd":
                        avkrypteratMeddelande = krypt.KrypteraRotVäxlande(krypteratMeddelande, true, 2, 3, 13);
                        break;
                    case "Ceasar Rot3 & Rot7 & Rot13; 5 längd":
                        avkrypteratMeddelande = krypt.KrypteraRotVäxlande(krypteratMeddelande, true, 5, 3, 7, 13);
                        break;
                    case "Nyckelkryptering":
                        // Hämta nyckeln som ska användas och skicka ett error till användaren
                        // om det inte har satts till något eller är väldigt kort.
                        string nyckel = tbxNyckel.Text;
                        if (nyckel.Length <= 7)
                        {
                            // Nyckeln ska inte kunna gissas alltför lätt, den måste vara ganska lång
                            MessageBox.Show("Var vänlig skriv in en nyckel under fliken 'nyckel' som är längre än 7 tecken");
                            avkrypteratMeddelande = "";
                        }
                        else
                        {
                            avkrypteratMeddelande = krypt.Nyckelkryptering(krypteratMeddelande, true, nyckel);
                        }
                        break;
                    default:
                        // Användaren har inte valt en metod
                        avkrypteratMeddelande = "Var vänlig välj en avkrypteringsmetod.";
                        break;
                }

                return avkrypteratMeddelande;
            }

        }

        private void btnKrypteraKryptera_Click(object sender, EventArgs e)
        {
            // Ta fram det krypterade meddelandet
            string krypteratMeddelande = Kryptera(true);

            // Avbryt krypteringen om det krypterade meddelandet är tomt,
            // eftersom detta innebär att krypteringen har misslyckats eller
            // har krypterat ett tomt meddelande.
            if (krypteratMeddelande == "")
            {
                return;
            }

            // Rensa tbxKrypterat för att kunna lägga in det nya krypterade meddelandet
            tbxKrypteratKryptera.Clear();

            // Lägg in det krypterade meddelandet i tbxKrypterat
            tbxKrypteratKryptera.Text = krypteratMeddelande;
        }

        private void btnAvkrypteraAvkryptera_Click(object sender, EventArgs e)
        {
            // Ta fram det avkrypterade meddelandet
            string avkrypteratMeddelande = Kryptera(false);

            // Avbryt avkrypteringen om det avkrypterade meddelandet är tomt,
            // eftersom detta innebär att avkrypteringen har misslyckats eller
            // har avkrypterat ett tomt meddelande.
            if (avkrypteratMeddelande == "")
            {
                return;
            }

            // Rensa tbxKrypterat för att kunna lägga in det nya krypterade meddelandet
            tbxMeddelandeAvkryptera.Clear();

            // Lägg in det krypterade meddelandet i tbxKrypterat
            tbxMeddelandeAvkryptera.Text = avkrypteratMeddelande;
        }
    }
}