using BookCatalogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogue.BusinessLayer
{
    public interface IBookCatalogueService
    {
        BookCatalogueSearchResponse SearchBooks(string searchKey, string searchBy);

        BookCatalogueResponse AddBook(BookDetails bookToAdd);


        BookCatalogueResponse UpdateBook(BookDetails bookToUpdate);

        BookCatalogueResponse DeleteBook(BookDetails bookToDelete);

    }
}
