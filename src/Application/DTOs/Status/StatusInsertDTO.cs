using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinAgendaBootcamp.src.Application.DTOs.Status
{
    public class StatusInsertDTO
    {
        [Required(ErrorMessage = "O Nome do status é obrigatório", AllowEmptyStrings = false)]
        public required string Name { get; set; }
    }
}