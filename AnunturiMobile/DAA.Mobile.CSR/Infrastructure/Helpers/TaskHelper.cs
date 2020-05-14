using OperationResult;
using Refit;
using Anunturi.Mobile.AppStart;
using Anunturi.Mobile.Resources.Language;
using Anunturi.Mobile.Services.Common.Alerts;
using System;
using System.Threading.Tasks;
using static OperationResult.Helpers;

namespace Anunturi.Mobile.Infrastructure.Helpers
{
    public class TaskHelper
    {
        private readonly IAlertService _alertService;

        private Action _whenStarting;
        private Action _whenFinished;

        private TaskHelper()
        {
            _alertService = AutofacConfig.Resolve<IAlertService>();
        }

        public static TaskHelper Create()
        {
            return new TaskHelper();
        }

        public TaskHelper WhenStarting(Action action)
        {
            _whenStarting = action;
            return this;
        }

        public TaskHelper WhenFinished(Action action)
        {
            _whenFinished = action;
            return this;
        }

        public async Task<Status> TryWithErrorHandlingAsync(Func<Task> task, Func<Exception, Task<bool>> customErrorHandler = null)
        {
            var taskWrapper = new Func<Task<object>>(() => WrapTaskAsync(task));
            var result = await TryWithErrorHandlingAsync(taskWrapper, customErrorHandler);

            if (result)
            {
                return Ok();
            }

            return Error();
        }

        public async Task<Result<T>> TryWithErrorHandlingAsync<T>(Func<Task<T>> task, Func<Exception, Task<bool>> customErrorHandler = null)
        {
            _whenStarting?.Invoke();

            try
            {
                T actualResult = await task();
                return Ok(actualResult);
            }
            catch (ApiException apiException)
            {
                if (customErrorHandler == null || !await customErrorHandler?.Invoke(apiException))
                {
                    _alertService.DisplayErrorAlert(LanguageResources.RequestGenericExceptionMessage);
                }
            }
            catch (Exception exception)
            {
                if (customErrorHandler == null || !await customErrorHandler?.Invoke(exception))
                {
                    _alertService.DisplayErrorAlert(LanguageResources.GenericExceptionMessage);
                }
            }
            finally
            {
                _whenFinished?.Invoke();
            }

            return Error();
        }

        private async Task<object> WrapTaskAsync(Func<Task> innerTask)
        {
            await innerTask();

            return new object();
        }
    }
}
