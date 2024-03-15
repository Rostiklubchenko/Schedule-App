using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    public class Reader
    {
        public string currentDay;
        //public static void Read()
        //{
        //    string filePath = "D:/Study/123/ConsoleApp9/schedule.txt";
        //    try
        //    {
        //        string[] lines = File.ReadAllLines(filePath);

        //        foreach (string line in lines)
        //        {
        //            if (!string.IsNullOrWhiteSpace(line))
        //            {
        //                if (IsDayOfWeek(line))
        //                {
        //                    currentDay = line;
        //                }
        //                else
        //                {
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //    }
        //}
        //public string[][][] Read()
        //{
        //    List<string> list = new List<string>();
        //    string[][][] strings = new string[][][];
        //    string filePath = "D:/Study/123/WinFormsApp1/schedule.txt";
        //    try
        //    {
        //        string[] lines = File.ReadAllLines(filePath);

        //        int day = 0;
        //        int pair = 0;
        //        for (int i = 0; i < lines.Length; i++)
        //        {
        //            string line = lines[i];
        //            if (line == ";")
        //            {
        //                day++;
        //                pair = 0;
        //                continue;
        //            }
        //            string[] parts = line.Split('|');
        //            for (int l = 0; l < 3; l++)
        //            {
        //                strings[day][pair][l] = parts[l];
        //            }
        //            pair++;
        //        }
        //    }
        //    catch (Exception) { }
        //    return strings;
        //}



        public string[][][] Read()
        {
            string[][][] strings = null;
            string filePath = "D:/Study/123/WinFormsApp1/schedule.txt";
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                int day = 0;
                int pair = 0;

                strings = new string[5][][];

                for (int i = 0; i < lines.Length; i++)
                {
                    strings[i] = new string[5][];
                    for (int j = 0; j < 5; j++)
                    {
                        strings[i][j] = new string[3];
                    }
                }

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (line == ";")
                    {
                        day++;
                        pair = 0;
                        continue;
                    }
                    string[] parts = line.Split('|');
                    for (int l = 0; l < 3; l++)
                    {
                        strings[day][pair][l] = parts[l];
                    }
                    pair++;
                }

                for (pair = 0; pair < 5; pair++)
                {
                    for (int l = 0; l < 3; l++)
                    {
                        strings[5][pair][l] = "";
                    }
                }
            }
            catch (Exception) { }
            return strings;
        }


        // Проверка, является ли строка днем недели
        static bool IsDayOfWeek(string line)
        {
            return new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" }
                .Contains(line.Trim());
        }
    }
}
