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
        public async Task<PropertyViewModel> AddPropertyAsync(PropertyAndCommunicationCreateModel model,User theOperator)
        {
            var property = Mapper.Map<Property>(model);

            using (var t = _propertyDataService.BeginTransaction())
            {
                await _propertyDataService.AddAsync(property);
                var communication = new Communication() { Property = property, PropertyId = property.Id, Content = model.FirstCommunication };
                await _communicationDataService.AddAsync(communication,true,theOperator);
                t.Commit();
            }

            if (property.CreatorId != default(int)) {
                await _snsService.EmailNotifyAsync(property.CreatorId, Resources.Property.EmailSubjectForCreation.FormatMe(property.Name), 
                    Resources.Property.EmailContentForCreation.FormatMe(property.CreatorId,property.Name,property.CreateTime));
            }

            var toReturn = Mapper.Map<PropertyViewModel>(property);
            return toReturn;
        }

        public async Task<PropertyViewModel> UpdatePropertyAsync(int id, PropertyUpdateModel model, User theOperator)
        {
            var property = await _propertyDataService.UpdateAsync(id, x => Mapper.Map(model, x),true,theOperator);
            var toReturn = Mapper.Map<PropertyViewModel>(property);
            return toReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Property> GetAllProperties(User theOperator = null) {
            return _propertyDataService.GetAll();
        }

        public Property GetProperty(int id, User theOperator = null)
        {
            return _propertyDataService.Get(id,theOperator);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="communication"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync<TRecordVersion>(int itemId,TRecordVersion clientRecordVersion = default(TRecordVersion), User theOperator = null)
        {
            var item = _propertyDataService.Get(itemId);
            using (var t = _propertyDataService.BeginTransaction())
            {
                if (item.Communications != null)
                {
                    foreach (var communication in item.Communications.ToArray())
                    {
                        await _communicationDataService.DeleteAsync(communication);
                    }
                }
                await _propertyDataService.DeleteAsync(item,clientRecordVersion,theOperator);
                //throw new ApplicationException("error on purpose");
                t.Commit();
            }
        }
    }
}
