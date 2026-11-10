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
            BubbleSort(ref contestants, count);
            //Start with main menu
            MainMenu(count, contestants);
        }
        static void BubbleSort(ref Contestant[] contestants, int count)     //Sorts by last name
        {
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - 1 - i; j++)
                {
                    if (contestants[j].lastname.CompareTo(contestants[j + 1].lastname) > 0)
                    {
                        Contestant temp = contestants[j];
                        contestants[j] = contestants[j + 1];
                        contestants[j + 1] = temp;
                    }
                }
            }
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
                    Console.WriteLine();
                    Console.WriteLine("Thank you for playing!");
                    Thread.Sleep(1500);
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
            Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
            Console.WriteLine();
            Console.WriteLine("[ Instructions ] ".PadLeft(15));
            Console.WriteLine();
            Console.WriteLine($"CONTESTANTS:\nUpon pressing play, you play as one of {count} contestants. You can view/edit contestant information in the 'Contestants' menu." +
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
                    BubbleSort(ref contestants, count);
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
            Console.WriteLine("Search by first/last name or leave blank to return");
            Console.WriteLine();
            Console.Write(">");
            string wanted = Console.ReadLine().ToLower().Trim();
            //Search
            if (string.IsNullOrWhiteSpace(wanted))
            {
                ContestantMenu(count, contestants);
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    if (contestants[i].firstname.ToLower().Trim() == wanted || contestants[i].lastname.ToLower().Trim() == wanted)
                    {
                        found = true;
                        int chosen = i;
                        ChosenContestant(count, chosen, contestants);
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
        }
        static void ChosenContestant(int count, int chosen, Contestant[] contestants)
        {
            Console.Clear();
            Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
            Console.WriteLine();
            Console.WriteLine("[ Contestant Menu ] ".PadLeft(15));
            Console.WriteLine();
            Console.WriteLine($"Selected contestant: {contestants[chosen].firstname} {contestants[chosen].lastname}");
            Console.WriteLine();
            Console.WriteLine($"Interest: {contestants[chosen].interest,-30} Score: {contestants[chosen].score}");
            Console.WriteLine();
            Console.WriteLine("1. Edit First Name\n2. Edit Last Name\n3. Edit Interest\n4. Reset Score\n0. Back");
            Console.WriteLine();
            Console.Write(">");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            char keyChar = keyInfo.KeyChar;
            ConsoleKey key = keyInfo.Key;
            switch (keyChar)
            {
                case '1':
                    EditFirstName(count, chosen, ref contestants);
                    break;
                case '2':
                    EditLastName(count, chosen, ref contestants);
                    break;
                case '3':
                    EditInterest(count, chosen, ref contestants);
                    break;
                case '4':
                    ResetScore(count, chosen, ref contestants);
                    break;
                case '0':
                    ContestantMenu(count, contestants);
                    break;
                default:
                    InvalidInput();
                    ChosenContestant(count, chosen, contestants);
                    break;

            }
        }
        static void ResetScore(int count, int chosen, ref Contestant[] contestants)
        {
            contestants[chosen].score = 0;
            Console.WriteLine();
            Console.Write("Score reset to 0! ENTER to return");
            Console.ReadLine();
            ChosenContestant(count, chosen, contestants);
        }
        static void EditInterest(int count, int chosen, ref  Contestant[] contestants)
        {
            Console.Clear();
            Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
            Console.WriteLine();
            Console.WriteLine("[ Contestant Menu ] ".PadLeft(15));
            Console.WriteLine();
            Console.WriteLine($"Enter new interest or leave blank to return.");
            Console.WriteLine();
            Console.WriteLine("Max 30 CHR");
            Console.WriteLine();
            Console.Write(">");
            string newinterest = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newinterest))
            {
                ChosenContestant(count, chosen, contestants);
            }
            else if (newinterest != null && newinterest.Length > 30)
            {
                Console.WriteLine();
                Console.Write("New interest cannot exceed 30 characters. ENTER to return");
                Console.ReadLine();
                EditInterest(count, chosen, ref contestants);
            }
            else
            {
                contestants[chosen].interest = newinterest;
                ChosenContestant(count, chosen, contestants);
            }
        }
        static void EditFirstName(int count, int chosen, ref Contestant[] contestants)
        {
            Console.Clear();
            Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
            Console.WriteLine();
            Console.WriteLine("[ Contestant Menu ] ".PadLeft(15));
            Console.WriteLine();
            Console.WriteLine($"Enter new first name or leave blank to return.");
            Console.WriteLine();
            Console.WriteLine("Max 15 CHR");
            Console.WriteLine();
            Console.Write(">");
            string newname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newname))
            {
                ChosenContestant(count, chosen, contestants);
            }
            else if (newname != null && newname.Length > 15)
            {
                Console.WriteLine();
                Console.Write("New first name cannot exceed 15 characters. ENTER to return");
                Console.ReadLine();
                EditFirstName(count, chosen, ref contestants);
            }
            else
            {
                contestants[chosen].firstname = newname;
                ChosenContestant(count, chosen, contestants);
            }
        }
        static void EditLastName(int count, int chosen, ref Contestant[] contestants)
        {
            Console.Clear();
            Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
            Console.WriteLine();
            Console.WriteLine("[ Contestant Menu ] ".PadLeft(15));
            Console.WriteLine();
            Console.WriteLine($"Enter new last name or leave blank to return.");
            Console.WriteLine();
            Console.WriteLine("Max 25 CHR");
            Console.WriteLine();
            Console.Write(">");
            string newname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newname))
            {
                ChosenContestant(count, chosen, contestants);
            }
            else if (newname != null && newname.Length > 25)
            {
                Console.WriteLine();
                Console.Write("New last name cannot exceed 25 characters. ENTER to return");
                Console.ReadLine();
                EditLastName(count, chosen, ref contestants);
            }
            else
            {
                contestants[chosen].lastname = newname;
                ChosenContestant(count, chosen, contestants);
                BubbleSort(ref contestants, count);
            }
        }
        static void ShowContestants(int count, Contestant[] contestants)
        {
            Console.Clear();
            Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
            Console.WriteLine();
            Console.WriteLine("[ Contestant Menu ] ".PadLeft(15));
            Console.WriteLine();
            Console.WriteLine("#  First Name       Last Name                 Interest                       Score");
            Console.WriteLine("----------------------------------------------------------------------------------");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(
                    $"{i + 1,-3}" +
                    $"{contestants[i].firstname,-16} {contestants[i].lastname,-26}" +
                    $"{contestants[i].interest,-31}" +
                    $"{contestants[i].score,-6}");
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
