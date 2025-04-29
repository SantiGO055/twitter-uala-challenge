using Npgsql;

namespace TwitterUalaChallenge.Infrastructure.Persistence;

public interface IReadOnlyQuery
{
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null);
    Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null);
    Task<T> ExecuteAsync<T>(Func<NpgsqlConnection, Task<T>> operation);
}