using Microsoft.AspNetCore.Mvc;
using ths.Infrastructure.Persistence.Repositories.Interfaces;
using ths.Server.Models;
using ths.Server.Validators;
using ths.Shared.Entities;

namespace ths.Server.Controllers
{
    [ApiController]
    [Route("/api/providers")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderRepository _providerRepository;

        public ProviderController(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken) => Ok(await _providerRepository.GetAllProvidersAsync(cancellationToken));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken) => Ok(await _providerRepository.GetProviderByIdAsync(id, cancellationToken));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddOrUpdateProviderDto model, CancellationToken cancellationToken)
        {
            var provider = new Provider
            {
                CorporateName = model.CorporateName,
                Address = model.Address,
                Phone = model.Phone,
                Document = model.Document,
            };
            return Ok(await _providerRepository.CreateProviderAsync(provider, cancellationToken));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AddOrUpdateProviderDto model, CancellationToken cancellationToken)
        {
            var validator = new AddOrUpdateProviderValidator();

            var modelValidated = await validator.ValidateAsync(model, cancellationToken);

            if (!modelValidated.IsValid)
            {
                return BadRequest(modelValidated.Errors);
            }

            var provider = new Provider
            {
                CorporateName = model.CorporateName,
                Address = model.Address,
                Phone = model.Phone,
                Document = model.Document,
            };
            return Ok(await _providerRepository.UpdateProviderAsync(id, provider, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _providerRepository.DeleteProviderAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
