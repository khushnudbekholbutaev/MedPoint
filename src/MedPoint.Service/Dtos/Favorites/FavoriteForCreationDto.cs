﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Service.Dtos.Favorites
{
    public class FavoriteForCreationDto
    {
        public int UserId { get; set; }
        public int MedicationId { get; set; }
    }
}
