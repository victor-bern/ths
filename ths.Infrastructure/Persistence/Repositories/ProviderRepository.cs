using Microsoft.EntityFrameworkCore;
using ths.Infrastructure.Exceptions;
using ths.Infrastructure.Persistence.Repositories.Interfaces;
using ths.Shared.Entities;

namespace ths.Infrastructure.Persistence.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly ApplicationDbContext _context;

        public ProviderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Provider>> GetAllProvidersAsync(CancellationToken cancellationToken) => await _context.Providers.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<Provider> GetProviderByIdAsync(int id, CancellationToken cancellationToken)
        {
            var provider = await _context.Providers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new ApiException("Fornecedor não encontrado.");
            return provider;
        }

        public async Task<int> CreateProviderAsync(Provider item, CancellationToken cancellationToken)
        {
            await _context.Providers.AddAsync(item, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return item.Id;
        }


        public async Task<int> UpdateProviderAsync(int id, Provider item, CancellationToken cancellationToken)
        {
            var provider = await _context.Providers
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new ApiException("Fornecedor não encontrado.");

            provider.CorporateName = item.CorporateName;
            provider.Address = item.Address;
            provider.Phone = item.Phone;
            provider.Document = item.Document;

            await _context.SaveChangesAsync(cancellationToken);

            return provider.Id;
        }

        public async Task<int> DeleteProviderAsync(int id, CancellationToken cancellationToken)
        {
            var provider = await _context.Providers
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new ApiException("Fornecedor não encontrado.");

            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync(cancellationToken);
            return provider.Id;

        }
    }
}
