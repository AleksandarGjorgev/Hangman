using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        List<string> hangmanStages = new List<string>
        {
 @" _______
 |     
 |
 |
 |
_|_",

 @" _______
 |     |
 |     O
 |
 |
_|_",

 @" _______
 |     |
 |     O
 |     |
 |
_|_",

 @" _______
 |     |
 |     O
 |    /|
 |
_|_",

 @" _______
 |     |
 |     O
 |    /|\
 |
_|_",

 @" _______
 |     |
 |     O
 |    /|\
 |    /
_|_ ",

 @" _______
 |     |
 |     O
 |    /|\
 |    / \
_|_"
        };
        List<string> winner = new List<string>
{
    " _o   ",
    "__|-  ",
    "   >  ",
    " o/   ",
    " |__  ",
    " |    ",
    "   \\o_",
    " __/  ",
    "   >  ",
    "      ",
    " \\__/o",
    " /  \\ ",
    " \\ /  ",
    "  |   ",
    " /o\\ ",
    "   __ ",
    "  /   ",
    " o|   ",
    " __o  ",
    "   |  ",
    "  <<  ",
    "  |o  ",
    "  /   ",
    " |    ",
    " \\__/o",
    "      ",
    "      ",
    "  |   ",
    "  o\\  ",
    "      ",
    "  o____",
    "  /    ",
    "       ",
    " _o   ",
    "   \\  ",
    "  <<  ",
    "  o/  ",
    "  |   ",
    " < \\  ",
    " ",
    " ",
    " ",
};
        List<string> vacation = new List<string>
        {
@"           ___   ____
        /' --;^/ ,-_\     \ | /
       / / --o\ o-\ \\   --(_)--
      /-/-/|o|-|\-\\|\\   / | \
       '`  ` |-|   `` '
             |-|
             |-|O
             |-(\,__
          ...|-|\--,\_....
      ,;;;;;;;;;;;;;;;;;;;;;;;;,.
~~,;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;,~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
~;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;,  ______   ---------   _____     ------",
 @" 
 O
/|\ 
/ \"
    };

        string filePath = "Words_text.txt";

        string[] lines = File.ReadAllLines(filePath);

        List<string> words = new List<string>();
        List<char> guessedWords = new List<char>();


        foreach (string line in lines)
        {
            string[] values = line.Split(',');
            foreach (string value in values)
            {
                words.Add(value);
            }
        }

        Random random = new Random();

        string chosenWord = words[random.Next(words.Count)]; 
        List<char> chosenWordList = new List<char>(); 
        foreach (char character in chosenWord) 
        {
            chosenWordList.Add(character);
        }

        List<char> guessedChars = new List<char>();

        char firstChar = char.ToLower(chosenWord[0]);
        guessedWords.Add(firstChar);
        string underlines = string.Join(" ", new string('_', chosenWord.Length-1).ToCharArray());
        string revealedWord = (chosenWord[0] + " " + underlines);

        for (int i = 1; i < chosenWordList.Count; i++)
        {
            if (chosenWordList[i] == firstChar)
            {
                revealedWord = revealedWord.Substring(0, 2 * i) + firstChar + revealedWord.Substring(2 * i + 1);
            }
        }


        int lives = 6;
        int activeLives = 6;




        Console.WriteLine("Welcome to hangman, a game where you need to guess the word. You have 6 lives.");

        while (true)
        {
            Console.WriteLine($"\n{hangmanStages[lives - activeLives]}\n" + "================");
            Console.WriteLine(revealedWord);
            Console.Write("\nGuessed characters: ");
            foreach (char character in guessedWords)
            {
                Console.Write(character + " ");
            }
            Console.Write("\n\nEnter a letter: ");
            char guess = char.Parse(Console.ReadLine());
            Console.Clear();
            if (!chosenWord.Contains(guess) && !guessedWords.Contains(guess))
            {
                activeLives--;
                Console.WriteLine("You guessed wrong!");
            }
            if (chosenWord.Contains(guess))
            {
                for (int i = 0; i < chosenWordList.Count; i++)
                {
                    if (chosenWordList[i] == guess)
                    {
                        revealedWord = revealedWord.Substring(0, 2 * i) + guess + revealedWord.Substring(2 * i + 1);
                    }
                }
                if (!guessedWords.Contains(guess))
                {
                    Console.WriteLine("You guessed right!");
                } 
            }
            if (guessedWords.Contains(guess))
            {
                Console.WriteLine("You already guessed this character, guess again. ");
            }
            else
            {
                guessedWords.Add(guess);
            }
            if (!revealedWord.Contains('_'))
            {
                Console.WriteLine("Congratulations you saved him! You guessed the word: " + chosenWord);
                Console.WriteLine(vacation[1]);
                Thread.Sleep(5000);
                Console.Clear();
                int counter = 0;
                foreach (string stickman in winner)
                {
                    if (counter % 3 == 0)
                    {
                        Console.WriteLine("LOOK AT HIM GO");
                    }
                    Console.WriteLine(stickman);
                    counter++;
                    if (counter % 3 == 0)
                    {
                        Thread.Sleep(400);
                        Console.Clear();
                    }
                }
                Thread.Sleep(500);
                Console.WriteLine(vacation[0] + "\n The end.");
                break;
            }
            if (activeLives == 0)
            {
                Console.Clear();
                Console.WriteLine("You killed him! The word was: {0}", chosenWord);
                Console.WriteLine($"\n{hangmanStages[lives - activeLives]}\n" + "================");
                break;
            }
        }
        Console.ReadKey();
    }
}
