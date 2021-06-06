using BookCatalogue.BusinessLayer;
using BookCatalogue.Constants;
using BookCatalogue.Models;
using BookCatalogue.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookCatalogue.Controllers
{  
    [RoutePrefix("api/BookCatalogue")]
    public class BookCatalogueController : ApiController,IBookCatalogueController
    {
        IBookCatalogueService bookCatalogueService = null;
        public BookCatalogueController()
        {
            bookCatalogueService = new BookCatalogueService();
        }

        /// <summary>
        /// search books in catalogue by matching title,author or ISBN partial or full string
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="searchBy"></param>
        /// <returns></returns>
        [Route("SearchBooks/{searchKey}/{searchBy}")]
        [HttpGet]
        public IHttpActionResult SearchBooks(string searchKey,string searchBy)
        {
            BookCatalogueSearchResponse bookCatalogueSearchResponse = new BookCatalogueSearchResponse();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            try
            {
                bookCatalogueSearchResponse=bookCatalogueService.SearchBooks(searchKey,searchBy);
            }
            catch (Exception ex)
            {
                bookCatalogueSearchResponse.systemErrors = new SystemException(ex.Message);
                statusCode = HttpStatusCode.InternalServerError;

            }

            HttpResponseMessage response = Request.CreateResponse(statusCode, bookCatalogueSearchResponse);
            return ResponseMessage(response);
        }


        /// <summary>
        /// Adding the book to the catalogue
        /// </summary>
        /// <param name="bookToAdd"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddBook")]

        //We can also add [FromHeader] headers and validate the headers
        public IHttpActionResult AddBook([FromBody] BookDetails bookToAdd)
        {
            BookCatalogueResponse bookCatalogueResponse = new BookCatalogueResponse();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            try
            {
                bookCatalogueResponse = bookCatalogueService.AddBook(bookToAdd);
            }
            catch (Exception ex)
            {
                bookCatalogueResponse.systemErrors = new SystemException(ex.Message);
                statusCode = HttpStatusCode.InternalServerError;
            }

            HttpResponseMessage response = Request.CreateResponse(statusCode, bookCatalogueResponse);
            return ResponseMessage(response);
        }

        /// <summary>
        /// updating the book catalogue by matching the ISBN number
        /// </summary>
        /// <param name="bookToUpdate"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateBook")]
        //We can also add [FromHeader] headers and validate the headers
        public IHttpActionResult UpdateBook([FromBody] BookDetails bookToUpdate)
        {
            BookCatalogueResponse bookCatalogueResponse = new BookCatalogueResponse();
            HttpStatusCode statusCode = HttpStatusCode.OK;
            try
            {
                bookCatalogueResponse = bookCatalogueService.UpdateBook(bookToUpdate);
            }
            catch (Exception ex)
            {
                bookCatalogueResponse.systemErrors = new SystemException(ex.Message);
                statusCode = HttpStatusCode.InternalServerError;
            }

            HttpResponseMessage response = Request.CreateResponse(statusCode, bookCatalogueResponse);
            return ResponseMessage(response);
        }

        /// <summary>
        /// deleting the catalogue by matching the ISBN number
        /// </summary>
        /// <param name="bookToDelete"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteBook")]
        //We can also add [FromHeader] headers and validate the headers
        public IHttpActionResult DeleteBook([FromBody] BookDetails bookToDelete)
        {
            BookCatalogueResponse bookCatalogueResponse = new BookCatalogueResponse();

            HttpStatusCode statusCode = HttpStatusCode.OK;
            try
            {
                bookCatalogueResponse = bookCatalogueService.DeleteBook(bookToDelete);
            }
            catch (Exception ex)
            {
                bookCatalogueResponse.systemErrors = new SystemException(ex.Message);
                statusCode = HttpStatusCode.InternalServerError;
            }

            HttpResponseMessage response = Request.CreateResponse(statusCode, bookCatalogueResponse);
            return ResponseMessage(response);
        }
    }
}
