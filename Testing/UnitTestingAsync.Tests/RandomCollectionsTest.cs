using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using UnitTestingAsync;

namespace UnitTestingAsync.Tests
{
    public class RandomCollectionsTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetIntGetCollectionOf_1_2_elementsResultIsNotNull(int count)
        {
            Assert.NotNull(RandomCollections.GetInt(count));
        }
        [Theory]
        [InlineData(-1)]
        public void GetIntCollectionLengthMinus1ThrowOverflowException(int i)
        {
            Assert.Throws<OverflowException>(() => RandomCollections.GetInt(i));
        }
        [Theory]
        [InlineData(10, -10, 10)]
        [InlineData(20, 10, 20)]
        public async Task GetIntEnumAsync_CheckIsCollectionValid_CollectionIsValid(int count, int bottom, int top)
        {
            var result = new List<int>();
            await foreach (var digit in RandomCollections.GetIntEnumAsync(count, bottom, top))
            {
                result.Add(digit);
            }

            Assert.Equal(count, result.Count);
            Assert.True(result.All(i => i >= bottom && i < top));
        }
        [Theory]
        [InlineData(10, 10, -10)]
        public async Task GetIntEnumAsync_InvalidBottomAndTop_ArgumentException(int count, int bottom, int top)
        {
            var createAndProcessInvalidEnum = async () =>
            {
                foreach (var item in RandomCollections.GetInt(count, bottom, top))
                {

                }
            };

            await Assert.ThrowsAsync<ArgumentException>(createAndProcessInvalidEnum);
        }
    }
}