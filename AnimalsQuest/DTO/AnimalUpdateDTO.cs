﻿using System.ComponentModel.DataAnnotations;

namespace AnimalsQuest.DTO;

public class AnimalUpdateDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; init; } = null!;

    [MaxLength(100)]
    public string Description { get; init; } = null!;

    [Required]
    [MaxLength(100)]
    public string Category { get; init; } = null!;

    [Required]
    [MaxLength(100)]
    public string Area { get; init; } = null!;
}