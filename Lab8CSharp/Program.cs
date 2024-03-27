// See https://aka.ms/new-console-template for more information
using System.Text;
using System.Text.RegularExpressions;

//  За бажанням студента косольний проект або WinForm
// Зчитуємо текст з файлу
static void Task1()
{
    string filePath = "input1.txt";
    string text = File.ReadAllText(filePath);

    // Визначаємо формат географічних координат
    string pattern = @"\b(\d{1,2})[.,](\d{1,2})[.,](\d{1,2})\b";

    // Витягуємо всі підтексти, що відповідають формату географічних координат
    MatchCollection matches = Regex.Matches(text, pattern);

    // Записуємо результат у новий файл
    string outputFilePath = "output1.txt";
    using (StreamWriter writer = new StreamWriter(outputFilePath))
    {
        foreach (Match match in matches)
        {
            writer.WriteLine(match.Value);
        }
    }

    // Виводимо кількість знайдених координат
    Console.WriteLine($"Знайдено координат: {matches.Count}");

    // Замінюємо певні координати
    Console.WriteLine("Введіть координати, які потрібно замінити (у форматі xx.yy.zz):");
    string replaceCoords = Console.ReadLine();
    Console.WriteLine("Введіть нові координати (у форматі xx.yy.zz):");
    string newCoords = Console.ReadLine();

    string replacedText = Regex.Replace(text, replaceCoords, newCoords);

    // Записуємо замінений текст у файл
    File.WriteAllText(filePath, replacedText);

    Console.WriteLine("Готово! Заміни виконано. Результат збережено у файлі.");

}

static void Task2()
{
    // Читання вмісту файлу
    string inputFilePath = "input2.txt";
    string outputFilePath = "output2.txt";

    string text = File.ReadAllText(inputFilePath);

    // Заміна слів з префіксом to- на at-
    text = Regex.Replace(text, @"\bto", "at");

    // Видалення слів з закінченням -re, -nd та -less
    text = Regex.Replace(text, @"\b\w+?(re|nd|less)\b", "");

    // Запис у новий файл
    File.WriteAllText(outputFilePath, text);

    Console.WriteLine("Результат збережено у файлі output2.txt");
}

static void Task3()
{
    // Задання текстових файлів
    string inputFile1 = "text1.txt";
    string inputFile2 = "text2.txt";
    string outputFile = "result.txt";

    // Читання текстів з файлів
    string[] words1 = File.ReadAllText(inputFile1).Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
    string text2 = File.ReadAllText(inputFile2);

    // Вилучення входжень слів першого тексту з другого тексту
    string result = RemoveWordsFromText(words1, text2);

    // Запис результату у новий файл
    File.WriteAllText(outputFile, result);

    Console.WriteLine("Результат записано у файл " + outputFile);
}
static string RemoveWordsFromText(string[] words, string text)
{
    // Розділення другого тексту на слова
    string[] words2 = text.Split(new[] { ' ', ',', '.', '!', '?', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

    // Вилучення входжень слів першого тексту з другого тексту
    string result = string.Join(" ", words2.Except(words));

    return result;
}

static void Task4()
{
    // Генеруємо послідовність чисел
    double[] numbers = { 1.2, 3.4, 5.6, 7.8, 9.1 };

    // Записуємо числа у бінарний файл
    using (BinaryWriter writer = new BinaryWriter(File.Open("numbers.bin", FileMode.Create)))
    {
        foreach (double number in numbers)
        {
            writer.Write(number);
        }
    }

    double maxOddPositionValue = FindMaxOddPositionValue("numbers.bin");
    Console.WriteLine($"Максимальне значення на непарних позиціях: {maxOddPositionValue}");
}
static double FindMaxOddPositionValue(string fileName)
{
    double maxOddPositionValue = double.MinValue;
    bool oddPosition = false;

    using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
    {
        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            double number = reader.ReadDouble();

            if (oddPosition)
            {
                if (number > maxOddPositionValue)
                {
                    maxOddPositionValue = number;
                }
            }

            oddPosition = !oddPosition;
        }
    }

    return maxOddPositionValue;
}

static void Task5()
{
    string studentSurname = "Svereda";
    string directory1 = $@"D:\temp\{studentSurname}1";
    string directory2 = $@"D:\temp\{studentSurname}2";
    string file1 = $@"{directory1}\t1.txt";
    string file2 = $@"{directory1}\t2.txt";
    string file3 = $@"{directory2}\t1.txt";
    string file4 = $@"{directory2}\t2.txt";
    string file5 = $@"{directory2}\t3.txt";
    string allDirectory = @"D:\temp\ALL";

    // Create directories
    Directory.CreateDirectory(directory1);
    Directory.CreateDirectory(directory2);

    // Write to t1.txt and t2.txt
    File.WriteAllText(file1, "<Шевченко Степан Іванович, 2001> року народження, місце проживання <м. Суми>");
    File.WriteAllText(file2, "<Комар Сергій Федорович, 2000> року народження, місце проживання <м. Київ>");

    // Read from t1.txt and t2.txt
    string text1 = File.ReadAllText(file1);
    string text2 = File.ReadAllText(file2);

    // Write to t3.txt
    File.WriteAllText(file5, text1 + Environment.NewLine + text2);

    // Move t2.txt to directory2
    File.Move(file2, file4);

    // Copy t1.txt to directory2
    File.Copy(file1, file3);

    // Rename K2 directory to ALL
    Directory.Move(directory2, allDirectory);

    // Delete directory1
    Directory.Delete(directory1, true);

    string text3 = File.ReadAllText(@"D:\temp\ALL\t3.txt");
    // Display information
    Console.WriteLine("Created files:");
    Console.WriteLine($"t1.txt: {text1}");
    Console.WriteLine($"t2.txt: {text2}");
    Console.WriteLine($"t3.txt: {text3}");
    Console.WriteLine();

    // Display files in ALL directory
    Console.WriteLine("Files in ALL directory:");
    foreach (string file in Directory.GetFiles(allDirectory))
    {
        Console.WriteLine(Path.GetFileName(file));
    }
}


static void ShowMenu()
{
    string[] menuStrings =
    {
                "1. Task 1!",
                "2. Task 2!",
                "3. Task 3!",
                "4. Task 4!",
                "5. Task 5!"
            };
    int currentOprtion = 0;
    ConsoleKeyInfo keyInfo;
    int choice = 0;
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Lab#8");
        PrintMenu(menuStrings, currentOprtion);


        keyInfo = Console.ReadKey();
        if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)
        {
            currentOprtion = currentOprtion + 1 <= menuStrings.Length - 1 ? currentOprtion + 1 : 0;
        }
        else if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
        {
            currentOprtion = currentOprtion - 1 >= 0 ? currentOprtion - 1 : menuStrings.Length - 1;
        }
        else if (keyInfo.Key == ConsoleKey.Enter)
        {
            choice = currentOprtion;
            break;
        }
    }
    switch (choice)
    {
        case 0:
            Console.WriteLine("Task1!");
            Task1();
            break;
        case 1:
            Console.WriteLine("Task2!");
            Task2();
            break;
        case 2:
            Console.WriteLine("Task3!");
            Task3();
            break;
        case 3:
            Console.WriteLine("Task4!");
            Task4();
            break;
        case 4:
            Console.WriteLine("Task5!");
            Task5();
            break;

        default:
            break;
    }
}
static void PrintMenu(string[] menuString, int choosenString)
{
    for (int i = 0; i < menuString.Length; i++)
    {
        if (i == choosenString)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
        }
        Console.WriteLine(menuString[i]);
        if (i == choosenString)
        {
            Console.ResetColor();
        }
    }
}

Console.OutputEncoding = Encoding.Unicode;
while (true)
{
    Console.Clear();
    ShowMenu();
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

