using System.Collections.Generic;
using System.Linq;
using Xunit;
using FunFaker;

namespace FunFaker.Tests
{
    public class SelectManyTests
    {
        private static List<string> names = new List<string>
        {
            "Adam",
            "Eve",
            "Luke"
        };

        private static List<string> surnames = new List<string>
        {
            "First",
            "Second",
            "Third"
        };

        [Fact]
        public void Enumerable_DoesCartesianProduct()
        {
            var names = SelectManyTests.names;
            var surnames = SelectManyTests.surnames;

            var results =
                from name in names
                from surname in surnames
                select name + " " + surname;

            var expectedResults = new List<string>
            {
                "Adam First",
                "Adam Second",
                "Adam Third",
                "Eve First",
                "Eve Second",
                "Eve Third",
                "Luke First",
                "Luke Second",
                "Luke Third"
            };
            Assert.Equal(expectedResults.Count, results.Count());
            Assert.All(results, result => expectedResults.Contains(result));
        }

        [Fact]
        public void Zippable_ZipsCollections()
        {
            var names = SelectManyTests.names.ToZippable();
            var surnames = SelectManyTests.surnames.ToZippable();

            var results =
                from name in names
                from surname in surnames
                select name + " " + surname;

            var expectedResults = new List<string>
            {
                "Adam First",
                "Eve Second",
                "Luke Third"
            };

            Assert.Equal(expectedResults.Count, results.Count());
            Assert.All(results, result => expectedResults.Contains(result));
        }
    }
}
