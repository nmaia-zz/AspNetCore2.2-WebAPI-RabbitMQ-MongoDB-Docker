using System.ComponentModel;

namespace Demo.Domain.Enums
{
    public enum FamilyTreeLevel
    {
        [Description("Ancestors")]
        ANCESTORS = 1,

        [Description("Children")]
        CHILDREN = 2,

        [Description("Parents")]
        PARENTS = 3
    }
}
