using Bogus;

namespace FluentContracts.Tests.Mocks;

public class Employee(Role role) : Person
{
    public Role Role { get; init; } = role;
}

public enum Role
{
    Developer,
    Manager,
    QualityAssurance,
    ProductManager,
}