using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookCatalogueService.Models
{
    public class BookCatalogueSearchResponse: BookCatalogueResponse
    {
        public List<BookDetails> searchResult;
    }
}