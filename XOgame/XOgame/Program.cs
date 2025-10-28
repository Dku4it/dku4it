using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Reflection;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
                string goA1;  //input Line number from user
                string goB1;  //input Column number from user
                byte goA2;     //Line number for use
                byte goB2;     //Line number for use
                bool parseResult;
                char turnNow = 'X'; //which player turns now

                char[,] marks = { { ' ', ' ', ' '}, { ' ', ' ', ' '}, { ' ', ' ', ' '} }; // Matrix of the game

            while (true)
            {
                // main screen drawing
                Console.Clear();
                Console.WriteLine("\n   ┌───────────────╖");
                Console.WriteLine(" ░░░░ The XO Game ░░░░ ");
                Console.WriteLine("   ╘═══════════════╝\n");

                Console.WriteLine($"     ┌───┬───┬───╖\n   0 ┤ {marks[0, 0]} │ {marks[0, 1]} │ {marks[0, 2]} ║\n     ├───┼───┼───╢\n   1 ┤ {marks[1, 0]} │ {marks[1, 1]} │ {marks[1, 2]} ║\n     ├───┼───┼───╢\n   2 ┤ {marks[2, 0]} │ {marks[2, 1]} │ {marks[2, 2]} ║\n     ╘═╤═╧═╤═╧═╤═╝\n       0   1   2 ");

                
                // Input from user a cell cordinates to draw his sign there
                do
                {

                    Console.Write($"\nPlayer {turnNow} it's Your turn.\nEnter number of Line: ");
                    do
                    {
                        goA1 = Console.ReadLine();
                        parseResult = byte.TryParse(goA1, out goA2);
                        if (!parseResult || goA2 > 2)
                        {
                            Console.Write("Wrong number. Enter number of Line again please: ");
                        };
                    } while (!parseResult || goA2 > 2);


                    Console.Write($"Enter number of Column: ");
                    do
                    {
                        goB1 = Console.ReadLine();
                        parseResult = byte.TryParse(goB1, out goB2);
                        if (!parseResult || goB2 > 2)
                        {
                            Console.Write("Wrong number. Enter number of Column again please: ");
                        };
                    } while (!parseResult || goB2 > 2);


                    // Checking the cell for emptiness
                    if (marks[goA2, goB2] != ' ')
                    {
                        Console.Write("Selected cell is not Empty. Try again\n");
                    };


                } while (marks[goA2, goB2] != ' ');  // if selected cell is not empty - go to input again


                // draw Player's sign to selected cell
                marks[goA2, goB2] = turnNow;


                // main screen drawing after current turn
                Console.Clear();
                Console.WriteLine("\n   ┌───────────────╖");
                Console.WriteLine(" ░░░░ The XO Game ░░░░ ");
                Console.WriteLine("   ╘═══════════════╝\n");

                Console.WriteLine($"     ┌───┬───┬───╖\n   0 ┤ {marks[0, 0]} │ {marks[0, 1]} │ {marks[0, 2]} ║\n     ├───┼───┼───╢\n   1 ┤ {marks[1, 0]} │ {marks[1, 1]} │ {marks[1, 2]} ║\n     ├───┼───┼───╢\n   2 ┤ {marks[2, 0]} │ {marks[2, 1]} │ {marks[2, 2]} ║\n     ╘═╤═╧═╤═╧═╤═╝\n       0   1   2 ");


                // Checking: Is some player has a winning combination after current turn
                string[] WinCombination = new string[8];
                WinCombination[0] = "" + marks[0, 0] + marks[0, 1] + marks[0, 2];
                WinCombination[1] = "" + marks[1, 0] + marks[1, 1] + marks[1, 2];
                WinCombination[2] = "" + marks[2, 0] + marks[2, 1] + marks[2, 2];
                WinCombination[3] = "" + marks[0, 0] + marks[1, 0] + marks[2, 0];
                WinCombination[4] = "" + marks[0, 1] + marks[1, 1] + marks[2, 1];
                WinCombination[5] = "" + marks[0, 2] + marks[1, 2] + marks[2, 2];
                WinCombination[6] = "" + marks[0, 0] + marks[1, 1] + marks[2, 2];
                WinCombination[7] = "" + marks[0, 2] + marks[1, 1] + marks[2, 0];

                if (WinCombination.Contains("XXX"))
                {
                    Console.WriteLine("\n\n░░░──────────────────────╖\n░░ Player X - Winner!!! ░░\n░░  Congratulations!!!  ░░\n╘══════════════════════░░░\n\nPress Enter to play again...\n(the winner will go second)");
                    goA1 = Console.ReadLine();

                    // reset all data to new game
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            marks[i, j] = ' ';
                        }
                    }
                }
                else
                {
                    if (WinCombination.Contains("OOO"))
                    {
                        Console.WriteLine("\n\n░░░──────────────────────╖\n░░ Player O - Winner!!! ░░\n░░  Congratulations!!!  ░░\n╘══════════════════════░░░\n\nPress Enter to play again...\n(the winner will go second)");
                        goA1 = Console.ReadLine();

                        // reset all data to new game
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                marks[i, j] = ' ';
                            }
                        }
                    }
                    else
                    {
                        if (!marks.Cast<char>().Contains(' ')) // not easy checking: is there an empty cells to next turn
                        {
                            Console.WriteLine($"\n\n░░░░░░░ Game Over ░░░░░░░░\n The game ended in a draw\n░░░░░ No one winned ░░░░░░\n\nPress Enter to play again...");
                            goA1 = Console.ReadLine();


                            // reset all data to new game
                            turnNow = 'O';
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    marks[i, j] = ' ';
                                }
                            }
                        }
                    }
                }


                // Passing the turn to the next player
                if (turnNow == 'X') { turnNow = 'O'; } else { turnNow = 'X'; }
            }
        }
    }
}

