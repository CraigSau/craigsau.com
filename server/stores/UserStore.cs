using Microsoft.AspNetCore.Identity;
using server.models;
using server.dataaccess;

namespace server.stores;

public class UserStore : IUserStore<User>, IUserPasswordStore<User>, IUserEmailStore<User>
{
    private readonly IConfiguration _configuration;

    private readonly AccountDataService _dataService;

    public UserStore(
        IConfiguration configuration,
        AccountDataService dataService
    )
    {
        _configuration = configuration;
        _dataService = dataService;
    }

    public void Dispose() { }

    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (user == null) throw new ArgumentNullException(nameof(user));

        return await _dataService.CreateAsync(user);
    }

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (user == null) throw new ArgumentNullException(nameof(user));

        return await _dataService.DeleteAsync(user);
    }

    public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
    {
        throw new NotImplementedException();
    }

    public Task SetNormalizedUserNameAsync(User? user, string? normalizedName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (normalizedName == null) throw new ArgumentNullException(nameof(normalizedName));

        user.NormalizedUserName = normalizedName;
        return Task.FromResult<object>(null);
    }

    public Task<string?> GetNormalizedUserNameAsync(User? user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }


    public async Task<User?> FindByNameAsync(string? userName, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (userName == null) throw new ArgumentNullException(nameof(userName));

        return await _dataService.FindByNameAsync(userName);
    }

    public Task SetUserNameAsync(User? user, string? userName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (userName == null) throw new ArgumentNullException(nameof(userName));

        throw new NotImplementedException();

    }

    public Task<string?> GetUserNameAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (user == null) throw new ArgumentNullException(nameof(user));

        return Task.FromResult(user.UserName);
    }

    public async Task<User?> FindByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (id == null) throw new ArgumentNullException(nameof(id));

        Guid idGuid;
        if (!Guid.TryParse(id, out idGuid))
        {
            throw new ArgumentException("Not a valid Guid Id.", nameof(id));
        }

        return await _dataService.FindByIdAsync(id);
    }

    public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (user == null) throw new ArgumentNullException(nameof(user));

        return Task.FromResult(user.Id.ToString());
    }


    public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetPasswordHashAsync(User? user, string? passwordHash, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));

        user.PasswordHash = passwordHash;
        return Task.FromResult<object>(null);

    }

    public Task<string?> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (user == null) throw new ArgumentNullException(nameof(user));

        return Task.FromResult(user.PasswordHash);
    }

    public Task<string?> GetEmailAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (user == null) throw new ArgumentNullException(nameof(user));

        return Task.FromResult(user.Email);
    }

    public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (email == null) throw new ArgumentNullException(nameof(email));

        user.Email = email;
        return Task.FromResult<object>(null);
    }

    public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (user == null) throw new ArgumentNullException(nameof(user));

        return Task.FromResult(user.EmailConfirmed);
    }

    public Task SetEmailConfirmedAsync(User user, bool emailConfirmed, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (user == null) throw new ArgumentNullException(nameof(user));

        user.EmailConfirmed = emailConfirmed;
        return Task.FromResult<object>(null);
    }

    public Task<string?> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken) 
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (user == null) throw new ArgumentNullException(nameof(user));

        return Task.FromResult(user.NormalizedEmail);
    }

    public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (normalizedEmail == null) throw new ArgumentNullException(nameof(normalizedEmail));

        user.NormalizedEmail = normalizedEmail;
        return Task.FromResult<object>(null);
    }

    public async Task<User?> FindByEmailAsync(string? email, CancellationToken cancellationToken = default(CancellationToken))
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (email == null) throw new ArgumentNullException(nameof(email));

        return await _dataService.FindByEmailAsync(email);
    }


}
