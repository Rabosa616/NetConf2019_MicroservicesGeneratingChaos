namespace Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.DataBase
{
    /// <summary>
    /// Class to parse Database configuration
    /// </summary>
    public class DbConfiguration
    {
        /// <summary>
        /// GetAsync or Set the Connection String
        /// </summary>
        public string DbConnectionString { get; set; }

        /// <summary>
        /// GetAsync or Set the Database Name
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>Gets or sets the retry count.</summary>
        /// <value>The retry count.</value>
        public int RetryCount { get; set; }
    }
}
