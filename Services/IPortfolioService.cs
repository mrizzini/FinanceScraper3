using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceScraper3.Models;


namespace FinanceScraper3.Services
{
    public interface IPortfolioService
    {
        Task<Portfolio[]> GetPortfolioSnapshotsAsync();

        Task<bool> TriggerSnapshotAsync(Portfolio newSnapshot);
    }
}

// This is an interface, there is no code, just the definition of the method that will return a Task<Portfolio[]>
// Task is used here b/c this method will be asynchronous, it may not be able to return the list of snapshots right away b/c it needs to talk to the database first
// Now we must create the actual service class.