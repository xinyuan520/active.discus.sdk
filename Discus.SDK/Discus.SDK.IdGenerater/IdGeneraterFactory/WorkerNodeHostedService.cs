using Discus.SDK.Core.Interfaces;
using Discus.SDK.Core.System.Extensions;
using Microsoft.Extensions.Hosting;

namespace Discus.SDK.IdGenerater.IdGeneraterFactory
{
    public class WorkerNodeHostedService : BackgroundService
    {
        private readonly ILogger<WorkerNodeHostedService> _logger;
        private readonly string _serviceName;
        private readonly WorkerNode _workerNode;
        private readonly int _millisecondsDelay = 1000 * 60;

        public WorkerNodeHostedService(ILogger<WorkerNodeHostedService> logger, WorkerNode workerNode,IServiceInfo serviceInfo)
        {
            _logger = logger;
            _serviceName = serviceInfo.ServiceName;
            _workerNode = workerNode;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _workerNode.InitWorkerNodesAsync(_serviceName);
            var workerId = await _workerNode.GetWorkerIdAsync(_serviceName);
            try
            {
                IdGenerater.SetWorkerId((ushort)workerId);
                await base.StartAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await StopAsync(cancellationToken);
                _logger.LogError(ex,"IdGenerater-SetWorkerId异常",workerId);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);

            _logger.LogInformation("停止服务 {0}", _serviceName);

            var subtractionMilliseconds = 0 - (_millisecondsDelay * 1.5);
            var score = DateTime.Now.AddMilliseconds(subtractionMilliseconds).GetTotalMilliseconds();
            await _workerNode.RefreshWorkerIdScoreAsync(_serviceName, IdGenerater.CurrentWorkerId, score);

            _logger.LogInformation("退出服务 {0}:{1}", _serviceName, score);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ = Task.Factory.StartNew(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(_millisecondsDelay, stoppingToken);

                        if (stoppingToken.IsCancellationRequested) break;

                        await _workerNode.RefreshWorkerIdScoreAsync(_serviceName, IdGenerater.CurrentWorkerId);
                    }
                    catch (Exception ex)
                    {
                        var message = $"{nameof(ExecuteAsync)}:{_serviceName}-{IdGenerater.CurrentWorkerId}";
                        _logger.LogError(ex, message);
                        await Task.Delay(_millisecondsDelay / 3, stoppingToken);
                    }
                }
            }, stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            await Task.CompletedTask;
        }
    }
}
