using PhoneDB.Model;

namespace PhoneDB.Repositories
{
    public interface IPhoneRepository
    {
        Task<IEnumerable<PhoneModel>> GetAllPhonesAsync();
        Task<PhoneModel?> GetPhoneByIdAsync(int id);
        Task<PhoneModel?> AddPhone(PhoneModel model);
    }
}
