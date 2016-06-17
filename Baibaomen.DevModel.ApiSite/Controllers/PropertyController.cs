using AutoMapper;
using Baibaomen.DevModel.ApiSite.ViewModels;
using Baibaomen.DevModel.Businsess;
using Baibaomen.DevModel.Businsess.Entities;
using Baibaomen.DevModel.Businsess.Services;
using Baibaomen.DevModel.Infrastructure;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.OData;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Query;

namespace Baibaomen.DevModel.ApiSite.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/property")]
    public class PropertyController : ApiController
    {
        PropertyService _propertyService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyService"></param>
        public PropertyController(PropertyService propertyService) {
            _propertyService = propertyService;
        }

        /// <summary>
        /// Create communication.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(PropertyViewModel))]
        public async Task<IHttpActionResult> Post(PropertyCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = Mapper.Map<Property>(model);
            await _propertyService.AddAsync(item);

            var viewModel = Mapper.Map<PropertyViewModel>(item);
            return CreatedAtRoute("property", new { id = item.Id}, viewModel);
        }

        /// <summary>
        /// Update communication.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("property/{id}")]
        [ResponseType(typeof(PropertyViewModel))]
        public async Task<IHttpActionResult> Put(int id,PropertyUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var updated = await  _propertyService.UpdateAsync(id,x=>Mapper.Map(model,x));
            return Ok(Mapper.Map<PropertyViewModel>(updated));
        }

        /// <summary>
        /// Get property by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [Route("{id:int}", Name = "property")]
        [ResponseType(typeof(PropertyViewModel))]
        public IHttpActionResult Get(int id,ODataQueryOptions<PropertyViewModel> options)
        {
            var item = _propertyService.Get(id);
            if (item == null)
            {
                return NotFound();
            }

            var toReturn = Mapper.Map<PropertyViewModel>(item);

            return Json(options.ApplyTo(toReturn,new ODataQuerySettings()));
        }

        /// <summary>
        /// Gets all properties.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(PagedResult<PropertyViewModel>))]
        public IHttpActionResult GetAll(ODataQueryOptions<Property> options)
        {
            var items = _propertyService.GetAll();
            
            return Ok(options.FilterResult<PropertyViewModel,Property>(items, this,new ODataValidationSettings()));
        }
    }
}
