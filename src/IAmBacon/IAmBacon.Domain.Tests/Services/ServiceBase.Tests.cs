namespace IAmBacon.Domain.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using IAmBacon.Data.Infrastructure;
    using IAmBacon.Domain.Services;
    using IAmBacon.Model.Common;
    using IAmBacon.Model.Entities;

    using Machine.Specifications;

    using Moq;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;

    using It = Machine.Specifications.It;

    [Subject("Service base")]
    public class When_repository_argument_null : ServiceContext
    {
        Because of = () => exception = Catch.Exception(() => Sut = new PostService(null, null));

        It should_throw_exception = () => exception.ShouldNotBeNull();

        It should_be_of_type_ArgumentNullException = () => exception.ShouldBeOfExactType<ArgumentNullException>();

        static Exception exception;
    }

    [Subject("Service base")]
    public class When_unit_of_work_argument_null : ServiceContext
    {
        Because of = () => exception = Catch.Exception(() => Sut = new PostService(Fixture.Create<IRepository<Post>>(), null));

        It should_throw_exception = () => exception.ShouldNotBeNull();

        It should_be_of_type_ArgumentNullException = () => exception.ShouldBeOfExactType<ArgumentNullException>();

        static Exception exception;
    }

    [Subject("Service base")]
    public class When_adding_an_entity : ServiceContext
    {
        Establish context = () =>
            {
                entity = Fixture.Create<Post>();
                entity.Id = 0;

                repoMock = new Mock<IRepository<Post>>();
                Fixture.Inject(repoMock.Object);

                Sut = Fixture.Create<PostService>();
            };

        Because of = () => Sut.Create(entity);

        It should_add_to_the_repository = () => repoMock.Verify(x => x.Add(Moq.It.IsAny<Post>()));

        static Post entity;
        static Mock<IRepository<Post>> repoMock;
    }

    [Subject("Service base")]
    public class When_deleting_an_entity : ServiceContext
    {
        Establish context = () =>
            {
                entity = Fixture.Create<Post>();

                repoMock = new Mock<IRepository<Post>>();
                Fixture.Inject(repoMock.Object);

                Sut = Fixture.Create<PostService>();
            };

        Because of = () => result = Sut.Delete(entity);

        It should_delete_from_the_repository = () => repoMock.Verify(x => x.Delete(Moq.It.IsAny<Post>()));

        It should_return_success_result = () => result.Success.ShouldBeTrue();

        static Post entity;
        static Mock<IRepository<Post>> repoMock;
        static IResult result;
    }

    [Subject("Service base")]
    public class When_retrieving_an_entity_by_id : ServiceContext
    {
        Establish context = () =>
            {
                var posts = Fixture.Create<List<Post>>();
                entityId = posts[1].Id;

                var repoMock = new Mock<IRepository<Post>>();

                repoMock
                .Setup(x => x.Find(Moq.It.IsAny<object[]>()))
                .Returns((object[] o) => posts.FirstOrDefault(x => x.Id == (int)o[0]));

                Fixture.Inject(repoMock.Object);
                Sut = Fixture.Create<PostService>();
            };

        Because of = () => result = Sut.Get(entityId);

        It should_return_the_entity_for_the_specified_id = () => result.Id.ShouldEqual(entityId);

        static Post result;
        static int entityId;
    }

    [Subject("Service base")]
    public class When_retrieving_an_entity_by_predicate : ServiceContext
    {
        Establish context = () =>
            {
                var posts = Fixture.Create<List<Post>>();
                entityId = posts[1].Id;

                var repoMock = new Mock<IRepository<Post>>();

                repoMock.Setup(x => x.Find(Moq.It.IsAny<Expression<Func<Post, bool>>>()))
                    .Returns((Expression<Func<Post, bool>> predicate) => posts.Where(predicate.Compile()));

                Fixture.Inject(repoMock);

                Sut = Fixture.Create<PostService>();
            };

        Because of = () => result = Sut.Get(x => x.Id == entityId);

        It should_return_the_entity_for_the_specified_predicate = () => result.First().Id.ShouldEqual(entityId);

        static IEnumerable<Post> result;
        static int entityId;
    }

    public abstract class ServiceContext
    {
        Establish context = () =>
            {
                Fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
                Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            };

        protected static IFixture Fixture;
        protected static ServiceBase<Post> Sut;
    }
}
