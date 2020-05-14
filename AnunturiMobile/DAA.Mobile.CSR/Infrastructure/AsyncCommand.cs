using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Anunturi.Mobile.Infrastructure
{
    public class AsyncCommand : Command
    {
        public AsyncCommand(Func<object, Task> execute) : base(param => execute(param).ConfigureAwait(false))
        {
        }

        public AsyncCommand(Func<Task> execute) : base(() => execute().ConfigureAwait(false))
        {
        }
    }
}
