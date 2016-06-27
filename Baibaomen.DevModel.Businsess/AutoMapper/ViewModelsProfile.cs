using AutoMapper;
using Baibaomen.DevModel.ApiWeb.Models;
using Baibaomen.DevModel.Businsess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Baibaomen.DevModel.Business.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class ViewModelsProfile:Profile
    {
        /// <summary>
        /// 
        /// </summary>
        protected override void Configure()
        {
            CreateMap<Property, PropertyViewModel>();
            CreateMap<PropertyCreateModel, Property>();
            CreateMap<PropertyUpdateModel, Property>();
            CreateMap<PropertyAndCommunicationCreateModel, Property>();

            CreateMap<Communication, CommunicationViewModel>();
            CreateMap<CommunicationCreateModel, Communication>();
            CreateMap<CommunicationUpdateModel, Communication>();
        }
    }
}