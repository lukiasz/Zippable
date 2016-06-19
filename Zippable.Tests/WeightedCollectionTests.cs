using System;
using System.Linq;
using Xunit;

namespace Zippable.Tests
{
    public class WeightedCollectionTests
    {
        public enum Region
        {
            Asia,
            Africa,
            Europe,
            NorthAmerica,
            SouthAmerica,
            AustraliaAndOceania,
            Antarctica
        }

        [Fact]
        public void WeightedCollection_ReturnsElementsAccordingToWeights()
        {
            var totalCount = 100;
            var seed = 1;
            Config.Random = new Random(seed);

            var results = Extensions.PossibleEnumValues<Region>()
                .ToList()
                .ToWeightedCollection(region =>
                {
                    switch (region)
                    {
                        case Region.Asia:
                            return 60.3;
                        case Region.Africa:
                            return 14.5;
                        case Region.Europe:
                            return 11.4;
                        case Region.NorthAmerica:
                            return 7.6;
                        case Region.SouthAmerica:
                            return 5.6;
                        case Region.AustraliaAndOceania:
                            return 0.5;
                        case Region.Antarctica:
                            return 0.00002;
                        default:
                            throw new Exception();
                    }
                })
                .Take(totalCount);

            var asians = results.Where(p => p == Region.Asia).ToList();
            var africans = results.Where(p => p == Region.Africa).ToList();

            Assert.Equal(62, asians.Count);
            Assert.Equal(17, africans.Count);
        }
    }
}
