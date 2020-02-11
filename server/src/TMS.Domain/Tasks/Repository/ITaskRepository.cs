using System;
using System.Collections.Generic;
using TMS.Domain.Interfaces;

namespace TMS.Domain.Tasks.Repository
{
    public interface ITaskRepository : IRepository<TaskData>
    {
        Subtask GetSubtaskById(Guid id);        
        void AddSubtask(Subtask subtask);
        void UpdateSubtask(Subtask subtask);
        void RemoveSubtask(Guid id);
        IEnumerable<Subtask> GetSubtasksByTaskId(Guid taskId);
    }
}
