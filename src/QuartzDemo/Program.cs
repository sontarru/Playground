using Quartz;
using Quartz.AspNetCore;
using Quartz.Impl.Matchers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddQuartz();
builder.Services.AddQuartzServer();

var app = builder.Build();

//
// Я не хочу заморачиваться с настоящей аутентификацией -
// в демо-целях имя пользователя передаю в Path запроса.
//

//
// POST /user_ololo
//

app.MapPost(
    "/{user}",
    async (string user, ISchedulerFactory schedulerFactory, CancellationToken ct) =>
    {
        // Использовать планировщик по-умолчанию
        var scheduler = await schedulerFactory.GetScheduler(ct).ConfigureAwait(false);

        // Создаём Job с новым GUID для имени и именем пользователя для группы
        var job = JobBuilder.Create<DemoJob>()
            .WithIdentity(Guid.NewGuid().ToString(), user)
            .Build();

        // Создаём триггер с новым GUID для имени и именем пользователя для группы
        var trigger = TriggerBuilder.Create()
            .WithIdentity(Guid.NewGuid().ToString(), user)
            // триггер должен запустить Job сразу же
            .StartNow()
            .Build();

        // Привязать триггер к Job - это его запустит
        await scheduler.ScheduleJob(job, trigger, ct).ConfigureAwait(false);
    });

//
// GET /user_ololo
//

app.MapGet(
    "/{user}",
    async (string user, ISchedulerFactory schedulerFactory, CancellationToken ct) =>
    {
        var scheduler = await schedulerFactory.GetScheduler(ct).ConfigureAwait(false);

        // выбираем все задачи по имени пользователя, которое для каждой
        // задачи является именем группы
        var keys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(user), ct)
            .ConfigureAwait(false);

        return keys.Select(k => k.Name);
    });

//
// DELETE /user_ololo/83fd9dbb-9aeb-483b-8c72-90dc06247dd8
//

app.MapDelete(
    "/{user}/{jobid}",
    async (string user, Guid jobid, ISchedulerFactory schedulerFactory, CancellationToken ct) =>
    {
        var scheduler = await schedulerFactory.GetScheduler(ct).ConfigureAwait(false);

        // останавливаем задачу (посылаем ей Cancel)
        await scheduler.Interrupt(new JobKey(jobid.ToString(), user), ct).ConfigureAwait(false);
    });

await app.RunAsync().ConfigureAwait(false);

/// <summary>
/// Бесконечная задача.
/// </summary>
class DemoJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        while (true)
        {
            await Task.Delay(1000, context.CancellationToken).ConfigureAwait(false);
        }
    }
}
