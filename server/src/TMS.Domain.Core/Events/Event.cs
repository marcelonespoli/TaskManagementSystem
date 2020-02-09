﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Domain.Core.Events
{
    public class Event: Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
