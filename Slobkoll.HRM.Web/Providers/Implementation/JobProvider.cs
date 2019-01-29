﻿using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Core.Repository.Interface;
using Slobkoll.HRM.Web.Providers.Interface;
using System;
using System.Collections.Generic;

namespace Slobkoll.HRM.Web.Providers.Implementation
{
    public class JobProvider : IJobProvider
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ISubTaskRepository _subTaskRepository;
        public JobProvider(ITaskRepository taskRepository, ISubTaskRepository subTaskRepository)
        {
            _taskRepository = taskRepository;
            _subTaskRepository = subTaskRepository;
        }
        public void JobTaskCheak()
        {
            IList<Task> ListTask = _taskRepository.LoadTaskAllAct();
            if (ListTask != null)
            {
                foreach (var item in ListTask)
                {
                    if (item.DateEnd <= DateTime.Now)
                    {
                        bool done = true;
                        foreach (var itemsub in item.SubTask)
                        {
                            if (itemsub.Status != "Выполнено")
                            {
                                done = false;
                            }
                        }
                        if (done)
                        {
                            item.Status = "Выполнено";
                            item.Change = false;
                            _taskRepository.TaskUpdate(item);
                        }
                        else
                        {
                            item.Status = "Не выполнено";
                            item.Change = false;
                            _taskRepository.TaskUpdate(item);
                        }
                    }
                }
            }
        }

    }
}