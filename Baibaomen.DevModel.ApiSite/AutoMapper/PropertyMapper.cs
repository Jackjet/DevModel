using AutoMapper;
using Baibaomen.DevModel.ApiSite.ViewModels;
using Baibaomen.DevModel.Businsess.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Baibaomen.DevModel.ApiSite.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class PropertyMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        protected override void Configure()
        {
            CreateMap<Communication, CommunicationViewModel>();
            CreateMap<CommunicationCreateModel, Communication>();
            CreateMap<CommunicationUpdateModel, Communication>();
            //CreateMap<IQueryable, IQuery<CommunicationViewModel>>();
            CreateMissingTypeMaps = true;
        }
    }
}