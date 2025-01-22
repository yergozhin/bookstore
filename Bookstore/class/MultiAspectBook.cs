using System;
using System.Collections.Generic;

namespace Bookstore.@class
{
    [Serializable]
    public class MultiAspectBook
    {
        private string title;
        private double price;
        private string language;
        
        private IGenre genreAspect; 
        private IType typeAspect;
        
        public IGenre GenreAspect => genreAspect;
        public IType TypeAspect => typeAspect;

        public MultiAspectBook(
            string title,
            double price,
            string language,
            IGenre genre,
            IType type
        )
        {
            if (string.IsNullOrEmpty(title)) 
                throw new ArgumentException("Title cannot be empty.");
            if (price < 0) 
                throw new ArgumentException("Price cannot be negative.");
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Language cannot be empty.");

            this.title = title;
            this.price = price;
            this.language = language;
            
            this.genreAspect = genre ?? throw new ArgumentNullException(nameof(genre));
            this.typeAspect = type ?? throw new ArgumentNullException(nameof(type));
        }
        
        public string GetFullDescription()
        {
            return $"{title} ({language}), Price={price}, Genre={genreAspect.GenreName}, Type={typeAspect.TypeName}";
        }
    }
}