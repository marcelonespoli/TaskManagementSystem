using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMS.Domain;
using TMS.Domain.Tasks.Repository;
using TMS.Infra.Data.Context;

namespace TMS.Infra.Data.Repository
{
    public class TaskRepository : Repository<TaskData>, ITaskRepository
    {
        public TaskRepository(TaskContext context) : base(context)
        {
        }

        // Used Dapper package for some queries
        // Dapper can have more query performance than the EF

        public override IEnumerable<TaskData> GetAll()
        {
            var sql = @"SELECT * FROM Tasks T " +
                      "ORDER BY T.FinishDate DESC";

            return Db.Database.GetDbConnection().Query<TaskData>(sql);
        }

        public override TaskData GetById(Guid id)
        {
            var sql = @"SELECT * FROM Tasks T " +
                      "LEFT JOIN Subtasks S " +
                      "ON T.Id = S.TaskId " +
                      "WHERE T.Id = @tid";

            var conference = Db.Database.GetDbConnection().Query<TaskData, Subtask, TaskData>(sql,
                (t, s) =>
                {
                    if (s != null)
                        t.Subtasks.Add(s);

                    return t;
                }, new { tid = id });

            return conference.FirstOrDefault();
        }

        public Subtask GetSubtaskById(Guid id)
        {
            var sql = @"SELECT * FROM Subtasks S " +
                       "WHERE S.Id = @uid";

            var subtask = Db.Database.GetDbConnection().Query<Subtask>(sql, new { uid = id });

            return subtask.SingleOrDefault();
        }

        public IEnumerable<Subtask> GetSubtasksByTaskId(Guid taskId)
        {
            var sql = @"SELECT * FROM Subtasks S " +
                       "WHERE S.TaskId = @tid";

            return Db.Database.GetDbConnection().Query<Subtask>(sql, new { tid = taskId });
        }

        public void AddSubtask(Subtask subtask)
        {
            Db.Subtasks.Add(subtask);
        }

        public void UpdateSubtask(Subtask subtask)
        {
            Db.Subtasks.Update(subtask);
        }

        public void RemoveSubtask(Guid id)
        {
            Db.Subtasks.Remove(Db.Subtasks.Find(id));
        }

        
    }
}
