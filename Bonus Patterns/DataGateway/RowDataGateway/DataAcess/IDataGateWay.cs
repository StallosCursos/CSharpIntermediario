using System;
using System.Collections.Generic;
using System.Text;

namespace DataGateway.DataGateWay
{
    public interface IDataGateWay<TClass>
        where TClass : class
    {
        TClass Insert(TClass objeto);
        
        void Update(TClass objeto);
        void Delete(TClass objeto);

        TClass Find(int Id);
        List<TClass> SelectAll();
        List<TClass> Select(TClass Object);
    }
}
