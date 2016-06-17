using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Baibaomen.DevModel.ApiSite.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class PropertyBaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AddressLine2 { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class PropertyUpdateModel : PropertyBaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public byte[] RecordVersion { get; set; }
    }

    /// <summary>
    /// Create property.
    /// </summary>
    public class PropertyCreateModel : PropertyBaseModel { }

    /// <summary>
    /// 
    /// </summary>
    public class PropertyViewModel : PropertyBaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] RecordVersion { get; set; }
    }
}