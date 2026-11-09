using Microsoft.VisualBasic;

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
            //Assign contestants
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
            //Bubble sort by last name
            for (int i = 0; i < contestants.Length - 1; i++)
            {
                for (int j = 0; j < contestants.Length - 1 - i; j++)
                {
                    if (contestants[j].lastname.CompareTo(contestants[j + 1].lastname) > 0)
                    {
                        Contestant temp = contestants[j];
                        contestants[j] = contestants[j + 1];
                        contestants[j + 1] = temp;
                    }
                }
            }
            //Start with main menu
            MainMenu(count, contestants);
        }
        static void MainMenu(int count, Contestant[] contestants)
        {
            Console.Clear();
            Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
            Console.WriteLine();
            Console.WriteLine("[ Main Menu ] ".PadLeft(15));
            Console.WriteLine();
            Console.WriteLine("1. Play\n2. Instructions\n3. Contestants\n0. Quit");
            Console.WriteLine();
            Console.Write(">");
            //Read key pressed, this means the user doesn't have to press enter again
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
                    ContestantMenu(count, contestants);
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
            Console.WriteLine($"CONTESTANTS:\nUpon pressing play, you play as one of {count} contestants. You can view/edit contestant information in the 'Contestants' section." +
                "\n10 contestants are randomly selected as finalists and 1 from the selected 10 will move forward to contend." +
                "\n\nINSTRUCTIONS:\nYou have to guess a randomly selected word. Each letter has been blanked out." +
                "\nEvery turn, you spin a wheel with 24 segments. 22 of these segments contain cash prizes ranging from $300 to $5000. 2 segments are penalties of -$2000 and -$3000." +
                "\nIf you guess a letter correctly, your prize is multiplied by the amount of times the guessed letter appears in the word." +
                "\nIf you guess a vowel, your score is reduced by a flat rate of $250." +
                "\nIf you guess a letter the word does not contain, you will lose your current prize and spin again." +
                "\nIf you enter the entire word early, you will not receive anymore spins, but you will not be penalized by vowels." +
                "\n\nKeep spinning until the word is complete!");
            Console.WriteLine();
            Console.Write("ENTER to return");
            Console.ReadLine();
            MainMenu(count, contestants); 
        }
        static void ContestantMenu(int count, Contestant[] contestants)
        {
            Console.Clear();
            Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
            Console.WriteLine();
            Console.WriteLine("[ Contestant Menu ] ".PadLeft(15));
            Console.WriteLine();
            Console.WriteLine("1. View Contestants\n2. Edit Contestant\n0. Back");
            Console.WriteLine();
            Console.Write(">");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            char keyChar = keyInfo.KeyChar;
            ConsoleKey key = keyInfo.Key;
            switch (keyChar)
            {
                case '1':
                    ShowContestants(count, contestants);
                    break;
                case '2':
                    EditContestant(count, contestants);
                    break;
                case '0':
                    MainMenu(count, contestants);
                    break;
                default:
                    InvalidInput();
                    ContestantMenu(count, contestants);
                    break;
            }
        }
        static void EditContestant(int count, Contestant[] contestants)
        {
            bool found = false;
            Console.Clear();
            Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
            Console.WriteLine();
            Console.WriteLine("[ Contestant Menu ] ".PadLeft(15));
            Console.WriteLine();
            Console.WriteLine("Search by first or last name:");
            Console.WriteLine();
            Console.Write(">");
            string wanted = Console.ReadLine().ToLower().Trim();
            //Search
            for (int i = 0; i < contestants.Length; i++)
            {
                if (contestants[i].firstname.ToLower().Trim() == wanted || contestants[i].lastname.ToLower().Trim() == wanted)
                {
                    found = true;
                    Console.Clear();
                    Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
                    Console.WriteLine();
                    Console.WriteLine("[ Contestant Menu ] ".PadLeft(15));
                    Console.WriteLine();
                    Console.WriteLine($"Selected contestant: {contestants[i].firstname} {contestants[i].lastname}");
                    Console.WriteLine();
                    Console.WriteLine($"Interest: {contestants[i].interest,-30} Score: {contestants[i].score}");
                    Console.WriteLine("1. Edit First Name\n2. Edit Last Name\n3. Edit Interest\n4. Reset Score\n0. Back");
                    Console.Write(">");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    char keyChar = keyInfo.KeyChar;
                    ConsoleKey key = keyInfo.Key;
                    switch (keyChar)
                    {
                        case '1':

                    }
                }
            }
            if (found == false)
            {
                Console.WriteLine();
                Console.Write("Contestant not found. ENTER to return");
                Console.ReadLine();
                EditContestant(count, contestants);
            }
        }
        static void EditFirstName(int count, Contestant[] contestants)
        {
            Console.Clear();
            Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
            Console.WriteLine();
            Console.WriteLine("[ Contestant Menu ] ".PadLeft(15));
            Console.WriteLine();
            Console.WriteLine($"Enter new first name or leave blank to return.");
            Console.WriteLine();
            Console.Write(">");
        }
        static void ShowContestants(int count, Contestant[] contestants)
        {
            Console.Clear();
            Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
            Console.WriteLine();
            Console.WriteLine("[ Contestant Menu ] ".PadLeft(15));
            Console.WriteLine();
            Console.WriteLine("#  First Name      Last Name                Interest                      Score");
            Console.WriteLine("--------------------------------------------------------------------------------");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(
                    $"{i + 1,-3}" +
                    $"{contestants[i].firstname,-15} {contestants[i].lastname,-25}" +
                    $"{contestants[i].interest,-30}" +
                    $"{contestants[i].score,-5}");
            }
            Console.WriteLine();
            Console.Write("ENTER to return");
            Console.ReadLine();
            ContestantMenu(count, contestants);
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
