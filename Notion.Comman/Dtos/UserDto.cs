﻿using System;
using System.Collections;
using System.Collections.Generic;
using Notion.DAL.Entity.Concrete;

namespace Notion.Comman.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}