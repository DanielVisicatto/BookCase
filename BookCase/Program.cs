using BookCase.Entities;

string filename = "books.txt";
string path = @"C:\\Users\\" + Environment.UserName + "\\";
string fullpath = string.Concat(path, filename);
bool userAnswer = true;
int op;

List<Book> books = new();


do
{
    op = Menu();
    switch (op)
    {
        default:
            Console.WriteLine("Invalid Option!\n" +
                              "Please try another...");
            break;

        case 1:
            books.Add(RegisterBook());
            WriteArchives(books);
            break;

        case 2:

            break;
        case 3:
            break;

        case 4:
            PrintListOFBooks(fullpath);
            break;

        case 5:
            Environment.Exit(0);
            break;
    }
} while (op != 5);


Book RegisterBook()
{
    Console.WriteLine("Enter book data to regitry");
    Console.Write("Book's Tile: ");
    string title = Console.ReadLine();
    Console.Write("Book's Authors _ ");
    List<string> authors = new();
    do
    {        
        Console.Write("Input an Author: ");
        var author = Console.ReadLine();
        authors.Add(author);
        Console.WriteLine("There is another author?  (S/N)");
        string answer = Console.ReadLine();    
        if(answer != "s")
            userAnswer = false;
    }
    while (userAnswer);    
    Console.Write("Witch Edition? : ");
    string edition = Console.ReadLine();
    Console.Write("ISBN_Code: ");
    string isbn = Console.ReadLine();

    return new Book(title, authors, edition, isbn);
}

void PrintListOFBooks(string filePath)
{
    List<Book> books = LoadBooksFromFile(filePath);

    if (books.Count == 0)
    {
        Console.WriteLine("There are no books on the bookcase.");
        return;
    }

    foreach (var book in books)
    {
        Console.WriteLine(book.ToString());
    }
}

void WriteArchives(List<Book> lb)
{
    try
    {
        if (File.Exists(fullpath))
        {
            using StreamWriter sw = new(fullpath);
            foreach (var item in lb)
            {
                sw.WriteLine(item);
            }

            Thread.Sleep(1200);
            Console.Clear();
            Console.WriteLine($"\nCreating file in {fullpath}\n" +
                $"Press Enter to continue.");
            Console.ReadKey();
        }
        else
        {
            using StreamWriter sw = new(fullpath);
            foreach (var item in lb)
            {
                sw.WriteLine(item);
            }
            Thread.Sleep(1200);
            Console.Clear();
            Console.WriteLine($"\nCreating file in: {fullpath}\n" +
                $"Press Enter to continue.");
            Console.ReadKey();
        }

    }
    catch (Exception e)
    {
        throw new(e.Message);
    }
    finally { Console.WriteLine("Registered Successfully!\n\n"); }
}  
int Menu()
{
    Console.WriteLine("Select an option\n" +
        "1 - To register a new book\n" +
        "2 - To mark a book as readed\n" +
        "3 - To loan a book\n" +
        "4 - To print current books in bookcase\n" +
        "5 - Exit Program");
    int option = int.Parse(Console.ReadLine());
    return option;
}
List<Book> LoadBooksFromFile(string filePath)
{
    List<Book> books = new();
    if (File.Exists(filePath))
    {
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            string[] bookData = line.Split(';');
            string title = bookData[1];
            List<string> authors = bookData[2].Split(',').ToList();
            string edition = bookData[3];
            string isbn = bookData[4];

            Book book = new Book(title, authors, edition, isbn);
            books.Add(book);
        }
    }
    else
    {
        Console.WriteLine("File not found!");
    }

    return books;
}
string ReadArchives(string file)
{
    string fileText = "";
    using StreamReader sr = new(file);
    try
    {
        fileText = sr.ReadToEnd();
        if (string.IsNullOrEmpty(fileText))
        {
            Console.WriteLine("Archive is empty!");
        }
        else
        {
            Console.WriteLine(fileText);
        }
    }
    catch (Exception e)
    {
        throw new(e.Message);
    }
    return fileText;
}
