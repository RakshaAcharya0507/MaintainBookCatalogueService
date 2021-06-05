using BookCatalogueService.Constants;
using BookCatalogueService.Models;
using BookCatalogueService.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookCatalogueService.Controllers
{
    [RoutePrefix("api/BookCatalogue")]
    public class BookCatalogueController : ApiController,IBookCatalogue
    {

        public static Dictionary<long, BookDetails> bookDetailsList = new Dictionary<long, BookDetails>();

        ValidationUtility Validation = null;
        public BookCatalogueController()
        {
            Validation = new ValidationUtility();
        }

        [Route("SearchBooks/{searchKey}/{searchBy}")]
        [HttpGet]
        public IHttpActionResult SearchBooks(string searchKey,string searchBy)
        {
            BookCatalogueSearchResponse bookCatalogueSearchResponse = new BookCatalogueSearchResponse();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            try
            {
                ValidationUtility validateSearch = new ValidationUtility();
                if (validateSearch.ValidateSearch(searchKey, searchBy))
                {
                    if (searchBy == AppConstants.ISBN)
                    {
                        bookCatalogueSearchResponse.searchResult = bookDetailsList.Where(x => x.Key.ToString().Contains(searchKey)).Select(x => x.Value).ToList();
                    }
                    else if (AppConstants.TITLE.Equals(searchBy, StringComparison.OrdinalIgnoreCase))
                    {
                        bookCatalogueSearchResponse.searchResult = bookDetailsList.Where(x => x.Value.title.Contains(searchKey)).Select(x => x.Value).ToList();
                    }
                    else if (AppConstants.Author.Equals(searchBy, StringComparison.OrdinalIgnoreCase))
                    {
                        bookCatalogueSearchResponse.searchResult = bookDetailsList.Where(x => x.Value.authors.Any(s => s.Contains(searchKey))).Select(x => x.Value).ToList();
                    }
                    if (bookCatalogueSearchResponse.searchResult.Count <= 0)
                    {
                        bookCatalogueSearchResponse.statusMessages = new List<string>{ "No data found" };
                    }                      
                    bookCatalogueSearchResponse.isSuccess = true;
                }
                else
                {
                    bookCatalogueSearchResponse.statusMessages =Validation.faultString;
                }
            }
            catch (Exception ex)
            {
                bookCatalogueSearchResponse.systemErrors = new SystemException(ex.Message);
                statusCode = HttpStatusCode.InternalServerError;

            }

            HttpResponseMessage response = Request.CreateResponse(statusCode, bookCatalogueSearchResponse);
            return ResponseMessage(response);
        }


        [HttpPost]
        [Route("AddBook")]

        //We can also add [FromHeader] headers and validate the headers
        public IHttpActionResult AddBook([FromBody] BookDetails bookToAdd)
        {
            BookCatalogueResponse bookCatalogueResponse = new BookCatalogueResponse();
            if (Validation.ValidateRequest(bookToAdd))
            {
                if (bookDetailsList.ContainsKey(bookToAdd.ISBN))
                {
                    bookCatalogueResponse.statusMessages = new List<string> { "ISBN already exists" };
                }                   
                else
                {
                    bookDetailsList.Add(bookToAdd.ISBN, bookToAdd);
                    bookCatalogueResponse.isSuccess = true;
                }
            }
            else
            {
                bookCatalogueResponse.statusMessages = Validation.faultString;
            }
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, bookCatalogueResponse);
            return ResponseMessage(response);
        }

        [HttpPut]
        [Route("UpdateBook")]
        //We can also add [FromHeader] headers and validate the headers
        public IHttpActionResult UpdateBook([FromBody] BookDetails bookToUpdate)
        {
            BookCatalogueResponse bookCatalogueResponse = new BookCatalogueResponse();
            ValidationUtility validateUpdate = new ValidationUtility();
            if (validateUpdate.ValidateRequest(bookToUpdate))
            {
                if (!bookDetailsList.ContainsKey(bookToUpdate.ISBN))
                    bookCatalogueResponse.statusMessages=new List<string>{ "ISBN does not exist"};
                else
                {
                    bookDetailsList[bookToUpdate.ISBN] = bookToUpdate;
                    bookCatalogueResponse.isSuccess = true;
                }
            }
            else
            {
                bookCatalogueResponse.statusMessages = validateUpdate.faultString;
            }
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, bookCatalogueResponse);
            return ResponseMessage(response);
        }

        [HttpDelete]
        [Route("DeleteBook")]
        //We can also add [FromHeader] headers and validate the headers
        public IHttpActionResult DeleteBook([FromBody] BookDetails bookToDelete)
        {
            BookCatalogueResponse bookCatalogueResponse = new BookCatalogueResponse();

            if (!bookDetailsList.ContainsKey(bookToDelete.ISBN))
                bookCatalogueResponse.statusMessages.Add("ISBN does not exist");
            else
            {
                bookDetailsList.Remove(bookToDelete.ISBN);
                bookCatalogueResponse.isSuccess = true;
            }
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, bookCatalogueResponse);
            return ResponseMessage(response);
        }
    }
}
