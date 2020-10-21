using System.ComponentModel;

namespace Demo.Domain.Enums
{
    public enum Gender
    {
        [Description("Male")]
        MALE = 1,

        [Description("Female")]
        FEMALE = 2
    }
}
