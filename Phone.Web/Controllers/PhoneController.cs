using Microsoft.AspNetCore.Mvc;
using Phone.Web.Exceptions;
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
        public async Task<Smartphone> GetPhoneById(int id)
        {
            var model = await repository.GetPhoneByIdAsync(id);

            if (model == null)
            {
                throw new PhoneNotFoundException(id);
            }

            return new Smartphone()
            {
                Id = model.Id,
                Producer = model.Producer,
                Model = model.Model,
                Price = model.Price
            };
        }

        [HttpPost]
        public async Task<Smartphone> AddSmartphone([FromBody]Smartphone phone)
        {
            if(String.IsNullOrWhiteSpace(phone.Model) || String.IsNullOrWhiteSpace(phone.Producer) || phone.Price < 0)
            {
                throw new BadRequestException();
            }

            var model = await repository.AddPhoneAsync(new PhoneDB.Model.PhoneModel()
            {
                Producer = phone.Producer,
                Model = phone.Model,
                Price = phone.Price
            });

            phone.Id = model.Id;
            return phone;
        }

        [HttpPut]
        public async Task<Smartphone> Update([FromBody] Smartphone phone)
        {
            if (String.IsNullOrWhiteSpace(phone.Model) || String.IsNullOrWhiteSpace(phone.Producer) || phone.Price < 0)
            {
                throw new BadRequestException();
            }

            var model = await repository.UpdatePhoneAsync(new PhoneDB.Model.PhoneModel()
            {
                Id = phone.Id,
                Producer = phone.Producer,
                Model = phone.Model,
                Price = phone.Price
            });

            if (model == null)
            {
                throw new PhoneNotFoundException(phone.Id);
            }

            return new Smartphone()
            {
                Id = model.Id,
                Producer = model.Producer,
                Model = model.Model,
                Price = model.Price
            };
        }

        [HttpDelete("{id:int}")]
        public async Task<bool> Remove(int id)
        {
            if(await repository.RemoveAsync(id) == false)
            {
                throw new PhoneNotFoundException(id);
            }

            return true;
        }
    }
}
