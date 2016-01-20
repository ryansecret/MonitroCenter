using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;

namespace Monitor.Service.Utility
{
    public class MonitorServiceLoacator:ServiceLocatorImplBase
    {
        protected override object DoGetInstance(Type serviceType, string key)
        {
            
            return serviceType.CreateInstanceDelegate(new Type[] {})(null);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
           throw new NotImplementedException();
        }
    }

    public static class Tool
    {
        public static Func<object[], object> CreateInstanceDelegate(this Type type, Type[] parameterTypes)
        {
          
            var constructor = type.GetConstructor(parameterTypes);

           
            var lambdaParam = Expression.Parameter(typeof(object[]), "_args");

          
            var constructorParam = buildParameters(parameterTypes, lambdaParam);

           
            NewExpression newExp = Expression.New(constructor, constructorParam);

         
            Expression<Func<object[], object>> lambdaExp =
                Expression.Lambda<Func<object[], object>>(newExp, lambdaParam);

            return lambdaExp.Compile();
        }


        static Expression[] buildParameters(Type[] parameterTypes, ParameterExpression paramExp)
        {
            List<Expression> list = new List<Expression>();
            for (int i = 0; i < parameterTypes.Length; i++)
            {
               
                var arg = BinaryExpression.ArrayIndex(paramExp, Expression.Constant(i));
                 
                var argCast = Expression.Convert(arg, parameterTypes[i]);

                list.Add(argCast);
            }
            return list.ToArray();
        }
    }




}
