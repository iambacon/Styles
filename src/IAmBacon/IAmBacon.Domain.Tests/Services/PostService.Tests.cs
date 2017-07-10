namespace IAmBacon.Domain.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using IAmBacon.Data.Infrastructure;
    using IAmBacon.Domain.Services;
    using IAmBacon.Model.Entities;

    using Machine.Specifications;

    using Moq;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;

    using It = Machine.Specifications.It;

    [Subject("Post service")]
    public class When_retrieving_a_post_by_seo_title : PostServiceContext
    {
        Establish context = () =>
            {
                searchTerm = "smokey-bacon";

                var posts = Fixture.Create<List<Post>>();
                posts.First().SeoTitle = searchTerm;

                repoMock = new Mock<IRepository<Post>>();
                repoMock.Setup(x => x.Find(Moq.It.IsAny<Expression<Func<Post, bool>>>()))
                    .Returns((Expression<Func<Post, bool>> predicate) => posts.Where(predicate.Compile()));

                Fixture.Inject(repoMock.Object);
                Sut = Fixture.Create<PostService>();
            };

        Because of = () => result = Sut.Get(searchTerm);

        It should_return_the_specified_post = () => result.SeoTitle.ShouldEqual(searchTerm);

        static Post result;
        static string searchTerm;
        static Mock<IRepository<Post>> repoMock;
    }

    [Subject("Post Service")]
    public class When_retrieving_all_posts : PostServiceContext
    {
        Establish context = () =>
            {
                var posts = Fixture.Create<List<Post>>();
                posts.First().Active = false;

                var repoMock = new Mock<IRepository<Post>>();
                repoMock.Setup(x => x.GetAll()).Returns(posts);

                Fixture.Inject(repoMock.Object);
                Sut = Fixture.Create<PostService>();
            };

        Because of = () => result = Sut.GetAllActive();

        It should_return_all_active_posts = () => result.Count().ShouldEqual(2);

        static IEnumerable<Post> result;
    }

    [Subject("Post Service")]
    public class When_retrieving_the_latest_posts : PostServiceContext
    {
        Establish context = () =>
            {
                var posts = Fixture.CreateMany<Post>(4).ToList();
                posts.First().Active = false;

                expectedOrder = posts.Where(x => x.Active).OrderByDescending(x => x.Id).Take(NoOfPosts);

                var repoMock = new Mock<IRepository<Post>>();
                repoMock.Setup(x => x.GetAll()).Returns(posts);

                Fixture.Inject(repoMock.Object);

                Sut = Fixture.Create<PostService>();
            };

        Because of = () => result = Sut.GetLatest(NoOfPosts);

        It should_return_the_number_specified = () => result.Count().ShouldEqual(NoOfPosts);

        It should_return_active_posts = () => result.Any(x => x.Active == false).ShouldBeFalse();

        It should_order_posts_by_id_descending = () => result.SequenceEqual(expectedOrder).ShouldBeTrue();

        static IEnumerable<Post> result;
        static IEnumerable<Post> expectedOrder;
        const int NoOfPosts = 2;
    }
    
    public abstract class PostServiceContext
    {
        Establish context = () =>
            {
                Fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
                Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
                
            };
        
        protected static IFixture Fixture;
        protected static PostService Sut;
    }
}
