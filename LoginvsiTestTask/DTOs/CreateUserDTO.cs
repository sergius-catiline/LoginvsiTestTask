using System.ComponentModel.DataAnnotations;

namespace LoginvsiTestTask.DTOs;

/// <summary>
/// The DTO (Data Transfer Object) class for creating a user.
/// </summary>
public class CreateUserDTO
{
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = String.Empty;
}