using PhoneDB.Model;

namespace PhoneDB.Repositories
{
    public interface IPhoneRepository
    {
        Task<IEnumerable<PhoneModel>> GetAllPhonesAsync();
        Task<PhoneModel?> GetPhoneByIdAsync(int id);
        Task<PhoneModel> AddPhoneAsync(PhoneModel model);
        Task<PhoneModel?> UpdatePhoneAsync(PhoneModel model);
        Task<bool> RemoveAsync(int id);
    }
}
