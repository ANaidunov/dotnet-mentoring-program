﻿using System;
using System.Collections.Generic;
using System.Text;
using Task3.DoNotChange;

namespace Task3
{
    public interface IUserTaskService
    {
        public int AddTaskForUser(int userId, UserTask task);
    }
}
