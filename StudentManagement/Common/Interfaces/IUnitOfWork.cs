using System.Threading.Tasks;

namespace StudentManagement.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}