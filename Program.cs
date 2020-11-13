using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamning_2_ra_kod
{
    class Person
    {    /* CLASS: Person
         * PURPOSE: Allows program to assign values to name, adress, phone, email for each inputted person
         *          Used to add new people to the list, remove from list, show list etc.     
         */
        public string name, adress, phone, email;
        public Person(string N, string A, string T, string E)
        {
            name = N; adress = A; phone = T; email = E;
        }
        /* METHOD: Print
         * PURPOSE: Prints the first name on the list
         * PARAMETERS: name - name of the person, adress - adress value of person, phone - phone number of person, email - email of person
         * RETURN VALUE: Returns the last person of the list
         */
        public void Print()
        {
            Console.WriteLine($"{name} {adress} {phone} {email}");
        }
        /* METHOD: ChangePerson (public)
         * PURPOSE: Use the values from ChangeValue and input new value
         * PARAMETERS: wantToChange - user input from ChangeValue of what you want to change,
         *             newValue - user input from ChangeValue of what the new value should be
         * RETURN VALUE: Returns list with updated change
         */
        public void ChangePerson(string wantToChange, string newValue)
        {
            
            switch (wantToChange)
            {
                case "namn": name = newValue; break;
                case "adress": adress = newValue; break;
                case "telefon": phone = newValue; break;
                case "email": email = newValue;  break;
            }
                
        }
        public Person()
        {
            Console.WriteLine("Lägger till ny person");
            Console.Write("  1. ange namn:    ");
            name = Console.ReadLine();
            Console.Write("  2. ange adress:  ");
            adress = Console.ReadLine();
            Console.Write("  3. ange telefon: ");
            phone = Console.ReadLine();
            Console.Write("  4. ange email:   ");
            email = Console.ReadLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> Dict = new List<Person>();
            Console.Write("Laddar adresslistan ... ");

            AdressList(Dict);

            Console.WriteLine("klart!");
            Console.WriteLine("Hej och välkommen till adresslistan");
            Console.WriteLine("Skriv 'sluta' för att sluta!");
            string command;
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                if (command == "sluta")
                {
                    Console.WriteLine("Hej då!");
                }
                else if (command == "ny")
                {
                    Dict.Add(new Person());
                    if (Dict.Count > 0)
                    {
                        Console.WriteLine("La till: ");
                        Dict[Dict.Count - 1].Print();
                    }
                }
                else if (command == "ta bort")
                {
                    Remove(Dict);
                }
                else if (command == "visa")
                {
                    for (int i = 0; i < Dict.Count(); i++)
                    {
                        Dict[i].Print();
                    }
                }
                else if (command == "ändra")
                {
                    ChangeValue(Dict);
                }
                else
                {
                    Console.WriteLine("Okänt kommando: {0}", command);
                }
            } while (command != "sluta");
        }
        /* METHOD: AdressList (static)
         * PURPOSE: Reads file for list of adresses
         * PARAMETERS: fileStream - path to file, line - reads lines in file, word - splits the lines at '#', 
         *             P - values of the persons in list, Dict.Add - Adds the persons in file to list Dict
         * RETURN VALUE: Returns the list of persons in file to list Dict
         */
        static void AdressList(List<Person> Dict)
        {
            using (StreamReader fileStream = new StreamReader(@"..\..\address.lis"))
            {
                while (fileStream.Peek() >= 0)
                {
                    string line = fileStream.ReadLine();
                    string[] word = line.Split('#');
                    Person P = new Person(word[0], word[1], word[2], word[3]);
                    Dict.Add(P);
                }
            }
        }
        /* METHOD: Remove (static)
         * PURPOSE: Removes existing person from list Dict
         * PARAMETERS: wantToRemove - user input, found - error handling,
         *             Dict.RemoveAt - Removes person if the user input matches person in list Dict
         * RETURN VALUE: Removes user inputted person from list
         */
        static void Remove(List<Person> Dict)
        {
            Console.Write("Vem vill du ta bort (ange namn): ");
            string wantToRemove = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].name == wantToRemove) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", wantToRemove);
            }
            else
            {
                Dict.RemoveAt(found);
            }
        }
        /* METHOD: ChoosePerson (static)
         * PURPOSE: Choose which person in the list to change
         * PARAMETERS: whoToChange - user input, found - error handling
         * RETURN VALUE: Return value of who to change, or return -1 
         */
        static int ChoosePerson(List<Person> Dict)
        {
            Console.Write("Vem vill du ändra (ange namn): ");
            string whoToChange = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].name == whoToChange) 
                    return i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", whoToChange);
            }
            return -1;
        }
        /* METHOD: ChangeValue (static)
         * PURPOSE: Choose what you want to change of the selected person
         * PARAMETERS: i - position value of person in list, P - name/adress etc. value of person in list, 
         * wantToChange - user input of what you want to change in person, newChange - user input of new value in changed value
         * RETURN VALUE: Return what value you want changed and what to change it to 
         */
        static void ChangeValue(List<Person> Dict)
        {
            int i = ChoosePerson(Dict);
            if (i == -1)
            {
                return;
            }
            else
            {
                Person P = Dict[i];
                Console.Write("Vad vill du ändra? (namn/adress/telefon/email)");
                string wantToChange = Console.ReadLine();
                Console.Write("Vad vill du ändra {0} till: ", wantToChange);
                string newChange = Console.ReadLine();
                P.ChangePerson(wantToChange, newChange);
            }
        }
    }
}
