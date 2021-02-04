﻿using System.Collections.Generic;

namespace SortTut.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<User> Users { get; set; }

        public Company()
        {
            Users = new List<User>();
        }
    }
}