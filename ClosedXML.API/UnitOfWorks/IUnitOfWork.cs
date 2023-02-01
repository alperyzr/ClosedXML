using ClosedXML.API.Repositories.Interfaces;

namespace ClosedXML.API.UnitOfWorks
{
    public interface IUnitOfWork
    {
        public ICovidRepository Covids { get; }

        void Save();
        Task SaveAsync();
    }
}
