using System;
using System.IO;
using static System.Console;

start();

bool keepruning = true;
ForegroundColor = ConsoleColor.White;
int i = 0;
char[] buffer = new char[50];
string str = string.Empty;

if (args.Length > 0)
    keepruning = recivecommand(args);

while (keepruning)
{
    var key = ReadKey(true);
    if (key.Key == ConsoleKey.Enter)
    {
        WriteLine();
        keepruning = recivecommand(str.TrimEnd('\0')
            .Split(' ', StringSplitOptions.RemoveEmptyEntries));
        buffer = new char[50];
        i = 0;
        WriteLine();
    }
    else if (key.Key == ConsoleKey.Backspace)
    {
        if (i == 0)
            continue;
        buffer[--i] = ' ';
    }
    else
    {
        if (i == buffer.Length - 1)
            continue;
        buffer[i++] = key.KeyChar;
    }
    str = string.Concat(buffer);
    CursorLeft = 0;
    show(str);
}

bool recivecommand(string[] command)
{
    if (command == null || command.Length == 0)
        return true;
    switch (command[0])
    {
        case "exit":
            return false;
        case "new":
            string name = "";
            newprj(name);
        break;
        default:
            error($"'{command[0]}' not is a valid command.");
        break;
    }

    return true;
}

void show(string s)
{
    foreach (var word in s.Replace('\0', ' ').Split(' '))
    {
        switch (word)
        {
            case "new":
            case "exit":
                printcolor(word, ConsoleColor.Green);
                break;
            default:
                Console.Write(word);
                break;
        }
        Console.Write(" ");
    }
}

void error(string s)
{
    printcolor(s, ConsoleColor.Red);
    Console.WriteLine();
}

void printcolor(string s, ConsoleColor color)
{
    ForegroundColor = color;
    Console.Write(s);
    ForegroundColor = ConsoleColor.White;
}

void start()
{
    printcolor("Visual Logic CLI", ConsoleColor.Blue);
    WriteLine();
    CursorVisible = false;
}

void newprj(string name)
{
    createprogram();
    createscproj(name);
}

void createprogram()
{
    var writter = File.CreateText($"Program.cs");
    writter.WriteLine("using VisualLogic;");
    writter.WriteLine("using VisualLogic.Elements;");
    writter.WriteLine("");
    writter.WriteLine("void logic(vRandomArray arr)");
    writter.WriteLine("{");
    writter.WriteLine("\tbool sorted = false;");
    writter.WriteLine("\twhile(!sorted)");
    writter.WriteLine("\t{");
    writter.WriteLine("\t\tsorted = true;");
    writter.WriteLine("\t\tfor (int i = 0; i < arr.Length; i++)");
    writter.WriteLine("\t\t{");
    writter.WriteLine("\t\t\tif (arr[i] < arr[i + 1])");
    writter.WriteLine("\t\t\t{");
    writter.WriteLine("\t\t\t\tvar temp = arr[i];");
    writter.WriteLine("\t\t\t\tvar arr[i] = arr[i + 1];");
    writter.WriteLine("\t\t\t\tvar arr[i + 1] = temp;");
    writter.WriteLine("\t\t\t}");
    writter.WriteLine("\t\t}");
    writter.WriteLine("\t}");
    writter.WriteLine("}");
    writter.Close();
}

void createscproj(string name)
{
    var writter = File.CreateText($"{name}.csproj");
    writter.WriteLine("<Project Sdk=\"Microsoft.NET.Sdk\">");
    writter.WriteLine();
    writter.WriteLine("\t<PropertyGroup>");
    writter.WriteLine("\t\t<OutputType>WinExe</OutputType>");
    writter.WriteLine("\t\t<TargetFramework>net6.0-windows</TargetFramework>");
    writter.WriteLine("\t\t<UseWindowsForms>true</UseWindowsForms>");
    writter.WriteLine("\t</PropertyGroup>");
    writter.WriteLine();
    writter.WriteLine("\t<ItemGroup>");
    writter.WriteLine("\t\t<PackageReference Include=\"VisualLogic\" Version=\"2.0.0\" />");
    writter.WriteLine("\t</ItemGroup>");
    writter.WriteLine();
    writter.WriteLine("</Project>");
}