using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Text.RegularExpressions;


class Program
{

    private static object item;

    static void Main(string[] args)
    {
        string[] strMy;
        List<string> listMy = new List<string>();
        LinkedList<string> linkListMy = new LinkedList<string>();
        Dictionary<string, int> dictionary = new Dictionary<string, int>();

        //Задание 13.6.1

        string filePath = @"/Users/no1/Projects/hw13/Text1.txt";
        using (var streamReader = new StreamReader(filePath))
        {
            var str = streamReader.ReadToEnd().ToLower();
            str = new string(str.Where(c => !char.IsPunctuation(c)).ToArray());
            strMy = str.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }


        var beforeAdd = Stopwatch.StartNew();
        foreach (var text in strMy)
            listMy.Add(text);

        Console.WriteLine("List<T> вставляет за {0} - {1} c слов", beforeAdd.Elapsed.TotalMilliseconds, strMy.Length);


        linkListMy.AddFirst("word 1");
        beforeAdd = Stopwatch.StartNew();
        foreach (var text in strMy)
            linkListMy.AddAfter(linkListMy.First, text);
        Console.WriteLine("LinkedList<T> вставляет за {0} - {1} слов", beforeAdd.Elapsed.TotalMilliseconds, strMy.Length);

        //Задание 13.6.2
        filePath = @"/Users/no1/Projects/hw13/Text2.txt";

        using (var streamReader = new StreamReader(filePath))
        {
            var str = streamReader.ReadToEnd().ToLower();

            str = new string(str.Where(c => !char.IsPunctuation(c)).ToArray());

            strMy = str.Split(new char[] {' ','\n'}, StringSplitOptions.RemoveEmptyEntries);
        }


        var topMy = strMy.GroupBy(x => x)
            .Where(x => x.Count() > 1)
            .Select(x => new {Word = x.Key, Frequency = x.Count()});


        foreach (var elem in topMy)
        {
            if (elem.Frequency > 1)
            {
                dictionary.Add(elem.Word, elem.Frequency);
            }
        }


        var arraySorted = dictionary.OrderByDescending(n => n.Value).Take(10);
        int i = 1;
        foreach (var elem in arraySorted)
        {
            Console.WriteLine("{0} слово {1} встречается {2} раз", i, elem.Key, elem.Value);
            i++;
        }

        Console.ReadKey();
    }

}