using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinAgendaBootcamp.src.Application.DTOs.Specialty
{
    public class SpecialtyInsertDTO
    {
        [Required(ErrorMessage = "O Nome da especialidade é obrigatório", AllowEmptyStrings = false)]
        public string @Name { get; set; }
        [Required(ErrorMessage = "O tempo de duração da especialidade é obrigatório", AllowEmptyStrings = false)]
        public int ScheduleDuration { get; set; }
    }
}