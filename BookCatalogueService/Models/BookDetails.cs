using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookCatalogueService.Models
{
    public class BookDetails
    {
        public string title;

        public List<string> authors;

        //assuming the digit wont start with zero
        public long ISBN;

        public string publicationDate;
    }
}