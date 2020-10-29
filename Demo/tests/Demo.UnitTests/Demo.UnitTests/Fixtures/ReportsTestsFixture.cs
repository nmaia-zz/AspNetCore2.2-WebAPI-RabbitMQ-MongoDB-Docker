using Bogus;
using Bogus.DataSets;
using Demo.Business.Reports;
using Moq.AutoMock;
using System;
using Xunit;
using DOMAIN = Demo.Domain.Entities;

namespace Demo.Tests.Fixtures
{
    [CollectionDefinition(nameof(ReportsCollection))]
    public class ReportsCollection : ICollectionFixture<ReportsTestsFixture> { }

    public class ReportsTestsFixture : IDisposable
    {
        public FamilyTreeReports _familyTreeReports;
        public AutoMocker Mocker;

        public FamilyTreeReports GetFamilyTreeReports()
        {
            Mocker = new AutoMocker();
            _familyTreeReports = Mocker.CreateInstance<FamilyTreeReports>();

            return _familyTreeReports;
        }

        public DOMAIN.AncestorsReport GetAncestorsReport() 
        {
            var gender = new Faker().PickRandom<Name.Gender>();

            var father = $"{ new Faker().Name.FirstName(Name.Gender.Male) } { new Faker().Name.LastName(Name.Gender.Male) }";
            var mother = $"{ new Faker().Name.FirstName(Name.Gender.Female) } { new Faker().Name.LastName(Name.Gender.Female) }";

            var ancestors = new Faker<DOMAIN.AncestorsReport>("en").CustomInstantiator(f => new DOMAIN.AncestorsReport(

                $"{ f.Name.FirstName(gender) } { f.Name.LastName(gender) }",
                new string[2] { father, mother },
                $"{ father }, { mother }"

                ));

            return ancestors;
        }

        public DOMAIN.ParentsReport GetParentsReport()
        {
            var gender = new Faker().PickRandom<Name.Gender>();

            var father = $"{ new Faker().Name.FirstName(Name.Gender.Male) } { new Faker().Name.LastName(Name.Gender.Male) }";
            var mother = $"{ new Faker().Name.FirstName(Name.Gender.Female) } { new Faker().Name.LastName(Name.Gender.Female) }";

            var parents = new Faker<DOMAIN.ParentsReport>("en").CustomInstantiator(f => new DOMAIN.ParentsReport(

                $"{ f.Name.FirstName(gender) } { f.Name.LastName(gender) }",
                $"{ father }, { mother }"

                ));

            return parents;
        }

        public DOMAIN.ChildrenReport GetChildrenReport()
        {
            var gender = new Faker().PickRandom<Name.Gender>();

            var child = $"{ new Faker().Name.FirstName(Name.Gender.Male) } { new Faker().Name.LastName(Name.Gender.Male) }";

            var children = new Faker<DOMAIN.ChildrenReport>("en").CustomInstantiator(f => new DOMAIN.ChildrenReport(

                $"{ f.Name.FirstName(gender) } { f.Name.LastName(gender) }",
                new string[1] { child }

                ));

            return children;
        }

        public void Dispose()
        {

        }
    }
}
