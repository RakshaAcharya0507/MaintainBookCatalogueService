using BookCatalogueService.Constants;
using BookCatalogueService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookCatalogueService.Utilities
{
    public class ValidationUtility
    {
        public List<String> faultString = null;
        public ValidationUtility()
        {
            faultString = new List<string>();
        }
        public bool ValidateRequest(BookDetails bookDetails)
        {
            if(bookDetails==null)
            {
                faultString.Add("Empty request");
            }
            else
            {
                if (bookDetails.authors == null || bookDetails.authors.Count <= 0)
                    faultString.Add("Authors not provided");

                if (bookDetails.title == null ||bookDetails.title==string.Empty)
                    faultString.Add("Title is invalid");

                if (!DataUtility.tryDateTimeParse(bookDetails.publicationDate))
                    faultString.Add("Invalid publication date");

                if (bookDetails.ISBN.ToString().Length != 13)
                    faultString.Add("ISBN is 13 digit number");
            }
           
            if (faultString.Count > 0)
                return false;
            else
                return true;
        }

        public bool ValidateSearch(string searchKey,string searchBy)
        {
            if (searchBy == AppConstants.ISBN)
            {
                try
                {
                    int.Parse(searchKey);
                }
                catch
                {
                    faultString.Add("ISBN should be numeric");

                    return false;
                }
            }
            else if (!AppConstants.SearchByTypes.Any(s => s.Equals(searchBy, StringComparison.OrdinalIgnoreCase)))
            {
                faultString.Add("Invalid search type, search type should be one of: " + String.Join(",", AppConstants.SearchByTypes));
                return false;
            }

            return true;
        }
    }
}