﻿using banque1.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque1.DTO.interbanque
{
    public class interbanqueReturnDTO
    {
        public string Id { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string Nom { get; set; } = null!; 
    }
}