
using Business.Handlers.WareHouseProductMappings.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.WareHouseProductMappings.Queries.GetWareHouseProductMappingQuery;
using Entities.Concrete;
using static Business.Handlers.WareHouseProductMappings.Queries.GetWareHouseProductMappingsQuery;
using static Business.Handlers.WareHouseProductMappings.Commands.CreateWareHouseProductMappingCommand;
using Business.Handlers.WareHouseProductMappings.Commands;
using Business.Constants;
using static Business.Handlers.WareHouseProductMappings.Commands.UpdateWareHouseProductMappingCommand;
using static Business.Handlers.WareHouseProductMappings.Commands.DeleteWareHouseProductMappingCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class WareHouseProductMappingHandlerTests
    {
        Mock<IWareHouseProductMappingRepository> _wareHouseProductMappingRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _wareHouseProductMappingRepository = new Mock<IWareHouseProductMappingRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task WareHouseProductMapping_GetQuery_Success()
        {
            //Arrange
            var query = new GetWareHouseProductMappingQuery();

            _wareHouseProductMappingRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<WareHouseProductMapping, bool>>>())).ReturnsAsync(new WareHouseProductMapping()
//propertyler buraya yazılacak
//{																		
//WareHouseProductMappingId = 1,
//WareHouseProductMappingName = "Test"
//}
);

            var handler = new GetWareHouseProductMappingQueryHandler(_wareHouseProductMappingRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.WareHouseProductMappingId.Should().Be(1);

        }

        [Test]
        public async Task WareHouseProductMapping_GetQueries_Success()
        {
            //Arrange
            var query = new GetWareHouseProductMappingsQuery();

            _wareHouseProductMappingRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<WareHouseProductMapping, bool>>>()))
                        .ReturnsAsync(new List<WareHouseProductMapping> { new WareHouseProductMapping() { /*TODO:propertyler buraya yazılacak WareHouseProductMappingId = 1, WareHouseProductMappingName = "test"*/ } });

            var handler = new GetWareHouseProductMappingsQueryHandler(_wareHouseProductMappingRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<WareHouseProductMapping>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task WareHouseProductMapping_CreateCommand_Success()
        {
            WareHouseProductMapping rt = null;
            //Arrange
            var command = new CreateWareHouseProductMappingCommand();
            //propertyler buraya yazılacak
            //command.WareHouseProductMappingName = "deneme";

            _wareHouseProductMappingRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<WareHouseProductMapping, bool>>>()))
                        .ReturnsAsync(rt);

            _wareHouseProductMappingRepository.Setup(x => x.Add(It.IsAny<WareHouseProductMapping>())).Returns(new WareHouseProductMapping());

            var handler = new CreateWareHouseProductMappingCommandHandler(_wareHouseProductMappingRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _wareHouseProductMappingRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task WareHouseProductMapping_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateWareHouseProductMappingCommand();
            //propertyler buraya yazılacak 
            //command.WareHouseProductMappingName = "test";

            _wareHouseProductMappingRepository.Setup(x => x.Query())
                                           .Returns(new List<WareHouseProductMapping> { new WareHouseProductMapping() { /*TODO:propertyler buraya yazılacak WareHouseProductMappingId = 1, WareHouseProductMappingName = "test"*/ } }.AsQueryable());

            _wareHouseProductMappingRepository.Setup(x => x.Add(It.IsAny<WareHouseProductMapping>())).Returns(new WareHouseProductMapping());

            var handler = new CreateWareHouseProductMappingCommandHandler(_wareHouseProductMappingRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task WareHouseProductMapping_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateWareHouseProductMappingCommand();
            //command.WareHouseProductMappingName = "test";

            _wareHouseProductMappingRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<WareHouseProductMapping, bool>>>()))
                        .ReturnsAsync(new WareHouseProductMapping() { /*TODO:propertyler buraya yazılacak WareHouseProductMappingId = 1, WareHouseProductMappingName = "deneme"*/ });

            _wareHouseProductMappingRepository.Setup(x => x.Update(It.IsAny<WareHouseProductMapping>())).Returns(new WareHouseProductMapping());

            var handler = new UpdateWareHouseProductMappingCommandHandler(_wareHouseProductMappingRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _wareHouseProductMappingRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task WareHouseProductMapping_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteWareHouseProductMappingCommand();

            _wareHouseProductMappingRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<WareHouseProductMapping, bool>>>()))
                        .ReturnsAsync(new WareHouseProductMapping() { /*TODO:propertyler buraya yazılacak WareHouseProductMappingId = 1, WareHouseProductMappingName = "deneme"*/});

            _wareHouseProductMappingRepository.Setup(x => x.Delete(It.IsAny<WareHouseProductMapping>()));

            var handler = new DeleteWareHouseProductMappingCommandHandler(_wareHouseProductMappingRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _wareHouseProductMappingRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

