using OperationResult;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static OperationResult.Helpers;

namespace AnunturiDesktop.Helpers
{
    public static class TaskHelper
    {
        public static async Task<bool> TryWithErrorHandlingAsync(Func<Task> task, Func<Exception, Task<bool>> customErrorHandler = null)
        {
            var taskWrapper = new Func<Task<object>>(() => WrapTaskAsync(task));
            var result = await TryWithErrorHandlingAsync(taskWrapper, customErrorHandler);

            return result;
        }

        public static async Task<Result<T>> TryWithErrorHandlingAsync<T>(Func<Task<T>> task, Func<Exception, Task<bool>> customErrorHandler = null)
        {
            try
            {
                T actualResult = await task();
                return actualResult;
            }
            catch (ApiException apiException)
            {
                MessageBox.Show($"Error: {apiException.Message}. {apiException.Content}");
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error: {exception.Message}.");
            }
            finally
            {
                
            }

            return Error();
        }

        private static async Task<object> WrapTaskAsync(Func<Task> innerTask)
        {
            await innerTask();

            return new object();
        }
    }
}
