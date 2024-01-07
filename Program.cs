
    using System.Text.Json;
    using System.Xml.Serialization;
    using Microsoft.EntityFrameworkCore;

    namespace ConsoleApp1;

    class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.GetHelp();
            Console.Out.WriteLine("Введите путь к файлу с контактами или введите 0, если его нет");
            string path = Console.In.ReadLine();
            var archive = new List<NotebookEntry>();

        if (path != "0")
            {
           
            Console.Out.WriteLine("Введите формат файла\n" +
                                  "1 - json\n" +
                                  "2 - xml\n" +
                                  "3 - sqlite");
            int fileExt = Convert.ToInt32(Console.In.ReadLine());
            if (!File.Exists(path))
                {
                    Console.Out.WriteLine("Нет файла с таким путем");
                }

                if (fileExt == 1)
                {
                    TextReader reader = new StreamReader(path);
                    string input = reader.ReadToEnd();
                    archive = JsonSerializer.Deserialize<List<NotebookEntry>>(input);
                }
                else if (fileExt == 2)
                {
                    var mySerializer = new XmlSerializer(typeof(List<NotebookEntry>), new XmlRootAttribute("ArrayOfNotebookEntry"));
                    var myFileStream = new FileStream(path, FileMode.Open);
                    archive = (List<NotebookEntry>)mySerializer.Deserialize(myFileStream);
                }
                else if (fileExt == 3)
                {
                    using (var context = new NotebookContext(path))
                    {
                        archive = context.Archive.ToList();
                    }
                }
            }
            var notebook = new Notebook(archive);

            while (true)
            {
                Console.Out.WriteLine("Введите номер команды");
                int commandId = Convert.ToInt16(Console.ReadLine());
                if (commandId == 5)
                {
                    break;
                }

                switch (commandId)
                {
                    case 1:
                    //добавление контакта

                    Console.Out.WriteLine("Введите имя:");
                        string name = Console.ReadLine();

                        Console.Out.WriteLine("Введите фамилию:");
                        string surname = Console.ReadLine();

                        Console.Out.WriteLine("Введите номер телефона:");
                        string phoneNumber = Console.ReadLine();

                        Console.Out.WriteLine("Введите электронную почту:");
                        string email = Console.ReadLine();

                        notebook.AddContact(name, surname, phoneNumber, email);
                        Console.Out.WriteLine();
                        break;

                    case 2:
                    // нахождение контакта
                    /*  
                    1 - имя
                    2 - фамилия
                    3 - телефон
                    4 - почта
                    5 - всё
                    */
                    int searchBy = Convert.ToInt16(Console.ReadLine());
                        NotebookEntry? found = null;
                        if (searchBy == 1)
                        {
                            Console.Out.WriteLine("Введите имя:");
                            string contactName = Console.In.ReadLine();
                            found = notebook.FindContactByName(contactName);
                        }
                        else if (searchBy == 2)
                        {
                            Console.Out.WriteLine("Введите фамилию:");
                            string contactSurname = Console.In.ReadLine();
                            found = notebook.FindContactBySurname(contactSurname);
                        }
                        else if (searchBy == 3)
                        {
                            Console.Out.WriteLine("Введите номер телефона:");
                            string contactPhoneNumber = Console.In.ReadLine();
                            found = notebook.FindContactByPhoneNumber(contactPhoneNumber);
                        }
                        else if (searchBy == 4)
                        {
                            Console.Out.WriteLine("Введите электронную почту:");
                            string contactEmail = Console.In.ReadLine();
                            found = notebook.FindContactByEmail(contactEmail);
                        }
                        else if (searchBy == 5)
                        {
                            Console.Out.WriteLine("Введите имя:");
                            string contactName = Console.In.ReadLine();
                            Console.Out.WriteLine("Введите фамилию:");
                            string contactSurname = Console.In.ReadLine();
                            Console.Out.WriteLine("Введите номер телефона:");
                            string contactPhoneNumber = Console.In.ReadLine();
                            Console.Out.WriteLine("Введите электронную почту:");
                            string contactEmail = Console.In.ReadLine();

                            found = notebook.FindContact(contactName, contactSurname, contactPhoneNumber, contactEmail);
                        }

                        if (found is not null)
                        {
                            ConsoleHelper.ShowContact(found);
                        }
                        else
                        {
                            Console.Out.WriteLine("Не существует такого контакта");
                        }

                        break;

                    case 3:
                    // отобразить все контакты
                    foreach (var entry in notebook.Archive)
                        {
                            ConsoleHelper.ShowContact(entry);
                        }
                        break;

                    case 4:
                        // сохранение в файл
                        Console.Out.WriteLine("Введите путь к файлу, в который вы хотите сохранить базу:");
                        path = Console.In.ReadLine();
                        Console.Out.WriteLine("Введите, в каком формате вы хотите сохранить базу:\n" +
                                              "1 - JSON\n" +
                                              "2 - XML\n" +
                                              "3 - sqlite");
                        Int32.TryParse(Console.In.ReadLine(), out var mode);

                        List<NotebookEntry> lst = new List<NotebookEntry>();
                        if (mode == 1)
                        {
                            StreamWriter writetext = new StreamWriter(File.Create(path));
                            string jsonString = JsonSerializer.Serialize(notebook.Archive);
                            writetext.Write(jsonString);
                            writetext.Flush();

                        }
                        else if (mode == 2)
                        {
                            StreamWriter writetext = new StreamWriter(File.Create(path));
                            var xmlSerializer = new XmlSerializer(typeof(List<NotebookEntry>));
                            xmlSerializer.Serialize(writetext, notebook.Archive);
                            writetext.Flush();

                        }
                        else if (mode == 3)
                        {
                            using (var context = new NotebookContext(path))
                            {
                                context.Database.EnsureCreated();
                                context.RemoveRange(context.Archive);
                                context.SaveChanges();
                                context.AddRange(notebook.Archive);
                                context.SaveChanges();
                            }
                        }

                        break;
                }
            }

        }
    }

