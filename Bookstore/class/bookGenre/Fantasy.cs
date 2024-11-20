using System;
using System.Collections.Generic;

namespace Bookstore.@class
{
    public class Fantasy : Book
    {
        private List<string> _magicalCreatures;
        private List<string> listOfPossibleMagicalCreatures = new List<string>{ "Vampires", "Werewolves", "Witches", "Wizzards", "Dragons", "Alliens", "Zombies", "Mermaids", "Others" };
        public List<string> MagicalCreatures
        {
            get => _magicalCreatures;
            set
            {
                if (value == null || value.Count == 0 || value.Contains(""))
                {
                    throw new ArgumentException("You must have at least one magical creature. Its name must not be empty");
                }
                for(int i = 0; i < value.Count; i++)
                {
                    if (!listOfPossibleMagicalCreatures.Contains(value[i]))
                    {
                        value[i] = "Others";
                    }
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
