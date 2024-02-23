namespace eCommerce.Domain.Abstractions.Repositories.Identity;

public interface IUserRepository
{
    Task<Response> CreateAsync(User user, CancellationToken ct);
    Task<Response> UpdateAsync(User user, CancellationToken ct);
    Task<Response> DeleteAsync(User user, CancellationToken ct);
    Task<Response> GetAsync(ISpecification<User> specification, CancellationToken ct);
    Task<Response> GetAllAsync(ISpecification<User> specification, CancellationToken ct);
}
