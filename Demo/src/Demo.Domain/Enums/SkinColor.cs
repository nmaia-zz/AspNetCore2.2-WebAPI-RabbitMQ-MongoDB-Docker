using System.ComponentModel;

namespace Demo.Domain.Enums
{
    public enum SkinColor
    {
        [Description("Albino")]
        ALBINO = 1,

        [Description("White")]
        WHITE = 2,

        [Description("Yellow")]
        YELLOW = 3,

        [Description("Olive")]
        OLIVE = 4,

        [Description("Brown")]
        BROWN = 5,

        [Description("Black")]
        BLACK = 6,

        [Description("Burnt")]
        BURNT = 7
    }
}
