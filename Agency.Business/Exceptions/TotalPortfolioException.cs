using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Exceptions
{
    public class TotalPortfolioException : Exception
    {
        public string Ex { get; set; }
        public TotalPortfolioException()
        {
        }

        public TotalPortfolioException(string ex,string? message) : base(message)
        {
            Ex = ex;
        }
    }
}
