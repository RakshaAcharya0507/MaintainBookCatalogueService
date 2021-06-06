using BookCatalogue.Constants;
using BookCatalogue.Factories;
using BookCatalogue.Models;
using BookCatalogue.Utilities;
using BookCatalogue.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookCatalogue.BusinessLayer;

namespace BookCatalogue.BusinessLayer
{
    public class BookCatalogueService:IBookCatalogueService
    {

        ValidationUtility validation = null;
        IMessageService messaging = null;
        public BookCatalogueService()
        {
            validation = new ValidationUtility();
            messaging = new MessageService();
        }


        public static Dictionary<long, BookDetails> bookDetailsList = new Dictionary<long, BookDetails>();

        public BookCatalogueSearchResponse SearchBooks(string searchKey, string searchBy)
        {
            BookCatalogueSearchResponse bookCatalogueSearchResponse = new BookCatalogueSearchResponse();
            try
            {
                if (validation.ValidateSearch(searchKey, searchBy))
                {
                    ISearchBookResultsFactory searchBookResultsFactory = new SearchBookResultsFactoryImpl();

                    //Factory pattern implementation
                    bookCatalogueSearchResponse.searchResult = searchBookResultsFactory.GetSearchBookResults(bookDetailsList,searchKey,searchBy);
                    
                    bookCatalogueSearchResponse.isSuccess = true;
                }
                else
                {
                    bookCatalogueSearchResponse.statusMessages = validation.faultString;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bookCatalogueSearchResponse;
        }

        public BookCatalogueResponse AddBook(BookDetails bookToAdd)
        {
            BookCatalogueResponse bookCatalogueResponse = new BookCatalogueResponse();
            if (validation.ValidateRequest(bookToAdd))
            {
                if (bookDetailsList.ContainsKey(bookToAdd.ISBN))
                {
                    bookCatalogueResponse.statusMessages = new List<string> { AppConstants.ISBNEXISTS };
                }
                else
                {
                    bookDetailsList.Add(bookToAdd.ISBN, bookToAdd);             
                    if(!messaging.AddToQueue(String.Format(AppConstants.MESSAGETOQUEUE, AppConstants.ADDED, bookToAdd.ISBN.ToString())))
                    {
                        bookCatalogueResponse.statusMessages = new List<string> { AppConstants.QUEUEFAIL };
                    }
                    bookCatalogueResponse.isSuccess = true;

                }
            }
            else
            {
                bookCatalogueResponse.statusMessages = validation.faultString;
            }
            return bookCatalogueResponse;
        }

        public BookCatalogueResponse UpdateBook(BookDetails bookToUpdate)
        {
            BookCatalogueResponse bookCatalogueResponse = new BookCatalogueResponse();
            if (validation.ValidateRequest(bookToUpdate))
            {
                if (!bookDetailsList.ContainsKey(bookToUpdate.ISBN))
                    bookCatalogueResponse.statusMessages = new List<string> { AppConstants.ISBNDONTEXISTS };
                else
                {
                    bookDetailsList[bookToUpdate.ISBN] = bookToUpdate;
                    if (!messaging.AddToQueue(String.Format(AppConstants.MESSAGETOQUEUE, AppConstants.UPDATED, bookToUpdate.ISBN.ToString())))
                    {
                        bookCatalogueResponse.statusMessages = new List<string> { AppConstants.QUEUEFAIL };
                    }
                    bookCatalogueResponse.isSuccess = true;
                }
            }
            else
            {
                bookCatalogueResponse.statusMessages = validation.faultString;
            }
            return bookCatalogueResponse;
        }

        public BookCatalogueResponse DeleteBook(BookDetails bookToDelete)
        {
            BookCatalogueResponse bookCatalogueResponse = new BookCatalogueResponse();

            if (!bookDetailsList.ContainsKey(bookToDelete.ISBN))
                bookCatalogueResponse.statusMessages.Add(AppConstants.ISBNDONTEXISTS);
            else
            {
                bookDetailsList.Remove(bookToDelete.ISBN);
                if (!messaging.AddToQueue(String.Format(AppConstants.MESSAGETOQUEUE, AppConstants.DELETED, bookToDelete.ISBN.ToString())))

                {
                    bookCatalogueResponse.statusMessages = new List<string> { AppConstants.QUEUEFAIL };
                }
                bookCatalogueResponse.isSuccess = true;
            }
            return bookCatalogueResponse;
        }
    }
}