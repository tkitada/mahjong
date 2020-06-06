using System.Collections.Generic;

namespace mjlib
{
    public static class Constants
    {
        public static List<int> TERMINAL_INDICES =>
            new List<int> { 0, 8, 9, 17, 18, 26 };

        public const int EAST = 27;
        public const int SOUTH = 28;
        public const int WEST = 29;
        public const int NORTH = 30;
        public const int HAKU = 31;
        public const int HATSU = 32;
        public const int CHUN = 33;

        public static List<int> WINDS =>
            new List<int> { EAST, SOUTH, WEST, NORTH };

        public static List<int> HONOR_INDICES =>
            new List<int> { EAST, SOUTH, WEST, NORTH, HAKU, HATSU, CHUN };

        public static List<int> YAOCHU_INDICES =>
            new List<int>
            {
                0, 8, 9, 17, 18, 26,
                EAST, SOUTH, WEST, NORTH, HAKU, HATSU, CHUN
            };

        public const int FIVE_RED_MAN = 16;
        public const int FIVE_RED_PIN = 52;
        public const int FIVE_RED_SOU = 88;

        public static List<int> AKA_DORA_LIST =>
            new List<int> { FIVE_RED_MAN, FIVE_RED_PIN, FIVE_RED_SOU };

        public static Dictionary<int, string> DISPLAY_WINDS =>
            new Dictionary<int, string>
            {
                { EAST, "East" },
                { SOUTH, "South" },
                { WEST, "West" },
                { NORTH, "North" }
            };
    }
}