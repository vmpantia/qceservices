using QCEServices.Shared.Enums;

namespace QCEServices.Domain.Entities;

public class MarriageLicense : AuditedSoftDeleteEntity
{
    public Guid Id { get; set; }
    public Guid ApplicationFormId { get; set; }

    public Party Groom { get; set; } = new();
    public Party Bride { get; set; } = new();

    public virtual ApplicationForm ApplicationForm { get; set; }
}

public class Party
{
    public Person Name { get; set; } = new();
    public DateOnly DateOfBirth { get; set; }
    public CivilStatus CivilStatus { get; set; }
    public string Citizenship { get; set; } = string.Empty;
    public string Religion { get; set; } = string.Empty;

    public Place BirthPlace { get; set; } = new();
    public Address Residence { get; set; } = new();
    public Parents Parents { get; set; } = new();
}

public class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
}

public class Parents
{
    public Parent Father { get; set; } = new();
    public Parent Mother { get; set; } = new();
}

public class Parent
{
    public Person Name { get; set; } = new();
    public string Citizenship { get; set; } = string.Empty;
    public ParentStatus Status { get; set; }
    public Address? Residence { get; set; }
}

public class Place
{
    public string Country { get; set; } = string.Empty;
    public string ProvinceOrState { get; set; } = string.Empty;
    public string CityOrMunicipality { get; set; } = string.Empty;
}

public class Address
{
    public string Country { get; set; } = string.Empty;
    public string ProvinceOrState { get; set; } = string.Empty;
    public string CityOrMunicipality { get; set; } = string.Empty;
    public string Barangay { get; set; } = string.Empty;
    public string? HouseNoOrStreet { get; set; }
}

