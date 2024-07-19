using CleanArch.Domain.Abstractions;
using CleanArch.Infrastructure.Context;

namespace CleanArch.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private IMemberRepository? _memberRepo;
    
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IMemberRepository MemberRepository
    {
        get
        {
            //Evitando criar instâncias desnecessárias de repositórios
            return _memberRepo = _memberRepo ?? new MemberRepository(_appDbContext);
        }
    }

    public async Task CommitAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }

    // Otimizando a liberação dos recursos após uso
    public void Dispose()
    {
        _appDbContext.Dispose();
    }
}
