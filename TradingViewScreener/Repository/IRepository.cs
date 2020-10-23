using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingViewScreener.Models;

namespace TradingViewScreener.Repository
{
    public interface IRepository<T>
    {
        List<T> GetCompanies();
    }
}
