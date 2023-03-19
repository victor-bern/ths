using ths.Shared.Entities;

namespace ths.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IProviderRepository
    {
        public Task<ICollection<Provider>> GetAllProvidersAsync(CancellationToken cancellationToken);
        public Task<Provider> GetProviderByIdAsync(int id, CancellationToken cancellationToken);
        public Task<int> CreateProviderAsync(Provider item, CancellationToken cancellationToken);
        public Task<int> UpdateProviderAsync(int id, Provider item, CancellationToken cancellationToken);
        public Task<int> DeleteProviderAsync(int id, CancellationToken cancellationToken);
    }
}
