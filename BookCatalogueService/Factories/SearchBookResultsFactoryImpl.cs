using BookCatalogue.Constants;
using BookCatalogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookCatalogue.Factories
{
    public class SearchBookResultsFactoryImpl: ISearchBookResultsFactory
    {
        public List<BookDetails> GetSearchBookResults(Dictionary<long, BookDetails> bookDetailsList, string searchKey, string searchBy)
        {
            IGetSearchBookResults getSearchBookResults = null;

            switch (searchBy.ToUpper())
            {
                case AppConstants.ISBN:
                    {
                        getSearchBookResults = new GetSearchByISBNResults();                      
                        break;
                    } 
                    
                case AppConstants.AUTHOR:
                    {
                        getSearchBookResults = new GetSearchByAuthorResults();
                        break;
                    }
                case AppConstants.TITLE:
                    {
                        getSearchBookResults = new GetSearchByTitlesResults();
                        break;
                    }
                default:
                    break;
            }
            return getSearchBookResults.SearchBookResults(bookDetailsList, searchKey);
        }
    }
}