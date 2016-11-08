using System.Linq;

using FluentAssertions;

using Xunit;

namespace RandomGift.Core.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="Giveaway"/> class.
    /// </summary>
    public class GiveawayTest
    {
        /// <summary>
        /// Test whether the method should return results or not.
        /// </summary>
        /// <param name="numberOfWinners">Number of winners.</param>
        /// <param name="numberOfEntries">Number of entries.</param>
        [Theory]
        [InlineData(1, 10)]
        [InlineData(2, 500)]
        public async void Given_Numbers_Draw_ShouldReturn_Results(int numberOfWinners, int numberOfEntries)
        {
            var ga = new Giveaway("sample.csv");
            var results = await ga.DrawAsync(numberOfWinners, numberOfEntries).ConfigureAwait(false);

            results.Should().HaveCount(numberOfWinners);
            results.Select(p => p.Should().BeInRange(0, numberOfEntries));
        }
    }
}