using Microsoft.AspNetCore.Mvc;
using Phone.Web.Model;
using PhoneDB.Repositories;

namespace Phone.Web.Controllers
{
    [ApiController]
    [Route("phones")]
    public class PhoneController : ControllerBase
    {
        private IPhoneRepository repository;
        public PhoneController(IPhoneRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("")]
        public async Task<List<Smartphone>> Get()
        {
            var models = await repository.GetAllPhonesAsync();

            var phones = new List<Smartphone>();

            foreach(var model in models)
            {
                phones.Add(new Smartphone()
                {
                    Id = model.Id,
                    Producer = model.Producer,
                    Model = model.Model,
                    Price = model.Price
                });
            }
            return phones;
        }

        [HttpGet("{id:int}")]
        public async Task<Smartphone?> GetPhoneById(int id)
        {
            var model = await repository.GetPhoneByIdAsync(id);

            if (model == null)
            {
                return null;
            }

            return new Smartphone()
            {
                Id= model.Id,
                Producer = model.Producer,
                Model = model.Model,
                Price = model.Price
            };
        }
    }
}
