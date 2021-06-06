using BookCatalogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogue.Factories
{
    interface ISearchBookResultsFactory
    {
        List<BookDetails> GetSearchBookResults(Dictionary<long, BookDetails> bookDetailsList, string searchKey, string searchBy);
    }
}
