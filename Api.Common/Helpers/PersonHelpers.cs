namespace Api.Common.Helpers;

public static class PersonHelpers
{
    public static bool BeAtLeast18YearsOld(DateTime? birthday)
    {
        if (!birthday.HasValue)
            return false;
        
        var today = DateTime.Today;
        var age = today.Year - birthday.Value.Year;

        if (birthday.Value > today.AddYears(-age))
            age--;
            
        return age >= 18;
    }
}