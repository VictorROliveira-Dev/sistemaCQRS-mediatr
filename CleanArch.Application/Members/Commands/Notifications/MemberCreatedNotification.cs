using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Members.Commands.Notifications;

public class MemberCreatedNotification : INotification
{
    public Member Member { get; set; }

    public MemberCreatedNotification(Member member)
    {
        Member = member;
    }
}
