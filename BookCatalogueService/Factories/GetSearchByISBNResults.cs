using BookCatalogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookCatalogue.Factories
{
    public class GetSearchByISBNResults: IGetSearchBookResults
    {
        public List<BookDetails> SearchBookResults(Dictionary<long, BookDetails> bookDetailsList, string searchKey)
        {
            return bookDetailsList.Where(x => x.Key.ToString().Contains(searchKey)).Select(x => x.Value).ToList();
        }
    }
}