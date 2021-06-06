using BookCatalogue.Constants;
using BookCatalogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookCatalogue.Utilities
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
            faultString.Clear();

            if (bookDetails==null)
            {
                faultString.Add(AppConstants.EMPTYREQUEST);
            }
            else
            {
                if (bookDetails.authors == null || bookDetails.authors.Count <= 0)
                    faultString.Add(AppConstants.AUTHORSNOTPROVIDED);

                if (bookDetails.title == null ||bookDetails.title==string.Empty)
                    faultString.Add(AppConstants.INVALIDTITLE);

                if (!DataUtility.tryDateTimeParse(bookDetails.publicationDate))
                    faultString.Add(AppConstants.INVALIDPUBLICATIONDATE);

                if (bookDetails.ISBN.ToString().Length != 13 && DataUtility.tryDateTimeParse(bookDetails.ISBN.ToString()))
                    faultString.Add("ISBN is 13 digit number");
            }
           
            if (faultString.Count > 0)
                return false;
            else
                return true;
        }

        public bool ValidateSearch(string searchKey,string searchBy)
        {
            faultString.Clear();

            if (searchBy == AppConstants.ISBN)
            {
                try
                {
                    int.Parse(searchKey);
                }
                catch
                {
                    faultString.Add(AppConstants.INVALIDISBNLENGTH);

                    return false;
                }
            }
            else if (!AppConstants.SearchByTypes.Any(s => s.Equals(searchBy, StringComparison.OrdinalIgnoreCase)))
            {
                faultString.Add(AppConstants.INVALIDSEARCHTYPE + String.Join(",", AppConstants.SearchByTypes));
                return false;
            }

            return true;
        }
    }
}