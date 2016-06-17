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
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int Creator { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class CommunicationUpdateModel : CommunicationBaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public byte[] RecordVersion { get; set; }
    }

    /// <summary>
    /// Create communication.
    /// </summary>
    public class CommunicationCreateModel : CommunicationBaseModel { }
    
    /// <summary>
    /// 
    /// </summary>
    public class CommunicationViewModel : CommunicationBaseModel {

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