namespace ConsoleApp1;

public class Notebook
{
    private readonly List<NotebookEntry> _archive;

    public Notebook(List<NotebookEntry> archive)
    {
        _archive = archive;
    }

    public ICollection<NotebookEntry> Archive => _archive;

    public void AddContact(
        string name,
        string surname,
        string phoneNumber,
        string email)
    {
        _archive.Add(new NotebookEntry(name, surname, phoneNumber, email, _archive.Count + 1));
    }

    public NotebookEntry? FindContactByName(string name)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        foreach (var entry in _archive)
        {
            if (entry.Name == name)
            {
                return entry;
            }
        }

        return null;
    }

    public NotebookEntry? FindContactBySurname(string surname)
    {
        if (surname is null)
        {
            throw new ArgumentNullException(nameof(surname));
        }

        foreach (var entry in _archive)
        {
            if (entry.Surname == surname)
            {
                return entry;
            }
        }

        return null;
    }

    public NotebookEntry? FindContactByPhoneNumber(string phoneNumber)
    {
        if (phoneNumber is null)
        {
            throw new ArgumentNullException(nameof(phoneNumber));
        }

        foreach (var entry in _archive)
        {
            if (entry.PhoneNumber == phoneNumber)
            {
                return entry;
            }
        }

        return null;
    }

    public NotebookEntry? FindContactByEmail(string email)
    {
        if (email is null)
        {
            throw new ArgumentNullException(nameof(email));
        }

        foreach (var entry in _archive)
        {
            if (entry.Email == email)
            {
                return entry;
            }
        }

        return null;
    }

    public NotebookEntry? FindContact(
        string name,
        string surname,
        string phoneNumber,
        string email)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (surname is null)
        {
            throw new ArgumentNullException(nameof(surname));
        }
        if (phoneNumber is null)
        {
            throw new ArgumentNullException(nameof(phoneNumber));
        }
        if (email is null)
        {
            throw new ArgumentNullException(nameof(email));
        }

        foreach (var entry in _archive)
        {
            if (entry.Name == name && entry.Surname == surname
                                   && entry.PhoneNumber == phoneNumber
                                   && entry.Email == email)
            {
                return entry;
            }
        }

        return null;
    }
}