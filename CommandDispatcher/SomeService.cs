using ConsoleEventDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public class SomeService
    {
        public void PodajWiek()
        {
            Console.WriteLine("'a' was pressed in main menu!");
            Console.WriteLine("Podaj wiek");
            var wiek = Console.ReadLine();
            Console.WriteLine("Twoj wiek to:" + wiek);

        }

        public void DodajDwieZmienne()
        {
            Console.WriteLine("Podaj zmienna A");
            var a = Console.ReadLine();
            Console.WriteLine("Podaj zmienna B");
            var b = Console.ReadLine();
            Console.WriteLine($"Wynik to: {a + b}");           
        }

        public void ThisIsCommandUnderKeyS()
        {
            Console.WriteLine("'s' was pressed. Switching to secondary menu!");           
        }

        public void ThisIsCommandUnderKeyX(int a)
        {
            Console.WriteLine($"MethodInvoked with value {a}");
        }
    }

}
