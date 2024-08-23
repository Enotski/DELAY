﻿using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Contracts.Models.Base
{
    public class KeyDto : IKey
    {
        public KeyDto() { }

        public KeyDto(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
