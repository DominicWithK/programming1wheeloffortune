namespace DominikAickenSpinTheWheel
{
    public struct Contestant
    {
        public string firstname;
        public string lastname;
        public string interest;
        public int score;
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Contestant[] contestants = new Contestant[23];
            StreamReader reader = new StreamReader("wheelOfFortune.txt");
            int count = 0;
            while (!reader.EndOfStream)
            {
                contestants[count].firstname = reader.ReadLine().Trim();
                contestants[count].lastname = reader.ReadLine().Trim();
                contestants[count].interest = reader.ReadLine().Trim();
                contestants[count].score = Convert.ToInt32(reader.ReadLine().Trim());
                count++;
            }
            for (int i = 0; i < contestants.Length - 1; i++)
            {
                for (int j = 0; j < contestants.Length - 1 - i; j++)
                {
                    if (contestants[j].firstname.CompareTo(contestants[j + 1].firstname) > 0)
                    {
                        Contestant temp = contestants[j];
                        contestants[j] = contestants[j + 1];
                        contestants[j + 1] = temp;
                    }
                }
            }
            MainMenu(count, contestants);
        }
        static void MainMenu(int count, Contestant[] contestants)
        {
            Console.Clear();
            Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
            Console.WriteLine();
            Console.WriteLine("[ Main Menu ] ".PadLeft(15));
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
                    Instructions(count, contestants);
                    break;
                case '3':
                    ShowContestants(count, contestants);
                    break;
                case '4':
                    break;
                case '0':
                    break;
                default:
                    InvalidInput();
                    MainMenu(count, contestants);
                    break;
            }
        }
        static void Instructions(int count, Contestant[] contestants)
        {
            Console.Clear();
            Console.WriteLine("HOW TO PLAY: Wheel of Fortune Game Show");
            Console.WriteLine();
            Console.WriteLine("CONTESTANTS:\nUpon pressing play, you play as one of 23 contestants. You can view/edit contestant information in the 'Contestants' section." +
                "\n10 contestants are randomly selected as finalists and 1 from the selected 10 will move forward to contend." +
                "\n\nINSTRUCTIONS:\nYou have to guess a randomly selected word. Each letter has been blanked out." +
                "\nEvery turn, you spin a wheel with 24 segments. 22 of these segments contain cash prizes ranging from $300 to $5000. 2 segments are penalties of -$2000 and -$3000." +
                "\nIf you guess a letter correctly, your prize is multiplied by the amount of times the guessed letter appears in the word." +
                "\nIf you guess a vowel, your score is reduced by a flat rate of $250." +
                "\nIf you guess a letter the word does not contain, you will spin again." +
                "\nIf you enter the entire word early, you will not receive anymore spins, but you will not be penalized by vowels." +
                "\n\nKeep spinning until the word is complete!");
            Console.WriteLine();
            Console.Write("ENTER to return");
            Console.ReadLine();
            MainMenu(count, contestants); 
        }
        static void ContestantMenu()
        {

        }
        static void ShowContestants(int count, Contestant[] contestants)
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{contestants[i].firstname} {contestants[i].lastname}");
            }
            Console.ReadLine();
        }
        static void InvalidInput()
        {
            Console.WriteLine();
            Console.WriteLine("Invalid input!");
            Console.WriteLine();
            Console.Write("ENTER to return");
            Console.ReadLine();
        }
    }
}
