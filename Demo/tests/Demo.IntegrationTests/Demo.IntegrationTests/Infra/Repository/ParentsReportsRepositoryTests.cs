using Demo.API;
using Demo.Infra.Data;
using Demo.Infra.Repository;
using Demo.Tests.Config;
using Demo.Tests.Fixtures;
using FluentAssertions;
using MongoDB.Bson;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests.Infra.Repository
{
    [Collection(nameof(IntegrationTestsFixtureCollection))]
    public class ParentsReportsRepositoryTests
    {
        private readonly IntegrationTestsFixture<Startup> _testsFixture;

        public ParentsReportsRepositoryTests(IntegrationTestsFixture<Startup> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Get Parents By Id")]
        [Trait("Category", "Repository Methods Tests")]
        public async Task GetParentsByIdAsyncTest()
        {
            //var getResponse = await _testsFixture.Client.GetStringAsync("api/researches/list-all"); // URL da API a ser testada
            

            /*
            var config = StartupApiTests.InitConfiguration();
            var context = new MongoDBContext(config);

            var research = _researchTestsFixture.CreateValidResearch();

            var researchRepository = new ResearchRepository(context);

            await researchRepository.AddAsync(research);

            ObjectId.TryParse(research.Id, out ObjectId id);
            var result = await researchRepository.GetByIdAsync(id);

            result.Id.Should().Be(research.Id, $"The research Id should be equals to: { research.Id }");
            */
        }
    }
}
