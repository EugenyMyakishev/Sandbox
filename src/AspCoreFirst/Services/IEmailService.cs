﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreFirst.Services
{
    public interface IEmailService
    {
        Task SendAsync(string email, string subject, string message);
    }
}
