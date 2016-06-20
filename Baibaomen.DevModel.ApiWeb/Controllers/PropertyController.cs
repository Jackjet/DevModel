using AutoMapper;
using Baibaomen.DevModel.ApiWeb.Models;
using Baibaomen.DevModel.Businsess.Entities;
using Baibaomen.DevModel.Businsess.Services;
using Baibaomen.DevModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.OData.Query;

namespace Baibaomen.DevModel.ApiWeb.Controllers
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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(PropertyViewModel))]
        public async Task<IHttpActionResult> Post(PropertyCreateModel model) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var property = Mapper.Map<Property>(model);
            await _propertyService.AddAsync(property);

            var toReturn = Mapper.Map<PropertyViewModel>(property);

            return CreatedAtRoute("property", new { id = property.Id }, toReturn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("{id}")]
        [ResponseType(typeof(PropertyViewModel))]
        public async Task<IHttpActionResult> Put(int id,PropertyUpdateModel model) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var theProp = await _propertyService.UpdateAsync(id, x => Mapper.Map(model, x));

            return Ok(Mapper.Map<PropertyViewModel>(theProp));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(PagedResult<PropertyViewModel>))]
        public IHttpActionResult GetAll(ODataQueryOptions<Property> options) {
            var result = _propertyService.GetAll();

            return Ok(options.FilterResult<PropertyViewModel, Property>(result, this));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [Route("{id}", Name = "property")]
        [ResponseType(typeof(PropertyViewModel))]
        public IHttpActionResult Get(int id, ODataQueryOptions<PropertyViewModel> options)
        {
            var theProperty = _propertyService.Get(id);

            if (theProperty == null)
            {
                return NotFound();
            }

            var toReturn = Mapper.Map<PropertyViewModel>(theProperty);

            return Json(options.ApplyTo(toReturn, new ODataQuerySettings()));
        }

    }
}
