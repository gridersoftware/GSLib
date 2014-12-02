using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Extensions
{
    public static class ConsoleX
    {
        public delegate bool TryValidate<T>(string input, out T result);

        /// <summary>
        /// Determines if the given key is associated with a printable character.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns>Returns true if the key is associated with a printable character. Otherwise, returns false.</returns>
        public static bool IsPrintableKey(ConsoleKeyInfo key)
        {
            ConsoleKey[] nonprinting = new ConsoleKey[] {
                ConsoleKey.Applications, ConsoleKey.Attention, ConsoleKey.Backspace, ConsoleKey.BrowserBack, ConsoleKey.BrowserFavorites, ConsoleKey.BrowserForward, 
                ConsoleKey.BrowserHome, ConsoleKey.BrowserRefresh, ConsoleKey.BrowserSearch, ConsoleKey.BrowserStop, ConsoleKey.Clear, ConsoleKey.CrSel, ConsoleKey.Delete,
                ConsoleKey.DownArrow, ConsoleKey.End, ConsoleKey.Enter, ConsoleKey.EraseEndOfFile, ConsoleKey.Escape, ConsoleKey.Execute, ConsoleKey.ExSel, ConsoleKey.F1, 
                ConsoleKey.F10, ConsoleKey.F11, ConsoleKey.F12, ConsoleKey.F13, ConsoleKey.F14, ConsoleKey.F15,
                ConsoleKey.F16, ConsoleKey.F17,ConsoleKey.F18,ConsoleKey.F19,ConsoleKey.F2,ConsoleKey.F20,ConsoleKey.F21,ConsoleKey.F22,ConsoleKey.F23,ConsoleKey.F24,ConsoleKey.F3,
                ConsoleKey.F4,ConsoleKey.F5,ConsoleKey.F6,ConsoleKey.F7,ConsoleKey.F8,ConsoleKey.F9,ConsoleKey.Help,ConsoleKey.Home,ConsoleKey.Insert, ConsoleKey.LaunchApp1,
                ConsoleKey.LaunchApp2,ConsoleKey.LaunchMail, ConsoleKey.LaunchMediaSelect,ConsoleKey.LeftArrow,ConsoleKey.LeftWindows,ConsoleKey.MediaNext,ConsoleKey.MediaPlay,
                ConsoleKey.MediaPrevious,ConsoleKey.MediaStop,ConsoleKey.Packet,ConsoleKey.PageDown,ConsoleKey.PageUp,ConsoleKey.Pause,ConsoleKey.Play,ConsoleKey.Print,
                ConsoleKey.PrintScreen,ConsoleKey.Process,ConsoleKey.RightArrow,ConsoleKey.RightWindows,ConsoleKey.Select,ConsoleKey.Separator,ConsoleKey.Sleep,ConsoleKey.VolumeDown,
                ConsoleKey.VolumeMute,ConsoleKey.VolumeUp,ConsoleKey.Zoom};

            if (nonprinting.Contains(key.Key)) return false;
            else return true;
        }

        /// <summary>
        /// Prompts the user for input, and validates the input, then returns the input.
        /// </summary>
        /// <param name="prompt">Prompt string</param>
        /// <param name="invalidMessage">Message to print if input is not valid</param>
        /// <param name="validityCheck">Func delegate for checking input validity</param>
        /// <returns>Returns valid user input string.</returns>
        /// <remarks>This method loops until the input passes the validityCheck.</remarks>
        public static string PromptUser(string prompt, string invalidMessage, Func<string, bool> validityCheck)
        {
            string input;
            bool valid;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                valid = validityCheck(input);
                if (!valid) Console.Write(invalidMessage);
            } while (!valid);
            return input;
        }

        public static T PromptUser<T>(string prompt, string invalidMessage, TryValidate<T> validityCheck)
        {
            string input;
            bool valid;
            T result;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                valid = validityCheck(input, out result);
            }
            while (!valid);

            return result;
        }

        /// <summary>
        /// Prompts the user for the answer to a yes/no question.
        /// </summary>
        /// <param name="prompt">Prompt string (without "(y/n)")</param>
        /// <returns>Returns true for yes, false for no.</returns>
        public static bool PromptYesNo(string prompt)
        {
            string msg = PromptUser(prompt + " (y/n)", "Must be answered with \'y\' or \'n\'.",
                delegate(string s)
                {
                    if (s.ToLower() == "y" | s.ToLower() == "n") return true;
                    else return false;
                });
            if (msg.ToLower() == "y") return true;
            else return false;
        }

        /// <summary>
        /// Prompts the user for a password using the given character as a mask.
        /// </summary>
        /// <param name="prompt">Prompt to present to the user.</param>
        /// <param name="invalidMessage">Message to print if the password is incorrect.</param>
        /// <param name="mask">Character to use in place of printable characters.</param>
        /// <param name="validityCheck">Function to run to test for a valid password.</param>
        /// <returns>Returns a string containing the password text.</returns>
        public static string PromptPassword(string prompt, string invalidMessage, char mask, Func<string, bool> validityCheck)
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo key;

            do
            {
                Console.Write(prompt);
                do
                {
                    key = Console.ReadKey(true);

                    if (ConsoleX.IsPrintableKey(key))
                    {
                        Console.Write(mask);
                        password.Append(key.KeyChar);
                    }
                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        if (password.Length > 0)
                        {
                            Console.Write("\b \b");
                            password.Remove(password.Length - 1, 1);
                        }
                        else
                        {
                            Console.Beep();
                        }
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine("\n");
                    }
                    else
                    {
                        Console.Beep();
                    }
                } while (key.Key != ConsoleKey.Enter);

                if (!validityCheck(password.ToString()))
                {
                    Console.Write(invalidMessage);
                    password.Clear();
                }

            } while (password.ToString() == "");

            return password.ToString();
        }
    }
}
