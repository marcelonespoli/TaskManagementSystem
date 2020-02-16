using AutoMapper;
using System;
using System.Collections.Generic;
using TMS.Application.Interfaces;
using TMS.Application.ViewModels;
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

        public IEnumerable<TaskViewModel> GetCompletedTasks()
        {
            var tasks = _taskRepository.Search(x => x.State == (int)States.Completed);
            return _mapper.Map<List<TaskViewModel>>(tasks);
        }
    }
}
