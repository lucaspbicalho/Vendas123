﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Vendas123.Domain.Entites;

namespace Vendas123.Domain.ViewModel
{
    public class VendaUpdateViewModel
    {
        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, Double.MaxValue, ErrorMessage = "O campo {0} precisa ser maior que {0.01}.")]
        public decimal Valor { get; set; }
        [Range(0, 2, ErrorMessage = "O campo {0} precisa ser entre 0 e 2.")]
        [DefaultValue(1)]
        public int Filial { get; set; }
        public ClienteViewModel Cliente { get; set; }

        public static implicit operator Venda(VendaUpdateViewModel vendaVM)
        {
            return new Venda
            {
                Id = Guid.NewGuid(),
                Valor = vendaVM.Valor,
                Filial = (Filial)vendaVM.Filial,
                Cliente = vendaVM.Cliente,
            };
        }
    }
}
