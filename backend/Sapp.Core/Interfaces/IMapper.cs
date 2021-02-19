using System.Collections.Generic;

namespace Sapp.Core.Interfaces
{
    public interface IMapper<TFrom, TTo>
    {
        TTo Map(TFrom source);
        
        IEnumerable<TTo> Map(IEnumerable<TFrom> source);
    }
}