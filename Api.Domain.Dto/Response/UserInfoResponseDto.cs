namespace Api.Domain.Dto.Response;

public class UserInfoResponseDto
{
    public int Id { get; set; }

    public Guid Code { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string CellPhone { get; set; } = null!;

    public string? Phone { get; set; }

    public short? Gender { get; set; }

    public string? GenderName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public int Age
    {
        get
        {
            if (!BirthDate.HasValue) return 0;

            DateTime endDate = DateTime.Now;
            TimeSpan difference = endDate - BirthDate.Value;
            int yearsDifference = (int)(difference.TotalDays / 365.25);

            return yearsDifference;
        }
    }

    public string FullName { get => $"{FirstName} {MiddleName} {LastName}".Trim(); }

    public bool? IsDeleted { get; set; }
}