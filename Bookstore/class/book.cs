[Serializable]
public class Book
{
    private static List<Book> books = new List<Book>();

    private string title;
    private double price;
    private string languageOfPublication;
    private List<string> listOfLanguagesToWhichTranslated;

    private int lengthMax = 50;
    private int lengthMin = 3;
    private int maxPrice = 10000;

    public string Title
    {
        get => title;
        set
        {
            if (string.IsNullOrEmpty(value) || value.Length < lengthMin || value.Length > lengthMax)
            {
                throw new ArgumentException($"Title must be between {lengthMin} and {lengthMax} characters.");
            }
            title = value;
        }
    }

    public double Price
    {
        get => price;
        set
        {
            if (value < 0 || value > maxPrice)
            {
                throw new ArgumentException($"Price must be between 0 and {maxPrice}.");
            }
            price = value;
        }
    }

    public string LanguageOfPublication
    {
        get => languageOfPublication;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Language of publication cannot be empty.");
            }
            languageOfPublication = value;
        }
    }

    public List<string> ListOfLanguagesToWhichTranslated
    {
        get => new List<string>(listOfLanguagesToWhichTranslated);
        set => listOfLanguagesToWhichTranslated = value ?? new List<string>();
    }

    public Book() // Параметрless конструктор
    {
        ListOfLanguagesToWhichTranslated = new List<string>(); // Инициализируем коллекцию
    }

    public Book(string title, double price, string languageOfPublication, List<string> listOfLanguagesToWhichTranslated = null)
    {
        Title = title;
        Price = price;
        LanguageOfPublication = languageOfPublication;
        ListOfLanguagesToWhichTranslated = listOfLanguagesToWhichTranslated ?? new List<string>();
        books.Add(this);
    }

    public static List<Book> GetBooks()
    {
        return books;
    }
}
