using $ext_projectname$.Application.Interfaces;
using $ext_projectname$.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace $safeprojectname$.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IMockRepository<Person> _dataGeneratorService;

        public PersonRepository(IMockRepository<Person> dataGeneratorService)
        {
            _dataGeneratorService = dataGeneratorService;
        }
        public async Task<IEnumerable<Person>> GetAllAsync(int pageNumber, int pageSize)
        {

            //Example of custom data
            GenFu.GenFu.Configure<Person>()
                .Fill(p => p.NumberOfKids)
                .WithinRange(1, 25);
            IEnumerable<Person> result = await _dataGeneratorService.Collection(100);
            return result.Skip((pageNumber - 1) * pageSize).Take(pageSize); 
        }

        public async Task<Person> GetByIdAsync(Guid id)
        {
            throw await Task.Run(() => new NotImplementedException());
        }


        public async Task<Guid> AddAsync(Person entity)
        {
            throw await Task.Run(() => new NotImplementedException());
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            throw await Task.Run(() => new NotImplementedException());
        }


        public async Task<int> UpdateAsync(Person entity)
        {
            throw await Task.Run(() => new NotImplementedException());

        }


    }
}
