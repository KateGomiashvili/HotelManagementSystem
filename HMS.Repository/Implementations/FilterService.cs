using HMS.Models.Dtos.Hotels;
using HMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Repository.Implementations
{
    public class FilterService : IFilterService
    {
        public Task AddAsync(HotelForGettingDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<HotelForGettingDto, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<HotelForGettingDto>> GetAllAsync(Expression<Func<HotelForGettingDto, bool>> filter, int pageNumber, int pageSize, string includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<HotelForGettingDto>> GetAllAsync(int pageNumber, int pageSize, string includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public Task<HotelForGettingDto> GetAsync(Expression<Func<HotelForGettingDto, bool>> filter, string includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public void Remove(HotelForGettingDto entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<HotelForGettingDto> entities)
        {
            throw new NotImplementedException();
        }
    }
}
