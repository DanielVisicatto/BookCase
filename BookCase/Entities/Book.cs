using BookCase.Enums;

namespace BookCase.Entities
{
    internal class Book
    {
        public string Title { get; set; }
        List<string> Authors { get; set; }
        public string Edition { get; set; }
        public string ISBN_code { get; set; }
        public Status Status { get; set; }

        public Book(string title, List<string> authors, string edition, string iSBN_code, Status status)
        {
            Title = title;
            Authors = authors;
            Edition = edition;
            ISBN_code = iSBN_code;
            Status = status;
        }



        public static Book GetBookByTitle(string title, List<Book> listOfBooks)
        {
            Console.WriteLine("Searching...");
            foreach (Book book in listOfBooks)
            {

                if (book.Title.ToLower().Replace(" ", "") == title.ToLower().Replace(" ", ""))
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Title was found!");
                    return book;
                }
            }
            return null;
        }
        public void ChangeStatus(Status newStatus)
        {
            Status = newStatus;
        }
        public string PrintAuthor()
        {
            return string.Join("-", Authors);
        }
        public string GetStatus()
        {
            return Status.ToString();
        }
        public string ToArchive()
        {
            return $"{Title} ,  {PrintAuthor()} , {Edition} , {ISBN_code} , {GetStatus()}";
        }
        public override string ToString()
        {
            return $"--------------------------------------------------\n" +
                     $"Book information:\n\n" +
                     $"Title: {Title}\n" +
                     $"Author(s): {PrintAuthor()}\n" +
                     $"Edition: {Edition}\n" +
                     $"ISBN Code: {ISBN_code}\n" +
                     $"Status: {Status}\n" +
                     $"--------------------------------------------------\n";
        }
    }
}
