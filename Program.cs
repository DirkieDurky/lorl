using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace lorl;

internal class Program
{
    public static List<Int32> Indices = new();
    public static Process process = new();

    private static void Main(String[] args)
    {
        while (true)
        {
            String? input = Console.ReadLine();
            if (String.IsNullOrEmpty(input))
            {
                Console.WriteLine("no");
                continue;
            }

            for (Int32 i = 0; i < input.Length; i++)
            {
                Char c = input[i];
                if (c is 'I' or 'l') Indices.Add(i);
            }

            if (Indices.Count > 0)
            {
                ReplaceIL(input, 0);
                Indices.Clear();
            }
            else
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = input;
                process.Start();
            }
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
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = str;
            process.Start();
        }
    }
}