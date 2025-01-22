using System;
using System.Collections.Generic;

namespace Bookstore.@class
{
    [Serializable]
    public class HorrorManga : Book, IHorror, IManga
    {
        // IHorror
        private int levelOfScariness;
        public int LevelOfScariness
        {
            get => levelOfScariness;
            set
            {
                if (value < 1 || value > 10)
                    throw new ArgumentException("Value must be 1..10 for scariness level.");
                levelOfScariness = value;
            }
        }
        public string GenreName => "Horror";

        // IManga
        private int volume;
        public int Volume
        {
            get => volume;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Volume must be greater than 0.");
                volume = value;
            }
        }
        public string TypeName => "Manga";

        // Constructor bridging them
        public HorrorManga(
            string title,
            double price,
            string language,
            int levelOfScariness,
            int volume,
            List<string> translations = null
        )
            : base(title, price, language, translations)
        {
            LevelOfScariness = levelOfScariness;
            Volume = volume;
        }
    }
}