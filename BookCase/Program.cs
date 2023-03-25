using BookCase.Entities;
using BookCase.Enums;
using System.Data;
using System.Security.Authentication.ExtendedProtection;

string answer;
string filename = "books.txt";
string path = @"C:\\Users\\" + Environment.UserName + "\\";
string fullpath = string.Concat(path, filename);

int op;

List<Book> books = new();
List<Book> recordedList = ReadArchives(fullpath, books);


do
{
    Console.Clear();
    op = Menu();
    switch (op)
    {
        default:
            Console.WriteLine("Invalid Option!\n" +
                              "Please try another...");
            break;

        case 1:
            books.Add(RegisterBook());
            WriteArchives(books, fullpath);
            break;

        case 2:
            //ReadArchives(fullpath, books);
            //string title3 = Console.ReadLine();
            //Book bk2 = Book.GetBookByTitle(title3, books)
            
            break;

        case 3:
            break;

        case 4:
            PrintAllBooks(recordedList);
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            break;

        case 5:
            Console.WriteLine("Witch book are you looking for?");
            try
            {
                ReadArchives(fullpath, books);
                string title = Console.ReadLine();
                Book bk = Book.GetBookByTitle(title, books);
                Console.WriteLine(bk.ToString());
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("Book not found!");
            }

            break;

        case 6:
            List<Book> listToDrop = new();
            ReadArchives(fullpath, listToDrop);
            Console.WriteLine("Witch book are you trying to delete?");
            string title2 = Console.ReadLine();
            DropBookAndUpdateArchive(title2, listToDrop, fullpath);
            recordedList = listToDrop;
            break;

        case 7:
            Console.WriteLine("Thank you for use our bookcase!");
            Environment.Exit(0);
            break;
    }
} while (op != 7);


Book RegisterBook()
{
    Console.WriteLine("Enter book data to register");
    Console.Write("Book's Tile: ");
    string title = Console.ReadLine();
    Console.Write("Book's Authors _ ");
    List<string> authors = new();
    do
    {
        Console.Write("Input an Author: ");
        var author = Console.ReadLine();
        authors.Add("-" + author);
        Console.WriteLine("There is another author?  (Y/N)");
        answer = Console.ReadLine().ToLower();
    }
    while (answer == "y");
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
    book.Status = Status.Lent;
    return book;
}

void WriteArchives(List<Book> lb, string path)
{
    StreamWriter sw = new(path);
    try
    {
        foreach (Book book in lb)
        {
            sw.WriteLine(book.ToArchive());
            Thread.Sleep(1200);
            Console.WriteLine("Recording...");          
        }
        Thread.Sleep(1200);
        Console.WriteLine($"\nCreating/Updating file in {path}");
        Console.WriteLine("Press Enter to continue");
        Console.ReadLine();
    }
    catch (Exception e)
    {
        path = fullpath + "error.log";
        sw = new(path);
        sw.WriteLine(e.Message.ToString());

    }
    finally
    {
        Console.Clear();
        Console.WriteLine("Registered Successfully!\n\n");
        sw.Close();
    }
}

List<Book> ReadArchives(string path, List<Book> lb)
{

    if (File.Exists(path))
    {
        StreamReader sr = new(path);

        while (!sr.EndOfStream)
        {
            string[] bookDetails = sr.ReadLine().Split(",");
            string title = bookDetails[0];
            string[] authorsVet = bookDetails[1].Split("-");
            List<string> authors = new(authorsVet); // criando uma nova lista de autores para cada livro lido do arquivo
            string edition = bookDetails[2];
            string isbnCode = bookDetails[3];
            Status bookStatus = (Status)Enum.Parse(typeof(Status), bookDetails[4]);
            #region[explicacao linha acima]
            /*o método Enum.Parse recebe dois argumentos primeiro é o tipo de de enumeração que desejamos converter
             e o segundo é a string que representa no caso o nosso Status
            Ele verifica se a string recebida na posição 4 do vetor bookDetails corresponde a algum valor especificado
            no nosso Enum
            Se encontrar ele retorna um Object, que necessita do cast (Status), então depois de converter explicitamente
            podemos salvar como Status e construir nosso objeto abaixo.
            O typeof é usado para obter informações sobre o tipo de uma classe, estrutura ou enum, permitindo que o desenvolvedor
            opere dinamicamente com esses tipos.
            ELe retorna um objeto System.Type, que no nosso código se refere ao tipo Status e assim o , método Enum.Parse sabe
            qual enumeração vai ser usada na conversão.*/
            #endregion
            lb.Add(new(title, authors, edition, isbnCode, bookStatus));
        }
        sr.Close();
    }
    else
    {
        Console.WriteLine("Archive still not exists...\n"); ;
        Thread.Sleep(1300);
    }
    return lb;
}

int Menu()
{
    Console.WriteLine("Select an option\n" +
        "1 - To register a new book\n" +
        "2 - To mark a book as readed\n" +
        "3 - To loan a book\n" +
        "4 - To list all books\n" +
        "5 - To search a book by the title\n" +
        "6 - To delete a book from shelf\n" +
        "7 - Exit Program");
    var option = int.Parse(Console.ReadLine());
    return option;
}

void PrintAllBooks(List<Book> lb)
{
    foreach (var item in lb)
    {
        Console.WriteLine(item.ToString());
    }
}

List <Book> DropBookAndUpdateArchive(string title, List<Book> listTodrop, string path)
{
    Book bookToDrop = Book.GetBookByTitle(title, listTodrop);
    if (bookToDrop != null)
    {
        Console.WriteLine($"The book:{bookToDrop.Title} will be lost!!!\n Is that really what you want?\n(Type \"Delete\" to confirm...)");
        string answer2 = Console.ReadLine();
        if (answer2 == "Delete")
        {
            listTodrop.Remove(bookToDrop);
            Console.WriteLine($"The book: {bookToDrop.Title} was thrown away...");
            WriteArchives(listTodrop, path);
            
        }
        else
        {
            return recordedList;
        }
        return listTodrop;
    }    
    else
    {
        Console.WriteLine("The book is not found!");
        return recordedList;
    }
}