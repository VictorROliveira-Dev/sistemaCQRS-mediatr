using CleanArch.Domain.Validation;
using System.Text.Json.Serialization;

namespace CleanArch.Domain.Entities;

public sealed class Member : Entity
{
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Gender { get; private set; }
    public string? Email { get; private set; }
    public bool? IsActive { get; private set; }

    public Member()
    {
        
    }

    public Member(string firstName, string lastName, string gender, string email, bool? active)
    {
        ValidationDomain(firstName, lastName, gender, email, active);
    }

    [JsonConstructor]
    public Member(int id, string firstName, string lastName, string gender, string email, bool? isActive)
    {
        DomainValidation.When(id < 0, "Invalid Id value.");
        Id = id;
        ValidationDomain(firstName, lastName, gender, email, isActive);
    }

    public void Update(string firstName, string lastName, string gender, string email, bool? active)
    {
        ValidationDomain(firstName, lastName, gender, email, active);
    }

    private void ValidationDomain(string firstName, string lastName, string gender, string email, bool? active)
    {
        DomainValidation.When(string.IsNullOrEmpty(firstName), "Invalid Name. FirstName is required.");
        DomainValidation.When(firstName.Length < 3, "Invalid name, too short, minimum 3 characters.");
        DomainValidation.When(string.IsNullOrEmpty(lastName), "Invalid LastName. LastName is required.");
        DomainValidation.When(lastName.Length < 3, "Invalid name, too short, minimum 3 characters.");
        DomainValidation.When(email.Length > 100, "Invalid email, too long, minimum 100 characters.");
        DomainValidation.When(string.IsNullOrEmpty(gender), "Invalid gender, gender is required.");
        DomainValidation.When(!active.HasValue, "Must define activity.");

        FirstName = firstName;
        LastName  = lastName;
        Gender = gender;
        Email = email;
        IsActive = active;
    }
}
