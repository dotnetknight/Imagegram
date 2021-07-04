using Imagegram.API.Controllers;
using Imagegram.Infrastructure.Bus.Command;
using Imagegram.Infrastructure.Bus.Query;
using Imagegram.Models.ResourceParameters;
using Imagegram.Models.Responses.Query;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Imagegram.Tests
{
    [TestFixture]
    public class PostsControllerTests
    {
        private Mock<ICommandBusAsync> commandBus;
        private Mock<IQueryBusAsync> queryBus;
        private PostsController postsController;

        [SetUp]
        public void Setup()
        {
            commandBus = new Mock<ICommandBusAsync>();
            queryBus = new Mock<IQueryBusAsync>();
            postsController = new PostsController(queryBus.Object, commandBus.Object);
        }

        [Test]
        public async Task Posts_WhenCalled_ReturnPostsWithLastThreeCommentsAsync()
        {
            var result = await postsController.Posts(new PostsResourceParameters());
            Assert.That(result, Is.TypeOf<ActionResult<PostQueryResponse>>());
        }
    }
}
