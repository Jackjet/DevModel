using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Baibaomen.DevModel.ApiWeb.Models
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class PropertyBaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AddressLine2 { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PropertyCreateModel:PropertyBaseModel
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class PropertyUpdateModel:PropertyBaseModel {

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public byte[] RecordVersion { get; set; }
    }

    /// <summary>
    /// /
    /// </summary>
    public class PropertyViewModel:PropertyBaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The time the record created. 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// The time the record updated. 
        /// </summary>
        public DateTime UpdateTime { get; set; }
        
        /// <summary>
        /// The user id of entity creator.
        /// </summary>
        public int? CreatorId { get; set; }

        /// <summary>
        /// The user id of last updator.
        /// </summary>
        public int? UpdatorId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] RecordVersion { get; set; }
    }
}