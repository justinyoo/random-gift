using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomGift.Core
{
    /// <summary>
    /// This provides interfaces to the <see cref="Giveaway"/> class.
    /// </summary>
    public interface IGiveaway : IDisposable
    {
        /// <summary>
        /// Draws the winning numbers based on the number of entries.
        /// </summary>
        /// <param name="numberOfWinners">Number of winners.</param>
        /// <param name="numberOfEntries">Number of entries.</param>
        /// <returns>Returns the list of winners</returns>
        Task<List<int>> DrawAsync(int numberOfWinners, int numberOfEntries);
    }
}