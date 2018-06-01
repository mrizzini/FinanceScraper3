using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceScraper3.Data;
using FinanceScraper3.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceScraper3.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly ApplicationDbContext _context;

        public PortfolioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio[]> GetPortfolioSnapshotsAsync()
        {
            return await _context.Portfolios.ToArrayAsync();
        }


    }
}