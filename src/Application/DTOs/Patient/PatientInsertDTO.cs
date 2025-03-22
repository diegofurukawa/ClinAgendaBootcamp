using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ClinAgendaBootcamp.src.Application.DTOs.Patient
{
    public class PatientInsertDTO
    {

        [Required(ErrorMessage = "Por favor insira um Nome.")]
        [StringLength(250)]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Por favor insira um Telefone.")]
        [StringLength(20)]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Por favor insira um Documento.")]
        [StringLength(50)]
        public required string DocumentNumber { get; set; }

        [Required(ErrorMessage = "Por favor insira um Status.")]
        [IntegerValidator]
        public required int StatusId { get; set; }
        
        [Required(ErrorMessage = "Por favor insira um Data.")]
        [StringLength(20)]
        public required string BirthDate { get; set; }
    }
}