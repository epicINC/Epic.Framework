using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Common;
using System.Reflection;
using Epic.Data.Expressions;

namespace Epic.Data.Builder
{
    public class DbQueryProvider : QueryProvider
    {
        DbConnection connection;

        public DbQueryProvider(DbConnection connection)
        {
            this.connection = connection;
        }

        public override string GetQueryText(Expression expression)
        {
            return this.Translate(expression).CommandText;
        }



        public override object Execute(Expression expression)
        {
            var translateResult = this.Translate(expression);


            var command = this.connection.CreateCommand();
            command.CommandText = translateResult.CommandText;
            for (int i = 0; i < translateResult.Parameters.Count; i++)
            {
                var p = new System.Data.SqlClient.SqlParameter("@p"+ i, translateResult.Values[i]);

                command.Parameters.Add(p);
            }
            var reader = command.ExecuteReader();
            Type elementType = TypeSystem.GetElementType(expression.Type);
            return Activator.CreateInstance(typeof(ObjectReader<>).MakeGenericType(elementType), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { reader }, null);
        }
        
        TranslateResult Translate(Expression expression)
        { 
            expression = Evaluator.PartialEval(expression);
            return new QueryTranslator().Translate(expression);
        }

    }
}
