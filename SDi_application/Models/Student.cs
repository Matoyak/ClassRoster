using System;
using System.ComponentModel.DataAnnotations;

namespace SDi_application.Models {
    public class Student {
        /// <summary>
        /// A Student's GPA
        /// </summary>
        [Required(ErrorMessage = "GPA cannot be empty")]
        public double GPA { get; set; }

        /// <summary>
        /// A Student's Date of Birth
        /// </summary>
        [Required(ErrorMessage = "DoB cannot be empty")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// A Student's First Name (Given Name)
        /// </summary>
        [Required(ErrorMessage = "First Name cannot be empty")]
        public string FirstName { get; set; }

        /// <summary>
        /// A Student's Last Name (Surname)
        /// </summary>
        [Required(ErrorMessage = "GPA cannot be empty")]
        public string LastName { get; set; }

        /// <summary>
        /// Student's ID in the database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Student's UserName for Lookup
        /// (Likely an Email)
        /// </summary>
        public string UserName { get; set; }
    }
}
