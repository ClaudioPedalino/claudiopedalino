﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TotvsChallenge.Entities
{
    public class Client
    {
        public Client(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}

