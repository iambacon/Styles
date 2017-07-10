namespace IAmBacon.Domain.Tests.Services
{
    using IAmBacon.Data.Infrastructure;
    using IAmBacon.Domain.Services;
    using IAmBacon.Model.Common;
    using IAmBacon.Model.Entities;

    using Machine.Specifications;

    using Moq;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;

    using It = Machine.Specifications.It;

    [Subject("Category service")]

    public class When_saving_a_category : CategoryServiceContext
    {
        Establish context = () =>
            {
                Sut = Fixture.Create<CategoryService>();
                Entity.Name = "Smokey Bacon";
            };

        It should_set_the_seo_name = () => Entity.SeoName.ShouldEqual("smokey-bacon");
    }

    [Subject("Category Service")]

    public class When_saving_a_new_category : CategoryServiceContext
    {
        Establish context = () =>
            {
                Entity.Id = 0;
                repoMock = new Mock<IRepository<Category>>();
                Fixture.Inject(repoMock.Object);

                Sut = Fixture.Create<CategoryService>();
            };

        It should_add_the_category_to_the_db = () => repoMock.Verify(x => x.Add(Entity));

        It should_return_true = () => Result.Success.ShouldBeTrue();

        static Mock<IRepository<Category>> repoMock;
    }

    [Subject("Category Service")]
    public class When_saving_an_existing_category : CategoryServiceContext
    {
        Establish context = () =>
            {
                repoMock = new Mock<IRepository<Category>>();
                Fixture.Inject(repoMock.Object);

                Sut = Fixture.Create<CategoryService>();
            };

        It should_update_the_category_in_the_db = () => repoMock.Verify(x => x.Update(Entity));

        It should_return_true = () => Result.Success.ShouldBeTrue();

        static Mock<IRepository<Category>> repoMock;
    }

    public abstract class CategoryServiceContext
    {
        Establish context = () =>
            {
                Fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
                Entity = Fixture.Create<Category>();
            };

        Because of = () => Result = Sut.Save(Entity);

        protected static CategoryService Sut;
        protected static IFixture Fixture;
        protected static Category Entity;
        protected static IResult Result;
    }
}
