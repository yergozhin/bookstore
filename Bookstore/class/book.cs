using Bookstore.@class;
using System;
using System.Collections.Generic;

[Serializable]
public class Book
{
    private static List<Book> books = new List<Book>();

    private List<Wishlist> associatedWishlists = new List<Wishlist>();
    public IReadOnlyList<Wishlist> getAssociatedWishlists() => associatedWishlists.AsReadOnly();

    private List<Order> associatedOrders = new List<Order>();
    public IReadOnlyList<Order> getAssociatedOrders() => associatedOrders.AsReadOnly();

    private List<Review> associatedReviews = new List<Review>();
    public IReadOnlyList<Review> getAssociatedReviews() => associatedReviews.AsReadOnly();

    private Author assignedAuthor; // Ассоциация с автором
    public Author AssignedAuthor => assignedAuthor;

    private Publisher publisher;

    public Publisher Publisher => publisher;
    
    private Dictionary<string, Author> authors = new Dictionary<string, Author>();

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
        set
        {
            if (listOfLanguagesToWhichTranslated != null)
            {
                for (int i = 0; i < listOfLanguagesToWhichTranslated.Count; i++)
                {
                    if (string.IsNullOrEmpty(listOfLanguagesToWhichTranslated[i]))
                    {
                        throw new ArgumentException("Translated language cannot be empty.");
                    }
                }
            }

            listOfLanguagesToWhichTranslated = value ?? new List<string>();
        }
    }

    public Book()
    {
        ListOfLanguagesToWhichTranslated = new List<string>();
    }

    public Book(string title, double price, string languageOfPublication,
        List<string> listOfLanguagesToWhichTranslated = null)
    {
        Title = title;
        Price = price;
        LanguageOfPublication = languageOfPublication;
        ListOfLanguagesToWhichTranslated = listOfLanguagesToWhichTranslated ?? new List<string>();
        books.Add(this);
    }

    public static void ClearBooks()
    {
        books.Clear();
    }

    public static List<Book> GetBooks()
    {
        return new List<Book>(books);
    }

    public void addLanguageToWhichTranslated(string language)
    {
        if (listOfLanguagesToWhichTranslated.Contains(language))
        {
            return;
        }

        if (string.IsNullOrEmpty(language))
        {
            throw new ArgumentException("Language to which the book was translated cannot be empty.");
        }

        listOfLanguagesToWhichTranslated.Add(language);
    }

    public bool bookPresent(Book book)
    {
        foreach (Book b in books)
        {
            if (b.title == book.title)
            {
                return true;
            }
        }

        return false;
    }

    public void assignToWishlist(Wishlist wishlist)
    {
        if (!associatedWishlists.Contains(wishlist))
        {
            associatedWishlists.Add(wishlist);
            wishlist.AddBook(this);
        }
    }

    public void removeFromWishlist(Wishlist wishlist)
    {
        if (associatedWishlists.Contains(wishlist))
        {
            associatedWishlists.Remove(wishlist);
            wishlist.RemoveBook(this);
        }
    }

    public void assignToOrder(Order order)
    {
        if (!associatedOrders.Contains(order))
        {
            associatedOrders.Add(order);
            order.addBook(this);
        }
    }

    public void removeFromOrder(Order order)
    {
        if (associatedOrders.Contains(order))
        {
            associatedOrders.Remove(order);
            order.removeBook(this);
        }
    }

    public void addReview(Review review)
    {
        if (!associatedReviews.Contains(review))
        {
            associatedReviews.Add(review);
            review.assignBook(this);
        }
    }

    public void removeReview(Review review)
    {
        if (associatedReviews.Contains(review))
        {
            associatedReviews.Remove(review);
            review.removeFromBook(this);
        }
    }

    public void removeAllReviews()
    {
        foreach (Review review in associatedReviews)
        {
            removeReview(review);
        }
    }

    // --- Методы для связи с автором ---
    public void AssignAuthor(Author author)
    {
        if (author == null)
            throw new ArgumentException("Author cannot be null.");

        if (assignedAuthor != author)
        {
            assignedAuthor = author;
            author.AddBook(this); // Обратная связь
        }
    }

    public void RemoveAuthor()
    {
        if (assignedAuthor != null)
        {
            var tempAuthor = assignedAuthor;
            assignedAuthor = null;
            tempAuthor.RemoveBook(this); // Обратная связь
        }
    }

    public void UpdateAuthor(Author newAuthor)
    {
        RemoveAuthor();
        AssignAuthor(newAuthor);
    }

    public void AssignPublisher(Publisher newPublisher)
    {
        if (publisher != newPublisher)
        {
            publisher = newPublisher;
            newPublisher.AddBook(this); // Обратная связь
        }
    }

    public void RemovePublisher()
    {
        if (publisher != null)
        {
            var tempPublisher = publisher;
            publisher = null;
            tempPublisher.RemoveBook(this); // Обратная связь
        }
    }
}