using Broker.Services;

namespace Broker.App_Start
{
    public static class PushQueueConfig
    {
        public static void InitQueue()
        {
            PushQueue.Init();
        }

    }
}