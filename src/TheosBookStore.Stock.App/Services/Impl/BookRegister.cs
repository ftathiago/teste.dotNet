using System.Linq;

using TheosBookStore.Stock.App.Factories;
using TheosBookStore.Stock.App.Models;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.Repositories;
using TheosBookStore.Stock.Domain.Services;

namespace TheosBookStore.Stock.App.Services.Impl
{
    public class BookRegister : BookServiceBase, IBookRegister
    {
        public BookRegister(IBookFactory bookFactory, IBookServices bookServices,
            IBookRepository repository)
            : base(bookFactory, bookServices, repository) { }

        public override BookResponse Execute(BookRequest bookInsertRequest)
        {
            Book book = _factory.FromRequest(bookInsertRequest);
            if (!book.IsValid())
            {
                book.ValidationResult.Errors.ToList().ForEach(error =>
                    AddErrorMessage(error.ErrorMessage));
                return EmptyResponse();
            }
            _services.Register(book);
            if (!_services.IsValid)
            {
                AddErrorMessage(_services.GetErrorMessages());
                return EmptyResponse();
            }

            var registeredBook = _repository.GetByISBN(book.ISBN);
            return _factory.FromEntityToResponse(registeredBook);
        }
    }
}
