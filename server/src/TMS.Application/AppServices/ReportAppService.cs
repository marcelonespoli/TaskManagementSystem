using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMS.Application.Interfaces;
using TMS.Domain;
using TMS.Domain.Tasks.Repository;

namespace TMS.Application.AppServices
{
    public class ReportAppService : IReportAppService
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;

        public ReportAppService(
            IMapper mapper,
            ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public FileStreamResult GetInProgressTasksByDate(DateTime? date)
        {
            var tasks = _taskRepository.Search(x => x.State == (int)States.InProgress && x.StartDate == date);
            var csvData = ConvertToCsvFormat(tasks);

            return CreateFileStream(csvData, "InProgress");
        }

        public FileStreamResult GetCompletedTasks()
        {
            var tasks = _taskRepository.Search(x => x.State == (int)States.Completed);
            var csvData = ConvertToCsvFormat(tasks);

            return CreateFileStream(csvData, "Completed");
        }

        private FileStreamResult CreateFileStream(string data, string title)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(data));

            return  new FileStreamResult(stream, "text/plain")
                    {
                        FileDownloadName = $"Report_{title}_{DateTime.Now}.csv"
                    };

        }

        private string ConvertToCsvFormat(IEnumerable<TaskData> tasks) 
        {
            var sb = new StringBuilder();

            sb.Append("Name,Description,Start Date,Finish Date,State\r\n");

            foreach (var task in tasks)
            {
                sb.AppendFormat("{0},", task.Name);
                sb.AppendFormat("{0},", task.Description);
                sb.AppendFormat("{0},", task.StartDate?.ToShortDateString());
                sb.AppendFormat("{0},", task.FinishDate?.ToShortDateString());
                sb.AppendFormat("{0}\r\n", task.State.ToString());
            }

            return sb.ToString();
        }
    }
}
