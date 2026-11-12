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
        static void ContestantSelect(int count, int[] finalists, Contestant[] contestants)
        {
            Random rand = new Random();
            int winner = rand.Next(10);
            int position = 0;
            int delay = 50;
            int spins = 40 + winner;
            for (int spin = 0; spin <= spins; spin++)
            {
                position = spin % 10;
                Console.Clear();
                Console.WriteLine("SELECTING TODAY'S CONTESTANT...\n");
                for (int i = 0; i < 10; i++)
                {
                    int c = finalists[i];
                    if (i == position)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"> {contestants[c].firstname} {contestants[c].lastname} <");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {contestants[c].firstname} {contestants[c].lastname}");
                    }
                }
                Thread.Sleep(delay);
                position++;
                if (position >= 10)
                    position = 0;
                if (delay < 250)
                    delay += 5;
            }
            int winningContestant = finalists[winner];
            Thread.Sleep(700);
            Console.WriteLine();
            Console.WriteLine($"{contestants[winningContestant].firstname} {contestants[winningContestant].lastname} has been selected!");
            Thread.Sleep(1400);
            Game(count, winningContestant, ref contestants);
        }
        static int WheelSpin()
        {
            Random rand = new Random();
            int[] wheel ={ 5000, 300, 1500, 900,4000, 800, 2500, -2000,600, 3500, 1200, 400,4500, 700, 3000, 1700,500, 2200, -3000, 1000,1800, 750, 2000, 1300 };
            int prize = rand.Next(24);
            int delay = 40;
            int spins = 24 + prize;
            for (int spin = 0; spin <= spins; spin++)
            {
                int position = spin % 24;
                Console.Clear();
                Console.WriteLine("SPINNING THE WHEEL\n");
                Console.WriteLine("        ▼");
                Console.WriteLine();
                for (int row = -2; row <= 2; row++)
                {
                    int index = (position + row + 24) % 24;
                    if (row == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        if (wheel[index] < 0)
                        {
                            Console.WriteLine($"   > ${wheel[index],-5} < ");
                        }
                        else
                        {
                            Console.WriteLine($"   > ${wheel[index],-5} < ");
                        }
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"     ${wheel[index]}");
                    }
                }
                Thread.Sleep(delay);
                if (delay < 200)
                    delay += 6;
            }
            int result = wheel[prize];
            if (result > 0) 
            { 
                Console.ForegroundColor = ConsoleColor.Green; 
            }
            else 
            { 
                Console.ForegroundColor = ConsoleColor.Red; 
            }
            Thread.Sleep(700);
            Console.Clear();
            Console.WriteLine($"WHEEL RESULT: ${result}");
            Console.ResetColor();
            Thread.Sleep(1400);
            return result;
        }
        static string WordSelect()
        {
            Random rand = new Random();
            //List of 20 words (with the help of CoPilot):
            string[] words =
            {
            "CHOCOLATES",
            "PLAYGROUND",
            "WATERFALLS",
            "SUNFLOWERS",
            "TELESCOPES",
            "HEADPHONES",
            "ALLIGATORS",
            "LIGHTHOUSE",
            "BACKPACKER",
            "FOOTBALLER",
            "SKATEBOARD",
            "BOOKSTORES",
            "RAINFOREST",
            "MICROWAVES",
            "PINEAPPLES",
            "ADVENTURES",
            "FIREFIGHTER",
            "BASKETBALL",
            "CENTIPEDES",
            "UNDERGROUND"
            };
            string answer = words[rand.Next(words.Length)];
            return answer;
        }
        static void GameScreen(int winningContestant, Contestant[] contestants, int result, int total, int spacing, char[] alphabet, char[] blank, int strikes)
        {
            Console.Clear();
            Console.WriteLine($"STRIKE: {strikes}/3");
            Console.WriteLine($"CONTESTANT: {contestants[winningContestant].firstname} {contestants[winningContestant].lastname}");
            Console.WriteLine($"CURRENT SPIN: {result,6:C0}");
            Console.WriteLine($"TOTAL SCORE:  {total,6:C0}");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("                 WHEEL OF FORTUNE");
            Console.WriteLine();
            for (int i = 0; i < spacing; i++)
            {
                Console.Write(" ");
            }
            for (int i = 0; i < blank.Length; i++)
            {
                Console.Write($"{blank[i]} ");
            }
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine();
            for (int i = 0; i < alphabet.Length; i++)
            {
                Console.Write(alphabet[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine();
            Console.Write("Enter a letter or solve the puzzle:");
            Console.WriteLine();
        }
        static void Game(int count, int winningContestant, ref Contestant[] contestants)
        {
            int strikes = 0;
            int result = WheelSpin();
            string answer = WordSelect();
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            int total = 0;
            bool solved = false;
            bool failed = false;
            char[] blank = new char[answer.Length];
            int spacing = 26 - blank.Length;
            for (int i = 0; i < blank.Length; i++)
            {
                blank[i] = '_';
            }
            while (solved == false && failed == false)
            {
                GameScreen(winningContestant, contestants, result, total, spacing, alphabet, blank, strikes);
                Console.Write(">");
                //Input = used for when redrawing screen. trueinput is used to match the guess to the letters.
                string input = Console.ReadLine();
                string trueinput = input.ToUpper().Trim();
                //Check to make sure user only enters one chr
                if (trueinput.Length == 1)
                {
                    char guess = trueinput[0];
                    int matches = 0;
                    if (guess >= 'A' && guess <= 'Z')
                    {
                        if (alphabet[guess - 'A'] == '-')
                        {
                            Console.WriteLine();
                            Console.WriteLine("You already guessed that letter!");
                            Console.WriteLine();
                            Console.WriteLine("ENTER to return");
                            Console.ReadLine();
                        }
                        else
                        {
                            for (int i = 0; i < answer.Length; i++)
                            {
                                if (answer[i] == guess)
                                {
                                    blank[i] = guess;
                                    matches++;
                                }
                            }
                            alphabet[guess - 'A'] = '-';
                            //Check for vowels and take away $250
                            if ("AEIOU".Contains(guess))
                            {
                                total -= 250;
                                GameScreen(winningContestant, contestants, result, total, spacing, alphabet, blank, strikes);
                                Console.WriteLine($">{input}");
                                Console.WriteLine();
                                Console.WriteLine("Vowel guessed! -$250");
                                Console.WriteLine();
                                if (matches > 0)
                                {
                                    Console.WriteLine($"You guessed {matches} letters correctly!");
                                    Console.WriteLine();
                                }
                                else
                                {
                                    Console.WriteLine($"Better luck next time! The word does not have that letter.");
                                    Console.WriteLine();
                                }
                                if (result >= 1000)
                                {
                                    Console.WriteLine("TIP: guess vowels when spinning a low prize.");
                                    Console.WriteLine();
                                }
                                Console.WriteLine("ENTER to continue");
                                Console.ReadLine();
                                result = WheelSpin();
                            }
                            //Change total, display win to user.
                            else
                            {
                                if (matches > 0)
                                {
                                    total += (result * matches);
                                    GameScreen(winningContestant, contestants, result, total, spacing, alphabet, blank, strikes);
                                    Console.WriteLine($">{input}");
                                    Console.WriteLine();
                                    if (result > 0)
                                    {
                                        Console.WriteLine($"Congratulations, you guessed {matches} letters correctly! {(result * matches):C0} was added to your winnings.");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"You guessed {matches} letters correctly and lost {(result * matches):C0}.");
                                        Console.WriteLine();
                                        Console.WriteLine("TIP: guess uncommon letters (like x, j and z) when you spin a penalty.");
                                    }
                                    Console.WriteLine();
                                    Console.WriteLine("ENTER to continue");
                                    Console.ReadLine();
                                    result = WheelSpin();
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine($"Better luck next time! The word does not have that letter.");
                                    Console.WriteLine();
                                    Console.WriteLine("ENTER to continue");
                                    Console.ReadLine();
                                    result = WheelSpin();
                                }
                            }
                        }
                    }
                    else
                    {
                        InvalidInput();
                    }
                }
                else if (trueinput.Length == answer.Length)
                {
                    //Check if user enters the whole word
                    if (trueinput == answer)
                    {
                        solved = true;
                        for (int i = 0; i < answer.Length; i++)
                        {
                            blank[i] = answer[i];
                        }
                    }
                    else
                    {
                        strikes++;
                        if (strikes == 3)
                        {
                            failed = true;
                        }
                    }
                }
                else
                {
                    InvalidInput();
                }
                solved = true;
                for (int i = 0; i < answer.Length; i++)
                {
                    if (blank[i] != answer[i])
                    {
                        solved = false;
                    }
                }
            }
            if (solved == true)
            {
                contestants[winningContestant].score += total;
                Console.WriteLine();
                Console.WriteLine("*         *          *               *            *            *              *               *        *");
                Console.WriteLine(" *                  *        *                 *                 *           *           *            *");
                Console.WriteLine($"  *  Congratulations {contestants[winningContestant].firstname}, you've won the wheel of fortune!  *");
                Console.WriteLine(" *                  *        *                 *                 *           *           *            *");
                Console.WriteLine("*         *          *               *            *            *              *               *        *");
                Console.WriteLine();
                Console.WriteLine($"{total:C0} has been added to your score! New final score: {contestants[winningContestant].score:C0}");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"Unfortunately {contestants[winningContestant].firstname}, you've lost the wheel of fortune...");
                Console.WriteLine();
                if (total > 0)
                {
                    Console.WriteLine($"You lost all your winnings of {total}.");
                }
                else
                {
                    contestants[winningContestant].score += total;
                    Console.WriteLine($"You did not escape the debt of {total}.");
                }
            }
            Console.WriteLine();
            Console.WriteLine("ENTER to continue");
            Console.ReadLine();
            MainMenu(count, contestants);
        }
        static void FinalistSelect(int count, Contestant[] contestants)
        {
            Console.Clear();
            Random rand = new Random();
            int[] finalists = new int[10];
            int finalistCount = 0;
            while (finalistCount < 10)
            {
                int candidate = rand.Next(count);
                bool duplicate = false;
                for (int i = 0; i < finalistCount && duplicate == false; i++)
                {
                    if (finalists[i] == candidate)
                    {
                        duplicate = true;
                    }
                }
                if (!duplicate)
                {
                    finalists[finalistCount] = candidate;
                    finalistCount++;
                }
            }
            Console.WriteLine("Drawing 10 Finalists...\n");
            for (int i = 0; i < 10; i++)
            {
                int c = finalists[i];
                Thread.Sleep(700);
                Console.WriteLine(
                    $"{i + 1,2}. " +
                    $"{contestants[c].firstname} " +
                    $"{contestants[c].lastname}");
            }
            Thread.Sleep(1400);
            ContestantSelect(count, finalists, contestants);
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
                    FinalistSelect(count, contestants);
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
                    Thread.Sleep(1400);
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
                "\nYou only have 3 chances to enter the complete word, if you run out of chances you lose and the game will be over!" +
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
        static void SaveContestants(int count, Contestant[] contestants)
        {
            using (StreamWriter writer = new StreamWriter("wheelOfFortune.txt"))
            {
                for (int i = 0; i < count; i++)
                {
                    writer.WriteLine(contestants[i].firstname);
                    writer.WriteLine(contestants[i].lastname);
                    writer.WriteLine(contestants[i].interest);
                    writer.WriteLine(contestants[i].score);
                }
            }
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