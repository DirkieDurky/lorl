using System;
using System.ComponentModel;
using System.Text;

namespace lorl;

internal class Program
{
    public static List<Int32> Indices = new();

    private static void Main(String[] args)
    {
        String input = Console.ReadLine();

        for (Int32 i = 0; i < input.Length; i++)
        {
            Char c = input[i];
            if (c is 'I' or 'l') Indices.Add(i);
        }

        Program program = new();
        program.ReplaceIL(input, 0);
    }

    private void ReplaceIL(String str, Int32 index)
    {
        Replace(str, index, 'I');
        Replace(str, index, 'l');
    }

    private void Replace(String str, Int32 index, Char replacement)
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
            Console.WriteLine(str);
        }
    }
}