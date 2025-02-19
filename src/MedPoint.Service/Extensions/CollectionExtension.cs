using MedPoint.Domain.Commons;
using MedPoint.Service.Configurations;
using MedPoint.Service.Exceptions;
using Newtonsoft.Json; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Extensions
{
    public static class CollectionExtension
    {
        public static IQueryable<TEntity> ToPagedList<TEntity>(
            this IQueryable<TEntity> source, 
            PaginationParams @params) where TEntity : Auditable
        {
            var config = new PaginationConfig(source.Count(), @params);

            var json = JsonConvert.SerializeObject(config);
            if(HttpContextHelper.ResponseHeader is not null)
            {
                if (HttpContextHelper.ResponseHeader.ContainsKey("X-Pagination"))
                    HttpContextHelper.ResponseHeader.Remove("X-Pagination");

                HttpContextHelper.ResponseHeader.Add("X-Pagination", json);
            }

            return @params.PageSize > 0 && @params.PageIndex > 0 ?
                source
                .OrderBy(c => c.Id)
                .Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
                : throw new MedPointException(400, "Please, enter valid numbers.");
        }
    }
}
