using BookCatalogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogue.Factories
{
    interface IGetSearchBookResults
    {
        List<BookDetails> SearchBookResults(Dictionary<long, BookDetails> bookDetailsList, string searchBy);
    }
}
