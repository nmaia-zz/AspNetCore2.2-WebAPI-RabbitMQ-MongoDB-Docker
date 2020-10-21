using System.ComponentModel;

namespace Demo.Domain.Enums
{
    public enum Schooling
    {
        [Description("PhD")]
        PHD = 1,

        [Description("Masters")]
        MASTERS = 2,       

        [Description("Postgraduate")]
        POSTGRADUATE = 3,

        [Description("University education")]
        UNIVERSITY_EDUCATION = 4,

        [Description("Elementary school")]
        ELEMENTARY_SCHOOL = 5,

        [Description("None")]
        NONE = 6
    }
}
