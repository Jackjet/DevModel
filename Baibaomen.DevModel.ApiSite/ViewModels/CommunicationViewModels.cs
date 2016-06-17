using Baibaomen.DevModel.Businsess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Baibaomen.DevModel.ApiSite.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CommunicationBaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Content { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CommunicationUpdateModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] RecordVersion { get; set; }
    }

    /// <summary>
    /// Create communication.
    /// </summary>
    public class CommunicationCreateModel : CommunicationBaseModel {
        
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int Creator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int PropertyId { get; set; }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class CommunicationViewModel : CommunicationBaseModel {

        /// <summary>
        /// 
        /// </summary>
        public int Creator { get; set; }

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
        public int PropertyId { get; set; }

        //Not suggested. Will cause expanding by default due to serializing.
        //public Property Property { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] RecordVersion { get; set; }
    }
}