namespace ConsoleApp1;

class Book
{
    public Book(string? name, string? author, BookType bookType)
    {
        Name = name;
        Author = author;
        BookType = bookType;
    }

    public string? Name { get; set; }
    public string? Author { get; set; }
    public BookType BookType { get; set; }

    public override string ToString()
    {
        return $"\"{Name}\" (autor: {Author}) - [Dewey: {(int)BookType:D3} {BookType}]";
    }
}
