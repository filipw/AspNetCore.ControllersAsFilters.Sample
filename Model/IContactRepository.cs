using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCore.ControllersAsFilters.Sample.Model
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAll();

        Task<Contact> Get(int id);

        Task<int> Add(Contact contact);

        Task Update(Contact updatedContact);

        Task Delete(int id);
    }
}
