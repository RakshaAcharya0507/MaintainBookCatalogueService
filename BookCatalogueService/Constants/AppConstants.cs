using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookCatalogueService.Constants
{
    public static class AppConstants
    {
        public const string ISBN = "ISBN";
        public const string TITLE = "title";
        public const string Author = "author";

        public static readonly List<String> SearchByTypes = new List<string>{ "ISBN", "title", "author" };
    }
}