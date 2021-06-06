using BookCatalogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BookCatalogue.Controllers
{
    interface IBookCatalogueController
    {

        IHttpActionResult SearchBooks(string searchKey, string searchBy);

        IHttpActionResult AddBook(BookDetails bookToAdd);

        IHttpActionResult UpdateBook(BookDetails bookToUpdate);

        IHttpActionResult DeleteBook(BookDetails bookToDelete);
    }
}
