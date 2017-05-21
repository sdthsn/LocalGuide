using Data.Interfaces.AbstractInterface;
using DataModel.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    interface ISpotsRepository :IRepository<Spot,string>
    {
    }
}
