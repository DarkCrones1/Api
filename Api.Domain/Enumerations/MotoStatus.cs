using System.ComponentModel;

namespace Api.Domain.Enumerations;

public enum MotoStatus
{
    [Description("Disponible")]
    Available = 1,
    [Description("Agotado")]
    NotAvailable = 2,
    [Description("Descontinuado")]
    discontinued = 3,
}