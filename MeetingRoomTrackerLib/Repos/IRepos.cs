using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomTrackerLib.Repos
{
    public interface IRepos<T>
    {
        T Add(T entityToBeAdded);
        IEnumerable<T> GetAll();
        T? GetById(int id);
        T? Update(T entityToBeUpdated, int id);
        T? Delete(int id);
    }
}
