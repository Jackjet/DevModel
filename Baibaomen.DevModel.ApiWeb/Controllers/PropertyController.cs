using AutoMapper;
using Baibaomen.DevModel.ApiWeb.Models;
using Baibaomen.DevModel.Businsess.DomainServices;
using Baibaomen.DevModel.Businsess.Entities;
using Baibaomen.DevModel.Infrastructure;
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
        public async Task<IHttpActionResult> Post(PropertyAndCommunicationCreateModel model) {
            var toReturn = await _propertyService.AddPropertyAsync(model);

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
            return Ok(await _propertyService.UpdatePropertyAsync(id,model));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(PagedResult<PropertyViewModel>))]
        public IHttpActionResult GetAll(ODataQueryOptions<Property> options) {
            var result = _propertyService.GetAllProperties();

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
            var theProperty = _propertyService.GetProperty(id);

            if (theProperty == null)
            {
                return NotFound();
            }

            var toReturn = Mapper.Map<PropertyViewModel>(theProperty);

            return Json(options.ApplyTo(toReturn, new ODataQuerySettings()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id, PropertyDeleteModel model)
        {
            var theProperty = _propertyService.GetProperty(id);
            if (theProperty == null)
            {
                return NotFound();
            }

            await _propertyService.DeleteAsync(theProperty);
            return Ok();
        }
    }
}
