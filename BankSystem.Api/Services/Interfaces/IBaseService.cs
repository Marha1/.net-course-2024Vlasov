namespace BankSystem.Api.Services.Interfaces
{
    public interface IBaseService<TEntity, TDto>
    {
        Task AddAsync(TDto dto);
        Task<bool> UpdateAsync(TDto dto);
        Task<bool> DeleteAsync(TDto dto);
        Task<IReadOnlyList<TDto>> GetEntitiesAsync(int pageNumber, int pageSize, Func<IQueryable<TEntity>, IQueryable<TEntity>> filter = null);
        Task<TDto> GetByIdAsync(Guid id);
    }
}