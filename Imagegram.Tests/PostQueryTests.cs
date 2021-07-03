using AutoMapper;
using Imagegram.Domain.PostsEntity;
using Imagegram.Models.Exceptions;
using Imagegram.Repository.Repository;
using Imagegram.Services.QueryHandlers;
using Moq;
using NUnit.Framework;
using System;

namespace Imagegram.Tests
{
    public class PostQueryTests
    {
        private Mock<IRepository<Posts>> db;
        private Mock<IMapper> mapper;

        [SetUp]
        public void Setup()
        {
            db = new Mock<IRepository<Posts>>();
            mapper = new Mock<IMapper>();
        }

        [Test]
        public void HandleAsync_PostNotFound_ThrowsPostNotFoundException()
        {
            var handler = new TestQueryHandler(db.Object, mapper.Object);

            Assert.Throws<PostNotFound>(() => handler.HandleAsync(new Models.Queries.PostQuery { PostId = Guid.Empty }));
        }
    }
}