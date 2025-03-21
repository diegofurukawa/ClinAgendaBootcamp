using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinAgendaBootcamp.src.Core.Entities
{
    public class Specialty
        {
            public int Id { get; set; }
            public required string Name { get; set; }
            public int ScheduleDuration { get; set; }
        }
}