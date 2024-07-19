using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Members.Commands;

public sealed class UpdateMemberCommand : MemberCommandBase
{
    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Member>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMemberCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Member> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _unitOfWork.MemberRepository.GetMemberByID(request.Id);

            if (member is null)
            {
                throw new InvalidOperationException("Member not found.");
            }

            member.Update(request.FirstName, request.LastName, request.Gender, request.Email, request.IsActive);
            _unitOfWork.MemberRepository.UpdateMember(member);
            await _unitOfWork.CommitAsync();

            return member;
        }
    }
}
