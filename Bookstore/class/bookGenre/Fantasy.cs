using System;
using System.Collections.Generic;

namespace Bookstore.@class
{
    public class Fantasy : Book
    {
        private List<string> _magicalCreatures;

        public List<string> MagicalCreatures
        {
            get => _magicalCreatures;
            set
            {
                if (value == null || value.Count == 0)
                {
                    throw new ArgumentException("You must have at least one magical creature.");
                }
                _magicalCreatures = value;
            }
        }

        public Fantasy(string title, float price, string languageOfPublication, List<string> magicalCreatures)
            : base(title, price, languageOfPublication)
        {
            MagicalCreatures = magicalCreatures;
        }
    }
}
