using System.ComponentModel;

namespace Company.Management.Domain.Companies.Enums;

public enum CompanySizeType
{
    [Description("Desconhecida")]
    Unknow = 0,

    [Description("Pequena")]
    Small = 1,

    [Description("Média")]
    Medium = 2,

    [Description("Grande")]
    Big = 3
}
