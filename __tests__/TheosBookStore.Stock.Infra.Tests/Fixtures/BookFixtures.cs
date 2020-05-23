using System;
using System.Collections.Generic;
using TheosBookStore.Stock.Domain.Entities;
using TheosBookStore.Stock.Domain.ValueObjects;
using TheosBookStore.Stock.Infra.Models;
using Xunit;

namespace TheosBookStore.Stock.Infra.Tests.Fixtures
{
    public class BookFixtures
    {
        public BookFixtures()
        {
            _authorFixtures = new AuthorFixtures();
            _publisherFixtures = new PublisherFixtures();
        }

        private const string ISBN_VALID = "123456789012";
        private readonly AuthorFixtures _authorFixtures;
        private readonly PublisherFixtures _publisherFixtures;

        public Book GetEntity()
        {
            var entity = new Book(
                Faker.Name.FullName(),
                new ISBN(ISBN_VALID),
                new List<Author>
                {
                    _authorFixtures.GetEntity(),
                    _authorFixtures.GetEntity()
                },
                Faker.RandomNumber.Next(100),
                _publisherFixtures.GetEntity(),
                Faker.RandomNumber.Next(0, 2020),
                Faker.RandomNumber.Next(1, 100),
                Faker.Address.City()
            );
            return entity;
        }

        internal BookModel GetModel()
        {
            var publisher = _publisherFixtures.GetModel();
            var model = new BookModel
            {
                Id = Faker.RandomNumber.Next(1, 100),
                Title = Faker.Name.FullName(),
                ISBN = ISBN_VALID,
                PageCount = Faker.RandomNumber.Next(1, 100),
                PublisherId = publisher.Id,
                Year = Faker.RandomNumber.Next(1, 2020),
                Edition = Faker.RandomNumber.Next(1, 50),
                City = Faker.Address.City(),
                Publisher = publisher,
                Authors = new List<AuthorModel>
                {
                    _authorFixtures.GetModel(),
                    _authorFixtures.GetModel(),
                }
            };
            return model;
        }
    }
}
