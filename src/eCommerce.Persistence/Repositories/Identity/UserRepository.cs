namespace eCommerce.Persistence.Repositories.Identity;

public sealed class UserRepository : IUserRepository
{
    public Task<Response> CreateAsync(User user, CancellationToken ct) =>
        throw new NotImplementedException();

    public Task<Response> UpdateAsync(User user, CancellationToken ct) =>
        throw new NotImplementedException();

    public Task<Response> DeleteAsync(User user, CancellationToken ct) =>
        throw new NotImplementedException();

    public Task<Response> GetAsync(ISpecification<User> specification, CancellationToken ct) =>
        throw new NotImplementedException();

    public Task<Response> GetAllAsync(ISpecification<User> specification, CancellationToken ct) =>
        throw new NotImplementedException();
}
