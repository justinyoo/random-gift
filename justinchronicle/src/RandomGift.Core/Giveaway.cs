using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomGift.Core
{
    /// <summary>
    /// This represents the entity for giveaway draw
    /// </summary>
    public class Giveaway : IGiveaway
    {
        private bool _disposed;

        /// <summary>
        /// Draws the winning numbers based on the number of entries.
        /// </summary>
        /// <param name="numberOfWinners">Number of winners.</param>
        /// <param name="numberOfEntries">Number of entries.</param>
        /// <returns>Returns the list of winners</returns>
        public async Task<List<int>> DrawAsync(int numberOfWinners, int numberOfEntries)
        {
            var results = await Task.Factory.StartNew(() => this.Draw(numberOfWinners, numberOfEntries)).ConfigureAwait(false);

            return results;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }

        private List<int> Draw(int numberOfWinners, int numberOfEntries)
        {
            var results = new List<int>();
            while (results.Count < numberOfWinners)
            {
                var result = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0) % numberOfEntries;
                if (!results.Contains((int)result))
                {
                    results.Add((int)result);
                }
            }

            return results;
        }
    }
}