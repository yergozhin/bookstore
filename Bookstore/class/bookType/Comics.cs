using System;

namespace Bookstore.@class
{
    public class Comics : Book
    {
        private int _issueNumber;
        public int IssueNumber
        {
            get => _issueNumber;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Issue number must be greater than 0.");
                }
                _issueNumber = value;
            }
        }

        public Comics(string title, float price, string languageOfPublication, int issueNumber) : base(title, price, languageOfPublication)
        {
            IssueNumber = issueNumber;
        }
    }
}
