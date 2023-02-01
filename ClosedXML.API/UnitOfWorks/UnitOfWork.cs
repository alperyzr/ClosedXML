using ClosedXML.API.Repositories;
using ClosedXML.API.Repositories.Interfaces;
using System;

namespace ClosedXML.API.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ICovidRepository Covids => new CovidRepository(_context);
        

        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
