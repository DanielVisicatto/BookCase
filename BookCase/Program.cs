using BookCase.Entities;
using BookCase.Enums;

string filename = "books.txt";
string path = @"C:\\Users\\" + Environment.UserName + "\\";
string fullpath = string.Concat(path, filename);
var fileText = "";
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
        Console.Write("Input an Author:");
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
    Console.Write("Book's current status: ");
    int bookStatus = int.Parse(Console.ReadLine());

    return new Book(title, authors, edition, isbn, (Status)bookStatus);
}
Book ToLoan(Book book)
{
    book.Status = (Status)3;
    return book;
}
Book SearchBook(Book book)
{
    var userSearch = Console.ReadLine();
    var readedFile = ReadArchives("books.txt");
    var compared = readedFile.Trim(' ').ToUpper().Contains(userSearch.Trim(' ').ToUpper());
    return book;
}

Book MarkAsReaded(Book book)
{
    Console.WriteLine("Witch book do you want to mark as read?");
    var userSearch = Console.ReadLine();
    var readedFile = ReadArchives("books.txt");
    var compared = readedFile.Trim(' ').ToUpper().Contains(userSearch.Trim(' ').ToUpper());
    if (compared)
    {
        book.Status = (Status)2;
    }
    return book;
}

bool WriteArchives(List<Book> lb)
{
    try
    {
        if (File.Exists(fullpath))
        {
            StreamWriter sw = new(fullpath);
            foreach (var item in lb)
            {
                sw.WriteLine(item.ToString());
            }

            Thread.Sleep(1200);
            Console.WriteLine($"\nCreating file in {fullpath}\n" +
                $"Press Enter to continue.");
            Console.ReadKey();
            sw.Close();
        }
        else
        {
            StreamWriter sw = new(fullpath);
            foreach (var item in lb)
            {
                sw.WriteLine(item.ToString());
            }
            Thread.Sleep(1200);
            Console.WriteLine($"\nCreating file in: {fullpath}\n" +
                $"Press Enter to continue.");
            Console.ReadKey();
            sw.Close();
        }

    }
    catch (Exception e)
    {
        throw new(e.Message);
        return false;
    }
    finally { Console.WriteLine("Registered Successfully!\n\n"); }
    return true;
}

string ReadArchives(string file)
{
    StreamReader sr = new(file);
    try
    {
        fileText = sr.ReadToEnd();
        if (fileText.Contains(null))
        {
            Console.WriteLine("Archive is empty!");
            sr.Close();
        }
        else
        {
            foreach (var item in fileText)
            {
                Console.WriteLine(fileText);
                sr.Close();
            }
        }

    }
    catch (Exception e)
    {
        throw new(e.Message);
    }
    finally
    {
        sr.Close();
    }
    return fileText;
}
int Menu()
{
    Console.WriteLine("Select an option\n" +
        "1 - To register a new book\n" +
        "2 - To mark a book as readed\n" +
        "3 - To loan a book\n" +
        "4 - Exit Program");
    var option = int.Parse(Console.ReadLine());
    return option;
}
