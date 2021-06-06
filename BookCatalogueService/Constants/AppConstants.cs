using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookCatalogue.Constants
{
    public static class AppConstants
    {
        public const string ISBN = "ISBN";
        public const string TITLE = "title";
        public const string AUTHOR = "author";

        public static readonly List<String> SearchByTypes = new List<string>{ "ISBN", "title", "author" };

        public const string DATETIMEFORMAT = "dd/MM/yyyy";

        public const string QUEUEPATH = @".\Private$\BookCatalogueQueue";
        //Return messages

        public const string ISBNEXISTS= "ISBN already exists";
        public const string ISBNDONTEXISTS = "ISBN does not exist";
        public const string EMPTYREQUEST = "Empty request";
        public const string AUTHORSNOTPROVIDED = "Authors not provided";
        public const string INVALIDTITLE = "Title is invalid";
        public const string INVALIDPUBLICATIONDATE = "Invalid publication date";
        public const string INVALIDISBNLENGTH = "ISBN is 13 digit number";
        public const string INVALIDSEARCHTYPE = "Invalid search type, search type should be one of: ";
        public const string QUEUEFAIL = "could not push to the queue, check logs for details";
        public const string QUEUENOTFOUND = "Queue not found";
        public const string NODATAINQUEUE = "No data found in queue";
        public const string MESSAGETOQUEUE = "Book {0} to the catalogue with ISBN ={1}";

        public const string ADDED = "added";
        public const string UPDATED = "updated";
        public const string DELETED = "deleted";
    }
}