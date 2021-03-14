using System;

namespace Enigma
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Input Wheels: ");
            char a = Console.ReadKey().KeyChar;
            char b = Console.ReadKey().KeyChar;
            char c = Console.ReadKey().KeyChar;
            Console.WriteLine("\nMessage: ");
            string message = Console.ReadLine();
            Enigma machine = new Enigma(a, b, c);
            machine.SetMessage(message);
            machine.Cipher();
            Console.WriteLine(machine.GetMessage());
            Console.WriteLine("Input Wheels: ");
            char d = Console.ReadKey().KeyChar;
            char e = Console.ReadKey().KeyChar;
            char f = Console.ReadKey().KeyChar;
            Console.WriteLine();
            machine.Decipher(d, e, f);
            Console.WriteLine(machine.GetMessage());

        }
    }
}
