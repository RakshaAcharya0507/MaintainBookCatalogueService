using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookCatalogue.Models
{
    public class BookCatalogueResponse
    {
        public bool isSuccess;

        public List<String> statusMessages;

        public SystemException systemErrors;
    }
}