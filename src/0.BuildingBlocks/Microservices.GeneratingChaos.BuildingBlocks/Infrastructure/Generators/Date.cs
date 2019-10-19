using Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.Generators.Interfaces;
using System;

namespace Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.Generators
{
    /// <inheritdoc/>
    public class Date : IDate
    {
        /// <inheritdoc/>
        public DateTime Now()
        {
            return DateTime.Now;
        }

        public DateTime Parse(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                throw new ArgumentNullException(nameof(date));
            }
            return DateTime.Parse(date);
        }
        public DateTime Parse(string date, DateTime alternative)
        {
            if (string.IsNullOrEmpty(date))
            {
                return alternative;
            }
            return DateTime.Parse(date);
        }

        public DateTime ParseFromUnix(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return Now();
            }

            return DateTimeOffset.FromUnixTimeSeconds(long.Parse(date)).DateTime;
        }
    }
}
