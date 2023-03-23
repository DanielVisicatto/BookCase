namespace BookCase.Entities
{
    [Serializable]
    internal class Book
    {
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public string Edition { get; set; }
        public string ISBN_code { get; set; }


        public Book(string title, List<string> authors, string edition, string iSBN_code)
        {
            Title = title;
            Authors = authors;
            Edition = edition;
            ISBN_code = iSBN_code;

        }
        #region[Comentário sobre método PrintAuthor que estava errado]
        /*Estávamos adicionando itens duplicados à lista Authors em vez de retornar a lista atual. A lógica também
        estava errada, estva recebendo a lista de autores como argumento, mas já tinha acesso à lista de autores
        por meio da propriedade Authors.*/
        #endregion
        public string PrintAuthor()
        {
            return string.Join(", ", Authors);
        }

        public override string ToString()
        {
            return
            $@"--------------------------------------------------
Title: {Title}
Author(s): {PrintAuthor()}
Edition: {Edition}
ISBN Code: {ISBN_code}
--------------------------------------------------";
        }
    }
}
