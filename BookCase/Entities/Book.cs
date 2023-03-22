using BookCase.Enums;

namespace BookCase.Entities
{
    internal class Book
    {
        public string Title { get; set; }
        List<string> Authors { get; set; }

        public string Edition { get; set; }
        public string ISBN_code {get; set;}
        public Status Status { get; set; }

        public Book(string title, List<string> authors, string edition, string iSBN_code, Status status)
        {
            Title = title;
            Authors = authors;
            Edition = edition;
            ISBN_code = iSBN_code;
            Status = status;
        }

        public string PrintAuthor(List <string> authors)
        {            
            foreach (var item in authors)
            {
               Authors.Add(item);
            }       
            return Authors.ToString();
        }

        public override string ToString()
        {
            return $"\n--------------------------------------------------\n" +
                     $"Book information:\n" +
                     $"Title: {Title}\n" +
                     $"Author(s): {PrintAuthor(Authors)}\n" +
                     $"Edition: {Edition}\n"+
                     $"ISBN Code: {ISBN_code}\n" +
                     $"Situation: {Status}\n" +
                     $"--------------------------------------------------\n";
        }
    }
}
