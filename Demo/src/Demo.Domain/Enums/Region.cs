using System.ComponentModel;

namespace Demo.Domain.Enums
{
    public enum Region
    {
        [Description("Northeast Region")]
        NORTHEST_REGION = 1,

        [Description("North Region")]
        NORTH_REGION = 2,

        [Description("Midwest Region")]
        MIDWEST_REGION = 3,

        [Description("Southeast Region")]
        SOUTHEAST_REGION = 4,

        [Description("South Region")]
        SOUTH_REGION = 5
    }
}
