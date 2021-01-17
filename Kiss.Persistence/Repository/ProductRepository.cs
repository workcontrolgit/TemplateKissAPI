using Microsoft.Extensions.Configuration;
using $ext_projectname$.Application.Interfaces;
using $ext_projectname$.Domain.Entities;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace $safeprojectname$.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly QueryFactory _db;

        public ProductRepository(IConfiguration configuration, QueryFactory db)
        {
            _configuration = configuration;
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize)
        {
            var result = await _db.Query("Products").ForPage(pageNumber, pageSize).GetAsync<Product>();
            return result;

        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var result = await _db.Query("Products").Where("Id", "=", id)
                .FirstOrDefaultAsync<Product>();
            return result;
        }


        public async Task<Guid> AddAsync(Product entity)
        {
            entity.AddedOn = DateTime.Now;
            entity.Id = await SetPrimaryKey(entity.Id);

            var affectedRows = await _db.Query("Products").InsertAsync(new
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Barcode = entity.Barcode,
                Rate = entity.Rate,
                AddedOn = entity.AddedOn
            });

            if (affectedRows != 1)
                // insert failed, return Guid structure all zero (0).  Front end should check status for empty Guid
                return Guid.Empty;
            
            //return Guid of new insert row
            return entity.Id;

        }

        public async Task<int> DeleteAsync(Guid id)
        {

            var affectedRows = await _db.Query("Products").Where("Id", "=", id).DeleteAsync();
            return affectedRows;

        }


        public async Task<int> UpdateAsync(Product entity)
        {
            entity.ModifiedOn = DateTime.Now;

            var affectedRows = await _db.Query("Products").Where("Id", entity.Id).UpdateAsync(new
            {
                Name = entity.Name,
                Description = entity.Description,
                Barcode = entity.Barcode,
                Rate = entity.Rate,
                ModifiedOn = entity.ModifiedOn,
            });

            return affectedRows;

        }

        /// <summary>
        /// Utility to setup primary key
        /// </summary>
        private async Task<Guid> SetPrimaryKey(Guid Id)
        {
            // set default key value
            var defaultKey = Guid.NewGuid();

            // check provided id is Guid format
            bool isGuid = Guid.TryParse(Id.ToString(), out Id);

            if (isGuid)
            {
                //use provided key if it has has not been used.
                if (!await IsUsedPrimaryKey(Id))
                {
                    // change defaultKey to provided Id
                    defaultKey = Id;
                }
            }
            return defaultKey;
        }


        /// <summary>
        /// Check used primary key
        /// </summary>
        private async Task<bool> IsUsedPrimaryKey(Guid Id)
        {
            var result = await _db.Query("Products").Where("Id", "=", Id)
                .FirstOrDefaultAsync<Product>();

            return result != null;
        }

    }
}
