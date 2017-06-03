using AutoMapper;
using Baibaomen.DevModel.ApiWeb.Models;
using Baibaomen.DevModel.Businsess.DomainServices;
using Baibaomen.DevModel.Businsess.Entities;
using Baibaomen.DevModel.Infrastructure;
using System;
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
        UserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyService"></param>
        /// <param name="userService"></param>
        public PropertyController(PropertyService propertyService,UserService userService) {
            _propertyService = propertyService;
            _userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(PropertyViewModel))]
        public async Task<IHttpActionResult> Post(PropertyAndCommunicationCreateModel model) {
            var toReturn = await _propertyService.AddPropertyAsync(model,this.GetOperator(_userService));
            return CreatedAtRoute("property", new { id = toReturn.Id }, toReturn);
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
            return Ok(await _propertyService.UpdatePropertyAsync(id,model,this.GetOperator(_userService)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        //[Auth()]
        [Route("")]
        [ResponseType(typeof(PagedResult<PropertyViewModel>))]
        public async Task<IHttpActionResult> GetAll(ODataQueryOptions<Property> options) {
            var result = _propertyService.GetAllProperties(this.GetOperator(_userService));
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
        public async Task<IHttpActionResult> Get(int id, ODataQueryOptions<PropertyViewModel> options)
        {
            var theProperty = _propertyService.GetProperty(id,this.GetOperator(_userService));

            if (theProperty == null)
            {
                return NotFound();
            }

            return Ok(options.FilterSingleResult(theProperty, this));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recordVersion"></param>
        /// <returns></returns>
        [Auth]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id, string recordVersion)
        {
            await _propertyService.DeleteAsync(id, Convert.FromBase64String(recordVersion), this.GetOperator(_userService));
            return Ok();
        }
    }
}
