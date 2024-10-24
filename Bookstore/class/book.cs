using System;
namespace Bookstore.@class
{
	public class Book
	{
		private string title;
		private float price;
<<<<<<< Updated upstream
=======
		private int lengthMax = 50;
		private int lengthMin = 3;
		private int maxPrice = 10000;
>>>>>>> Stashed changes

		public Book(string title, float price)
		{
			this.title = title;
			this.price = price;
		}
<<<<<<< Updated upstream
=======

        public string getTitle()
        {
            return this.title;
        }

        public float getPrice()
        {
            return this.price;

        }

        public void setPrice(float price)
        {
            this.price = price;
        }

        public bool checkTitle()
		{
			if (this.title.length() <= lengthMax && this.title.lenght() >= lengthMin)
			{
				return true;
			}
			return false;
		}

		public bool checkPrice ()
		{
			if (this.price >= 0 && this.price < maxPrice)
				return true;
			return false;
		}
>>>>>>> Stashed changes
	}
}

