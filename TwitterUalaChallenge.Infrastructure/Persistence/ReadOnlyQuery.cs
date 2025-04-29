using Npgsql;
using Dapper;

namespace TwitterUalaChallenge.Infrastructure.Persistence;

public class ReadOnlyQuery : IReadOnlyQuery, IDisposable
{
    private readonly NpgsqlConnection _connection;
    private bool _disposed = false;

    public ReadOnlyQuery(string connectionString)
    {
        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
    {
        ThrowIfDisposed();
        return await _connection.QueryAsync<T>(sql, param);
    }

    public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null)
    {
        ThrowIfDisposed();
        return await _connection.QuerySingleOrDefaultAsync<T>(sql, param);
    }

    public async Task<T> ExecuteAsync<T>(Func<NpgsqlConnection, Task<T>> operation)
    {
        ThrowIfDisposed();
        return await operation(_connection);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _connection?.Dispose();
            }

            _disposed = true;
        }
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
        }
    }
}