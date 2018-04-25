using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data
{





    public class OptimizeSqlDataProvider<T> : DefaultSqlDataProvider where T : class
    {



        internal protected T SelectByID(int id)
        {
            //ParameterList parameters = new ParameterList();
            //parameters.Add("@ID", SqlDbType.Int).Value = id;
            //return this.ExecuteSPSingle<ProductionBrand>("[Production].[BrandSelectByID]", parameters);

            return null;

        }
    }
}
