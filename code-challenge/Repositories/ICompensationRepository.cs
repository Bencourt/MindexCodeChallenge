using challenge.Models;
using System;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    //Compensation repository interface
    public interface ICompensationRepository
    {
        Compensation GetById(String id);
        Compensation Add(Compensation compensation);
        Task SaveAsync();
    }
}