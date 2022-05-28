using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Slutprojekt_Kryptering_Georg
{
    class Krypteringsmetoder
    {
        // Sträng med alla bokstäver i det svenska alfabetet
        string alfabet = "abcdefghijklmnopqrstuvwxyzåäö";


        // Metoden som utför Ceasarchiffer med vilken rot som helst
        // Klarar både kryptering och avkryptering.
        // Metoden ignorerar alla tecken som inte finns i det svenska alfabetet
        public string KrypteraRot(string inputMeddelande, bool kryptera, int rot)
        {
            string outputMeddelande;

            // Skapa outputMeddelandeLista som kommer att omvandlas till en string
            // (outputMeddelande) efter den är färdig
            List<char> outputMeddelandeLista = new List<char>();

            // outputBokstavPosition kommer att spara den utgående bokstavens position i alfabetet
            int outputBokstavPosition;

            // inputMeddelandeBokstavPosition sparar den ingående bokstavens position i alfabetet 
            int inputMeddelandeBokstavPosition;
            char outputBokstav;
            bool storBokstav;

            // För varje char i inputMeddelande
            foreach (char inputMeddelandeBokstav in inputMeddelande)
            {
                // Spara om bokstaven är liten eller stor
                if (char.ToLower(inputMeddelandeBokstav) != inputMeddelandeBokstav)
                {
                    storBokstav = true;
                }
                else
                {
                    storBokstav = false;
                }

                // Omvandla bokstaven till en liten bokstav om den inte redan är det,
                // programmet har redan sparat bokstavens storlek
                char inputMeddelandeBokstavLiten = char.ToLower(inputMeddelandeBokstav);

                // Kryptera om inputMeddelandeBokstav är en bokstav
                if (alfabet.Contains(inputMeddelandeBokstavLiten))
                {
                    // Hittar bokstavens position i alfabetet
                    inputMeddelandeBokstavPosition = alfabet.IndexOf(inputMeddelandeBokstavLiten);

                    // Skiftar bokstavens position ett antal (bestämst av värdet på rot)
                    // steg i alfabetet för att få den (av)krypterade bokstaven
                    if (kryptera)
                    {
                        // Om metoden ska kryptera ska bokstaven skiftas nedåt
                        outputBokstavPosition = inputMeddelandeBokstavPosition + rot;
                    }
                    else
                    {
                        // Om metoden ska avkryptera ska bokstaven skiftas uppåt
                        outputBokstavPosition = inputMeddelandeBokstavPosition - rot;
                    }

                    // Om bokstaven har "gått runt" hela alfabetet uppåt
                    // (t.ex. om bokstaven är ö och skiftas nedåt)
                    if (outputBokstavPosition >= alfabet.Length)
                    {
                        // Se till så att den går runt till början igen
                        outputBokstavPosition -= alfabet.Length;

                    }
                    // Samma sak fast bokstaven har "gått runt" nedåt
                    else if (outputBokstavPosition < 0)
                    {
                        outputBokstavPosition += alfabet.Length;
                    }

                    // Omvandlar bokstavens placering i alfabetet till själva bokstaven
                    outputBokstav = alfabet[outputBokstavPosition];
                    
                    // Lägger till outputBokstav till outputMeddelandeLista
                    if (storBokstav)
                    {
                        // Om bokstaven är stor

                        // Omvandlar outputbokstav till en stor bokstav
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
            // Omvandla listan till en string
            outputMeddelande = string.Join("", outputMeddelandeLista);

            return outputMeddelande;
        }

        // Metod som utför Ceasarchiffer med först rot7 under fem bokstäver,
        // sedan rot13 under fem bokstäver och fortsätter så.
        // Kan även göra detta med andra rot, samt andra mängder bokstäver
        public string KrypteraRotVäxlande(string inputMeddelande, bool kryptera, int sektionslängd, int rotNr1, int rotNr2)
        {
            // skapa meddelandevariabeln som kommer att returneras i slutet av metoden
            string outputMeddelande;

            // Skapa outputMeddelandeLista som kommer att sammanfogas till
            // en enda lång string i slutet av programmet
            List<string> outputMeddelandeLista = new List<string>();

            // Skapa sektionslistan som fylls med sektioner (bitar från inputmeddelande) längre ner
            List<string> sektionLista = new List<string>();

            // Skapa sektion som läggs in i sektionLista senare
            string sektion;

            // Fyll sektionLista med sektioner vars längd bestäms av sektionslängd.
            // Denna lista kommer sedan skickas bit för bit till metoden KrypteraRot
            for (int i = 0; i < inputMeddelande.Length; i += sektionslängd)
            {
                // Återställ strängen sektion
                sektion = "";

                // Fyll den nuvarande sektionen med bokstäver från inputMeddelande
                for (int inputMeddelandeBokstavPos = i; inputMeddelandeBokstavPos < i + sektionslängd; inputMeddelandeBokstavPos++)
                {
                    // Om slutet av meddelandet har nåtts
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

            // Skicka sektionerna till KrypteraRot och lägg in alla resultaten i outputMeddelande
            for (int i = 0; i < sektionLista.Count - 1; i += 2)
            {

                // Skicka sektion 1(i) till KrypteraRot
                outputMeddelandeLista.Add(KrypteraRot(sektionLista[i], kryptera, rotNr1));

                // Skicka sektion 2(i+1) till KrypteraRot
                outputMeddelandeLista.Add(KrypteraRot(sektionLista[i+1], kryptera, rotNr2));
            }

            // Gör om meddelandelistan till en string som kan returneras
            outputMeddelande = string.Join("", outputMeddelandeLista);

            return outputMeddelande;
        }

        // Metoden (av)krypterar meddelandet med hjälp av en nyckel 
        // för att krypteras omvandlas meddelandet först till en lång sträng med nummer,
        // ett nummer för varje bokstav, och sedan multipliceras varje nummer med ett pseudoslumpat
        // tal med ett seed som genererats från nyckeln som skrivs in.
        // Samma sak fast omvänt sker för att avkryptera strängen.
        public string Nyckelkryptering(string inputMeddelande, bool kryptera, string nyckel)
        {
            // nyckelnummer är det nummer som matas in i slumpgeneratorn som seed
            int nyckelNummer = 0;

            // Gör om nyckel till ett nummer genom addition
            for (int i = 0; i < nyckel.Length; i++)
            {
                nyckelNummer += Convert.ToInt32(nyckel[i]);
            }

            // Skapa en slumpgenerator med hjälp av nyckeln som har matats in
            Random slump = new Random(nyckelNummer);

            // inputMeddelandeLista kommer att inehålla:
            // en massa teckens nummer representationer t.ex. [55, 673, 234, 16] om meddelandet ska krypteras
            // en lång lista med nummer som inte än har dividerats med slumpgeneratorn om meddelandet ska avkrypteras
            List<long> inputMeddelandeLista = new List<long>();

            // outputMeddelandeLista kommer att innehålla de tecken som sedan sammanfogas till outputMeddelande
            List<char> outputMeddelandeLista = new List<char>();

            if (kryptera)
            {
                // Meddelandet ska krypteras

                // inputMeddelande ser ut så här: "test"
                // Gör om inputMeddelande till ett lång lista med nummer utifrån varje teckens nummerrepresentation
                foreach (char tecken in inputMeddelande)
                {
                    inputMeddelandeLista.Add(Convert.ToInt32(tecken));
                }

                // outputMeddelandeSiffrorLista kommer spara de krypterade numren
                List<long> outputMeddelandeSiffrorLista = new List<long>();

                // inputMeddelandeLista ser ut så här just nu: [55, 673, 234, 55]
                // *Multiplicera* varje nummer i inputMeddelandeLista med ett slumpat tal och spara numret
                foreach (long nummer in inputMeddelandeLista)
                {
                    // Talet som *multipliceras* får inte vara 0 (datan förloras då) 
                    // eller extremt stort (kan leda till en integer overflow)
                    outputMeddelandeSiffrorLista.Add(nummer * (long)slump.Next(1, 100000));
                }

                // Gör om hela listan till en lång string (en lista)
                // för att senare kunna plocka isär den siffra för siffra
                string outputMeddelandeSiffrorString = string.Join(" ", outputMeddelandeSiffrorLista);

                // outputMeddelandeSiffrorString ser nu ut så här: "123833 441838 223034 390531"
                // Omvandla varje siffra i outputMeddelandeSiffrorString till en bokstav
                // efter dess position i det svenska alfabetet (det är coolare än siffror)
                foreach (char tecken in outputMeddelandeSiffrorString)
                {
                    if (tecken == ' ')
                    {
                        // Omvandla inte bokstaven om det är ett mellanslag
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

                // Kolla först så att indatan är korrekt formaterad
                // inputMeddelande får bara innehålla a-j och mellanslag
                foreach (char bokstav in inputMeddelande)
                {
                    if ('a' <= bokstav && bokstav <= 'j' || bokstav == ' ')
                    {
                        continue;
                    }
                    else
                    {
                        // Denna borde aldrig visas om användaren använder programmet korrekt
                        System.Windows.Forms.MessageBox.Show("Ditt krypterade meddelande har tecken som inte är tillåtna.");
                        return "";
                    }
                }

                // Ta bort mellanslag i slutet eller början av meddelandet eftersom det kan krascha programmet
                string saneratInputMeddelande = inputMeddelande.Trim();

                // inputMeddelandeCharLista kommer att vara en lång lista med individuella siffertecken
                // t.ex. ['1','2','3','4','5',' ','5','4','3','2','1']
                List<int> inputMeddelandeCharLista = new List<int>();

                // inputmeddelande ser till en början ut såhär: "bcdefgh hgfedcb"
                // Gör om varje bokstav i inputMeddelande till en siffra
                // efter dess position i det svenska alfabetet
                foreach (char bokstav in saneratInputMeddelande)
                {
                    // Ta mellanslaget från den krypterade texten vars syfte är
                    // att skilja tecken i den okrypterade texten åt och lägg in det oförändrat
                    if (bokstav == ' ')
                    {
                        inputMeddelandeCharLista.Add(' ');
                    }
                    else
                    {
                        // Lägg in ett siffertecken i inputMeddelandeLista utifrån bokstavens
                        // position i alfabetet
                        // +'0' gör så att 0 = '0', 1 = '1' osv.
                        inputMeddelandeCharLista.Add((char)(alfabet.IndexOf(bokstav)+'0'));
                    }
                }


                List<string> inputMeddelandeStringLista = new List<string>();

                List<char> inputTemporärLista = new List<char>();

                // inputMeddelandeCharLista ser nu ut så här:
                // ['1','2','3','4','5',' ','5','4','3','2','1']
                // Kombinera ihop inputMeddelandeCharLista så att den istället ser ut så här:
                // ["12345","54321"]
                foreach (char tecken in inputMeddelandeCharLista)
                {
                    if (tecken == ' ')
                    {
                        inputMeddelandeStringLista.Add(string.Join("",inputTemporärLista));
                        inputTemporärLista.Clear();
                    }
                    else
                    {
                        inputTemporärLista.Add(tecken);
                    }
                }
                // Lägg till den sista strängen
                inputMeddelandeStringLista.Add(string.Join("", inputTemporärLista));

                // inputMeddelandeStringLista ser nu ut så här: ["12345","54321"]
                // Gör om varje string i inputMeddelandeStringLista till en long
                foreach (string nummerstring in inputMeddelandeStringLista)
                {
                    bool lyckadOmvandling = long.TryParse(nummerstring, out long inputMeddelandeLong);

                    if (lyckadOmvandling)
                    {
                        inputMeddelandeLista.Add(inputMeddelandeLong);
                    }
                    else
                    {
                        // Om nummerstring inte kan omvandlas till siffror skickas ett felmeddelande till användaren
                        System.Windows.Forms.MessageBox.Show("Dubbelkolla din inmatning. Det finns antingen inga tecken alls i 'avkryptera' textboxen, " +
                            "för många mellanslag mellan 'bokstavssektionerna' eller för långa 'bokstavssektioner'");
                        return "";
                    }
                    
                }

                List<int> outputMeddelandeSiffrorLista = new List<int>();

                // inputMeddelandeLista ser nu ut så här: [12345, 54321]
                // *Dividera* varje nummer i inputMeddelandeLista med ett slumpat tal
                foreach (long nummer in inputMeddelandeLista)
                {
                    outputMeddelandeSiffrorLista.Add((int)(nummer / slump.Next(1, 100000)));
                }

                // outputMeddelandeSiffrorLista ser nu ut så här: [241, 67]
                // Gör om varje tal i outputMeddelandeSiffrorLista till ett tecken
                foreach (int tal in outputMeddelandeSiffrorLista)
                {
                    // Kontrollera att numret är inom omfånget för teckenvärden
                    // innan det omvandlas till ett tecken
                    if (tal <= 65535)
                    {
                        outputMeddelandeLista.Add(Convert.ToChar(tal));
                    }
                    else
                    {
                        // Skicka ett felmeddelande till användaren om det inte går
                        System.Windows.Forms.MessageBox.Show("En av dina bokstavssträngar är för stor.");
                        return "";
                    }
                }
            }

            // Gör om outputlistan till en string
            string outputMeddelande = string.Join("", outputMeddelandeLista);

            return outputMeddelande;
        }
    }
}
