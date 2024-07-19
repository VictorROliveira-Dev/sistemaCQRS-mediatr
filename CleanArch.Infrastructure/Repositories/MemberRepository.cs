using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using CleanArch.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Repositories;

public class MemberRepository : IMemberRepository
{
    protected readonly AppDbContext _appDbContext;

    public MemberRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Member> AddMember(Member member)
    {
        if (member == null)
        {
            throw new ArgumentNullException(nameof(member));
        }

        await _appDbContext.Members.AddAsync(member);
        return member;
    }

    public async Task<Member> DeleteMember(int memberId)
    {
        var member = await GetMemberByID(memberId);

        if (member is null)
        {
            throw new InvalidOperationException("Member not found.");
        }

        _appDbContext.Members.Remove(member);
        return member;
    }

    public async Task<Member> GetMemberByID(int memberId)
    {
        var member = await _appDbContext.Members.FindAsync(memberId);

        if (member is null)
        {
            throw new InvalidOperationException("Member not found.");
        }

        return member;
    }

    public async Task<IEnumerable<Member>> GetMembers()
    {
        var memberList = await _appDbContext.Members.ToListAsync();
        return memberList ?? Enumerable.Empty<Member>();
    }

    public void UpdateMember(Member member)
    {
        if (member is null)
        {
            throw new ArgumentNullException(nameof(member));
        }

        _appDbContext.Members.Update(member);
    }
}
