﻿using System.ComponentModel.DataAnnotations;

namespace FiscalLabApp.Models;

public class PlantModel
{
    public string Id { get; set; } = string.Empty;
    [Required(ErrorMessage = "Nome é obrigatório.")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Cidade é obrigatório.")]
    public string City { get; set; } = string.Empty;
    [Required(ErrorMessage = "Estado é obrigatório.")]
    public string State { get; set; } = string.Empty;
}