using AutoMapper;
using BankSystem.Api.Services.Interfaces;
using BankSystem.App.Exceptions;
using BankSystem.Data.Storage.Interfaces;
using BankSystemDomain.Models;

namespace BankSystem.Api.Services.Implementations
{
    public class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto> where TEntity : Person
    {
        private readonly IBaseStorage<TEntity> _storage;
        private readonly IMapper _mapper;

        public BaseService(IBaseStorage<TEntity> storage, IMapper mapper)
        {
            _storage = storage;
            _mapper = mapper;
        }

        public virtual async Task AddAsync(TDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto), "DTO не может быть null.");

            var entity = _mapper.Map<TEntity>(dto);

            if (string.IsNullOrWhiteSpace(entity.PassportDetails))
                throw new PassportException("Паспортные данные отсутствуют.");

            await _storage.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(TDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto), "DTO не может быть null.");

            var entity = _mapper.Map<TEntity>(dto);

            if (entity.Age < 18)
                throw new AgeException("Возраст должен быть не менее 18 лет.");

            if (string.IsNullOrWhiteSpace(entity.PassportDetails))
                throw new PassportException("Паспортные данные отсутствуют.");

            return await _storage.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(TDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto), "DTO не может быть null.");

            var entity = _mapper.Map<TEntity>(dto);
            return await _storage.DeleteAsync(entity);
        }

        public async Task<IReadOnlyList<TDto>> GetEntitiesAsync(int pageNumber, int pageSize, Func<IQueryable<TEntity>, IQueryable<TEntity>> filter = null)
        {
            if (pageNumber <= 0) throw new ArgumentOutOfRangeException(nameof(pageNumber), "Номер страницы должен быть больше 0.");
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "Размер страницы должен быть больше 0.");

            var entities = await _storage.GetEntitiesAsync(pageNumber, pageSize, filter);
            return _mapper.Map<IReadOnlyList<TDto>>(entities);
        }

        public async Task<TDto> GetByIdAsync(Guid id)
        {
            var entity = await _storage.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }
    }
}