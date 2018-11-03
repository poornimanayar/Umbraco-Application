namespace Umbraco_Application.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class QueryViewModel
    {
        public Guid MemberId { get; set; }

        public string MemberName { get; set; }

        public string MemberEmail { get; set; }

        [Required]
        public Guid Category { get; set; }

        [Required]
        public Guid Product { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string RaisedOnBehalfOf { get; set; }

        public SelectList Categories { get; set; }

        public SelectList Products { get; set; }

        public bool IsReseller { get; set; }
    }
}