using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData.Extensions;
using System.Web.OData.Query;

namespace Baibaomen.DevModel.Infrastructure
{
    public static class ODataQueryFilterExtention
    {
        /// <summary>
        /// Filter result based on OData options. $select and $expand are handled gracefully.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="options"></param>
        /// <param name="source"></param>
        /// <param name="controller"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static PagedResult<TViewModel> FilterResult<TViewModel,TSource>(this ODataQueryOptions<TSource> options, IQueryable<TSource> source, ApiController controller, ODataValidationSettings settings = null)
        {
            if (settings != null) {
                options.Validate(settings);
            }

            dynamic filterred = options.ApplyToWithExpandAndSelectSupport(source);

            var viewModels = Mapper.Map<IEnumerable<TViewModel>>(filterred);

            var pagedResult = new PagedResult<TViewModel>(
                viewModels,
                controller.Request.ODataProperties().TotalCount);
            
            return pagedResult;
        }

        public static TViewModel FilterSingleResult<TViewModel, TSource>(this ODataQueryOptions<TViewModel> options, TSource source, ApiController controller, ODataValidationSettings settings = null)
        {
            if (settings != null)
            {
                options.Validate(settings);
            }
            var mapped = Mapper.Map<TViewModel>(source);
           return options.ApplyToWithExpandAndSelectSupport(mapped);
        }

        /// <summary>
        /// Apply filter. Handle $select and $expand gracefully.($select tested. $expand not tested yet.)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <param name="communications"></param>
        /// <returns></returns>
        public static dynamic ApplyToWithExpandAndSelectSupport<T>(this ODataQueryOptions<T> options, IQueryable<T> communications)
        {
            var filtered = options.ApplyTo(communications);

            dynamic raw = new ArrayList();
            if (options.SelectExpand != null)
            {

                foreach (var item in filtered)
                {
                    raw.Add(JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(item)));
                }
            }
            else
            {
                raw = filtered;
            }

            return raw;
        }

        /// <summary>
        /// Apply filter. Handle $select and $expand gracefully.($select tested. $expand not tested yet.)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <param name="communications"></param>
        /// <returns></returns>
        public static dynamic ApplyToWithExpandAndSelectSupport<T>(this ODataQueryOptions<T> options, T entity)
        {
            var filtered = options.ApplyTo(entity,new ODataQuerySettings());

            if (options.SelectExpand != null)
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(filtered));
            }
            else
            {
                return entity;
            }
        }
    }
}
