﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class Member
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }


        public string Gender { get; set; }

        public DateTime dateJoined { get; set; }
    }
}
