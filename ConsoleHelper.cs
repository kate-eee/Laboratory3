namespace ConsoleApp1;

public static class ConsoleHelper
{
    public static void GetHelp()
    {
        Console.Out.WriteLine("Список команд:");
        Console.Out.WriteLine("1) Чтобы создать новый контакт, нажмите  1");
        Console.Out.WriteLine("2) Чтобы найти контакт, нажмите 2");
        Console.Out.WriteLine("3) Чтобы отобразить все контакты, нажмите 3");
        Console.Out.WriteLine("4) Чтобы сохранить контакт в файл, нажмите 4");
    }

    public static void ShowContact(NotebookEntry entry)
    {
        // выводим информацию об одном контакте 
        Console.WriteLine("Имя: " + entry.Name);
        Console.WriteLine("Фамилия: " + entry.Surname);
        Console.WriteLine("Номер телефона: " + entry.PhoneNumber);
        Console.WriteLine("Электронная почта: " + entry.Email);

        Console.Out.WriteLine();
    }
}