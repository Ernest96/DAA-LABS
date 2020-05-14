using BLL.DTO;
using Broker.Helpers;
using Refit;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Broker.Services
{
    public static class PushQueue
    {
        private static ConcurrentQueue<AnuntDto> _pushQueue;
        private const int DELAY_TIME = 30000; // 30 seconds
        private static IAnuntApi _anuntApi;

        public static void Init()
        {
            _anuntApi = RestService.For<IAnuntApi>(HttpClientFactory.Create(BrokerEnvironment.ApiBaseUrl));
            _pushQueue = new ConcurrentQueue<AnuntDto>();
            Task.Factory.StartNew(SendMessagesToApiWorker, TaskCreationOptions.LongRunning);
        }

        public static void Enqueue(AnuntDto anunt)
        {
            _pushQueue.Enqueue(anunt);
        }

        private static void SendMessagesToApiWorker()
        {
            while(true)
            {
                try
                {
                    var listToSend = new List<AnuntDto>();

                    while (!_pushQueue.IsEmpty)
                    {
                        var anunt = new AnuntDto();
                        if (_pushQueue.TryDequeue(out anunt))
                        {
                            listToSend.Add(anunt);
                        }
                    }

                    if (listToSend.Any())
                    {
                        _anuntApi.PublishAnunt(listToSend);
                    }
                }
                catch (Exception e)
                {
                    // Logger
                }
                finally
                {
                    Thread.Sleep(DELAY_TIME);
                }
            }
        }
    }
}