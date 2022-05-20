using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Slutprojekt_Kryptering_Georg
{
    class Krypteringsmetoder
    {
        //////////////////////////////////////////////////////
        /*                  Fält/Variabler                  */
        //////////////////////////////////////////////////////

        // Sträng med alla bokstäver i det svenska alfabetet
        string alfabet = "abcdefghijklmnopqrstuvwxyzåäö";


        //////////////////////////////////////////////////////
        /*                     Metoder                      */
        //////////////////////////////////////////////////////

        // Metoden som utför Ceasarchiffer med vilken rot som helst
        // Klarar både kryptering och avkryptering
        public string KrypteraRot(string inputMeddelande, bool avkryptera, int rot)
        {
            // Initiera outputMeddelande
            string outputMeddelande;

            // Initiera outputMeddelandeLista som kommer att konverteras till en string
            // efter den är färdig
            List<char> outputMeddelandeLista = new List<char>();

            // Initiera outputBokstavPosition
            int outputBokstavPosition;

            // Initiera inputMeddelandeBokstavPosition
            int inputMeddelandeBokstavPosition;

            // Initiera outputBokstav
            char outputBokstav;

            bool storBokstav;

            // För varje char i inputMeddelande
            foreach (char inputMeddelandeBokstav in inputMeddelande)
            {
                // Spara om bokstaven är gemen(liten) eller versal(stor)
                // Om bokstaven är stor 
                if (char.ToLower(inputMeddelandeBokstav) != inputMeddelandeBokstav)
                {
                    storBokstav = true;
                }
                // Om den är liten
                else
                {
                    storBokstav = false;
                }

                // Konvertera bokstaven till en liten bokstav om det går,
                // programmet har redan sparat bokstavens storlek
                char inputMeddelandeBokstavLiten = char.ToLower(inputMeddelandeBokstav);

                // Kryptera om inputMeddelandeBokstav är en bokstav
                if (alfabet.Contains(inputMeddelandeBokstavLiten))
                {
                    // Hittar bokstavens position i alfabetet
                    inputMeddelandeBokstavPosition = alfabet.IndexOf(inputMeddelandeBokstavLiten);

                    // Skiftar bokstavens position ett antal (bestämst av värdet på rot)
                    // steg i alfabetet för att få den (av)krypterade bokstaven
                    if (!avkryptera)
                    {
                        // Om metoden ska kryptera ska bokstaven skiftas nedåt
                        outputBokstavPosition = inputMeddelandeBokstavPosition + rot;
                    }
                    else
                    {
                        // Om metoden ska avkryptera ska bokstaven skiftas uppåt
                        outputBokstavPosition = inputMeddelandeBokstavPosition - rot;
                    }
                    // Om bokstaven har "gått runt" hela alfabetet nedåt
                    // (t.ex. om bokstaven är ö och skiftas nedåt)
                    // ((ändrade till >= istället för = för att det var det enda som funkade))
                    if (outputBokstavPosition >= alfabet.Length)
                    {
                        // Se till så att den går runt till början igen
                        outputBokstavPosition -= alfabet.Length;

                    }
                    // Samma sak fast bokstaven har "gått runt" uppåt
                    else if (outputBokstavPosition < 0)
                    {
                        outputBokstavPosition += alfabet.Length;
                    }
                    outputBokstav = alfabet[outputBokstavPosition];
                    
                    // Lägger till outputBokstav till outputMeddelandeLista
                    if (storBokstav)
                    {
                        // Om bokstaven är stor

                        // Konvertera outputbokstav till en stor bokstav
                        outputBokstav = char.ToUpper(outputBokstav);
                    }
                    // Om bokstaven är liten görs inget
                    
                    outputMeddelandeLista.Add(outputBokstav);
                }
                // Lägg annars bara till inputMeddelandeBokstav utan att kryptera den
                else
                {
                    outputMeddelandeLista.Add(inputMeddelandeBokstav);
                }
            }
            // Konvertera listan till en string
            outputMeddelande = string.Join("", outputMeddelandeLista);

            return outputMeddelande;
        }

        // Metod som utför Ceasarchiffer med först rot7 under fem bokstäver,
        // sedan rot13 under fem bokstäver och fortsätter så.
        // Kan även göra detta med andra rot, andra mängder bokstäver
        // samt med tre rötter istället för två
        public string KrypteraRotVäxlande(string inputMeddelande, bool avkryptera, int sektionslängd, int rotNr1, int rotNr2, int rotNr3 = -1)
        {
            // Initiera outputMeddelande
            string outputMeddelande;

            // Initiera outputMeddelandeLista som kommer att sammanfogas till
            // en enda lång string i slutet av programmet
            List<string> outputMeddelandeLista = new List<string>();

            // Initiera sektionslistan som fylls längre ner
            List<string> sektionLista = new List<string>();

            // Initiera sektion som läggs in i sektionLista senare
            string sektion;

            // Skapa ytterligare variabler som alltid kommer få värden senare i koden
            bool treOlikaRot;
            int antalRot;

            // Fyll sektionLista med sektioner vars längd bestäms av sektionslängd.
            // Denna lista kommer sedan skickas bit för bit till metoden KrypteraRot
            for (int i = 0; i < inputMeddelande.Length; i += sektionslängd)
            {
                // OBS! i += sektionslängd ovan

                // Återställ strängen sektion
                sektion = "";

                // Fyll den nuvarande sektionen med bokstäver från inputMeddelande
                for (int inputMeddelandeBokstavPos = i; inputMeddelandeBokstavPos < i + sektionslängd; inputMeddelandeBokstavPos++)
                {
                    // Om du har nått slutet på meddelandet
                    if (inputMeddelandeBokstavPos >= inputMeddelande.Length)
                    {
                        break;
                    }

                    // Fortsätt annars som vanligt
                    sektion += inputMeddelande[inputMeddelandeBokstavPos];
                }

                // Lägg in sektionen i sektionLista
                sektionLista.Add(sektion);
            }

            // TODO: Byt namn på "initiera" till "skapa" varhelst du har skrivit det

            // Eftersom rot3 är en valfri variabel måste vi kolla om användaren har valt
            // att använda den
            if (rotNr3 != -1)
            {
                // Användaren/programmet har valt att använda tre olika rot istället för två
                treOlikaRot = true;

                // Kör sektionsloopen med 3 stycken rot
                antalRot = 3;
            }
            else
            {
                // Användaren har inte gett ett eget värde till rotNr3
                treOlikaRot = false;

                // Kör sektionsloopen med 2 stycken rot
                antalRot = 2;
            }

            // Skicka sektionerna till KrypteraRot och lägg in alla resultaten i outputMeddelande
            for (int i = 0; i < sektionLista.Count; i += antalRot)
            {
                // OBS: Lägg märke till i += antalRot ovan!
                // Detta innebär att loopen tar tre sektioner fram varje gång om det finns tre stycken rötter
                // men bara två om användaren aldrig har valt några rötter

                // Skicka sektion 1(i) till KrypteraRot
                outputMeddelandeLista.Add(KrypteraRot(sektionLista[i], avkryptera, rotNr1));

                // Skicka sektion 2(i+1) till KrypteraRot
                outputMeddelandeLista.Add(KrypteraRot(sektionLista[i+1], avkryptera, rotNr2));

                // Skicka sektion 3(i+2) till KrypteraRot OM DU HAR VALT ROT 3
                if (treOlikaRot)
                {
                    // TODO: TA BORT
                    System.Windows.Forms.MessageBox.Show("Test");
                    outputMeddelandeLista.Add(KrypteraRot(sektionLista[i+2], avkryptera, rotNr3));
                }
            }

            // Gör om meddelandelistan till en string som kan returneras
            outputMeddelande = string.Join("", outputMeddelandeLista);

            return outputMeddelande;
        }

        // Metoden som utför Ceasarchiffer med rot3 
        // MEN! Denna metoden skiftar tecknets sifferrepresentation istället för
        // positionen i alfabetet 3 steg så även mellanslag, punkter osv. ändras
        public string KrypteraRot3Char(string inputMeddelande)
        {
            // Initiera det outputMeddelande och ge det värdet av en tom string
            string outputMeddelande = "";
            // För varje char i inputMeddelande
            foreach (char inputMeddelandeBokstav in inputMeddelande)
            {
                // konverterar inputMeddelandeBokstav till en sifferrepresentation,
                // lägger sedan till 3 så att bokstaven byts ut mot bokstaven tre lägen
                // ner i sifferrepresentationstabellen. 
                char outputBokstav = Convert.ToChar(Convert.ToInt32(inputMeddelandeBokstav) + 3);

                // Lägger till den outputBokstav till outputMeddelande
                outputMeddelande.Append(outputBokstav);
            }
            return outputMeddelande;
        }

        // Metoden (av)krypterar meddelandet med hjälp av en nyckel 
        public string Nyckelkryptering(string inputMeddelande, bool avkryptera, string nyckel)
        {
            // Konvertera nyckelsträngen till ett nummer genom att ta char representationen
            // och addera ihop allt. TODO Kanske hitta en bättre metod. eller ta bort den här texten är den ens nödvändig

            // Skapa nyckelNummer och ge det ett tomt värde
            int nyckelNummer = 0;

            // Gör om nyckel till ett långt nummer genom addition
            // TODO: Detta nummer blir faktiskt ganska litet
            for (int i = 0; i < nyckel.Length; i++)
            {
                nyckelNummer += Convert.ToInt32(nyckel[i]);
            }

            // Skapa en slumpgenerator med hjälp av nyckeln som du skapat
            Random slump = new Random(nyckelNummer);

            // Skapa inputMeddelandeLista 
            List<int> inputMeddelandeLista = new List<int>();

            // Skapa outputMeddelande och ge det ett tomt värde
            string outputMeddelande = "";

            // Skapa outputMeddelandeLista
            List<char> outputMeddelandeLista = new List<char>();

            if (!avkryptera)
            {
                // Meddelandet ska krypteras

                // Gör om inputMeddelande till ett lång lista med nummer
                for (int i = 0; i < inputMeddelande.Length; i++)
                {
                    // Efter varje teckens nummerrepresentation
                    inputMeddelandeLista.Add(Convert.ToInt32(inputMeddelande[i]));
                }

                // Skapa outputMeddelandeSiffrorLista
                List<int> outputMeddelandeSiffrorLista = new List<int>();

                // *Multiplicera* varje nummer i inputMeddelandeLista med ett slumpat tal och spara numret
                foreach (int nummer in inputMeddelandeLista)
                {
                    // Talet som *multipliceras* får inte vara 0 (datan förloras då) 
                    // eller extremt stort (kan leda till en integer overflow)
                    // TODO: ÄR DET RÄTT TERM MED INTEGER OVERFLOW!?
                    outputMeddelandeSiffrorLista.Add(nummer * slump.Next(1, 1000000));
                }

                // Gör om hela listan till en lång string (en lista)
                // för att senare kunna plocka isär den siffra för siffra
                string outputMeddelandeSiffrorString = string.Join(" ", outputMeddelandeSiffrorLista);

                // Gör om varje siffra i outputMeddelandeSiffrorString till en bokstav
                // efter dess position i det svenska alfabetet (det är coolt)
                foreach (char tecken in outputMeddelandeSiffrorString)
                {
                    if (tecken == ' ')
                    {
                        // Konvertera inte bokstaven om det är ett mellanslag
                        // som ska användas för att särskilja bokstäver i originalmeddelandet
                        outputMeddelandeLista.Add(' ');
                    }
                    else
                    {
                        // Gör om char till en int efter dess numeriska värde
                        // Ex: '2' --> 2
                        int teckenVärde = (int)char.GetNumericValue(tecken);

                        // Lägg in en bokstav i outputMeddelandeLista utifrån bokstavens
                        // position i alfabetet (för att det ser coolare ut)
                        outputMeddelandeLista.Add(alfabet[teckenVärde]);
                    }
                }
            }
            else
            {
                // Meddelandet ska avkrypteras

                // Skapa inputMeddelandeString och ge den ett tomt värde
                string inputMeddelandeString = "";

                // Skapa inputMeddelandeStringLista
                List<string> inputMeddelandeStringLista = new List<string>();

                // Gör om varje bokstav i inputMeddelande till en siffra
                // efter dess position i det svenska alfabetet
                foreach (char bokstav in inputMeddelande)
                {
                    // Ta mellanslaget från den krypterade texten vars syfte är
                    // att skilja tecken i den okrypterade texten åt och lägg in det oförändrat
                    if (bokstav == ' ')
                    {
                        inputMeddelandeString.Append(' ');
                    }
                    else
                    {
                        // Lägg in ett siffertecken i inputMeddelandeLista utifrån bokstavens
                        // position i alfabetet
                        inputMeddelandeString.Append((char)alfabet.IndexOf(bokstav));
                    }
                }

                // Gör om inputMeddelandeString till en lista 
                inputMeddelandeStringLista = inputMeddelandeString.Split(' ').ToList<string>();

                // Gör om varje string i inputMeddelandeStringLista till en int
                foreach (string nummerstring in inputMeddelandeStringLista)
                {
                    inputMeddelandeLista.Add(int.Parse(nummerstring));
                }

                // Skapa outputMeddelandeSiffrorLista
                List<int> outputMeddelandeSiffrorLista = new List<int>();

                // *Dividera* varje nummer i inputMeddelandeLista med ett slumpat tal och spara numret
                foreach (int nummer in inputMeddelandeLista)
                {
                    // Talet som *divideras* får inte vara 0 (datan förloras då) 
                    // eller extremt stort (kan leda till en integer overflow)
                    // TODO: ÄR DET RÄTT TERM MED INTEGER OVERFLOW!?
                    outputMeddelandeSiffrorLista.Add(nummer * slump.Next(1, 1000000));
                }

                // Gör om outputMeddelandeSiffrorLista till ett lång lista med nummer
                for (int i = 0; i < outputMeddelandeSiffrorLista.Count; i++)
                {
                    // Efter varje teckens nummerrepresentation
                    outputMeddelandeLista.Add(Convert.ToChar(outputMeddelandeSiffrorLista[i]));
                }

                // Gör om outputlistan till en string
                outputMeddelande = string.Join("", outputMeddelandeLista);
            }
            return outputMeddelande;
        }
    }
}
