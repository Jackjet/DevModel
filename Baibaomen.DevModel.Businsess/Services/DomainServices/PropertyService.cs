using AutoMapper;
using Baibaomen.DevModel.ApiWeb.Models;
using Baibaomen.DevModel.Businsess.ComponentServices;
using Baibaomen.DevModel.Businsess.DataServices;
using Baibaomen.DevModel.Businsess.Entities;
using Baibaomen.DevModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Baibaomen.DevModel.Businsess.DomainServices
{
    public class PropertyService
    {
        PropertyDataService _propertyDataService;
        PropertyCommunicationDataService _communicationDataService;
        SnSService _snsService;

        public PropertyService(PropertyDataService propertyDataService, PropertyCommunicationDataService communicationDataService, SnSService snsService) {
            _propertyDataService = propertyDataService;
            _communicationDataService = communicationDataService;
            _snsService = snsService;
        }

        /// <summary>
        /// Add property and the required first communication record.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="operatorId"></param>
        /// <returns></returns>
        public async Task<PropertyViewModel> AddPropertyAsync(PropertyAndCommunicationCreateModel model, int? operatorId = null)
        {
            var property = Mapper.Map<Property>(model);

            using (var t = _propertyDataService.BeginTransaction())
            {
                await _propertyDataService.AddAsync(property);
                var communication = new Communication() { Property = property, PropertyId = property.Id, Content = model.FirstCommunication };
                await _communicationDataService.AddAsync(communication, operatorId);
                t.Commit();
            }

            if (property.CreatorId != null) {
                await _snsService.EmailNotifyAsync(property.CreatorId.GetValueOrDefault(), Resources.Property.EmailSubjectForCreation.FormatMe(property.Name), 
                    Resources.Property.EmailContentForCreation.FormatMe(property.CreatorId.GetValueOrDefault(),property.Name,property.CreateTime));
            }

            var toReturn = Mapper.Map<PropertyViewModel>(property);
            return toReturn;
        }

        public async Task<PropertyViewModel> UpdatePropertyAsync(int id, PropertyUpdateModel model, int? operatorId = null)
        {
            var property = await _propertyDataService.UpdateAsync(id, x => Mapper.Map(model, x),operatorId);
            var toReturn = Mapper.Map<PropertyViewModel>(property);
            return toReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Property> GetAllProperties() {
            return _propertyDataService.GetAll();
        }

        public Property GetProperty(int id)
        {
            return _propertyDataService.Get(id);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="communication"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(Property item)
        {
            using (var t = _propertyDataService.BeginTransaction())
            {
                if (item.Communications != null)
                {
                    foreach (var communication in item.Communications)
                    {
                        await _communicationDataService.DeleteAsync(communication);
                    }
                }
                await _propertyDataService.DeleteAsync(item);
                //throw new ApplicationException("error on purpose");
                t.Commit();
            }
        }
    }
}
