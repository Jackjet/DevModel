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
    [RoutePrefix("api/property-communication")]
    public class PropertyCommunicationController : ApiController
    {
        PropertyCommunicationService _propertyService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyService"></param>
        public PropertyCommunicationController(PropertyCommunicationService propertyService)
        {
            _propertyService = propertyService;
        }

        /// <summary>
        /// Create communication that relates to some property.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(CommunicationViewModel))]
        public async Task<IHttpActionResult> Post(CommunicationCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var communication = Mapper.Map<Communication>(model);
            await _propertyService.AddAsync(communication);

            var communicationViewModel = Mapper.Map<CommunicationViewModel>(communication);
            return CreatedAtRoute("communication", new { id = communication.Id }, communicationViewModel);
        }

        /// <summary>
        /// Update communication.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("{id}")]
        [ResponseType(typeof(CommunicationViewModel))]
        public async Task<IHttpActionResult> Put(int id, CommunicationUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var communication = Mapper.Map<Communication>(model);

            var updated = await _propertyService.UpdateAsync(id, x => Mapper.Map(model, x));
            return Ok(Mapper.Map<CommunicationViewModel>(updated));
        }

        /// <summary>
        /// Get communication by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [Route("{id:int}", Name = "communication")]
        [ResponseType(typeof(CommunicationViewModel))]
        public IHttpActionResult Get(int id, ODataQueryOptions<CommunicationViewModel> options)
        {
            var communication = _propertyService.Get(id);
            if (communication == null)
            {
                return NotFound();
            }

            var toReturn = Mapper.Map<CommunicationViewModel>(communication);

            return Json(options.ApplyTo(toReturn, new ODataQuerySettings()));
        }

        /// <summary>
        /// Gets all communications.
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [Route("property/{propertyId:int}")]
        [ResponseType(typeof(PagedResult<CommunicationViewModel>))]
        public IHttpActionResult GetAllByProperty(int propertyId,ODataQueryOptions<Communication> options)
        {
            var communications = _propertyService.GetAll().Where(x=>x.PropertyId == propertyId);

            return Ok(options.FilterResult<CommunicationViewModel, Communication>(communications, this, new ODataValidationSettings()));
        }
    }
}
