namespace DominikAickenSpinTheWheel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }
        static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
            Console.WriteLine();
            Console.WriteLine("[ Main Menu] ".PadLeft(15));
            Console.WriteLine();
            Console.WriteLine("1. Play\n2. Instructions\n3. Contestants\n4. Reset Save File\n0. Exit");
            Console.WriteLine();
            Console.Write(">");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            char keyChar = keyInfo.KeyChar;
            ConsoleKey key = keyInfo.Key;
            switch (keyChar)
            {
                case '1':
                    break;
                case '2':
                    break;
                case '3':
                    break;
                case '4':
                    break;
                case '5':
                    break;
                default:
                    break;
            }
            Console.ReadLine();
        }
        static void Instructions()
        {

        }
        static void ContestantMenu()
        {

        }
    }
}
