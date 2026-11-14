using Microsoft.VisualBasic;
using System.IO;

namespace DominikAickenSpinTheWheel
{
    public struct Contestant            //Structure for contestants
    {
        public string firstname;
        public string lastname;
        public string interest;
        public int score;
    }
    public class Program
    {
        static Random rand = new Random();      //Set up random variable
        static void Main(string[] args)
        {
            int count = 23;
            StreamReader sr = new StreamReader("wheelOfFortune.txt");
            //Assign contestants
            Contestant[] contestants = new Contestant[23];
            {
                for (int i = 0; i < contestants.Length; i++)
                {
                    contestants[i].firstname = sr.ReadLine();
                    contestants[i].lastname = sr.ReadLine();
                    contestants[i].interest = sr.ReadLine();
                    contestants[i].score = Convert.ToInt32(sr.ReadLine());
                }
                sr.Close();
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
                    if (contestants[j].lastname.CompareTo(contestants[j + 1].lastname) > 0)  //Check if a contestant's (j) last name is in alphabetical order compared to the next contestant in the array (j+1)
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
            int winner = rand.Next(10);     //Pre-selects a winner
            int position;
            int delay = 50;                 //Determines the speed the wheel spins
            int spins = 40 + winner;
            for (int spin = 0; spin <= spins; spin++)
            {
                position = spin % 10;       //Makes sure the wheel always lands on the winner, using modulo
                Console.Clear();
                Console.WriteLine("SELECTING TODAY'S CONTESTANT...\n");
                for (int i = 0; i < 10; i++)                                                                    //Print out the finalists
                {
                    int c = finalists[i];
                    if (i == position)      //Checks which name to point to
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"> {contestants[c].firstname} {contestants[c].lastname} <");         //Colors in the user's name of which the arrow is pointing to
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {contestants[c].firstname} {contestants[c].lastname}");           //Static: Arrow not pointing to it
                    }
                }
                Thread.Sleep(delay);
                if (delay < 250)
                    delay += 5;                         //Delay increases, makes the wheel looking like it's slowing down
            }
            int player = finalists[winner];             //Player = whoever won the spin
            Thread.Sleep(700);
            Console.WriteLine();
            Console.WriteLine($"{contestants[player].firstname} {contestants[player].lastname} has been selected!");
            Thread.Sleep(1400);
            Game(count, player, ref contestants);
        }
        static int WheelSpin()      //Spins wheel, returns prize
        {
            int[] wheel ={ 5000, 300, 1500, 900,4000, 800, 2500, -2000, 600, 3500, 1200, 400, 4500, 700, 3000, 1700,500, 2200, -3000, 1000, 1800, 750, 2000, 1300 };
            int prize = rand.Next(24);      //Pre-select prize
            int delay = 40;
            int spins = 24 + prize;
            for (int spin = 0; spin <= spins; spin++)
            {
                int position = spin % 24;
                Console.Clear();
                Console.WriteLine("SPINNING THE WHEEL\n");
                Console.WriteLine("        V");
                Console.WriteLine();
                for (int row = -2; row <= 2; row++)                         //Generates 5 rows, row 0 is the middle row
                {
                    int index = (position + row + 24) % 24;
                    if (row == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;      //Color in the prize the arrow points to
                        Console.WriteLine($"   > {wheel[index],-7:C0} < ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"     ${wheel[index]}");         //Static
                    }
                }
                Thread.Sleep(delay);
                if (delay < 160)
                    delay += 6;         //Delay increases to make the wheel look like it's slowing down
            }
            int result = wheel[prize];
            if (result > 0) //Good prze
            { 
                Console.ForegroundColor = ConsoleColor.Green; 
            }
            else    //Penalty
            { 
                Console.ForegroundColor = ConsoleColor.Red; 
            }
            Thread.Sleep(700);
            Console.Clear();
            Console.WriteLine($"WHEEL RESULT: ${result}:0C");
            Console.ResetColor();
            Thread.Sleep(2000);
            return result;
        }
        static string WordSelect()      //Randomly selects word
        {
            //List of 20 words (with the help of CoPilot):
            string[] words = { "CHOCOLATES","PLAYGROUND","WATERFALLS","SUNFLOWERS","TELESCOPES","HEADPHONES","ALLIGATORS","LIGHTHOUSE","BACKPACKER","FOOTBALLER","SKATEBOARD","BOOKSTORES","RAINFOREST","MICROWAVES","PINEAPPLES","ADVENTURES","FIREFIGHTER","BASKETBALL","CENTIPEDES","UNDERGROUND" };
            string answer = words[rand.Next(words.Length)];
            return answer;
        }
        static void GameScreen(int player, Contestant[] contestants, int result, int total, int spacing, char[] alphabet, char[] blank, int strikes)        //Draws game screen, for redrawing in-game
        {
            Console.Clear();
            Console.WriteLine($"STRIKE: {strikes}/3");
            Console.WriteLine($"CONTESTANT: {contestants[player].firstname} {contestants[player].lastname}");
            Console.WriteLine($"CURRENT SPIN: {result,6:C0}");
            Console.WriteLine($"TOTAL WINNINGS:  {total,6:C0}");
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
                Console.Write($"{alphabet[i]} ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine();
            Console.Write("Enter a letter or solve the puzzle:");
            Console.WriteLine();
        }
        static void Game(int count, int player, ref Contestant[] contestants)           //Main game
        {
            int strikes = 0;
            int result = WheelSpin();
            string answer = WordSelect();
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            int total = 0;
            bool solved = false;
            bool failed = false;
            char[] blank = new char[answer.Length];
            int spacing = (41 - blank.Length) / 2;
            for (int i = 0; i < blank.Length; i++)
            {
                blank[i] = '_';
            }
            while (solved == false && failed == false)
            {
                GameScreen(player, contestants, result, total, spacing, alphabet, blank, strikes);
                Console.Write(">");
                //Input = used for when redrawing screen. trueinput is used to match the guess to the letters.
                string input = Console.ReadLine();
                string trueinput = input.ToUpper().Trim();
                //Check to make sure user only enters one chr
                if (trueinput.Length == 1)      //Check for 1 chr
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
                            Console.Write("ENTER to return");
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
                            alphabet[guess - 'A'] = '-';      //Changes the used letter to a '-'
                            if ("AEIOU".Contains(guess))        //Checks for A, E, I, O, U
                            {
                                total -= 250;           //$250 penalty
                                solved = true;
                                for (int i = 0; i < answer.Length; i++)     //Check each letter in the word, if it's blank, solved is set back to false
                                {
                                    if (blank[i] != answer[i])
                                    {
                                        solved = false;
                                    }
                                }
                                if (solved == false)
                                {
                                    GameScreen(player, contestants, result, total, spacing, alphabet, blank, strikes);      //Redraw screen
                                    Console.WriteLine($">{input}");
                                    Console.WriteLine();
                                    Console.WriteLine("Vowel guessed! -$250");
                                    Console.WriteLine();
                                    if (matches > 0)
                                    {
                                        Console.WriteLine($"You guessed {matches} letter(s) correctly!");
                                        Console.WriteLine();
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Better luck next time! The word does not have that letter.");
                                        Console.WriteLine();
                                    }
                                    if (result >= 1000)         //If prize is large
                                    {
                                        Console.WriteLine("TIP: guess vowels when spinning a low prize.");
                                        Console.WriteLine();
                                    }
                                    Console.Write("ENTER to continue");
                                    Console.ReadLine();
                                    result = WheelSpin();       //Respin for prize
                                }
                            }
                            else             //Not a vowel:
                            {
                                if (matches > 0)
                                {
                                    total += (result * matches);        //Multiply per occurence
                                    solved = true;
                                    for (int i = 0; i < answer.Length; i++)
                                    {
                                        if (blank[i] != answer[i])
                                        {
                                            solved = false;
                                        }
                                    }
                                    if (solved == false)
                                    {
                                        GameScreen(player, contestants, result, total, spacing, alphabet, blank, strikes);      //Redraw screen
                                        Console.WriteLine($">{input}");
                                        Console.WriteLine();
                                        if (result > 0)     //Check to make sure the spin isn't a penalty
                                        {
                                            Console.WriteLine($"Congratulations, you guessed {matches} letter(s) correctly! {(result * matches):C0} was added to your winnings.");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"You guessed {matches} letter(s) correctly and lost {(result * matches) * -1:C0}.");
                                            Console.WriteLine();
                                            Console.WriteLine("TIP: guess uncommon letters (like x, j and z) when you spin a penalty.");
                                        }
                                        Console.WriteLine();
                                        Console.Write("ENTER to continue");
                                        Console.ReadLine();
                                        result = WheelSpin();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine($"Better luck next time! The word does not have that letter.");
                                    Console.WriteLine();
                                    Console.Write("ENTER to continue");
                                    Console.ReadLine();
                                    result = WheelSpin();       //Respin
                                }
                            }
                        }
                    }
                    else  
                    {
                        InvalidInput();
                    }
                }
                else if (trueinput.Length == answer.Length)     //Check if input and answer have the same length (This is specific to try and stop invalid inputs from being penalized)
                {
                    //Check if user enters the whole word
                    if (trueinput == answer)
                    {
                        total = (total + result);
                        solved = true;
                        for (int i = 0; i < answer.Length; i++)
                        {
                            blank[i] = answer[i];
                        }
                    }
                    else
                    {
                        strikes++;
                        if (strikes < 3)
                        {
                            GameScreen(player, contestants, result, total, spacing, alphabet, blank, strikes);
                            Console.Write($">{input}");
                            Console.WriteLine();
                            Console.WriteLine("You guessed incorrectly!");
                            Console.WriteLine();
                            Console.WriteLine($"You received a strike! Be careful, you only have {3 - strikes}/3 chance(s) left!");
                            Console.WriteLine();
                            Console.WriteLine("ENTER to continue");
                            Console.ReadLine();
                        }
                        else if (strikes == 3)      //Out!
                        {
                            failed = true;
                        }
                    }
                }
                else
                {
                    InvalidInput();
                }
                if (solved == true) //Win
                {
                    contestants[player].score += total;     //Add score to corresponding player
                    GameScreen(player, contestants, result, total, spacing, alphabet, blank, strikes);
                    Console.Write($">{input}");
                    Console.WriteLine();
                    Console.WriteLine("*         *          *               *            *            *              *               *        *");
                    Console.WriteLine(" *                  *        *                 *                 *           *           *            *");
                    Console.WriteLine($"  *      *      *  Congratulations {contestants[player].firstname}, you've won the wheel of fortune!       *      *   *");
                    Console.WriteLine(" *                  *        *                 *                 *           *           *            *");
                    Console.WriteLine("*         *          *               *            *            *              *               *        *");
                    Console.WriteLine($"{total:C0} has been added to your balance! New final balance: {contestants[player].score:C0}");
                }
                else if (failed == true)    //Lose
                {
                    result = 0;
                    int temp = total;
                    if (total > 0) { total = 0; }
                    GameScreen(player, contestants, result, total, spacing, alphabet, blank, strikes);
                    Console.Write($">{input}");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine($"Unfortunately {contestants[player].firstname}, you've lost the wheel of fortune...");
                    Console.WriteLine();
                    if (temp >= 0)
                    {
                        Console.WriteLine($"You lost all your winnings of {temp:C0}.");
                    }
                    else
                    {
                        contestants[player].score += temp;      //Take away score (if negative)
                        Console.WriteLine($"You did not escape the debt of {(temp*-1):C0}. New final balance: {contestants[player].score:C0}");
                    }
                }
            }
            SaveContestants(count, contestants);
            Console.WriteLine();
            Console.Write("ENTER to continue");
            Console.ReadLine();
        }
        static void FinalistSelect(int count, Contestant[] contestants)
        {
            Console.Clear();
            int[] finalists = new int[10];
            int finalistCount = 0;
            while (finalistCount < 10)
            {
                int candidate = rand.Next(count);
                bool duplicate = false;
                for (int i = 0; i < finalistCount && duplicate == false; i++)       //Check for duplicates
                {
                    if (finalists[i] == candidate)
                    {
                        duplicate = true;
                    }
                }
                if (duplicate == false)
                {
                    finalists[finalistCount] = candidate;   //Add candidate to array of finalists
                    finalistCount++;
                }
            }
            Console.WriteLine("Drawing 10 Finalists...\n");
            for (int i = 0; i < 10; i++)        //Write out finalists
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
            bool quit = false;
            while (quit == false)
            {
                Console.Clear();
                Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
                Console.WriteLine();
                Console.WriteLine("[ Main Menu ] ".PadLeft(15));
                Console.WriteLine();
                Console.WriteLine("1. Play\n2. Instructions\n3. Contestants\n4. Leaderboard\n0. Quit");
                Console.WriteLine();
                Console.Write(">");
                //Read key pressed, this means the user doesn't have to press enter again
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                char keyChar = keyInfo.KeyChar;
                switch (keyChar)
                {
                    case '1':
                        FinalistSelect(count, contestants);
                        break;
                    case '2':
                        Instructions(count);
                        break;
                    case '3':
                        ContestantMenu(count, contestants);
                        break;
                    case '4':
                        Leaderboard(contestants, count);
                        break;
                    case '0':
                        Console.WriteLine();
                        Console.WriteLine("Thank you for playing!");
                        Thread.Sleep(1400);
                        quit = true;
                        break;
                    default:
                        InvalidInput();
                        break;
                }
            }
        }
        static void Leaderboard(Contestant[] contestants, int count)
        {
            Console.Clear();
            Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
            Console.WriteLine();
            Console.WriteLine("[ Main Menu ] ".PadLeft(15));
            Console.WriteLine();
            Console.WriteLine("#  First Name       Last Name                 Interest                       Bank");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            ScoreSort(ref contestants, count);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(
                    $"{i + 1,-3}" +
                    $"{contestants[i].firstname,-16} {contestants[i].lastname,-26}" +
                    $"{contestants[i].interest,-31}" +
                    $"{contestants[i].score,-7:C0}");
            }
            BubbleSort(ref contestants, count);
            Console.WriteLine();
            Console.Write("ENTER to return");
            Console.ReadLine();
        }
        static void ScoreSort(ref Contestant[] contestants, int count)     //Sorts by score for leaderboard
        {
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - 1 - i; j++)
                {
                    if (contestants[j].score.CompareTo(contestants[j + 1].score) < 0)
                    {
                        Contestant temp = contestants[j];
                        contestants[j] = contestants[j + 1];
                        contestants[j + 1] = temp;
                    }
                }
            }
        }
        static void Instructions(int count)
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
                "\nIf you guess a vowel, your winnings is reduced by a flat rate of $250." +
                "\nIf you guess a letter the word does not contain, you will lose your current spin's prize and spin again." +
                "\nIf you enter the entire word early, you will not receive anymore spins, but you will not be penalized by vowels and still receive that spin's prize!." +
                "\nYou only have 3 chances to enter the complete word, if you run out of chances you lose and the game will be over!" +
                "\n\nKeep spinning until the word is complete!");
            Console.WriteLine();
            Console.Write("ENTER to return");
            Console.ReadLine();
        }
        static void ContestantMenu(int count, Contestant[] contestants)
        {
            bool quit = false;
            while (quit == false)
            {
                BubbleSort(ref contestants, count);     //Re-sort contestants
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
                        quit = true;
                        break;
                    default:
                        InvalidInput();
                        break;
                }
            }
        }
        static void EditContestant(int count, Contestant[] contestants)
        {
            bool quit = false;
            bool found = false;
            while (quit == false && found == false)
            {
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
                    quit = true;
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (found == false && (contestants[i].firstname.ToLower().Trim() == wanted || contestants[i].lastname.ToLower().Trim() == wanted))      //Check: Not already found and there is a match between 'wanted' and first/last name
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
                    }
                }
            }
        }
        static void ChosenContestant(int count, int chosen, Contestant[] contestants)
        {
            bool quit = false;
            while (quit == false)
            {
                Console.Clear();
                Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
                Console.WriteLine();
                Console.WriteLine("[ Contestant Menu ] ".PadLeft(15));
                Console.WriteLine();
                Console.WriteLine($"Selected contestant: {contestants[chosen].firstname} {contestants[chosen].lastname}");
                Console.WriteLine();
                Console.WriteLine($"Interest: {contestants[chosen].interest,-30} Balance: {contestants[chosen].score:C0}");
                Console.WriteLine();
                Console.WriteLine("1. Edit First Name\n2. Edit Last Name\n3. Edit Interest\n4. Reset Balance\n0. Back");
                Console.WriteLine();
                Console.Write(">");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                char keyChar = keyInfo.KeyChar;
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
                        quit = true;
                        break;
                    default:
                        InvalidInput();
                        break;

                }
            }
        }
        static void ResetScore(int count, int chosen, ref Contestant[] contestants)
        {
            contestants[chosen].score = 0;
            Console.WriteLine();
            Console.Write("Score reset to 0! ENTER to return");
            Console.ReadLine();
            SaveContestants(count, contestants);
        }
        static void EditInterest(int count, int chosen, ref  Contestant[] contestants)
        {
            bool quit = false;
            while (quit == false)
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
                    quit = true;
                }
                else if (newinterest != null && newinterest.Length > 30)
                {
                    Console.WriteLine();
                    Console.Write("New interest cannot exceed 30 characters. ENTER to return");
                    Console.ReadLine();
                }
                else
                {
                    contestants[chosen].interest = newinterest;
                    SaveContestants(count, contestants);
                    quit = true;
                }
            }
        }
        static void EditFirstName(int count, int chosen, ref Contestant[] contestants)
        {
            bool quit = false;
            while (quit == false)
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
                    quit = true;
                }
                else if (newname != null && newname.Length > 15)
                {
                    Console.WriteLine();
                    Console.Write("New first name cannot exceed 15 characters. ENTER to return");
                    Console.ReadLine();
                }
                else
                {
                    contestants[chosen].firstname = newname;
                    SaveContestants(count, contestants);
                    quit = true;
                }
            }
        }
        static void EditLastName(int count, int chosen, ref Contestant[] contestants)
        {
            bool quit = false;
            while (quit == false)
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
                    quit = true;
                }
                else if (newname != null && newname.Length > 25)
                {
                    Console.WriteLine();
                    Console.Write("New last name cannot exceed 25 characters. ENTER to return");
                    Console.ReadLine();
                }
                else
                {
                    contestants[chosen].lastname = newname;
                    SaveContestants(count, contestants);
                    quit = true;
                }
            }
        }
        static void ShowContestants(int count, Contestant[] contestants)
        {
            Console.Clear();
            Console.WriteLine("!*!*!*!*! Wheel of Fortune Game Show !*!*!*!*!");
            Console.WriteLine();
            Console.WriteLine("[ Contestant Menu ] ".PadLeft(15));
            Console.WriteLine();
            Console.WriteLine("#  First Name       Last Name                 Interest                       Bank");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            for (int i = 0; i < count; i++)             //Writes out contestants
            {
                Console.WriteLine(
                    $"{i + 1,-3}" +
                    $"{contestants[i].firstname,-16} {contestants[i].lastname,-26}" +
                    $"{contestants[i].interest,-31}" +
                    $"{contestants[i].score,-7:C0}");
            }
            Console.WriteLine();
            Console.Write("ENTER to return");
            Console.ReadLine();
        }
        static void SaveContestants(int count, Contestant[] contestants)
        {
            StreamWriter sw = new StreamWriter("wheelOfFortune.txt");
            for (int i = 0; i < count; i++)
            {
                sw.WriteLine(contestants[i].firstname);
                sw.WriteLine(contestants[i].lastname);
                sw.WriteLine(contestants[i].interest);
                sw.WriteLine(contestants[i].score);
            }
            sw.Close();
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
//KNOWN BUGS: If two contestants have the exact same first and last name, searching for them becomes impossible until you change the names of one. Trying to input words/characters during an animation may cause issues.
//EXTRA FEATURES: Editable first/last name (restricted with a character limit, as well as editable interest), resettable score and instructions. Player can guess the full word to end the game early, with a strike system to punish incorrect guesses which can be used to escape vowel penalties.
//NOTE: 'count' is left over from an extra feature I planned to add where you could add contestants in-game, I didn't add this in the end and cleaning it out of the code was not worth the time.