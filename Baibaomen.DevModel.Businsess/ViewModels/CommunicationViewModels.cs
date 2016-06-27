using System;
using System.ComponentModel.DataAnnotations;

namespace Baibaomen.DevModel.ApiWeb.Models
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
    public class CommunicationUpdateModel:CommunicationBaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public byte[] RecordVersion { get; set; }
    }

    /// <summary>
    /// Create communication.
    /// </summary>
    public class CommunicationCreateModel : CommunicationBaseModel
    {

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
    public class CommunicationViewModel : CommunicationBaseModel
    {

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