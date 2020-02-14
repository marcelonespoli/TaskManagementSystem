using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TMS.Application.AppServices;
using TMS.Application.Interfaces;
using TMS.Domain.Core.Notifications;
using TMS.Domain.Handlers;
using TMS.Domain.Interfaces;
using TMS.Domain.Tasks.Commands;
using TMS.Domain.Tasks.Events;
using TMS.Domain.Tasks.Repository;
using TMS.Infra.Data.Context;
using TMS.Infra.Data.EventSourcing;
using TMS.Infra.Data.Repository;
using TMS.Infra.Data.Repository.EventSourcing;
using TMS.Infra.Data.UoW;

namespace TMS.Infra.CrossCutting.IoC    
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Application
            services.AddScoped<ITaskAppService, TaskAppService>();
            services.AddScoped<IReportAppService, ReportAppService>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<CreateTaskCommand, bool>, TaskCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateTaskCommand, bool>, TaskCommandHandler>();
            services.AddScoped<IRequestHandler<ExcludeTaskCommand, bool>, TaskCommandHandler>();
            services.AddScoped<IRequestHandler<AddSubtaskCommand, bool>, TaskCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateSubtaskCommand, bool>, TaskCommandHandler>();
            services.AddScoped<IRequestHandler<ExcludeSubtaskCommand, bool>, TaskCommandHandler>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<TaskCreatedEvent>, TaskEventHandler>();
            services.AddScoped<INotificationHandler<TaskUpdatedEvent>, TaskEventHandler>();
            services.AddScoped<INotificationHandler<TaskExcludedEvent>, TaskEventHandler>();            
            services.AddScoped<INotificationHandler<SubtaskAddedEvent>, TaskEventHandler>();            
            services.AddScoped<INotificationHandler<SubtaskUpdatedEvent>, TaskEventHandler>();            
            services.AddScoped<INotificationHandler<SubtaskExcludedEvent>, TaskEventHandler>();            

            // Infra - Data
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<TaskContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSqlContext>();
        }
    }
}
