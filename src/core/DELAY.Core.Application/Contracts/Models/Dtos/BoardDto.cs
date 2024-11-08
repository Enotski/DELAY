﻿using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Domain.Enums;

namespace DELAY.Core.Application.Contracts.Models.Dtos
{
    /// <summary>
    /// Board model
    /// </summary>
    public class BoardDto
    {
        public BoardDto()
        {
        }

        public Guid? Id { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public string? Description { get; set; }

        public IList<BoardUserDto>? Users { get; set; }
    }

    /// <summary>
    /// Board-user model
    /// </summary>
    public class BoardUserDto
    {
        public BoardUserDto()
        {
        }

        public KeyNameDto User { get; set; }

        public BoardRoleType UserRole { get; set; }
    }
}
