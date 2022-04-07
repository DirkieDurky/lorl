using System.Diagnostics;
using System.IO.Pipes;
using System.Text;
using Microsoft.VisualBasic;

namespace lorl;

internal class Program
{
    public static List<Int32> Indices = new();
    public static Process Process = new();
    public static Boolean Browse;
    public static Boolean Hide;
    public static List<String> Results = new();

    private static void Main()
    {
        while (true)
        {
            String? input = Console.ReadLine();
            if (String.IsNullOrEmpty(input))
            {
                Console.WriteLine("no");
                continue;
            }
            String[] args = input.Split(' ');
            String str = args[0];

            if (args.Length is < 1)
            {
                Console.WriteLine("no");
                continue;
            }

            for (Int32 i=1;i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--browse":
                    {
                        Browse = true;
                    }
                    break;
                    case "--hide":
                    {
                        Hide = true;
                    }
                    break;
                }
            }

            for (Int32 i = 0; i < str.Length; i++)
            {
                Char c = str[i];
                if (c is 'I' or 'l') Indices.Add(i);
            }

            if (Indices.Count > 0)
            {
                ReplaceIL(str, 0);
            }
            else
            {
                Results.Add(str);
            }

            if (!Hide)
            {
                foreach (String result in Results)
                {
                    Console.WriteLine(result);
                }
            }

            if (Browse)
            {
                Boolean? answerGiven = null;
                if (Results.Count <= 1)
                {
                    answerGiven = true;
                }
                else
                {
                    Console.WriteLine($"{Results.Count} results. Are you sure you want to open those in your browser? (y/N)");
                    while (answerGiven == null)
                    {
                        input = Console.ReadLine();
                        if (String.IsNullOrEmpty(input) || String.Equals(input, "n", StringComparison.OrdinalIgnoreCase))
                        {
                            answerGiven = false;
                        }
                        else if (String.Equals(input, "y", StringComparison.OrdinalIgnoreCase))
                        {
                            answerGiven = true;
                            break;
                        }
                    }
                }

                if ((Boolean) answerGiven)
                {
                    foreach (String result in Results)
                    {
                        OpenUrl(result);
                    }
                }
                else
                {
                    Console.WriteLine("Aborted");
                }
            }

            Results.Clear();
            Indices.Clear();
        }
    }

    private static void ReplaceIL(String? str, Int32 index)
    {
        Replace(str, index, 'I');
        Replace(str, index, 'l');
    }

    private static void Replace(String? str, Int32 index, Char replacement)
    {
        StringBuilder sb = new(str);
        sb.Remove(Indices[index], 1);
        sb.Insert(Indices[index], replacement);
        str = sb.ToString();

        if (index < Indices.Count-1)
        {
            ReplaceIL(str, index+1);
        }
        else
        {
            Results.Add(str);
        }
    }

    private static void OpenUrl(String str)
    {
        try
        {
            Process.StartInfo.UseShellExecute = true;
            Process.StartInfo.FileName = str;
            Process.Start();
        }
        catch
        {
            Console.WriteLine($"Entry \"{str}\" not recognised as URL");
        }
    }
}